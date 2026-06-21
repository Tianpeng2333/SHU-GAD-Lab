using UnityEditor;
using UnityEngine;

public static class WwiseEventBankTools
{
    [MenuItem("Tools/Wwise/Clear User-Defined SoundBank On All Events")]
    public static void ClearUserDefinedSoundBankOnAllEvents()
    {
        var guids = AssetDatabase.FindAssets("t:WwiseEventReference");
        var changedCount = 0;

        foreach (var guid in guids)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var eventReference = AssetDatabase.LoadAssetAtPath<WwiseEventReference>(path);
            if (eventReference == null || !eventReference.IsInUserDefinedSoundBank)
            {
                continue;
            }

            eventReference.IsInUserDefinedSoundBank = false;
            EditorUtility.SetDirty(eventReference);
            changedCount++;
        }

        if (changedCount > 0)
        {
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        Debug.Log($"Cleared IsInUserDefinedSoundBank on {changedCount} Wwise event reference(s).");
    }
}
