//<<<include=using.txt
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
//using Excel = Microsoft.Office.Interop.Excel;
//using Office = Microsoft.Office.Core;
using G=stateview.Globals;
using DStateData=stateview.Draw.DrawStateData;
using EFU=stateview._5300_EditForm.EditFormUtil;
using SS=stateview.StateStyle;
using DS=stateview.DesignSpec;
//>>>


namespace stateview
{
    internal static class Flow
    {
        internal static void main_flow(bool bForceReadExcel=false)
        {
            //ヒストリ初期化
            History.Init();

            History2.Init();
            FocusTrack.Init();

            G.Dirty_clear_all(); //G.bDirty = false;

            Coroutine.Start(main_flow_co(false, bForceReadExcel));
        }

        internal static void main_skip_load_flow()
        {
            //SaveLoad.LoadTemp();
            //SaveLoadJson.LoadTempJson();
            Coroutine.Start(main_flow_co(true));
        }

        internal static void main_skip_load_and_reset_flow()
        {
            G.state_location_list = null;
            Coroutine.Start(main_flow_co(true));
        }
        static Draw m_draw { get { return G.draw; } set { G.draw = value; } }
        static bool m_busy = false;

        //
        //
        internal static IEnumerator main_flow_co(bool bSkipLoadFiles=false, bool bForceReadExcel = false)
        {
            if (m_busy) yield break;

            m_busy = true;
            G.frontend_enable(false); //G.view_form.menuStrip1.Enabled = false;

            G.view_form.SuspendLayout();

//#if xx
            //２重オープン確認
            var b= CheckOpenSameDoc.Check_firstOpen();
            if (!b)
            {
                //if (!G.option_jump_if_statego_exist)
                if (!RegistryWork.Get_item_jump_if_statego_exist())
                {
                    var dlg = new _5000_ViewForm.dialog.OkForm();
                    dlg.Text = "Warning";
                    dlg.textBox1.Text = G.Localize("wdg_alredyopend");// "The file was opened by another StateGo."+ Environment.NewLine + "Quit." ;
                    dlg.Show(G.view_form);
                
                    while(dlg.DialogResult == DialogResult.None)
                    {
                        yield return null;
                    }
                }

                WindowsUtil.ActiveWindow(CheckOpenSameDoc.handle_check_firstOpen);
                G.view_form.DialogResult = DialogResult.Abort;
                G.view_form.Close();
                yield break;
            }
//#endif
            //上記の方法でも抜けが発生する
            //Mutexを利用して、完全起動阻止する。
            //オリジナルへのFocus移動はなしとする。
            if (!CheckOpenSameDoc.Check_firstOpenByMutex())
            {
                var dlg = new _5000_ViewForm.dialog.OkForm();
                dlg.Text = "Warning";
                dlg.textBox1.Text = G.Localize("wdg_alredyopend_exit");// "The file was opened by another StateGo."+ Environment.NewLine + "Quit." ;
                dlg.Show(G.view_form);

                while (dlg.DialogResult == DialogResult.None)
                {
                    yield return null;
                }
                G.view_form.DialogResult = DialogResult.Abort;
                G.view_form.Close();
                yield break;
            }

            //スケール退避
            var savescale = G.scale_percent;
            var panel_scroll_pos  = G.view_form.panel1.AutoScrollPosition;

            G.view_form.panel1.AutoScrollPosition = new Point(0,0);

            if (!bSkipLoadFiles)
            {
                Dispose();

                G.NoticeToUser("PSGG DATA VERSION : " + G.psgg_header_info_version);

                //
                if (G.psgg_file_w_data)
                {
                    G.NoticeToUser(string.Format("Reading PSGG \"{0}\"", Path.GetFileName( G.psgg_file ) ));
                }
                else
                { 
                    G.NoticeToUser(string.Format("Reading Excel \"{0}\" step 1 of 2", G.load_file_name));
                }
                yield return null;

                // 仮想Excelよりデータ収集

                var ep2 = new ExcelCellCacheRead();

                if (G.psgg_file_w_data) //新PSGG
                {
                    var tmp_bReadFromExcel = false;
                    if (!bForceReadExcel) // 強制されてない場合、ヘッダのreadfromがexcel時は、ExcelReadをtrueへ
                    {
                        if (FileDbUtil.is_psggfile_readfrom_excel())
                        {
                                tmp_bReadFromExcel = true;
                        }
                    }
                    else
                    {
                        tmp_bReadFromExcel = true;
                    }

                    if (tmp_bReadFromExcel)
                    {
                        if (!File.Exists(G.load_file))
                        {
                            G.NoticeToUser_warning("Because Excel File does not exist, PSGG file will be read.");
                            tmp_bReadFromExcel = false;
                        }
                    }

                    // リード
                    var coindex = Coroutine.Start(ep2.ReadCellsAndBmpFromPsggFile_co(tmp_bReadFromExcel));
                    while(Coroutine.IsRunning(coindex))
                    {
                        yield return null;
                    }
                }
                else
                { 
                    var coindex = Coroutine.Start(ep2.ReadCellsAndBmps_CO());
                    while(Coroutine.IsRunning(coindex))
                    {
                        yield return null;
                    }
                }
                //
                ExcelSave.CreateCache(); //変更前を保存

                //
                //G.status = "Read Excel 2";
                if (G.psgg_file_w_data)
                {
                    G.NoticeToUser("Reading table data.");
                }
                else
                { 
                    G.NoticeToUser("Reading Excel step 2 of 2");
                }
                yield return null;
                G.excel_program = new ExcelProgram();
                G.excel_program.Init();


                G.state_working_list_reflesh();
                //G.state_working_list = G.excel_program.GetStateList();
                //G.state_working_col_list = G.excel_program.GetStateColList();


                G.name_list  = G.excel_program.GetNameList();

                //uuid補完
                var maxuuid = G.excel_program.get_max_uuid();
                if (maxuuid==0) maxuuid = 100000;
                foreach(var st in G.state_working_list)
                {
                    var uuid = G.excel_program.GetUUID(st);
                    if (uuid==-1)
                    {
                        G.excel_program.SetUUID(st,(double)(++maxuuid));
                    }
                }

                //G.status = "Load Layout";
                G.NoticeToUser("Reading config data");

                yield return null;

                if (!SaveLoadIni.LoadIni())
                {
                    SaveLoadIni.LoadTempIni(); //最後に一時Iniファイル
                }

                GroupNodeUtil.NormalizeGroupNodeCommentPosList(); // G.nodegroup_comment_listの更新

                //SaveUserProfile.Load();

                G.view_form.UpdateUserButton();

                G.use_cmn_editor = RegistryWork.Get_use_cmn_editor();

                if (string.IsNullOrEmpty(G.external_source_editor))
                {
                    var registname = SettingIniUtil.GetLangFramrwork_registName();
                    if (!string.IsNullOrEmpty( G.source_editor_set ))
                    {
                        registname += "~~" + G.source_editor_set;
                    }
                    if (RegistryWork.Get_use_cmn_editor())
                    {
                        registname = "cmn(cmn)";
                    }

                    G.external_source_editor = RegistryWork.Get_srceditcmd(registname);
                }
                {
                    var optionstr = RegistryWork.Get_srceditcmd_option(SettingIniUtil.GetLangFramrwork_registName());
                    if (optionstr!=null) {
                        G.source_editor_vs2015_support = optionstr.Contains(WordStorage.Store.srceditcmd_option_vs2015);
                    }
                }
                {
                    G.use_batch_for_source_editor_open = RegistryWork.Get_execbatch_editor();
                }


                G.line_color.Write_to_LineColorTab();

                G.fillter_state_location_list = LocationUtil.CleanUp(G.fillter_state_location_list, G.state_working_list);

                G.macro_ini = new MacroIni();
                G.macro_ini.ReadMacroIni();

                //G.psggConverter.getChartFunc

                G.view_form.webBrowserHelp_setup();

                G.cc.SetWorkFolderIfExists();
                G.cc.SetAndCreateSysTempFolferIfNotExists();
            }

            //SaveLoad.LoadTemp();

            //
            //G.status = "Create Bitmap";
            G.NoticeToUser("Creating initial bitmap");
            yield return null;

            m_draw = new Draw();
            var g    = m_draw.create_bitmap(out G.mainbitmap);
            G.maingraphs = g;

            var pb = G.main_picturebox;
            pb.Parent = G.view_form.panel1;
            pb.Location = new Point(0,0);
            pb.Image = G.mainbitmap;
            pb.Width = pb.Image.Width;
            pb.Height = pb.Image.Height;


            DrawBenri.draw_opt();

            //state control 開始
            G.vf_sc = new ViewFormStateControl();
            G.vf_sc.Init();

            //
            //G.status = "Done";
            G.NoticeToUser("Initialization was done.");

            //SaveLoad.SaveTemp(); //ポジション更新
            //SaveLoadJson.SaveTempJson();
            SaveLoadIni.SaveTempIni();

            //Tree View Update
            //G.dirForm.UpdateTree();
            G.tabNodeTree.CreateAndSetCurrent();

            //スケール復帰
            G.set_scalepercent_with_textbox(savescale);
            ChangeScale.Change(savescale);
            G.view_form.panel1.AutoScrollPosition = panel_scroll_pos;

            //option
            G.view_form.checkBoxDeleteBr.Checked = G.option_delete_br_string;
            G.view_form.checkBoxDeleteThis.Checked = G.option_delete_thisstring;
            G.view_form.checkBoxDelBracket.Checked = G.option_delete_bracket_string;
            G.view_form.checkBoxStateS_.Checked    = G.option_delete_s_state_string;
            G.view_form.checkBoxDeleteBase.Checked = G.option_omit_basestate_string;
            G.view_form.checkBoxHideBaseContents.Checked = G.option_hide_basestate_contents;
            G.view_form.checkBoxHideBranchCmt.Checked = G.option_hide_branchcmt_onbranchbox;
            G.view_form.checkBoxCmtHeightFixed.Checked = G.comment_block_fixed;

            //custom
            G.view_form.cb_statecmt.Checked = G.use_statecmt;
            G.view_form.cb_thumbnail.Checked = G.use_thumbnail;
            G.view_form.cb_contents.Checked = G.use_contents;

            //マーク
            G.view_form.radioButton_mark_show();

            //
            G.view_form.webBrowserInfo_setup2();

            //最初の履歴
            History2.SaveForce_initialized("Initilized");
            FocusTrack.Record_atinitiaze();
            //FocusTrack.Record_curpath();

            //フォント
            G.view_form.font_and_frame_setup();

            //yield return null;

            G.frontend_enable(true);//G.view_form.menuStrip1.Enabled = true;

            //コンバーター準備
            Converter.Prepare();

            //ブランチ収集
            G.brancApiCollector = new BranchApiCollector();
            G.brancApiCollector.Refresh();
            G.brancApiCollector.ModifiyMenuItemInViewForm();

            //ステートメニュー表示のリンク項目 インスタンス
            G.linkItemsOnStateMenu = new LinkItemsOnStateMenu();

            //ウインドウ項目更新
            G.view_form.UpdateWindows();

            //スクロール更新
            G.Scroll_init();
            G.Scroll_mpb_changed();

            //シリアル確認
            //RegisterControl.Invoke(RegisterControl.Mode.check);

            //ヘルプ
            G.help_program2 = new helpProgram2();
            G.itemsInfo_program = new itemsInfoProgram();


            //ファイルバージョンチェック
            if (G.psgg_ask_upgrade==null) //ヌル時は一度も機能していない
            {
                if (G.psgg_file_w_data)
                {
                    G.psgg_ask_upgrade=false; //尋ねる必要なし
                }
                else
                {
                    G.psgg_ask_upgrade=true; //尋ねる
                }
            }

            //Tab ソース Box 初期化
            var zoom = RegistryWork.Get_srctabpanel_zoom();
            G.view_form.scintillaBoxTabFunc.Init(G.view_form,zoom,true);
            G.view_form.scintillaBoxTabFunc.ReadOnly = true;

            G.view_form.ResumeLayout();

            m_busy = false;
        }

