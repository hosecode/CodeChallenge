<Query Kind="Program" />

/*
This problem was asked by Facebook.

Given the mapping a = 1, b = 2, ... z = 26, and an encoded message, count the number of ways it can be decoded.

For example, the message '111' would give 3, since it could be decoded as 'aaa', 'ka', and 'ak'.

You can assume that the messages are decodable. For example, '001' is not allowed.
*/
void Main()
{
	Count("111".ToCharArray(), 0, 2).Dump();
}

int Count(char[] msg, int start, int end){
	if (end-start==0) return 0;
	if (end-start==1) return 1;
	return 2 + Count(msg, start+1, end) + Count(msg, start+2 ,end);
}
// Define other methods and classes here
