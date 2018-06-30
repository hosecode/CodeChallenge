<Query Kind="Program" />

/*
Good morning! Here's your coding interview problem for today.

This problem was asked by Stripe.

Given an array of integers, find the first missing positive integer in linear time and constant space. 
In other words, find the lowest positive integer that does not exist in the array. 
The array can contain duplicates and negative numbers as well.

For example, the input [3, 4, -1, 1] should give 2. The input [1, 2, 0] should give 3.
You can modify the input array in-place.
*/
void Main()
{
	Solve(new int[] {3, 4, -1, 1}).Dump("Expect: 2");
	Solve(new int[] { 1, 2, 0 }).Dump("Expect: 3");
}

int Solve(int[] input)
{
	for (int i=0; i < input.Length; i++)
	{
		if (!((input[i] == i+1) || (input[i] < 1 || input[i] >= input.Length))) {
			swap(input, i, input[i]-1);
		}
	}
	
	int j;
	for (j=0; j<input.Length; j++){
		if (input[j]!=j+1) break;
	}
	return j+1;
}

void swap(int[] input, int a, int b){
	int t=input[b];
	input[b]=input[a];
	input[a] = t;
}