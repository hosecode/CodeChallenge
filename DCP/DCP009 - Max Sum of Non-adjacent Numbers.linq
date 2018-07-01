<Query Kind="Program" />

/*
This problem was asked by Airbnb.

Given a list of integers, write a function that returns the largest sum of non-adjacent numbers. Numbers can be 0 or negative.

For example, [2, 4, 6, 2, 5] should return 13, since we pick 2, 6, and 5. [5, 1, 1, 5] should return 10, since we pick 5 and 5.

Follow-up: Can you do this in O(N) time and constant space?
*/
void Main()
{
	Solve(new int[] { 2, 4, 6, 2, 5 }).Dump("Expect: 2,6,5");
	Solve(new int[] { 5, 1, 1, 5 }).Dump("Expect: 5, 5");
}

List<int> Solve(int[] arr){
	throw new NotImplementedException();
}
// Define other methods and classes here
