# StateGo AI Integration - 引継書 (Handover Document)

## プロジェクト概要
StateGoに外部ツール（AIエージェント等）からの制御を可能にするための「ヘッドレスモード」と「ローカルAPIサーバー」を実装しました。
現在、`plan_ai` ブランチにて実装・検証が完了しています。

## 実装機能一覧

### 1. ヘッドレス起動モード
UIを表示せずにプロジェクトを作成・操作するモードです。
- **コマンド**: `StateGo.exe /new /src "..." /name "..." /dir "..."`
- **修正点**: 起動ダイアログのスキップ、設定保存(`RegistryWork`)の修正、`PathUtil`のクラッシュ修正。

### 2. ローカルAPIサーバー
StateGo起動時に `http://localhost:5000` (ポート競合時はインクリメント) でサーバーが立ち上がります。
- **主要エンドポイント**:
  - `GET /api/system/noop`: 生存確認
  - `POST /api/state/create`: ステート作成
  - `POST /api/state/edit`: ステートの作成・更新 (Upsert)
  - `POST /api/system/reset`: 全ステート削除・初期化 (S_START, S_ENDのみ)
  - `POST /api/system/save_and_convert`: 保存とコード生成
  - `POST /api/system/batch`: バッチ処理（複数コマンドの一括実行）

### 3. バッチ処理API (`system/batch`)
パフォーマンス改善のため、複数の編集コマンドを1リクエストで送信し、UI再描画と履歴保存を最後に一度だけ行うAPIを実装しました。
- **機能**: `state/create`, `state/edit`, `state/move`, `state/delete`, `group/create`, `system/reset`, `system/save_and_convert` を配列で受け取り順次実行。
- **制約**: 新規作成ステート名は `S_` で始まる必要があります。

### 4. デバッグ環境
- `BuildAuto.bat Debug` でDebugビルド環境を一括構築可能にしました。
- 検証用スクリプト `TestBed/verify_helloworld.ps1` を整備しました（ヘッドレス作成～バッチ編集～コンパイル実行のE2Eテスト）。

## 開発・検証環境の注意点

> [!WARNING]
> **Debugビルドのクリーンアップについて**
> `bin\Debug` フォルダに古いビルド生成物が残っていると、最新のコード修正が反映されない（先祖返り現象）が発生することがあります。
> 挙動がおかしい場合は、必ず `BuildAuto.bat Debug` を実行する前に `bin\Debug` を**手動で削除**するか、スクリプト内で削除を行ってください。

## 検証手順

リポジトリルート (`documents/psgg-editor-public/editor`) にて以下のPowerShellスクリプトを実行することで、Hello World生成フローの検証が可能です。

```powershell
cd TestBed
.\verify_helloworld.ps1
```

このスクリプトは以下の処理を行います：
1. `StateGo.exe` をヘッドレスモードで起動し、`test1Control` プロジェクトを作成。
2. API経由で `system/batch` を送信し、ステートのリセット・作成・接続・保存を実行。
3. 生成されたC#コードをコンパイルし、実行結果が "Hello World" であることを確認。

## ファイル構成
- `m1/StateViewer/StateViewer/Form1.cs`: 起動引数解析、サーバー起動トリガー。
- `m1/stateview/stateview/5900_AIIntegration/LocalServer.cs`: HTTPサーバー実装。
- `m1/stateview/stateview/5900_AIIntegration/StateBridge.cs`: APIロジック、バッチ処理実装。
- `doc/plan_ai/walkthrough.md`: 詳細な機能説明とログ。
- `TestBed/verify_helloworld.ps1`: E2E検証スクリプト。

## 残課題・今後の展望
- 現状、APIはローカル実行のみを想定しています。認証機能はありません。
- エラーハンドリングの強化（不正なJSON入力時の詳細なレスポンス等）。

---
作成日: 2026-01-13
作成者: Antigravity AI Agent
