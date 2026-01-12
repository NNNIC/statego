# PSGG File Format Specification

The `.psgg` file allows StateGo to persist state machine data, project settings, and templates in a single text-based file.

## 1. High-Level Structure
The file is divided into multiple **Sheets**, separated by a unique GUID marker line.

**Separator Format:**
```text
------#======*<Guid(UUID)>*======#------
```
*Note: The UUID changes between sections but follows this pattern.*

## 2. Sheets
Each section starts with `sheet={Type}`.

| Sheet Type | Description |
| :--- | :--- |
| **`state-chart`** | Core data containing state definitions, positions, and connections. |
| **`config`** | Editor view settings (zoom, scroll position, flags). |
| **`setting.ini`** | Project configuration (paths, kits, build settings). |
| **`template-source`** | Template for the main source file structure. |
| **`template-statefunc`** | Template for individual state functions. |
| **`itemsinfo`** | Metadata about item types (input methods, selection lists). |
| **`help`** | Help text references. |

## 3. State Chart Data Model (`state-chart`)
The State Chart is structured like a database table or spreadsheet, decoupling logical names from internal IDs.

### 3.1 ID Lists
- **`nameid_list`**: The "Columns" or properties of a state (e.g., `state-typ`, `nextstate`, `embed`).
- **`stateid_list`**: The "Rows" or actual state instances.

### 3.2 ID Mappings
Use the dictionaries to resolve IDs to human-readable names.
- **`[id_name_dic]`**: Maps Property IDs (`nXXX`) to Name (`state`, `thumbnail`, etc.).
  - `n001=thumbnail`
  - `n002=state`
- **`[id_state_dic]`**: Maps State IDs (`sXXX`) to State Names.
  - `s0001=S_START`

### 3.3 State Definitions
Each state has its own INI section `[sXXX]`. Inside, properties are assigned using `nXXX` keys.

**Example:**
```ini
[s0001]
n002=S_START        ; State Name
n01c=start          ; State Type
n006=S_NEXT         ; Next State
n01a=100,200        ; Position (x,y)
```

## 4. Syntax Rules
- **INI-Style**: `Key=Value` and `[Section]`.
- **Multiline Constants**: Enclosed in `@@@`.
  ```ini
  n001=@@@
  Line 1
  Line 2
  @@@
  ```
- **Comments**: Start with `;`.
- **Special Markers**: 
  - `###VARIOUS-CONTENTS-BEGIN###` and `###VARIOUS-CONTENTS-END###` are used in non-chart sheets to encapsulate content.

## 5. Common Properties (Typical mapping)
*Exact IDs vary per file, check `[id_name_dic]`.*
- `!pos` (`n01a` often): Coordinates `x,y`.
- `!uuid` (`n01b` often): Unique ID for the state object.
- `state`: The display name of the state.
- `nextstate`: The default transition target.
- `embed`: Code embedded at the beginning of the state function.
- `update`: Code executed in the update loop.
