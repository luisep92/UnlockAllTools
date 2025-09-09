using System.Collections;
using BepInEx;
using UnityEngine;
using UnityEngine.PlayerLoop;

[BepInPlugin("com.luisep92.silksong.unlockalltools", "UnlockAllTools", "1.0.0")]
public sealed class UnlockAllTools : BaseUnityPlugin
{
    private bool unlocked = false;

    private void Update()
    {
        if (unlocked)
            return;

        if (GameManager._instance.GameState == GlobalEnums.GameState.PLAYING)
        {
            UnlockTools();
            UnlockCrests();
            unlocked = true;
        }
    }

    private void UnlockTools()
    {
        var lists = Resources.FindObjectsOfTypeAll<ToolItemList>();
        foreach (var list in lists)
        {
            Logger.LogInfo("Unlocking tools");
            list.UnlockAll();
            foreach (var t in list)
            {
                Logger.LogInfo(t.name);
            }
        }
    }

    private void UnlockCrests()
    {
        var lists = Resources.FindObjectsOfTypeAll<ToolCrestList>();
        foreach (var list in lists)
        {
            Logger.LogInfo("Unlocking crests");
            list.UnlockAll();
            foreach (var t in list)
            {
                Logger.LogInfo(t.name);
            }
        }
    }
}
