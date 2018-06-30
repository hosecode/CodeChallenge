<Query Kind="Program">
  <Namespace>LINQPad</Namespace>
</Query>

//#define DEBUG
#undef DEBUG

/*
Given the root to a binary tree, implement serialize(root), which serializes the tree into a string, and deserialize(s), which deserializes the string back into the tree.

For example, given the following Node class
class Node:
    def __init__(self, val, left=None, right=None)
	
        self.val = val
        self.left = left
        self.right = right
The following test should pass:

node = Node('root', Node('left', Node('left.left')), Node('right'))
assert deserialize(serialize(node)).left.left.val == 'left.left'

*/

void Main()
{

	Node node = new Node("root", new Node("left", new Node("left.left")), new Node("right"));
serialize(node).Dump("serialized");
serialize(deserialize(serialize(node))).Dump("roundtrip");
	(deserialize(serialize(node)).Left.Left.Val == "left.left").Dump("Assert");

}

// Nodes Val can not be empty, null, or contain commas 
public class Node{
	public Node Left;
	public Node Right;
	public string Val;
	public Node(string v, Node l=null, Node r=null){
		this.Left=l;
		this.Right=r;
		this.Val=v;
	}
}
string serialize(Node n)
{
	if (n==null) return "";
	return $"{n.Val},{serialize(n.Left)},{serialize(n.Right)}";
}

Node deserialize(string s){
	int start = 0;
	return deserialize(s, ref start);
}

Node deserialize(string s, ref int start)
{
	string val=ParseText(s, ref start);
	if (val==null) return null;

	Node left = deserialize(s, ref start);
	Node right = deserialize(s, ref start);
	return new Node(val, left, right);
}

string ParseText(string s, ref int position)
{
#if (DEBUG)
$"{s.Substring(position)}: {position}".Dump();
#endif

	if (position >= s.Length) return null;
	
	int tend = s.IndexOf(',', position);
	if (tend == -1) tend = s.Length - 1;
	if (position == tend)
	{
		tend++;
		position = tend;
		return null;
	}
	string val= s.Substring(position, tend - position);
	position = tend + 1;
return val;

}