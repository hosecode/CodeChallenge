<Query Kind="Program" />

/*
This problem was asked by Google.

Given two singly linked lists that intersect at some point, find the intersecting node. The lists are non-cyclical.

For example, given A = 3 -> 7 -> 8 -> 10 and B = 99 -> 1 -> 8 -> 10, return the node with value 8.

In this example, assume nodes with the same value are the exact same node objects.

Do this in O(M + N) time (where M and N are the lengths of the lists) and constant space.
*/

/*
Constant space means we can't use a hashtable to store values
O(M+N) time means we can't iterate though the list(s) multiple times
BUT if the nodes are the same... then it means they would have the same .next() value. So the ends are both lists must be the same.
ie it is a Y shape where the lists converge. so if we walk to the ends of the lists ( O(N+M) ), and then walk back while 
HOWEVER this is a singly linked lists, so we cant go backwards.
So, what if we act like the 2nd list is cyclical, and return to the start. then by moving pointers at different speeds
eventully they overlap. Then calculate backwards the lengths of the lists for the point of divergance.
off the top of my head i forget what the exact algo is for this, but this situation is also slightly different
in that there are two "heads" (of possible differing length). whereas the textbook would have one "head" into a cycle.
Now, we can potentially ignore that, and just treat the B graph as the cycle which feeds off the A graph. (since we know
that they interesect).

Seperate thought, what if we reverse the lists as we go. then were at the end and walk back. this is destructive unless we re-reverse
but simplier to understand. Lets use the knowledge that we have the ends of the list!
*/
void Main()
{
	Node a = new Node(3, new Node(7, new Node(8, new Node(10, null))));
	Node b = new Node(99, new Node(1, new Node(8, new Node(10, null))));

	FindIntersect(a, b).Dump("Expect val=8"); 
}

Node FindIntersect(Node a, Node b){
	Node aEnd = Reverse(a); 
	Node bEnd = Reverse(b);
	

	while(aEnd.next.Equals(bEnd.next)){	 //shouldnt be null with proper inputs
		aEnd = aEnd.next;
		bEnd = bEnd.next;
	}
	return aEnd;
}
Node Reverse(Node a){
	Node prev = null;
	while (a!=null){
		Node t = a.next;
		a.next = prev;
		prev=a;
		a = t;
	}
	return prev;
	
}
Node Next(Node a, Node defaultNode){
	if (a.next!=null) return a.next;
	return defaultNode;
}
class Node
{
	public Node() {}
	public Node(int val, Node next){
		this.val=val;
		this.next = next;
	}
	public Node next;
	public int val;
	public override bool Equals(object obj)
	{ 	//In this example, assume nodes with the same value are the exact same node objects.
		return (obj as Node)?.val == val;
	}
}

// Define other methods and classes here
