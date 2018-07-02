<Query Kind="Program" />

/*
This problem was asked by Twitter.

Implement an autocomplete system. That is, given a query string s and a set of all possible query strings, return all strings in the set that have s as a prefix.

For example, given the query string de and the set of strings [c], return [deer, deal].

Hint: Try preprocessing the dictionary into a more efficient data structure to speed up queries.
*/
void Main()
{
	AutoComplete ac = new AutoComplete();
	ac.Populate(new[]{"dog", "deer", "deal"});
	ac.PrintWords();
	ac.Query("de").Dump("Expect: [deer, deal]");
	ac.Query("dea").Dump("Expect: [deal]");
	ac.Query("deal").Dump("Expect: [deal]");
	ac.Query("deals").Dump("Expect: []");
	ac.Query("").Dump("Expect: [dog, deer, deal]");
	ac.Query("a").Dump("Expect: []");
}

class AutoComplete{

	Node root = new Node();
	
	public void Populate(ICollection<string> words)
	{
		foreach (var word in words)
		{
			AddWord(word);
		}
	}

	public List<string> Query(string s){
		Node node = FindNode(s);
		List<string> results = new List<string>();
		if (node!=null) node.ChildWords(results);
		return results;
	}
	Node FindNode(string s)
	{
		Node node = root;
		foreach (char c in s)
		{
			if (!node.next.TryGetValue(c, out node))
			{
				return null;
			}
		}
		return node;
	}
	void AddWord(string s)
	{
		Node node = root;
		foreach (char c in s)
		{
			Node nextNode;
			if (!node.next.TryGetValue(c, out nextNode))
			{
				nextNode = new Node(c);
				node.next[c] = nextNode;
			}
			node = nextNode;
		}
		node.complete = s;
	}
	
	public void PrintWords(){
		List<string> words = new List<string>();
		root.ChildWords(words);	// this looks a lot like Query("")
		foreach (var word in words)
		{
			Console.WriteLine(word);
		}
	}
}

class Node
{
	public Node() {}
	public Node(char val){
		this.val = val;
	}
	public char val;
	public string complete=null;
	public Dictionary<char, Node> next = new Dictionary<char, Node>();

	//if size wasn't an issue we could store a list of all complete words at each node, rather than searching
	public void ChildWords(List<string> result){
		foreach (Node node in next.Values)
		{
			node.ChildWords(result);
		}
		if (complete!=null) result.Add(complete);
	}
}

