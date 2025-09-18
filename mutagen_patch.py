from esp_tool import ESM

input_file = "input.esp"
output_file = "output.esp"

# Load the ESP/ESM file
esm = ESM(input_file)

# Example: Print plugin info
print(f"Loaded plugin: {esm.header.name}, {len(esm.records)} records")

# --- PATCHING LOGIC START ---
# You can modify esm.records here if you want to patch the plugin.
# For now, this script just copies the file as a round-trip test.
# --- PATCHING LOGIC END ---

# Save as output.esp
esm.save(output_file)
print(f"Patched ESP written to {output_file}")
