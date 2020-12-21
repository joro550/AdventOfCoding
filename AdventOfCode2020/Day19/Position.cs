namespace AdventOfCode2020.Day19
{
    public record Position(int X, int Y)
    {
        public string GetUniqueKey() {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes($"X:{X},Y:{Y}");
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}