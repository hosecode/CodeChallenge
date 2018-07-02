<Query Kind="Program" />

/*
We have a two dimensional matrix A where each value is 0 or 1.

A move consists of choosing any row or column, and toggling each value in that row or column: changing all 0s to 1s, and all 1s to 0s.

After making any number of moves, every row of this matrix is interpreted as a binary number, and the score of the matrix is the sum of these numbers.

Return the highest possible score.

Example 1:

Input: [[0,0,1,1],[1,0,1,0],[1,1,0,0]]
Output: 39
Explanation:
Toggled to [[1,1,1,1],[1,0,0,1],[1,1,1,1]].
0b1111 + 0b1001 + 0b1111 = 15 + 9 + 15 = 39
 
Note:
1 <= A.length <= 20
1 <= A[0].length <= 20
A[i][j] is 0 or 1.
*/
void Main()
{
	//int[][] test = new int[][] { new int[] { 0, 0, 1, 1 }, new int[] { 1, 0, 1, 0 }, new int[] { 1, 1, 0, 0 } }; //39
	//int[][] test = new int[][] { new int[] { 0, 1 }, new int[] { 0, 1 }, new int[] { 0, 1 }, new int[] { 0, 0 } }; //11
	int[][] test = new int[][] { new int[] { 0, 0, 1, 1 }, new int[] { 1, 0, 1, 0 }, new int[] { 1, 1, 0, 0 } }; //39
	new Solution().MatrixScore(test).Dump();
}

// Define other methods and classes here

public class Solution
{
	public int MatrixScore(int[][] A)
	{
		FlippedRows(A);
		A.Dump();
		FlipColumns(A);
		A.Dump();
		return sum(A);
	}
	void FlippedRows(int[][] A)
	{
		int r = A.Length;
		int c = A[0].Length;
		for (int i = 0; i < r; i++)
		{
			if (A[i][0] == 0)
			{
				("" + i).Dump("Flipping Row");
				for (int j = 0; j < c; j++)
				{
					A[i][j] = A[i][j] == 0 ? 1 : 0;
				}
			}
		}

	}
	void FlipColumns(int[][] A)
	{
		int r = A.Length;
		int c = A[0].Length;
		for (int i = 1; i < c; i++)
		{
			int count = 0;
			for (int j = 0; j < r; j++)
			{
				if (A[j][i] == 1) count++;
			}
			if (count <= r / 2)
			{
				("" + i).Dump("Flipping Col");
				for (int j = 0; j < r; j++)
				{
					A[j][i] = A[j][i] == 0 ? 1 : 0;
				}
			}
		}
	}
	int sum(int[][] A)
	{
		int r = A.Length;
		int c = A[0].Length;
		int result = 0;
		for (int i = 0; i < r; i++)
		{
			result += binary(A[i]);
		}
		return result;
	}
	int binary(int[] a)
	{
		int result = 0;
		foreach (int d in a)
		{
			result = result << 1;
			result += d;
		}
		return result;
	}
}
