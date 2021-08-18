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
        }
        public Distance FollowPath()
        {
            if (counter == pathway.Count)
                return new Distance(0, 0);
            else
            {
                counter++;
                return pathway[counter - 1];
            }
        }
        public bool PathLeftToFollow()
        {
            if (counter == pathway.Count)
                return false;
            return true;
        }
    }
}
