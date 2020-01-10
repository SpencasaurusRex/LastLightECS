using System;

namespace LastLightECS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (Game game = new Game())
            {
                game.Start();
            }
        }
    }
}
