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

/*
    キー処理
    
    view formのキーは一度ここに入る ※処理のタイミングが異なるの注意
    直実行は、ここで処理
    詳細は キーボード操作.pptx
        
*/
namespace stateview
{
    /// <summary>
    /// ViewFormステートマシン内で実行する機能
    /// </summary>
    public enum KEYEXEC {
        none,
        focus_home,
        focus_end,
        open_contextmenu,
        paste_w_ouflow,
        paste_wo_outflow,
        history_back,
        history_forward,
        focustrack_back,
        focustrack_forward,

        focus_specified_state,
        open_inout_menu,

        focus_clear,
        focus_all,

        enter_group,
        leave_group,

        delete_states,

    }

    public class KeyProc
    {
        public static ArrowKeyControl m_akc;
        public static EnterKeyControl m_ekc;
        public static Keys m_keycode_from_viewform=Keys.None;
        public static void input_update()
        {
            G.Key = m_keycode_from_viewform;
            m_keycode_from_viewform = Keys.None;
        }
        public static void work_at_update() //直実行
        {
            Keys key = G.Key;
            var control_on = (Control.ModifierKeys & Keys.Control) == Keys.Control;
            var shift_on   = (Control.ModifierKeys & Keys.Shift)   == Keys.Shift;

            if (!G.frontend_enabled()) return;
            if (G.Key == Keys.None) return;
            if (G.vf_sc == null) return;

            #region 保存・変換
            if ( control_on &&  key == Keys.S ) //保存＆変換
            {
                G.view_form.SaveAndRun(false);
                return;
            }
            if ( shift_on && control_on && key == Keys.S)//保存のみ
            {
                G.view_form.SaveAndRun(true);
                return;
            }
            #endregion

            #region フォルダ・ソース開く
            if (
                (control_on && key == Keys.D1) //ドキュメントフォルダ開く
                )
            {
                G.view_form.buttonOpenExcelFolder_Click(null,null);
                return;
            }
            if (
                (control_on && key == Keys.D2) //ソースフォルダ開く
                )
            {
                G.view_form.buttonOpenSourceFolder_Click(null,null);
                return;
            }
            if (
                (control_on && key == Keys.D3) //ソース開く
                ||
                (control_on && key == Keys.J) //ソース開く
                )
            {
                G.view_form.OpenEditorWithCreateFile();
                return;
            }
            #endregion
            #region 拡大・縮小
            if (key == Keys.Oemcomma) //カンマ '<'　縮小
            {
                var newscale = G.scale_percent;
                newscale -= 5;
                if (newscale < 5) newscale = 5;
                G.set_scalepercent_with_textbox(newscale);
                return;
            }
            if (key == Keys.OemPeriod) //ピリオド '>'　拡大
            {
                var newscale = G.scale_percent;
                newscale += 5;
                if (newscale > 200) newscale = 200;
                G.set_scalepercent_with_textbox(newscale);
                return;
            }
            if (key == Keys.OemQuestion) //?  １００％
            {
                G.set_scalepercent_with_textbox(100);
                return;
            }
            #endregion

            #region スクロール
            Func<Keys, float, float,bool> scroll_diff_func = (k,dx,dy)=> {
                if (control_on && key== k)
                { 
                    G.scroll.MoveMpbLoc(-dx,-dy);
                    return true;
                }
                return false;
            };
            if (scroll_diff_func(Keys.Right,100,0) ) return;
            if (scroll_diff_func(Keys.Left,-100,0) ) return;
            if (scroll_diff_func(Keys.Down,0,100)  ) return;
            if (scroll_diff_func(Keys.Up,0,-100)   ) return;


            Func<Keys, float, float,bool> scroll_abs_func = (k, rx, ry) =>
            {
                if (key != k) return false;

                ViewUtil.SetScrollTopLeft_at_0to1(rx,ry);

                return true;
            };
            if ( scroll_abs_func(Keys.NumPad7,0.0f,0.0f) ) return;
            if ( scroll_abs_func(Keys.NumPad8,0.5f,0.0f) ) return;
            if ( scroll_abs_func(Keys.NumPad9,1.0f,0.0f) ) return;
            if ( scroll_abs_func(Keys.NumPad4,0.0f,0.5f) ) return;
            if ( scroll_abs_func(Keys.NumPad5,0.5f,0.5f) ) return;
            if ( scroll_abs_func(Keys.NumPad6,1.0f,0.5f) ) return;
            if ( scroll_abs_func(Keys.NumPad1,0.0f,1.0f) ) return;
            if ( scroll_abs_func(Keys.NumPad2,0.5f,1.0f) ) return;
            if ( scroll_abs_func(Keys.NumPad3,1.0f,1.0f) ) return;

            #endregion

            #region HOME and END
            if (G.Key == Keys.Home)
            {
                set_nearest_state_and_focus();
                return;
            }
            if (key == Keys.End)
            {
                set_farest_state_and_focus();
                return;
            }
            
            // フォーカストラックパネル
            if (key == Keys.T && control_on)
            {
                G.m_focus_track_panel.open_or_close();
                return;
            }
            //ヒストリレコードパネル
            if (key == Keys.H && control_on)
            {
                G.m_history_record_panel.open_or_close();
                return;
            }
            //検索フォーム
            if (key == Keys.F && control_on)
            {
                G.find_form.Visible = !G.find_form.Visible;
                return;
            }
            #endregion

            #region CTRL A 全選択
            if (control_on && key == Keys.A)
            {
                G.vf_sc.m_group_focus_list = new List<string>();
                foreach(var k in G.m_draw_data_list.Keys)
                {
                    G.vf_sc.m_group_focus_list.Add(k);
                }


                G.keyexec = KEYEXEC.focus_all;
                return;
            }
            #endregion

            #region ペースト
            if (control_on && key == Keys.V)
            {
                G.vf_sc.m_pos_at_menu_on_bmp = Point.Round( G.vf_sc.GetPointerOnMainBmp() );
                G.keyexec = KEYEXEC.paste_wo_outflow;
                return;
            }
            if (shift_on && control_on && key == Keys.V)
            {
                G.vf_sc.m_pos_at_menu_on_bmp = Point.Round( G.vf_sc.GetPointerOnMainBmp() );
                G.keyexec = KEYEXEC.paste_w_ouflow;
                return;
            }
            #endregion

            #region 履歴
            if (control_on && key == Keys.Z)
            {
                G.keyexec = KEYEXEC.history_back;
                return;
            }
            if (control_on && key == Keys.Y)
            {
                G.keyexec = KEYEXEC.history_forward;
                return;
            }
            #endregion
            #region フォーカストラック
            if (key == Keys.OemOpenBrackets)
            {
                G.keyexec = KEYEXEC.focustrack_back;
                G.vf_sc.m_track_focused_pathdir = G.node_get_cur_dirpath();
                G.vf_sc.m_track_focused_states = G.vf_sc.GetFocusingStates();
                return;
            }
            if (key == Keys.OemCloseBrackets)
            {
                G.keyexec = KEYEXEC.focustrack_forward;
                G.vf_sc.m_track_focused_pathdir = G.node_get_cur_dirpath();
                G.vf_sc.m_track_focused_states = G.vf_sc.GetFocusingStates();
                return;
            }
            #endregion


            #region コンテキストメニュー
            if (key == Keys.Enter &&  G.view_form.PointToScreen(G.view_form.splitter1.Location).X <Cursor.Position.X)
            {
                G.vf_sc.Check_state_under_pointer();
                G.vf_sc.m_idleSc.CheckIsOnBranch();

                if (m_ekc==null) m_ekc = new EnterKeyControl();
                m_ekc.reset();
                m_ekc.m_pointer = Cursor.Position;
                m_ekc.m_state_under_pointer = G.vf_sc.m_state_under_pointer;

                var branchinfo          = G.vf_sc.m_branchInfo;
                if (branchinfo!=null)
                { 
                    m_ekc.m_state_with_pin    = branchinfo.m_branchpoint_state;
                    m_ekc.m_in_or_outflow_pin = branchinfo.m_branchpoint_inputpoint; 
                    if      (branchinfo.m_branchpoint_isNextStateOrBranchOrGosub==1) m_ekc.m_outflow_attr = InOutBaseData.ATTRIB.nextstate;
                    else if (branchinfo.m_branchpoint_isNextStateOrBranchOrGosub==2) m_ekc.m_outflow_attr = InOutBaseData.ATTRIB.branch;
                    else if (branchinfo.m_branchpoint_isNextStateOrBranchOrGosub==3) m_ekc.m_outflow_attr = InOutBaseData.ATTRIB.gosub;
                    else                                                             m_ekc.m_outflow_attr = InOutBaseData.ATTRIB._base; //使わない意味で
                    m_ekc.m_branch_index = branchinfo.m_branchpoint_branch_index!=null ? (int)branchinfo.m_branchpoint_branch_index : -1;
                    m_ekc.m_nextstate = branchinfo.m_branchpoint_label;
                }

                m_ekc.Run();

                return;
            }
            #endregion
            #region フォーカス中受付キー
            if (
                G.vf_sc.IsFocusing() //何かしらフォーカス中（選択中）
                &&
                G.view_form.panel1.Focused
                &&
                G.view_form.panel1.Bounds.Contains(G.view_form.PointToClient( Cursor.Position))
                &&
                !G.view_form.NoticeTextBox.Focused
                &&
                !G.view_form.scintillaBoxTabFunc.Focused //!G.view_form.textBoxTabFunc.Focused
                )
            {
                if (control_on && key == Keys.C) //コピー
                {
                    if (G.vf_sc.IsFocusing_A_STATE())
                    {
                        G.ExportToClipboard(new List<string>() { G.latest_focuse_state });
                    }
                    else if (G.vf_sc.IsFocusing_A_GROUP())
                    {
                        var templist = G.node_get_allstates_on_groupnode(G.vf_sc.m_groupnode_name);
                        G.ExportToClipboard(templist);
                    }
                    else
                    {
                        G.vf_sc.gfstatemenu_export_clipboard();
                    }
                    return;
                }
                if (key == Keys.Delete) //削除
                {
                    G.keyexec = KEYEXEC.delete_states;
                }
            }
            #endregion
            #region 全選択

            #endregion

            #region ENTER/LEAVE
            if (
                control_on && key == Keys.E
                )
            {
                G.vf_sc.Check_state_under_pointer();
                if (AltState.IsAltState(G.vf_sc.m_state_under_pointer))
                {
                    G.vf_sc.m_groupnode_name = AltState.TrimAltStateName(G.vf_sc.m_state_under_pointer);
                    G.keyexec = KEYEXEC.enter_group;
                }
            }
            if (
                control_on && key == Keys.L
                )
            {
                G.keyexec = KEYEXEC.leave_group;
            }
            #endregion

            #region //方向キー処理
            if (
                shift_on==false && control_on==false
                &&
               (key == Keys.Right || key == Keys.Left || key == Keys.Down || key == Keys.Up)
             )
            { 
                G.vf_sc.Check_state_under_pointer();
                G.vf_sc.m_idleSc.CheckIsOnBranch();

                if (m_akc==null) m_akc = new ArrowKeyControl();
                m_akc.reset();
                m_akc.m_key = key;
                m_akc.m_pointer = Cursor.Position;
                m_akc.m_state_under_pointer = G.vf_sc.m_state_under_pointer;

                var branchinfo          = G.vf_sc.m_branchInfo;
                if (branchinfo!=null)
                { 
                    m_akc.m_state_with_pin    = branchinfo.m_branchpoint_state;
                    m_akc.m_in_or_outflow_pin = branchinfo.m_branchpoint_inputpoint; 
                    if      (branchinfo.m_branchpoint_isNextStateOrBranchOrGosub==1) m_akc.m_outflow_attr = InOutBaseData.ATTRIB.nextstate;
                    else if (branchinfo.m_branchpoint_isNextStateOrBranchOrGosub==2) m_akc.m_outflow_attr = InOutBaseData.ATTRIB.branch;
                    else if (branchinfo.m_branchpoint_isNextStateOrBranchOrGosub==3) m_akc.m_outflow_attr = InOutBaseData.ATTRIB.gosub;
                    else                                                             m_akc.m_outflow_attr = InOutBaseData.ATTRIB._base; //使わない意味で
                    m_akc.m_branch_index = branchinfo.m_branchpoint_branch_index!=null ? (int)branchinfo.m_branchpoint_branch_index : -1;
                    m_akc.m_nextstate = branchinfo.m_branchpoint_label;
                }
                m_akc.Run();
                return;
            }
            #endregion
        }

