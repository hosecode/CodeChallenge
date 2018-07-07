<Query Kind="Program" />

/*
Maximize stock trade profit

Stock prices at each minute from yesterday are put in a list called stock_prices

Write an efficient function that takes stock_prices and returns the best profit that could have made from one purchase and one sale of one share of stock yesterday.

For example:

  stock_prices = [10, 7, 5, 8, 11, 9]

get_max_profit(stock_prices)
# Returns 6 (buying for $5 and selling for $11)

No "shorting"—you need to buy before you can sell. Also, you can't buy and sell in the same time step—at least 1 minute has to pass.
*/
void Main()
{

	get_max_profit(new int[] { 10, 7, 5, 8, 11, 9 }).Dump("Expect: 6");
	get_max_profit(new int[] { 10, 10, 10, 10, 10, 10 }).Dump("Expect: 0");
	get_max_profit(new int[] { 9, 8, 7, 6, 5, 4 }).Dump("Expect: 0");
	get_max_profit(new int[] { 5, 8, 7, 6, 5, 10 }).Dump("Expect: 5");
	get_max_profit(new int[] { 5, 8, 10, 4, 5, 10 }).Dump("Expect: 6");
	get_max_profit(new int[] { 3, 8, 10, 4, 5, 10 }).Dump("Expect: 7");
}

//The question specified a single pair(buy&sell) of trades, not multiple sets of trades thoughout the day
int get_max_profit(int[] prices){
	
	int max=0;//the max profit would could make selling so far
	int min=int.MaxValue; //the min price we could purchase at so far
	for(int i=0; i<prices.Length; i++){
		//if we could increase profit by selling now
		if (prices[i]-min > max) max = prices[i]-min;
		//if this is a new lower purchase price
		if (prices[i]<min) min = prices[i];
	}
	return max;
}