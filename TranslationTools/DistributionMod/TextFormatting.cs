using System.Text.RegularExpressions;

namespace ScheduleISpanish;

public static class TextFormatting
{
    private static readonly Regex Visible=new(@"<[^>]*>|\[[0-9]+\]|[^<]",RegexOptions.CultureInvariant,TimeSpan.FromMilliseconds(10));
    public static string CapitalizeFirstVisible(string text)
    {
        foreach(Match match in Visible.Matches(text))
        {
            if(match.Value.Length!=1 || !char.IsLetter(match.Value[0])) continue;
            char upper=char.ToUpperInvariant(match.Value[0]);
            return upper==match.Value[0] ? text : text[..match.Index]+upper+text[(match.Index+1)..];
        }
        return text;
    }
}
