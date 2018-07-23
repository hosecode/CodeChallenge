<Query Kind="Program" />

/*
Asked by Niantic
     10
 -2      -3
        16   17
		
Q1. max sum of a path from root
10->-2 =8
10->-3->16=23
10->-3->17=24
*Doesnt have to end at leaf

Q2. max sum of a path form any node to other node
-2->10->-3->-17=22
16-->-3->17=30
10->-3->16->-3->17 NO! (-3 node is duplicated)
*dont alter node data structure
*/

void Main()
{
	Node root = new Node(10);
	root.left = new Node(-2);
	root.right=new Node(-3);
	root.right.left = new Node(16);
	root.right.right = new Node(17);
	System.Console.WriteLine(MaxSumQ1(root).ToString());
	System.Console.WriteLine(MaxSumQ2(root).ToString());
}

class Node{
	public int val;
	public Node left;
	public Node right;
	public Node(int val){this.val=val;}
}

int MaxSumQ1(Node root){
	return MaxSumQ1(root, 0,0);
}
 int MaxSumQ1(Node root, int max, int sum)
{
	if (root==null) return max;
	
	sum+=root.val;
	max = Math.Max(max, sum);
	
	return Math.Max(MaxSumQ1(root.left, max, sum),MaxSumQ1(root.right, max, sum));
}
int MaxSumQ2(Node root){
	int maxst;
	int maxpath;
	MaxSumQ2(root, out maxst, out maxpath);
	return Math.Max(maxst, maxpath);
}
void MaxSumQ2(Node root, out int maxst, out int maxpath)
{
	maxst=0;
	maxpath=0;
	if (root==null) return;

	int lmaxst;
	int lmaxpath;
	MaxSumQ2(root.left, out lmaxst, out lmaxpath);

	int rmaxst;
	int rmaxpath;
	MaxSumQ2(root.right, out rmaxst, out rmaxpath);
	
	maxpath = Math.Max(lmaxpath, rmaxpath) + root.val;
	
	maxst = Math.Max(lmaxst, rmaxst);
	maxst = Math.Max(maxst, lmaxpath + root.val + rmaxpath);
}