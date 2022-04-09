namespace tictactoe
{
    internal abstract class Field : Button ,ICordinate
    {
        abstract internal int State { get; set; }
        abstract internal List<FieldButton> Connections { get; set; }
        abstract public int PosX { get; set; }
        abstract public int PosY { get; set; }
    }
}




