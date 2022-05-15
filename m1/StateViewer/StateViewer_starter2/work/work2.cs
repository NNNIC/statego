using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Reflection;

/*
    2019.8.4

   スタート方法を改良するため、途中で選択で分けるようにした。
   １．これまでどおり、ドキュメントフォルダとソースフォルダを自由に設定
   ２．ソースフォルダとドキュメントフォルダが同じ
   ３．ソースフォルダの中にドキュメントフォルダ(doc)を作成

    本ファイルは、上記のためのもの
     
*/

namespace StateViewer_starter2
{
   public enum SRC_DOC_FOLDER_DFEINE_TYPE
    {
        none,
        all_in_one_folder,
        src_with_doc_folder,
    }

    public partial class WORK
    {
        public static SRC_DOC_FOLDER_DFEINE_TYPE SrcDocFolderDefineType = SRC_DOC_FOLDER_DFEINE_TYPE.none;
    }
}
