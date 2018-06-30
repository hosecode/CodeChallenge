<Query Kind="Program" />

void Main()
{
	
}

// Define other methods and classes here

Node Min(Node root){
	if (root==null) return null;
	while(root.left!=null) root = root.left;
	return root;
}
private Node ParentGreater(Node n){
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
Node Greator(Node n){
	if (n==null) throw new ArgumentNullException("n");
	//min node of sub tree to right (if exists), else GreaterParent
	return Min(n.right)??ParentGreater(n);
}
class Node
{
	public Node parent;
	public Node left;
	public Node right;
	public object val;
}
