using ScheduleISpanish;
using System.IO.Compression;
using System.Reflection;

void Assert(bool condition,string message) { if(!condition) throw new Exception(message); }
const string disclaimerSource="TVGS does not condone the manufacturing, trade, or use of illegal drugs.";
const string disclaimerSpanish="Aviso del juego en español.";
Assert(SupportMessage.Append(disclaimerSource,disclaimerSpanish,"")==disclaimerSpanish,"Unset profile shows an invented link");
Assert(SupportMessage.Append(disclaimerSource,disclaimerSpanish).Contains("https://buymeacoffee.com/sasoikuyo"),"Owner's profile missing");
Assert(SupportMessage.Append(disclaimerSource,disclaimerSpanish,"https://buymeacoffee.com")==disclaimerSpanish,"Generic homepage accepted as owner's profile");
string support=SupportMessage.Append(disclaimerSource,disclaimerSpanish,"https://buymeacoffee.com/test-support");
Assert(support.StartsWith(disclaimerSpanish) && support.Contains("¿Te sirve esta traducción?") && support.Contains("https://buymeacoffee.com/test-support"),"Support footer invalid");
Assert(SupportMessage.Append(disclaimerSource,support,"https://buymeacoffee.com/test-support")==support,"Support footer repeated");
TranslationEngine Load(string mode)
{
    string file=mode=="Online" ? "ScheduleTranslate.dll" : "ScheduleISpanish.dll";
    string path=Path.GetFullPath($"TranslationTools/DistributionMod/bin/{mode}/Release/net6.0/{file}");
    var assembly=Assembly.LoadFile(path);
    using var resource=assembly.GetManifestResourceStream("ScheduleISpanish.Dictionary.gz") ?? throw new Exception("Missing embedded dictionary");
    using var gzip=new GZipStream(resource,CompressionMode.Decompress);
    using var reader=new StreamReader(gzip);
    var result=new TranslationEngine(); result.Load(reader);
    bool http=assembly.GetReferencedAssemblies().Any(r=>r.Name=="System.Net.Http");
    Assert(http==(mode=="Online"),"HTTP dependency in wrong variant");
    return result;
}
var offline=Load("Offline"); var onlineEngine=Load("Online");
Assert(offline.Translate("Can I interest you in a free sample?")=="¿Quieres una muestra gratis?","Short sample option");
Assert(offline.Translate("I'm Stuck")=="Estoy estancado","Menu capitalization");
Assert(offline.Translate("My vehicle is stuck")=="Mi vehículo está atascado","Vehicle menu capitalization");
onlineEngine.AddLearned("'DOCKS' REGION MUST BE UNLOCKED","LA REGIÓN 'DOCKS' DEBE ESTAR DESBLOQUEADA");
Assert(onlineEngine.Translate("'DOCKS' REGION MUST BE UNLOCKED")=="Desbloquea Docks","Old online requirement cache overrides curated wording");
Assert(offline.Translate("'UPTOWN' REGION MUST BE UNLOCKED")=="Desbloquea Uptown","Dynamic region requirement");
Assert(offline.ContainsOriginal("Desbloquea Uptown"),"Curated requirement sent online again");
Assert(offline.EntryCount==onlineEngine.EntryCount,"Variants have different dictionaries");
Assert(offline.Translate("Save game")=="Guardar partida","Save label");
Assert(offline.Translate("We Need To Cook")=="Tenemos que cocinar","Quest title");
Assert(offline.Translate("Weed Seeds")=="Semillas de marihuana","Cannabis seeds context");
Assert(offline.Translate("Green Crack Seed")=="Semilla de Green Crack","Strain name translated literally");
Assert(offline.Translate("Plastic Pot")=="Maceta de plástico","Pot confused with cookware");
Assert(offline.Translate("Baggie")=="Bolsita" && offline.Translate("Acid")=="Ácido" && offline.Translate("Bed")=="Cama","Item capitalization");
Assert(offline.Translate("Albert Hoover")=="Albert Hoover","NPC name changed");
Assert(offline.Translate("Talk to Jeff")=="Hablar con Jeff","NPC interaction");
Assert(offline.Translate("Can't sleep before 6:00 PM")=="No puedes dormir antes de las 6:00 PM","Bed time restriction");
Assert(offline.Translate("Cheap Skateboard<color=#54E717> ($75)</color>")=="Patineta barata<color=#54E717> ($75)</color>","NPC skateboard option with price");
Assert(offline.Translate("Golden Skateboard<color=#54E717> ($1,500)</color>")=="Patineta dorada<color=#54E717> ($1,500)</color>","Price with thousands separator changed");
Assert(offline.Translate("I'd like to purchase something")=="Me gustaría comprar algo","Menu sentence case");
Assert(TextFormatting.CapitalizeFirstVisible("<color=green>[1]</color> me gustaría comprar algo")=="<color=green>[1]</color> Me gustaría comprar algo","Casing changed markup or hotkey");
Assert(offline.Translate("(1 day, 20 hours remaining)")=="(1 día, 20 horas restantes)","Remaining hours from screenshot");
Assert(offline.Translate("(<color=#00FF00>1 day, 20 hours remaining</color>)")=="(<color=#00FF00>1 día, 20 horas restantes</color>)","Duration with internal color tag");
Assert(offline.Translate("<color=green>(1 day, <b>20 hours</b> remaining)</color>")=="<color=green>(1 día, <b>20 horas</b> restantes)</color>","Duration nested rich text");
Assert(offline.Translate("Hey, I need 2x Granddaddy Purple. I'll pay <color=#46CB4F>$145</color>. Deal?")=="Oye, necesito 2x Granddaddy Purple. Te pago <color=#46CB4F>$145</color>. ¿Trato hecho?","Customer message with colored price");
Assert(offline.Translate("How about 5x Granddaddy Purple for $455?")=="¿Qué tal 5 unidades de Granddaddy Purple por $455?","Phone counteroffer");
string phoneMeeting=offline.Translate("Ok, I'll meet you in front of the motel between 6:00 PM and 12:00 AM.");
Assert(offline.Translate("Request Dead Drop")=="Pedir mercancía","Supplier order title context");
Assert(offline.Translate("Select items to order from Shirley")=="Elige qué pedirle a Shirley","Supplier subtitle/name");
Assert(offline.Translate("Order Total")=="Total" && offline.Translate("Order Limit ")=="Máximo" && offline.Translate("Item Limit")=="Artículos","Compact phone order captions");
Assert(offline.Translate("Got it. I'll let you know when it's ready. Should be about 4 hours")=="Entendido. Te avisaré cuando esté listo. Tiempo estimado: 4 horas","Supplier message duration");
Assert(SpanishGrammar.TranslateTimeSpan("<b>1 hour</b>, 20 minutes")=="<b>1 hora</b>, 20 minutos","Formatted delivery duration");
Assert(SpanishGrammar.TranslateTimeSpan("6:00 PM")=="6:00 PM","Meeting clock changed by duration helper");
Assert(PhoneWording.Compact("Pseudoefedrina de baja calidad")=="Pseudo (baja calidad)","Phone product alias");
Assert(PhoneWording.Compact("Se desbloquea en Hustler III")=="Requiere Hustler III","Phone rank alias");
Assert(PhoneWording.Compact("Granddaddy Purple")=="Granddaddy Purple","Phone alias changes strain name");
Assert(phoneMeeting=="Vale, nos vemos frente al motel entre las 6:00 PM y las 12:00 AM.","Phone meeting location/time: "+phoneMeeting);
Assert(offline.Translate("Use Packaging Station")=="Usar estación de envasado","Packaging interaction prompt");
Assert(offline.Translate("Use Mixing Station")=="Usar "+offline.Translate("Mixing Station"),"Other known-item interaction prompt");
Assert(offline.Translate("Today")=="Hoy" && offline.Translate("Last Played")=="Jugado" && offline.Translate("Net worth")=="Patrimonio","Save screen captions/date");
string mission="Deal for the Benzies Family\n<color=#00FF00>(2 days, 9 hours remaining)</color>\n• (Optional) Message Thomas to call off the truce\n• Deliver 30x OG Kush to Hyland Manor";
string translatedMission=offline.Translate(mission);
Assert(translatedMission=="Traficar para la familia Benzies\n<color=#00FF00>(2 días, 9 horas restantes)</color>\n• (Opcional) Escribe a Thomas para poner fin a la tregua\n• Entrega 30 unidades de OG Kush en Hyland Manor","Combined mission block: "+translatedMission);
Assert(offline.Translate("Hmm... Oscar seen one lying around somewhere in <color=blue>Westville</color> I think...")=="Mmm... Oscar vio uno por ahí, en <color=blue>Westville</color>, creo...","Runtime region placeholder");
Assert(offline.Translate("(1 day, 1 hour remaining)")=="(1 día, 1 hora restantes)","Singular remaining time");
Assert(offline.Translate("Deliver 18x Sour Diesel to Hyland Manor")=="Entrega 18 unidades de Sour Diesel en Hyland Manor","Dynamic delivery quantity or strain");
Assert(offline.Translate(translatedMission)==translatedMission,"Translated mission queried again");
Assert(offline.Translate("<color=#FFFF00>Save game</color>")=="<color=#FFFF00>Guardar partida</color>","Color wrapper");
Assert(offline.Translate("7:08 AM Saturday")=="7:08 AM Sábado","Clock pattern");
Assert(offline.ContainsOriginal("7:08 AM Sábado"),"Derived translated output is not recognized");
Assert(offline.Translate("Very unusual unlisted message") == "Very unusual unlisted message","Offline changed an unknown string");
for(int i=0;i<5000;i++) offline.Translate("Unlisted string "+i);
Assert(offline.CachedCount<=1024,"Derived cache has no bound");
Console.WriteLine($"Embedded dictionaries: {offline.EntryCount} entries; labels, formatting, clock, offline behavior and bounded cache passed.");

