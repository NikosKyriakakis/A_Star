namespace A_Star
{
    internal class Algo
    {
        private readonly PriorityQueue<Tile, float> _minHeap = new();
        private readonly HashSet<Tile> _openSet = new();
        private readonly HashSet<Tile> _closedSet = new();

        public void Run(Grid grid, int startRow = 0, int startColumn = 0, int endRow = 7, int endColumn = 8)
        {
            var start = grid.SetStart(startRow, startColumn);
            var end = grid.SetEnd(endRow, endColumn);

            grid.Print();
            Console.WriteLine("\n--> Press any key to find path ...");
            Console.ReadKey();

            start.Gscore = 0;
            start.Hscore = 1;
            start.Fscore = start.Gscore + start.Hscore;

            _minHeap.Enqueue(start, start.Fscore);
            _ = _openSet.Add(start);

            while (_openSet.Count > 0)
            {
                var current = _minHeap.Dequeue();
                if (current == end)
                {
                    var path = new List<Tile>();
                    var previous = current;

                    path.Add(previous);
                    while (previous.CameFrom != null)
                    {
                        var row = previous.Row;
                        var column = previous.Column;

                        grid.Tiles[row, column].Status = IConstants.Mark;

                        path.Add(previous.CameFrom);
                        previous = previous.CameFrom;
                    }

                    grid.Tiles[end.Row, end.Column].Status = IConstants.Goal;

                    grid.Print();
                    Console.ReadKey();
                    break;
                }

                _ = _openSet.Remove(current);
                _ = _closedSet.Add(current);

                var neighbors = current.Neighbors;
                foreach (var neighbor in neighbors)
                {
                    if (!_closedSet.Contains(neighbor) && neighbor.Status != IConstants.Wall)
                    {
                        var tentativeGscore = current.Gscore + Grid.ManhattanDistance(current, neighbor);

                        if (tentativeGscore < neighbor.Gscore)
                        {
                            neighbor.CameFrom = current;
                            neighbor.Gscore = tentativeGscore;
                            neighbor.Hscore = Grid.ManhattanDistance(neighbor, end);
                            neighbor.Fscore = neighbor.Gscore + neighbor.Hscore;
                        }

                        if (!_openSet.Contains(neighbor))
                        {
                            _minHeap.Enqueue(neighbor, neighbor.Fscore);
                            _openSet.Add(neighbor);
                        }
                    }
                }
            }
            
        }
    }
}
