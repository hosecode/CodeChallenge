<Query Kind="Program" />

/*
This problem was asked by Amazon.

There exists a staircase with N steps, and you can climb up either 1 or 2 steps at a time. 
Given N, write a function that returns the number of unique ways you can climb the staircase. 
The order of the steps matters.

For example, if N is 4, then there are 5 unique ways:

1, 1, 1, 1
2, 1, 1
1, 2, 1
1, 1, 2
2, 2
What if, instead of being able to climb 1 or 2 steps at a time, you could climb any number from a set of positive integers X? 
For example, if X = {1, 3, 5}, you could climb 1, 3, or 5 steps at a time
*/
void Main()
{
	new Solution(new int[] { 1, 2 }).Count(4).Dump("Expect 5");
	new Solution(new int[] { 1, 3, 5 }).Count(4).Dump("Expect 3");
	new Solution(new int[] { 1, 3, 5 }).Count(5).Dump("Expect 5");
}


class Solution
{
	//note: stepsize values must be unique. otherwise it will consider them unique paths
	public Solution(int[] stepsize){
		this.stepsize = stepsize;
	}
	int[] stepsize;
	Dictionary<int, int> memo = new Dictionary<int, int>();
	
	public int Count(int n)
	{
		if (n < 0) return 0;
		if (n == 0) return 1;
		int count;
		if (memo.TryGetValue(n, out count)){
			return count;
		}
		foreach(var steps in stepsize){
			count += Count(n-steps);
		}
		memo[n] = count;
		//count.Dump(""+n);
		return count;
	}

}