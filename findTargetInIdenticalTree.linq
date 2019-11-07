//find same node in two identical binary tree
//Note: i am assuming that trees have identical values and structure, but different references. 
//therefore check for reference on tree 1 and return reference on tree 2
//consider what if tree had mutliple node values
void Main()
{
	Test1();
	Test2();
	Console.WriteLine("Success");
}

Node findNodeInSecondTree(Node target, Node root1, Node root2)
{
	if (root1 == null) return null;

	if (root1 == target)
	{
		return root2;
	}
	
	return findNodeInSecondTree(target, root1.left, root2.left) ?? findNodeInSecondTree(target, root1.right, root2.right);
}

public class Node
{
	public int value;
	public Node left;
	public Node right;

	public override string ToString()
	{
		return base.ToString() + $"{left}>{value}<{right}";
	}
}


void Test1()
{
	Node target1 = new Node() { value = 3};
	Node l1 = new Node() { value = 2, left = new Node() { value = 1 }, right = target1 };
	Node r1 = new Node() { value = 8, left = new Node() { value = 4 }, right = new Node() { value = 9 } };
	Node t1 = new Node() { value = 5, left = l1, right = r1 };

	Node target2 = new Node() { value = 3 };
	Node l2 = new Node() { value = 2, left = new Node() { value = 1 }, right = target2 };
	Node r2 = new Node() { value = 8, left = new Node() { value = 4 }, right = new Node() { value = 9 } };
	Node t2 = new Node() { value = 5, left = l1, right = r1 };

	AssertEquals(target2, findNodeInSecondTree(target1, t1, t2));
}

void Test2()
{
	Node target1 = new Node() { value = 3 };
	Node l1 = new Node() { value = 2, left = new Node() { value = 1 }, right = target1 };
	Node r1 = new Node() { value = 8, left = new Node() { value = 4 }, right = new Node() { value = 9 } };
	Node t1 = new Node() { value = 5, left = l1, right = r1 };

	Node target2 = new Node() { value = 3 };
	Node l2 = new Node() { value = 2, left = new Node() { value = 1 }, right = target2 };
	Node r2 = new Node() { value = 8, left = new Node() { value = 4 }, right = new Node() { value = 9 } };
	Node t2 = new Node() { value = 5, left = l1, right = r1 };

	AssertEquals(r2, findNodeInSecondTree(r1, t1, t2));
}

void AssertEquals(object expected, object actual)
{
	if (expected == actual) throw new AssertionFailed(expected, actual);
}
class AssertionFailed : Exception
{
	public AssertionFailed(Object expected, Object actual) : base($"Expected: {expected}, Actual: {actual}")
	{

	}
}
