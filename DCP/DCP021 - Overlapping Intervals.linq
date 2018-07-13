<Query Kind="Program" />

/*
This problem was asked by Snapchat.

Given an array of time intervals (start, end) for classroom lectures (possibly overlapping), find the minimum number of rooms required.

For example, given [(30, 75), (0, 50), (60, 150)], you should return 2.
*/

/*
A.naive solution is to look at the intervals and find ones that overlap (and check if others overlap that overlap)
B.could just make an array of the discrete slots and increment counts. eg. use array for each minute stating with values of 0
	(this breaks down if its not discrete intervals, uses lots of memory , and depends on length of start-end constraint.) 
	using a sparce data structure could reduce required memory, but then you need to search for that position 
	which leads to thoughs of some type of priority structure.
C. divide and conconquer, start with one then merge with next interval.
d. some sort of sorting to examine the liklest overlaps 
e. using c. we could start to break intervals apart. so there is only clear overlaps and not. 
f. what if we just sort all of the numbers, such that starts are ++ and ends are --, then we just iterate throught the sorted times and record rooms in use and return max. (nlogn) due to sort
clarify, that start end are inclusive?. so that meeting ends at 59, next starts at 60. this is no overlap, but ending at 60 starting at 60 is overlap
*/
void Main()
{
	int[] input = new int[]{ 30, 75, 0, 50, 60, 150};
	slot[] slots = new slot[input.Length/2];
	for(int i=0; i< input.Length-1; i+=2){
		slots[i/2] = new slot(input[i], input[i+1]);
	}
	Solve(slots).Dump("Expect: 2");
}

int Solve(slot[] slots){
	
	int[] starts = slots.Select(s => s.start).ToArray();
	Array.Sort(starts);
	int[] ends = slots.Select(s => s.end).ToArray();
	Array.Sort(ends);
	int si=0;
	int ei=0;
	int max=0;
	int current=0;
	
	while(si<starts.Length && ei<ends.Length){
		if (starts[si]<ends[ei]) {
			current++;
			si++;
		}
		else{
			if (current>max) max = current;
			current--;
			ei++;
		}
	}
	

	return max;
}

//List<slot> Merge(slot a, slot b){
//	slot first = a;
//	slot second = b;
//	slot overlap;
//
//	first.start = Math.Min(a.start, b.start);
//	first.end = Math.Min(a.start, b.start);
//
//	if (a.start > b.start)
//	{
//		first = b; second = a
//	}
//	if (second.start < first.end)
//	{
//		overlap = new slot(second.start, first.end, 2);
//		first.end = overlap.start;
//		second.start = overlap.end;
//	}
//	
//	return
//}

class slot
{
	public slot() {}
	public slot(int start, int end, int count=1){
		this.start=start;
		this.end=end;
		this.count=count;
	}
	public int start;
	public int end;
	public int count;
}
