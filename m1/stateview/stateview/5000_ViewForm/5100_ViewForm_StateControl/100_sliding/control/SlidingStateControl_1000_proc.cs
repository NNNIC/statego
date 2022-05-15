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

public partial class SlidingStateControl  : StateControlBase {

    PictureBox m_pb     { get { return G.view_form.pictureBoxHoldMark; } }
    Panel      m_panel1 { get { return G.view_form.panel1;             } }  //Windows Fill Scroll付き
    //Panel      m_panel2 { get { return G.view_form.panel2;             } }  //可変 Scrollなし
    PictureBox m_mpb    { get { return G.view_form.MainPictureBox; } }

    //Point      m_prev;
    //PointF     m_prevBmp;
    //Point      m_prevASP;

    //Point      m_save;
    //PointF     m_saveBmp;// { get { return m_parent.GetPointerOnMainBmp(m_save); }  }
    Point      m_saveASP;

    Point      m_first;
    PointF     m_firstBmp;
    Point      m_firstMPBLoc;   //MainPictureBox Location
    //int        m_firstP2Height; //Panel2 Height 
    //int        m_firstP2Width;  //   :   Width
    
    //PointF     m_firstASP;

    void show_pb()
    {
        m_pb.Parent = G.main_picturebox;
        _set_slidingmark();
        m_pb.Visible = true;
        m_pb.Enabled = true;

        m_first    = Cursor.Position;
        m_firstBmp = m_parent.GetPointerOnMainBmp(m_first); //m_saveBmp;
        m_firstMPBLoc   = m_mpb.Location;
        
        G.debug_form.textBoxSaveASP.Text = m_saveASP.ToString();
    }

    void update_pb()
    {
        m_panel1.SuspendLayout();
        

        //m_panel2.Width = m_panel1.Width;
        //m_panel2.Height = m_panel1.Height;

        var diff = PointUtil.Sub_Point(Cursor.Position , m_first);
        m_mpb.Location = PointUtil.Add_Point( m_firstMPBLoc, diff);

        G.Scroll_mpb_changed();

        _set_slidingmark();

        m_panel1.ResumeLayout();
        m_panel1.Refresh();
    }
#if xxx
    void show_pb()
    {
        m_pb.Parent = G.main_picturebox;
        _set_slidingmark();
        m_pb.Visible = true;
        m_pb.Enabled = true;

        updatepos();

        m_first    = m_save;
        m_firstBmp = m_saveBmp;
        m_firstASP = m_saveASP;

        m_firstMPBLoc   = m_mpb.Location;
        m_firstP2Height = m_panel2.Height;
        m_firstP2Width  = m_panel2.Width;

        G.debug_form.textBoxSaveASP.Text = m_saveASP.ToString();
    }
    void updatepos()
    {
        m_prev    = m_save;
        m_prevBmp = m_saveBmp;
        m_prevASP = m_saveASP;

        m_save     = Cursor.Position;        
        m_saveBmp = m_parent.GetPointerOnMainBmp(m_save);
        m_saveASP = PointUtil.Abs(m_panel1.AutoScrollPosition);
    }

