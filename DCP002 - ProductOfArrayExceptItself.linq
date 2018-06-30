<Query Kind="Program" />

/*
Given an array of integers, return a new array such that each element at index i of the new array is the product of all the numbers in the original array except the one at i.

For example, if our input was [1, 2, 3, 4, 5], the expected output would be [120, 60, 40, 30, 24]. If our input was [3, 2, 1], the expected output would be [2, 3, 6].

Follow-up: what if you can't use division?
*/

void Main()
{
	ProductArray(new int[] { 1, 2, 3, 4, 5 }).Dump("Expected [120, 60, 40, 30, 24]");
	ProductArray(new int[] { 3, 2, 1 }).Dump("Expected [2, 3, 6]");

	"".Dump("No Division");
	ProductArrayNoDivision(new int[] { 1, 2, 3, 4, 5 }).Dump("Expected [120, 60, 40, 30, 24]");
	ProductArrayNoDivision(new int[] { 3, 2, 1 }).Dump("Expected [2, 3, 6]");
}

int[] ProductArray(int[] input){
	int product=1;
	for (int i = 0; i < input.Length; i++)
	{
		product*=input[i];
	}
	int[] output = new int[input.Length];
	for (int i = 0; i < input.Length; i++)
	{
		output[i] = product/input[i];
	}
	return output;
}

int[] ProductArrayNoDivision(int[] input)
{
	int product1 = 1;
	int product2 = 1;
	int[] a1 = new int[input.Length];
	int[] a2 = new int[input.Length];
	
	for (int i = 0; i < input.Length; i++)
	{
		a1[i] = product1; 
		product1 *= input[i];

		a2[input.Length-1-i] = product2;
		product2 *= input[input.Length-1-i];
	}
	
	int[] output = new int[input.Length];
	for (int i = 0; i < input.Length; i++)
	{
		output[i] = a1[i]*a2[i];
	}
	return output;
}