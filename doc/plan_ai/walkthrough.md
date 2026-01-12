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

**修正事項:**
- **設定保存**: ヘッドレス作成時に設定が正しく保存されるように `RegistryWork` を更新しました。
- **xlsxパス**: `.xlsx` パスが未指定の場合の自動補完を追加しました。
- **[New] PathUtil クラッシュ修正**: ヘッドレスモード時に `PathUtil.GetThisAppPath` がコマンドライン引数の解析エラーでクラッシュする問題を `AppDomain.CurrentDomain.BaseDirectory` を使用するように変更して修正しました。

### 2. ローカルAPIサーバー
StateGoの起動時に、ポート **5000～5009** の範囲で空いているポートを探し、HTTPサーバーを自動的に起動します。

**API エンドポイント:**

| Method | Endpoint | Description |
| :--- | :--- | :--- |
| GET | `/api/system/noop` | サーバー稼働確認 (Ping)。 |
| GET | `/api/system/info` | キャンバスサイズとカレントディレクトリを取得。 |
| GET | `/api/state/list` | 全ステートの情報(座標、接続先含む)を取得。 |
| POST | `/api/state/create` | ステート作成。 |
| POST | `/api/state/delete` | ステート削除。 |
| POST | `/api/state/move` | ステート移動。 |
| POST | `/api/group/create` | グループ作成。 |
| POST | `/api/state/edit` | ステートの新規作成・更新 (Upsert)。 |
| POST | `/api/system/save_and_convert` | 保存と変換を実行 (UIレス)。 |
| POST | `/api/system/reset` | **(New)** ステートマシンを初期化 (S_START, S_ENDのみにする)。 |

### 3. デバッグ環境の同期
**[New] BuildAuto.bat**: Debugビルド時にもReleaseと同様のアセット (`starterkit2`, `tools`, `ini` 等) が `bin\Debug` にコピーされるようにスクリプトを更新しました。引数で `Debug` を指定可能です。

> [!IMPORTANT]
> **クリーンビルドの必要性**: Debugビルドにおいて、過去のビルド生成物が残り、一見すると古いバージョンに先祖返りしたような挙動を示す場合があることが確認されました。
> このような場合は、`bin\Debug` フォルダを手動で削除するか、クリーンビルドを実行することで解消します。

## 検証結果

### Reset API の検証
`run_verification.ps1` を使用して、ヘッドレス起動から API によるリセット、そして保存までのフローを検証しました。

1. **予備動作**: `bin\Debug` をクリアし、`BuildAuto.bat Debug` で再構築 (クリーンビルド)。
2. **起動**: 指定されたテンプレートでヘッドレス起動 (成功 - ダイアログなし)。
3. **リセット**: `POST /api/system/reset` を実行。ステート数が 2 (S_START, S_END) になることを確認 (成功)。
4. **保存**: `POST /api/system/save_and_convert` を実行。**5秒間の待機**を経て、正常に保存・変換が行われることを確認 (成功)。

すべてのテスト項目をパスしました。
