namespace mines
{
    public abstract class Field : Button, ICordinate
    {
        abstract public bool IsFlaged { get; set; }
        abstract public List<FieldButton> Connections { get; set; }
        abstract public int PosX { get; set; }
        abstract public int PosY { get; set; }
        abstract public bool IsBomb { get; set; }
        abstract public bool IsOpend { get; set; }
        abstract public string state { get; set; }
        
    }
    
}