namespace ScheduleISpanish;

public static class PhoneWording
{
    // Display aliases only: inventory names and supplier item identifiers stay intact.
    public static string Compact(string text) => text switch {
        "Pseudoefedrina de baja calidad"=>"Pseudo (baja calidad)",
        "Pseudoefedrina de alta calidad"=>"Pseudo (alta calidad)",
        _=>text.StartsWith("Se desbloquea en ",StringComparison.Ordinal) ? "Requiere "+text["Se desbloquea en ".Length..] : text
    };
}
