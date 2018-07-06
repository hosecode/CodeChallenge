<Query Kind="Program" />

/*
Return the length of the shortest, non-empty, contiguous subarray of A with sum at least K.

If there is no non-empty subarray with sum at least K, return -1.

Example 1:
Input: A = [1], K = 1
Output: 1

Example 2:
Input: A = [1,2], K = 4
Output: -1

Example 3:
Input: A = [2,-1,2], K = 3
Output: 3
 
Note:
1 <= A.length <= 50000
-10 ^ 5 <= A[i] <= 10 ^ 5
1 <= K <= 10 ^ 9
*/
void Main()
{
	new Solution().ShortestSubarray(new int[] { 1 }, 1).Dump("Expect: 1");
	new Solution().ShortestSubarray(new int[] { 1, 2}, 4).Dump("Expect: -1");
	new Solution().ShortestSubarray(new int[] { 2, -1, 2 }, 3).Dump("Expect: 3");
	new Solution().ShortestSubarray(new int[] { -1, 2, 1, 1 }, 3).Dump("Expect: 2");
	new Solution().ShortestSubarray(new int[] { -28, 81, -20, 28, -29 }, 89).Dump("Expect: 3");
	new Solution().ShortestSubarray(new int[] { 2, -1, 2 }, 3).Dump("Expect: 3");
}

public class Solution
{
	int[] sums;
	//O(N^2) worst case, but tries to find short solutions first (at the cost of N memory).
	//Still thinking of faster solutions, but because elements can be negative it rules out some options. 
	public int ShortestSubarray(int[] A, int K)
	{
		sums = new int[A.Length];

		for (int l = 1; l <= A.Length; l++)
		{
			for (int i = 0; i + l <= A.Length; i++)
			{
				sums[i] += A[i + l - 1];
				if (sums[i] >= K) return l;
			}
		}
		return -1;
	}

