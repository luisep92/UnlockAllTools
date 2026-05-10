# UnlockAllTools — Hollow Knight: Silksong

A BepInEx mod that lets you unlock specific tools and crests in **Hollow Knight: Silksong** via a config-file allow-list. Pick exactly what you want unlocked; everything else stays untouched.

[**Download on Nexus Mods →**](https://www.nexusmods.com/hollowknightsilksong/mods/42)
*10,375 unique downloads · 12,334 total downloads · 166 endorsements · 38,213 views · current version 1.1.0*

---

## What it does

When the game enters a playing state, the mod reads two config files (`UnlockTool.cfg` and `UnlockCrest.cfg`) and unlocks every tool or crest you've marked as "on". It does not unlock skills, and it does not give you anything you haven't explicitly asked for.

I originally built it because an NPC that was supposed to give me a tool died before triggering the dialog — the mod is a way out of that kind of dead end without starting over.

## Install

1. Install [BepInEx](https://github.com/BepInEx/BepInEx/releases).
2. Download the [latest release from Nexus Mods](https://www.nexusmods.com/hollowknightsilksong/mods/42).
3. Unzip into the game's root folder. The `.dll` should land at:

   ```text
   <GameFolder>/BepInEx/plugins/UnlockAllTools/UnlockAllTools.dll
   ```

## Configuration

The first time you run the game with the mod installed, two config files are created in `<GameFolder>/BepInEx/config/`:

- `UnlockTool.cfg`
- `UnlockCrest.cfg`

Each file lists every tool or crest in the game prefixed with `-` (off):

```text
-Silk Spear
-Thread Sphere
-Parry
...
```

To unlock something, **change the `-` to `+`** at the start of the line:

```text
+Silk Spear
-Thread Sphere
+Parry
```

Save the file, load your game, and the entries marked with `+` will be unlocked.

## Usage

There are no in-game keys or controls. The unlock happens automatically when the game state becomes `PLAYING`, so you load your save normally.

If you want to undo or change the unlocks, edit the config files again and reload the game. Already-unlocked items stay unlocked in your save.

## Caveats

- **Items you "shouldn't" have yet.** You can end up with a tool *and* its upgrade at the same time, or a crest before the related quest is done. Game logic generally tolerates this, but a few scripted moments may behave oddly. Not destructive in my experience, just occasionally weird.
- **Once unlocked, it sticks.** Unlocked items live in the save file. If you want a clean run later, start a new game — clearing the config file alone won't relock items already in your save.
- **Tool/crest names.** The default config files ship with the full list as of game version 1.1.0. If Silksong adds new tools/crests in a patch, the config won't know about them until the mod is updated.

---

## Stack

C# · [BepInEx 6](https://github.com/BepInEx/BepInEx) · Unity (Hollow Knight: Silksong)

## How it's built

Intentionally simple. Single `BaseUnityPlugin` that polls `GameManager._instance.GameState` from `Update()` and runs the unlock pass exactly once when the state turns `PLAYING`:

- Tools and crests are enumerated via Unity's `Resources.FindObjectsOfTypeAll<ToolItemList>` / `<ToolCrestList>` — gives every list asset loaded in memory, including ones not yet placed in the player's inventory.
- For each entry whose `name` matches a `+`-prefixed line in the config, call its `Unlock()` method directly.
- An `unlocked` flag keeps the `Update()` loop a no-op for the rest of the session, so there's no per-frame cost.

The default config strings (every tool and crest in the game) are hardcoded in `Strings.cs`. When the game ships new content, the canonical fix is to extend that list — there's no auto-discovery of new types.

## License

[MIT](LICENSE). Not affiliated with Team Cherry.
