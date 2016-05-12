/*
	@ masphei
	email : masphei@gmail.com
*/
// --------------------------------------------------------------------------
// 2016-05-11 <oss.devel@searchathing.com> : created csprj and splitted Main into a separate file
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace ConvexHull
{

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
     
}