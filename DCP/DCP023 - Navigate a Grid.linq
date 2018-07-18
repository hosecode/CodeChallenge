<Query Kind="Program" />

/*
This problem was asked by Google.

You are given an M by N matrix consisting of booleans that represents a board. Each True boolean represents a wall. Each False boolean represents a tile you can walk on.

Given this matrix, a start coordinate, and an end coordinate, return the minimum number of steps required to reach the end coordinate from the start. 
If there is no possible path, then return null. You can move up, left, down, and right. You cannot move through walls. 
You cannot wrap around the edges of the board.

For example, given the following board:

[[f, f, f, f],
[t, t, f, t],
[f, f, f, f],
[f, f, f, f]]
and start = (3, 0) (bottom left) and end = (0, 0) (top left), the minimum number of steps required to reach the end is 7, 
since we would need to go through (1, 2) because there is a wall everywhere else on the second row.
*/
void Main()
{
	bool[][] map = new bool[][] { 
						new bool[] {false, false, false, false}, 
						new bool[] { true,  true, false,  true}, 
						new bool[] {false, false, false, false}, 
						new bool[] {false, false, false, false} } ;
						
	Solve(map, 3, 0, 0, 0).Dump();				

}

struct Path
{
	public int row;
	public int col;
	public int count;
	public Path(int row, int col, int count) {this.row=row;this.col=col;this.count=count;}
}


//Uses a breadth first search to flood outwards and prevents backtracking. When end is found, it will be the shortest path.
int Solve(bool[][] map, int srow, int scol, int erow, int ecol)
{
	//bounds check erow < 0 || ecol < 0 || erow > map.Length || erow > map[0].Length
	Queue<Path> next = new Queue<Path>();
	next.Enqueue(new Path(srow, scol, 0));

	while (next.Count > 0)
	{
		Path p = next.Dequeue();
		
		//check if walkable
		if (p.row < 0 || p.col < 0 || p.row > map.Length-1 || p.col > map[0].Length-1 || map[p.row][p.col] == true) continue;
		if (p.row == erow && p.col == ecol) return p.count;
		//block returning to this tile
		map[p.row][p.col] = false;
		//enqueue each direction
		next.Enqueue(new Path(p.row + 1, p.col, p.count + 1));
		next.Enqueue(new Path(p.row - 1, p.col, p.count + 1));
		next.Enqueue(new Path(p.row, p.col + 1, p.count + 1));
		next.Enqueue(new Path(p.row, p.col - 1, p.count + 1));
	}

	return -1;
}