        public static void set_nearest_state_and_focus()
        {
            var st = ViewUtil.GetNearestState("^S_|^" + AltState.GetAltMark());
            if (StateUtil.IsValidStateName(st))
            {
                if (AltState.IsAltState(st))
                {
                    var group = AltState.TrimAltStateName(st);
                    G.vf_sc.m_center_focus_group = group;
                    G.vf_sc.m_center_focus_same_dirpath = true;
                    G.keyexec = KEYEXEC.focus_home;
                }
                else
                {
                    G.vf_sc.m_center_focus_state = st;
                    G.vf_sc.m_center_focus_same_dirpath = true;
                    G.keyexec = KEYEXEC.focus_home;
                }
            }
        }
        public static void set_farest_state_and_focus()
        {
            var st = ViewUtil.GetFarestState("^S_|^" + AltState.GetAltMark());
            if (StateUtil.IsValidStateName(st))
            {
                if (AltState.IsAltState(st))
                {
                    var group = AltState.TrimAltStateName(st);
                    G.vf_sc.m_center_focus_group = group;
                    G.vf_sc.m_center_focus_same_dirpath = true;
                    G.keyexec = KEYEXEC.focus_end;
                }
                else
                {
                    G.vf_sc.m_center_focus_state = st;
                    G.vf_sc.m_center_focus_same_dirpath = true;
                    G.keyexec = KEYEXEC.focus_end;
                }
            }
        }
    }
}