async Task<OnlineTranslator.Result> Take(OnlineTranslator translator)
{
    var deadline=DateTime.UtcNow.AddSeconds(5);
    while(DateTime.UtcNow<deadline)
    {
        if(translator.TryTake(out var result) && result!=null) return result;
        await Task.Delay(20);
    }
    throw new Exception("Worker did not return a result");
}
string fixture=Path.Combine(Path.GetTempPath(),"ScheduleSpanishOnlineTests-"+Guid.NewGuid().ToString("N"));
Directory.CreateDirectory(fixture);
Assert(DisplaySettings.Load(fixture).PhoneTextScale==2,"Phone density default");
Assert(DisplaySettings.Load(fixture).UseSdfPhoneText,"SDF phone font default");
File.WriteAllText(Path.Combine(fixture,"display.json"),"{\"PhoneTextScale\":1}");
Assert(DisplaySettings.Load(fixture).PhoneTextScale==1,"Phone density cannot be disabled");
Assert(DisplaySettings.Load(fixture).UseSdfPhoneText,"Upgrading old display settings disables new font");
File.WriteAllText(Path.Combine(fixture,"display.json"),"{\"PhoneTextScale\":1,\"UseSdfPhoneText\":false}");
Assert(!DisplaySettings.Load(fixture).UseSdfPhoneText,"Phone font cannot be restored");
int requests=0;
string original="Novel label ZXQdemo <color=#FF0000>{0}</color>";
using(var service=new OnlineTranslator(onlineEngine,fixture,(text,token)=> {
    Interlocked.Increment(ref requests); return Task.FromResult(text.Replace("Novel label","Etiqueta nueva"));
}))
{
    Assert(!service.Request("Guardar partida"),"Already translated UI label was sent online");
    Assert(service.Request(original),"Online request not queued");
    var result=await Take(service);
    Assert(result.Translation=="Etiqueta nueva ZXQdemo <color=#FF0000>{0}</color>","Formatting was lost");
    Assert(onlineEngine.Translate(original)==result.Translation,"Learned result hidden by a cached miss");
    Assert(!service.Request(original),"Saved text queried again");
    Assert(!service.Request(result.Translation!),"Learned Spanish output was sent online");
    Assert(requests==1,"Duplicate request");
}
var restarted=Load("Online");
File.AppendAllText(Path.Combine(fixture,"cache.es.jsonl"),"{bad json}\n{\"Source\":null,\"Translation\":null}\n");
using(var service=new OnlineTranslator(restarted,fixture,(text,token)=>throw new Exception("Restart must use disk cache")))
{
    Assert(restarted.Translate(original).StartsWith("Etiqueta nueva"),"Persistent cache was not reused");
    Assert(!service.Request(original),"Cached text requested after restart");
}
using(var failure=new OnlineTranslator(restarted,fixture,(text,token)=>throw new HttpRequestException("Simulated failure")))
{
    Assert(failure.Request("Network failure sample"),"Failure fixture not queued");
    Assert((await Take(failure)).Translation==null,"Failed result accepted");
    Assert(!failure.Request("Network failure sample"),"No failure cooldown");
    Assert(restarted.Translate("Save game")=="Guardar partida","Network failure damaged local translations");
}
var block=new TaskCompletionSource<string>(TaskCreationOptions.RunContinuationsAsynchronously);
using(var queue=new OnlineTranslator(restarted,fixture,(text,token)=>block.Task.WaitAsync(token)))
{
    for(int i=0;i<1000;i++) Assert(!queue.Request(i+" FPS"),"FPS counter consumed translation queue");
    Assert(!queue.Request("v0.4.7f12") && !queue.Request("$191,7K"),"Version or currency queried");
    int accepted=0;
    for(int i=0;i<100;i++) if(queue.Request("Pending novel message "+i)) accepted++;
    Assert(accepted<=64,"Request queue is unbounded");
    Assert(queue.IsEligible("Visible unknown menu option") && !queue.Request("Visible unknown menu option"),"Queue-full text must remain eligible for retry");
}
Console.WriteLine("Online: protected formatting, single request, disk reuse after restart, malformed-cache tolerance, failure cooldown and queue limit passed.");
var defaults=LanguageSettings.Load(fixture);
Assert(defaults.TargetLanguage=="es","Default language changed");
File.WriteAllText(Path.Combine(fixture,"language.json"),"{\"TargetLanguage\":\"fr\"}");
Assert(LanguageSettings.Load(fixture).TargetLanguage=="fr","Language setting ignored");
bool rejected=false;
try { LanguageSettings.Validate("../../es"); } catch(ArgumentException) { rejected=true; }
Assert(rejected,"Unsafe language code accepted");
var frenchEngine=new TranslationEngine();
frenchEngine.Load(new StringReader("Save game=Guardar partida\nBenji=Benji\n"),identitiesOnly:true);
Assert(frenchEngine.Translate("Save game")=="Save game" && frenchEngine.ContainsOriginal("Benji"),"Spanish dictionary leaked into another language");
using(var french=new OnlineTranslator(frenchEngine,fixture,(text,token)=>Task.FromResult("Sauvegarder la partie"),targetLanguage:"fr"))
{
    Assert(french.Request("Save game"),"French request rejected");
    Assert((await Take(french)).Translation=="Sauvegarder la partie","French translation failed");
}
var frenchRestart=new TranslationEngine();
using(var french=new OnlineTranslator(frenchRestart,fixture,(text,token)=>throw new Exception("Must reuse French cache"),targetLanguage:"fr"))
    Assert(frenchRestart.Translate("Save game")=="Sauvegarder la partie","French cache not reused");
