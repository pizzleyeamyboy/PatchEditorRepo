using System;
using System.Linq;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Plugins;

internal class Program
{
    private const string inputPath = "MyPatch.esp";
    private const string outputPath = "MyPatch_Updated.esp";
    private const string targetEditorId = "LItemWeaponAll";
    private static readonly FormKey steelSwordFormKey = FormKey.Factory("000139B5:Skyrim.esm");

    private static int Main(string[] args)
    {
        try
        {
            Console.WriteLine($"Opening {inputPath} ...");
            var mod = SkyrimMod.CreateFromBinaryOverlay(inputPath, SkyrimRelease.SkyrimSE);
            if (mod == null)
            {
                Console.Error.WriteLine("Failed to open the plugin file.");
                return 2;
            }
            Console.WriteLine("Enumerating leveled lists in the plugin...");
            var leveledLists = mod.LeveledItems.ToList();
            if (!leveledLists.Any())
            {
                Console.WriteLine("No leveled lists found in the provided plugin.");
            }
            else
            {
                Console.WriteLine($"Found {leveledLists.Count} leveled lists. Sample:");
                foreach (var l in leveledLists.Take(30))
                {
                    Console.WriteLine($" - EditorID: {l.EditorID ?? "<no EDID>"}  FormKey: {l.FormKey}");
                }
            }
            var target = leveledLists.FirstOrDefault(l => string.Equals(l.EditorID, targetEditorId, StringComparison.OrdinalIgnoreCase));
            if (target == null)
            {
                Console.Error.WriteLine($"Could not find a leveled list with EditorID '{targetEditorId}'. Exiting (no changes).");
                mod.WriteToBinary(outputPath);
                Console.WriteLine($"Wrote (unchanged) {outputPath}");
                return 0;
            }
            Console.WriteLine($"Editing leveled list {target.EditorID} ({target.FormKey}) ...");
            var newEntry = new LeveledItemEntry()
            {
                Data = new LeveledItemEntryData()
                {
                    Reference = new FormLink<ISkyrimMajorRecordGetter>(steelSwordFormKey),
                    Level = 1,
                    Count = 1
                }
            };
            target.Entries.Add(newEntry);
            Console.WriteLine($"Saving patched plugin to {outputPath} ...");
            mod.WriteToBinary(outputPath);
            Console.WriteLine("Done.");
            return 0;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Exception during patching: " + ex);
            return 3;
        }
    }
}
