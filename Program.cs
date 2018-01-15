namespace ConnectFour
{
    class Program
    {
        /// <summary>
        /// Console application entry point
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Start();
        }
    }
}
