using System;
using System.Collections.Generic;
using System.Text;

namespace JustSomeRandomRPGMechanics
{
    class Path
    {
        List<Distance> pathway;
        int counter;
        public Path()
        {
            pathway = new List<Distance>();
            counter = 0;
        }
        public void AddPath(Distance path)
        {
            pathway.Add(path);
            counter++;
        }
        public Distance FollowPath()
        {
            if (counter ==0)
                return new Distance(0, 0);
            else
            {
                counter--;
                return pathway[counter];
            }
        }
        public bool PathLeftToFollow()
        {
            if (counter >0)
                return true;
            return false;
        }
        public void DisplayFullPath()
        {
            StringBuilder sb = new StringBuilder();
            foreach(Distance dist in pathway)
            {
                sb.Append(dist.X);
                sb.Append(dist.Y);
                sb.Append(" ");
            }
            Display.DisplayDebugMessage(sb.ToString());
        }
    }
}
