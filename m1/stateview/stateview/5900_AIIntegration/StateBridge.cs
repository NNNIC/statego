using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization;
using G=stateview.Globals;

namespace stateview._5900_AIIntegration
{
    public class StateBridge
    {
        public static string ProcessCommand(string command, string data)
        {
            // Thread safety: Execute on UI thread if needed
             if (G.view_form != null && G.view_form.InvokeRequired)
            {
                string result = "";
                G.view_form.Invoke(new MethodInvoker(() => {
                    result = _ProcessCommand(command, data);
                }));
                return result;
            }
            else
            {
                return _ProcessCommand(command, data);
            }
        }

        private static string _ProcessCommand(string command, string data)
        {
            try 
            {
                switch(command.ToLower())
                {
                    case "state/list":
                        return GetStateList();
                    case "system/info":
                        return GetSystemInfo();
                    case "system/noop":
                        return JsonUtil.Serialize(new SuccessResponse { success = "ok" });
                    case "state/create":
                        return CreateState(data);
                    case "state/delete":
                        return DeleteState(data);
                    case "state/move":
                        return MoveState(data);
                    case "group/create":
                        return CreateGroup(data);
                    case "system/save_and_convert":
                        return SaveAndConvert();
                    case "state/edit":
                        return EditState(data);
                    default:
                        return JsonUtil.Serialize(new ErrorResponse { error = "unknown command: " + command });
                }
            }
            catch (Exception ex)
            {
                return JsonUtil.Serialize(new ErrorResponse { error = ex.Message });
            }
        }

        private static string SaveAndConvert()
        {
            if (G.view_form == null) throw new InvalidOperationException("ViewForm is not active");
            
            // Bypass confirmation dialogs
            bool backup_confirm = G.option_convert_with_confirm;
            G.option_convert_with_confirm = false;
            try
            {
                G.view_form.SaveAndRun(false);
            }
            finally
            {
                G.option_convert_with_confirm = backup_confirm;
            }

            return JsonUtil.Serialize(new SuccessResponse { success = "save and convert started" });
        }

        #region Data Contracts
        [DataContract]
        class ErrorResponse { [DataMember] public string error; }
        
        [DataContract]
        class SuccessResponse { [DataMember] public string success; }

        [DataContract]
        class StateListResponse { [DataMember] public List<StateData> states; }

        [DataContract]
        class StateData { 
            [DataMember] public string name; 
            [DataMember] public string type; 
            [DataMember] public string comment;
            [DataMember] public float x;
            [DataMember] public float y;
            [DataMember] public string next;
            [DataMember] public string gosub;
            [DataMember] public string branch;
        }

        [DataContract]
        class SystemInfoResponse { 
            [DataMember] public int canvas_width;
            [DataMember] public int canvas_height;
            [DataMember] public string current_dir;
        }

        [DataContract]
        class CreateStateParams {
            [DataMember] public string name;
            [DataMember] public string type; // start, end, loop, gosub, etc. default: state
            [DataMember] public float x;
            [DataMember] public float y;
            [DataMember] public string comment;
        }

        [DataContract]
        class DeleteStateParams {
            [DataMember] public string name;
        }

        [DataContract]
        class MoveStateParams {
            [DataMember] public string name;
            [DataMember] public float x;
            [DataMember] public float y;
        }

        [DataContract]
        class CreateGroupParams {
            [DataMember] public string group_name;
            [DataMember] public List<string> states;
            [DataMember] public string comment;
        }

        [DataContract]
        class EditStateParams {
            [DataMember] public string name;
            [DataMember] public Dictionary<string,string> @params;
            [DataMember] public float? x;
            [DataMember] public float? y;
        }
        #endregion

        #region Implementation
        private static string GetStateList()
        {
            var list = new List<StateData>();
            var all_states = G.excel_program.GetStateList();
            
            foreach(var s in all_states)
            {
                var sd = new StateData();
                sd.name = s;
                sd.type = G.excel_program.GetString(s, G.STATENAME_statetyp);
                sd.comment = G.excel_program.GetString(s, G.STATENAME_statecmt);
                sd.next = G.excel_program.GetString(s, G.STATENAME_nextstate);
                sd.gosub = G.excel_program.GetString(s, G.STATENAME_gosubstate);
                sd.branch = G.excel_program.GetString(s, G.STATENAME_branch); // Raw branch string
                
                if (G.state_location_list != null && G.state_location_list.ContainsKey(s))
                {
                    var p = G.state_location_list[s];
                    sd.x = p.X;
                    sd.y = p.Y;
                }
                else
                {
                    var p = G.get_excel_position(s);
                    if (p != null)
                    {
                        sd.x = p.Value.X;
                        sd.y = p.Value.Y;
                    }
                }

                list.Add(sd);
            }

            return JsonUtil.Serialize(new StateListResponse { states = list });
        }

        private static string GetSystemInfo()
        {
            return JsonUtil.Serialize(new SystemInfoResponse {
                canvas_width = G.bitmap_width,
                canvas_height = G.bitmap_height,
                current_dir = G.node_get_cur_dirpath()
            });
        }

