<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

/*
This problem was asked by Apple.

Implement a job scheduler which takes in a function f and an integer n, and calls f after n milliseconds.
*/
void Main()
{
	"Starting Scheduling Tests".Dump("Tests");
	using (Scheduler s = new Scheduler())
	{
		TestCase(s, "Job1", 1000, 5000);
		TestCase(s, "Job2", 1001, 5000);
		TestCase(s, "Job3", 50, 500);
		TestCase(s, "Job4", 2000, 5000);
		PrintTimestamp("Request Shutdown");
		s.Shutdown();
		PrintTimestamp("Complete Shutdown");
		PrintTimestamp("Request join");
		s.Join();
		PrintTimestamp("Completed join");

		PrintTimestamp("Main Thread Sleep");
		Thread.Sleep(10000); //arbitrary sleep to let jobs complete
		PrintTimestamp("Main Thread Awake");
	}

	
	"Starting Async Scheduling Tests".Dump("Async Tests");
	//now test simpler async scheduler
	AsyncSchedulerTest();
}

void TestCase(Scheduler s, string name, int delay, int work){
	Stopwatch sw = Stopwatch.StartNew();
	s.Schedule(name, () =>
		{
			sw.Stop();
			PrintTimestamp($"Started[{name}]");
			PrintTimestamp($"Ellapsed[{name}]: {sw.ElapsedMilliseconds}ms");
			PrintTimestamp($"Delta[{name}]: {sw.ElapsedMilliseconds-delay}ms");
			Thread.Sleep(work);
			PrintTimestamp($"Complete[{name}]: {work}");
		}, delay);
}

/*A basic scheduler that runs jobs on the thread pool. 
This class doesnt use locks and has race conditions which i am ignoring for simplicity. 
This class does not have high accuracy for scheduling. 
This class also cant schedule for the same tick, because its used as the key
*/
class Scheduler : IDisposable
{
	Thread runnerThread;
	SortedList<long, Action> jobs = new SortedList<long, Action>();
	AutoResetEvent jobSchedule = new AutoResetEvent(false);
	volatile bool exit;
	volatile bool drain;

	public Scheduler() { 
	}
	
	public void Schedule(string name, Action f, int delayms)
	{
		DateTimeOffset due = DateTimeOffset.UtcNow.AddMilliseconds(delayms);
		UserQuery.PrintTimestamp($"Schedule[{name}] @" + due.ToString("ss.ffff"));

		//throws arg exception if dup key used
		jobs.Add(due.Ticks, f); 
		if (runnerThread == null)
		{
			runnerThread = new Thread(new ThreadStart(runner));
			runnerThread.Start(); 
		}
		jobSchedule.Set();
	}

	void runner()
	{
		try
		{
			while (!exit)
			{
				if (jobs.Count == 0)
				{
					if (drain) return;
					
					//wait for a job to schedule or exit
					jobSchedule.WaitOne();
					continue;
				}
				
				KeyValuePair<long, Action> nextJobPair = jobs.FirstOrDefault();
				DateTimeOffset now = DateTimeOffset.UtcNow;
				if (nextJobPair.Key <= now.Ticks)
				{
					//push job onto thread pool so scheduler doesnt block
					ThreadPool.QueueUserWorkItem((o) => { nextJobPair.Value(); });
					jobs.Remove(nextJobPair.Key);
					//Since we just did work, don't wait
				}
				else
				{
					//get MS to next job. This is not highly accurate.
					int nextJobMS = (int)(new DateTimeOffset(nextJobPair.Key, TimeSpan.Zero) - DateTimeOffset.UtcNow).TotalMilliseconds;
					//Wake if a job is scheduled, exiting, or timeout if next job is due.
					if (nextJobMS>0) jobSchedule.WaitOne(nextJobMS);
				}
			}
		}
		finally
		{
			UserQuery.PrintTimestamp("SchedulerThreadExit");
		}
	}
	
	//Shutdown will set the thread to exit after scheduling any pending jobs
	//Any running jobs will continue
	public void Shutdown(){
		drain=true;
		jobSchedule.Set();//force wakeup in case no jobs
	}
	
	//Abort will exit the thread and not processing any pending jobs
	//any running jobs will continue to run on the threadpool
	public void Abort(){
		exit=true;
		jobSchedule.Set();//force wakeup
	}
	
	public void Join(){
		if (runnerThread!=null) runnerThread.Join();
	}

	#region IDisposable Support
	private bool disposedValue = false; // To detect redundant calls

	protected virtual void Dispose(bool disposing)
	{
		if (!disposedValue)
		{
			if (disposing)
			{
				// TODO: dispose managed state (managed objects).
				Abort();
			}

			// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
			// TODO: set large fields to null.
			jobs=null;
			disposedValue = true;
		}
	}

	// This code added to correctly implement the disposable pattern.
	public void Dispose()
	{
		// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
		Dispose(true);
	}
	#endregion
}



void AsyncSchedulerTest()
{

	AsyncTestCase("Job 1async", 5000, 5000);
	AsyncTestCase("Job 2async", 2000, 4000);

	//	PrintTimestamp("Schedule Job 2@2000ms");
	//	ScheduleAsync(() => { PrintTimestamp("Job 2"); Thread.Sleep(4000); PrintTimestamp("Job 2 Done"); }, 2000);
	//	PrintTimestamp("Job 2 Scheduled");

	PrintTimestamp($"Exit {nameof(AsyncSchedulerTest)}");
}

void AsyncTestCase(string name, int delay, int workAmount)
{
	PrintTimestamp($"Schedule[{name}] @{delay}ms");
	Stopwatch s = Stopwatch.StartNew();
	ScheduleAsync(() => {
		s.Stop();
		PrintTimestamp($"Started[{name}]");
		PrintTimestamp($"Stopwatch[{name}]: " + s.ElapsedMilliseconds);
		PrintTimestamp($"Delta[{name}]: " + (s.ElapsedMilliseconds-delay));
		Thread.Sleep(workAmount); //fake work 
		PrintTimestamp($"Complete[{name}]: {workAmount}"); 
		}, delay);
	PrintTimestamp($"Scheduled[{name}]");
}

//Simple scheduler using .net async
async void ScheduleAsync(Action job, int delayMS)
{
	await Task.Delay(delayMS).ContinueWith(t => job());
}

static void PrintTimestamp(string s){
	Console.WriteLine(DateTimeOffset.UtcNow.ToString("ss.ffff") + ": " + s);
}