<Query Kind="Program" />

/*
This problem was asked by Google.

An XOR linked list is a more memory efficient doubly linked list. 
Instead of each node holding next and prev fields, it holds a field named both, which is an XOR of the next node and the previous node. 
Implement an XOR linked list; it has an add(element) which adds the element to the end, and a get(index) which returns the node at index.

If using a language that has no pointers (such as Python), you can assume you have access to get_pointer and dereference_pointer functions 
that converts between nodes and memory addresses.
*/

void Main()
{
	//Quick test of the node and memory manager
	Node n = null;
	FakeMemory.address(n).Dump("null");
	Node[] nodes = new UserQuery.Node[10];
	foreach (var element in Enumerable.Range(0,10))
	{
		nodes[element]= new Node(element);
	}
	Node n1 = nodes[1];
	FakeMemory.address(n1).Dump("n1");
	//test of node left/right
	n1.Dump();
	n1.Left(null).Dump("Left");
	n1.Right(null).Dump("Right");

	n1.Set(null, nodes[2]);
	n1.Left(nodes[2]).Dump("Left");
	n1.Right(null).Dump("Right");

	//quick test of XOR list add
	XORList list = new XORList();
	Node listStart = new Node(0);
	list.add(listStart);
	for (int i = 1; i < 11; i++)
	{
		list.add(new Node(i));
	}
	list.Dump();
	
	Node index = listStart;
	Node prev = null;
	while(index!=null){
		index.Dump("ListWalk");
		Node next = index.Right(prev);
		prev = index;
		index=next;
	}
	
	Node end = null;
	for (int i = 0; i < 11; i++)
	{
		end = list.get(i).Dump($"list.get({i})");
	}
	end.Right(end.Left(null)).Dump("Ends right: ");
	FakeMemory.Dump();
}

class XORList{
	Node start;
	Node end;
	int count=0;
	public void add(Node n)
	{
		if (start == null)
		{
			start = n;
			end = n;
			return;
		}
		
		n.Set(end, null);
		end.Set(end.Left(null), n);
		end = n;
		count++;
	}
	public Node get(int index) {
		if (index<0 || index>count) throw new ArgumentOutOfRangeException(nameof(index));
		Node cNode = start;
		Node prev = null;
		while(index-->0){
			Node next = cNode.Right(prev);
			prev = cNode;
			cNode = next;
		}
		return cNode;
	}
}

class Node
{
	public Node() {}
	public Node(int val){this.value=val;}
	public int value;
	int both;
	public void Set(Node left, Node right)
	{
		both = xor(FakeMemory.address(left),FakeMemory.address(right));
	}
	Node Neighbour(Node other)
	{
		return FakeMemory.deref(xor(FakeMemory.address(other), both));
	}
	int xor(int a, int b)
	{
		return a^b;
	}
	public Node Right(Node left)
	{
		return Neighbour(left);
	}
	public Node Left(Node right)
	{
		return Neighbour(right);
	}
}


//avoiding .net pointers unsafe etc..
static class FakeMemory{
	static Random rnd = new Random(0);
	//chose to use dictionary instead of array
	static Dictionary<int, Node> memory = new Dictionary<int, UserQuery.Node>();
	static Dictionary<Node, int> lookup = new Dictionary<UserQuery.Node, int>();
	public static Node deref(int ptr){
		Node result;
		return memory.TryGetValue(ptr, out result)?result:null;
	}
	public static int address(Node node){
		if (node==null) return 0;
		int ptr;
		if (lookup.TryGetValue(node, out ptr)) return ptr;	
		return AddToMemory(node);
	}
	public static int AddToMemory(Node n){
		int ptr = GetNextAddress();
		while (memory.ContainsKey(ptr = GetNextAddress())) { }; //so bad :p
		memory.Add(ptr, n);
		lookup.Add(n, ptr);	//this means that each node can only be in memory once (also Hash dependent)
		return ptr;
	}
	static int GetNextAddress(){
		return rnd.Next(); 	//this is obviously bad
	}
	internal static void Dump(){
		memory.Dump("Memory");
	}
	
}

// Define other methods and classes here
