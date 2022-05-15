using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using stateview;
using G = stateview.Globals;

public partial class ItemEditControl
{
    public class Help
    {
        Hashtable m_ht;

        public void load()
        {
            m_ht = IniUtil.CreateHashtable(G.excel_convertsettings.m_help_ini);
        }

        public string Get(string itemname, string lang)
        {
            var s = IniUtil.GetValueFromHashtable(itemname,lang,m_ht);
            return s;
        }
        public string Get(string itemname, bool bJorE)
        {
            return Get(itemname, bJorE ? "jpn":"en");
        }

        public void Set(string itemname, string lang, string value)
        {
            IniUtil.SetValueFromHashtable(itemname,lang,value,ref m_ht);
        }

        public void Set(string itemname, bool bJorE, string value)
        {
            Set(itemname,bJorE ? "jpn":"en",value);
        }

        public bool UpdateIni(ItemEditControl pm )
        {
            bool bNeedUpdate = false;
            for(var r = 0; r <pm.m_dg.Rows.Count; r++)
            {
                var index = pm.m_dg[pm.CC_INDEX,r].Value?.ToString();
                if ( !(ParseUtil.ParseInt(index) >= 1 || index == pm.NEWMARK) )
                {
                    continue;
                }
                var itemname = pm.m_dg[pm.CC_NAME,r].Value?.ToString();
                if (string.IsNullOrEmpty(itemname)) continue;

                var helpe = pm.m_dg[pm.CC_HELPEN,r].Value?.ToString();
                var helpj = pm.m_dg[pm.CC_HELPJP,r].Value?.ToString();

                var help_oe = Get(itemname,false);
                var help_oj = Get(itemname,true);

                if (!StringUtil.IsEqual(helpe,help_oe))
                {
                    bNeedUpdate = true;
                    Set(itemname, false, helpe);
                }

                if (!StringUtil.IsEqual(helpj,help_oj))
                {
                    bNeedUpdate = true;
                    Set(itemname, true, helpj);
                }
            }
            if (bNeedUpdate)
            {
                var s = IniUtil.MakeOutput(m_ht);
                G.excel_convertsettings.m_help_ini = s;

                //help ini
                if (G.psgg_file_w_data)
                {
                    FileDbUtil.WriteHelp();
                }
                else
                { 
                    ExcelSaveOneSheet.WriteHelp();
                }

            }

            return bNeedUpdate;
        }
    }







}
