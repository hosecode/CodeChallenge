<Query Kind="Program" />

/*
This problem was asked by Facebook.

Given a stream of elements too large to store in memory, pick a random element from the stream with uniform probability.
*/
void Main()
{
	//This requries some clarifying questions
	//do we know the number of elements
	//can that number fit in an int/long
	//is the stream fix, or is new data streaming in.
	//what if we just split elements in half based on rnd(), until only 1 element
	
	//upon further reading, it seems that the quetion implies that the size of the stream is not known initially, and only one element will be returned from the entire stream.
	//so for this you start by picking the first element, and as you enumerate the entire stream, you attempt to swap the chosen element if probablitly dictates.
	//that probablity depends on 1/1+i 
	//#reservoir sampling
	int[] count = new int[100];

	foreach (var element in Enumerable.Range(1,100000))
	{
		count[Solve(Enumerable.Range(1, 100).GetEnumerator())-1]++;
	}
	count.Dump();
}

Random rnd = new Random();

int Solve(IEnumerator<int> e){
	
	e.MoveNext();
	int pick = e.Current;
	
	for(int i=1; e.MoveNext(); i++){
		if (rnd.Next(1,1+i+1)==1) pick=e.Current;	// 1/1+i but maxvalue requires additional +1 
	} 
	
	return pick;
}