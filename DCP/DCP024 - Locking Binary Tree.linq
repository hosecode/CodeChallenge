<Query Kind="Program" />

/*
This problem was asked by Google.

Implement locking in a binary tree. A binary tree node can be locked or unlocked only if all of its descendants or ancestors are not locked.

Design a binary tree node class with the following methods:

is_locked, which returns whether the node is locked
lock, which attempts to lock the node. If it cannot be locked, then it should return false. Otherwise, it should lock it and return true.
unlock, which unlocks the node. If it cannot be unlocked, then it should return false. Otherwise, it should unlock it and return true.
You may augment the node to add parent pointers or any other property you would like. 
You may assume the class is used in a single-threaded program, so there is no need for actual locks or mutexes. 
Each method should run in O(h), where h is the height of the tree.
*/
void Main()
{
	Node root = new Node(5);
	root.left = new Node(3); root.left.parent=root;
	root.left.left = new Node(2); root.left.left.parent = root.left;
	root.left.right = new Node(4); root.left.right.parent = root.left;
	root.right = new Node(6); root.right.parent = root;
	root.right.right = new Node(7); root.right.right.parent = root.right;

	root.unlock().Dump("unlock");
	root._lock().Dump("lock");
	root.is_locked().Dump("islocked");
	root.unlock().Dump("unlock");
	root._lock().Dump("lock");
	root.is_locked().Dump("islocked");
	
	root.right.right._lock().Dump();
	root.right.right.is_locked().Dump();
	root.right.right.unlock().Dump();
	
	root.unlock().Dump("unlock");

	root.right.right._lock().Dump();
	root.right.right.is_locked().Dump();
	root.right.right.unlock().Dump();
	root.right.right.is_locked().Dump();


	root.unlock().Dump("unlock");
	root._lock().Dump("lock");
	root.is_locked().Dump("islocked");
	root.unlock().Dump("unlock");
	root._lock().Dump("lock");
	root.is_locked().Dump("islocked");

}

class Node
{
	public Node parent;
	public Node left;
	public Node right;
	bool locked;
	int decendantsLockCount=0;
	int val;
	public Node(int val){
		this.val=val;
	}
	public Node(Node parent, Node left, Node right)
	{
		this.parent = parent;
		this.left = left;
		this.right = right;
	}

	public bool is_locked()
	{
		return locked;
	}
	
	public bool unlock()
	{
		if (!locked || ancestorLocked() || decendantsLocked()) return false;
		locked = false;
		Node next=parent;
		while (next != null)
		{
			next.decendantsLockCount--;
			next = next.parent;
		}
		return true;
	}
	public bool _lock()
	{
		if (locked || ancestorLocked() || decendantsLocked()) return false;
		locked = true;
		Node next = parent;
		while (next != null)
		{
			next.decendantsLockCount++;
			next = next.parent;
		}
		return true;
	}
	//track number of locked decendants to avoid O(n) search. instead track decendants lock/unlock updates O(h) with O(1) lookup
	bool decendantsLocked(){
		return decendantsLockCount>0;
	}
	bool ancestorLocked(){
		Node next = this.parent;
		while(next!=null){
			if (next.locked) return true;
			next = next.parent;
		}
		return false;
	}

	
}
// Define other methods and classes here
