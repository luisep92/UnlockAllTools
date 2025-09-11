using BepInEx;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static Strings;

class Configuration
{
    private readonly string configToolFile = Path.Combine(Paths.ConfigPath, "UnlockTool.cfg");
    private readonly string configCrestFile = Path.Combine(Paths.ConfigPath, "UnlockCrest.cfg");
    public List<string> toolsToUnlock = new();
    public List<string> crestsToUnlock = new();

    public static Configuration Read()
    {
        var ret = new Configuration();
        if (!File.Exists(ret.configToolFile))
        {
            Debug.LogError($"Error reading config: Tool Config File not found. Creating default config...");
            File.WriteAllText(ret.configToolFile, Strings.toolFile);
        }
        if (!File.Exists(ret.configCrestFile))
        {
            Debug.LogError($"Error reading config: Crest Config File not found. Creating default config...");
            File.WriteAllText(ret.configCrestFile, Strings.crestFile);
        }

        ret.toolsToUnlock = ReadFile(ret.configToolFile);
        ret.crestsToUnlock = ReadFile(ret.configCrestFile);

        return ret;
    }

    private static List<string> ReadFile(string path)
    {
        var ret = new List<string>();
        try
        {
            foreach (var line in File.ReadAllLines(path))
            {
                var trimmed = line.Trim();
                if (string.IsNullOrEmpty(trimmed))
                    continue;
                if (trimmed.StartsWith("+"))
                    ret.Add(trimmed.Substring(1));
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"[QuickWarp Config] Error reading config: {ex.Message}");
        }

        return ret;
    }
}
