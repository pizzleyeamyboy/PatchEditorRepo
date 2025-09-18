from pathlib import Path
from mutagen.bethesda import ESM

input_path = Path("input.esp")
output_path = Path("output.esp")

if not input_path.exists():
    print("ERROR: input.esp not found!")
    exit(1)

# Read the input ESP
with input_path.open("rb") as f:
    data = f.read()

# Load with Mutagen (this checks if the file is valid)
esp = ESM(data)

# Write out the exact same file (round-trip test)
with output_path.open("wb") as f:
    f.write(esp.to_bytes())

print("Successfully read and wrote ESP file.")
