void Main()
{
	AssertFalse(canConstruct("a","b"));
	AssertFalse(canConstruct("aa","ab"));
	AssertTrue(canConstruct("aa","aab"));
}

//Can you construct a ransom note given the source letters from a magazine.
//magazine letter can only be used once.
bool canConstruct(string note, string source)
{
	Dictionary<char, int> mag = new Dictionary<char, int>();
	foreach (var element in source)
	{
		int count=0;
		mag.TryGetValue(element, out count);	
		mag[element] = ++count;
	}
	foreach (var element in note)
	{
		int count;
		if (!mag.TryGetValue(element, out count) || count<=0) return false;
		mag[element] = --count;
	}
	return true;
}

void AssertTrue(bool actual)
{
	if (!actual) throw new AssertionFailed("true", "false");
}
void AssertFalse(bool actual)
{
	if (actual) throw new AssertionFailed("false", "true");
}
class AssertionFailed : Exception
{
	public AssertionFailed(String expected, String actual) : base($"Expected: {expected}, Actual: {actual}")
	{
	}
}
