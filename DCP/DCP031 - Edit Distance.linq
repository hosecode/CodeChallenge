<Query Kind="Program" />

/*
This problem was asked by Google.

The edit distance between two strings refers to the minimum number of character insertions, 
deletions, and substitutions required to change one string to the other. 

For example, the edit distance between “kitten” and “sitting” is three: substitute the “k” for “s”, substitute the “e” for “i”, and append a “g”.

Given two strings, compute the edit distance between them.
*/
/*
Solution Approach:
If you only considered substitutions then it would be simpiler, but you must consider  axyz and xyzz. if you replace a->x youd miss the minimal solution 
- the deletion that causes xyz to shift into place.
Since it would be difficult to know for sure which action to take you may need to branch and try several and take the minimal solution. A greedy algo could
lead down the wrong path, so can we derive a backtracking, dynamic programming, or divide and conquer solution.
-couple things we know:
	- could replace each letter, then insert/delete until done, so worst case is k score (k is number of letters of result word)
	- no point in deleting letter, or inserting more that k length.
-Since we are trying to mimimize it feels like a DP problem. 
Track index for each word. if chars match then we move to next char for both ptrs.
if not a match, then do 1 operation. we either move both or 1 of the ptrs and add 1 to the score. 
if we get to end of either word then its either all insertions or all deletions. 
memoize with both pts as key to avoid recaluating. 
*/
void Main()
{
	EditDistance("a", "b").Dump("Expect: 1");
	EditDistance("", "a").Dump("Expect: 1");
	EditDistance("a", "b").Dump("Expect: 1");
	EditDistance("kitten", "sitting").Dump("Expect: 3");
	memo.Stats().Dump();
	EditDistance("abbbb", "bbbb").Dump("Expect: 1");
	EditDistance("aa", "abbba").Dump("Expect: 3");
	EditDistance("abbba", "aa").Dump("Expect: 3");
	EditDistance("bcdef", "abcdef").Dump("Expect: 1");
	EditDistance("bcdef", "abcde").Dump("Expect: 2");
}


int EditDistance(string src, string dest)
{ 
	memo = new Cache();
	return EditDistance(src, dest, src.Length, dest.Length);
}
int EditDistance(string src, string dest, int n, int m){
	
	int result;
	if (memo.Lookup(Key(n,m), out result)) return result;
	if (n==0) return m;
	if (m==0) return n;

	if (src[n-1] == dest[m-1])
	{
		result = EditDistance(src, dest, n - 1, m - 1);
	}
	else
	{
		result = 1 + Min(
			EditDistance(src, dest, n - 1, m - 1),
			EditDistance(src, dest, n - 1, m ), //deletion
			EditDistance(src, dest, n, m - 1) //insertion
		);
	}

	memo.Add(Key(n,m), result);
	return result;
	
}

int Min(params int[] args){
	int min = args[0];
	for(int i=1;i<args.Length;i++){
		min = Math.Min(min, args[i]);
	}
	return min;
}
Func<int, int, string> Key = (x, y) => { return $"{x},{y}";};

Cache memo;
class Cache
{
	int hits = 0;
	int lookups = 0;
	Dictionary<string, int> cache = new Dictionary<string, int>();
	public bool Lookup(string key, out int value)
	{
		lookups++;
		if (!cache.TryGetValue(key, out value)) return false;

		hits++;
		return true;
	}
	public void Add(string key, int value)
	{
		cache.Add(key, value);
	}
	
	public string Stats()
	{
		return $"Hits: {hits}, Lookups: {lookups}, Size: {cache.Keys.Count}";
	}
}