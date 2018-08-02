<Query Kind="Program" />

/*
This problem was asked by Microsoft.

Compute the running median of a sequence of numbers. That is, given a stream of numbers, print out the median of the list so far on each new element.

Recall that the median of an even-numbered list is the average of the two middle numbers.

For example, given the sequence [2, 1, 5, 7, 2, 0, 5], your algorithm should print out: 2, 1.5, 2, 3.5, 2, 2, 2
*/
void Main()
{
	Median(new float[] { 2, 1, 5, 7, 2, 0, 5 }).Dump("Expect: 2, 1.5, 2, 3.5, 2, 2, 2");
}

IList<float> Median(float[] arr)
{
	List<float> result = new List<float>();

	MaxHeap<float> min = new MaxHeap<float>();
	MinHeap<float> max = new MinHeap<float>();

	foreach (int i in arr)
	{	
		if (max.Count==0 || i>max.Peek()){
			max.Add(i);
		}
		else{
			min.Add(i);
		}
		
		//rebalance
		if (min.Count>max.Count+1){
			max.Add(min.Pop());			
		}
		else if (max.Count > min.Count + 1)
		{
			min.Add(max.Pop());
		}
		
		
		//add to running result
		if (min.Count == max.Count)
		{
			//even
			float minf = min.Peek();
			float maxf = max.Peek();
			result.Add((min.Peek() + max.Peek()) / 2);
		}		
		else if (min.Count > max.Count)
		{
			result.Add(min.Peek());
		}
		else
		{
			result.Add(max.Peek());
		}
		
	}
	
	return result;
}





//copy paste minheap, added count and peek, but didn't examine performance 

class MinHeap<T> where T : IComparable
{
	List<T> elements;

	public MinHeap()
	{
		elements = new List<T>();
	}
	public int Count
	{
		get
		{
			return elements.Count;
		}
	}
	public void Add(T item)
	{
		elements.Add(item);
		Heapify();
	}

	public void Delete(T item)
	{
		int i = elements.IndexOf(item);
		int last = elements.Count - 1;

		elements[i] = elements[last];
		elements.RemoveAt(last);
		Heapify();
	}
	public T Peek(){
		return elements[0];
	}
	public T Pop()
	{
		if (elements.Count > 0)
		{
			T item = elements[0];
			Delete(item);
			return item;
		}
		throw new ArgumentOutOfRangeException();
	}

	public void Heapify()
	{
		for (int i = elements.Count - 1; i > 0; i--)
		{
			int parentPosition = (i + 1) / 2 - 1;
			parentPosition = parentPosition >= 0 ? parentPosition : 0;

			if (elements[parentPosition].CompareTo(elements[i]) >= 1)
			{
				T tmp = elements[parentPosition];
				elements[parentPosition] = elements[i];
				elements[i] = tmp;
			}
		}
	}
}


class MaxHeap<T> where T : IComparable
{
	List<T> elements;

	public MaxHeap()
	{
		elements = new List<T>();
	}

	public void Add(T item)
	{
		elements.Add(item);
		Heapify();
	}
	public T Peek()
	{
		return elements[0];
	}
	public int Count
	{
		get
		{
			return elements.Count;
		}
	}
	public void Delete(T item)
	{
		int i = elements.IndexOf(item);
		int last = elements.Count - 1;

		elements[i] = elements[last];
		elements.RemoveAt(last);
		Heapify();
	}

	public T Pop()
	{
		if (elements.Count > 0)
		{
			T item = elements[0];
			Delete(item);
			return item;
		}
		
		throw new ArgumentOutOfRangeException();
	}

	public void Heapify()
	{
		for (int i = elements.Count - 1; i > 0; i--)
		{
			int parentPosition = (i + 1) / 2 - 1;
			parentPosition = parentPosition >= 0 ? parentPosition : 0;

			if (elements[parentPosition].CompareTo(elements[i]) <= -1)
			{
				T tmp = elements[parentPosition];
				elements[parentPosition] = elements[i];
				elements[i] = tmp;
			}
		}
	}
}