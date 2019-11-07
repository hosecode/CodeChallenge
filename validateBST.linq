void Main()
{
	Test1();
	Test2();
	Test3();
	Test4();
	Test5();
	Console.WriteLine("Success");
}


public class Node{

	public int value;
	public Node left;
	public Node right;
	
}

public class TreeUtil
{
	public static bool isBST(Node root)
	{
		return isBST(root, int.MinValue, int.MaxValue);
	}

	private static bool isBST(Node node, int min, int max)
	{
		if (node == null) return true;

		if (node.value <= min || node.value >= max)
		{
			return false;
		}
		return isBST(node.left, min, node.value) && isBST(node.right, node.value, max);
	}
}

void Test1(){
	//simple valid tree test
	AssertTrue(TreeUtil.isBST(null));
	AssertTrue(TreeUtil.isBST(new Node() { value = 1 }));
	Node t1 = new Node() { value = 2, left = new Node() { value = 1 }, right = new Node() { value = 3 }};
	AssertTrue(TreeUtil.isBST(t1));

}

void Test2()
{
	//left is greater than parent
	Node t1 = new Node() { value = 1, left = new Node() { value = 2 }, right = new Node() { value = 3 } };
	AssertFalse(TreeUtil.isBST(t1));

	//left is greater than parent
	Node t2 = new Node() { value = 1, left = new Node() { value = 2 }};
	AssertFalse(TreeUtil.isBST(t1));

	//right is less than parent
	Node t3 = new Node() { value = 2, right = new Node() { value = 1 } };
	AssertFalse(TreeUtil.isBST(t2));
}

 
void Test3()
{
	//      5
	//   2  ^    8
	// 1   3   4   9 
	//         ^

	//right subtree min less than root
	Node l1 = new Node() { value = 2, left = new Node() { value = 1 }, right = new Node() { value = 3 } };
	Node r1 = new Node() { value = 8, left = new Node() { value = 4 }, right = new Node() { value = 9 } };
	Node t1 = new Node() { value = 5, left = l1, right = r1};
	AssertFalse(TreeUtil.isBST(t1));
}

void Test4()
{
	//      5
	//   1     6
	//       7 ^ 8 
	//       ^
	
	//invalid right subtree: left node greater than parent
	Node r1 = new Node() { value = 6, left = new Node() { value = 7 }, right = new Node() { value = 8 } };
	Node t1 = new Node() { value = 5, left = new Node() {value=1}, right = r1 };
	AssertFalse(TreeUtil.isBST(t1));
}
void Test5()
{
	//      5
	//   1  ^  6
	//       4   7 
	//       ^
	
	//right subtree min is less than parent
	Node r1 = new Node() { value = 6, left = new Node() { value = 4 }, right = new Node() { value = 7 } };
	Node t1 = new Node() { value = 5, left = new Node() { value = 1 }, right = r1 };
	AssertFalse(TreeUtil.isBST(t1));
}
void AssertTrue(bool actual){
	if (!actual) throw new AssertionFailed("true", "false");
}
void AssertFalse(bool actual)
{
	if (actual) throw new AssertionFailed("false", "true");
}
class AssertionFailed : Exception
{
	public AssertionFailed(String expected, String actual) : base($"Expected: {expected}, Actual: {actual}") {
		
	}
}
