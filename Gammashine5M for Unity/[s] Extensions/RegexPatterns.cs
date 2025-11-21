namespace System.Text.RegularExpressions
{
    public enum RegexPatternsTypemodel
    {
        PatternNumbers,
    }

    public static class RegexPatterns
    {
        public static string PatternNumbers = @"\d+"; 
    }

    public static class MatchEX
    {
        public static int Match(this Regex regex, string input, RegexPatternsTypemodel typemodel)
        {
            if (typemodel == RegexPatternsTypemodel.PatternNumbers) return int.Parse(Regex.Match(input, RegexPatterns.PatternNumbers).Value);
            else throw new NullReferenceException();
        }
    }
}
