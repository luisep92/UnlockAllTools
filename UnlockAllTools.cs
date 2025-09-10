using System;
using System.Collections;
using System.Collections.Generic;
using BepInEx;
using UnityEngine;
using UnityEngine.PlayerLoop;

[BepInPlugin("com.luisep92.silksong.unlockalltools", "UnlockAllTools", "1.0.0")]
public sealed class UnlockAllTools : BaseUnityPlugin
{
    private bool unlocked = false;
    Configuration config;

    void Awake()
    {
        config = Configuration.Read();
    }

    private void Update()
    {
        if (unlocked)
            return;

        if (GameManager._instance.GameState == GlobalEnums.GameState.PLAYING)
        {
            config = Configuration.Read();
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
            foreach (var tool in list)
            {
                if (config.toolsToUnlock.Contains(tool.name))
                {
                    Logger.LogInfo($"Unlocking {tool.name}");
                    tool.Unlock();
                }
            }
        }
    }

    private void UnlockCrests()
    {
        var lists = Resources.FindObjectsOfTypeAll<ToolCrestList>();
        foreach (var list in lists)
        {
            Logger.LogInfo("Unlocking crests");
            foreach (var crest in list)
            {
                if (config.crestsToUnlock.Contains(crest.name))
                {
                    Logger.LogInfo($"Unlocking {crest.name}");
                    crest.Unlock();
                }
            }
        }
    }
}
