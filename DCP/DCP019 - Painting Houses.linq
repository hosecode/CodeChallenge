<Query Kind="Program" />

/*
his problem was asked by Facebook.

A builder is looking to build a row of N houses that can be of K different colors. 
He has a goal of minimizing cost while ensuring that no two neighboring houses are of the same color.

Given an N by K matrix where the nth row and kth column represents the cost to build the nth house with kth color, return the minimum cost which achieves this goal.
*/
/*
if we only had one house, we would just find the min cost.
if we had two houses, we would look at each paint color for that house + the min of any other color on the previous house. The min of all those options is the min total.
if we had three houses, its like have 2 houses just repeat on more time. 
generalized we start with the 2nd house, pick a color and add the min of any other color on the 1st house. do for each color.then repeat for the third house using the previous sums.
finally find the min total cost (sum) for the Nth house's color. thats the answer
*/

void Main()
{
	int N=10;
	int K=3;
	int[][] costs = new int[N][];
	for (int n=0; n<N; n++){
		costs[n] = new int[K];
		for (int k=0; k<K; k++){
			costs[n][k] = n+k;
		}
	}
	minCost(costs).Dump();
}


int minCost(int[][] cost){

	//for each house
	for (int n = 1; n < cost.Length; n++)
	{
		//for each possible color of this house
		for (int k = 0; k < cost[0].Length; k++)
		{
			//find min sum with any other color on previous house
			int min = int.MaxValue;
			for (int k2 = 0; k2 < cost[0].Length; k2++)
			{ 
				if (k==k2) continue; //can't use same color as prev house
				min = Math.Min(min, cost[n-1][k2]);
			}
			cost[n][k] += min;
		}
	}

	//Find the min total (summed) price for each paint color on the final house
	int minTotal = int.MaxValue;
	for (int k2 = 0; k2 < cost[0].Length; k2++)
	{
		minTotal = Math.Min(minTotal, cost[cost.Length-1][k2]);
	}
	//cost.Dump();
	return minTotal;

}