using System;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Standalone;

class Program
{
    static void Main(string[] args)
    {
        var inputPath = "../input.esp";
        var outputPath = "../output.esp";

        var mod = SkyrimMod.CreateFromBinary(inputPath, SkyrimRelease.SkyrimSE);

        Console.WriteLine($"Loaded {mod.ModKey}: {mod.EnumerateMajorRecords().Count} top-level records");

        // --- PATCHING LOGIC START ---
        // You could modify records here.
        // --- PATCHING LOGIC END ---

        mod.WriteToBinary(outputPath);
        Console.WriteLine($"Patched ESP written to {outputPath}");
    }
}