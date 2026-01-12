using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization;
using G=stateview.Globals;
using WordStorage;

namespace stateview._5900_AIIntegration
{
    public class StateBridge
    {
        #region Public Interface
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
        #endregion

        #region Internal Dispatcher
        private static string _ProcessCommand(string command, string data, bool skipRedraw = false)
        {
            try 
            {
                object resultData = null;
                string successMsg = null;

                switch(command.ToLower())
                {
                    // --- System ---
                    case "system/batch":
                        return ProcessBatch(data); // Batch handles its own response structure
                    case "system/info":
                        resultData = GetSystemInfo();
                        break;
                    case "system/noop":
                        successMsg = "ok";
                        break;
                    case "system/save_and_convert":
                        successMsg = SaveAndConvert();
                        break;
                    case "system/reset":
                        successMsg = Reset();
                        break;
                    
                    // --- State ---
                    case "state/list":
                        resultData = GetStateList();
                        break;
                    case "state/create":
                        successMsg = CreateState(data, skipRedraw);
                        break;
                    case "state/delete":
                        successMsg = DeleteState(data, skipRedraw);
                        break;
                    case "state/move":
                        successMsg = MoveState(data, skipRedraw);
                        break;
                    case "state/edit":
                        successMsg = EditState(data, skipRedraw);
                        break;
                    
                    // --- Group ---
                    case "group/create":
                         successMsg = CreateGroup(data, skipRedraw);
                         break;

                    default:
                        throw new ArgumentException("unknown command: " + command);
                }

                if (resultData != null)
                {
                   return JsonUtil.Serialize(new ApiResponse { success = true, data = resultData });
                }
                else
                {
                   return JsonUtil.Serialize(new ApiResponse { success = true, data = successMsg ?? "ok" });
                }
            }
            catch (Exception ex)
            {
                return JsonUtil.Serialize(new ApiResponse { success = false, error = ex.Message });
            }
        }
        #endregion

        #region Batch Processing
        private static string ProcessBatch(string data)
        {
            var p = (BatchParams)JsonUtil.Deserialize(data, typeof(BatchParams));
            if (p.commands == null || p.commands.Count == 0) throw new ArgumentException("no commands provided");

            List<object> results = new List<object>();

            // Execute all commands without redraw
            foreach(var cmd in p.commands)
            {
                try {
                    string cmdName = cmd.command.ToLower();
                    bool success = false;
                    
                    switch(cmdName)
                    {
                         case "state/create": 
                             if (cmd.create_params == null) throw new ArgumentException("create_params missing");
                             // Re-serialize params because our internal methods expect a JSON string
                             CreateState(JsonUtil.Serialize(cmd.create_params), true); 
                             break;
                             
                         case "state/delete": 
                             if (cmd.delete_params == null) throw new ArgumentException("delete_params missing");
                             DeleteState(JsonUtil.Serialize(cmd.delete_params), true); 
                             break;
                             
                         case "state/move":   
                             if (cmd.move_params == null) throw new ArgumentException("move_params missing");
                             MoveState(JsonUtil.Serialize(cmd.move_params), true); 
                             break;
                             
                         case "state/edit":   
                             if (cmd.edit_params == null) throw new ArgumentException("edit_params missing");
                             EditState(JsonUtil.Serialize(cmd.edit_params), true); 
                             break;
                             
                         case "group/create": 
                             if (cmd.group_params == null) throw new ArgumentException("group_params missing");
                             CreateGroup(JsonUtil.Serialize(cmd.group_params), true); 
                             break;
                        
                         case "system/reset":
                             Reset();
                             break;

                         case "system/save_and_convert":
                             SaveAndConvert();
                             break;
                             
                         default: throw new ArgumentException("Command not supported in batch: " + cmdName);
                    }
                    results.Add(new { command = cmdName, success = true });
                }
                catch(Exception ex)
                {
                     throw new Exception($"Batch failed at {cmd.command}: {ex.Message}");
                }
            }

            // Finally Redraw
            G.req_redraw_force();

            return JsonUtil.Serialize(new ApiResponse { success = true, data = results });
        }
        #endregion

        #region Implementation Methods
        
        // Return object (list of StateData)
        private static List<StateData> GetStateList()
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
                sd.branch = G.excel_program.GetString(s, G.STATENAME_branch); 
                
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
            return list;
        }

        private static SystemInfoResponse GetSystemInfo()
        {
            return new SystemInfoResponse {
                canvas_width = G.bitmap_width,
                canvas_height = G.bitmap_height,
                current_dir = G.node_get_cur_dirpath()
            };
        }

        private static string CreateState(string data, bool skipRedraw)
        {
            var p = (CreateStateParams)JsonUtil.Deserialize(data, typeof(CreateStateParams));
            if (string.IsNullOrEmpty(p.name)) throw new ArgumentException("name required");

            // Scope Limitation: Must start with S_
            if (!p.name.StartsWith("S_")) throw new ArgumentException("State name must start with 'S_' in this version.");

            // Scope Limitation: Type must be empty or "state"
            string typ = p.type; 
            if (string.IsNullOrEmpty(typ)) typ = "state";
            if (typ != "state") throw new ArgumentException("Only generic 'state' type is supported in this version.");

            // Validate name 
            if (!stateview.StateUtil.IsValidStateName(p.name)) throw new ArgumentException("invalid state name");
            if (G.excel_program.CheckExists(p.name)) throw new ArgumentException("state already exists");

            var newstate = G.excel_program.NewState(p.name, G.node_get_cur_dirpath());
            
            G.excel_program.SetString(newstate, G.STATENAME_statetyp, typ);
            
            if (!string.IsNullOrEmpty(p.comment))
            {
                G.excel_program.SetString(newstate, G.STATENAME_statecmt, p.comment);
            }

            var pt = new PointF(p.x, p.y);
            G.UpdateExcelPos(newstate, pt, true);
            History2.SaveForce_new(newstate);

            if (!skipRedraw) G.req_redraw_force();

            return "created " + newstate;
        }

