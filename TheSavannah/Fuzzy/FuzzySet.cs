using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheSavannah.Fuzzy
{
    public abstract class FuzzySet : FuzzyTerm
    {
        public double DOM;
        public int center;
        public abstract void CalculateDOM(double d);
        public abstract double GetMax(double min, double max);
        public abstract void ClearDOM();
    }

    public class FuzzySet_Triangle : FuzzySet
    {
        private int deviation;

        public FuzzySet_Triangle(int cen, int devi)
        {
            center = cen;
            deviation = devi;
        }
        public override void CalculateDOM(double d)
        {
            //calculate distance from center
            double diff = Math.Abs(d-center);
            
            //if d equals center point, return one
            if (diff == 0)
                DOM = 1;

            //if it's within the triangle
            if (diff < deviation)
            {
                //calculate DOM
                DOM = 1 - diff/deviation;
            }
            else
            {
                DOM = 0;
            }
        }

        public override double GetMax(double min, double max)
        {
            return center;
        }

        public override void ClearDOM()
        {
            DOM = 0;
        }

        public override double GetDOM()
        {
            return DOM;
        }

        public override void ORWithDOM(double d)
        {
            DOM = Math.Max(DOM, d);
        }
    }

    public class FuzzySet_Shoulder : FuzzySet
    {
        private int slopeWidth;
        private bool negative;

        public FuzzySet_Shoulder(int cen, int slope, bool inv)
        {
            center = cen;
            slopeWidth = slope;
            negative = inv;
        }
        public override void CalculateDOM(double d)
        {
            
            double delta = d - center;

            //if it's a left shoulder, flip everything around
            if (negative)
                delta = -delta;

            if (delta <= 0)
            {
                DOM = 1;
                return;
            }
                

            double slope = delta/slopeWidth;
            if (slope < 1)
                DOM = 1 - slope;
            else
            {
                DOM = 0;
            }

        }

        public override double GetMax(double min, double max)
        {
            if (!negative)
            {
                return (min + center)/2;
            }
            return (max + center)/2;
        }

        public override void ClearDOM()
        {
            DOM = 0;
        }

        public override double GetDOM()
        {
            return DOM;
        }

        public override void ORWithDOM(double d)
        {
            DOM = Math.Max(DOM, d);
        }
    }
}