        private static string CreateState(string data)
        {
            var p = (CreateStateParams)JsonUtil.Deserialize(data, typeof(CreateStateParams));
            if (string.IsNullOrEmpty(p.name)) throw new ArgumentException("name required");

            // Validate name (simple check)
            if (!stateview.StateUtil.IsValidStateName(p.name)) throw new ArgumentException("invalid state name");
            if (G.excel_program.CheckExists(p.name)) throw new ArgumentException("state already exists");

            string typ = p.type; 
            if (string.IsNullOrEmpty(typ)) typ = "state"; // default
            
            var newstate = G.excel_program.NewState(p.name, G.node_get_cur_dirpath());
            
            if (!string.IsNullOrEmpty(typ))
            {
                G.excel_program.SetString(newstate, G.STATENAME_statetyp, typ);
            }
            
            if (!string.IsNullOrEmpty(p.comment))
            {
                G.excel_program.SetString(newstate, G.STATENAME_statecmt, p.comment);
            }

            var pt = new PointF(p.x, p.y);
            G.UpdateExcelPos(newstate, pt, true);
            History2.SaveForce_new(newstate);

            G.req_redraw_force();

            return JsonUtil.Serialize(new SuccessResponse { success = "created " + newstate });
        }

        private static string DeleteState(string data)
        {
            var p = (DeleteStateParams)JsonUtil.Deserialize(data, typeof(DeleteStateParams));
             if (string.IsNullOrEmpty(p.name)) throw new ArgumentException("name required");
            
            if (!G.excel_program.CheckExists(p.name)) throw new ArgumentException("state not found");

            G.excel_program.Delete(p.name);
            History2.SaveForce_delete(p.name);

            G.req_redraw_force();

             return JsonUtil.Serialize(new SuccessResponse { success = "deleted " + p.name });
        }

        private static string MoveState(string data)
        {
            var p = (MoveStateParams)JsonUtil.Deserialize(data, typeof(MoveStateParams));
            if (string.IsNullOrEmpty(p.name)) throw new ArgumentException("name required");
             if (!G.excel_program.CheckExists(p.name)) throw new ArgumentException("state not found");

            var pt = new PointF(p.x, p.y);
            G.UpdateExcelPos(p.name, pt, true);
            
            G.req_redraw_force();

            return JsonUtil.Serialize(new SuccessResponse { success = "moved " + p.name });
        }

        private static string CreateGroup(string data)
        {
            var p = (CreateGroupParams)JsonUtil.Deserialize(data, typeof(CreateGroupParams));
            if (string.IsNullOrEmpty(p.group_name)) throw new ArgumentException("group_name required");
            if (p.states == null || p.states.Count == 0) throw new ArgumentException("states list required");

            // Verify all states exist
            foreach(var s in p.states) {
                 if (!G.excel_program.CheckExists(s)) throw new ArgumentException("state not found: " + s);
            }
            
            string clickState = p.states[0]; // arbitrary pivot
            G.node_grouping(p.group_name, p.states, clickState, p.comment);
            
            History2.SaveForce_grouping("Make g:" + p.group_name);

            G.req_redraw_force();

            return JsonUtil.Serialize(new SuccessResponse { success = "created group " + p.group_name });
        }

        private static string EditState(string data)
        {
            var p = (EditStateParams)JsonUtil.Deserialize(data, typeof(EditStateParams));
            if (string.IsNullOrEmpty(p.name)) throw new ArgumentException("name required");
            if (!stateview.StateUtil.IsValidStateName(p.name)) throw new ArgumentException("invalid state name");

            bool isNew = false;
            if (!G.excel_program.CheckExists(p.name))
            {
                G.excel_program.NewState(p.name, G.node_get_cur_dirpath());
                History2.SaveForce_new(p.name);
                isNew = true;
            }

            int updateCount = 0;
            if (p.@params != null)
            {
                foreach(var kv in p.@params)
                {
                    if (kv.Key.StartsWith("!"))
                    {
                        // Modification of system items starting with '!' is prohibited via params dictionary.
                        // Use dedicated fields (like x, y) for system properties instead.
                        throw new ArgumentException("Cannot modify system property via params: " + kv.Key);
                    }
                    G.excel_program.SetString(p.name, kv.Key, kv.Value);
                    updateCount++;
                }
            }

            if (p.x.HasValue && p.y.HasValue)
            {
                G.UpdateExcelPos(p.name, new PointF(p.x.Value, p.y.Value), true);
            }
            else if (isNew)
            {
                 // Default position for newly created states if x, y are omitted.
                 G.UpdateExcelPos(p.name, new PointF(100, 100), true);
            }
            
            G.req_redraw_force();

            return JsonUtil.Serialize(new SuccessResponse { success = (isNew ? "created and " : "") + "updated " + p.name + " (" + updateCount + " params)" });
        }
        #endregion
    }
}
