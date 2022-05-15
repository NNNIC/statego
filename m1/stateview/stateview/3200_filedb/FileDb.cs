using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateViewer_filedb
{
    public partial class FileDb
    {
        /*
        FileDb.cs       -- 本ファイル　説明のみ
        FileDb_excel.cs -- エクセルロード・セーブ   ロード時はstatechart.csのInitExcelが呼び出されて詳細をセットアップ
        　　　　　　　　　 エクセルの扱いは、１．１以降はオプションとなる。

        FileDb_psgg_create.cs -- PSGGファイルを生成する。 最適化のため、FileDbで展開されたファイルをAppendして作成。かなり高速
        FileDb_psgg_read.cs   -- PSGGファイルの読込み。データ付きになったPSGGをここで読込み、FileDbを作成する。

        FileDb_statechart.cs  -- 管理クラス state_chartを持つ。

        FileDb_write_internal_files.cs -- 内部ファイルの更新用

        FileDbUtil.cs -- 外部からのアクセス用API


    ※　以下は、作成メモ


        ■ エクセルロード・セーブ
          1. LoadExcel(エクセルファイル)  -- 現フォーマットを固定とする。 
          2. SaveExcel(エクセルファイル)  -- 既存も、新規も
　　　　  
        ■ エキスポート・インポート　---
          1. ExportPsgg(psggファイル)      
          2. InportPsgg(psggファイル)               
        
        ■ 読み書き
          １．ReadState(ステート) -> Dic   ステートの項目とデータを辞書で得る
         　　 ReadState(行番号) -> Dic
            　Read(行番号、列番号) -> string

        　２．WriteState(ステート,Dic)     項目とデータの辞書を指定ステートのデータとして書き込む
              WriteState(行番号,Dic)
              Write(行番号、列番号、string) 

          ３．GetColState(ステート)　　列番号取得
              GetRowItem(アイテム)     行番号取得
              
          ４．SetItem(Row,アイテム)    列番号の部分にアイテムを設定。既にあれば上書き
                                       ※アイテム名はID管理されているので、データとの関連はOK
          
        　５．ReadFileFromDb(ファイル名)->string

        ■ 削除挿入

          1．RemoveItem(アイテム)     アイテムが消え、行が消える。
             RemoveRow(行番号)       

          2．RemoveState(ステート名)  ステートが消え、行が消える
             RemoveCol(列番号)

          3．InsertCol(列番号)　　　　列挿入

          4．InsertRow(行番号)        行挿入 

        */
    }
}
