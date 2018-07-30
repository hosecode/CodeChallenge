<Query Kind="Program" />

/*
Perfect information black jack, knowing the order of all cards in the deck maximize profit
*assume bet is constant +1 win, 0 push/draw, -1 lose / hand
*assume no splits or double downs etc. simplified rules you may hit or stand.
*dealer is required to hit <17 and stand on 17 or greater
*bust on 21
*Ace is 1 or 11
*if player busts or blackjack, dealer doesn't draw additional cards
*if dealer has blackjack player doesnt draw cards and loses, unless player also has BJ then its a push
*black jack beats 21.
*scoring for win for a blackjack is same as normal hand win
*/
/*
I'll assume if we run out of cards the game ends? and the last bet is refunded. IE. if dealer can't complete a valid hand, since their rules are deterministic.

This needs a big refactor for cleanliness. More test for multigame result verification. remove unused code. Decide whether to go more object-oriented or not. 
*/
void Main()
{
	Deck deck = Deck.Build();
	//deck.Dump("New Deck");
	deck.Shuffle();
	//deck.Dump("Shuffled");
	//deck.GetCards();
	//	
	//	Ace().Dump();
	//	King().Dump();
//	NumberCard(2).Dump();
//	NumberCard(10).Dump();

	//Single hand games
	List<Card> cards = new List<Card>(){Ace(), Ace(), King(), King()};
	MaxProfit(cards, 0).Dump("Dealer & Player Blackjack Expect: 0");
	cards = new List<Card>() { Ace(), King(), King(), King() };
	MaxProfit(cards, 0).Dump("Dealer Blackjack Expect: -1");
	cards = new List<Card>() { King(), Ace(), King(), King() };
	MaxProfit(cards, 0).Dump("Player Blackjack Expect: 1");
	cards = new List<Card>() { Ace(), Ace(), King()};
	MaxProfit(cards, 0).Dump("Not enough cards, no hands played. Expect: 0");
	cards = new List<Card>() { Ace(), King(), King(), King(), Ace() };
	MaxProfit(cards, 0).Dump("Dealer Blackjack, player 21 Expect: -1");
	cards = new List<Card>() { King(), King(), King(), King(), Ace() };
	MaxProfit(cards, 0).Dump("Dealer 20, player 21 Expect: 1");
	
	//Multigames
	cards = new List<Card>() { King(), King(), King(), King(), Ace(), King(), King(), King(), King(), Ace()};
	MaxProfit(cards, 0).Dump("2games Dealer 20, player 21 Expect: 2");

	cards = new List<Card>() { King(), King(), King(), King(), Ace(), King(), King(), King(),King() };
	MaxProfit(cards, 0).Dump("2games Dealer 20, player 21 Expect: 2");

	//ShowHands=true;
	parentPointer=new Stack<int>();
	memo = new Cache();
	MaxProfit(deck.GetCards(), 0).Dump("Full Deck. Expect: ?");
	memo.Stats().Dump("Cache Stats");
	parentPointer.Dump();
	deck.GetCards().Dump();
}

Stack<int> parentPointer;
Cache memo = null;
bool ShowHands = false;

Card Ace(){
	return new Card(0);
}
Card King()
{
	return new Card(12);
}
Card NumberCard(int num){
	if (num<2 || num>10) throw new ArgumentException();
	return new Card(num-1);
}

class Cache{
	int hits=0;
	int lookups = 0;
	Dictionary<int, int> cache = new Dictionary<int, int>();
	public bool Lookup(int key, out int value){
		lookups++;
		if (!cache.TryGetValue(key , out value)) return false;
		
		hits++; 
		return true;
	}
	public void Add(int key, int value){
		cache.Add(key, value);
	}
	public string Stats()
	{
		return $"Hits: {hits}, Lookups: {lookups}, Size: {cache.Keys.Count}";
	}
}

//returns max score ass
int MaxProfit(IList<Card> deck, int index){
	
	int result;
	if (memo!=null && memo.Lookup(index, out result)) return result;
	if (index>=deck.Count) return 0;

	//deal
	int hitCount=0;
	//since we must play the hand, we must take and mimimize a loss
	int? profit=null;
	//try all hit while possible
	for (int hits = 0; index+4+hits <= deck.Count; hits++)
	{
		//score this hand
		int dealerHits;
		int score = ScoreHand(deck, index, hits, out dealerHits);

		int p = score + MaxProfit(deck, index + 4 + hits + Math.Max(0, dealerHits));
		if (p > (profit ?? int.MinValue))
		{
			profit = p;
			hitCount=hits;
		}

		//signal to stop taking hits, because of had blackjack or bust
		if (dealerHits == -1) break;
	} 
	
	parentPointer?.Push(hitCount);	//if we want to reconstruct how to play the winner games
	profit = profit??0;
	memo?.Add(index, profit.Value);
	return profit.Value;
	
}

const int PlayerLosesHand = -1;
const int PushHand = 0;
const int PlayerWinsHand = 1;

