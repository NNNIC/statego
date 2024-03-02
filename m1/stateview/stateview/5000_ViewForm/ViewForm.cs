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
using System.Reflection;
using System.Net;
using System.Collections.Specialized;

namespace stateview._5000_MainForm
{
    public partial class ViewForm:Form
    {
        public string m_version { get { return G.version + "." + G.githash.Substring(0,7); } }

        public ViewForm()
        {
            G.cc = new CopyCollection();

            G.line_color = new LineColor();
            G.line_color.Reset();

            G.view_form = this;

            if (!WBEmulator.IsBrowserEmulationSet())
            {
                WBEmulator.SetBrowserEmulationVersion();
            }

            InitializeComponent();
        }

        private void configToolStripMenuItem_Click(object sender,EventArgs e)
        {
            if (G.bDirty)
            {
                MessageBox.Show(G.Localize("cnff_must_not_dirty"));
                return;
            }
           

            var cf = new ConfigForm();
            cf.ShowDialog();
        }

        //private void OpenExcelToolStripMenuItem1_Click(object sender, EventArgs e)
        //{
        //    if (File.Exists(G.load_file))
        //    {
        //        //ExecUtil.execute("start \"\" \"" + Path.GetFullPath(G.load_file) +"\"", Path.GetDirectoryName(G.load_file));
        //        ExecUtil.execute_start(Path.GetFullPath(G.load_file), G.load_file_dir);
        //    }

        //}
        private void ReloadToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (G.psgg_file_w_data)
            {
                if (File.Exists(G.psgg_file))
                {
                    Flow.main_flow();
                    return;
                }
            }
            else {
                if (File.Exists(G.load_file))
                {
                    Flow.main_flow();
                    return;
                }
            }

