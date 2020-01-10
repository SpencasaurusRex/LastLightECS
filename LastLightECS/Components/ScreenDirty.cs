using System;
using System.Collections.Generic;

namespace LastLightECS.Components
{
    public class ScreenDirty
    {
        public List<Tuple<int, int>> Coords = new List<Tuple<int, int>>();

        public void Add(int x, int y)
        {
            Coords.Add(new Tuple<int, int>(x, y));
        }
    }
}
