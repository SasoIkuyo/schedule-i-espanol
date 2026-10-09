using System.Net.Http;
using System.Text.Json;

namespace ScheduleISpanish;

public sealed record UpdateSettings(bool CheckForUpdates=false,bool PreviewNotification=false)
{
    public static UpdateSettings Load(string directory,bool enabledByDefault)
    {
        Directory.CreateDirectory(directory);
        string path=Path.Combine(directory,"updates.json");
        if(!File.Exists(path)) File.WriteAllText(path,JsonSerializer.Serialize(new UpdateSettings(enabledByDefault),new JsonSerializerOptions {WriteIndented=true}));
        return JsonSerializer.Deserialize<UpdateSettings>(File.ReadAllText(path)) ?? throw new InvalidDataException("Invalid updates.json");
    }
}

public static class UpdateChecker
{
    public const string ReleasesUrl="https://github.com/SasoIkuyo/schedule-i-translate/releases";
    public static Version? FindNewer(string json,Version current,string variant)
    {
        using var document=JsonDocument.Parse(json);
        Version? newest=null;
        foreach(var release in document.RootElement.EnumerateArray())
        {
            if(release.GetProperty("draft").GetBoolean()) continue;
            string tag=release.GetProperty("tag_name").GetString() ?? "";
            if(!Version.TryParse(tag.TrimStart('v','V'),out var version) || version<=current || (newest!=null && version<=newest)) continue;
            // Published previews are included: this project currently distributes prereleases.
            if(!release.GetProperty("assets").EnumerateArray().Any(a=>
                (a.GetProperty("name").GetString() ?? "").Equals($"ScheduleTranslate-{variant}-{version}.zip",StringComparison.OrdinalIgnoreCase))) continue;
            newest=version;
        }
        return newest;
    }

    public static async Task<Version?> CheckAsync(Version current,string variant)
    {
        using var client=new HttpClient {Timeout=TimeSpan.FromSeconds(8),MaxResponseContentBufferSize=2*1024*1024};
        client.DefaultRequestHeaders.UserAgent.ParseAdd("ScheduleITranslate/"+current);
        client.DefaultRequestHeaders.Accept.ParseAdd("application/vnd.github+json");
        string json=await client.GetStringAsync("https://api.github.com/repos/SasoIkuyo/schedule-i-translate/releases?per_page=100").ConfigureAwait(false);
        return FindNewer(json,current,variant);
    }
}