var germanEngine=new TranslationEngine();
using(var german=new OnlineTranslator(germanEngine,fixture,(text,token)=>Task.FromResult("Spiel speichern"),targetLanguage:"de"))
    Assert(germanEngine.Translate("Save game")=="Save game","French cache leaked into German");
Console.WriteLine("Language settings, identity preservation, cache isolation and restart reuse passed.");
using(var glossary=new OnlineTranslator(new TranslationEngine(),fixture,(text,token)=>Task.FromResult(text),targetLanguage:"es"))
{
    Assert(glossary.Request("Buy weed seeds: Green Crack and Sour Diesel from Albert Hoover"),"Glossary request rejected");
    Assert((await Take(glossary)).Translation=="Buy semillas de marihuana: Green Crack and Sour Diesel from Albert Hoover","Online glossary or proper names lost");
}
Console.WriteLine("Mission blocks, dynamic dialogue, contextual vocabulary, names, item case and phone setting passed.");
if(args.Contains("--live"))
{
    var liveEngine=new TranslationEngine();
    using var live=new OnlineTranslator(liveEngine,Path.Combine(fixture,"live"));
    const string sample="Save your progress before leaving";
    Assert(live.Request(sample),"Live request was not queued");
    var response=await Take(live);
    Assert(response.Translation!=null && response.Translation!=sample,"Google did not return a usable translation");
    Console.WriteLine("Live Google request passed: "+response.Translation);
    const string richSample="Cheap Skateboard<color=#54E717> ($75)</color>";
    Assert(live.Request(richSample),"Live rich-text request was not queued");
    var richResponse=await Take(live);
    Assert(richResponse.Translation!=null && richResponse.Translation!=richSample && richResponse.Translation.Contains("<color=#54E717>") && richResponse.Translation.Contains("$75"),"Google rich-text request failed: "+richResponse.Error);
    Console.WriteLine("Live formatted-price request passed: "+richResponse.Translation);
    foreach(var file in Directory.GetFiles(Path.Combine(fixture,"live"))) File.Delete(file);
    Directory.Delete(Path.Combine(fixture,"live"));
}
// Remove only the explicitly created test fixture.
foreach(var file in Directory.GetFiles(fixture)) File.Delete(file);
Directory.Delete(fixture);