            G.NoticeToUser_warning("Faild To Reload");
        }
        private void importExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!G.psgg_file_w_data)
            {
                G.NoticeToUser_warning("Disable here in program! {CBF81B9A-2DB5-45B0-818A-B8A6C491760D}");
                return;
            }
            if (File.Exists(G.load_file))
            {
                Flow.main_flow(true);
            }
            else
            {
                G.NoticeToUser_warning("Excel file does not exist.");
            }
        }
        public int m_timer_tick_counter = 0;
        private void timer1_Tick(object sender,EventArgs e)
        { 
            m_timer_tick_counter++;
            if (G.request_startload)
            {
                G.request_startload = false;
                Flow.main_flow(G.psgg_header_info_read_from == "excel"  );
            }
            AppUpdate.main_update();

            //if (RegisterControl.V != null) RegisterControl.V.Update();

            if (G.debug_form!=null)
            {
                //G.NoticeToUser("active="+  ActiveControl.Name);
                //panel1.Focus();
                //System.Diagnostics.Debug.WriteLine("active="+  ActiveControl.Name);
            }

            if (panel1.m_wheel_delta!=0)
            {
                var times = Math.Abs( panel1.m_wheel_delta / 120 );
                ChangeScale.Change(panel1.m_wheel_delta, times);

                zoomTextBox.Text = G.scale_percent.ToString();

                panel1.m_wheel_delta = 0;

            }

            if (m_timer_tick_counter % 5 == 0)
            {
               labelZoomCheck(); 
            }

            KeyProc.input_update();
            KeyProc.work_at_update();

            G.m_history_record_panel.timer_update();
            G.m_focus_track_panel.timer_update();

            //modify update
            if (G.tick_counter%10==0)
            {
                var view_change_on = G.bDirty_by_not_same_inital_group;
                var pos_change_on = G.bDirty_by_edited_pos_only;
                var change_value_on = G.bDirty_by_modified_value;
                if (label_changedValue.Visible != change_value_on)
                {
                    label_changedValue.Visible = change_value_on;
                    label_changedValue.Enabled = change_value_on;
                }
                if (label_pos_changed.Visible != pos_change_on)
                {
                    label_pos_changed.Visible = pos_change_on;
                    label_pos_changed.Enabled = pos_change_on;
                }
                if (label_view_changed.Visible != view_change_on)
                {
                    label_view_changed.Visible = view_change_on;
                    label_view_changed.Enabled = view_change_on;
                }
            }

            //Control c = ActiveControl;

            //if (c != null)
            //{
            //    Console.WriteLine("現在アクティブなコントロールは、{0}です。", c.Name);
            //}
            //else
            //{
            //    Console.WriteLine("現在アクティブなコントロールはありません。");
            //}
        }

        private void ViewForm_Load(object sender,EventArgs e)
        {
            //System.Diagnostics.Debugger.Launch();

            //G.cc.SetWorkFolderIfExists();

            MainPictureBox.AllowDrop = true;
            panel1.AllowDrop = true;

            testToolStripMenuItem.Visible = false;
            aboutToolStripMenuItem.Visible = false;
#if DEBUG
            testToolStripMenuItem.Visible = true;
            debugFormOnOffToolStripMenuItem.Visible = true;
#endif

            label_scale_pos.Parent = this;

            this.DialogResult = DialogResult.None;

            //labelVer.Text = m_version;

            zoomTextBox.MouseWheel += ZoomTextBox_MouseWheel;

            G.main_picturebox.MouseWheel += ZoomTextBox_MouseWheel;

            G.debug_form = new _7000_DebugForm.DebugForm();
            G.debug_form.Show();
            G.debug_form.Visible = false;

            G.find_form = new _5000_ViewForm.dialog.FindForm();
            G.find_form.Show(G.view_form);
            G.find_form.Visible = false;

            G.option_form = new _5000_ViewForm.dialog.OptionForm();

            //G.focustrack_form = new _1185_FocusTrack.FocusTrackForm();
            //G.focustrack_form.Show();

            G.m_history_record_panel = new HistoryRecordPanel();
            G.m_history_record_panel.onload();

            G.m_focus_track_panel = new FocusTrackPanel();
            G.m_focus_track_panel.onload(); 


            G.vf_sc = null;

            if (string.IsNullOrEmpty(G.load_file))
            {
                var args = Environment.GetCommandLineArgs();
                if (args.Length > 1)
                {
                    var file = args[1];
                    G.load_file = Path.GetFullPath(file);
                    if (string.IsNullOrEmpty( Path.GetDirectoryName(G.load_file) ))
                    {
                        G.load_file = Path.Combine( Environment.CurrentDirectory, G.load_file);
                    }

                    G.request_startload = true;
                }
                if (args.Length > 2)
                {
                    InterpretIniFile(args[2]); //2番目はiniファイル
                }
            }
            else
            {
                if (string.IsNullOrEmpty( Path.GetDirectoryName(G.load_file) ))
                {
                    G.load_file = Path.Combine( Environment.CurrentDirectory, G.load_file);
                }

                G.request_startload = true;
            }

            //
            //G.dirForm = new _5150_DirForm.DirForm();
            //G.dirForm.Show();

            G.tabNodeTree = new TabNodeTree();

            //G.debug_form.Visible = false;


            textBoxLabel.Parent = this;
            textBoxLabel.Location = PointUtil.Add_X(textBoxLabel.Location, panel1.Location.X);
            textBoxLabel.BringToFront();

            SysLangWork.Setup();
            SysLangWork.ChangeSysLang();

            //webBrowserHelp_setup();
            webBrowserInfo_setup();

            //font_setup();

            setupUtitlitesButtons();

            setupCustomPanel();

            WordStorage.Res.ChangeAll(this,G.system_lang);
            WordStorage.Res.ChangeAll(this.helpToolStripMenuItem,G.system_lang);

            RegistryWork.Set_running_timestamp();

            this.Focus();

            //key event
            this.panel1.PreviewKeyDown += Panel1_PreviewKeyDown;

            MainPictureBox.Parent = panel1;
            ////panel2追加
            //// panel2上にbitmapを持ってもらう。
            //// panel1は Windowsと一体
            //panel2.Parent = panel1;
            //MainPictureBox.Parent = panel2;
            //panel2.Width = G.bitmap_width;
            //panel2.Height = G.bitmap_height;
            //panel2.Location = Point.Empty;
            ////this.panel2.PreviewKeyDown += Panel1_PreviewKeyDown;

        }

        private void Panel1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //if (e.KeyCode == Keys.T)
            //{
            //    SimpleTrace.exec();
            //}
            KeyProc.m_keycode_from_viewform = e.KeyCode;
            //G.NoticeToUser("preview key = " + e.KeyCode);
        }

        //private void Panel1_MouseWheel(object sender, MouseEventArgs e)
        //{
        //    //throw new NotImplementedException();

        //}

        //protected override void WndProc(ref Message m)
        //{
        //    int WM_MOUSEWHEEL = 0x20A; //マウスホイールのメッセージ

        //    if (G.debug_form!=null) G.debug_form.textBoxWheel.Text = m.Msg.ToString();
        //    //if (m.Msg == WM_MOUSEWHEEL){
        //    //    //long wheelDelta = (Int64)m.WParam;
        //    //    int wheelDelta = (int)(Int64)m.WParam >> 16;
        //    //    G.debug_form.textBoxWheel.Text = wheelDelta.ToString();
        //    //}
        //    //else
        //    //{
        //    //    if (G.debug_form!=null) G.debug_form.textBoxWheel.Text = "";
        //    //}

        //    base.WndProc(ref m);
        //}
        public void font_and_frame_setup()
        {
            // font
            comboBoxFont.Items.Clear();
            foreach(var ff in FontFamily.Families)
            {
                if (string.IsNullOrWhiteSpace(ff.Name)) continue;
                comboBoxFont.Items.Add(ff.Name);
            }

            var cell = G.excel_program.GetCell(G.STATE_ROW,G.NAME_COL);
            var fontname = G.DEFAULT_FONT;
            var fontsize = G.font_size;
            //try {
            //    using(var font = new Font(cell.fontname,cell.fontsize))
            //    {
            //        fontname = font.FontFamily.GetName(0);
            //        fontsize = cell.fontsize;
            //    }
            //} catch {
            //    fontname = G.DEFAULT_FONT;
            //    fontsize = G.DEFAULT_FONTSIZE;
            //}

            if(Array.FindIndex(FontFamily.Families,i => i.GetName(0) == fontname) >= 0)
            {
                comboBoxFont.Text = fontname;
            }
            else
            {
                comboBoxFont.Text = G.DEFAULT_FONT;
            }
            textBoxFontSize.Text = fontsize.ToString();

            //frame
            var state_width  = (int)G.state_width;
            var state_height = (int)G.state_height;

            var state_short_width  = (int)G.state_short_width;
            var state_short_height = (int)G.state_short_height;

            var cmt_height  = (int)G.comment_block_height;
            var cnt_max_height = (int)G.content_max_height;
            var line_space  = (int)G.line_space;
            //try {
            //    state_width = (int)cell.width;
            //}
            //catch {
            //    state_width  = (int)G.state_width;
            //    state_height = (int)G.state_height;
            //    cmt_height   = (int)G.comment_block_height;
            //    line_space  = (int)G.line_space;
            //}
            //G.state_width          = state_width;
            //G.state_height         = state_height;
            //G.comment_block_height = cmt_height;
            //G.line_space           = line_space;

            textBoxStateWidth.Text   = state_width.ToString();
            textBoxStateHeight.Text  = state_height.ToString();

            textBoxShortWidth.Text   = state_short_width.ToString();
            textBoxShortHeight.Text  = state_short_height.ToString();
           
            textBoxCmtBlkHeight.Text = cmt_height.ToString();
            textBoxCntMaxHeight.Text = cnt_max_height.ToString();
            textBoxLineSpace.Text    = (-line_space).ToString();

            var bEnabled = G.comment_font_size > 0;
            textBoxCommentFontSize.Enabled = bEnabled;
            textBoxCommentFontSize.Text = bEnabled ?  G.comment_font_size.ToString() : string.Empty;

            bEnabled = G.contents_font_size > 0;
            textBoxContentFontSize.Enabled = bEnabled;
            textBoxContentFontSize.Text =  bEnabled ? G.contents_font_size.ToString() : string.Empty;
        }

        private void setupUtitlitesButtons()
        {
            bool jxmode = false;

            Action<Button,bool> set = (but,b) => {
                but.Enabled = b;
                but.Visible = b;
            };

            set(buttonOpenSourceFolder,!jxmode);
            set(buttonOpenExcelFolder, !jxmode);
            set(buttonOpenConvertSrc,  !jxmode);
            set(buttonOpenImpleSource, !jxmode);

            //set(buttonExtra,            jxmode);

            //if (jxmode)
            //{
            //    buttonExtra.Text = G.but_extra_text;
            //}
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
           //base.OnMouseWheel(e);
           HandledMouseEventArgs wEventArgs = e as HandledMouseEventArgs;
            wEventArgs.Handled = true;
        }

        private void ZoomTextBox_MouseWheel(object sender,MouseEventArgs e)
        {
            //G.log += "wheel " + e.Delta;

            //ChangeScale.Change(e.Delta);

            var newscale = ChangeScale.CalcChange(e.Delta);

            zoomTextBox.Text =  newscale.ToString("F0");  //G.scale_percent.ToString();
        }

        private void MainPictureBox_Click(object sender,EventArgs e)
        {
            panel1.Focus();
            //G.log += "Clicked" + Environment.NewLine;
            AppUpdate.mouse_update(MouseEventId.CLICK);
        }

        private void MainPictureBox_DoubleClick(object sender,EventArgs e)
        {
            panel1.Focus();
            G.log+= "DClicked" + Environment.NewLine;
            AppUpdate.mouse_update(MouseEventId.DOUBLECLICK);
        }

        private void MainPictureBox_MouseDown(object sender,MouseEventArgs e)
        {
            panel1.Focus();
            //G.log+="MouseDown" + Environment.NewLine;
            AppUpdate.mouse_update(MouseEventId.MOUSEDOWN);
        }

        private void MainPictureBox_MouseUp(object sender,MouseEventArgs e)
        {
            //G.log+="MouseUp" + Environment.NewLine;
            AppUpdate.mouse_update(MouseEventId.MOUSEUP);
        }

        private void MainPictureBox_MouseMove(object sender,MouseEventArgs e)
        {
            //G.log+="Move" + Environment.NewLine;
            AppUpdate.mouse_update(MouseEventId.MOVE);
        }

        private void zoomTextBox_MouseEnter(object sender,EventArgs e)
        {

        }

        private void zoomTextBox_MouseHover(object sender,EventArgs e)
        {

        }

        private void zoomTextBox_MouseLeave(object sender,EventArgs e)
        {

        }

        private void zoomTextBox_TextChanged(object sender,EventArgs e)
        {
            double v = G.scale_percent;
            try {
                v = double.Parse(zoomTextBox.Text);

                if (v == G.scale_percent)
                {
                    return;
                }

            }
            catch
            {
                return;
            }

            try {
                if ((int)trackBarZoom.Value != v)
                {
                    trackBarZoom.Value = (int)v;
                }

#if trytry
                /*
                   カーソルがViewのBitmap内の場合、それを中心にスケールする
                */
                if (ViewUtil.CheckInViewScreenPosition(Cursor.Position))
                {
#if try
                    var bmppos1 = G.vf_sc.GetPointerOnMainBmp(Cursor.Position);
                    
                    ChangeScale.Change(v,false);

                    var bmppos2  = G.vf_sc.GetPointerOnMainBmp(Cursor.Position);
                    var topleft2 = ViewUtil.GetViewTopLeft();
                    
                    var topleft3 = PointUtil.Add_XY(topleft2, bmppos1.X - bmppos2.X, bmppos1.Y - bmppos2.Y  );

                    ViewUtil.SetViewTopLeft(topleft3.X, topleft3.Y);
#endif
                    var center = ViewUtil.GetCenterInView();
                    ChangeScale.Change(v,false);
                    ViewUtil.SetViewCenter(center.X,center.Y);
                }
                else
                {                 
                    ChangeScale.Change(v);
                }
                update_label_scale_indicator();
#endif
                ChangeScale.Change(v);
                update_label_scale_indicator();
            }
            catch
            {
                return;
            }
        }

        //private void test2ToolStripMenuItem_Click(object sender,EventArgs e)
        //{
        //    //G.load_file = @"C:\Users\gea01\Documents\state_managemet_tools\state-chart2\m1\stateview\stateview\5100_ViewForm_StateControl\doc\ViewFormStateControl.xlsx";
        //    G.load_file = @"C:\Users\gea01\Documents\state_managemet_tools\state-chart2\test\ViewFormStateControl.xlsx";
        //    Flow.main_flow();
        //}

        //private void copyToolStripMenuItem_Click(object sender,EventArgs e)
        //{
        //    if (G.mainbitmap!=null)
        //    {
        //        Clipboard.SetImage(G.mainbitmap);
        //    }
        //}

        //private void saveLayoutToolStripMenuItem_Click(object sender,EventArgs e)
        //{
        //    //SaveLoad.Save();
        //    //SaveLoadJson.SaveJson();
        //    SaveLoadIni.SaveIni();
        //    //SaveUserProfile.Save();
        //}

        private void refreshStripMenuItem_Click(object sender,EventArgs e)
        {
            Flow.main_skip_load_flow();
        }
        //private void refreshLayoytStripMenuItem_Click(object sender,EventArgs e)
        //{
        //    //SaveLoad.ClearPositionsOfFillterId();
        //    //SaveLoad.SaveTemp(false);
        //    G.state_location_list = null;
        //    if ( G.fillter_state_location_list!=null && G.fillter_state_location_list.ContainsKey(G.m_target_pathdir))
        //    {
        //        G.fillter_state_location_list.Remove(G.m_target_pathdir);
        //    }
        //    Flow.main_skip_load_and_reset_flow();
        //}

        //private void test4ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    G.load_file =@"C:\project-jx\project\1000_develop\main\build\2000_room.xlsm";
        //    Flow.main_flow();

        //}

        private void ViewForm_FormClosing(object sender,FormClosingEventArgs e)
        {
            RegistryWork.Set_srctabpanel_zoom(scintillaBoxTabFunc.GetZoom());

            if (this.DialogResult == DialogResult.None) {
                bool bOkClose = false;

                var needDialog = G.bDirty_by_modified_value || G.bDirty_by_edited_pos_only;
                if (!G.option_forceclose_ifviewchangeonly)
                {
                    needDialog = needDialog || G.bDirty_by_not_same_inital_group;
                }

                if (needDialog)//if (History.HasHistoryGtOne())
                {
                    var s = G.Localize("w_check_close") + "\n";
                    if (G.bDirty_by_modified_value)
                    { 
                        s += "\n - "+ G.Localize("w_valuehanged");//"Value has been modfied.";
                    }
                    else
                    { 
                        if ( G.bDirty_by_edited_pos_only && !G.bDirty_by_not_same_inital_group) s+= "\n - " + G.Localize("w_poschanged");//"Positions has been modified";
                        if (!G.bDirty_by_edited_pos_only &&  G.bDirty_by_not_same_inital_group) s+= "\n - " + G.Localize("w_viewingchanged");// "Viewing group has been changed.";
                    }
                    //s += "\nmodified values : " + ( G.bDirty_by_modified_value ? "YES" :"NO"  );
                    //s += "\nedited states positions : " + (G.bDirty_by_edited_pos_only ? "YES":"NO");
                    //s += "\nchanged viewing group :" + (G.bDirty_by_moved_group_only ? "YES":"NO");

                    if (MessageBox.Show(s, "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        bOkClose = true;
                    }
                    else
                    {
                        bOkClose = false;
                    }
                }
                else
                {
                    bOkClose = true;
                }


                if (bOkClose)
                {
                    G.ABORT= true;
                    G.find_form.Close();
                    //G.focustrack_form.Close();

                    if (G.mutex != null)
                    {
                        G.mutex.ReleaseMutex();
                        G.mutex = null;
                    }

                    Flow.Dispose();
                }
                else
                {
                    e.Cancel = true;
                }
            }

        }

        //private void test3toolStripMenuItem2_Click(object sender, EventArgs e)
        //{
        //    G.load_file =@"C:\Users\671\Documents\state_managemet_tools\state-chart2\m1\stateview\stateview\1510_Arrow\doc\ArrowFlow.xlsx";// @"C:\project-jx\project\1000_develop\main\build\2000_room.xlsm";
        //    Flow.main_flow();
        //}

        private void SaveOverWriteStripMenuItem2_Click(object sender,EventArgs e)
        {
            var ret = MessageBox.Show("Update File?","",MessageBoxButtons.YesNo);
            if (ret== DialogResult.Yes)
            {
                //if (m_sw == null)
                //{
                //    m_sw = new System.Diagnostics.Stopwatch();
                //}
                //m_sw.Reset();

                //m_sw.Start();

                //Coroutine.Start(save_co(false,false));
                SaveOnly();
            }
        }
        public void SaveOnly()
        {
            if (m_sw == null)
            {
                m_sw = new System.Diagnostics.Stopwatch();
            }
            m_sw.Reset();

            m_sw.Start();

            Coroutine.Start(save_co(false,false));
        }


        private void SaveAndRunToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveAndRun();
        }

        private System.Diagnostics.Stopwatch m_sw;
        public void SaveAndRun(bool bCheck=true)
        {
            bool bforce_convert_wo_save=false; //セーブせずに変換する。 Readonly時の対応用

            if (m_sw == null)
            {
                m_sw = new System.Diagnostics.Stopwatch();
            }
            m_sw.Reset();

            m_sw.Start();

            DialogResult ret= DialogResult.Yes;

            if (G.option_ignore_case_of_state) //ステート名の大文字小文字を区別しない ※同じとした時に同一となるステートはないか？
            {
                G.NoticeToUser("Checking ignoring case of state");
                var samename_list = new List<string>();

                var statelist = G.excel_program.GetStateList();
                for(var i = 0; i<statelist.Count; i++)
                {
                    var str_i = statelist[i];
                    if (string.IsNullOrEmpty(str_i)) { G.NoticeToUser_warning("Unexpected! {5EAFBE19-50BC-42CD-806F-F90B588608D8}"); continue; }
                    str_i = str_i.ToUpper();
                    for(var j = 0; j<statelist.Count; j++)
                    {
                        if (i==j) continue;
                        var str_j = statelist[j];
                        if (string.IsNullOrEmpty(str_j)) { G.NoticeToUser_warning("Unexpected! {C8389FF0-9D23-4809-9C8B-A885BFA27F95}"); continue;  }
                        str_j = str_j.ToUpper();
                        if (str_i == str_j)
                        {
                            if (!samename_list.Contains(str_i))
                            {
                                samename_list.Add(str_i);
                            }
                        }
                    }
                }

                if (samename_list.Count == 0)
                {
                    G.NoticeToUser("... OK");
                }
                else
                {
                    var s = "";
                    samename_list.ForEach(i=> { if (s != "") s +=", "; s += i;  });
                    G.NoticeToUser_warning("... Error : " + s);
                    G.NoticeToUser_msgbox( " Found same state names with ignoring case conditon" + Environment.NewLine + s);
                    G.NoticeToUser_warning("Stopped converting." );
                    return;
                }
            }

            //substart と subreturn が一対になっているか？
            {
                var statelist = G.excel_program.GetStateList();
                var substart_list = new List<string>();
                //1. substartの収集
                foreach(var s in statelist)
                {
                    if (G.excel_program.GetString(s,G.STATENAME_statetyp)==WordStorage.Store.state_typ_substart)
                    {
                        substart_list.Add(s);
                    }
                }
                //2. それぞれのsubstartに連なるstateを収集
                foreach(var s in substart_list)
                {
                    var all_states = new List<string>();

                    // nextstateとbranchの収集
                    Func<string,List<string>> get_braches = (_)=> {
                        var tmp_list = new List<string>();
                        //2.1 nextstate , branch のステートラベル収集
                        var nextstate = G.excel_program.GetString(_,G.STATENAME_nextstate);
                        var branches = BranchUtil.GetLabelListFromState(_);
                        if (!string.IsNullOrEmpty(nextstate)) tmp_list.Add(nextstate);
                        if (branches.Count!=0) tmp_list.AddRange(branches);
                        return tmp_list;
                    };


                    Func<string,bool> getall = null;
                    getall = (_)=> {
                        var branches = get_braches(_);
                        if (branches.Count>0)
                        { 
                            var bAdd =false;
                            foreach(var a in branches)
                            { 
                                if (!all_states.Contains(a))
                                { 
                                    all_states.Add(a);
                                    bAdd = true;
                                }
                            }
                            return bAdd;
                        }
                        return false;
                    };


                    all_states.Add(s);
                    for(var loop = 0; loop <= 1E+6; loop++)
                    {
                        if (loop == 1E+6) throw new SystemException("{CB2A39C1-3BD8-4BC9-85B4-A2F0DE5643A2}");
                        var bNeedContinue = false;
                        foreach(var a in all_states)
                        {
                            var b = getall(a);
                            if (b) {
                                bNeedContinue = true;
                                break;
                            }
                        }
                        if (!bNeedContinue)
                        {
                            break;
                        }
                    }
                    //3. 収集したステートのなかのsubreturnは一つだけか？
                    var count = 0;
                    foreach(var a in all_states)
                    {
                        if (G.excel_program.GetString(a,G.STATENAME_statetyp)==WordStorage.Store.state_typ_subreturn)
                        {
                            count ++;                            
                        }
                    }
                    if (count != 1)
                    {
                        MessageBox.Show( s + " : " + WordStorage.Res.Get("vf_subroutinepairerror",G.system_lang)/*"Subroutine 'start' and 'return' pair is inconsistent."*/);
                        return;
                    }
                    else
                    {
                        G.NoticeToUser(s + " : Cheekced ..Subroutine 'start' and 'return' pair is consistent.");
                    }
                }
            }

            var bCheckExistLoadFile = false;
            var bCheckWritableLoadFile = false;
            if (!G.psgg_file_w_data)
            {
                bCheckExistLoadFile = true;
                bCheckWritableLoadFile = true;
            }
            else
            {
                if (G.psgg_header_info_save_mode_withexcel)
                {
                    bCheckExistLoadFile = false;
                    bCheckWritableLoadFile = true;
                }
                if (G.psgg_header_info_check_excel_writable_yes)
                {
                    bCheckWritableLoadFile = true;
                }
            }
            if (bCheckExistLoadFile)
            { 
                if (!File.Exists(G.load_file))
                {
                    MessageBox.Show(WordStorage.Res.Get("vf_excelnotexist",G.system_lang)  /*"Document file does not exist."*/);
                    return;
                }
            }
            if (bCheckWritableLoadFile)
            {
                if (File.Exists(G.load_file)) //ファイルがあるときだけ
                { 
                    if (FileUtil.IsReadOnly(G.load_file))
                    {
                        var cret = MessageBox.Show(WordStorage.Res.Get("vf_excelreadonly", G.system_lang) /*"Document file is a READ ONLY file.\n Continue?"*/, "WARNING", MessageBoxButtons.YesNo);
                        if (cret == DialogResult.No) return;
                        bforce_convert_wo_save = true;
                    }
                }
            }

            if (G.psgg_file_w_data)
            {
                if (!File.Exists(G.psgg_file))
                {
                    MessageBox.Show(WordStorage.Res.Get("vf_psggnotexist",G.system_lang)  /*"the psgg file does not exist."*/);
                    return;
                }
                if (FileUtil.IsReadOnly(G.psgg_file))
                {
                    var cret = MessageBox.Show(WordStorage.Res.Get("vf_psggreadonly",G.system_lang) /*"the psgg file is a READ ONLY file.\n Continue?"*/, "WARNING", MessageBoxButtons.YesNo);
                    if (cret == DialogResult.No) return;
                    bforce_convert_wo_save = true;
                }
            }


            if (bCheck || G.option_convert_with_confirm)
            {
                var msg = WordStorage.Res.Get("vf_askconvert",G.system_lang);
                ret = MessageBox.Show(msg, "Confirmation", MessageBoxButtons.YesNo);
            }
            if (ret == DialogResult.Yes)
            {
                Coroutine.Start(save_co(true, bforce_convert_wo_save));
            }
        }
        bool m_bSaveRunning = false;
        private IEnumerator save_co(bool bExecCommand, bool bConvert_wo_save)
        {
            //if (m_sw == null)
            //{
            //    m_sw = new System.Diagnostics.Stopwatch();
            //    m_sw.Start();
            //}

            if (m_bSaveRunning) yield break;
            m_bSaveRunning = true;
            G.frontend_enable(false); //G.view_form.menuStrip1.Enabled = false;
            {
                AltState.DeleteAllAltStates();

                if (!bConvert_wo_save)
                { 
                    SaveLoadIni.SaveIni();
                }
                yield return null;
                //SaveUserProfile.Save();
                //G.NoticeToUser("Saving " + G.load_file + " ....");

                //ExcelSave.Save();
                if (!bConvert_wo_save)
                { 
                    if (G.psgg_file_w_data)
                    {
                        //ExcelSave.Save_by_FileDb();           
                        var coindex = Coroutine.Start(ExcelSave.Save_by_FileDb_co());
                        while(Coroutine.IsRunning(coindex))
                        {
                            yield return null;
                        }
                    }
                    else
                    { 
                        var coindex = Coroutine.Start(ExcelSave.Save_co());
                        while(Coroutine.IsRunning(coindex))
                        {
                            yield return null;
                        }
                    }
                }
                //Command
                if (bExecCommand){
                    Converter.Convert();
                    if (!string.IsNullOrEmpty(SettingIniUtil.GetGeneratedHpp()))
                    {
                        Converter.Convert(true);
                    }
                }

                if (G.option_copy_output_to_clipboard)
                {
                    SettingIniUtil.CopyGeneratedSourceToClipboard();
                    G.NoticeToUser("Copy src to clipboard.");
                }
                G.NoticeToUser("Done !!");

                G.req_redraw_force();

                if (G.userbutton_callafterconvert)
                {
                    ExecuteUserButtonCommand();
                }
            }

            History2.SaveForce_initialized("Saved");

            G.brancApiCollector.Refresh();
            G.frontend_enable(true);//G.view_form.menuStrip1.Enabled = true;
            m_bSaveRunning = false;

            if (m_sw!=null) { 
                m_sw.Stop();
                G.NoticeToUser("Saving elapsed : " + m_sw.ElapsedMilliseconds + "msec");
            }
        }


        private void aboutToolStripMenuItem_Click(object sender,EventArgs e)
        {
            var dlg = new _6990_aboutForm.AboutForm();
            //dlg.FormBorderStyle = FormBorderStyle.None;

            dlg.ShowDialog();
        }

        private void deleteToolStripMenuItem_Click(object sender,EventArgs e)
        {
            ViewFormStateControl.m_viewFormStateMenuItem = ViewFormStateMenuItem.DELETE;
        }

        private void MenuItem_Edit_Click(object sender,EventArgs e)
        {
            ViewFormStateControl.m_viewFormStateMenuItem = ViewFormStateMenuItem.EDITFULL;
        }

        private void removeCommentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewFormStateControl.m_viewFormStateMenuItem = ViewFormStateMenuItem.REMOVE_COMMENT;
        }

        private void editBranchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewFormStateControl.m_viewFormStateMenuItem = ViewFormStateMenuItem.EDITBRANCH;
        }

        private void MenuItem_Copy_Click(object sender,EventArgs e)
        {
            ViewFormStateControl.m_viewFormStateMenuItem = ViewFormStateMenuItem.COPY;
        }

        //private void MenuItem_Rename_Click(object sender,EventArgs e)
        //{
        //    ViewFormStateControl.m_viewFormStateMenuItem = ViewFormStateMenuItem.RENAME;
        //}

        private void MenuItem_Refactor_Click(object sender,EventArgs e)
        {
            ViewFormStateControl.m_viewFormStateMenuItem = ViewFormStateMenuItem.REFACTOR;
        }
        private void MenuItem_MoveTo_Click(object sender,EventArgs e)
        {
            ViewFormStateControl.m_viewFormStateMenuItem = ViewFormStateMenuItem.MOVETO;
        }
        private void stateToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ViewFormStateControl.m_viewFormStateMenuItem = ViewFormStateMenuItem.CHANGE_STATE;
        }
        private void embedToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ViewFormStateControl.m_viewFormStateMenuItem = ViewFormStateMenuItem.CHANGE_EMBED;
        }
        private void commentToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ViewFormStateControl.m_viewFormStateMenuItem = ViewFormStateMenuItem.CHANGE_COMMENT;
        }
        private void StateMenu_Closed(object sender,ToolStripDropDownClosedEventArgs e)
        {
            ViewFormStateControl.m_viewFormStateMenuItem = ViewFormStateMenuItem.CANCEL;
        }
        private void MenuItem_exportToClipboard_Click(object sender, EventArgs e)
        {
            ViewFormStateControl.m_viewFormStateMenuItem = ViewFormStateMenuItem.EXPORTCLIPBAORD;
        }
        private void copyStateNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewFormStateControl.m_viewFormStateMenuItem = ViewFormStateMenuItem.COPY_STATENAME;
        }
        private void cancelToolStripMenuItem_Click(object sender,EventArgs e)
        {
            ViewFormStateControl.m_viewFormStateMenuItem = ViewFormStateMenuItem.CANCEL;
        }
        private void viewSourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewFormStateControl.m_viewFormStateMenuItem = ViewFormStateMenuItem.OPENSRC;
        }
        private void viewThisExstateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewFormStateControl.m_viewFormStateMenuItem = ViewFormStateMenuItem.VIEWEXSTATE;
        }
        private void setStopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewFormStateControl.m_viewFormStateMenuItem = ViewFormStateMenuItem.SET_STOP;
        }
        private void resetStopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewFormStateControl.m_viewFormStateMenuItem = ViewFormStateMenuItem.RESET_STOP;
        }
        private void setBaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewFormStateControl.m_viewFormStateMenuItem = ViewFormStateMenuItem.SET_BASE;
        }
        private void resetBaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewFormStateControl.m_viewFormStateMenuItem = ViewFormStateMenuItem.RESET_BASE;
        }
        private void setPassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewFormStateControl.m_viewFormStateMenuItem = ViewFormStateMenuItem.SET_PASS;
        }
        private void resetPassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewFormStateControl.m_viewFormStateMenuItem = ViewFormStateMenuItem.RESET_PASS;
        }

        private void groupingToolStripMenuItem_Click(object sender,EventArgs e)
        {
            ViewFormStateControl.m_viewFormStateMenuGFItem = ViewFormStateMenuGFItem.GROUPING;
        }
        private void deleteToolStripMenuItem1_Click(object sender,EventArgs e)
        {
            ViewFormStateControl.m_viewFormStateMenuGFItem = ViewFormStateMenuGFItem.DELETE;
        }
        private void exportToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewFormStateControl.m_viewFormStateMenuGFItem = ViewFormStateMenuGFItem.EXPORTCLIPBOARD;
        }
        private void alignHorizontallyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewFormStateControl.m_viewFormStateMenuGFItem = ViewFormStateMenuGFItem.ALIGN_HORIZONTALLY;
        }
        private void alignVerticallyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewFormStateControl.m_viewFormStateMenuGFItem = ViewFormStateMenuGFItem.ALIGN_VERTICALLY;
        }
        private void moveToToolStripMenuItem_Click(object sender,EventArgs e)
        {
            ViewFormStateControl.m_viewFormStateMenuGFItem = ViewFormStateMenuGFItem.MOVETO;
        }
        private void commentOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewFormStateControl.m_viewFormStateMenuGFItem = ViewFormStateMenuGFItem.COMMENT_OUT;
        }
        //private void unGroupingToolStripMenuItem_Click(object sender,EventArgs e)
        //{
        //    ViewFormStateControl.m_viewFormStateMenuGFItem = ViewFormStateMenuGFItem.UNGROUPING;
        //}

