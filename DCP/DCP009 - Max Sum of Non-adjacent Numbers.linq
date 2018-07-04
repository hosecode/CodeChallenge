<Query Kind="Program" />

/*
This problem was asked by Airbnb.

Given a list of integers, write a function that returns the largest sum of non-adjacent numbers. Numbers can be 0 or negative.

For example, [2, 4, 6, 2, 5] should return 13, since we pick 2, 6, and 5. [5, 1, 1, 5] should return 10, since we pick 5 and 5.

Follow-up: Can you do this in O(N) time and constant space?
*/
void Main()
{
	Solve(new int[] { 2, 4, 6, 2, 5 }).Dump("Expect: 13");
	Solve(new int[] { 5, 1, 1, 5 }).Dump("Expect: 10");
	Solve(new int[] { }).Dump("Empty Test. Expect: 0"); //clarify question specs
	Solve(new int[] { 7 }).Dump("Single Test. Expect: 7");
	Solve(new int[] { 1, 3 }).Dump("Pair Test 2nd. Expect: 3");
	Solve(new int[] { 3, 1 }).Dump("Pair Test 1st. Expect: 3");
	Solve(new int[] { 2, 3, 7 }).Dump("Triplet Test 1st. Expect: 9");
	Solve(new int[] { 2, 7, 3 }).Dump("Triplet Test 2st. Expect: 7");
	Solve(new int[] { 2, 3, 2 }).Dump("Delta 1 Test 1st. Expect: 4");
	Solve(new int[] { 2, 5, 2 }).Dump("Delta 1 Test 2st. Expect: 5");
	Solve(new int[] { 2, 0, 2 }).Dump("Delta 1 Test 2st. Expect: 4");
	Solve(new int[] { 6, 5, -1 }).Dump("Neg. Expect: 6");
	Solve(new int[] { -1, 5, -1 }).Dump("Neg. Expect: 5");
	Solve(new int[] { -1, }).Dump("Neg. Expect: 0"); //clarify, must an element be picked to be a sum  
	Solve(new int[] { 1, 2, 3, 4 }).Dump("inc. Expect: 6");

}

int Solve(int[] arr)
{
	//doubly linked so O(1) insert/remove at head/tail
	LinkedList<int> prev = new LinkedList<int>();
	prev.AddLast(0);
	prev.AddLast(0);
	for (int i = 0; i < arr.Length; i++)
	{
		prev.AddLast(Math.Max(arr[i] + prev.Last.Previous.Value, prev.Last.Value));
		prev.RemoveFirst();	//Keep memory use constant by purging old results. 
	}
	return prev.Last.Value;
}