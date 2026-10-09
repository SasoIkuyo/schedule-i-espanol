using System.Reflection;
using HarmonyLib;
using MelonLoader;
using MelonLoader.Utils;
using Il2CppTMPro;
using UnityEngine.UI;
using System.Text.Json;
using System.IO.Compression;

#if ONLINE
[assembly: MelonInfo(typeof(ScheduleISpanish.SpanishMod),"Schedule I Spanish Online","1.2.0","Saso")]
#else
[assembly: MelonInfo(typeof(ScheduleISpanish.SpanishMod),"Schedule I Spanish Offline","1.2.0","Saso")]
#endif
[assembly: MelonGame("TVGS","Schedule I")]

namespace ScheduleISpanish;

public sealed class SpanishMod : MelonMod
{
    private static TranslationEngine? engine;
    [ThreadStatic] private static bool replacing;
    private static int errors;
    private static string targetLanguage="es";
    private static StreamWriter? capture;
    private static readonly HashSet<string> missing = new(StringComparer.Ordinal);
#if ONLINE
    private static OnlineTranslator? online;
    private static readonly Dictionary<string,List<object>> waiting=new(StringComparer.Ordinal);
#endif
    public override void OnInitializeMelon()
    {
        try
        {
            using var resource=typeof(SpanishMod).Assembly.GetManifestResourceStream("ScheduleISpanish.Dictionary.gz") ?? throw new InvalidDataException("Diccionario integrado ausente");
            using var decompressed=new GZipStream(resource,CompressionMode.Decompress);
            using var reader=new StreamReader(decompressed,System.Text.Encoding.UTF8);
            string parent=Path.Combine(MelonEnvironment.GameRootDirectory,"UserData","ScheduleISpanish");
#if ONLINE
            try { targetLanguage=LanguageSettings.Load(parent).TargetLanguage; }
            catch(Exception ex) { LoggerInstance.Warning("language.json: "+ex.Message+" Using es."); }
#endif
            engine=new TranslationEngine(); engine.Load(reader,identitiesOnly:targetLanguage!="es");
#if ONLINE
            if(targetLanguage!="en") online=new OnlineTranslator(engine,parent,targetLanguage:targetLanguage);
            LoggerInstance.Msg("Target language: "+targetLanguage+". Change UserData/ScheduleISpanish/language.json and restart to select another language.");
#endif
            if(File.Exists(Path.Combine(parent,"CaptureMissing.flag")))
                capture=new StreamWriter(Path.Combine(parent,"missing-runtime.jsonl"),append:true,System.Text.Encoding.UTF8,65536);
            var patcher=new HarmonyLib.Harmony("saso.schedule.spanish");
            var prefix=new HarmonyMethod(typeof(SpanishMod).GetMethod(nameof(TextPrefix),BindingFlags.Static|BindingFlags.NonPublic));
            int hooks=0;
            // Only three text classes are inspected. No scene scans or per-frame polling.
            foreach(var type in new[]{typeof(TMP_Text),typeof(Text)})
            foreach(var method in type.GetMethods(BindingFlags.Public|BindingFlags.Instance|BindingFlags.DeclaredOnly))
            {
                var parameters=method.GetParameters();
                if((method.Name=="set_text" || method.Name=="SetText") && parameters.Length>0 && parameters[0].ParameterType==typeof(string))
                {
                    try { patcher.Patch(method,prefix:prefix); hooks++; }
                    catch(Exception ex) { LoggerInstance.Warning("No se pudo interceptar "+type.Name+"."+method.Name+": "+ex.Message); }
                }
            }
            HookEnable(patcher,typeof(Text),nameof(EnableUGUI));
            HookEnable(patcher,typeof(TextMeshProUGUI),nameof(EnableTMP));
            HookEnable(patcher,typeof(TextMeshPro),nameof(EnableTMP));
            LoggerInstance.Msg($"Diccionario integrado: {engine.EntryCount} textos, {engine.RuleCount} patrones, {engine.NumericCount} plantillas numericas. {hooks} setters.");
#if ONLINE
            LoggerInstance.Msg("Modo online: solo frases nuevas, cola limitada y cache persistente. Servicio: Google.");
#else
            LoggerInstance.Msg("Modo offline: sin consultas de red.");
#endif
        }
        catch(Exception ex) { LoggerInstance.Error("No se pudo iniciar la traduccion local: "+ex); engine=null; }
    }