#region STATEMENU GN
        private void stateMenuGNItem_goInto_Click(object sender,EventArgs e)
        {
            ViewFormStateControl.m_viewFormStateMenuGNItem = ViewFormStateMenuGNItem.ENTER;
        }
        private void stateMenuGNITem_ungrouping_Click(object sender,EventArgs e)
        {
            ViewFormStateControl.m_viewFormStateMenuGNItem = ViewFormStateMenuGNItem.UNGROUPING;
        }
        private void stateMenuGNItem_edit_Click(object sender,EventArgs e)
        {
            ViewFormStateControl.m_viewFormStateMenuGNItem = ViewFormStateMenuGNItem.EDIT;
        }
        private void movetoToolStripMenuItem_Click_1(object sender,EventArgs e)
        {
            ViewFormStateControl.m_viewFormStateMenuGNItem = ViewFormStateMenuGNItem.MOVETO;
        }
        private void copyToolStripMenuItem_Click_1(object sender,EventArgs e)
        {
            ViewFormStateControl.m_viewFormStateMenuGNItem = ViewFormStateMenuGNItem.COPY;
        }

        private void deleteToolStripMenuItem_Click_1(object sender,EventArgs e)
        {
            ViewFormStateControl.m_viewFormStateMenuGNItem = ViewFormStateMenuGNItem.DELETE;
        }
        private void exportToClipboardToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ViewFormStateControl.m_viewFormStateMenuGNItem = ViewFormStateMenuGNItem.EXPORTCLIPBOARD;
        }
#endregion

        private void goOutToolStripMenuItem_Click(object sender,EventArgs e)
        {
            if (G.node_leave_group())
            {
                G.req_redraw();
            }
        }

        private void leaveToolStripMenuItem_Click(object sender,EventArgs e)
        {
            ViewFormStateControl.m_viewFormStataMenuBlankItem = ViewFormStataMenuBlankItem.LEAVE;
        }
        private void newStateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 廃止
            //ViewFormStateControl.m_viewFormStataMenuBlankItem = ViewFormStataMenuBlankItem.NEWSTATE;
        }
        private void stateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!G.template_has_statetyp)
            { 
                ViewFormStateControl.m_viewFormStataMenuBlankItem = ViewFormStataMenuBlankItem.NEW_STATE;
            }
            else
            {
                //if ( stateToolStripMenuItem.DropDown.IsDropDown)
                //{
                //    ViewFormStateControl.m_viewFormStataMenuBlankItem = ViewFormStataMenuBlankItem.NEW_STATE;
                //    blankMenu.Close();
                //}
            }
        }
        private void stateToolStripMenuItem_DoubleClick(object sender, EventArgs e)
        {
                ViewFormStateControl.m_viewFormStataMenuBlankItem = ViewFormStataMenuBlankItem.NEW_STATE;
                blankMenu.Close();
        }

        private void embedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewFormStateControl.m_viewFormStataMenuBlankItem = ViewFormStataMenuBlankItem.NEW_EMBED;
        }
        private void commentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewFormStateControl.m_viewFormStataMenuBlankItem = ViewFormStataMenuBlankItem.NEW_COMMENT;
        }
        private void normalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewFormStateControl.m_viewFormStataMenuBlankItem = ViewFormStataMenuBlankItem.NEW_STATE;
        }
        private void loopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewFormStateControl.m_viewFormStataMenuBlankItem = ViewFormStataMenuBlankItem.TYP_LOOP;
        }
        private void gosubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewFormStateControl.m_viewFormStataMenuBlankItem = ViewFormStataMenuBlankItem.TYP_GOSUB;
        }
        private void substartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewFormStateControl.m_viewFormStataMenuBlankItem = ViewFormStataMenuBlankItem.TYP_SUBSTART;
        }
        private void subendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewFormStateControl.m_viewFormStataMenuBlankItem = ViewFormStataMenuBlankItem.TYP_SUBRETURN;
        }
        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewFormStateControl.m_viewFormStataMenuBlankItem = ViewFormStataMenuBlankItem.TYP_START;
        }
        private void endToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewFormStateControl.m_viewFormStataMenuBlankItem = ViewFormStataMenuBlankItem.TYP_END;
        }
        private void pASSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewFormStateControl.m_viewFormStataMenuBlankItem = ViewFormStataMenuBlankItem.TYP_PASS;
        }
         private void sTOPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewFormStateControl.m_viewFormStataMenuBlankItem = ViewFormStataMenuBlankItem.TYP_STOP;
        }

        //private void backToolStripMenuItem1_Click(object sender,EventArgs e)
        //{
        //    ViewFormStateControl.m_viewFormStataMenuBlankItem = ViewFormStataMenuBlankItem.HISTORY_BACK;
        //}
        //private void forwardToolStripMenuItem1_Click(object sender,EventArgs e)
        //{
        //    ViewFormStateControl.m_viewFormStataMenuBlankItem = ViewFormStataMenuBlankItem.HISTORY_FORWARD;
        //}
        private void historyToolStripMenuItem1_Click(object sender,EventArgs e)
        {
            ViewFormStateControl.m_viewFormStataMenuBlankItem = ViewFormStataMenuBlankItem.HISTORY_BACK;
        }
        private void historyshowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewFormStateControl.m_viewFormStataMenuBlankItem = ViewFormStataMenuBlankItem.HISTORY_SHOW;
        }
        private void historyForwardToolStripMenuItem1_Click(object sender,EventArgs e)
        {
            ViewFormStateControl.m_viewFormStataMenuBlankItem = ViewFormStataMenuBlankItem.HISTORY_FORWARD;
        }
        private void importFromClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewFormStateControl.m_viewFormStataMenuBlankItem = ViewFormStataMenuBlankItem.IMPORT_CLIPBOARD;
        }
        private void pastWithoutOutflowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewFormStateControl.m_viewFormStataMenuBlankItem = ViewFormStataMenuBlankItem.IMPORT_CLIPBOARD_WO_OUTFLOW;
        }
        private void pasteUsingBaseModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewFormStateControl.m_viewFormStataMenuBlankItem = ViewFormStataMenuBlankItem.IMPORT_AS_BASESTATE;
        }
        private void trackshowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewFormStateControl.m_viewFormStataMenuBlankItem = ViewFormStataMenuBlankItem.TRACK_SHOW;
        }
        private void trackBackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewFormStateControl.m_viewFormStataMenuBlankItem = ViewFormStataMenuBlankItem.TRACK_BACK;
        }
        private void trackForwardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewFormStateControl.m_viewFormStataMenuBlankItem = ViewFormStataMenuBlankItem.TRACK_FORWARD;
        }
        private void TailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewFormStateControl.m_viewFormStataMenuBlankItem = ViewFormStataMenuBlankItem.FOCUS_TAIL;
        }
        private void headToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewFormStateControl.m_viewFormStataMenuBlankItem = ViewFormStataMenuBlankItem.FOCUS_HEAD;
        }
        private void headToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ViewFormStateControl.m_viewFormStataMenuBlankItem = ViewFormStataMenuBlankItem.FOCUS_HEAD;
        }
        private void headToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ViewFormStateControl.m_viewFormStataMenuBlankItem = ViewFormStataMenuBlankItem.FOCUS_HEAD;
        }
        private void webBrowser1_DocumentCompleted(object sender,WebBrowserDocumentCompletedEventArgs e)
        {
        }
        public void webBrowserHelp_setup()
        {
            // for update browser version, see WBEmulator class

            bool bOk = false;
            var index = tabControl.TabPages.IndexOfKey("tabPageHelp");
            if (index >= 0)
            { 
                try {
                    webBrowser1.ScriptErrorsSuppressed = true;
                    webBrowser1.ObjectForScripting = new WebBrowserFunc();
#if obs
                    ////webBrowser1.DocumentText = "<html><body>TEST</body></html>";
                    ////webBrowser1.Navigate.ToString("");
                    if (string.IsNullOrEmpty(G.web_help))
                    {
                        ////https://stackoverflow.com/questions/7194851/load-local-html-file-in-a-c-sharp-webbrowser
                        string appDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
                        webBrowser1.Url = new Uri(Path.Combine(appDir,@"help/index.html"));
                    }
                    else
                    {
                        webBrowser1.Url = new Uri(G.web_help);
                    }

                    //string appDir =  Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);//                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
                    //var str = File.ReadAllText(Path.Combine( appDir,@"help\index.html"));

                    //DisplayHtml(str);
#endif
                    var helpweb = SettingIniUtil.GetHelpweb();
                    if (!string.IsNullOrEmpty(helpweb)) // && File.Exists(helpweb))
                    {
                        if (File.Exists(helpweb))
                        { 
                            var str = File.ReadAllText(helpweb);
                            DisplayHtml(str);
                            bOk = true;
                             
                        }
                        else if (helpweb.StartsWith("http"))
                        {
                            var url = helpweb;  // reference https://stackoverflow.com/questions/16642196/get-html-code-from-website-in-c-sharp
                            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url); 
                            HttpWebResponse response = (HttpWebResponse) request.GetResponse(); 
                            StreamReader sr = new StreamReader(response.GetResponseStream()); 
                            var str = sr.ReadToEnd(); 
                            sr.Close(); 
                            DisplayHtml(str);
                            bOk = true;
                        }
                        else
                        {
                            DisplayHtml( string.Format( "<html><body>cannot find  {0}</body></html>", helpweb)  );
                        }
                    } 
                    else
                    {
                        DisplayHtml("<html><body>helpweb is not defined.</body></html>");
                    }   
                } catch (SystemException e) {
                        DisplayHtml("<html><body>helpweb is not defined. " + e.Message +"</body></html>");
                }
            }
            if (!bOk)//表示物が無いなら消す
            {
                if (index >= 0) 
                {
                    var tc = tabControl.TabPages[index];
                    if (tc.Name == "tabPageHelp")
                    { 
                        tc.Dispose(); 
                    }
                }
            }
       }
        public void webBrowserInfo_setup()
        {
            // for update browser version, see WBEmulator class

            webBrowserAdd.ScriptErrorsSuppressed = true;
            webBrowserAdd.ObjectForScripting = new WebBrowserFunc();

            webBrowserAdd.Parent = this;
            webBrowserAdd.Location = PointUtil.Add_X(webBrowserAdd.Location, panel1.Location.X);
            webBrowserAdd.BringToFront();


            pictureBoxClose.Parent = this;
            pictureBoxClose.Location = PointUtil.Add_X(pictureBoxClose.Location, panel1.Location.X);
            pictureBoxClose.BringToFront();

            pictureBoxReload.Parent = this;
            pictureBoxReload.Location =  PointUtil.Add_X(pictureBoxReload.Location, panel1.Location.X);
            pictureBoxReload.BringToFront();
            

            //1. serial があるか?
            if (!string.IsNullOrEmpty(RegistryWork.Get_serial()))
            {
                //最後のrunningの日付と今の日付を比較する。
                var saveday = RegistryWork.Get_running_timestamp().Day;
                var nowday = DateTime.Now.Day;
                if (saveday==nowday) //同じ場合は、消す
                {
                    pictureBoxCloseSub();
                }
            }

            //if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            if (!IsInternetAvailable())
            {
                pictureBoxCloseSub();
            }

        }
        [System.Runtime.InteropServices.DllImport("wininet.dll")]
        extern static bool InternetGetConnectedState(out int lpdwFlags, int dwReserved);

        bool IsInternetAvailable() //https://dobon.net/vb/dotnet/internet/detectinternetconnect.html
        {
            int flags;
            if (InternetGetConnectedState(out flags, 0))
                return true;//Console.WriteLine("インターネットに接続されています。");
            else
                return false;//Console.WriteLine("インターネットに接続されていません。");
        }


        public void webBrowserInfo_setup2()
        {
            var url = G.web_info;

            if (!string.IsNullOrEmpty(url))
            {
                try {
                    webBrowserAdd.Url = new Uri(url);
                } catch { }
            }

            WebAdCheckControl.m_req = true;
        }
        private void button1_Click(object sender,EventArgs e)
        {
            //string js = "alert('Click!')";
            //var js = "amzn_assoc_ad_type =\"responsive_search_widget\"; amzn_assoc_tracking_id =\"programanic-22\"; amzn_assoc_marketplace =\"amazon\"; amzn_assoc_region =\"JP\"; amzn_assoc_placement =\"\"; amzn_assoc_search_type = \"search_widget\";amzn_assoc_width =\"auto\"; amzn_assoc_height =\"auto\"; amzn_assoc_default_search_category =\"\"; amzn_assoc_default_search_key =\"\";amzn_assoc_theme =\"light\"; amzn_assoc_bg_color =\"FFFFFF\"; ";
            //webBrowser1.Url = new Uri("javascript:" + Uri.EscapeDataString(js) + ";");
        }

        private void DisplayHtml(string html)
        {
            if (webBrowser1==null) return;

            try
            {
                webBrowser1.Url = null;
                webBrowser1.Navigate("about:blank");
                if (webBrowser1.Document != null)
                {
                    webBrowser1.Document.Write(string.Empty);
                }
                webBrowser1.DocumentText = html;
                //webBrowser1.Refresh();
            }
            catch (Exception e)
            {
                G.NoticeToUser_warning("help web error : " + e.Message);
            } // do nothing with this
        }

        private void trackBarZoom_Scroll(object sender,EventArgs e)
        {
            zoomTextBox.Text = trackBarZoom.Value.ToString();
        }
        private void labelZoom115_Click(object sender, EventArgs e)
        {
            zoomTextBox.Text = "115";
        }
        private void labelZoom115_DoubleClick(object sender, EventArgs e)
        {
            zoomTextBox.Text = "2";
            zoomTextBox.Text = "115";
        }
        private void labelZoom100_Click(object sender,EventArgs e)
        {
            zoomTextBox.Text = "100";
        }
        private void labelZoom100_DoubleClick(object sender, EventArgs e)
        {
            zoomTextBox.Text = "2";
            zoomTextBox.Text = "100";
        }
        private void labelZoom85_Click(object sender,EventArgs e)
        {
            zoomTextBox.Text = "85";
        }
        private void labelZoom85_DoubleClick(object sender, EventArgs e)
        {
            zoomTextBox.Text = "2";
            zoomTextBox.Text = "85";
        }
        private void labelZoom75_Click(object sender,EventArgs e)
        {
            zoomTextBox.Text = "75";
        }
        private void labelZoom75_DoubleClick(object sender, EventArgs e)
        {
            zoomTextBox.Text = "2";
            zoomTextBox.Text = "75";
        }
        private void labelZoom60_Click(object sender,EventArgs e)
        {
            zoomTextBox.Text = "60";
            //m_lvcc.mode = "60";
            //m_lvcc.Run();
        }
        private void labelZoom60_DoubleClick(object sender, EventArgs e)
        {
            zoomTextBox.Text = "2";
            zoomTextBox.Text = "60";
        }
        private void labelZoom40_Click(object sender,EventArgs e)
        {
            zoomTextBox.Text = "40";
        }
        private void labelZoom40_DoubleClick(object sender, EventArgs e)
        {
            zoomTextBox.Text = "2";
            zoomTextBox.Text = "40";
        }

        private void labelZoom25_Click(object sender,EventArgs e)
        {
            zoomTextBox.Text = "25";
        }

        Color m_hilight_zoom_on  = System.Drawing.Color.DarkBlue;
        Color m_hilight_zoom_off = System.Drawing.Color.Gray;
        private void labelZoomCheck()
        {
            Action<Label,double> check = (l,s) => {
                if ( Math.Floor( G.scale_percent ) == s)
                {
                    l.BackColor = m_hilight_zoom_on;
                }
                else
                {
                    l.BackColor = m_hilight_zoom_off;
                }            
            };
            check(labelZoom115,115);
            check(labelZoom100,100);
            check(labelZoom85,85);
            check(labelZoom75,75);
            check(labelZoom60,60);
            check(labelZoom40,40);
            check(labelZoom25,25);
        }

        private void tabPageContents_Click(object sender,EventArgs e)
        {

        }

