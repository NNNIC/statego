using System.Windows.Forms;
using ScintillaNET;
using System.Drawing;
using System;
using G=stateview.Globals;
using stateview;

public class ScintillaBox : Panel
{
    public Scintilla TextArea { get; private set; }
    Form      form;

    public bool ReadOnly { get { return TextArea.ReadOnly; } internal set {
            TextArea.ReadOnly = value;
            set_colors();
        } }

    public Action SaveAction = null;


    public override string Text
    {
        get
        {
            return TextArea.Text;
        }

        set
        {
            var save = TextArea.ReadOnly;
            TextArea.ReadOnly = false;
            TextArea.Text = value;
            TextArea.ReadOnly = save;
        }
    }

    bool m_readonly_is_normal; // リードオンリー画面をノーマル画面の色で設定する
    bool m_save_keypreviewstate;
    public void Init(Form iform, int zoom,  bool readonly_is_normal)
    {
        form = iform;
        m_readonly_is_normal = readonly_is_normal;
        // CREATE CONTROL
        TextArea = new ScintillaNET.Scintilla();
        this.Controls.Add(TextArea);

        // BASIC CONFIG
        TextArea.Dock = DockStyle.Fill;

        // INITIAL VIEW CONFIG
        TextArea.WrapMode = WrapMode.None;
        TextArea.IndentationGuides = IndentView.LookBoth;

        // STYLING
        TextArea.SetSelectionBackColor(true, Color.FromArgb(255,173,214,255));

        // INIT HOTKEYS
        m_save_keypreviewstate = form.KeyPreview;
        form.KeyPreview = true;

        //TextArea.KeyDown += Zoom;
        TextArea.KeyDown += Save;

        TextArea.ClearCmdKey(Keys.Control | Keys.S);
        

        set_colors();

        if (zoom != int.MinValue)
        {
            TextArea.Zoom = zoom;
        }

        var s = TextArea.PropertyNames();
        G.NoticeToUser(s.ToString());
    }
    ScintillaDef m_def;
    void set_colors()
    {
        if (m_def == null)
        {
            m_def = new ScintillaDef();
            m_def.Setup(TextArea,m_readonly_is_normal);
        }
        else
        {
            m_def.SetColors(TextArea);
        }

        if (!G.option_lexical_color_onoff)
        {
            TextArea.Lexer = Lexer.Null;
        }
    }
    public void RedrawWLexer()
    {
        if (G.option_lexical_color_onoff)
        {
            m_def.set_lexer_keywords(TextArea); // Null後は、Wordの再設定必須
        }
        else 
        {
            TextArea.Lexer = Lexer.Null;
        }
        var s = Text;
        Text = s;
    }

    //void set_bg()
    //{
    //    var col = TextArea.ReadOnly ? m_background_readonly : m_background_normal;
    //    TextArea.Styles[Style.Default].BackColor = col;
    //}
    public int GetZoom()
    {
        if (TextArea != null)
        {
            return TextArea.Zoom;
        }
        return int.MinValue;
    }
    public void UnInit()
    {
        TextArea.KeyDown -= Save;
        //TextArea.KeyDown -= Zoom;
        form.KeyPreview = m_save_keypreviewstate;
        //TextArea.Dispose();
        //TextArea = null;
    }
    //private void Zoom(object sender, KeyEventArgs e)
    //{
    //    //if (this.Focused == false) return;
    //    if (e.Control == false) return;
    //    if (e.KeyCode == Keys.Oemplus)
    //    {
    //        TextArea.ZoomIn();
    //    }
    //    else if (e.KeyCode == Keys.OemMinus)
    //    {
    //        TextArea.ZoomOut();
    //    }
    //    //else if (e.KeyCode == Keys.Multiply)
    //    //{
    //    //    TextArea.Zoom = 0;
    //    //}
    //}
    private void Save(object sender, KeyEventArgs e)
    {
        if (e.Control == false) return;
        if (e.KeyCode == Keys.S)
        {
            //G.NoticeToUser("Control + S");
            SaveAction?.Invoke();
        }
    }

    public new void Focus()
    {
        TextArea.Focus();
    }
}
