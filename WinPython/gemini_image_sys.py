import os
import google.generativeai as genai
from PIL import Image
import io
import argparse

genai.configure(api_key=os.environ["GEMINI_API_KEY"])

try:
    # Create the parser
    parser = argparse.ArgumentParser(description="Generate an image using Gemini API.")

    # Add arguments
    parser.add_argument("--output", "-o", type=str, default="generated_image.png",
                        help="Output filename for the generated image.")
    parser.add_argument("--prompt", "-p", type=str, default="A beautiful cat, photorealistic.",
                        help="Prompt for image generation.")

    # Parse arguments
    args = parser.parse_args()

    ## あなたはプロのデザイナーです。ソースコードの関数ごとにユニークなアイコンをデザインしてください。提供されたソースコードから素敵なアイコンイメージをデザインしてください。動物、美少女やモンスターを使って、ソースコードから状況を例えるとユーザーは嬉しいです。アイコンは縮小して使われることを考慮してください。
    
    ##  ソースコードの状況を理解した後、かわいい猫のキャラクターを使用して状況にあったアイコンをデザインしてください。
    model = genai.GenerativeModel(
        model_name = 'gemini-2.5-flash-image-preview',
        system_instruction='After understanding the source code situation, please design an icon that fits the situation using a cute cat character.'

    )
    prompt = args.prompt # Use the prompt from arguments
    output_filename = args.output # Use the output filename from arguments

    print(f"Generating image with prompt: '{prompt}'")

    response = model.generate_content(prompt, stream=True)

    print("Response received (streaming). Processing chunks...")
    image_bytes = b""
    for chunk in response:
        # --- Debugging ---
        # print(f"--- Received Chunk ---")
        # print(chunk)
        # print(f"--- End Chunk ---")
        # --- End Debugging ---
        if chunk.parts:
            if hasattr(chunk.parts[0], 'inline_data'):
                image_bytes += chunk.parts[0].inline_data.data

    if image_bytes:
        print("Image data concatenated. Attempting to save...")
        try:
            image = Image.open(io.BytesIO(image_bytes))
            image.save(output_filename) # Use the output filename
            print(f"Image saved as {output_filename}")
        except Exception as e_inner:
            print(f"Error saving image: {e_inner}")
    else:
        print("No image data found in any of the response chunks.")

except Exception as e:
    print(f"An error occurred: {e}")