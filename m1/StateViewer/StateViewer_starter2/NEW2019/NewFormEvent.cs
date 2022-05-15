using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateViewer_starter2.NEW2019
{
    enum NewFormEvent
    {
        none,
        onload,
        fromclosing,
        
        button_starterkit_dir_open,
        button_starterkit_dir_reset,

        button_gendir_open,
        button_docdir_open,

        button_goold,
        button_create,

        treeview_selected,
        checkbox_usedoc_changed, //ソース直下にdocフォルダ強制   ※廃止
        checkbox_doc_changed,    //ドキュメントフォルダは別指定  ※廃止

        combobox_docpathusage_changed, //コンボボックスでドキュメントパス指定　 0 -- 同じ 1 -- docフォルダ 2 -- 任意

        statemachine_name_changed,
        statamachine_checkbox_control_checked,
        statemachine_reset,

        gendir_path_textchanged //ソースパス変更



    }
}
