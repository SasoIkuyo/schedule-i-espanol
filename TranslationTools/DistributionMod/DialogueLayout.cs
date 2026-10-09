using System.Reflection;
using HarmonyLib;
using Il2CppTMPro;
using UnityEngine;

namespace ScheduleISpanish;

internal static class DialogueLayout
{
    internal static void Install(HarmonyLib.Harmony harmony)
    {
        // The game resizes choice labels after setting text. Apply bounds immediately
        // before TMP renders, after those assignments, without scanning a scene.
        var method=AccessTools.DeclaredMethod(typeof(TextMeshProUGUI),"OnPreRenderCanvas");
        if(method!=null) harmony.Patch(method,prefix:new HarmonyMethod(typeof(DialogueLayout).GetMethod(nameof(Fit),BindingFlags.Static|BindingFlags.NonPublic)));
    }

    internal static bool IsChoice(TMP_Text label) => label.name=="ChoiceText" && label.transform.parent!=null && label.transform.parent.name=="Background" && label.transform.parent.parent!=null && label.transform.parent.parent.name.StartsWith("DialogueChoice",StringComparison.Ordinal);

    private static void Fit(TextMeshProUGUI __instance)
    {
        try
        {
            var parent=__instance.transform.parent;
            var background=IsChoice(__instance) ? parent : parent!=null && parent.name=="NotPossible" ? parent.parent : null;
            if(background==null || background.name!="Background" || background.parent==null || !background.parent.name.StartsWith("DialogueChoice",StringComparison.Ordinal)) return;
            var choiceTransform=background.Find("ChoiceText");
            var reasonTransform=background.Find("NotPossible/Text");
            var inputTransform=background.Find("InputLabel");
            if(choiceTransform==null || reasonTransform==null || inputTransform==null) return;
            var choice=choiceTransform.GetComponent<TextMeshProUGUI>();
            var reason=reasonTransform.GetComponent<TextMeshProUGUI>();
            var input=inputTransform.GetComponent<TextMeshProUGUI>();
            if(choice==null || reason==null || input==null) return;
            bool blocked=reason.transform.parent.gameObject.activeSelf && !string.IsNullOrWhiteSpace(reason.text);
            float split=blocked ? 0.55f : 1;
            float start=input.rectTransform.anchoredPosition.x+input.rectTransform.rect.width*(1-input.rectTransform.pivot.x)+10;
            SetBounds(choice,0,split,Math.Max(40,start),10);
            SetBounds(reason,0.55f,1,8,12);
        }
        catch { /* Preserve the game's rendering if a future version changes the hierarchy. */ }
    }

    private static void SetBounds(TMP_Text text,float left,float right,float insetLeft,float insetRight)
    {
        var rect=text.rectTransform;
        rect.anchorMin=new Vector2(left,0); rect.anchorMax=new Vector2(right,1);
        rect.offsetMin=new Vector2(insetLeft,0); rect.offsetMax=new Vector2(-insetRight,0);
        text.margin=Vector4.zero;
        text.enableWordWrapping=false;
        text.enableAutoSizing=true;
        text.fontSizeMin=12; text.fontSizeMax=16;
        text.overflowMode=TextOverflowModes.Ellipsis;
        text.alignment=TextAlignmentOptions.MidlineLeft;
    }
}