    private static void HookEnable(HarmonyLib.Harmony patcher,Type type,string name)
    {
        try
        {
            var method=AccessTools.DeclaredMethod(type,"OnEnable");
            if(method!=null) patcher.Patch(method,postfix:new HarmonyMethod(typeof(SpanishMod).GetMethod(name,BindingFlags.Static|BindingFlags.NonPublic)));
        }
        catch(Exception ex) { MelonLogger.Warning("No se pudo interceptar OnEnable de "+type.Name+": "+ex.Message); }
    }

    private static void TextPrefix(object __instance,ref string __0)
    {
        if(engine==null || replacing || string.IsNullOrEmpty(__0)) return;
        try { string original=__0; __0=Render(original,engine.Translate(__0)); Capture(original,__0); Track(__instance,original,__0); }
        catch(Exception ex) { Warn(ex); }
    }

    private static void EnableUGUI(Text __instance)
    {
        if(engine==null || replacing) return;
        try
        {
            string original=__instance.text, translated=Render(original,engine.Translate(original));
            Capture(original,translated);
            Track(__instance,original,translated);
            if(original==translated) return;
            replacing=true; __instance.text=translated;
        }
        catch(Exception ex) { Warn(ex); }
        finally { replacing=false; }
    }

    private static void EnableTMP(TMP_Text __instance)
    {
        if(engine==null || replacing) return;
        try
        {
            string original=__instance.text, translated=Render(original,engine.Translate(original));
            Capture(original,translated);
            Track(__instance,original,translated);
            if(original==translated) return;
            replacing=true; __instance.text=translated;
        }
        catch(Exception ex) { Warn(ex); }
        finally { replacing=false; }
    }

    private static string Render(string original,string translated) => targetLanguage!="es" && targetLanguage!="en" && original==translated ? translated : SupportMessage.Append(original,translated,spanish:targetLanguage=="es");

    private static void Warn(Exception ex)
    {
        if(errors++<3) MelonLogger.Warning("Etiqueta no traducida: "+ex.Message);
    }

    private static void Capture(string original,string translated)
    {
        if(capture==null || engine==null || original!=translated || engine.ContainsOriginal(original) || missing.Count>=2048) return;
        if(original.Length>=3 && original.Any(char.IsLetter) && missing.Add(original))
            capture.WriteLine(JsonSerializer.Serialize(original));
    }

    private static void Track(object target,string original,string translated)
    {
#if ONLINE
        if(original!=translated || SupportMessage.IsSupportMessage(original) || online==null || !online.Request(original)) return;
        if(!waiting.TryGetValue(original,out var targets)) waiting[original]=targets=new List<object>();
        if(targets.Count<4 && !targets.Contains(target)) targets.Add(target);
#endif
    }

#if ONLINE
    public override void OnUpdate()
    {
        if(online==null) return;
        for(int i=0;i<8 && online.TryTake(out var result);i++)
        {
            if(result==null || !waiting.Remove(result.Source,out var targets) || result.Translation==null) continue;
            foreach(var target in targets)
            {
                try
                {
                    replacing=true;
                    if(target is TMP_Text tmp && tmp.text==result.Source) tmp.text=Render(result.Source,result.Translation);
                    else if(target is Text text && text.text==result.Source) text.text=Render(result.Source,result.Translation);
                }
                catch(Exception ex) { Warn(ex); }
                finally { replacing=false; }
            }
        }
    }
#endif

    public override void OnDeinitializeMelon()
    {
        capture?.Dispose(); capture=null;
#if ONLINE
        online?.Dispose(); online=null; waiting.Clear();
#endif
    }
}
