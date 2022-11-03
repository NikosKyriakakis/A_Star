namespace A_Star
{
    public class App
    {
        public static void Main()
        {
            var grid = new Grid(10, 10);
            var algo = new Algo();

            algo.Run(grid);
        }
    }
}