        //
        internal static void Dispose()
        {
            if (G.excel_program!=null) {
                G.excel_program = null;
            }
            if (G.excel_pictures!=null)
            {
                G.excel_pictures.Dispose();
                G.excel_pictures = null;
            }
        }

        // tool for this class

        //static PointF? _get_lo_position_from_excel(string state)
        //{
        //    return LocUtil.Get_lo_position_from_excel(state);
        //    //return G.get_node_pos(state);
        //}

        static PointF? _get_lo_position(string state)
        {
            return LocUtil.Get_lo_position(state);
//            Dictionary<string, PointF> list = null;

//#if x
//            if ( FillterWork2.HasFillterValue(G.fillter_cur_id) )
//            {
//                if (G.fillter_state_location_list!=null && G.fillter_state_location_list.ContainsKey(G.fillter_cur_id))
//                {
//                    list = G.fillter_state_location_list[G.fillter_cur_id];
//                }
//            }
//#else
//            if (G.fillter_state_location_list!=null)
//            {
//                var id = G.get_node_curdir();
//                if (G.fillter_state_location_list.ContainsKey(id))
//                {
//                    list = G.fillter_state_location_list[id];
//                }
//                if (list == null)
//                {
//                    if (id == "/") id = ""; //旧ロケーション対策
//                    if (G.fillter_state_location_list.ContainsKey(id))
//                    {
//                        list = G.fillter_state_location_list[id];
//                    }
//                }
//            }
//#endif
//            if (list == null)
//            {
//                list = G.state_location_list;
//            }

//            if (list!=null)
//            {
//                var lodic = list;
//                if (lodic.ContainsKey(state))
//                {
//                    return lodic[state];
//                }
//            }
//            return null;
        }
    }
}

