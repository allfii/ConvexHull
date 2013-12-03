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
public class GrahamScan
{ 
	const int TURN_LEFT = 1;
	const int TURN_RIGHT = -1;
	const int TURN_NONE = 0;
	public int turn(Point p, Point q, Point r)
	{
		return ((q.getX() - p.getX())*(r.getY() - p.getY()) - (r.getX() - p.getX())*(q.getY() - p.getY())).CompareTo(0);
	}
	
	public void keepLeft(List<Point> hull, Point r)
	{
		while (hull.Count>1 && turn(hull[hull.Count-2], hull[hull.Count-1], r) !=  TURN_LEFT){
			Console.WriteLine("Removing Point ({0}, {1}) because turning right ", hull[hull.Count-1].getX(), hull[hull.Count-1].getY());
			hull.RemoveAt(hull.Count-1);
		}
		if (hull.Count==0 || hull[hull.Count-1] != r){
			Console.WriteLine("Adding Point ({0}, {1})", r.getX(), r.getY());
			hull.Add(r);
		}
		Console.WriteLine("# Current Convex Hull #");
		foreach (Point value in hull)
		{
			Console.Write("("+value.getX()+","+value.getY()+") ");
		}
		Console.WriteLine();
		Console.WriteLine();
		
	}
	
	public double getAngle(Point p1, Point p2){
		float xDiff = p2.getX() - p1.getX();
		float yDiff = p2.getY() - p1.getY();
		return Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI;
	}
	
	public List<Point> MergeSort ( Point p0, List<Point> arrPoint ) {
            if (arrPoint.Count == 1) {
                return arrPoint;
            }
            List<Point> arrSortedInt = new List<Point>();
            int middle = (int)arrPoint.Count/2;
            List<Point> leftArray = arrPoint.GetRange(0, middle);
            List<Point> rightArray = arrPoint.GetRange(middle, arrPoint.Count - middle);
            leftArray =  MergeSort(p0, leftArray);
            rightArray = MergeSort(p0, rightArray);
            int leftptr = 0;
            int rightptr=0;
            for (int i = 0; i < leftArray.Count + rightArray.Count; i++) {
                if (leftptr==leftArray.Count){
                    arrSortedInt.Add(rightArray[rightptr]);
                    rightptr++;
                }else if (rightptr==rightArray.Count){
                    arrSortedInt.Add(leftArray[leftptr]);
                    leftptr++;
                }else if (getAngle(p0, leftArray[leftptr])<getAngle(p0, rightArray[rightptr])){
                    arrSortedInt.Add(leftArray[leftptr]);
                    leftptr++;
                }else{
                    arrSortedInt.Add(rightArray[rightptr]);
                    rightptr++;
                }
            }
            return arrSortedInt;
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
		
		Point p0 = null;
		foreach (Point value in points)
		{
			if (p0 == null)
				p0 = value;
			else{
				if (p0.getY() > value.getY())
					p0 = value;
			}
		}
		List <Point> order = new List<Point>();
		foreach (Point value in points)
		{
			if (p0 != value)
				order.Add(value);
		}
		
		order = MergeSort(p0, order);
		Console.WriteLine("# Sorted points based on angle with point p0 ({0},{1})#", p0.getX(), p0.getY());
		foreach (Point value in order)
		{
			Console.WriteLine("("+value.getX()+","+value.getY()+") : {0}",getAngle(p0, value));
		}
		List<Point> result = new List<Point>();
		result.Add(p0);
		result.Add(order[0]);
		result.Add(order[1]);
		order.RemoveAt(0);
		order.RemoveAt(0);
		Console.WriteLine("# Current Convex Hull #");
		foreach (Point value in result)
		{
			Console.Write("("+value.getX()+","+value.getY()+") ");
		}
		Console.WriteLine();
		Console.WriteLine();
		foreach (Point value in order)
		{
			keepLeft(result, value);
		}
		Console.WriteLine();
		Console.WriteLine("# Convex Hull #");
		foreach (Point value in result)
		{
			Console.Write("("+value.getX()+","+value.getY()+") ");
		}
		Console.WriteLine();
	}

	public static void Main()
    {
		GrahamScan gs = new GrahamScan();
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
		gs.convexHull(listPoints);
		stopwatch.Stop();
		float elapsed_time = stopwatch.ElapsedMilliseconds;
		Console.WriteLine("Elapsed time: {0} milliseconds", elapsed_time);
		Console.WriteLine("Press enter to close...");
		Console.ReadLine();
    }
}