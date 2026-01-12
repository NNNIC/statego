# 自動化とAPI統合のウォークスルー

## 概要
このドキュメントでは、StateGoの **ヘッドレス起動モード** と **ローカルAPIサーバー** の実装について説明します。これらの機能により、外部ツール（現在のAIエージェントなど）がプログラムからプロジェクトを作成したり、ステートマシンを操作したりすることが可能になります。

## 新機能

### 1. ヘッドレス起動 (`/new`)
StateGoは、起動ダイアログをスキップし、コマンドライン引数を介して直接新しいプロジェクトを作成できるようになりました。

**コマンド:**
```batch
StateGo.exe /new /src "{テンプレートパス}" /name "{プロジェクト名}" /dir "{出力ディレクトリ}"
```

- **/new**: 自動作成モードをトリガーします。
- **/src**: `.psgg` テンプレートファイルへのパス。
- **/name**: プロジェクト名（およびメインクラス名）。
- **/dir**: 生成先のディレクトリ。

**検証:**
ユーザー操作なしで、`HeadlessTest` ディレクトリに `HeadlessTestControl` というプロジェクトを生成することに成功しました。

### 2. ローカルAPIサーバー
StateGoの起動時に、ポート **5000～5009** の範囲で空いているポートを探し、HTTPサーバーを自動的に起動します。これにより、StateGoの複数同時起動が可能になります。

**アーキテクチャ:**
- **LocalServer.cs**: `HttpListener` を使用してHTTPリクエストを処理します。
- **StateBridge.cs**: コントローラーとして機能し、コマンドを処理してStateGo内部と対話します。

**API エンドポイント:**

| Method | Endpoint | Description |
| :--- | :--- | :--- |
| GET | `/api/system/noop` | **(New)** サーバー稼働確認 (Ping)。`{"success":"ok"}`を返します。 |
| GET | `/api/system/info` | キャンバスサイズとカレントディレクトリを取得 |
| GET | `/api/state/list` | 全ステートの情報(座標、接続先含む)を取得 |
| POST | `/api/state/create` | ステート作成。JSON: `{name, type, x, y, comment}` |
| POST | `/api/state/delete` | ステート削除。JSON: `{name}` |
| POST | `/api/state/move` | ステート移動。JSON: `{name, x, y}` |
| POST | `/api/group/create` | グループ作成。JSON: `{group_name, states:[], comment}` |
| POST | `/api/system/save_and_convert` | **(New)** 保存と変換を実行。UIダイアログは抑制されます。 |
| POST | `/api/state/edit` | **(New)** ステートの新規作成・更新。JSON: `{name, params:{}, x?, y?}`。 !で始まるアイテム名は変更禁止。 |

**検証:**
以下のコマンドで一連の動作を確認済みです。(PowerShell example)

```powershell
# 1. サーバー生存確認 (NOP)
Invoke-RestMethod -Uri "http://localhost:5000/api/system/noop"
#Response: @{success=ok}

# 2. ステート作成
Invoke-RestMethod -Uri "http://localhost:5000/api/state/create" -Method Post -Body '{"name":"S_NEW","x":100,"y":100}' -ContentType "application/json"

# 3. 保存と変換
Invoke-RestMethod -Uri "http://localhost:5000/api/system/save_and_convert" -Method Post
#Response: @{success=save and convert started}

# 4. ステートの統合編集 (Upsert)
$body = @{ name="S_EDIT"; params=@{ "state-typ"="loop"; "nextstate"="S_END" }; x=200; y=200 } | ConvertTo-Json
Invoke-RestMethod -Uri "http://localhost:5000/api/state/edit" -Method Post -Body $body -ContentType "application/json"
# 既存ステートの更新（座標のみ変更など）
Invoke-RestMethod -Uri "http://localhost:5000/api/state/edit" -Method Post -Body '{"name":"S_EDIT","x":300}' -ContentType "application/json"

# 5. ステート削除
Invoke-RestMethod -Uri "http://localhost:5000/api/state/delete" -Method Post -Body '{"name":"S_EDIT"}' -ContentType "application/json"
```

## 変更されたファイル
- `StateViewer/Form1.cs`: CLI引数の解析とヘッドレス初期化呼び出しを追加。
- `StateViewer_starter2/NEW2019/NewControl.cs`: `HeadlessRun` ロジックを実装。
- `StateViewer/5900_AIIntegration/LocalServer.cs`: 新規HTTPサーバー実装。
- `StateViewer/5900_AIIntegration/StateBridge.cs`: 新規APIコントローラー (全機能実装済み)。
- `stateview.csproj`: ビルドに新しいファイルを追加。

## 完了したステップ
- `StateBridge.cs` に実際のステート操作ロジックを実装しました。
- 基本的なステート操作(作成、削除、移動)とリスト取得のAPIを提供しました。
