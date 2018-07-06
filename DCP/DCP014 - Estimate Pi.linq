<Query Kind="Program" />

/*
This problem was asked by Google.

The area of a circle is defined as πr^2. Estimate π to 3 decimal places using a Monte Carlo method.

Hint: The basic equation of a circle is x2 + y2 = r2.
*/
void Main()
{
	//take a circle of radius 1 at the orgin, then take a square of width 1 {(0,0),(1,0),(0,1),(1,1)}
	//That square encompasses 1/4 of the circles area
	//If we take random x,y points we can determine if they are inside or outside the circle by calculationg
	//the distance from the origin (pythagoras)
	//The ratio of points inside circle should approx PI/4 with enough samples. 
	//Area of a square is x*x
	//Area of cicle is PIr^2
	// x = 2r
	//P is the proportion or ratio
	// (x*x) * P = PI*r^2
	// P = (PI*r^2)/(x*x)
	// P = (PI*r^2)/(2r*2r)
	// P = (PI*r^2)/(4r^2)
	// P = (PI)/(4)

	//I'm not exactly sure how to estimate precision. Some thoughts are watching how result changes and when 3 digits seem constant
	//or calulating a moving average and std deviation
	PIEstimate(100000000).Dump("3.14159265359");
}

// Define other methods and classes here

double PIEstimate(int samples){
	Random r = new Random(); //could use seed be deterministic 
	int inside=1;
	int outside = 1;
	while (inside + outside < samples)
	{
		double x = r.NextDouble();
		double y = r.NextDouble();
		if (Distance(x,y)>1){
			outside++;
		}
		else{
			inside++;
		}
	}
	return inside/(double)samples*4;
}

double Distance( double x, double y){
	
	return Math.Sqrt(x*x+y*y);
}