    void update_pb()
    {
        var log = string.Empty;

        updatepos();
        //var posbmp = m_parent.GetPointerOnMainBmp();
        var diffBmp = PointUtil.Sub_Point(m_firstBmp,m_saveBmp);
        var diff    = PointUtil.Sub_Point(m_first,m_save);  

        var diffx_a = Math.Abs(diff.X);
        var diffy_a = Math.Abs(diff.Y);
        var diffrange = 3;
        //if (diffx_a <= diffrange && diffy_a <= diffrange) return;

        log += "diff    =" + diff.ToString() + Environment.NewLine;
        log += "diffBmp =" + diffBmp.ToString() + Environment.NewLine;

        G.debug_form.textBox_saveposX.Text = m_saveBmp.X.ToString();
        G.debug_form.textBox_saveposY.Text = m_saveBmp.Y.ToString();

        G.debug_form.textBoxSlideDiffX.Text = diffBmp.X.ToString();
        G.debug_form.textBoxSlideDiffY.Text = diffBmp.Y.ToString();

        var rel_diff = m_parent.GetPosOnMainPB(diffBmp);
        G.debug_form.textBox_reldiffX.Text = rel_diff.X.ToString();
        G.debug_form.textBox_reldiffY.Text = rel_diff.Y.ToString();

        //var spos = Point.Truncate( PointUtil.Add_Point((PointF)m_firstASP ,diff));
        //G.debug_form.textBox_scrollHMax.Text = spos.X.ToString();
        //G.debug_form.textBox_scrollVMax.Text = spos.Y.ToString();
        //G.logAppend(m_panel.AutoScrollPosition.ToString() +":::" + spos.ToString() + Environment.NewLine);

#if xx
        // panel1の右下のスクリーンポジションから、ビットマップのポジションを求める
        // そのポジションがBMP外であれば、オーバーしていることを検知できる。
        var scpos_panel1_br = m_panel.PointToScreen( new Point( m_panel.Width,m_panel.Height));
        var bmpos_panel1_br  = m_parent.GetPointerOnMainBmp(scpos_panel1_br);

        //var BRpos_on_mainpb = m_parent.GetPanelPos(new PointF(G.bitmap_width, G.bitmap_height));  
        G.debug_form.textBox_botrightX.Text = bmpos_panel1_br.X.ToString();
        G.debug_form.textBox_botrightY.Text = bmpos_panel1_br.Y.ToString();
#endif

        var bBotVscroll   = ((m_panel1.VerticalScroll.Maximum - m_panel1.VerticalScroll.LargeChange) - m_panel1.VerticalScroll.Value) < 0;
        var bRightHscroll = ((m_panel1.HorizontalScroll.Maximum - m_panel1.HorizontalScroll.LargeChange) - m_panel1.HorizontalScroll.Value) < 0;

        /*

             m_mpb->m_panel2->m_panel1->root

             m_panel1にスクロール
             m_panel2のLocationは、m_panel1の(0,0)に固定 (マイナスにできないため)
             m_mpbはm_panel2を自由にLocationできる。      

        */


        Action panel2Clamp = ()=> {
            if (m_panel2.Height < m_mpb.Location.Y + m_mpb.Height)  m_panel2.Height = m_mpb.Location.Y + m_mpb.Height;
            if (m_panel2.Width  < m_mpb.Location.X +  m_mpb.Width ) m_panel2.Width  = m_mpb.Location.X + m_mpb.Width;
        };

        // メインPBがパネルより小さいか？
        var mpb_width_smaller_than_panel  = m_mpb.Width < m_panel1.Width;   //幅が小さい
        var mpb_height_smaller_than_panel = m_mpb.Height < m_panel1.Height; //高さが小さい 

        // 

        var mpbloc = m_firstMPBLoc;

        var tmp_mpb_loc     = mpbloc;
        var tmp_panel2_size = m_panel2.Size; 

        var bXmodified = false;
        var bYmodified = false;

        if (diff.Y!=0)
        {
            if (mpb_height_smaller_than_panel) //ビットマップが縮小されてパネル１より小さい時
            {
                var addvalue = (int)-diff.Y;
                if (mpbloc.Y + addvalue < 0)
                {
                    tmp_mpb_loc = PointUtil.Mod_Y(mpbloc, 0);//m_mpb.Location  = PointUtil.Mod_Y(mpbloc, 0);
                    tmp_panel2_size.Height = m_mpb.Height;// m_panel2.Height = m_mpb.Height;
                }
                else
                {
                    tmp_mpb_loc =  PointUtil.Add_Y(mpbloc, addvalue); //m_mpb.Location = PointUtil.Add_Y(mpbloc, addvalue);
                    m_panel2.Height = m_mpb.Location.Y + m_mpb.Height;
                }

                log += "小高さPBモード：addvalue = " + addvalue ;

                bYmodified = true;
            }
            else
            {
                var newmpblocY = mpbloc.Y 

            }

            
            else if (m_panel1.VerticalScroll.Value == 0)
            {
                var addvalue = (int)-diff.Y;
                tmp_mpb_loc =  PointUtil.Add_Y(mpbloc, addvalue); //m_mpb.Location = PointUtil.Add_Y(mpbloc, addvalue);
                tmp_panel2_size.Height = m_firstP2Height + addvalue; //m_panel2.Height = m_firstP2Height + addvalue;
                log += "SCRトップ上下へモード： diff.y=" + diff.Y ;

                bYmodified = true;
            }
            else if (bBotVscroll)
            {
                var newy = (int)diff.Y;
                tmp_panel2_size.Height = m_firstP2Height + newy; //m_panel2.Height = m_firstP2Height + newy;
                if (tmp_panel2_size.Height < m_mpb.Height) tmp_panel2_size.Height = m_mpb.Height;

                log += "SCRボトム上下へモード：";

                bYmodified = true;
            }
        }


        if (diff.X != 0)
        {
            if (mpb_width_smaller_than_panel)
            {
                var addvalue = (int)-diff.X;
                if (mpbloc.X + addvalue < 0)
                {
                    tmp_mpb_loc = PointUtil.Mod_X(mpbloc, 0); //m_mpb.Location = PointUtil.Mod_X(mpbloc, 0);
                    tmp_panel2_size.Width =  m_mpb.Width;//m_panel2.Width = m_mpb.Width;
                }
                else
                {
                    tmp_mpb_loc =  PointUtil.Add_X(tmp_mpb_loc, addvalue); //m_mpb.Location = PointUtil.Add_X(mpbloc, addvalue);
                    tmp_panel2_size.Width =  m_mpb.Location.X + m_mpb.Width; //m_panel2.Width = m_mpb.Location.X + m_mpb.Width;
                }

                log += "小幅PBモード：addvalue = " + addvalue ;

                bXmodified = true;
            }
            else if (m_panel1.HorizontalScroll.Value == 0)
            {
                var addvalue = (int)-diff.X;
                tmp_mpb_loc = PointUtil.Add_X(tmp_mpb_loc, addvalue); //m_mpb.Location = PointUtil.Add_X(mpbloc, addvalue);
                tmp_panel2_size.Height = m_firstP2Width + addvalue; //m_panel2.Height = m_firstP2Width + addvalue;

                log += "SCR左端左右へモード： diff.x=" + diff.X ;

                bXmodified = true;
            }
            else if (bRightHscroll)
            {
                var newx = (int)diff.X;
                tmp_panel2_size.Width = m_firstP2Width + newx; // m_panel2.Width = m_firstP2Width + newx;
                if (tmp_panel2_size.Width < m_mpb.Width) tmp_panel2_size.Width = m_mpb.Width;//if (m_panel2.Width < m_mpb.Width) m_panel2.Width = m_mpb.Width;
                //m_panel1.HorizontalScroll.Value = m_panel1.HorizontalScroll.Maximum;

                log += "SCR右端右へモード：";

                bXmodified = true;
            }
        }

        m_panel2.Size = tmp_panel2_size;
        m_mpb.Location = tmp_mpb_loc;

        if (bXmodified==false && bYmodified==false)
        {
            //m_panel1.AutoScrollPosition = spos;
            log += "ノーマルモード：";
        }
        //else if (bXmodified == true && bYmodified == false)
        //{
        //    spos.Y = -m_panel1.AutoScrollPosition.Y;
        //    m_panel1.AutoScrollPosition = spos;
        //    log += "AutoScrollXのみ更新";
        //}
        //else if (bXmodified == false && bYmodified == true)
        //{
        //    spos.X = -m_panel1.AutoScrollPosition.X;
        //    m_panel1.AutoScrollPosition = spos;
        //    log += "AutoScrollYのみ更新";
        //}

        _set_slidingmark();

        //m_mpb.Refresh();
        //m_panel2.Refresh();
        G.Scroll_mpb_changed();
        m_panel1.Refresh();

        //Cursor.Position = Point.Round( m_parent.GetScreenPosFormPointOnImage(m_firstBmp) );
        //updatepos();

        G.log = log + Environment.NewLine;
    }
#endif
#if obs
        void update_pb()
    {
        var log = string.Empty;

        updatepos();
        //var posbmp = m_parent.GetPointerOnMainBmp();
        var diffBmp = PointUtil.Sub_Point(m_prevBmp , m_saveBmp);
        var diff    = PointUtil.Sub_Point(m_prev, m_save);  

        var diffx_a = Math.Abs(diff.X);
        var diffy_a = Math.Abs(diff.Y);
        var diffrange = 3;
        if (diffx_a <= diffrange && diffy_a <= diffrange) return;

        log += "diff    =" + diff.ToString() + Environment.NewLine;
        log += "diffBmp =" + diffBmp.ToString() + Environment.NewLine;

        G.debug_form.textBox_saveposX.Text = m_saveBmp.X.ToString();
        G.debug_form.textBox_saveposY.Text = m_saveBmp.Y.ToString();

        G.debug_form.textBoxSlideDiffX.Text = diffBmp.X.ToString();
        G.debug_form.textBoxSlideDiffY.Text = diffBmp.Y.ToString();

        var rel_diff = m_parent.GetPosOnMainPB(diffBmp);
        G.debug_form.textBox_reldiffX.Text = rel_diff.X.ToString();
        G.debug_form.textBox_reldiffY.Text = rel_diff.Y.ToString();

        var spos = Point.Truncate( PointUtil.Add_Point((PointF)m_saveASP ,diff));
        G.debug_form.textBox_scrollPosX.Text = spos.X.ToString();
        G.debug_form.textBox_scrollPosY.Text = spos.Y.ToString();
        //G.logAppend(m_panel.AutoScrollPosition.ToString() +":::" + spos.ToString() + Environment.NewLine);

#if xx
        // panel1の右下のスクリーンポジションから、ビットマップのポジションを求める
        // そのポジションがBMP外であれば、オーバーしていることを検知できる。
        var scpos_panel1_br = m_panel.PointToScreen( new Point( m_panel.Width,m_panel.Height));
        var bmpos_panel1_br  = m_parent.GetPointerOnMainBmp(scpos_panel1_br);

        //var BRpos_on_mainpb = m_parent.GetPanelPos(new PointF(G.bitmap_width, G.bitmap_height));  
        G.debug_form.textBox_botrightX.Text = bmpos_panel1_br.X.ToString();
        G.debug_form.textBox_botrightY.Text = bmpos_panel1_br.Y.ToString();
#endif

        var bBotVscroll   = ((m_panel1.VerticalScroll.Maximum - m_panel1.VerticalScroll.LargeChange) - m_panel1.VerticalScroll.Value) < 0;
        var bRightHscroll = ((m_panel1.HorizontalScroll.Maximum - m_panel1.HorizontalScroll.LargeChange) - m_panel1.HorizontalScroll.Value) < 0;

        /*

             m_mpb->m_panel2->m_panel1->root

             m_panel1にスクロール
             m_panel2のLocationは、m_panel1の(0,0)に固定 (マイナスにできないため)
             m_mpbはm_panel2を自由にLocationできる。      

        */


        Action panel2Clamp = ()=> {
            if (m_panel2.Height < m_mpb.Location.Y + m_mpb.Height)  m_panel2.Height = m_mpb.Location.Y + m_mpb.Height;
            if (m_panel2.Width  < m_mpb.Location.X +  m_mpb.Width ) m_panel2.Width  = m_mpb.Location.X + m_mpb.Width;
        };

        // メインPBがパネルより小さいか？
        var mpb_width_smaller_than_panel  = m_mpb.Width < m_panel1.Width;   //幅が小さい
        var mpb_height_smaller_than_panel = m_mpb.Height < m_panel1.Height; //高さが小さい 

        var mpbloc = Point.Round(PointUtil.ToPointF(m_mpb.Location));

        var bXmodified = false;
        var bYmodified = false;

        if (diff.Y!=0)
        {
            if (mpb_height_smaller_than_panel)
            {
                var addvalue = (int)-diff.Y;
                if (mpbloc.Y + addvalue < 0)
                {
                    m_mpb.Location  = PointUtil.Mod_Y(mpbloc, 0);
                    m_panel2.Height = m_mpb.Height;
                }
                else
                {
                    m_mpb.Location = PointUtil.Add_Y(mpbloc, addvalue);
                    m_panel2.Height = m_mpb.Location.Y + m_mpb.Height;
                }

                log += "小高さPBモード：addvalue = " + addvalue ;

                bYmodified = true;
            }
            else if (m_panel1.VerticalScroll.Value == 0 && diff.Y < 0)
            {
                var addvalue = (int)-diff.Y;
                m_mpb.Location = PointUtil.Add_Y(mpbloc, addvalue);
                m_panel2.Height += addvalue;
                //panel2Clamp();
                log += "SCRトップ上へモード： diff.y=" + diff.Y ;

                bYmodified = true;
            }
            else if (m_panel1.VerticalScroll.Value == 0 && m_mpb.Location.Y > 0)
            {
                var addvalue = (int)-diff.Y;
                m_mpb.Location = PointUtil.Add_Y(mpbloc, addvalue);
                m_panel2.Height += addvalue;
                //panel2Clamp();

                log += "SCRトップ下へモード：";

                bYmodified = true;
            }
            else if (bBotVscroll && diff.Y > 0)
            {
                var newy = (int)diff.Y;
                m_panel2.Height +=  newy;
                //panel2Clamp();
                m_panel1.VerticalScroll.Value = m_panel1.VerticalScroll.Maximum;

                log += "SCRボトム下へモード：";

                bYmodified = true;
            }
            else if (bBotVscroll && m_panel2.Height > m_mpb.Height && diff.Y < 0)
            {
                var newy = (int)diff.Y;
                m_panel2.Height +=  newy;
                //panel2Clamp();
                m_panel1.VerticalScroll.Value = m_panel1.VerticalScroll.Maximum;

                log += "SCRボトム上へモード：";

                bYmodified = true;
            }
        }
        if (diff.X != 0)
        {
            if (mpb_width_smaller_than_panel)
            {
                var addvalue = (int)-diff.X;
                m_mpb.Location = PointUtil.Add_X(mpbloc, addvalue);
                m_panel2.Width = m_mpb.Location.X + m_mpb.Width;
                panel2Clamp();

                log += "小幅PBモード：addvalue = " + addvalue ;

                bXmodified = true;
            }
            else if (m_panel1.HorizontalScroll.Value == 0 && diff.X < 0)
            {
                var addvalue = (int)-diff.X;
                m_mpb.Location = PointUtil.Add_X(mpbloc, addvalue);
                m_panel2.Width += addvalue;
                panel2Clamp();
                log += "SCR左端左へモード： diff.x=" + diff.X ;

                bXmodified = true;
            }
            else if (m_panel1.HorizontalScroll.Value == 0 && m_mpb.Location.X > 0)
            {
                var addvalue = (int)-diff.X;
                m_mpb.Location = PointUtil.Add_X(mpbloc, addvalue);
                m_panel2.Width += addvalue;
                panel2Clamp();

                log += "SCR左端右へモード：";

                bXmodified = true;
            }
            else if (bRightHscroll && diff.X > 0)
            {
                var newx = (int)diff.X;
                m_panel2.Width +=  newx;
                panel2Clamp();
                m_panel1.HorizontalScroll.Value = m_panel1.HorizontalScroll.Maximum;

                log += "SCR右端右へモード：";

                bXmodified = true;
            }
            else if (bRightHscroll && m_panel2.Width > m_mpb.Width && diff.X < 0)
            {
                var newx = (int)diff.X;
                m_panel2.Width +=  newx;
                panel2Clamp();
                m_panel1.HorizontalScroll.Value = m_panel1.HorizontalScroll.Maximum;

                log += "SCR右端左へモード：";

                bXmodified = true;
            }
        }


        if (bXmodified==false && bYmodified==false)
        {
            m_panel1.AutoScrollPosition = spos;
            log += "ノーマルモード：";
        }
        else if (bXmodified == true && bYmodified == false )
        {
            spos.Y = - m_panel1.AutoScrollPosition.Y;
            m_panel1.AutoScrollPosition = spos;
            log += "AutoScrollXのみ更新";
        }
        else if (bXmodified == false && bYmodified == true)
        {
            spos.X = - m_panel1.AutoScrollPosition.X;
            m_panel1.AutoScrollPosition = spos;
            log += "AutoScrollYのみ更新";
        }

        _set_slidingmark();

        //m_pb.Refresh();
        //m_panel2.Refresh();
        m_panel1.Refresh();

        Cursor.Position = Point.Round( m_parent.GetScreenPosFormPointOnImage(m_firstSave) );
        updatepos();

        G.log = log + Environment.NewLine;
    }
#endif

    void _set_slidingmark()
    {
        m_pb.Location = PointUtil.Add_XY(G.main_picturebox.PointToClient(Cursor.Position), - m_pb.Size.Width / 2, - m_pb.Size.Height / 2);
    }

    void hide_pb()
    {
        m_pb.Enabled = false;
        m_pb.Visible = false;
    }


    bool check_mouse()
    {
        return !G.mouse_down_or_up;
    }


}
