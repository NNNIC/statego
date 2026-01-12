# Batch API Implementation and Verification

## Goal
Optimize API performance by implementing a batch processing mechanism and reorganizing the API structure. This allows multiple commands (create, edit, delete, group) to be sent in a single request, triggering only one UI redraw and history save at the end.

## Key Changes

### 1. API Structure Refactoring
*   **Standardized Endpoints:** `/api/{category}/{action}` (e.g., `state/create`, `system/batch`).
*   **Unified Response:** `{"success": true, "data": ...}`.
*   **Internal Methods:** Separated core logic from UI/History overhead in `StateBridge.cs`. Methods like `CreateState` now accept a `skipRedraw` flag.

### 2. Batch API (`system/batch`)
*   **Endpoint:** POST `/api/system/batch`
*   **Payload:**
    ```json
    {
      "commands": [
        { "command": "system/reset" },
        { "command": "state/create", "create_params": { ... } },
        { "command": "state/edit", "edit_params": { ... } }
      ]
    }
    ```
*   **Processing:** Executes all commands internally with `skipRedraw=true`. Calls `G.req_redraw_force()` only once after all commands succeed.

### 3. Scope Limitation
*   **State Names:** Must start with `S_`.
*   **Types:** Only generic `state` type is supported for creation via API.

## Verification

### Automated Verification Scripts
Two PowerShell scripts were created to verify the implementation.

#### 1. Batch API Test (`verify_batch_api.ps1`)
*   **Location:** `editor/m1/stateview/verify_batch_api.ps1`
*   **Tests:**
    *   Creation of multiple states (`S_Batch1`, `S_Batch2`).
    *   Grouping of states.
    *   Deletion (Cleanup).
*   **Result:** Verified that objects are created and deleted correctly in a single batch.

#### 2. Hello World E2E (`verify_helloworld.ps1`)
*   **Location:** `editor/TestBed/verify_helloworld.ps1`
*   **Tests:**
    *   **Headless Project Creation:** Launches `StateGo.exe` with `/new` to create `test1Control`.
    *   **Batch Modification:**
        1.  `system/reset`
        2.  Create `S_SAYHELLO`
        3.  Edit `S_SAYHELLO` to add `System.Console.WriteLine("Hello World");`
        4.  Edit `S_START` to point to `S_SAYHELLO`
        5.  `system/save_and_convert`
    *   **Compilation:** Creates a runner C# file and compiles with `csc.exe`.
    *   **Execution:** Runs the compiled executable.
*   **Result:** Output matched "Hello World".

## Artifacts
*   **Implementation Plan:** `implementation_plan.md`
*   **Task List:** `task.md`
*   **Scripts:** `editor/TestBed/verify_helloworld.ps1`, `editor/m1/stateview/verify_batch_api.ps1`
