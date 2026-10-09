using System.Text.Json;

namespace ScheduleISpanish;

public sealed record DisplaySettings(int PhoneTextScale=2)
{
    public static DisplaySettings Load(string directory)
    {
        Directory.CreateDirectory(directory);
        string path=Path.Combine(directory,"display.json");
        if(!File.Exists(path)) File.WriteAllText(path,JsonSerializer.Serialize(new DisplaySettings(),new JsonSerializerOptions {WriteIndented=true}));
        var settings=JsonSerializer.Deserialize<DisplaySettings>(File.ReadAllText(path)) ?? throw new InvalidDataException("Invalid display.json");
        if(settings.PhoneTextScale is <1 or >2) throw new InvalidDataException("PhoneTextScale must be 1 (original) or 2 (sharper).");
        return settings;
    }
}
