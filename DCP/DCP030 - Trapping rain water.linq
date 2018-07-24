<Query Kind="Program" />

/*
This problem was asked by Facebook.

You are given an array of non-negative integers that represents a two-dimensional elevation map where each element is unit-width wall and the integer is the height. 
Suppose it will rain and all spots between two walls get filled up.

Compute how many units of water remain trapped on the map in O(N) time and O(1) space.

For example, given the input [2, 1, 2], we can hold 1 unit of water in the middle.

Given the input [3, 0, 1, 3, 0, 5], we can hold 3 units in the first index, 2 in the second, 
and 3 in the fourth index (we cannot hold 5 since it would run off to the left), so we can trap 8 units of water.
*/
void Main()
{
	Solve(new int[] { 2, 1, 2 }).Dump("Expect: 1");
	Solve(new int[] { 1, 0, 1 }).Dump("Expect: 1");
	Solve(new int[] { 2, 0, 1 }).Dump("Expect: 1");
	Solve(new int[] { 1, 0, 2 }).Dump("Expect: 1");
	Solve(new int[] { 2, 2 }).Dump("Expect: 0");
	Solve(new int[] { 3, 0, 1, 3, 0, 5 }).Dump("Expect: 8");
	Solve(new int[] { 3, 0, 1, 2, 0, 5 }).Dump("Expect: 9");
	Solve(new int[] { 3, 0, 1, 2 }).Dump("Expect: 3");
	Solve(new int[] { 5, 4, 0, 1 }).Dump("Expect: 1");
	Solve(new int[] { 2, 0, 0, 1 }).Dump("Expect: 2");
	Solve(new int[] { 1, 2, 3, 4 }).Dump("Expect: 0");
	Solve(new int[] { 4, 3, 2, 1 }).Dump("Expect: 0");
	Solve(new int[] { 4, 3, 0, 1 }).Dump("Expect: 1");

}

int Solve(int[] input){
	int result=0;
	
	int lmax=0;
	int rmax=0;
	
	int lo = 0;
	int hi = input.Length - 1;
	while (hi >= lo)
	{
		if (input[lo] < input[hi]) {
			if (input[lo]>lmax){
				lmax=input[lo];
			}
			else{
				result+=lmax-input[lo];
			}
			lo++;
		}
		else
		{
			if (input[hi] > rmax)
			{
				rmax = input[hi];
			}
			else
			{
				result += rmax - input[hi];
			}
			hi--;
		}


	}
	return result;
}