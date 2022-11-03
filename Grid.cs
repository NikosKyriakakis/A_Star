namespace A_Star
{
    internal class Grid
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        public Tile[,] Tiles { get; set; }
  

        public Grid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Tiles = Create(rows, columns);

            AddNeighbors();
        }

        private static Tile[,] Create(int rows, int columns)
        {
            var tiles = new Tile[rows, columns];

            var generator = new Random();
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < columns; j++)
                {
                    var isWall = generator.NextDouble();

                    string? status;
                    if (isWall < IConstants.WallProbability)
                        status = IConstants.Wall;
                    else
                        status = IConstants.Empty;

                    tiles[i, j] = new Tile(i, j, status);
                }
            }

            return tiles;
        }

        public static int ManhattanDistance(Tile start, Tile end)
        {
            var steps = Math.Abs(start.Row - end.Row) + Math.Abs(start.Column - end.Column);

            return steps;
        }

        private void AddNeighbors()
        {
            for (var row = 0; row < Rows; row++)
            {
                for (var column = 0; column < Columns; column++)
                {
                    var currentTile = Tiles[row, column];

                    if (row < Rows - 1)
                        currentTile.Neighbors.Add(Tiles[row + 1, column]);

                    if (row > 0)
                        currentTile.Neighbors.Add(Tiles[row - 1, column]);

                    if (column > 0)
                        currentTile.Neighbors.Add(Tiles[row, column - 1]);

                    if (column < Columns - 1)
                        currentTile.Neighbors.Add(Tiles[row, column + 1]);
                }
            }       
        }

        public void Print()
        {
            Console.Clear();

            for (var i = 0; i < Rows; i++)
            {
                Console.Write("| ");

                for (var j = 0; j < Columns; j++)
                {
                    Console.ForegroundColor = Tiles[i, j].Status switch
                    {
                        var value when value == IConstants.Mark => ConsoleColor.Green,
                        var value when value == IConstants.Start || value == IConstants.Goal => ConsoleColor.Red,
                        var value when value == IConstants.Wall => ConsoleColor.Blue,
                        _ => ConsoleColor.White,
                    };
                    Console.Write(Tiles[i, j].Status);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(IConstants.Space);
                }
                    
                Console.WriteLine("\n" + new string('-', 41));
            }
        }

        private bool IsValidTile(int row, int column)
        {
            return row >= 0 && row < Rows && column >= 0 && column < Columns;
        }

        public void SetTileStatus(int row, int column, string status)
        {
            if (IsValidTile(row, column))
                Tiles[row, column].Status = status;
            else
                throw new IndexOutOfRangeException("--> Specified row or column out of bounds.");
        }

        public Tile SetStart(int row, int column)
        {
            SetTileStatus(row, column, IConstants.Start);

            return Tiles[row, column];
        }

        public Tile SetEnd(int row, int column)
        {
            SetTileStatus(row, column, IConstants.Goal);

            return Tiles[row, column];
        }
    }
}