using System;
using System.Collections.Generic;
using System.Text;

namespace JustSomeRandomRPGMechanics
{
    class TestPath
    {
        public TestPath path { get; private set; }
        public Distance distance { get; private set; }
        public Distance targetDistance { get; private set; }
        public Distance tileLocation { get; private set; }
        public TestPath(int directionx,int directiony, int tileX,int tileY,Distance distanceToTarget,TestPath path=null)
        {
            distance = new Distance(directionx, directiony);
            this.path = path;
            targetDistance=distanceToTarget;
            tileLocation = new Distance(tileX, tileY);

        }
        public int CalculateDistanceToTarget()
        {
            return (int)Math.Sqrt(Math.Pow(Math.Abs(targetDistance.X- tileLocation.X) * 10, 2) + Math.Pow(Math.Abs(targetDistance.Y- tileLocation.Y) * 10, 2));
        }
        private int CalculateDistanceValue()
        {
            return (int)Math.Sqrt(Math.Pow(distance.X * 10, 2) + Math.Pow(distance.Y * 10, 2));
        }
        public int ReturnTotalDistance()
        {
            if(path!=null)
                return path.CalculateDistanceValue() + CalculateDistanceValue();
            return CalculateDistanceValue();
        }
    }
}
