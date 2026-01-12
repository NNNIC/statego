# HTTP Performance Optimization and Batch API

# Goal Description
The goal is to improve the performance of the StateGo Editor's API, specifically to address the slowness caused by frequent UI redraws and history saves when creating or modifying multiple states. The solution involves:
1.  **Refactoring the API Structure:** Moving towards a standard `api/{category}/{action}` format.
2.  **Implementing a Batch API:** Creating a `system/batch` endpoint that accepts a list of commands and executes them with a single UI update at the end.
3.  **Scope Limitation:** Restricting the `state/create` API to only support generic states (type=`state`) starting with `S_`.

## User Review Required
> [!IMPORTANT]
> **Scope Limitation**: The `state/create` API will strictly enforce that state names start with `S_` and the type is `state`. This is to ensure safety and simplify the initial implementation.

## Proposed Changes
### [StateView]
#### [MODIFY] [StateBridge.cs](file:///c:/Users/gea01/Documents/psgg/psgg-editor-public/editor/m1/stateview/stateview/5900_AIIntegration/StateBridge.cs)
-   Refactor `ProcessCommand` to delegate to internal methods.
-   Implement `ProcessBatch` method.
-   Redefine `CreateState`, `DeleteState`, `EditState` to accept a `skipRedraw` parameter.
-   Define `BatchParams` and `BatchCommand` data contracts.

#### [MODIFY] [LocalServer.cs](file:///c:/Users/gea01/Documents/psgg/psgg-editor-public/editor/m1/stateview/stateview/5900_AIIntegration/LocalServer.cs)
-   Ensure it can handle larger payloads if necessary (mostly configuration).

## Proposed Changes (Completed)
 [x] Standardized API endpoints
 [x] Unified Response Format
 [x] Refactored `StateBridge.cs`
 [x] Implemented `system/batch`
 [x] Created `verify_batch_api.ps1`

## Verification Plan
### Automated Tests
-   `verify_batch_api.ps1`: 
    -   Starts `StateGo.exe` (headless or manual).
    -   Sends a batch of commands (Create S_1, Create S_2, Group S_1+S_2).
    -   Verifies states exist.
    -   Deletes them via batch.
-   `verify_helloworld.ps1` (E2E):
    -   Full flow from creating project -> batch edit -> compile -> run.