int ScoreHand(IList<Card> deck, int index, int hits, out int dealerHits)
{
	int result=-99;
	dealerHits = -1;
	if (index + 4 + hits > deck.Count) return result=PushHand;   //run out of cards to play hand
	List<Card> dealer = new List<Card>();
	List<Card> player = new List<Card>();
	try
	{

		dealer.Add(deck[index++]);
		player.Add(deck[index++]);
		dealer.Add(deck[index++]);
		player.Add(deck[index++]);

		if (Blackjack.IsBlackjack(dealer))
			if (Blackjack.IsBlackjack(player))
				return result = PushHand;
			else
				return result = PlayerLosesHand;

		if (Blackjack.IsBlackjack(player)) return result = PlayerWinsHand;

		while (hits-- > 0)
		{
			player.Add(deck[index++]);
		}
		//if bust return 0
		// dealer does not draw more cards
		if (Blackjack.IsBust(player)) return result = PlayerLosesHand;

		//dealer hit
		dealerHits = 0;
		while (!Blackjack.IsBust(dealer) && Blackjack.BestHandValue(dealer) < 17)
		{
			if (index >= deck.Count) return result = PushHand; //no more cards
			dealer.Add(deck[index++]);
		}

		int playerScore = Blackjack.BestHandValue(player);
		int dealerScore = Blackjack.BestHandValue(dealer);
		if (playerScore > dealerScore)
			return result = PlayerWinsHand;
		else if (playerScore < dealerScore)
			return result = PlayerLosesHand;
		else
			return result = PushHand;
	}
	finally
	{
		if (ShowHands) new BlackjackHand(player, dealer, result).Dump("Hand Played");
	}
}

struct BlackjackHand{
	public IList<Card> PlayerHand;
	public IList<Card> DealerHand;
	public int Result;
	public BlackjackHand(IList<Card> playerHand, IList<Card> dealerHand, int result){
		this.PlayerHand=playerHand;
		this.DealerHand = dealerHand;
		this.Result=result;
	}
}

class Blackjack{


	//Highest valid scoring hand value
	public static int BestHandValue(ICollection<Card> cards)
	{
		return PossibleHandValues(cards).DefaultIfEmpty(0).Max();
	}
	public static bool IsBlackjack(ICollection<Card> cards){
		return (cards.Count==2 && BestHandValue(cards)==21) ;
	}
	public static bool IsBust(ICollection<Card> cards){
		return BestHandValue(cards)==0;
	}
	//all possible hand scores. 
	public static List<int> PossibleHandValues(ICollection<Card> cards){
		int AceCount=0;
		int handValue=0;
		foreach (var card in cards)
		{
			handValue+=Math.Min(10,card.RankValue());
			if (card.IsAce()) AceCount++;
		}
		List<int> handValues = new List<int>();
		
		if (IsBust(handValue)) return handValues;
		handValues.Add(handValue);
		
		while(AceCount>0 && !IsBust(handValue+10)){
			AceCount--;
			handValue+=10;	//1 + 10 == 11
			handValues.Add(handValue);
		}
		
		return handValues;
	}
	static bool IsBust(int handValue){
		return handValue>21;
	}
	public static int CardValue(Card card, bool aceHigh = true)
	{
		if (aceHigh && card.IsAce()) return 11;
		return Math.Min(card.RankValue(), 10);
	}
}
class Hand{
	List<Card> cards = new List<Card>();
	bool isDealer;
	public Hand(bool isDealer){
		this.isDealer = isDealer;
	}
	public void AddCard(Card card){
		cards.Add(card);
	}
	
}
class Card {
	int id;
	public Card(int card){
		if (card<0 || card>=52) throw new ArgumentException();
		id = card;
	}
	public override string ToString()
	{
		return string.Empty + Rank() + Suit();
	}
	public int RankValue(){
		return (id%13)+1;
	}
	char Rank()
	{
		switch (RankValue())
		{
			case 1:
				return 'A';
			case 13:
				return 'K';
			case 12:
				return 'Q';
			case 111:
				return 'J';
			case 10:
				return 'X'; //Ten
			default:
				return (RankValue()).ToString()[0];
		}
	}
	public char Suit()
	{
		switch (id / 13)
		{
			case 0:
				return '♦';
			case 1:
				return '♠';
			case 2:
				return '♥';
			case 3:
				return '♣';
		}
		throw new ArgumentException();
	}
	public bool IsAce()
	{
		return RankValue() == 1;
	}
	
}

class Deck
{
	//using 0 because I want determinisitic results between runs.
	Random rnd = new Random(0);
	Card[] cards;
	int dealt=0;
	public Card Deal(){
		if (cards.Length >= dealt) return null;
		return cards[dealt++];
	}
	public int RemainingCards(){
		return cards.Length-dealt;
	}
	public static Deck Build()
	{
		Deck deck = new Deck();
		deck.cards = new Card[52];
		Card[] cards = deck.cards;
		for (int card = 0; card < 52; card++)
		{
			cards[card] = new Card(card);
		}
		return deck;
	}
	public void Shuffle()
	{
		dealt=0;
		for (int i = 0; i < 10000; i++)
		{
			int a = rnd.Next(0, 52);
			int b = rnd.Next(0, 52);
			Card t = cards[a];
			cards[a] = cards[b];
			cards[b] = t;
		}
	}
	public Card[] GetCards(){
		return cards;
	}
	object ToDump()
	{
		return cards;
	}
}