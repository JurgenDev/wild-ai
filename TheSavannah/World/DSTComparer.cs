using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheSavannah.World
{
    class DSTComparer : Comparer<Tuple<float, NavNode>>
    {
        public override int Compare(Tuple<float, NavNode> x, Tuple<float, NavNode> y)
        {
            if (x.Item1 > y.Item1)
                return 1;

            if (x.Item1 < y.Item1)
                return -1;

            return 0;
        }
    }
}
