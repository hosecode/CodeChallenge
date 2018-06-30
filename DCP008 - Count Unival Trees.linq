<Query Kind="Program" />


/*This problem was asked by Google.

A unival tree (which stands for "universal value") is a tree where all nodes under it have the same value.

Given the root to a binary tree, count the number of unival subtrees.

For example, the following tree has 5 unival subtrees:

   0
  / \
 1   0
	/ \
   1   0
  / \
 1   1
 */

void Main()
{
	Node ex = new Node(0, new Node(1), new Node(0, new Node(1, new Node(1), new Node(1)), new Node(0)));
	Count(ex).Dump("Expect: 5");
}

class Node{
	public Node(int val, Node l = null, Node r=null){
		this.val=val;
		this.left=l;
		this.right=r;
	}
	public int val;
	public Node left;
	public Node right;
}


int Count(Node root)
{
	int? val;
	return Count(root, out val);
}

int Count(Node root, out int? val){
	if (root == null)
	{
		val = null;
		return 0;
	}

	val = root.val;
	if (root.left == null && root.right == null)
	{
		return 1;
	}

	int? leftVal;
	int? rightVal;
	int count = 0;
	count = Count(root.left, out leftVal) + Count(root.right, out rightVal);
	if ((leftVal ?? root.val) == root.val && (rightVal ?? root.val) == root.val)
	{
		count++;
	}
	else{
		val = -1; //assuming positive ints are valid
	}
	
	return count;

}