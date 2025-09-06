import os
import google.generativeai as genai

def run_gemini_api(prompt_text):
    try:
        # 環境変数からAPIキーを読み込む
        api_key = os.getenv("GEMINI_API_KEY")
        if not api_key:
            print("Error: GOOGLE_API_KEY environment variable not set.")
            return "Error: API key not set."

        genai.configure(api_key=api_key)

        # モデルの初期化
        model = genai.GenerativeModel('gemini-2.0-flash')

        # テキスト生成
        response = model.generate_content(prompt_text)

        # 結果の表示
        if response.candidates:
            return response.text
        else:
            return "No response from Gemini API."

    except Exception as e:
        return f"An error occurred: {e}"

if __name__ == "__main__":
    import sys
    if len(sys.argv) > 1:
        prompt = sys.argv[1]
        result = run_gemini_api(prompt)
        print(result)
    else:
        print('Usage: python gemini_api_script.py "Your prompt here"')
        print("Please set the GOOGLE_API_KEY environment variable.")