#region tab contents
        private void cb_statecmt_CheckedChanged(object sender,EventArgs e)
        {
            _cb_contents_changed(sender,e);
        }

        private void cb_thumbnail_CheckedChanged(object sender,EventArgs e)
        {
            _cb_contents_changed(sender,e);
        }

        private void cb_contents_CheckedChanged(object sender,EventArgs e)
        {
            _cb_contents_changed(sender,e);
        }

        private void _cb_contents_changed(object sender,EventArgs e)
        {
            G.use_statecmt = cb_statecmt.Checked;
            G.use_thumbnail = cb_thumbnail.Checked;
            G.use_contents = cb_contents.Checked;


            G.req_redraw_force();
            //AppUpdate.mouse_update(MouseEventId.ABORT);

            //G.req_redraw();
        }
#endregion

        private void tabControl1_Selected(object sender,TabControlEventArgs e)
        {
            if (tabControl.SelectedTab.Name == "tabPageContents")
            {
                cb_statecmt.Checked = G.use_statecmt;
                cb_thumbnail.Checked = G.use_thumbnail;
                cb_contents.Checked = G.use_contents;
            }
        }

        private void exitToolStripMenuItem_Click(object sender,EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender,DataGridViewCellEventArgs e)
        {
            var dg = dataGridViewLineColor;
            if (e.ColumnIndex == 1)
            {
                try {
                    colorDialog1.Color = ColorUtil.FromRRGGBB(dg[1,e.RowIndex].Value.ToString());
                } catch { }
                if (colorDialog1.ShowDialog() == DialogResult.OK)
                {
                    var col = colorDialog1.Color;
                    try {
                        var cell  = dg[1,e.RowIndex];
                        cell.Value = ColorUtil.ToRRGGBB(col);
                        var cell2 =  dg[2,e.RowIndex];
                        cell2.Style.BackColor = col;
                        cell2.Style.ForeColor = col;
                        cell2.Style.SelectionBackColor =col;
                        cell2.Style.SelectionForeColor =col;
                    } catch { }
                }
            }
        }

        private void tabPageLine_Click(object sender,EventArgs e)
        {

        }

        private void buttonLineSave_Click(object sender,EventArgs e)
        {
            G.line_color.Read_from_LineColorTab();
            G.req_redraw_force();
        }

        private void buttonLineLoad_Click(object sender,EventArgs e)
        {
            G.line_color.Write_to_LineColorTab();
            G.req_redraw_force();
        }

        private void buttonLineReset_Click(object sender,EventArgs e)
        {
            G.line_color.Reset();
            G.line_color.Write_to_LineColorTab();
            G.req_redraw_force();
        }

        private void newStripMenuItem1_Click(object sender,EventArgs e)
        {
           //if (G.JX) return;

#if obs
            saveFileDialog1.CheckPathExists = true;
            saveFileDialog1.FileName = "StateControl.xlsx";
            saveFileDialog1.Filter = "Excel Files(*.xlsx)|*.xlsx|All Files(*.*)|*.*";
            var ret = saveFileDialog1.ShowDialog(this);
            if (ret!= DialogResult.OK) {
                G.NoticeToUser_warning("Save Dialog canceled.");
                return;
            }
            var filename = saveFileDialog1.FileName;
            var dir      = Path.GetDirectoryName(filename);
            var fullpath = Path.GetFullPath(filename);

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
                G.NoticeToUser(string.Format("\"{0|\" directory was created.",dir));
            }
            try {
                var srcfilebin = Properties.Resources.StateControl;
                File.WriteAllBytes(fullpath,srcfilebin);
                G.NoticeToUser(string.Format("\"{0}\" was created.",filename));

                G.load_file = fullpath;
                Flow.main_flow();
            }
            catch
            {
                G.NoticeToUser_warning(string.Format("\"{0}\" was not created.",filename));
            }
#else
            //{
            //    var p = System.Diagnostics.Process.GetCurrentProcess();
            //    var cmd = p.MainModule.FileName;
            //    ExecUtil.execute_wo_args(cmd,Path.GetDirectoryName(G.load_file));
            //}

            StartUtil.OpenNewOrLoad();
