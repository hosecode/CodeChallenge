<Query Kind="Program" />

/*
Write a function that will return the minimum node in a Binary Search Tree (BST).   Node Min(root)
Write a function that will return the next greater node of a given node. Node Greater(
*/
void Main()
{
	Node tree = Build(4);
	
	Node min = Min(tree);
	min.val.Dump("Min");
	
	Node node;
	node = min;
	while (node != null)
	{
		int nodeVal = node.val;
		(GreaterParent(node)?.val??null).Dump($"Parent Greater than {nodeVal}");
		((node = Greater(node))?.val??null).Dump($"Greater than {nodeVal}");
	}
	
	"Inject nulls".Dump("TESTING-----");
	node = Find(tree, 6);
	node.right = null;	//del node 7
	Greater(node).val.Dump("Expect 8");

	node = Find(tree, 2);
	node.left = null;  //del node 1
	Greater(node).val.Dump("Expect 3");
	Min(tree).val.Dump("Min expect 2");

}

// Define other methods and classes here

Node Min(Node root){
	if (root==null) return null;
	while(root.left!=null) root = root.left;
	return root;
}
private Node GreaterParent(Node n){
	//while not root of tree
	while (n.parent != null){
		//if n is left child of parent, then parent is greater
		if (n.parent.left==n) return n.parent;
		//otherwise parent is less
		n = n.parent;
	}
	//all parents were less, no more parents to check
	return null;
}
Node Greater(Node n){
	if (n==null) throw new ArgumentNullException("n");
	//min node of sub tree to right (if exists), else GreaterParent
	return Min(n.right)??GreaterParent(n);
}
class Node
{
	public Node() {}
	public Node(int val, Node left=null, Node right=null, Node parent=null){
		this.val=val;
		this.left=left;
		this.right=right;
		this.parent=parent;
	}
	public Node parent;
	public Node left;
	public Node right;
	public int val;
}

Node Build(int depth){
	return Build(depth, Counter().GetEnumerator());
}
Node Build(int depth, IEnumerator<int> nextVal, Node parent=null)
{
	if (depth == 0) return null;
	Node result = new Node();
	result.parent = parent;
	result.left = Build(depth-1, nextVal, result);
	nextVal.MoveNext();
	result.val = nextVal.Current;
	result.right = Build(depth-1, nextVal, result);
	return result;
}

IEnumerable<int> Counter(){
	int i=1;
	while (true)
	{
		yield return i++;
	}
}
Node Find(Node root, int val){
	if (root == null) return null;
	if (root.val == val) return root;
	if (root.val > val) return Find(root.left, val);
	//if (root.val < val) 
	return Find(root.right, val);
}