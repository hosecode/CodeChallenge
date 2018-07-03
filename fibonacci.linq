<Query Kind="Program" />

/*
	Testing out finonacci enumerators with closures and minimal state.
	Interesting how you can alternate using two implementations with a shared state.
	Could imagine scenarios where you choose implementation based on current conditions. eg. performant for small vs large inputs. or CPU/memory trade offs. 
*/
void Main()
{
	int a = 1;
	int b = 1;
	Func<int> fibonacci = () =>
	{
		try
		{
			return a;
		}
		finally
		{
			b = a + b;
			a = b - a;
		}
	};
	Func<int> fibonacci2 = () =>
			{
				b = a + b;
				a = b - a;
				return b - a;
			};

	fibonacci().Dump();
	fibonacci2().Dump();
	fibonacci().Dump();
	fibonacci2().Dump();
	fibonacci().Dump();
}

// Define other methods and classes here
