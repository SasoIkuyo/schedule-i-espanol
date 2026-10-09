namespace ScheduleISpanish;

public static class SupportMessage
{
    public const string DonationUrl="https://buymeacoffee.com/sasoikuyo";
    public static string Append(string original,string translated,string url=DonationUrl,bool spanish=true)
    {
        if(!original.StartsWith("TVGS does not condone",StringComparison.Ordinal)) return translated;
        if(!Uri.TryCreate(url,UriKind.Absolute,out var profile) || profile.Scheme!="https" ||
           (profile.Host!="buymeacoffee.com" && profile.Host!="www.buymeacoffee.com") || profile.AbsolutePath.Trim('/').Length==0) return translated;
        if(translated.Contains(url,StringComparison.Ordinal)) return translated;
        string message=spanish ? "¿Te sirve esta traducción? Puedes apoyar su desarrollo aquí:" : "Enjoying this translation? Support its development here:";
        return translated.TrimEnd()+"\n\n<color=#FFDD00>"+message+"\n"+url+"</color>";
    }

    public static bool IsSupportMessage(string text) => DonationUrl.Length>0 && text.Contains(DonationUrl,StringComparison.Ordinal);
}
