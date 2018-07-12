<Query Kind="Program" />

/*
Facebook
Langford series
Given a ntural number Z(>=1), permute [1,1,2,2,3,3,....Z,Z] such that each number n is exactly n distanct apart
For Example"
1) Z=2, permute [1,1,2,2] => no solution
2) Z=3, permute [1,1,2,2,3,3] => one possible solution: [3,1,2,1,3,2]
3) Z=4, permute [1,1,2,2,3,3,4,4] => one possible solution:  [2,3,4,2,1,3,1,4]

given N where N>=1 return true if it is possible to transform the arrary [1,1,2,2,3,3...N,N] such for each element x its pair is x places apart 
*/

void Main()
{
	for (int i = 0; i < 20; i++)
	{
		solve(i).Dump(i.ToString());
	}
}

//TODO more performant solution
bool solve(int n)
{
	return solve(new int[n*2],n);
}
bool solve(int[] arr, int n){
	if (n==0) return true;
	
	for (int i=0; i+n+1<arr.Length; i++){
		if (arr[i]==0 && arr[i+n+1]==0){
			arr[i]=n;
			arr[i+n+1]=n;
			if ( solve(arr, n-1) ) return true;
			arr[i] = 0;
			arr[i + n + 1] = 0;
		}
	}
	
	return false;
}