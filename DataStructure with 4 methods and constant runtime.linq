<Query Kind="Program" />

/*
Asked by Oracle
Create a data structure with the following interface and performance.
Insert(int)
Remove(int)
Search(int) find element determine if exists
Random()		return any element with equal probablity
All should have O(1) time, Memory use is not a concern, and you can specificy a fixed capcity 
*/

//a hash table makes insert, remove, search O(1) (typical case) but rand whould require some way it numerically index into the keys.
//an array would make random easy, but search would be O(n)
//so what if we keep an array and a hash table, but we still need a way to make updates quick. 
//we can use the dictionary with the element as the key, but the value would be the array index
//we could keep a count of elements, and always add to the next slot int the array
//random is just picking any element from the filled part of the array
//delete is the tricky part, but by finding the index of the element from the dictionary, we can swap the last element from the array into that slot
//that will keep the array packed and remove the element in O(1)
void Main()
{
		
}

class DataStructure{

	Dictionary<int,int> lookup;
	int[] array;
	int count=0;
	Random r = new Random();
	
	public DataStructure(int capacity){
		array = new int[capacity];
		lookup = new Dictionary<int, int>(capacity);
	}
	
	public void Insert(int val){
		array[count]= val;
		lookup[val] = count;
		count++;
	}
	
	public void Remove(int val){
		//ignoring all validation and bounds checking :)
		int index = lookup[val];
		array[index] = array[count-1];
		lookup.Remove(val);
		count--;
	}
	
	public bool Search(int val){
		return lookup.ContainsKey(val);
	}
	
	public int Random(){
		return array[r.Next(count)];
	}
}

// Define other methods and classes here
