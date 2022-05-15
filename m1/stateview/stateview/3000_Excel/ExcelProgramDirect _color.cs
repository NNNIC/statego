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


    public class Libxl {
        public enum XlColor {
            COLOR_BLACK		        =8	,
            COLOR_WHITE		        =9	,
            COLOR_RED		        =10	,
            COLOR_BRIGHTGREEN       =11	,
            COLOR_BLUE		        =12	,
            COLOR_YELLOW	        =13	,
            COLOR_PINK		        =14	,
            COLOR_TURQUOISE	        =15	,
            COLOR_DARKRED	        =16	,
            COLOR_GREEN		        =17	,
            COLOR_DARKBLUE	        =18	,
            COLOR_DARKYELLOW        =19	,
            COLOR_VIOLET	        =20	,
            COLOR_TEAL		        =21	,
            COLOR_GRAY25	        =22	,
            COLOR_GRAY50	        =23	,
            COLOR_PERIWINKLE_CF     =24	,
            COLOR_PLUM_CF		    =25	,
            COLOR_IVORY_CF		    =26	,
            COLOR_LIGHTTURQUOISE_CF	=27	,
            COLOR_DARKPURPLE_CF	    =28	,
            COLOR_CORAL_CF		    =29	,
            COLOR_OCEANBLUE_CF	    =30	,
            COLOR_ICEBLUE_CF	    =31	,
            COLOR_DARKBLUE_CL	    =32	,
            COLOR_PINK_CL		    =33	,
            COLOR_YELLOW_CL		    =34	,
            COLOR_TURQUOISE_CL	    =35	,
            COLOR_VIOLET_CL		    =36	,
            COLOR_DARKRED_CL	    =37	,
            COLOR_TEAL_CL		    =38	,
            COLOR_BLUE_CL		    =39	,
            COLOR_SKYBLUE		    =40	,
            COLOR_LIGHTTURQUOISE    =41	,
            COLOR_LIGHTGREEN	    =42	,
            COLOR_LIGHTYELLOW	    =43	,
            COLOR_PALEBLUE		    =44	,
            COLOR_ROSE		        =45	,
            COLOR_LAVENDER		    =46	,
            COLOR_TAN		        =47	,
            COLOR_LIGHTBLUE		    =48	,
            COLOR_AQUA		        =49	,
            COLOR_LIME		        =50	,
            COLOR_GOLD		        =51	,
            COLOR_LIGHTORANGE	    =52	,
            COLOR_ORANGE		    =53	,
            COLOR_BLUEGRAY		    =54	,
            COLOR_GRAY40		    =55	,
            COLOR_DARKTEAL		    =56	,
            COLOR_SEAGREEN		    =57	,
            COLOR_DARKGREEN		    =58	,
            COLOR_OLIVEGREEN	    =59	,
            COLOR_BROWN		        =60	,
            COLOR_PLUM		        =61	,
            COLOR_INDIGO	        =62	,
            COLOR_GRAY80	        =63	
        }

        public static Dictionary<int,string> m_dic = null;
        public static Color GetColor(int cnum)
        {
            if (m_dic == null)
            {
                var dic = new Dictionary<int,string>();
                dic.Add(8	,"#000000");
                dic.Add(9	,"#FFFFFF");
                dic.Add(10	,"#FF0000");
                dic.Add(11	,"#00FF00");
                dic.Add(12	,"#0000FF");
                dic.Add(13	,"#FFFF00");
                dic.Add(14	,"#FF00FF");
                dic.Add(15	,"#00FFFF");
                dic.Add(16	,"#800000");
                dic.Add(17	,"#008000");
                dic.Add(18	,"#000080");
                dic.Add(19	,"#808000");
                dic.Add(20	,"#800080");
                dic.Add(21	,"#008080");
                dic.Add(22	,"#C0C0C0");
                dic.Add(23	,"#808080");
                dic.Add(24	,"#9999FF");
                dic.Add(25	,"#993366");
                dic.Add(26	,"#FFFFCC");
                dic.Add(27	,"#CCFFFF");
                dic.Add(28	,"#660066");
                dic.Add(29	,"#FF8080");
                dic.Add(30	,"#0066CC");
                dic.Add(31	,"#CCCCFF");
                dic.Add(32	,"#000080");
                dic.Add(33	,"#FF00FF");
                dic.Add(34	,"#FFFF00");
                dic.Add(35	,"#00FFFF");
                dic.Add(36	,"#800080");
                dic.Add(37	,"#800000");
                dic.Add(38	,"#008080");
                dic.Add(39	,"#0000FF");
                dic.Add(40	,"#00CCFF");
                dic.Add(41	,"#CCFFFF");
                dic.Add(42	,"#CCFFCC");
                dic.Add(43	,"#FFFF99");
                dic.Add(44	,"#99CCFF");
                dic.Add(45	,"#FF99CC");
                dic.Add(46	,"#CC99FF");
                dic.Add(47	,"#FFCC99");
                dic.Add(48	,"#3366FF");
                dic.Add(49	,"#33CCCC");
                dic.Add(50	,"#99CC00");
                dic.Add(51	,"#FFCC00");
                dic.Add(52	,"#FF9900");
                dic.Add(53	,"#FF6600");
                dic.Add(54	,"#666699");
                dic.Add(55	,"#969696");
                dic.Add(56	,"#003366");
                dic.Add(57	,"#339966");
                dic.Add(58	,"#003300");
                dic.Add(59	,"#333300");
                dic.Add(60	,"#993300");
                dic.Add(61	,"#993366");
                dic.Add(62	,"#333399");
                dic.Add(63	,"#333333");
                dic.Add(-1,"");

                m_dic = dic;
            }

            if (!m_dic.ContainsKey(cnum))
            {
                return Color.White;
            }

            return ColorTranslator.FromHtml(m_dic[cnum]);
        }

        public static string GetColorHex(int cnum)
        {
            if (m_dic==null)
            {
                GetColor((int)XlColor.COLOR_WHITE);
            }
            if (m_dic.ContainsKey(cnum))
            {
                return m_dic[cnum];
            }
            
            return null;
        }
        
    }

    public partial class ExcelProgramDirect
    {

    }
}
