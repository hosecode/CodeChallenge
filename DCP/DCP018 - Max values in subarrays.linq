<Query Kind="Program" />

/*
This problem was asked by Google.

Given an array of integers and a number k, where 1 <= k <= length of the array, compute the maximum values of each subarray of length k.

For example, given array = [10, 5, 2, 7, 8, 7] and k = 3, we should get: [10, 7, 8, 8], since:
10 = max(10, 5, 2)
7 = max(5, 2, 7)
8 = max(2, 7, 8)
8 = max(7, 8, 7)

Do this in O(n) time and O(k) space. You can modify the input array in-place and you do not need to store the results. You can simply print them out as you compute them.
*/
/*
The idea is to track he k biggest elements, but we also need to track their index so we know if they are outside the sliding window.
we also know that if a bigger number comes (after) then we dont care about the second biggest number, so we can avoid keeping a sorted list of max
so we'll keep a doubly linked list as a max queue, that we can performantly read/edit both the head and tail.
first we populate it to add the first K elements to get the first result. BUT we can toss out any smaller number in our list, because it will never be a result.
(That will keep our list smaller and ordered.)
we do that by removing anything from the tail that is smaller then the current element
once we processed k elements, our list should have the largest element at the head.
We print that out.
then we check to remove any elements in our list that our outside the K sliding window.
then we remove anything from the tail that is smaller then our current element
then we add the current element and repeat
when the loop ends we need to print out our last result
*/
void Main()
{
	solve(new int[] {10, 5, 2, 7, 8, 7}, 3).Dump("Expect: [10, 7, 8, 8]");
}

ICollection solve(int[] arr, int k){
	//Problem stated could print on fly but I want to test results I'll push to a queue
	Queue<int> result = new Queue<int>();
	LinkedList<int> list = new LinkedList<int>();
	int i=0;
	for (i=0; i<k;i++){
		//if this element is bigger, then you don't need the smaller ones
		while (list.Count>0 && arr[list.Last.Value]<arr[i]){
			list.RemoveLast();
		}
		//try to fill up the list with the k biggest elements index
		list.AddLast(i);	
	}
	
	for (; i < arr.Length; i++){
		//the first item in the list is the biggest item 
		result.Enqueue(arr[list.First()]);

		//remove anything outside of the sliding scale
		while (list.Count > 0 && list.First.Value <= i - k)
		{
			list.RemoveFirst();
		}

		//TODO: dupe code here
		//if this element is bigger, then you don't need the smaller ones
		while (list.Count > 0 && arr[list.Last.Value] < arr[i])
		{
			list.RemoveLast();
		}
		//try to fill up the list with the k biggest elements index
		list.AddLast(i);
	}
	
	//print the last element
	result.Enqueue(arr[list.First()]);

	return result;
}


