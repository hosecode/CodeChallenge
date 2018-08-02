<Query Kind="Program" />

/*
Find a shortest route for sending a message from sender to reciever.
*/
void Main()
{
	Route("Jayden", "Adam").Dump("Expect: { Jayden, Amelia, Adam }");
	Route("Jayden", "Jayden").Dump("Expect: { Jayden }");
	Route("Lucas", "William").Dump("Expect: { Lucas, Adam, Amelia, Jaden, Min, William }");
}

List<string> Route(string src, string dest)
{
	//assuming src, dest are valid.
	
	Dictionary<string, string> path = new Dictionary<string, string>();
	Queue<string> toVisit = new Queue<string>();
	
	path.Add(src, null);	
	toVisit.Enqueue(src);
	
	while (toVisit.Count>0){
		string node = toVisit.Dequeue();

		if (node == dest)
		{
			//walk back path
			List<string> route = new List<string>();
			string n=dest;
			route.Add(dest);
			while(null!=path[n]){
			 	route.Add(n=path[n]);
			}
			route.Reverse();
			return route; 
		}

		foreach (var next in network[node])
		{
			if (!path.ContainsKey(next) && network.ContainsKey(next))
			{
				toVisit.Enqueue(next);
				path.Add(next, node);
			}
		}
	}
	
	//no path
	return null;
}


Dictionary<string, string[]> network = new Dictionary<string, string[]>
{
	{"Min",     new[] { "William", "Jayden", "Omar" }},
	{"William", new[] { "Min", "Noam" }},
	{"Jayden",  new[] { "Min", "Amelia", "Ren", "Noam" }},
	{"Ren",     new[] { "Jayden", "Omar" }},
	{"Amelia",  new[] { "Jayden", "Adam", "Miguel" }},
	{"Adam",    new[] { "Amelia", "Miguel", "Sofia", "Lucas" }},
	{"Miguel",  new[] { "Amelia", "Adam", "Liam", "Nathan" }},
	{"Noam",    new[] { "Nathan", "Jayden", "William" }},
	{"Omar",    new[] { "Ren", "Min", "Scott" }},
	{"Nathan",    new[] { "Noam", "Miguel" }},
	{"Lucas",    new[] { "Adam" }},
};
// Define other methods and classes here
