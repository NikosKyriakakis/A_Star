namespace A_Star
{
    internal class Tile
    {
        public string Status { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public float Fscore { get; set; }
        public float Gscore { get; set; }
        public float Hscore { get; set; }

        public IList<Tile> Neighbors { get; set; }
        public Tile? CameFrom { get; set; }

        public Tile(int row, int column, string status)
        {
            Status = status;
            Row = row;
            Column = column;
            Fscore = float.PositiveInfinity;
            Gscore = float.PositiveInfinity;
            Hscore = 0.0f;
            Neighbors = new List<Tile>();
        }

        
    }
}
