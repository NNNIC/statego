# AI機能統合計画（ハイブリッド：内部APIサーバー案）

## 目標
StateGo (StateViewer) に**ローカルAPIサーバー**を実装し、外部ツール（現在のAIエージェントなど）と内部機能の両方からステートマシンを安全に操作可能にする。

## ユーザー確認事項
> [!TIP]
> **ハイブリッド案を採用します**。
> StateGo自体に簡易HTTPサーバーを内蔵させることで、以下の両方を実現します。
> 1.  **外部からの操作**: 私（Antigravity）が `curl` 等でコマンドを送って操作可能。
> 2.  **内部からの操作**: 将来的に内部チャットUIを作る場合も同じAPIを利用可能。

## 変更内容

### 1. ローカルAPIサーバーの実装
- **依存関係**: `System.Net.HttpListener` (標準ライブラリ) を使用。
- **`LocalServer.cs` の作成**:
    - 指定ポート（例: 5000）でリクエストを待機。
    - JSON形式のコマンドを受け付ける。
    - **スレッドセーフ制御**: 受け取ったコマンドをメインスレッド（UIスレッド）のキューに入れ、安全に実行する。

### 2. ステート操作のAPI化 (API Endpoints)
内部メソッド (`G.excel_program`, `ViewFormStateControl` 等) にマップする以下のエンドポイントを実装します。

#### A. ステート操作 (State Operations)
- **`GET /api/state/list`**
  - **Internal**: `G.state_location_list`, `G.excel_program.GetStateList()`
  - **Response**: JSON array `{ name, type, comment, pos: {x, y}, next, branch, gosub }`

- **`POST /api/state/create`**
  - **Param**: `name`, `type` (start/end/state/...), `x`, `y`, `comment`
  - **Internal**: 
    1. `G.excel_program.NewState`
    2. `G.excel_program.SetString` (type, comment)
    3. `G.UpdateExcelPos`

- **`POST /api/state/delete`**
  - **Param**: `name`
  - **Internal**: `G.excel_program.Delete`

- **`POST /api/state/move`**
  - **Param**: `name`, `x`, `y`
  - **Internal**: `G.UpdateExcelPos`

- **`POST /api/state/edit`**
  - **Param**: `name`, `params` (dictionary of item:value), `x` (optional), `y` (optional)
  - **Logic**: 
    1. If `name` not found, create new state.
    2. Update values for keys in `params` using `G.excel_program.SetString`.
    3. If `x, y` provided, update position.


#### B. グループ・システム (Grouping & System)
- [POST] `/api/group/create`
  - **Param**: `group_name`, `states` (array), `comment`
  - **Internal**: `G.node_grouping`

- **`POST /api/system/save_and_convert`**
  - **Action**: Runs "Save and Convert" logic (same as button). Use `G.option_convert_with_confirm=false` temporarily to avoid UI blocking.
  - **Internal**: `G.view_form.SaveAndRun(false)`

- **`GET /api/system/info`**
  - **Response**: `{ canvas_width, canvas_height, current_dir }`
  - **Internal**: `G.bitmap_width/height`, `G.node_get_cur_dirpath`

### 3. StateBridgeの拡張
- 以前の案の `StateBridge` を、APIコントローラーとして機能するように調整。

### 4. 起動プロセスの自動化 (Automated Startup)
- **現状の課題**: `StateGo.exe` 起動時にGUIダイアログ（新規作成・ファイル選択）が表示され、自動化を阻害している。また、作成ロジック (`WORK` クラス) がUIコントロール (`Start2Form`, `CreateNewForm`) と密結合している。
- **改修方針**: CLI引数によるヘッドレス（UIなし）起動モードを実装する。
    1.  **`WORK` クラスの拡張**: `Start2Form` や `CreateNewForm` に依存せずにプロジェクト作成に必要な設定（`XLSDIR`, `GENDIR`, `SELECT_SETTING` 等）を行う `SetupHeadless` メソッドを追加。
    2.  **`CreateFileWork` の改修**: コンストラクタや `Save` メソッド内でのUIアクセスを回避し、設定値に基づいてファイル生成を行うヘッドレス対応を追加。
    3.  **CLI引数対応**: `Form1.cs` で `/new` 等の引数を解析し、上記ヘッドレスメソッドを呼び出してプロジェクトを作成後、自動的にロードする。
    - 引数仕様案:
        - `/new`: 新規作成モード
        - `/kit "name"`: スターターキット名（例: "c-sharp-unity"）
        - `/dir "path"`:出力ディレクトリ
        - `/name "ProjectName"`: ステートマシン名

## 検証計画

### 1. サーバー起動確認
- StateGo起動時にサーバーが立ち上がることを確認（ログ出力）。

### 2. 外部からの操作テスト
- ターミナルから `curl` コマンドを叩いて、StateGo上にステートが追加されるかテスト。
  ```bash
  curl -X POST -d '{"name":"NewState"}' http://localhost:5000/api/state/add
  ```
- 私（AI）が実際にコマンドを発行して操作できるか確認。
