using System;

namespace notris
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new NotrisGame())
                game.Run();
        }
    }
}
