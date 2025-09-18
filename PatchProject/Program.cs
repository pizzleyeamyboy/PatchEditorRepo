using System;
using System.Linq; // Add this line
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Plugins;

class Program
{
    static void Main(string[] args)
    {
        var inputPath = "../input.esp";
        var outputPath = "../output.esp";

        // Load the mod
        var mod = SkyrimMod.CreateFromBinaryOverlay(inputPath, SkyrimRelease.SkyrimSE);

        Console.WriteLine($"Loaded {mod.ModKey}: {mod.EnumerateMajorRecords().Count()} top-level records"); // Fixed line

        // --- PATCHING LOGIC START ---
        // Example: enumerate leveled lists
        foreach (var ll in mod.LeveledItems)
        {
            Console.WriteLine($"LeveledList EditorID: {ll.EditorID}, FormKey: {ll.FormKey}");
            // Example modification (optional)
            // ll.Entries.Add(new LeveledItemEntry {
            //     Data = new LeveledItemEntryData {
            //         Reference = new FormLink<ISkyrimMajorRecordGetter>(FormKey.Factory("000139B5:Skyrim.esm")),
            //         Level = 1,
            //         Count = 1
            //     }
            // });
        }
        // --- PATCHING LOGIC END ---

        mod.WriteToBinary(outputPath);
        Console.WriteLine($"Patched ESP written to {outputPath}");
    }
}