        private static string DeleteState(string data, bool skipRedraw)
        {
            var p = (DeleteStateParams)JsonUtil.Deserialize(data, typeof(DeleteStateParams));
             if (string.IsNullOrEmpty(p.name)) throw new ArgumentException("name required");
            
            if (!G.excel_program.CheckExists(p.name)) throw new ArgumentException("state not found");

            G.excel_program.Delete(p.name);
            History2.SaveForce_delete(p.name);

            if (!skipRedraw) G.req_redraw_force();

             return "deleted " + p.name;
        }

        private static string MoveState(string data, bool skipRedraw)
        {
            var p = (MoveStateParams)JsonUtil.Deserialize(data, typeof(MoveStateParams));
            if (string.IsNullOrEmpty(p.name)) throw new ArgumentException("name required");
             if (!G.excel_program.CheckExists(p.name)) throw new ArgumentException("state not found");

            // Avoid moving S_START/S_END if that's preferred, but for now allow it.

            var pt = new PointF(p.x, p.y);
            G.UpdateExcelPos(p.name, pt, true);
            
            if (!skipRedraw) G.req_redraw_force();

            return "moved " + p.name;
        }

        private static string CreateGroup(string data, bool skipRedraw)
        {
            var p = (CreateGroupParams)JsonUtil.Deserialize(data, typeof(CreateGroupParams));
            if (string.IsNullOrEmpty(p.group_name)) throw new ArgumentException("group_name required");
            if (p.states == null || p.states.Count == 0) throw new ArgumentException("states list required");

            foreach(var s in p.states) {
                 if (!G.excel_program.CheckExists(s)) throw new ArgumentException("state not found: " + s);
            }
            
            string clickState = p.states[0]; 
            G.node_grouping(p.group_name, p.states, clickState, p.comment);
            
            History2.SaveForce_grouping("Make g:" + p.group_name);

            if (!skipRedraw) G.req_redraw_force();

            return "created group " + p.group_name;
        }

        private static string EditState(string data, bool skipRedraw)
        {
            var p = (EditStateParams)JsonUtil.Deserialize(data, typeof(EditStateParams));
            if (string.IsNullOrEmpty(p.name)) throw new ArgumentException("name required");
            
            // Allow implied creation for EditState? Plan says restricted creation.
            // Let's stick to update only for safety, or strict creation check.
            bool isNew = false;
            if (!G.excel_program.CheckExists(p.name))
            {
                // Enforce creation rules if new
                if (!p.name.StartsWith("S_")) throw new ArgumentException("New state name must start with 'S_'.");
                if (!stateview.StateUtil.IsValidStateName(p.name)) throw new ArgumentException("invalid state name");

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
                 G.UpdateExcelPos(p.name, new PointF(100, 100), true);
            }
            
            if (!skipRedraw) G.req_redraw_force();

            return (isNew ? "created and " : "") + "updated " + p.name;
        }

        private static string SaveAndConvert()
        {
            if (G.view_form == null) throw new InvalidOperationException("ViewForm is not active");
            
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

            return "save and convert started";
        }

        private static string Reset()
        {
            var list = new List<string>(G.excel_program.GetStateList());
            foreach (var s in list)
            {
                G.excel_program.Delete(s);
            }

            Action<string, string, int, int, string, string> create_state = (name, type, x, y, comment, next) => {
                var newstate = G.excel_program.NewState(name, "/");
                G.excel_program.SetString(newstate, G.STATENAME_statetyp, type);
                G.excel_program.SetString(newstate, G.STATENAMESYS_pos, string.Format("{0},{1}", x, y));
                if (!string.IsNullOrEmpty(next))
                {
                    G.excel_program.SetString(newstate, G.STATENAME_nextstate, next);
                }
                G.excel_program.SetString(newstate, G.STATENAME_statecmt, comment);
            };

            create_state("S_START", WordStorage.Store.state_typ_start, 100, 100, "", "S_END");
            create_state("S_END", WordStorage.Store.state_typ_end, 500, 100, "", null);

            History2.SaveForce_modify_value("Delete All");
            G.node_enter_group("/");
            G.req_redraw_force();

            return "reset completed";
        }
        #endregion

        #region Data Contracts
        [DataContract]
        class ApiResponse {
            [DataMember] public bool success;
            [DataMember] public object data;  // Can be string, list, object
            [DataMember] public string error;
        }

        [DataContract]
        class StateData { 
            [DataMember] public string name; 
            [DataMember] public string type; 
            [DataMember] public string comment;
            [DataMember] public float x; // Changed to match JS conventions often preferred, but C# serialized fields usually match
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
        class BatchParams {
            [DataMember] public List<BatchCommand> commands;
        }

        [DataContract]
        class BatchCommand {
            [DataMember] public string command;
            [DataMember] public CreateStateParams create_params;
            [DataMember] public DeleteStateParams delete_params;
            [DataMember] public MoveStateParams   move_params;
            [DataMember] public EditStateParams   edit_params;
            [DataMember] public CreateGroupParams group_params;
        }
        
        [DataContract]
        class CreateStateParams {
            [DataMember] public string name;
            [DataMember] public string type; 
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
            [DataMember] public Dictionary<string,string> @params; // Might need `UseSimpleDictionaryFormat` properties if using standard JSON
            [DataMember] public float? x;
            [DataMember] public float? y;
        }
        #endregion
    }
}