#endif
        }

        private void execCommandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dlg = new _5000_ViewForm.dialog.ExternalCommendForm();
            dlg.ShowDialog(G.view_form);
            //dlg.Location = G.view_form.PointToClient(Cursor.Position);
            //dlg.Location = Cursor.Position;
        }

        //private void execSavedCommandToolStripMenuItem_Click(object sender,EventArgs e)
        //{

        //    G.NoticeToUser_warning("Faild to execute !!!!! NA now.");
        //    //if (string.IsNullOrEmpty(G.external_command)) {
        //    //    G.NoticeToUser_warning("Faild to execute because of null.");
        //    //    return;
        //    //}
        //    //ExecUtil.execute(G.external_command,Path.GetDirectoryName(G.load_file));
        //}

        private void groupBox1_Enter(object sender,EventArgs e)
        {

        }

        private void textBoxTabState_TextChanged(object sender,EventArgs e)
        {

        }

        private void textBoxTabState_DoubleClick(object sender,EventArgs e)
        {
            G.log += "textBoxTabState_DoubleClick" + "\n";
        }

        private void tabPageContents_Enter(object sender,EventArgs e)
        {
            //textBoxTabState.Enabled = textBoxTabCmt.Enabled = textBoxTabNext.Enabled = textBoxTabBranch.Enabled = true;
        }

        private void tabPageContents_Leave(object sender,EventArgs e)
        {
            //textBoxTabState.Enabled = textBoxTabCmt.Enabled = textBoxTabNext.Enabled = textBoxTabBranch.Enabled = false;
        }

        private void tabPageContents_MouseEnter(object sender,EventArgs e)
        {
            //textBoxTabState.Enabled = textBoxTabCmt.Enabled = textBoxTabNext.Enabled = textBoxTabBranch.Enabled = true;
        }

        private void tabPageContents_MouseLeave(object sender,EventArgs e)
        {
            //textBoxTabState.Enabled = textBoxTabCmt.Enabled = textBoxTabNext.Enabled = textBoxTabBranch.Enabled = false;
        }

        private void labelTabState_DoubleClick(object sender,EventArgs e)
        {
            //textBoxTabState.Enabled = true;
        }

        private void labelTabCmt_Click(object sender,EventArgs e)
        {
        }

        private void labelTabCmt_DoubleClick(object sender,EventArgs e)
        {
            //textBoxTabCmt.Enabled = true;
        }

        private void labelTabThumb_DoubleClick(object sender,EventArgs e)
        {

        }

        private void labelTabNext_DoubleClick(object sender,EventArgs e)
        {
            //textBoxTabNext.Enabled = true;

        }

        private void labelTabBranch_DoubleClick(object sender,EventArgs e)
        {
            //textBoxTabBranch.Enabled = true;
        }

        private void textBoxTabState_KeyDown(object sender,KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Escape) textBoxTabState.Enabled = false;
            //if (e.KeyCode == Keys.Enter)  textBoxTabState.Enabled = false;
        }
        private void textBoxTabState_Leave(object sender,EventArgs e)
        {
            //textBoxTabState.Enabled = false;
        }

        private void textBoxTabCmt_KeyDown(object sender,KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Escape) textBoxTabCmt.Enabled = false;
            //if (e.KeyCode == Keys.Enter)  textBoxTabCmt.Enabled = false;
        }

        private void ViewForm_KeyDown(object sender, KeyEventArgs e)
        {
            //Debug
            //if (e.KeyCode == Keys.D)
            //{
            //    G.debug_form.Visible = !G.debug_form.Visible;
            //}
            //G.NoticeToUser("ViewForm_KeyDown = " + e.KeyCode);

            KeyProc.m_keycode_from_viewform = e.KeyCode;
        }

        private void debugFormOnOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Debug
            G.debug_form.Visible = !G.debug_form.Visible;
        }
        private void focusTrackFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //G.focustrack_form.Visible = !G.focustrack_form.Visible;
            G.m_focus_track_panel.open_or_close();
        }
        
        private void historyRecordPanelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            G.m_history_record_panel.open_or_close();
        }

        private void buttonSaveAndConvert_Click_1(object sender, EventArgs e)
        {
            SaveAndRun(false);
        }

        private void setSourceEditorToolStripMenuItem_Click(object sender,EventArgs e)
        {
            //var dlg = new stateview._5000_ViewForm.dialog.SetSourceEditorFormcs();
            var dlg = new stateview._5000_ViewForm.dialog.SetSourceEditorForm2();
            if (dlg.ShowDialog(G.view_form) == DialogResult.OK && !string.IsNullOrEmpty(G.source_editor_set))
            {
                MessageBox.Show("設定名の記録には、保存を実行する必要があります。","Notice", MessageBoxButtons.OK);
            }
        }

        private void setUserCustomButtonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dlg = new stateview._5000_ViewForm.dialog.SetUserButtonForm();
            dlg.ShowDialog(G.view_form);
        }


        private void ButtonOpenCreatedSource_Click(object sender,EventArgs e)
        {
            OpenEditorWithCreateFile();

        }

        public void OpenEditorWithCreateFile()
        {
            var file = Converter.GetGeneratedSourceFileName();
            //var file = SettingIniUtil.GetGeneratedSource();
            //if (string.IsNullOrEmpty(file))
            //{
            //    file = TemplateUtil.GetCreatedFile();
            //}

            int? linep = null;
            {
                var str = RegexUtil.Get1stMatch(@"[0-9]+", label_Linenum.Text);
                if (!string.IsNullOrEmpty(str))
                {
                    var i = ParseUtil.ParseInt(str);
                    if (i >= 1)
                    {
                        linep = i;
                    }
                }
            }

            if (!string.IsNullOrEmpty(file))
            {

                //ExecEditorUtil.Exec(file, linep);
                OpenEditorUtil.OpenEditor(linep);
            }
            else
            {
                G.NoticeToUser_warning(G.Localize("w_generatedfileismissing")/* "generated file is missing."*/);
            }
        }

        private void buttonOpenImplemetingSource_Click(object sender, EventArgs e)
        {
            var file = SettingIniUtil.GetSourceForImplementing();
            if (!string.IsNullOrEmpty(file))
            {
                //ExecEditorUtil.Exec(file);
                OpenEditorUtil.OpenEditor(file);
            }
            else
            {
                G.NoticeToUser_warning(G.Localize("w_implementationsourcefileinotdefine")/* "Implemetation source file is not defined."*/);
            }
            //G.macro_ini.ReadMacroIni();
        }
        private void buttonOpenMacro_Click(object sender, EventArgs e)
        {
            var file = SettingIniUtil.GetMacroIni();
            if (!string.IsNullOrEmpty(file))
            {
                ExecEditorUtil.Exec(file);
            }
            else
            {
                G.NoticeToUser_warning(G.Localize("w_macrofilenotdefined")/* "Macro file is not defined."*/);
            }
        }
        public void buttonOpenExcelFolder_Click(object sender, EventArgs e)
        {
            ExecUtil.execute_start(G.load_file_dir,G.load_file_dir);
        }

        public void buttonOpenSourceFolder_Click(object sender, EventArgs e)
        {
            var gendir = SettingIniUtil.GetGenDir();
            if (string.IsNullOrEmpty(gendir)) {
                G.NoticeToUser_warning(G.Localize("w_sourcefoldercannotfound")/* "Source folder cannot be found."*/);
                return;
            }
            ExecUtil.execute_start(gendir,G.load_file_dir);

        }

        private void setLabelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dlg =  new _5000_ViewForm.dialog.SetLabelForm();
            dlg.ShowDialog();
        }

        private void editTemplateSourceToolStripMenuItem_Click(object sender,EventArgs e)
        {
            try {
                var text = G.excel_convertsettings.m_template_src;
                var newtext = edit_convertsettings_text(G.sheettempsrc,text);
                if (text != newtext)
                {
                    G.NoticeToUser("Updated template-src.");
                    G.excel_convertsettings.m_template_src = newtext;
                    if (G.psgg_file_w_data)
                    {
                        FileDbUtil.WriteTemplateSource();   //新PSGGファイル
                    }
                    else
                    { 
                        ExcelSaveOneSheet.WriteTemplateSource(); //従来式
                    }
                }
                else
                {
                    G.NoticeToUser( "Not updated because of nothing changes.");
                }
            } catch (SystemException ev) {
                G.NoticeToUser_warning(G.Localize("w_faildtoupdatetemplatesource")/* "Faild to update template-source. " */+ ev.Message  );
            }
            Converter.Prepare(); //コンバーター再準備
        }
        private void editTemplateFunctionToolStripMenuItem_Click(object sender,EventArgs e)
        {
            edit_functemplate();
        }

        private void edit_functemplate()
        {
            try
            {
                var _text = G.excel_convertsettings.m_template_func;

                var _newlinechar = StringUtil.FindNewLineChar(_text);
                var text = StringUtil.ConvertNewLineChar(_text,Environment.NewLine);

                var newtext = edit_convertsettings_text(G.sheettempfunc, text);
                if (text != newtext)
                {
                    var _newtext = StringUtil.ConvertNewLineChar(newtext,_newlinechar);

                    G.NoticeToUser("Updated template-func.");
                    G.excel_convertsettings.m_template_func = _newtext;

                    if (G.psgg_file_w_data)
                    {
                        FileDbUtil.WriteTemplateFunction();
                    }
                    else //従来式
                    { 
                        ExcelSaveOneSheet.WriteTemplateFunction();
                    }
                }
                else
                {
                    G.NoticeToUser("Not updated because of nothing changes.");
                }
            }
            catch (SystemException ev)
            {
                G.NoticeToUser_warning(G.Localize("w_faildtoupdatetempfunc") /* "Faild to update template-func. "*/ + ev.Message);
            }
            Converter.Prepare(); //コンバーター再準備
        }

        private void editSettinginiToolStripMenuItem_Click(object sender,EventArgs e)
        {
            try {
                var _text = G.excel_convertsettings.m_setting_ini;

                var _newlinechar = StringUtil.FindNewLineChar(_text);
                var text = StringUtil.ConvertNewLineChar(_text,Environment.NewLine);

                var newtext = edit_convertsettings_text(G.sheetsetting,text);
                if (text!=newtext)
                {
                    var _newtext = StringUtil.ConvertNewLineChar(newtext,_newlinechar);

                    G.NoticeToUser("Updated setting.ini." );
                    G.excel_convertsettings.m_setting_ini = _newtext;
                    if (G.psgg_file_w_data)
                    {
                        FileDbUtil.WriteSettings(); //新PSGGファイル
                    }
                    else
                    { 
                        ExcelSaveOneSheet.WriteSettings(); //従来式
                    }
                    MessageBox.Show(G.Localize("cnff_affect_after_restart")/*"Please restart StateGo to affect the setteings."*/);
                    StartUtil.OpenNewOrLoad();
                    Environment.Exit(0);
                }
                else
                {
                    G.NoticeToUser("Not updated because of nothing changes.");
                }
            } catch (SystemException ev) {
                G.NoticeToUser_warning(G.Localize("w_failedtoupdatesettingini") /* "Faild to update setting.ini. " */ + ev.Message  );
            }
        }

        private void editHelpiniToolStripMenuItem_Click(object sender,EventArgs e)
        {
            try {
                var text = G.excel_convertsettings.m_help_ini;
                var newtext = edit_convertsettings_text(G.sheethelp,text);
                if (text != newtext)
                {
                    G.NoticeToUser( "Updated setting.ini.");
                    G.excel_convertsettings.m_help_ini = newtext;
                    if (G.psgg_file_w_data)
                    {
                        FileDbUtil.WriteHelp();  //新PSGGファイル
                    }
                    else 
                    { 
                        ExcelSaveOneSheet.WriteHelp(); //従来式
                    }
                }
                else
                {
                    G.NoticeToUser("Not updated because of nothing changes.");
                }
            } catch (SystemException ev)
            {
                G.NoticeToUser_warning(G.Localize("w_faildtoupdatehelpini") /*"Faild to update help.ini. "*/ + ev.Message  );
            }
        }
        private void editItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (G.bDirty)
            {
                if (MessageBox.Show(G.Localize("w_save_prior_toedit")/*"You must save prior to editing items.\nDo you want to force to edit items? "*/,"CAUTION", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
            }
            var dlg = new ItemEditForm();
            dlg.ShowDialog(this);
            if (dlg.m_ask_reload)
            {
                if (G.psgg_file_w_data) //新PSGG
                {
                    if (File.Exists(G.psgg_file))
                    {
                        Flow.main_flow();
                    }
                }
                else
                {
                    if (File.Exists(G.load_file))
                    {
                        Flow.main_flow();
                    }
                }
            }
        }

        private string edit_convertsettings_text(string sheetname, string text)
        {
			var filename        = "~viewform_" + sheetname + ".txt";
			var dir             = Path.GetTempPath();
			var fullpath        = Path.Combine(dir,filename);

            if (G.psgg_file_w_data) //新方式
            {
                dir = FileDbUtil.GetTempPath();
                fullpath = Path.Combine(dir,filename);
            }

            File.WriteAllText(fullpath ,text,Encoding.UTF8);

            var cmdline = string.Empty;
#if obs
            if (string.IsNullOrEmpty(G.external_source_editor))
            {
				cmdline = FindWindowsAppUtil.FindAssociatedCommand(fullpath);
				cmdline = cmdline.Replace("%1", filename);
            }
			else
			{
				if (G.external_source_editor.Contains("%1"))
				{
					cmdline = G.external_source_editor.Replace("%1", filename);
				}
				else
				{
					cmdline = string.Format(" \"{0}\" {1} ", G.external_source_editor, filename);
				}
			}
#else
            cmdline = FindWindowsAppUtil.FindAssociatedCommand(fullpath);
            cmdline = cmdline.Replace("%1", filename);
#endif

            ExecUtil.execute_and_wait(cmdline, dir);

            if (MessageBox.Show(G.Localize("w_checkupdate"),"Confirmation", MessageBoxButtons.YesNo ) == DialogResult.Yes)
            {
                return File.ReadAllText(fullpath, Encoding.UTF8);
            }
            return text;
        }



        private void jPToolStripMenuItem_Click(object sender,EventArgs e)
        {
            G.system_lang = "jpn";
            SysLangWork.ChangeSysLang();
            WordStorage.Res.ChangeAll(this,G.system_lang);
            WordStorage.Res.ChangeAll(this.helpToolStripMenuItem,G.system_lang);
            G.find_form.ChangeText();

            this.Hide();  //一部反映されないための措置
            this.Show();
        }

        private void eNToolStripMenuItem_Click(object sender,EventArgs e)
        {
            G.system_lang = "en";
            SysLangWork.ChangeSysLang();
            WordStorage.Res.ChangeAll(this,G.system_lang);
            WordStorage.Res.ChangeAll(this.helpToolStripMenuItem,G.system_lang);
            G.find_form.ChangeText();

            this.Hide();  //一部反映されないための措置
            this.Show();
        }

        #region フローカラーローカライズ
        public void change_flowColorLang()
        {
            dataGridViewLineColor.Columns["Regex"].HeaderText = G.Localize("pctm_patn");
            dataGridViewLineColor.Columns["Color"].HeaderText = G.Localize("pctm_color");
        }
        #endregion

        private void pictureBoxClose_DoubleClick(object sender,EventArgs e)
        {
            pictureBoxCloseSub();
        }
        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            pictureBoxCloseSub();
        }

        private void pictureBoxCloseSub()
        {
            WebAdCheckControl.m_done = true;

            pictureBoxClose.Hide();
            pictureBoxReload.Hide();
            webBrowserAdd.Hide();
            webBrowserAdd.Visible = false;
            webBrowserAdd.Stop();
            webBrowserAdd.Dispose(); //完全に削除 ※Stopをしても動いていたから。
            webBrowserAdd = null;
        }

        private void pictureBoxReload_Click(object sender,EventArgs e)
        {
            var url = G.web_info;

            if (!string.IsNullOrEmpty(url))
            {
                try {
                    webBrowserAdd.Url = new Uri(url);
                } catch { }
            }

            webBrowserAdd.Refresh();
        }

        private void webBrowserAdd_DocumentCompleted(object sender,WebBrowserDocumentCompletedEventArgs e)
        {
            this.panel1.Focus();
        }

        private void checkBoxDeleteThis_CheckedChanged(object sender,EventArgs e)
        {
            G.option_delete_thisstring = this.checkBoxDeleteThis.Checked;
            G.req_redraw_force();
        }

        private void checkBoxDeleteBr_CheckedChanged(object sender,EventArgs e)
        {
            G.option_delete_br_string = this.checkBoxDeleteBr.Checked;
            G.req_redraw_force();
        }

        private void checkBoxDeleteBracket_CheckedChanged(object sender,EventArgs e)
        {
            G.option_delete_bracket_string = this.checkBoxDelBracket.Checked;
            G.req_redraw_force();
        }

        private void checkBoxStateS__CheckedChanged(object sender,EventArgs e)
        {
            G.option_delete_s_state_string = this.checkBoxStateS_.Checked;
            G.req_redraw_force();
        }

        private void checkBoxDeleteBase_CheckedChanged(object sender, EventArgs e)
        {
            G.option_omit_basestate_string = this.checkBoxDeleteBase.Checked;
            G.req_redraw_force();
        }
        private void checkBoxHideBaseContents_CheckedChanged(object sender, EventArgs e)
        {
            G.option_hide_basestate_contents = this.checkBoxHideBaseContents.Checked;
            G.req_redraw_force();
        }
        private void checkBoxHideBranchCmt_CheckedChanged(object sender, EventArgs e)
        {
            G.option_hide_branchcmt_onbranchbox = this.checkBoxHideBranchCmt.Checked;
            G.req_redraw_force();
        }

#region panel tab contents or thumbnail
        Color m_labelTabHilight = System.Drawing.Color.Black;
        Color m_labelTabNohilight = System.Drawing.Color.Gray;
        private void labelTabThumb_Click(object sender,EventArgs e)
        {
            //labelTabContents.ForeColor = m_labelTabNohilight;
            //labelTabThumb.ForeColor    = m_labelTabHilight;
            //textBoxTabContents.Hide();
        }
        private void labelTabContents_Click(object sender,EventArgs e)
        {
            //labelTabContents.ForeColor = m_labelTabHilight;
            //labelTabThumb.ForeColor    = m_labelTabNohilight;
            //textBoxTabContents.Show();
        }
#endregion

        private void buttonExtra_Click(object sender,EventArgs e)
        {
            ExecUtil.execute_and_wait(G.but_extra_cmd,G.load_file_dir);
        }

        private void infoWebToolStripMenuItem_Click(object sender,EventArgs e)
        {
            var msg = G.web_js_disable ? "次回起動よりノーマル版の情報ボックスを表示" :"次回起動よりテキスト版の情報ボックスを表示";
            if (MessageBox.Show(msg,"Confirmation",MessageBoxButtons.OKCancel)== DialogResult.OK)
            {
                G.web_js_disable = !G.web_js_disable;
                //SaveUserProfile.Save();

                MessageBox.Show(msg);
            }
        }

#region Fillter
        private void labelFillter_Click(object sender,EventArgs e)
        {
            var dlg = new _5000_ViewForm.FillterForm();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                FilleterUpdate();
                G.req_redraw();
            }
        }
        private void FilleterUpdate()
        {
            var text = "Fillter";
            if (!string.IsNullOrEmpty(G.fillter_regextext) || G.max_num_of_states!=int.MaxValue)
            {
                text += " : ";
                if (G.max_num_of_states!=int.MaxValue)
                {
                    text += "max=" + G.max_num_of_states + " ";
                }
                if (!string.IsNullOrEmpty(G.fillter_regextext))
                {
                    text+= " regex=\"" +  G.fillter_regextext +"\"";
                }
            }
            labelFillter.Text = text;
        }
