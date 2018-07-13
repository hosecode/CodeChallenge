<Query Kind="Program" />


void Main()
{
	int[] testcase = new int[]{2,4,-1,1,2,6,1,1};
	IsCompleteCycle(testcase).Dump();
}

//Each array value is an offset to the next node.
//Does each node get visited once and only once, and return to the start.
bool IsCompleteCycle(int[] cycle){
	int remaining = cycle.Length;
	bool[] visted = new bool[cycle.Length];
	int index = (cycle[0] + cycle.Length) % cycle.Length;
	while( index != 0 )
	{
		if (visted[index]) return false;
		visted[index] = true;
		remaining--;
		index = (index+cycle[index] + cycle.Length) % cycle.Length;
	}
	
	return remaining==1;
}

// Define other methods and classes here