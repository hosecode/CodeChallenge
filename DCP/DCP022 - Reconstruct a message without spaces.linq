<Query Kind="Program" />

/*
This problem was asked by Microsoft.

Given a dictionary of words and a string made up of those words (no spaces), return the original sentence in a list. 
If there is more than one possible reconstruction, return any of them. If there is no possible reconstruction, then return null.

For example, given the set of words 'quick', 'brown', 'the', 'fox', and the string "thequickbrownfox", you should return ['the', 'quick', 'brown', 'fox'].

Given the set of words 'bed', 'bath', 'bedbath', 'and', 'beyond', and the string "bedbathandbeyond", return either ['bed', 'bath', 'and', 'beyond] or ['bedbath', 'and', 'beyond'].
*/
void Main()
{
	Solve(new HashSet<string>(new string[] { "quick", "brown", "the", "fox" }), "thequickbrownfox").Dump("Expect: ['the', 'quick', 'brown', 'fox']");
	Solve(new HashSet<string>(new string[] { "bed", "bath", "bedbath", "and", "beyond" }), "bedbathandbeyond").Dump("Expect: either ['bed', 'bath', 'and', 'beyond] or ['bedbath', 'and', 'beyond']");
	Solve(new HashSet<string>(new string[] { "bed", "bedbath", "and", "beyond" }), "bedbathandbeyond").Dump("Expect: ['bedbath', 'and', 'beyond']");
	Solve(new HashSet<string>(new string[] { "bed", "bedbath", "and", "beyond" }), "bedbathbedbathandbeyond").Dump("Expect: ['bedbath', 'bedbath', 'and', 'beyond']");
	Solve(new HashSet<string>(new string[] {"bed", "bath", "bedbath", "and", "beyond"}), "bedbathandbeyondx").Dump("Expect: null");
}

//Tried writting this iteratively as a greedy algo with backtracking. It makes it must less readable however. 
List<string> Solve(HashSet<string> dict, string code)
{
	List<string> results = new List<string>();
	string word = "";
	int index = 0;
	while(true)
	{
		int i = index;
		for (; i < code.Length; i++)
		{
			word += code[i];
			if (dict.Contains(word))
			{
				results.Add(word);
				word = "";
				if (i == code.Length - 1) return results;   //we found a final word with no left over characters
				index=i+1;
			}
		}
		
		//if we got to the end with leftover characters
		if (i == code.Length) 
		{
			if (results.Count==0) return null; //There is no more backtracking
			//remove the last word, and back track
			word = results[results.Count-1];
			results.RemoveAt(results.Count-1);
		};
	} 
}
