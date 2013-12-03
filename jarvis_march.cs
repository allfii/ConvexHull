/*
	@ masphei
	email : masphei@gmail.com
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
public class Point
{
	private int y;
	private int x;
	public Point(int _x, int _y)
	{
		x = _x;
		y = _y;
	}
	public int getX()
	{
		return x;
	}
	public int getY()
	{
		return y;
	}
}
public class JarvisMatch
{ 
	const int TURN_LEFT = 1;
	const int TURN_RIGHT = -1;
	const int TURN_NONE = 0;
	public int turn(Point p, Point q, Point r)
	{
		return ((q.getX() - p.getX())*(r.getY() - p.getY()) - (r.getX() - p.getX())*(q.getY() - p.getY())).CompareTo(0);
	}
	
	public int dist(Point p, Point q)
	{
		int dx = q.getX() - p.getX();
		int dy = q.getY() - p.getY();
		return dx * dx + dy * dy;
	}
	
	public Point nextHullPoint(List<Point> points, Point p)
	{
		Point q = p;
		int t;
		foreach (Point r in points)
		{
			t = turn(p, q, r);
			if (t == TURN_RIGHT || t == TURN_NONE && dist(p, r) > dist (p, q))
				q = r;
		}
		return q;
	}
	
	public double getAngle(Point p1, Point p2){
		float xDiff = p2.getX() - p1.getX();
		float yDiff = p2.getY() - p1.getY();
		return Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI;
	}
	
	public void convexHull(List<Point> points)
	{
		Console.WriteLine("# List of Point #");
		foreach (Point value in points)
		{
			Console.Write("("+value.getX()+","+value.getY()+") ");
		}
		Console.WriteLine();
		Console.WriteLine();
		List<Point> hull = new List<Point>();
		foreach (Point p in points)
		{
			if (hull.Count == 0)
				hull.Add(p);
			else
			{
				if (hull[0].getX() > p.getX())
					hull[0] = p;
				else if (hull[0].getX() == p.getX())
					if (hull[0].getY() > p.getY())
						hull[0] =  p;
			}
		}
		Point q;
		int counter = 0;
		Console.WriteLine("The lowest point is ("+hull[0].getX()+", "+hull[0].getY()+")");
		while (counter < hull.Count)
		{
			q = nextHullPoint(points, hull[counter]);
			if (q != hull[0]){
				Console.WriteLine("Next Point is ("+q.getX()+","+q.getY()+") compared to Point ("+hull[hull.Count-1].getX()+","+hull[hull.Count-1].getY()+") : "+getAngle(hull[hull.Count-1], q)+" degrees");
				hull.Add(q);
			}
			counter++;
		}
		Console.WriteLine();
		Console.WriteLine("# Convex Hull #");
		foreach (Point value in hull)
		{
			Console.Write("("+value.getX()+","+value.getY()+") ");
		}
		Console.WriteLine();
	}
 
	public static void Main()
    {
		JarvisMatch jm = new JarvisMatch();
		List<Point> listPoints = new List<Point>();
		listPoints.Add(new Point(9, 1));
		listPoints.Add(new Point(4, 3));
		listPoints.Add(new Point(4, 5));
		listPoints.Add(new Point(3, 2));
		listPoints.Add(new Point(14, 2));
		listPoints.Add(new Point(4, 12));
		listPoints.Add(new Point(4, 10));
		listPoints.Add(new Point(5, 6));
		listPoints.Add(new Point(10, 2));
		listPoints.Add(new Point(1, 2));
		listPoints.Add(new Point(1, 10));
		listPoints.Add(new Point(5, 2));
		listPoints.Add(new Point(11, 2));
		listPoints.Add(new Point(4, 11));
		listPoints.Add(new Point(12, 4));
		listPoints.Add(new Point(3, 1));
		listPoints.Add(new Point(2, 6));
		listPoints.Add(new Point(2, 4));
		listPoints.Add(new Point(7, 8));
		listPoints.Add(new Point(5, 5));

		var stopwatch = new Stopwatch();
		stopwatch.Start();
		jm.convexHull(listPoints);
		stopwatch.Stop();
		float elapsed_time = stopwatch.ElapsedMilliseconds;
		Console.WriteLine("Elapsed time: {0} milliseconds", elapsed_time);
		Console.WriteLine("Press enter to close...");
		Console.ReadLine();
    }
}