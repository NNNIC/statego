using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using G=stateview.Globals;

namespace StateViewer_filedb
{
    public partial class FileDb
    {
        #region write file db
        public void write_filedb_all()
        {
            m_state_chart.write_filedb_manager(m_statechart_manager_file);
            m_state_chart.write_filedb_statedata_files(m_statechart_data_folder);

            m_state_chart.write_filedb_bmp_files(m_statechart_bmp_folder);

            write_filedb_varous_sheets();

            m_state_chart.id_state_dirty_dic.Clear();
        }

        public void write_filedb_manager_and_dirty_statedata_only() //マネージャと更新されたステートデータのみを更新
        {
            m_state_chart.write_filedb_manager(m_statechart_manager_file);
            m_state_chart.write_filedb_statedata_dirty_files(m_statechart_data_folder);
            
            m_state_chart.id_state_dirty_dic.Clear();
        }

        private void write_filedb_varous_sheets()
        {
            write_filedb_config_sheet();
            write_filedb_tempsrc_sheet(); 
            write_filedb_tempfunc_sheet();
            write_filedb_setting_sheet(); 
            write_filedb_help_sheet();    
            write_filedb_items_sheet();    
        }
        public void write_filedb_config_sheet()   { File.WriteAllText(Path.Combine(m_various_folder,G.sheetconfig   + ".txt"), m_sheet_config_val,         Encoding.UTF8 ); }
        public void write_filedb_tempsrc_sheet()  { File.WriteAllText(Path.Combine(m_various_folder,G.sheettempsrc  + ".txt"), m_sheet_template_source_val,Encoding.UTF8 ); }
        public void write_filedb_tempfunc_sheet() { File.WriteAllText(Path.Combine(m_various_folder,G.sheettempfunc + ".txt"), m_sheet_template_func_val,  Encoding.UTF8 ); }
        public void write_filedb_setting_sheet()  { File.WriteAllText(Path.Combine(m_various_folder,G.sheetsetting  + ".txt"), m_sheet_setting_ini_val,    Encoding.UTF8 ); }
        public void write_filedb_help_sheet()     { File.WriteAllText(Path.Combine(m_various_folder,G.sheethelp     + ".txt"), m_sheet_help_val,           Encoding.UTF8 ); }
        public void write_filedb_items_sheet()    { File.WriteAllText(Path.Combine(m_various_folder,G.sheetitems    + ".txt"), m_sheet_items_val,          Encoding.UTF8 ); }
        
        public void write_filedb_bmp_files()      { m_state_chart.write_filedb_bmp_files(m_statechart_bmp_folder); }
        public void write_filedb_specified_bmp_files(List<string> hashlist) { m_state_chart.write_filedb_spefified_bmp_files(m_statechart_bmp_folder, hashlist); }
        #endregion


        #region remove
        //public void remove_unused_bmp_files() { m_state_chart.RemoveUnusedBitmap(m_statechart_bmp_folder); }
        #endregion
    }
}
