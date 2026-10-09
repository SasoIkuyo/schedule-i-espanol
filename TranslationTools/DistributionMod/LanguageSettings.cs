using System.Text.Json;
using System.Text.RegularExpressions;

namespace ScheduleISpanish;

public sealed record LanguageSettings(string TargetLanguage="es")
{
    public static string Validate(string language)
    {
        language=language.Trim();
        if(!Regex.IsMatch(language,@"^[a-z]{2,3}(?:-[A-Za-z]{2,4})?$",RegexOptions.CultureInvariant))
            throw new ArgumentException("Use a Google Translate language code, for example es, fr, de, pt or zh-CN.");
        return language;
    }

    public static LanguageSettings Load(string directory)
    {
        Directory.CreateDirectory(directory);
        string path=Path.Combine(directory,"language.json");
        if(!File.Exists(path)) File.WriteAllText(path,JsonSerializer.Serialize(new LanguageSettings(),new JsonSerializerOptions {WriteIndented=true}));
        var settings=JsonSerializer.Deserialize<LanguageSettings>(File.ReadAllText(path)) ?? throw new InvalidDataException("Invalid language.json");
        return settings with {TargetLanguage=Validate(settings.TargetLanguage)};
    }
}
