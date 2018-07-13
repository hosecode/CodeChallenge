<Query Kind="Program" />

/*
Asked by Oracle
Write a throttling method, that can accept X requests per period  eg. 100 per second
*Single machine, single user
*/
// this is an exact solution, but there are popular approximation methods like exponential decay to consider as well 
// esp if this is 1million requests per hour
void Main()
{
	
}

const int Limit = 100;
readonly TimeSpan window = TimeSpan.FromSeconds(1);
Queue<DateTime> traffic = new Queue<DateTime>();
bool Allowed(){
		//while old traffic is outside the window, remove it. 
		while (traffic.Count>0 && traffic.Peek()<DateTime.UtcNow-window) traffic.Dequeue();
		
		if (traffic.Count > Limit) return false;
		
		traffic.Enqueue(DateTime.UtcNow);
		return true;
}