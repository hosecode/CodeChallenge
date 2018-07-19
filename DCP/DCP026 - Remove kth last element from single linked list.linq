<Query Kind="Program" />

/*
This problem was asked by Google.

Given a singly linked list and an integer k, remove the kth last element from the list. k is guaranteed to be smaller than the length of the list.

The list is very long, so making more than one pass is prohibitively expensive.

Do this in constant space and in one pass.
*/

/*
Solution:
Ok, for a single pass and constant space, we'll need to keep 2 pointers that are K apart and move them in sync.
When the first pointer hits the end then the second pointer will be K from the end. That is the node to remove.
Something to consider if the implied offsets. Lets assume remove(0) will remove the very last time, and k is is small than length. 
	in that case, Length-1 == the first element of the list. Then we would need to return the new head of the list. 
	That wouldn't be the case if remove(1) expected the last item removed, but then it'd be impossible to remove the head. 
*/
void Main()
{
	test(1, 0, "{null}", "Single Element");
	test(2, 0, "(1)->","Remove last of two");
	test(2, 1, "(2)->","Remove head of two");
	test(3, 0, "(1)->(2)->","Remove last of three");
	test(3, 1, "(1)->(3)->","Remove 2nd of three");
	test(3, 2, "(2)->(3)->","Remove head of three");
}

bool throwOnFailedTest = false;
void test(int n, int remove, string expect, string testname=""){
	node head = createList(n);
	string output;

	(
		$"List: " + node.printList(head) + "\n" +
		$"Remove({remove})\n" + 
		$"Expect: {expect}\n" + 
		$"Output: {output = node.printList(RemoveFromEnd(head, remove))}\n" + 
		("Test Result: " + (output == expect ? "Passed" : "FAILED"))
	).Dump($"Test Case: {testname}");

	if (throwOnFailedTest && output != expect) throw new Exception($"Test {testname} failed.");
}

node createList(int n)
{
	if (n<1) throw new ArgumentOutOfRangeException();
	node head = new node(1);
	node prev = head;
	for (int i = 2; i <= n; i++)
	{
		prev.next = new node(i);
		prev = prev.next;
	}
	return head;
}


//remove the kth last element from the list
//k is guaranteed to be smaller than the length of the list.
//Return the new head of the list
node RemoveFromEnd(node list, int k){
	if (list.next==null && k==0) return null;
	
	node first = list;
	node second = null;
	
	while(first.next!=null){
		first = first.next;
		if (k==0 || k--==0) second = second?.next??list;
	}

	//we are removing the head of the list.
	if (second==null) return list.next;
	
	//remove node
	second.next = second.next?.next;

	//return the head of the list
	return list;
}

class node
{
	public int val; //just for convention and debugging visualization
	public node next;
	public node(int val, node next=null){
		this.val=val;
		this.next = next;
	}

	public static string printList(node head)
	{
		if (head == null) return "{null}";

		StringBuilder sb = new StringBuilder();
		node prev = head;
		while (prev != null)
		{
			sb.Append($"({prev.val})->");
			prev = prev.next;
		}
		return sb.ToString();
	}
	public string printList()
	{
		return printList(this);
	}
}
// Define other methods and classes here
