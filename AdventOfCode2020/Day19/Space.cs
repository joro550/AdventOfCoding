namespace AdventOfCode2020.Day19
{
    public record Space(Position Position, bool IsActivated)
    {
        public Space(int x, int y, bool isActivated)
            :this(new Position(x, y), isActivated)
        {
            
        }

        public string GetString()
        {
            return IsActivated ? "#" : ".";
        }
        
        public string GetUniqueKey() 
            => Position.GetUniqueKey();
    }
}