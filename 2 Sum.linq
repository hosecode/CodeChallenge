<Query Kind="Program" />

/*
Given an array of integers, find two numbers such that they add up to a specific target number.

The function twoSum should return indices of the two numbers such that they add up to the target, where index1 < index2. Please note that your returned answers (both index1 and index2 ) are not zero-based. 
Put both these numbers in order in an array and return the array from your function ( Looking at the function signature will make things clearer ). Note that, if no pair exists, return empty list.

If multiple solutions exist, output the one where index2 is minimum. If there are multiple solutions with the minimum index2, choose the one with minimum index1 out of them.

Input: [2, 7, 11, 15], target=9
Output: index1 = 1, index2 = 2
*/
void Main()
{
	Solve(new int[]{2, 7, 11, 15}, 9).Dump("Expect: index1 = 1, index2 = 2");
	
}
int[] Solve(int[] arr, int target){
	Dictionary<int, int> lookup = new Dictionary<int, int>();
	for(int i=0; i<arr.Length; i++){
		int val = arr[i];
		int p1;
		if (lookup.TryGetValue(target - val, out p1))
		{
			return new int[] {p1, i+1};
		}
		
		if (!lookup.ContainsKey(val)) lookup[val]= i+1;
	}
	
	//not found
	return new int[0];
}
// Define other methods and classes here
