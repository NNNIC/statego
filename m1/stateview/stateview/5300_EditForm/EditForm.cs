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

namespace stateview._5300_EditForm
{
    public partial class EditForm:Form
    {
        public string m_state { get { return G.multiedit_control.m_state; } }

        private string m_tempstate { get {
                if (m_dgvcache!=null)
                {
                    if (m_dgvcache.ContainsKey(G.STATENAME_state))
                    {
                        return m_dgvcache[G.STATENAME_state];
                    }
                }
                return m_state;
            } } 

        public bool   m_modified;
        public int?   m_bmp_row = null;
        public Bitmap m_bmp;

        //public Bitmap m_outbmp;
        //void Done() { G.multiedit_control.Done();}

        private Dictionary<string,string> m_dgvcache;  //データグリッドキャッシュ item nameと値を保持
                                                       //Show_itemsの直前にキャッシュ
                                                       //キャッシュがない場合は、excelのキャッシュを使用する


        public EditForm()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.None;
        }

        private void ok_button_Click(object sender,EventArgs e)
        {
            var bModified = false;

            //全部反映
            var new_state = m_state;//
            var excel_col = G.get_state_col(m_state);
            for(var row = 0; row < dataGridView1.Rows.Count; row++)
            {
                var value1     = dataGridView1[1,row].Value!=null ? dataGridView1[1,row].Value : null;
                var value2     = dataGridView1[3,row].Value!=null ? dataGridView1[3,row].Value : null;
                var name = string.Empty;
                var text = string.Empty;
                if (value1!=null)
                {
                    name      = value1.ToString();
                }
                if (value2!=null)
                {
                    text      = value2.ToString();
                }
                var excel_row = int.Parse(dataGridView1[0,row].Value.ToString());
                var ec = G.get_excel_cell_cache(excel_row,excel_col);
                if (ec!=null)
                {
                    if (ec.text!=text)
                    {
                        ec.modified = true;
                        bModified = true;

                        if (name == G.STATENAME_state)
                        {
                            var srctext = ec.text;
                            StateUtil.Refactor(srctext,text);
                            stateview.CopyRenameStateSave.calc_new_dst_position(srctext,text,false);
                            new_state = text;
                        }
                        else
                        {
                            ec.text = text;
                        }
                    }
                }
                else if (!string.IsNullOrWhiteSpace(text))
                {
                    if (name != G.STATENAME_thumbnail && text != "(bitmap)")
                    { 
                        bModified = true;
                    }

                    ExcelCellCacheItem refitem = null;
                    //excel_rowから上の存在するセルデータを取得し、クローンして上書き利用
                    for(var er = excel_row-1; er >=1; er--)
                    {
                        var i = G.get_excel_cell_cache(er,excel_col);
                        if (i!=null)
                        {
                            refitem = i;
                            break;
                        }
                    }
                    if (refitem!=null)
                    {
                        var citem = refitem.Clone();
                        citem.row = excel_row;
                        citem.col = excel_col;
                        citem.text = text;
                        citem.modified= true;
                        citem.newitem = true;
                        G.m_excel_cell_cache_dic.Add(citem.key,citem);
                    }
                }
            }

            if(m_bmp_row!=null)
            {
                var hash  = G.excel_pictures.SetItem( (int)m_bmp_row,excel_col,m_bmp);
                if (G.psgg_file_w_data)
                {
                    if (!string.IsNullOrEmpty(hash) && m_bmp!=null)
                    { 
                        FileDbUtil.RegisterFieDbBitmap(hash, BitmapUtil.ToBase64(m_bmp));
                    }

                    var ec = G.get_excel_cell_cache((int)m_bmp_row, excel_col);
                    if (ec != null)
                    {
                        ec.text = hash;
                    }
                }

                bModified = true;
            }

            StateUtil.ContentTab_write(new_state);

            if (bModified)
            { 
                G.bDirty_by_modified_value = bModified;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
            //Done();
        }

        private void cancel_button_Click(object sender,EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
            //Done();
        }

        private List<string> m_name_list;
        private void EditForm_Load(object sender,EventArgs e)
        {
            WordStorage.Res.ChangeAll(this,G.system_lang);

            this.Row.HeaderText = G.Localize("eftf_row");
            this.help.HeaderText = G.Localize("eftf_desc");
            this.NAME.HeaderText = G.Localize("eftf_name");
            this.ITEM_VAUE.HeaderText = G.Localize("eftf_value");

            // ※fullボタンがデザイナ設定からずれるため、プログラムで修正
            //this.radioButton_full.Location = PointUtil.Mod_X(this.radioButton_full.Location, this.radioButton_opt.Location.X);

            m_name_list = FilteredTemplate.get_names_from_current_filterdtemplate();

            ShowItems();
            this.Location = Cursor.Position;

            //if (G.JX)
            //{
            //    this.ImportButton.Enabled = true;
            //    this.ImportButton.Visible = true;
            //}

            set_explain_label();
        }

        private void ShowItems()
        {
            if (m_dgvcache!=null)
            {
                var dgv = dataGridView1;
                for(var r = 0;r<dgv.Rows.Count; r++)
                {
                    var iname = dgv[1,r].Value?.ToString();
                    var val = dgv[3,r].Value?.ToString();
                    if (!string.IsNullOrEmpty(iname))
                    {
                        if (m_dgvcache.ContainsKey(iname))
                        {
                            m_dgvcache[iname]=val;
                        }
                        else
                        {
                            m_dgvcache.Add(iname,val);
                        }
                    }
                }
            }
            //if (radioButton_opt.Checked)
            //{
            //    EditForm_Control.Show_optimized_items(m_state, m_tempstate, ref m_dgvcache,true);
            //}
            //else if (radioButton_optmid.Checked)
            //{
            //    EditForm_Control.Show_optimized_items(m_state, m_tempstate, ref m_dgvcache,false);
            //}
            //else
            //{
            //    EditForm_Control.Show_all_items(m_state, m_tempstate, ref m_dgvcache);
            //}

            EditForm_Control.Show_all_items(m_state, m_tempstate, ref m_dgvcache);

            //
            Func<string,string,bool> is_visible_name = (n,v)=> {
                var bVisible = true;
                if (string.IsNullOrEmpty(n)) bVisible = false;

                if (!m_name_list.Contains(n) && string.IsNullOrEmpty(v)) bVisible = false;

                if (n == G.STATENAME_statetyp)  bVisible = false;
                if (n == G.STATENAME_branch)    bVisible = false;
                if (n == G.STATENAME_brcond)    bVisible = false;
                if (n == G.STATENAME_branchcmt) bVisible = false;

                if (!string.IsNullOrEmpty(n) && n.StartsWith("!")) bVisible = false;

                return bVisible;
            };
            if (radioButton_opt.Checked)
            {
                for(var r=0; r < dataGridView1.Rows.Count; r++)
                {
                    var dgname = dataGridView1[1,r].Value?.ToString();
                    var dgval = dataGridView1[3,r].Value?.ToString();

                    dataGridView1.Rows[r].Visible = is_visible_name(dgname, dgval);
                }
            }
            else if (radioButton_optmid.Checked)
            {
                for(var r=0; r < dataGridView1.Rows.Count; r++)
                {
                    var dgname  = dataGridView1[1,r].Value?.ToString();
                    
                    var dgname2 = string.Empty;
                    dgname2 = RegexUtil.Get1stMatch(@"\!{0,1}[a-zA-Z0-9_]+",dgname);
                    var dgval = dataGridView1[3,r].Value?.ToString();

                    dataGridView1.Rows[r].Visible = is_visible_name(dgname2,dgval);
                }
            }

            HilitenItems();
        }

        private void HilitenItems()
        {
            //どれを選択したらよいか、強調させる
            // state - > 黒のboldへ
            // xxx-xxx -> 灰色
            // !xxx -> 灰色
            for (var r = 0; r < dataGridView1.Rows.Count; r++)
            {
                var cell_1 = dataGridView1[1, r];
                var cell_2 = dataGridView1[3, r];
                var font = dataGridView1.Font;
                if (cell_1 == null || cell_1.Value == null) continue;
                if (cell_1.Value.ToString() == G.STATENAME_state)
                {
                    cell_1.Style.Font = new Font(font.Name, font.Size, FontStyle.Bold);
                    cell_2.Style.Font = new Font(font.Name, font.Size, FontStyle.Bold | FontStyle.Italic);
                }
                else if (cell_1.Value.ToString() == G.STATENAME_branch)
                {
                    cell_1.Style.ForeColor = Color.LightGray;
                }
                else if (cell_1.Value.ToString() == G.STATENAME_brcond)
                {
                    cell_1.Style.ForeColor = Color.LightGray;
                }
                else if (cell_1.Value.ToString().Contains("-"))
                {
                    cell_1.Style.ForeColor = Color.LightGray;
                    cell_2.Style.ForeColor = Color.Gray;
                }
                else if (cell_1.Value.ToString().StartsWith("!"))
                {
                    cell_1.Style.ForeColor = Color.Gray;
                    cell_2.Style.ForeColor = Color.Gray;
                }
            }
        }

        private void EditForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.None)
            {
                this.DialogResult = DialogResult.Cancel;
                //Done();
            }
        }

        private void dataGridView1_CellContentClick(object sender,DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView1_CellClick(object sender,DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(object sender,DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex <0) return;

            dataGridView1__open_editor(e.RowIndex,string.Empty);
        }

        private void dataGridView1__open_editor(int row, string inputkey)
        {
            if (row < 0) return;
            var text = string.Empty;
            try
            {
                text = this.dataGridView1[3,row].Value.ToString();
            }
            catch { }

            text += inputkey;

            var name = string.Empty;
            try {
                name = this.dataGridView1[1,row].Value.ToString();
            }
            catch { }
            var excel_row = int.Parse(this.dataGridView1[0,row].Value.ToString());
            var excel_col = G.get_state_col(m_state);

            var item = G.excel_pictures.GetItem(excel_row,excel_col);
            Bitmap bmp = null;

            if (m_bmp!=null)
            {
                bmp = m_bmp;
            }
            else
            {
                bmp = item !=null ?  item.bmp : null;
            }

            //for cmt *start
            var cmt_row = DataGridViewUtil.FindRowIndexAtColumnForText(dataGridView1,1,name + "-cmt");
            var cmt     = DataGridViewUtil.GetString(dataGridView1,3,cmt_row);
            //*end
            //for ref *start
            var ref_row = DataGridViewUtil.FindRowIndexAtColumnForText(dataGridView1,1,name + "-ref");
            var refx    = DataGridViewUtil.GetString(dataGridView1,3,ref_row);
            //*end

            var    savetext = text;
            var    savecmt  = cmt;
            var    saveref  = refx;
            var    savebmp  = bmp;
            var    bModifiedBmp = false;
            var    bCmt = cmt_row >= 0;
            var    bRef = ref_row >= 0;
            var    bNeedDgvUpdt = false;

            EditForm_Control.Call_click_cotrol(this, m_state, name , ref text, bCmt, ref cmt, bRef, ref refx,  ref bmp, out bModifiedBmp, out bNeedDgvUpdt);

            if (savetext!=text || savecmt!=cmt || saveref!=refx)
            {
                m_modified = true;
                this.dataGridView1[3,row].Value = text;
                if (cmt_row >=0) this.dataGridView1[3,cmt_row].Value = cmt;
                if (ref_row >=0) this.dataGridView1[3,ref_row].Value = refx;
                ShowItems();
            }

            if ( bModifiedBmp )
            {
                m_modified    = true;
                m_bmp_row     = excel_row;
                m_bmp         = bmp;
            }

            if (bNeedDgvUpdt)
            {
                ShowItems();
            }
        }



        //セルを選択してキーダウンしたら、エディタを開くようにする。入力したキーコードはカラムの内容に追加される。
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            var dg = this.dataGridView1;
            if (dg.SelectedCells==null || dg.SelectedCells.Count==0) return;
            var selected = dg.SelectedCells[0];
            var row = selected.RowIndex;

            var inputkey = (new KeysConverter()).ConvertToString(e.KeyCode).ToLower();

            dataGridView1__open_editor( row, inputkey);
        }

        private void ImportButtonX_Click(object sender, EventArgs e)
        {

        }

        private void export_to_cb_button_Click(object sender, EventArgs e)
        {
            var nl = Environment.NewLine;
            var s = string.Empty;
            for(var r = 0; r<dataGridView1.Rows.Count; r++)
            {
                var name = dataGridView1[1,r].Value.ToString();
                var value= dataGridView1[3,r].Value.ToString();
                if (string.IsNullOrEmpty(name)) continue;
                if (string.IsNullOrEmpty(value)) continue;
                if (name.StartsWith("!")) continue;

                if (s!=string.Empty) s+=nl;

                s += name +"=@@@" + nl;
                s += value + nl;
                s += "@@@" + nl;
            }

            Clipboard.SetText(s);
            G.NoticeToUser("Exported to clipboard.");
        }

        private void import_from_cb_button_Click(object sender, EventArgs e)
        {
            var s = Clipboard.GetText();
            if (string.IsNullOrEmpty(s)) { G.NoticeToUser_warning(G.Localize("w_clipboardisnull")/* "Clipboard is null." */); return; }

            Hashtable ht = null;
            try {
                ht = IniUtil.CreateHashtable(s);
            } catch (SystemException e2)
            {
                G.NoticeToUser_warning(G.Localize("w_bufferisnotvalidd") /* "Buffer is not valid."*/ + e2.Message);
                return;
            }
            if (ht == null) { G.NoticeToUser_warning( G.Localize("w_clipboardbuffernotmatch") /* "Clipbaord buffer does not match" */); return; }

            for (var r = 0; r < dataGridView1.Rows.Count; r++)
            {
                var name = dataGridView1[1, r].Value.ToString();
                if (string.IsNullOrEmpty(name)) continue;
                var value = IniUtil.GetValueFromHashtable(name, ht);
                if (string.IsNullOrEmpty(value)) continue;

                if (name == G.STATENAME_state)
                {
                    if (!StateUtil.IsValidStateName(value))
                    {
                        G.NoticeToUser_warning(G.Localize("w_statenamenotvalidmakedapproname") /*  "state name is not valid. Maked aproproate name." */);
                        value = StateUtil.MakeNewName("S_0000");
                    }
                    if (!G.excel_program.GetStateList().Contains(value))
                    {
                        G.NoticeToUser_warning(G.Localize("w_statenameexistsmakedaname") /*  "the state name exists. Maked aproproate name." */);
                        value = StateUtil.MakeNewName(value);
                    }
                }

                dataGridView1[3, r].Value = value;
            }
            G.NoticeToUser("Imported from clipboard.");
        }

        private void radioButton_opt_CheckedChanged(object sender, EventArgs e)
        {
            ShowItems();
        }
        private void radioButton_optmid_CheckedChanged(object sender, EventArgs e)
        {
            ShowItems();
        }

        private void label_help_Click(object sender, EventArgs e)
        {
            HelpJumpUtil.Jump("tec_userguide","edit-item-dlg",G.system_lang=="jpn");
        }

        private void label_explain_Click(object sender, EventArgs e)
        {
            //var b = dataGridView1.Columns[2].Visible;
            //dataGridView1.Columns[2].Visible = !b;
            var b = RegistryWork.Get_editform_desc_onoff();
            RegistryWork.Set_editform_desc_onoff(!b);

            set_explain_label();
        }

        private void set_explain_label()
        {
            var b = RegistryWork.Get_editform_desc_onoff();
            this.dataGridView1.Columns[2].Visible = b;

            this.label_explain.Text = (b ? G.Localize("eftf_expon") : G.Localize("eftf_expoff"));
        }

        //private void ImportButton_Click(object sender, EventArgs e)
        //{

        //}

        private void button_clearStateCmt_Click(object sender, EventArgs e)
        {
            for(var r = 0; r < dataGridView1.Rows.Count; r++)
            {
                var name = dataGridView1[1,r].Value.ToString();
                if (name == G.STATENAME_statecmt)
                {
                    dataGridView1[3,r].Value = string.Empty;
                    break;
                }
            }
        }
    }
}
