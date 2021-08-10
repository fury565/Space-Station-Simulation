using System;
using System.Collections.Generic;
using System.Text;

namespace JustSomeRandomRPGMechanics
{
    static class NumberGenerator
    {
        static Random generator = new Random();
        public static int Generate(int lowerbound,int upperbound)
        {
            return generator.Next(lowerbound, upperbound+1);
        }
    }
}