#endregion

        //private void backToolStripMenuItem_Click(object sender,EventArgs e)
        //{
        //    History.Back();
        //}

        //private void forwardToolStripMenuItem_Click(object sender,EventArgs e)
        //{
        //    History.Forward();
        //}

        private void historyToolStripMenuItem_Click(object sender,EventArgs e)
        {
            History.Back();
            G.req_redraw_force();

        }

        private void historyForwardToolStripMenuItem_Click(object sender,EventArgs e)
        {
            History.Forward();
            G.req_redraw_force();
        }

        private void comboBoxFont_SelectedIndexChanged(object sender,EventArgs e)
        {
            var fontname = comboBoxFont.Text;
            if(Array.FindIndex(FontFamily.Families,i => i.GetName(0) == fontname) >= 0)
            {
                G.font_name = fontname;
                G.req_redraw();
            }
        }

        #region フォントサイズ
        private void textBoxFontSize_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                changefontsize(textBoxFontSize);
            }
        }
        private void textBoxFontSize_Leave(object sender, EventArgs e)
        {
            changefontsize(textBoxFontSize);
        }
        private void changefontsize(TextBox textbox)
        {
            var fontnum = ParseUtil.ParseIntExtract(textbox.Text, 0, 100);
            if (fontnum >= 0)
            {
                textbox.Text = fontnum.ToString();
                var fontsize = MathX.Clamp(fontnum, 5, 32);
                if (textbox.Name == "textBoxFontSize")
                {
                    G.font_size = fontsize;
                }
                else if (textbox.Name == "textBoxCommentFontSize")
                {
                    G.comment_font_size = fontsize;
                }
                else if (textbox.Name == "textBoxContentFontSize")
                {
                    G.contents_font_size = fontsize;
                }
                G.req_redraw();
            }
        }
        #endregion

        #region ステート幅変更
        private void textBoxStateWidth_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                change_StateWidth();
            }
        }
        private void textBoxStateWidth_Leave(object sender, EventArgs e)
        {
            change_StateWidth();
        }
        private void change_StateWidth()
        {
            var num = ParseUtil.ParseIntExtract(textBoxStateWidth.Text,0,1000);
            if (num>=0)
            {
                textBoxStateWidth.Text = num.ToString();
                G.state_width = MathX.Clamp(num,50,500);
                G.req_redraw();
            }
        }
        #endregion

        #region ステート高さ変更
        private void textBoxStateHeight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                change_StateHeight();
            }
        }

        private void textBoxStateHeight_Leave(object sender, EventArgs e)
        {
            change_StateHeight();
        }
        private void change_StateHeight()
        {
            var num = ParseUtil.ParseIntExtract(textBoxStateHeight.Text,0,1000);
            if (num>=0)
            {
                textBoxStateHeight.Text = num.ToString();
                G.state_height = MathX.Clamp(num, 1, 100);
                G.req_redraw();
            }
        }
        #endregion

        #region コメント高さ変更
        private void textBoxCmtBlkHeight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                change_CmtBlkHeight();
            }
        }

        private void textBoxCmtBlkHeight_Leave(object sender, EventArgs e)
        {
            change_CmtBlkHeight();
        }
        private void change_CmtBlkHeight()
        {
            var num = ParseUtil.ParseIntExtract(textBoxCmtBlkHeight.Text,0,1000);
            if (num>=0)
            {
                textBoxCmtBlkHeight.Text = num.ToString();
                G.comment_block_height = MathX.Clamp(num, 1, 200);
                G.req_redraw();
            }
        }
        #endregion

        #region コンテンツ最大高さ変更
        private void textBoxCntMaxHeight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                change_CntMaxHeight();
            }
        }

        private void textBoxCntMaxHeight_Leave(object sender, EventArgs e)
        {
            change_CntMaxHeight();
        }

        private void change_CntMaxHeight()
        {
            var num = ParseUtil.ParseIntExtract(textBoxCntMaxHeight.Text,0,1000);
            if (num>=0)
            {
                textBoxCntMaxHeight.Text = num.ToString();
                G.content_max_height = MathX.Clamp(num,1,1000);
                G.req_redraw();
            }
        }
        #endregion

        #region 短いステート時の幅変更
        private void textBoxShortWidth_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                change_ShortWidth();
            }
        }

        private void textBoxShortWidth_Leave(object sender, EventArgs e)
        {
            change_ShortWidth();
        }
        private void change_ShortWidth()
        {
            var num = ParseUtil.ParseIntExtract(textBoxShortWidth.Text,0,1000);
            if (num>=0)
            {
                textBoxShortWidth.Text = num.ToString();
                G.state_short_width = MathX.Clamp(num, 1, 200);
                G.req_redraw();
            }
        }
        #endregion

        #region 短いステート時の高さ変更
        private void textBoxShortHeight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                textBoxShortHeight_TextChanged();
            }
        }
        private void textBoxShortHeight_Leave(object sender, EventArgs e)
        {
            textBoxShortHeight_TextChanged();
        }
        private void textBoxShortHeight_TextChanged()
        {
            var num = ParseUtil.ParseIntExtract(textBoxShortHeight.Text,0,1000);
            if (num>=0)
            {
                textBoxShortHeight.Text = num.ToString();
                G.state_short_height = MathX.Clamp(num, 1, 200);
                G.req_redraw();
            }
        }
        #endregion

        #region ライン間隔
        private void textBoxLineSpace_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                change_LineSpace();
            }
        }

        private void textBoxLineSpace_Leave(object sender, EventArgs e)
        {
            change_LineSpace();
        }

        private void change_LineSpace()
        {
            var num = ParseUtil.ParseIntExtract(textBoxLineSpace.Text,-100,100,int.MinValue);
            if (num>=-100)
            {
                textBoxLineSpace.Text = num.ToString();
                G.line_space = MathX.Clamp(num, -50, 50) * (-1);
                G.req_redraw();
            }
        }
        #endregion

        private void clipboardTStripMenuItem_Click(object sender,EventArgs e)
        {
            Clipboard.SetImage(G.mainbitmap);
            MessageBox.Show(G.Localize("w_bitmapcopied"));/*"The bitmap image was copied to the clipboard."*/
        }

        private void tabControl_SelectedIndexChanged(object sender,EventArgs e)
        {
            //if (tabControl.SelectedTab!=null && tabControl.SelectedTab.Tag != null && tabControl.SelectedTab.Tag.ToString() == "help")
            //{
            //    webBrowser1.Refresh();
            //}
        }

        //private void labelwebBrowser1Reload_Click(object sender,EventArgs e)
        //{
        //    webBrowser1.Refresh();
        //}

        private void pictureBox1_Click(object sender,EventArgs e)
        {
            webBrowserHelp_setup();
        }

        private void setupCustomPanel()
        {
            var panel = this.panelTabCustom;
            label11.Parent = panel;
            comboBoxFont.Parent = panel;
            label13.Parent = panel;
            textBoxFontSize.Parent = panel;

            label8.Parent = panel;
            textBoxStateWidth.Parent = panel;

            //label12.Parent = panel;
            textBoxStateHeight.Parent = panel;

            label9.Parent = panel;
            textBoxCmtBlkHeight.Parent = panel;

            label10.Parent =  panel;
            label14.Parent = panel;
            textBoxLineSpace.Parent = panel;

            label7.Parent = panel;
            dataGridViewLineColor.Parent = panel;

            buttonLineReset.Parent = panel;
            buttonLineLoad.Parent = panel;
            buttonLineSave.Parent = panel;

            //groupBox4.Parent = panel;

        }

        public bool m_show_filltered = false;
        private void labelTemplateInSrcTab_Click(object sender,EventArgs e)
        {
            var text = m_show_filltered ? G.current_func_template : G.excel_convertsettings.m_template_func;
            var text2 = StringUtil.ConverNewLineCharForDisplay(text);

            //textBoxTabFunc.Text = ScrambleText.get( text2 );
            scintillaBoxTabFunc.Text = ScrambleText.get( text2 );

            m_show_filltered = !m_show_filltered;
        }

        private void labelSrcInSrcTab_Click(object sender,EventArgs e)
        {
            var text = G.current_func_src;
            //textBoxTabFunc.Text = ScrambleText.get( text );
            scintillaBoxTabFunc.Text = ScrambleText.get( text ); 
        }
        private void labelSrcInSrcTab_DoubleClick(object sender, EventArgs e)
        {
            OpenEditorWithCreateFile();
        }

        private void splitter1_SplitterMoved(object sender,SplitterEventArgs e)
        {
            //if (splitter1.Location.X < 272)
            //{
            //    splitter1.Location = PointUtil.Mod_X(splitter1.Location, 272);
            //}
        }

        private void panel3_SizeChanged(object sender,EventArgs e)
        {
            //G.NoticeToUser(panel3.Size.ToString()  );
            NoticeTextBox.Width = panel3.Width-10; 
            tabControl.Width    = panel3.Width-10; 

        }

        private void toolsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openCopyCollectionToolStripMenuItem_visible_check();
        }
        private void toolsToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            openCopyCollectionToolStripMenuItem_visible_check();
        }
        void openCopyCollectionToolStripMenuItem_visible_check() {
            this.openCopyCollectionToolStripMenuItem.Visible = (!string.IsNullOrEmpty(G.cc.m_copycollectionFolder_fullpath) && Directory.Exists( G.cc.m_copycollectionFolder_fullpath));
            this.createCopyCollectionFolderToolStripMenuItem.Visible = G.cc.m_read_only==true || string.IsNullOrEmpty(G.cc.m_copycollectionFolder_fullpath);
        }

        private void windowsToolStripMenuItem_Click(object sender,EventArgs e)
        {


        }

        private void windowsToolStripMenuItem_MouseHover(object sender,EventArgs e)
        {
            //this.windowsToolStripMenuItem.DropDownItems.Clear();
            //var list = CheckOpenSameDoc.Get_WindowTitles();
            //list.ForEach(i=> {
            //    this.windowsToolStripMenuItem.DropDownItems.Add(i);
            //});
        }

        private void ViewForm_Enter(object sender,EventArgs e)
        {
        }

        private void menuStrip1_Leave(object sender,EventArgs e)
        {

        }

        private void menuStrip1_Enter(object sender,EventArgs e)
        {
        }

        private void ViewForm_Activated(object sender,EventArgs e)
        {
            G.Active = true;
            UpdateWindows();

            G.option_mrb_enable = stateview.RegistryWork.Get_item_mrb_enable();
            G.option_form.set_label_enable_mbr();

            G.option_historypanel_onstart = stateview.RegistryWork.Get_item_historypanelonstart_enable();
            G.option_form.set_label_historypanelonstart();

            G.option_forceclose_ifviewchangeonly = stateview.RegistryWork.Get_item_forceclose_ifviewchangeonly();
            G.option_form.set_label_forceclose_ifviewchangeonly();
            G.option_form.set_setdefault_comment();
            G.option_form.set_jump_if_statego_exist();
            G.option_form.set_lexical_color_onoff();

            G.option_jump_if_statego_exist = stateview.RegistryWork.Get_item_jump_if_statego_exist();
            G.option_lexical_color_onoff = stateview.RegistryWork.Get_lexical_color_onoff();
        }
        private void ViewForm_Deactivate(object sender, EventArgs e)
        {
            G.Active = false;
        }

        //public void UpdateWindows_obs()
        //{
        //    this.windowsToolStripMenuItem.DropDownItems.Clear();
        //    var list = CheckOpenSameDoc.Get_WindowTitles();
        //    var dropitems = this.windowsToolStripMenuItem.DropDownItems;
        //    foreach(var p in list)
        //    {
        //        dropitems.Add(p.Value);
        //        var n = dropitems.Count - 1;
        //        var dropitem = dropitems[n];
        //        dropitem.Tag = p.Key;
        //        dropitem.Click += WindowDropitem_Click;
        //        if (p.Key == G.PROCESS_ID)
        //        {
        //            dropitem.Enabled = false;
        //        }
        //    }
        //}
        //private void WindowDropitem_Click(object sender,EventArgs e)
        //{
        //    var dropitem = (ToolStripItem)sender;
        //    var procid = (int)dropitem.Tag;

        //    var proc = System.Diagnostics.Process.GetProcessById(procid);
        //    if (proc != null)
        //    {
        //        //WindowsUtil.WakeupWindow(proc);
        //        var h = G.view_form.Handle;
        //    }
            
        //    //CheckOpenSameDoc.Set_order_focus(procid);  
             
        //}

        public List<IntPtr> m_main_panel_winkey_list  = new List<IntPtr>();
        public void UpdateWindows()
        {
            this.windowsToolStripMenuItem.DropDownItems.Clear();
            var list = CheckOpenSameDoc.Get_WindowTitles2();
            var dropitems = this.windowsToolStripMenuItem.DropDownItems;
            foreach(var p in list)
            {
                dropitems.Add(p.Value);
                var n = dropitems.Count - 1;
                var dropitem = dropitems[n];
                dropitem.Tag = p.Key;
                dropitem.Click += WindowDropitem_Click2;
                if (p.Key == G.VIEWFORM_HANDLE)
                {
                    dropitem.Enabled = false;
                }
            }

            //#region メインパネルのStateGo Window List
            //var winitems = listBoxWindows.Items;
            //winitems.Clear();
            //m_main_panel_winkey_list.Clear();
            //foreach(var p in list)
            //{
            //    var s = p.Value;
            //    var psgg = RegexUtil.Get1stMatch(@"\[.+\]",s).Trim('[',']');

            //    if (p.Key == G.VIEWFORM_HANDLE)
            //    {
            //        s = psgg + " (this window)";
            //    }
            //    else
            //    {
            //        s = psgg;
            //    }
            //    winitems.Add(s);
            //    m_main_panel_winkey_list.Add(p.Key);
            //}
            //#endregion

            //template-srcがない場合、menu を disable
            if (G.excel_convertsettings!=null) {
                this.editTemplateSourceToolStripMenuItem.Visible = !string.IsNullOrEmpty(G.excel_convertsettings.m_template_src);
            }

            //impletemnt srcがない場合、ボタンをdisable
            this.buttonOpenImpleSource.Enabled = !string.IsNullOrEmpty(SettingIniUtil.GetSourceForImplementing());

            //macro iniがない場合、ボタンをdisable
            this.buttonOpenMacro.Enabled = !string.IsNullOrEmpty(SettingIniUtil.GetMacroIni());

            //copy to clipboard更新
            G.option_form.set_labelCpy2Clipboard();

            //with confirm 更新
            G.option_form.set_labelWithConfirm();

            //ignore case 更新
            G.option_form.set_labelIgnoreCase();

            //save with excel更新
            G.option_form.set_labelSaveWithExcel();

            //use custom prefix更新
            G.option_form.set_label_custom_prefix();

            //enable mrb
            G.option_form.set_label_enable_mbr();

            // 履歴パネル
            G.option_form.set_label_historypanelonstart();

            // 閲覧変更の終了時の確認
            G.option_form.set_label_forceclose_ifviewchangeonly();

            // デフォルトコメント挿入
            G.option_form.set_setdefault_comment();

            // 既存StateGoへの自動遷移
            G.option_form.set_jump_if_statego_exist();
        }
        private void WindowDropitem_Click2(object sender,EventArgs e)
        {
            var dropitem = (ToolStripItem)sender;
            var handle= (IntPtr)dropitem.Tag;

            WindowsUtil.ActiveWindow(handle);
             
        }
        public void UpdateUserButton()
        {
            if (!string.IsNullOrEmpty(G.userbutton_title))
            {
                buttonUserButton.Tag = null;
                buttonUserButton.Text = G.userbutton_title;
            }
        }

        private void buttonUserButton_Click(object sender, EventArgs e)
        {
            ExecuteUserButtonCommand();
        }

        public void ExecuteUserButtonCommand()
        {
            if (string.IsNullOrEmpty(G.userbutton_command))
            {
                G.NoticeToUser_warning(G.Localize("w_nocommandusercustombutton")/*  "No command in the user custom button."*/);
                return;
            }
            var command = Path.Combine(G.load_file_dir, G.userbutton_command);
            if (!File.Exists(command))
            {
                G.NoticeToUser_warning(G.Localize("w_cmdinuserctmbtnnotexist")/* "The command in the user custom button does not exist." */);
                return;
            }
            try
            {
                ExecUtil.execute_start(command, Path.GetDirectoryName(command));
                G.NoticeToUser(G.Localize("w_executeuserctmcmd") /*  "Execute user custom command : "*/ + command);
            }
            catch (SystemException e2)
            {
                G.NoticeToUser_warning(G.Localize("w_cmdinuserctmbutnexception") /* "The command in the user custom button has a exception. " */ + e2.Message);
            }
        }

        private void labelCpy2Clipboard_Click(object sender, EventArgs e)
        {
            //G.option_copy_output_to_clipboard = !G.option_copy_output_to_clipboard;
            //set_labelCpy2Clipboard();
        }
        //public void set_labelCpy2Clipboard()
        //{
        //    var id = G.option_copy_output_to_clipboard ? "options_enable_clipboard"/*"enable to use clipboard"*/ : "options_disable_clipboard"/*"desable to use clipboard"*/;
        //    labelCpy2Clipboard.Text = G.Localize(id);
        //}
        private void label_ignorecase_Click(object sender, EventArgs e)
        {
            //G.option_ignore_case_of_state = !G.option_ignore_case_of_state;
            //set_labelIgnoreCase();
        }
        //public void set_labelIgnoreCase()
        //{
        //    var id = G.option_ignore_case_of_state ? "options_ignore_case"/*"ignore case of state"*/ : "options_not_ignore_case"/*"not ignore case of state"*/;
        //    label_ignorecase.Text = G.Localize(id);
        //}
        private void labelWithConfirm_Click(object sender, EventArgs e)
        {
            //※移動済み
            //G.option_convert_with_confirm = !G.option_convert_with_confirm;
            //set_labelWithConfirm();
        }
        //public void set_labelWithConfirm()
        //{
        //    //var id = G.option_convert_with_confirm ? "options_w_confirm"/*"with confirmation"*/ : "options_wo_confirm"/*"without confirmation"*/;
        //    //labelWithConfirm.Text = G.Localize(id);
        //    //G.option_form.labelWithConfirm.Text = G.Localize(id);
        //}
        private void label_savewith_excel_Click(object sender, EventArgs e)
        {
            //G.psgg_header_info_save_mode_withexcel = !G.psgg_header_info_save_mode_withexcel;
            //G.psgg_header_info_read_from_excel     =  G.psgg_header_info_save_mode_withexcel;  //連動しておく
            //set_labelSaveWithExcel();    
        }
        private void label_custom_prefix_Click(object sender, EventArgs e)
        {
            //G.option_use_custom_prefix = !G.option_use_custom_prefix;
            //set_label_custom_prefix();
        }
        //public void set_label_custom_prefix()
        //{
        //    var id = G.option_use_custom_prefix ? "options_custom_prefix_yes" : "options_custom_prefix_no";
        //    label_custom_prefix.Text = G.Localize(id);
        //}
        private void label_enable_mbr_Click(object sender, EventArgs e)
        {
            //G.option_mrb_enable = !G.option_mrb_enable;
            //stateview.RegistryWork.Set_item_mrb_enable(G.option_mrb_enable);
            //set_label_enable_mbr();

        }
        //public void set_label_enable_mbr()
        //{
        //    var id = G.option_mrb_enable ? "options_mrb_enable_yes" : "options_mrb_enable_no";
        //    label_enable_mbr.Text = G.Localize(id);
        //}
        //public void set_labelSaveWithExcel()
        //{
        //    //if (G.psgg_file_w_data)
        //    //{
        //    //    label_savewith_excel.Visible = true;
        //    //    var id = G.psgg_header_info_save_mode_withexcel ? "headerinfo_save_with_excel" : "headerinfo_save_psgg_only";
        //    //    label_savewith_excel.Text = G.Localize( id );
        //    //}
        //    //else
        //    //{
        //    //    label_savewith_excel.Visible = false;
        //    //}
        //}
        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //var dlg = new _5000_ViewForm.dialog.FindForm();
            //dlg.ShowDialog(this);
            G.find_form.combox_text.Focus();
            G.find_form.Visible = true;
        }

        private void createCloneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (G.psgg_file_w_data)
            {
            
            }
            else
            { 
                if (FileUtil.IsReadOnly(G.load_file))
                {
                    MessageBox.Show("Please remove the read only attribute from the document file.");
                    return;
                }
            }

            var dlg = new _5000_ViewForm.dialog_createclone.CreateCloneForm();
            dlg.ShowDialog(this);
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void openCustomEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dlg = new stateview._5000_ViewForm.dialog.SetSourceEditorForm2();
            dlg.ShowDialog(this);
        }

        private void openCustomItemEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Addon.OpenItemEditor();
        }

