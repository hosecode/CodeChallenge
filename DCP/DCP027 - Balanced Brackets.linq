<Query Kind="Program" />

/*
This problem was asked by Facebook.

Given a string of round, curly, and square open and closing brackets, return whether the brackets are balanced (well-formed).

For example, given the string "([])[]({})", you should return true.

Given the string "([)]" or "((()", you should return false.
*/
void Main()
{
	//data for the hack later. I just didnt want to write multiple statements. 
	foreach (var c in new char[] {'{','}','(',')','[',']'})
	{
		($"{c}:{(int)c}").Dump();
	}

	isBalanced("()").Dump("Expect: true");
	isBalanced("(").Dump("Expect: false");
	isBalanced(")").Dump("Expect: false");
	isBalanced("(]").Dump("Expect: false");
	isBalanced("(])").Dump("Expect: false");
	isBalanced("[(])").Dump("Expect: false");

	isBalanced("([])[]({})").Dump("Expect: true");
	isBalanced("([)]").Dump("Expect: false");
	isBalanced("((()").Dump("Expect: false");
}

bool isBalanced(string input){
	input.Dump("Input");
	Stack<char> nest = new Stack<char>();
	
	foreach (char c in input)
	{
		switch (c)
		{
			case '(':
			case '{':
			case '[':
				nest.Push(c);
				break;
			case ')':
			case '}':
			case ']':
				//hack: since we are dealing with specific inputs, if the val of the char is within 2 then its a match
				if (nest.Count==0 || !( Math.Abs((int)nest.Pop()-(int)c)<=2 )) return false;
				break;
			//ignores all other chars
		}
	}
	return nest.Count==0;
}
