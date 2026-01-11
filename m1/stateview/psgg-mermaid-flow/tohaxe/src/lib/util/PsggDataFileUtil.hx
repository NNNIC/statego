package lib.util;
using StringTools;
import system.*;
import anonymoustypes.*;

class PsggDataFileUtil
{
    public static function ReadPsgg(path:String):lib.util.PsggDataFileUtil_Item
    {
        var buf:String = psgg.HxFile.ReadAllText_String_Encoding(path, system.text.Encoding.UTF8);
        return ReadPsggData(buf);
    }
    public static function ReadPsggData(data:String):lib.util.PsggDataFileUtil_Item
    {
        var item:lib.util.PsggDataFileUtil_Item = new lib.util.PsggDataFileUtil_Item();
        var buf:String = data;
        var list:Array<String> = new Array<String>();
        while (buf != null && buf.length > 1)
        {
            var index:Int = buf.indexOf(lib.wordstrage.Store.PSGG_MARK_PREFIX, 1);
            if (index < 0)
            {
                break;
            }
            var pick:String = buf.substr(0, index);
            list.push(pick);
            buf = buf.substr(index);
        }
        if (buf != null && buf.length > 0)
        {
            list.push(buf);
        }
        var i:Int = 0;
        for (listitem in list)
        {
            if (i == 0)
            {
                item.m_header_buf = listitem;
            }
            else if (listitem.indexOf(lib.wordstrage.Store.PSGG_MARK_STATECHART_SHEET) >= 0)
            {
                item.m_chart_buf = listitem;
            }
            else if (listitem.indexOf(lib.wordstrage.Store.PSGG_MARK_VARIOUS_SHEET) >= 0)
            {
                if (listitem.indexOf("sheet=config") >= 0)
                {
                    item.m_config_buf = listitem;
                }
                else if (listitem.indexOf("sheet=template-source") >= 0)
                {
                    item.m_tmpsrc_buf = listitem;
                }
                else if (listitem.indexOf("sheet=template-statefunc") >= 0)
                {
                    item.m_tmpfnc_buf = listitem;
                }
                else if (listitem.indexOf("sheet=setting.ini") >= 0)
                {
                    item.m_setting_buf = listitem;
                }
                else if (listitem.indexOf("sheet=help") >= 0)
                {
                    item.m_help_buf = listitem;
                }
                else if (listitem.indexOf("sheet=itemsinfo") >= 0)
                {
                    item.m_iteminf_buf = listitem;
                }
            }
            else if (listitem.indexOf(lib.wordstrage.Store.PSGG_MARK_BITMAP_DATA) >= 0)
            {
                item.m_bitmap_buf = listitem;
            }
            i++;
        }
        var get_tmp:(String -> String) = function (s:String):String
        {
            var begin:String = lib.wordstrage.Store.PSGG_MARK_VARIOUS_BEGIN;
            var end:String = lib.wordstrage.Store.PSGG_MARK_VARIOUS_END;
            var si:Int = s.indexOf(begin);
            if (si < 0)
            {
                return null;
            }
            var s1:String = s.substr(si + begin.length);
            var ei:Int = s1.indexOf(end);
            if (ei < 0)
            {
                return null;
            }
            return s1.substr(0, ei);
        }
        ;
        item.m_tmpsrc = get_tmp(item.m_tmpsrc_buf);
        item.m_tmpfnc = get_tmp(item.m_tmpfnc_buf);
        return item;
    }
    public function new()
    {
    }
}
