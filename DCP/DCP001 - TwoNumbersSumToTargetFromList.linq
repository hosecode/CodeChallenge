<Query Kind="Program" />

/*
Given a list of numbers and a number k, return whether any two numbers from the list add up to k.

For example, given [10, 15, 3, 7] and k of 17, return true since 10 + 7 is 17.

Bonus: Can you do this in one pass?

*/
void Main()
{
	int[] set = new int[]{10, 15, 3, 7};
	int target = 17;
	
	DoesSum(target, set).Dump();
}

// Define other methods and classes here

bool DoesSum(int target, int[] set)
{
	HashSet<int> s = new HashSet<int>();
	for (int i = 0; i < set.Length; i++)
	{
		if (s.Contains(set[i])) return true;
		s.Add(target - set[i]);
	}

	return false;
}