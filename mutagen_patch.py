import os

# List all ESP files in the current directory

def list_esp_files():
    esp_files = [f for f in os.listdir('.') if f.endswith('.esp')]
    return esp_files

if __name__ == '__main__':
    print('ESP Files:', list_esp_files())