namespace Generics
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var farm = new Farm(new Player());
            var game = new Game(farm);
            
            game.StartGame();
        }
    }
}