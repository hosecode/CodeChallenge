<Query Kind="Program" />

/*
This problem was asked by Amazon.

Given an integer k and a string s, find the length of the longest substring that contains at most k distinct characters.

For example, given s = "abcba" and k = 2, the longest substring with k distinct characters is "bcb".
*/
//Note: clarify: asked for length, but example shows substring
void Main()
{
	BruteForce(0, "abcba").Dump("Expect 0");
	BruteForce(1, "abcba").Dump("Expect 1");

	BruteForce(2, "abcba").Dump("Expect 3");
	BruteForce(3, "abcba").Dump("Expect 5");

}

// Define other methods and classes here


//param k: max allowed distinct characters
//returns length of longest substring
int BruteForce(int k, string s){
	int maxLength=0;
	HashSet<char> distinct = new HashSet<char>();
	int j;
	for (int i=0; i< s.Length; i++){
		for (j = i; j < s.Length; j++)
		{
			if (distinct.Add(s[j])) {
				if (distinct.Count > k){
					break;
				}
			}
		}
		if (j-i> maxLength){
			s.Substring(i, j-i).Dump();
			maxLength = j-i;
		}
		distinct.Clear();
	}
	return maxLength;
}
