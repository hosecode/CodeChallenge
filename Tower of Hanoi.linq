<Query Kind="Program" />

/*
Classic tower of Hanoi 
-There are three towers, different sized disks 1...n
-They start on tower with the smallest disk resting on the next larger disk, with the Nth disk on bottom.
-Task is to move all disks to the 3rd tower with the following rules
 - You can only move one disk at a time
 - You can never place a larger disk on top of a smaller disk.
*/

/*
So there is a source tower, a destination, and a buffer. If we wanted to move the largest disk to the des,
we'd have to move the other disks to the buffer first. then move those disks to the dest using the 1st tower as the buffer.
Follow that same pattern recursively for N-1 disks.
*/
void Main()
{
	Hanoi h = new Hanoi(9);
	h.Print();
	h.Move();
	h.Print();
}

class Tower{
	Stack<int> stack ;
	public Tower(){
		stack=new Stack<int>();
	}
	public Tower(int capacity){
		stack = new Stack<int>(capacity);
	}
	//Builder method to ensure a proper tower is created
	public static Tower Build(int disks)
	{
		Tower t = new Tower(disks);
		while (disks > 0)
		{
			t.stack.Push(disks--);
		}
		return t;
	}
	//encapsulate to ensure no violations of rules
	public void MoveDisk(Tower des)
	{
		if (des.stack.Count > 0 && des.stack.Peek() < stack.Peek()) throw new InvalidOperationException("Can not push larger items onto stack");
		des.stack.Push(stack.Pop());
	}
	public int Count
	{
		get { return stack.Count;}
	}
	public void Print()
	{
		Stack<int> temp = new Stack<int>(stack.Count);
		while (stack.Count > 0)
		{
			temp.Push(stack.Pop());
		}
		while (temp.Count > 0)
		{
			Console.Write($"[{temp.Peek()}]");
			stack.Push(temp.Pop());
		}
		Console.WriteLine();
	}
}

class Hanoi
{
	Tower[] towers = new Tower[3];
	public Hanoi(int disks)
	{
		towers[0] = Tower.Build(disks);
		towers[1] = new Tower(disks);
		towers[2] = new Tower(disks);
	}
	public void Print(){
		for (int i = 0; i < 3; i++)
		{
			Console.Write($"Tower {i}: ");
			towers[i].Print();
		}
		Console.WriteLine();
	}
	
	public void Move(){
		Move(towers[0], towers[2], towers[1], towers[0].Count);	
	}
	void Move(Tower src, Tower des, Tower buf, int count){
		//no-op
		if (count == 0)
		{
			Print();	//Take a peek at the progress
			return;
		}
		//Move all top disk to buffer
		Move(src, buf, des, count-1);
		//Move final disk to des
		src.MoveDisk(des);
		//Move top disk to final
		Move(buf, des, src, count - 1);
	}


}

