namespace A_Star
{
    internal interface IConstants
    {
        static readonly string Wall = "#";
        static readonly string Empty = " ";
        static readonly string Mark = "*";
        static readonly string Goal = "X";
        static readonly string Space = " | ";
        static readonly string Start = "@";
        static readonly float WallProbability = 0.15f;
    }
}