#region 
        private void labelCommentFontSize_Click(object sender, EventArgs e)
        {
            _change_fontsize_enabled(textBoxCommentFontSize, ref G.comment_font_size);
        }
        private void labelContentFontSize_Click(object sender, EventArgs e)
        {
            _change_fontsize_enabled(textBoxContentFontSize, ref G.contents_font_size);
        }
        private void _change_fontsize_enabled(TextBox sizebox, ref float size)
        {
            sizebox.Enabled = !sizebox.Enabled;
            sizebox.ReadOnly = !sizebox.Enabled;

            if (sizebox.Enabled)
            {
                size = G.font_size;
                sizebox.Text = size.ToString();
            }
            else
            {
                size = 0;
                sizebox.Text = string.Empty;
            }
        }
            #region コメントフォントサイズ変更
        private void textBoxCommentFontSize_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                change_CommentFontSize();
            }
        }
        private void textBoxCommentFontSize_Leave(object sender, EventArgs e)
        {
            change_CommentFontSize();
        }
        private void change_CommentFontSize()
        {
            _textchange_fontsize(textBoxCommentFontSize, ref G.comment_font_size);
        }
            #endregion
            #region コンテンツフォントサイズ変更
        private void textBoxContentFontSize_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                change_ContentFontSize();
            }
        }

        private void textBoxContentFontSize_Leave(object sender, EventArgs e)
        {
            change_ContentFontSize();
        }
        private void change_ContentFontSize()
        {
            _textchange_fontsize(textBoxContentFontSize, ref G.contents_font_size);
        }
            #endregion

        private void _textchange_fontsize(TextBox sizebox, ref float size)
        {
            if (sizebox.Enabled)
            { 
                var i = ParseUtil.ParseInt(sizebox.Text,0);
                if (i<=0) size = 1;
                size = i;
                sizebox.Text = i.ToString();
            }
            G.req_redraw_force();
        }

        private void label_fontsize_up_Click(object sender, EventArgs e)
        {
            _label_fontsize_updown(textBoxFontSize,true);
        }
        private void label_fontsize_down_Click(object sender, EventArgs e)
        {
            _label_fontsize_updown(textBoxFontSize,false);
        }
        private void label_comment_fontsize_up_Click(object sender, EventArgs e)
        {
            _label_fontsize_updown(textBoxCommentFontSize,true);
        }
        private void label_comment_fontsize_down_Click(object sender, EventArgs e)
        {
            _label_fontsize_updown(textBoxCommentFontSize,false);
        }
        private void contents_fontsize_up_Click(object sender, EventArgs e)
        {
            _label_fontsize_updown(textBoxContentFontSize,true);
        }
        private void contents_fontsize_down_Click(object sender, EventArgs e)
        {
            _label_fontsize_updown(textBoxContentFontSize,false);
        }

        private void _label_fontsize_updown(TextBox textbox, bool updown)
        {
            if (!textbox.Enabled) return;
            var i = ParseUtil.ParseInt(textbox.Text,-1);
            if (updown)
            {
                i++;
            }
            else
            {
                i--;
            }
            if (i <=0) i = 1;

            textbox.Text = i.ToString();
            changefontsize(textbox);
        }
#endregion

        private void checkBoxCmtHeightFixed_CheckedChanged(object sender, EventArgs e)
        {
            G.comment_block_fixed = checkBoxCmtHeightFixed.Checked;
            G.req_redraw_force();
        }

        //private LabelVerClickControl m_lvcc = new LabelVerClickControl();
        private void labelVer_Click(object sender, EventArgs e)
        {
        }

        private void labelVer_MouseDown(object sender, MouseEventArgs e)
        {
            //m_lvcc.mode = "labelVer";
            //m_lvcc.Run();
            //if (m_lvcc.m_count == m_lvcc.SCRT)
            //{
            //    labelVer.ForeColor = System.Drawing.Color.LightGray;
            //}
            //else
            //{
            //    labelVer.ForeColor = System.Drawing.Color.Black;
            //}
        }

        private void searchStateGoFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new  stateview._5000_ViewForm.dialog.SearchForm();
            form.ShowDialog(this);
        }

        private void label_Linenum_Click(object sender, EventArgs e)
        {
            var num = RegexUtil.Get1stMatch(@"[0-9]+",label_Linenum.Text);
            Clipboard.SetText(num);
            G.NoticeToUser(string.Format("Copied the line number({0}) to the clipboard.",num));
        }

        private void label_help_win_Click(object sender, EventArgs e)
        {
            HelpJumpUtil.Jump("tec_userguide","window-description",G.system_lang=="jpn");
        }

        private void label_help_notice_Click(object sender, EventArgs e)
        {
            HelpJumpUtil.Jump("tec_userguide","window-description",G.system_lang=="jpn");
        }

        private void label_help_main_panel_Click(object sender, EventArgs e)
        {
            HelpJumpUtil.Jump("tec_userguide","window-panel-main",G.system_lang=="jpn");
        }

        private void label_help_contents_panel_Click(object sender, EventArgs e)
        {
            HelpJumpUtil.Jump("tec_userguide","window-panel-contents",G.system_lang=="jpn");
        }

        private void label_help_custom_panel_Click(object sender, EventArgs e)
        {
            HelpJumpUtil.Jump("tec_userguide","window-panel-custom",G.system_lang=="jpn");
        }

        private void label_help_help_panel_Click(object sender, EventArgs e)
        {
            HelpJumpUtil.Jump("tec_userguide","window-panel-help",G.system_lang=="jpn");

        }

        private void label_help_src_panel_Click(object sender, EventArgs e)
        {
            HelpJumpUtil.Jump("tec_userguide","window-panel-src",G.system_lang=="jpn");
        }

        private void label_help_utility_Click(object sender, EventArgs e)
        {
            HelpJumpUtil.Jump("tec_userguide","window-utility",G.system_lang=="jpn");
        }

        private void label_help_top_Click(object sender, EventArgs e)
        {
            HelpJumpUtil.Jump("tec_userguide","window-top-desc",G.system_lang=="jpn");
        }

        private void NodeTreeView_DoubleClick(object sender, EventArgs e)
        {
            //if (G.vf_sc.CheckState(ViewFormStateControl.)
            if (!G.vf_sc.IsValidStateforCenterFocus()) return; //アイドル中以外できない。

            var node = NodeTreeView.SelectedNode;
            if (node == null) return;

            if (node.Tag==null) return;
            var tag = (stateview.TabNodeTree.TagData)node.Tag;
            if (tag.typ == TabNodeTree.TagData.Type.DIR)
            {
                //var group = node.Text;
                G.vf_sc.m_center_focus_group = tag.path;
            }
            else
            { 
                //G.NoticeToUser("Double clicked :" + node.Text);
                var state = node.Text;
                G.vf_sc.m_center_focus_state = state;
            }

            //var dirpath = DirPathExcelUtil.get_dirpath(state);
            //G.node_set_curdir(dirpath);

            //DrawBenri.draw_opt();
            //G.set_scalepercent_with_textbox(100);

            //{
            //    G.tabNodeTree.CreateAndSetCurrent();
            //    G.tabNodeTree.SelectState(state);                //NodeTreeView.SelectedNode = node; //同じものを再選択
            //}

            //var pos = G.node_get_pos(state); //位置

            //if (pos!=null)
            //{ 
            //    G.view_form.panel1.AutoScrollPosition = (Point)pos;
            //}


        }

        private void label_bmpos_DoubleClick(object sender, EventArgs e)
        {
            SimpleTrace.exec();//暫定実装
        }

        private void testToolStripMenuItem1_Click(object sender, EventArgs e)
        {
#if !xx
            var s = SettingIniUtil.GetKitPath();
            G.NoticeToUser(s); 
#elif dfs
            var find = FileUtil.FindFileUpperDirs(@"C:\Program Files (x86)\PSGG\starterkit2\bash","StateGo.exe","^Program Files");
            G.NoticeToUser(find);
#else
            var path = PathUtil.ExtractPathWithEnvVals(@"%ProgramFiles(x86)%\PSGG\StateGo.exe");
            var b = File.Exists(path);
            G.NoticeToUser("File Exists = " + b.ToString());
            var path2 = PathUtil.ExtractPathWithEnvVals(@"%ProgramFiles(x86)%\PSGG\" + Guid.NewGuid().ToString());
            //var path2 = PathUtil.ExtractPathWithEnvVals(@"%APPDATA%\PSGG\" + Guid.NewGuid().ToString());
            try {
                File.WriteAllText(path2, "test");
            } catch (System.Exception e2)
            {
                G.NoticeToUser("The folde cannot be written.\n" + path2);
                return;
            }
            G.NoticeToUser("The folde can be written.\n" + path2);
            File.Delete(path2);
#endif
        }

        private void labelTemplateInSrcTab_DoubleClick(object sender, EventArgs e)
        {
            edit_functemplate();
        }

        private void testWritePsggToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fd = new StateViewer_filedb.FileDb();
            fd.CreatePsgg(G.psgg_file);
        }

        private void psggUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sm = new psggVerUpdateControl();
            sm.Run();
        }

        private void fileToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            for(var i = 0; i< fileToolStripMenuItem.DropDownItems.Count; i++)
            {
                var item = fileToolStripMenuItem.DropDownItems[i];
                if (item.Name == "importExcelToolStripMenuItem")
                {
                    item.Visible = G.psgg_file_w_data && !string.IsNullOrEmpty( G.load_file) && File.Exists( G.load_file);
                    continue;
                }
                if (item.Name == "updateToolStripMenuItem")
                {
                    item.Visible = !G.psgg_file_w_data;
                }
            }

        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            G.psgg_open_upgrade = true;
            //var dlg = new _5000_ViewForm.dialog.UpgradePsggForm();
            //dlg.ShowDialog(this);
            //if (dlg.DialogResult != DialogResult.OK) return;

            //MessageBox.Show(WordStorage.Res.Get("pvdc_restart",G.system_lang ));

            //StartUtil.OpenNewOrLoad();

            //Environment.Exit(0);
        }

        private void label1_noticeClear_Click(object sender, EventArgs e)
        {
            this.NoticeTextBox.Text = string.Empty;
        }

        private void panel1_Enter(object sender, EventArgs e)
        {
            this.panel1.Focus();
        }

        private void inouttMenu_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            G.vf_sc.inoutmenu_closed();
        }

#region mark
        private void radioButton_symbol_CheckedChanged(object sender, EventArgs e)
        {
            radioButton_mark_start_co();
        }
        private void radioButton_gothic_CheckedChanged(object sender, EventArgs e)
        {
            radioButton_mark_start_co();
        }
        private void radioButton_script_CheckedChanged(object sender, EventArgs e)
        {
            radioButton_mark_start_co();
        }
        private void radioButton_jGothic_CheckedChanged(object sender, EventArgs e)
        {
            radioButton_mark_start_co();
        }
        private void radioButton_jGyosyo_CheckedChanged(object sender, EventArgs e)
        {
            radioButton_mark_start_co();
        }
        private void radioButton_noMark_CheckedChanged(object sender, EventArgs e)
        {
            radioButton_mark_start_co();
        }
        IEnumerator m_mark_changed_co = null;
        bool m_mark_change_bDisabled=false;
        private void radioButton_mark_start_co()
        {
            if (m_mark_change_bDisabled) return;
            //G.NoticeToUser("radioButton_mark_start_co");

            if (m_mark_changed_co == null)
            { 
                m_mark_changed_co = mark_changed_co();
                Coroutine.Start(m_mark_changed_co);
            }
        }
        IEnumerator mark_changed_co()
        {
            for(var i = 0; i<5; i++) //wait 0.5秒
            {
                yield return null; 
            }
            //G.NoticeToUser("radioButton_mark_start_co---DONE");

            if (this.radioButton_symbol.Checked)  G.decoimage_typ_name = "sym";
            if (this.radioButton_gothic.Checked)  G.decoimage_typ_name = "eGothic";
            if (this.radioButton_script.Checked)  G.decoimage_typ_name = "eScript";
            if (this.radioButton_jGothic.Checked) G.decoimage_typ_name = "jGothic";
            if (this.radioButton_jGyosyo.Checked) G.decoimage_typ_name = "jGyosyo";
            if (this.radioButton_noMark.Checked)  G.decoimage_typ_name = "";

            stateview.DecoImage.Reset();

            Flow.main_skip_load_flow();

            m_mark_changed_co = null;
        }
        internal void radioButton_mark_show()
        {
            m_mark_change_bDisabled = true;

            if (G.decoimage_typ_name == "sym")      this.radioButton_symbol.Checked = true;
            if (G.decoimage_typ_name == "eGothic")  this.radioButton_gothic.Checked = true;
            if (G.decoimage_typ_name == "eScript")  this.radioButton_script.Checked = true;
            if (G.decoimage_typ_name == "jGothic")  this.radioButton_jGothic.Checked = true;
            if (G.decoimage_typ_name == "jGyosyo")  this.radioButton_jGyosyo.Checked = true;
            if (G.decoimage_typ_name == ""       )  this.radioButton_noMark.Checked = true;

            m_mark_change_bDisabled = false;
        }
