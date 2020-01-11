using DefaultEcs;
using System;
using System.Collections.Generic;

namespace LastLightECS.Components
{
    public class Board
    {
        Dictionary<Tuple<int,int>, Entity> Movers;

        public Board()
        {
            Movers = new Dictionary<Tuple<int, int>, Entity>();
        }
    }
}
