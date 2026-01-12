# StateGo AI Integration Research

- [x] Analyze project structure and entry points <!-- id: 0 -->
    - Found `5100_ViewForm_StateControl` which indicates potential logic separation.
- [ ] Evaluate `ViewForm.cs` and `StateControl` for logic/UI separation <!-- id: 1 -->
- [x] Evaluate `ViewForm.cs` and `StateControl` for logic/UI separation <!-- id: 1 -->
- [x] Check existing CLI capabilities in `Program.cs` <!-- id: 2 -->
- [x] Formulate recommendation (Option 2: Internal) <!-- id: 3 -->
- [x] Create Implementation Plan for Option 2 <!-- id: 4 -->
- [x] User Approval of Plan (Hybrid Approach Selected) <!-- id: 5 -->

# Implementation (Hybrid / Local Server)
- [x] Implement `LocalServer.cs` (HttpListener) <!-- id: 6 -->
- [x] Create `StateBridge.cs` basic structure <!-- id: 7 -->
- [x] Connect `ViewForm` to start server <!-- id: 8 -->
- [x] Verify server responsiveness (curl test) <!-- id: 9 -->
    - [x] Build Project <!-- id: 10 -->
    - [x] Launch StateGo and test API <!-- id: 11 -->
    - [x] Debug: Resolve Start Dialog blocker <!-- id: 12 -->
    - [x] Enh: Support multiple instances (Dynamic Ports) <!-- id: 17 -->

# Enhancement (Automated Startup)
- [x] Analyze startup logic (`StateViewer/Form1.cs`) <!-- id: 13 -->
- [x] Implement CLI args for "New Project" (`/new`, `/kit`, etc.) <!-- id: 14 -->
- [x] Bypass Start Dialog on CLI request <!-- id: 15 -->
- [x] Verify headless startup with test command <!-- id: 16 -->

# Enhancement (Local API Implementation)
- [x] Impl: `GET /api/system/noop` (Ping/Ready check) <!-- id: 26 -->
- [x] Impl: `GET /api/state/list` & `GET /api/system/info` <!-- id: 20 -->
- [x] Impl: `POST /api/state/create` <!-- id: 21 -->
- [x] Impl: `POST /api/state/delete` <!-- id: 22 -->
- [x] Impl: `POST /api/state/move` <!-- id: 23 -->
- [x] Impl: `POST /api/group/create` <!-- id: 24 -->
- [x] Impl: `POST /api/system/save_and_convert` (Save & Convert) <!-- id: 27 -->
- [x] Verification: Test all endpoints <!-- id: 25 -->


