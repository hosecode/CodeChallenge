<Query Kind="Program" />

/*
This problem was asked by Twitter.

You run an e-commerce website and want to record the last N order ids in a log. Implement a data structure to accomplish this, with the following API:

record(order_id): adds the order_id to the log
get_last(i): gets the ith last element from the log. i is guaranteed to be smaller than or equal to N.
You should be as efficient with time and space as possible.
*/
void Main()
{
	//Could store in linked list and add to the head and delete the N+1 tail, but access would be N
	//If N is known and fixed can store in an array as a circular buffer with a pointer. This would have O(1) lookups and O(n) memory
	RecentOrders ro = new RecentOrders(1);
	ro.AddOrder("a");
	ro.get_last(1).Dump();
	ro.AddOrder("b");
	ro.AddOrder("c");
	ro.get_last(1).Dump();

	ro = new RecentOrders(3);
	ro.AddOrder("a");
	ro.AddOrder("b");
	ro.AddOrder("c");
	ro.get_last(1).Dump();
	ro.get_last(2).Dump();
	ro.get_last(3).Dump();
}

class RecentOrders{
	
	string[] buffer;
	int index;
	int count=0;
	public RecentOrders(int capacity){
		buffer = new string[capacity];
	}
	public void AddOrder(string orderId){
		buffer[index] = orderId;
		index =  index + 1 % buffer.Length; 
		if (count<buffer.Length) count++; 
	}
	public string get_last(int i){
		if (i>count) throw new ArgumentOutOfRangeException();
		return buffer[(buffer.Length+index - i) % buffer.Length];
	}
}

// Define other methods and classes here
