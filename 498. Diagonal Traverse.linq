<Query Kind="Program" />

/*498. Diagonal Traverse

Given a matrix of M x N elements (M rows, N columns), return all elements of the matrix in diagonal order as shown in the below image.

Example:
Input:
[
 [ 1, 2, 3 ],
 [ 4, 5, 6 ],
 [ 7, 8, 9 ]
]

Output:  [1,2,4,7,5,3,6,8,9]
*/
void Main()
{
	int[,] input = null;
	int[] expect = null;
	
	input = new int[,] { { 1, } };
	expect = new int[] { 1 };
	new Solution().FindDiagonalOrder(input).Dump("Expect: " + string.Join(",", expect));

	input = new int[,] { { 1, 2, 3}};
	expect = new int[] { 1, 2, 3 };
	new Solution().FindDiagonalOrder(input).Dump("Expect: " + string.Join(",", expect));

	input = new int[,] { {1}, {2}, {3} };
	expect = new int[] { 1, 2, 3 };
	new Solution().FindDiagonalOrder(input).Dump("Expect: " + string.Join(",", expect));

	input = new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
	expect = new int[] {1,2,4,7,5,3,6,8,9};
	new Solution().FindDiagonalOrder(input).Dump("Expect: " + string.Join(",", expect));

}

// Define other methods and classes here
public class Solution
{
	public int[] FindDiagonalOrder(int[,] matrix)
	{
		//todo: throw if not correct shape
		int width = matrix.GetLength(1);
		int height = matrix.GetLength(0);
		int[] result = new int[width * height];

		if (height == 1)
		{
			//Buffer.BlockCopy(matrix, 0, result, 0, matrix.Length);
			return matrix.Cast<int>().ToArray();
		}
		
		int x = 0;
		int y = 0;
		int index = 0;
		while (index < result.Length)
		{
			//move up
			while (x < width && y >= 0)
			{
				result[index++] = (matrix[y, x]);
				x++;
				y--;
			}


			//change dir
			y++;
			if (x == width)
			{
				x--; y++;
			}

			//move down
			while (x >= 0 && y < height)
			{
				result[index++] = (matrix[y, x]);
				x--;
				y++;
			}

			//change dir
			x++;
			if (y == height)
			{
				x++; y--;
			}

			//set done?
		}
		return result;
	}
}