#endregion

        private void basicOperationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpJumpUtil.Jump("tec_basic","",G.system_lang=="jpn");
        }

        private void referenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpJumpUtil.Jump("tec_main","",G.system_lang=="jpn");
        }

        private void contactToolStripMenuItem_Click(object sender, EventArgs e)
        {
             ExecUtil.execute_start("https://www.secure-cloud.jp/sf/1551614451aYEhBbzg","");
        }

        private void hompageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (G.system_lang=="jpn")
            {
                ExecUtil.execute_start("https://statego.programanic.com/lang-j/index.html","");
            }
            else
            {
                ExecUtil.execute_start("https://statego.programanic.com/lang-e/index.html","");
            }
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var dlg = new _6990_aboutForm.AboutForm();
            dlg.ShowDialog();
        }

        private void webBrowserAdd_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //G.NoticeToUser("webBrowserAdd_PreviewKeyDown = " + e.KeyCode.ToString());
            //G.Key = e.KeyCode;
            KeyProc.m_keycode_from_viewform = e.KeyCode;
        }

        private void update_label_scale_indicator()
        {
            var bounds115 = labelZoom115.Bounds;
            var bounds100 = labelZoom100.Bounds;
            var bounds85  = labelZoom85.Bounds;
            var bounds75  = labelZoom75.Bounds;
            var bounds60  = labelZoom60.Bounds;
            var bounds40  = labelZoom40.Bounds;
            var bounds25  = labelZoom25.Bounds;

            var bounds115c = (int)RectangleUtil.Center(bounds115).X;
            var bounds100c = (int)RectangleUtil.Center(bounds100).X;
            var bounds85c  = (int)RectangleUtil.Center(bounds85).X;
            var bounds75c  = (int)RectangleUtil.Center(bounds75).X;
            var bounds60c  = (int)RectangleUtil.Center(bounds60).X;
            var bounds40c  = (int)RectangleUtil.Center(bounds40).X;
            var bounds25c  = (int)RectangleUtil.Center(bounds25).X;

            Func<
                double,/*cur_scael_percent*/
                double,/*min percent*/
                double,/*max percent*/
                int,   /*left pos*/
                int,   /*right pos*/
                int    /*output*/
                > calc_ind_x = (cur,min,max,left,right)=> {
                    if ( cur >= min && cur <=max)
                    {
                        var t = (cur - min) / (max - min);
                        var result = (int)MathX.Lerp((float)left,(float)right,(float)t);
                        return result;
                    }
                    return -1;
            };

            var rLess25 = calc_ind_x(G.scale_percent,0,25, bounds25.Left, bounds25c); 
            var r25to40 = calc_ind_x(G.scale_percent,25,40,bounds25c,bounds40c);
            var r40to60 = calc_ind_x(G.scale_percent,40,60,bounds40c,bounds60c);
            var r60to75 = calc_ind_x(G.scale_percent,60,75,bounds60c,bounds75c);
            var r75to85 = calc_ind_x(G.scale_percent,75,85,bounds75c,bounds85c);
            var r85to100 = calc_ind_x(G.scale_percent,85,100,bounds85c,bounds100c);
            var r100to115 = calc_ind_x(G.scale_percent,100,115,bounds100c,bounds115c);
            var rGthan115 = calc_ind_x(G.scale_percent,115,400,bounds115c,bounds115.Right);

            var locx = Math.Max(rLess25,r25to40);
            locx = Math.Max(locx,r40to60);
            locx = Math.Max(locx,r60to75);
            locx = Math.Max(locx,r75to85);
            locx = Math.Max(locx,r85to100);
            locx = Math.Max(locx,r100to115);
            locx = Math.Max(locx,rGthan115);

            this.label_scale_pos.Location  = PointUtil.Mod_X(this.label_scale_pos.Location,locx); 
        }

        private void label_focustrackpanel_close_Click(object sender, EventArgs e)
        {
            G.m_focus_track_panel.close();
        }

        private void groupBox_focustrackpanel_MouseHover(object sender, EventArgs e)
        {
        }

        private void groupBox_focustrackpanel_MouseCaptureChanged(object sender, EventArgs e)
        {
        }

        private void label_historyrecord_close_Click(object sender, EventArgs e)
        {
            G.m_history_record_panel.close();
        }

        private void label_historyrecord_up_Click(object sender, EventArgs e)
        {
            G.keyexec = KEYEXEC.history_forward;
        }

        private void label_historyrecord_down_Click(object sender, EventArgs e)
        {
            G.keyexec = KEYEXEC.history_back;
        }

        private void label_focustrack_up_Click(object sender, EventArgs e)
        {
            G.keyexec = KEYEXEC.focustrack_forward;
            G.vf_sc.m_track_focused_pathdir = G.node_get_cur_dirpath();
            G.vf_sc.m_track_focused_states = G.vf_sc.GetFocusingStates();
        }

        private void label_focustrack_down_Click(object sender, EventArgs e)
        {
            G.keyexec = KEYEXEC.focustrack_back;
            G.vf_sc.m_track_focused_pathdir = G.node_get_cur_dirpath();
            G.vf_sc.m_track_focused_states = G.vf_sc.GetFocusingStates();
        }

        private void label_home_Click(object sender, EventArgs e)
        {
            KeyProc.set_nearest_state_and_focus();
            G.vf_sc.m_center_focus_wo_cursor = true;
        }

        private void label_end_Click(object sender, EventArgs e)
        {
            KeyProc.set_farest_state_and_focus();
            G.vf_sc.m_center_focus_wo_cursor = true;
        }

#region 変更状況
        private void label_view_changed_MouseHover(object sender, EventArgs e)
        {
            textbox_change_detail(label_view_changed.Text, true);
        }
        private void label_view_changed_MouseEnter(object sender, EventArgs e)
        {
            textbox_change_detail(label_view_changed.Text, true);
        }
        private void label_view_changed_MouseLeave(object sender, EventArgs e)
        {
            textbox_change_detail(label_view_changed.Text, false);
        }

        private void label_pos_changed_MouseEnter(object sender, EventArgs e)
        {
            textbox_change_detail(label_pos_changed.Text, true);
        }

        private void label_pos_changed_MouseHover(object sender, EventArgs e)
        {
            textbox_change_detail(label_pos_changed.Text, true);
        }

        private void label_pos_changed_MouseLeave(object sender, EventArgs e)
        {
            textbox_change_detail(label_pos_changed.Text, false);
        }

        private void label_changedValue_MouseEnter(object sender, EventArgs e)
        {
            textbox_change_detail(label_changedValue.Text, true);
        }

        private void label_changedValue_MouseHover(object sender, EventArgs e)
        {
            textbox_change_detail(label_changedValue.Text, true);
        }

        private void label_changedValue_MouseLeave(object sender, EventArgs e)
        {
            textbox_change_detail(label_changedValue.Text, false);
        }
        private void textbox_change_detail(string type, bool visible )
        {
            this.textBox_change_detail.BringToFront();
            this.textBox_change_detail.Visible = visible;
            
            var text = string.Empty;
            switch(type)
            {
                case "P": text = G.Localize("dirty_pos"); break;
                case "V": text = G.Localize("dirty_view"); break;
                case "C": text = G.Localize("dirty_changed"); break;
            }

            this.textBox_change_detail.Text = text;
        }
#endregion

        private void panel1_Resize(object sender, EventArgs e)
        {
            if (G.m_history_record_panel!=null) G.m_history_record_panel.adjust_size();
            if (G.m_focus_track_panel!=null) G.m_focus_track_panel.adjust_size();
            G.Scroll_mpb_changed();
        }

        private void optionFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            G.option_form.ShowDialog(this);
        }

        private void vScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            G.Scroll_moved();
        }

        private void hScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            G.Scroll_moved();
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("clicked");
            AppUpdate.mouse_update(MouseEventId.CLICK);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            panel1.Focus();
            AppUpdate.mouse_update(MouseEventId.MOUSEDOWN);
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            panel1.Focus();
            AppUpdate.mouse_update(MouseEventId.MOUSEUP);
        }

        private void deleteAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(G.Localize("deleteall_ask"),G.Localize("deleteall_confirm"),MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var list = new List<string>(  G.excel_program.GetStateList() );
                foreach(var s in list)
                {
                    G.excel_program.Delete(s);
                }
                create_new_state("S_START",WordStorage.Store.state_typ_start, 100,100, "","S_END");
                create_new_state("S_END"  ,WordStorage.Store.state_typ_end, 500,100, "");

                History2.SaveForce_modify_value("Delete All");

                G.node_enter_group("/");

                G.req_redraw_force();
            }
        }
        private string create_new_state(string state_name, string state_typ, int x, int y, string comment, string nextstate=null)
        {
            var newstate = G.excel_program.NewState(state_name,"/");
            G.excel_program.SetString(newstate, G.STATENAME_statetyp, state_typ);
            G.excel_program.SetString(newstate, G.STATENAMESYS_pos, string.Format("{0},{1}",x,y));
            if (!string.IsNullOrEmpty(nextstate))
            {
                G.excel_program.SetString(newstate, G.STATENAME_nextstate, nextstate);
            }
            G.excel_program.SetString(newstate, G.STATENAME_statecmt, comment);
            return newstate;
        }

        private void copyFilenameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var s = Path.GetFileName( G.psgg_file_w_data ? G.psgg_file :  G.load_file );
            Clipboard.SetText(s);
        }

        private void copyFilenameToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var s = Path.GetFileName( G.psgg_file_w_data ? G.psgg_file :  G.load_file );
            Clipboard.SetText(s);
        }

        private void copyCollectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dlg = new stateview._5800_CopyCollection.CCCreateForm();
            dlg.ShowDialog();
        }

        private void label_cc_DoubleClick(object sender, EventArgs e)
        {
            open_cc();
        }
        private void openCopyCollectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            open_cc();
        }

        private void createCopyCollectionFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(G.cc.m_copycollectionFolder_fullpath) && Directory.Exists(G.cc.m_copycollectionFolder_fullpath))
            {
                if (G.cc.m_read_only==false)
                {
                    if (MessageBox.Show("既に存在しますが、強制的に作成しますか？","確認",MessageBoxButtons.YesNo)== DialogResult.No)
                    {
                        return;
                    }
                }
            }

            var dlg = new stateview._5800_CopyCollection.CCCreateForm();
            dlg.ShowDialog();

            if (dlg.DialogResult == DialogResult.OK)
            {
                if (m_cc2 != null)
                {
                    m_cc2.Close();
                    open_cc();
                }
            }
        }

        private void open_cc()
        {
            if (!Directory.Exists(G.cc.m_copycollectionFolder_fullpath))
            {
                var dlg = new stateview._5800_CopyCollection.CCCreateForm();
                dlg.ShowDialog();
            }
            else
            {
                if (m_cc2==null)
                {
                    m_cc2 = new _5800_CopyCollection.CCForm2();
                    m_cc2.Show(this);
                    m_cc2.Location = Cursor.Position;
                }
            }
        }

#region CCドラッグ＆ドロップ
        private void MainPictureBox_DragDrop(object sender, DragEventArgs e)
        {
            G.NoticeToUser("main pb drag drop");
            G.m_cc_dragdrop = G.CCDD.dragdrop;

            var files = (StringCollection)e.Data.GetData(DataFormats.FileDrop);
            if (files!=null && files.Count==1)
            {
                G.NoticeToUser("main pb drag frop file : " + files[0]);
                G.m_cc_droppath = files[0];
            }
        }

        private void MainPictureBox_DragEnter(object sender, DragEventArgs e)
        {
            G.NoticeToUser("main pb drag enter");

            if (
                e.Data.GetDataPresent(typeof(Bitmap))
                &&
                e.Data.GetDataPresent(DataFormats.FileDrop)
                )
            {
                G.NoticeToUser("main pb drag enter : bitmap and file drop");
                e.Effect = DragDropEffects.Copy;

                G.m_cc_dragdrop = G.CCDD.dragenter;
                if (G.m_cc_dropbmp!=null)
                {
                    G.m_cc_dropbmp.Dispose();
                    G.m_cc_dropbmp = null;
                }
                G.m_cc_dropbmp = new Bitmap((Image)e.Data.GetData(typeof(Bitmap)));

                return;
            }
            e.Effect = DragDropEffects.None;
           
        }

        private void MainPictureBox_DragLeave(object sender, EventArgs e)
        {
            G.NoticeToUser("main pb drag leave");
            G.m_cc_dragdrop = G.CCDD.none;
        }

        private void MainPictureBox_DragOver(object sender, DragEventArgs e)
        {
            G.NoticeToUser("main pb drag over");
            //G.m_cc_dragdrop = G.CCDD.none;
        }

        private void MainPictureBox_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            G.NoticeToUser("main pb give feedback");
        }

        private void MainPictureBox_QueryContinueDrag(object sender, QueryContinueDragEventArgs e)
        {
            G.NoticeToUser("main pb query continue drag");
        }
#endregion

#region コピーコレクション
        public _5800_CopyCollection.CCForm2 m_cc2;
        private void copyCollection3rdTryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_cc2 == null)
            {
                m_cc2 = new _5800_CopyCollection.CCForm2();
                m_cc2.Show(this);
            }
        }
#endregion

#region StateGoウインドウ選択
        //private void listBoxWindows_DoubleClick(object sender, EventArgs e)
        //{
        //    var index = listBoxWindows.SelectedIndex;
        //    var key = ListUtil.GetVal( m_main_panel_winkey_list, index);

        //    if (key!=null && key != G.VIEWFORM_HANDLE)
        //    {
        //        WindowsUtil.ActiveWindow(key);
        //    }
        //    else
        //    {
        //        G.NoticeToUser_warning(G.Localize("w_selectewin_this")/*"The selected window is this one."*/);
        //    }

        //}
        private void stateGoWindowListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dlg = new _5000_ViewForm.dialog.WindowListForm();
            dlg.ShowDialog();
        }

        private void label_openWinList_Click(object sender, EventArgs e)
        {
            var dlg = new _5000_ViewForm.dialog.WindowListForm();
            dlg.ShowDialog();
        }
        #endregion

        private void setStarterKitPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dg = new _5000_ViewForm.dialog.SetStarterKitRootForm();
            dg.Show(this);
        }

        private void flowChartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            output_flowchart_html(false);
        }

        private void flowChartWithCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            output_flowchart_html(true);
        }

        private void output_flowchart_html(bool bwCode)
        {
            if (G.bDirty)
            {
                MessageBox.Show(G.Localize("output_flowchart_html_warning"));
                return;
            }

            var NL = Environment.NewLine;
            var DQ = "\"";
            var SP = " ";
            MessageBox.Show(G.Localize("output_flowchart_html_msg"));
                /*
                "mermaidを利用してフローチャートへ変換します。" + NL +
                "【次のステップを実行します】" + NL +
                "１．出力ファイル(html)を指定" + NL +
                "２．ブラウザで開く"
                */
                
            var sfd = saveFileDialog1;
            sfd.FileName = G.load_file_name_woext + ".html";
            sfd.InitialDirectory = G.load_file_dir;
            var ret = sfd.ShowDialog(this);
            if (ret != DialogResult.OK)
            {
                return;
            }
            var tool = @"G:\statego\tools\psgg-mermaid-flow\psgg2mermaid\bin\Debug\psgg2mermaid.exe";
            {
                var cand = Path.Combine( Path.GetDirectoryName( PathUtil.GetThisAppPath()) , "psgg2mermaid.exe");
                if (File.Exists(cand))
                {
                    tool = cand;
                }
            }

            var src = G.psgg_file;
            var dst = sfd.FileName; //Path.Combine(G.load_file_dir, "~~mermaid.txt");
            var opt = "-html";
            if (bwCode)
            {
                opt += SP + "-c";
            }

            ExecUtil.execute_w_args_and_wait( tool , DQ + src + DQ + SP + DQ + dst + DQ + SP + opt, Path.GetDirectoryName(dst));

            //var md = File.ReadAllText(dst,Encoding.UTF8);
            if (File.Exists(dst))
            {
                ExecUtil.execute_start2(dst,null);
            }
            else
            {
                MessageBox.Show("Error \n{9B094997-FC93-4BD3-94D4-58CC28605D51}");
            }
        }

        private void scrambleText_none_toolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScrambleText.setlevel( 0 );
           
            Flow.main_skip_load_flow();
        }
        private void scrambleText_level1_toolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScrambleText.setlevel( 1 );
            Flow.main_skip_load_flow();
        }
        private void scambleText_levle2_toolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScrambleText.setlevel( 2 );
            Flow.main_skip_load_flow();
        }
        private void scrambleText_level3_toolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScrambleText.setlevel( 3 );
            Flow.main_skip_load_flow();
        }

        private void convertSettingToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            update_donoteditmarkOnOff();
        }
        private void update_donoteditmarkOnOff()
        {
            donoteditmarkOnToolStripMenuItem.Checked  =  G.option_use_donotedit_mark;
            donoteditmarkOffToolStripMenuItem.Checked = !G.option_use_donotedit_mark;
        }

        private void donoteditmarkOnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            G.option_use_donotedit_mark = true;
            update_donoteditmarkOnOff();
        }

        private void donoteditmarkOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            G.option_use_donotedit_mark = false;
            update_donoteditmarkOnOff();
        }
    }
}

