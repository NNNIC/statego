# StateGo ビルドプロセス

## 概要
StateGo は以下のハイブリッドなビルドプロセスを採用しています：
1.  **コアビルド**: GitHub Actions による自動化（バイナリのコンパイルと基本アセットの収集）。
2.  **完全版リリースの作成**: ローカル環境にて、コアビルドの成果物と巨大な `WinPython` ディストリビューションを統合します。

---

## 1. 自動ビルド (GitHub Actions)
メインアプリケーションのビルドは **GitHub Actions** を使用して自動化されています。

-   **トリガー**: `master`, `main`, または `tougou_conv` ブランチへのプッシュ。
-   **使用スクリプト**: ワークフロー内で **`BuildAuto.bat`** を実行します。
-   **成果物 (Artifact)**: **`StateGo-Release`** という名前の ZIP ファイルが生成されます。
    -   *注意*: ビルド時間とストレージ容量を節約するため、この成果物には `winPython` フォルダは**含まれません**。

### BuildAuto.bat
CI環境で使用される主要なビルドスクリプトです。
-   `StateViewer.sln` をコンパイルします。
-   必要な依存ファイル（starter-kit, mermaid, tools）を収集します。
-   `MSBUILD17` や `NUGET` のパスを環境変数から受け取ることで、CI環境に適応します。

---

## 2. 完全版配布パッケージの作成 (WinPython の追加)
Python サポートを含む完全な配布パッケージを作成するには、CI の成果物とローカルの `winPython` ディレクトリを統合する必要があります。

### ツール: `CreateFullArchive.bat`
この統合プロセスを自動化するためのヘルパースクリプトが用意されています。

#### 使用方法
1.  GitHub Actions の実行結果画面（Summary）から、成果物 **`StateGo-Release.zip`** をダウンロードします。
2.  ダウンロードした zip ファイルのパスを引数として `CreateFullArchive.bat` を実行します（ドラッグ＆ドロップでも可）。

```cmd
CreateFullArchive.bat "C:\Path\To\Downloads\StateGo-Release.zip"
```

#### 出力
-   カレントディレクトリに **`StateGo_Full_with_Python.zip`** が生成されます。
-   この zip ファイルには、成果物の中身に加え、ローカルの `winPython` フォルダが含まれています。
