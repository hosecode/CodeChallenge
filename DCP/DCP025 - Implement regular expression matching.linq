<Query Kind="Program" />

/*
This problem was asked by Facebook.

Implement regular expression matching with the following special characters:

. (period) which matches any single character
* (asterisk) which matches zero or more of the preceding element
That is, implement a function that takes in a string and a valid regular expression and returns whether or not the string matches the regular expression.

For example, given the regular expression "ra." and the string "ray", your function should return true. The same regular expression on the string "raymond" should return false.

Given the regular expression ".*at" and the string "chat", your function should return true. The same regular expression on the string "chats" should return false.

*/
void Main()
{
	//TODO: refactor for named test cases that throw to highlight failures.
	Match("", "").Dump("Expect: true");
	Match("", "*").Dump("Expect: true");
	Match("", "**").Dump("Expect: true");
	Match("a", "*").Dump("Expect: true");
	Match("aa", "*").Dump("Expect: true");
	Match("aaa", "*").Dump("Expect: true");
	Match("aaa", "**").Dump("Expect: true");
	Match("aaa", "*.*").Dump("Expect: true");
	Match("a", ".").Dump("Expect: true");
	Match("a", "..").Dump("Expect: false");
	Match("aa", ".").Dump("Expect: false");
	Match("aa", "a.a").Dump("Expect: false");
	Match("aaa", "a.a").Dump("Expect: true");
	Match("a", "a").Dump("Expect: true");
	Match("aa", "a").Dump("Expect: false");
	Match("a", "aa").Dump("Expect: false");

	Match("ray", "ray").Dump("Expect: true");
	Match("ray", "rays").Dump("Expect: false");
	Match("rays", "ray").Dump("Expect: false");
	Match("ray", "ra.").Dump("Expect: true");
	Match("raymond", "ra.").Dump("Expect: false");
	Match("chat", ".*at").Dump("Expect: true");
	Match("chats", ".*at").Dump("Expect: false");
}

//TODO: refactor for simplicity and readability 
bool Match(string source, string pattern)
{
	//if no source and no remaining pattern then it's a match
	if (source == "" && pattern == "") return true;
	//if source has remaining characters but no pattern then that is not a match
	if (source.Length > 0 && pattern == "") return false;
	// If source==0 and pattern remaining will be handled below because we must condsider if pattern has one or more '*'

	char p = pattern[0];

	if (p == '*')
	{
		if (Match(source, pattern.Substring(1))) return true; //0 Characters consumed, '*' consumed then match
		if (source.Length>0 && Match(source.Substring(1), pattern)) return true; //1+ Characters consumed (after recursion), '*' not consumed then match
	}
	else if (p == '.')
	{
		//if there is no source to consume then not a match
		if (source=="") return false;
		//consume 1 source and 1 pattern and match
		return Match(source.Substring(1), pattern.Substring(1));
	}
	else
	{	//There is no source to consume
		if (source=="") return false;
		//source does not match pattern
		if (source[0]!=p) return false;
		//consume 1 source, 1 pattern and match
		return Match(source.Substring(1), pattern.Substring(1));
	}

	return false;
}

