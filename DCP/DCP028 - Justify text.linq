<Query Kind="Program" />

/*
This problem was asked by Palantir.

Write an algorithm to justify text. Given a sequence of words and an integer line length k, return a list of strings which represents each line, fully justified.

More specifically, you should have as many words as possible in each line. There should be at least one space between each word. 
Pad extra spaces when necessary so that each line has exactly length k. 
Spaces should be distributed as equally as possible, with the extra spaces, if any, distributed starting from the left.

If you can only fit one word on a line, then you should pad the right-hand side with spaces.

Each word is guaranteed not to be longer than k.

For example, given the list of words ["the", "quick", "brown", "fox", "jumps", "over", "the", "lazy", "dog"] and k = 16, you should return the following:

["the  quick brown", # 1 extra space on the left
"fox  jumps  over", # 2 extra spaces distributed evenly
"the   lazy   dog"] # 4 extra spaces distributed evenly
*/
void Main()
{
	Justify(new List<string>() { "the", "quick", "brown", "fox", "jumps", "over", "the", "lazy", "dog", "finally" }, 16).Dump("EXPECT:\n" +
		"'the++quick+brown', # 1 extra space on the left\n" +
		"'fox++jumps++over', # 2 extra spaces distributed evenly\n" +
		"'the+++lazy+++dog', # 4 extra spaces distributed evenly\n" +
		"'finally+++++++++', # 9 extra spaces on the right"
	);
}


//k is line length
List<string> Justify(List<string> words, int k){
	List<string> result = new List<string>();
	Line line = new Line(k);
	foreach (var word in words)
	{
		if (!line.AddWord(word)){
			//current line is full.
			result.Add(line.ToString());
			//start new line
			line = new Line(k);
			line.AddWord(word);//Each word is guaranteed not to be longer than k.
		}
	}
	//a final line
	result.Add(line.ToString()); 
	return result;
}

class Line{
	char paddingChar = '+'; //for visibility
	int LineLength;
	List<string> words = new List<string>();
	int length;
	public Line(int lineLength)
	{
		this.LineLength = lineLength;
	}
	public bool AddWord(string word){
		//new word +current length + requird space
		if (word.Length + length + words.Count() > LineLength) return false;
		words.Add(word);
		length+=word.Length;
		return true;
	}
	public override string ToString()
	{
		//If you can only fit one word on a line, then you should pad the right-hand side with spaces.
		if (words.Count==1) return words[0].PadRight(LineLength, paddingChar);
		//Add padding
		int index=0;	//track which word to add padding to. 
		while(length<LineLength){
			words[((index++)%(words.Count-1))] += paddingChar;	//dont pad the last word of the line
			length++;
		}
		return string.Concat(words);
	}

}