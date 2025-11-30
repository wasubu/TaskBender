# TaskBender Development Roadmap

## Phase 1: Project Setup (VS Code)
- [ ] **Initialize Project**
    - Open VS Code Terminal.
    - Run: `dotnet new wpf -n TaskBender`
    - Run: `cd TaskBender`
- [ ] **Install Nuget Packages**
    - *UI Styling:* `dotnet add package Wpf.Ui`
    - *Database:* `dotnet add package LiteDB`
    - *System Tray:* `dotnet add package Hardcodet.NotifyIcon.Wpf`
- [ ] **Clean Startup**
    - Remove `StartupUri="MainWindow.xaml"` from `App.xaml` (because we want the app to start hidden in the background, not pop up immediately).

## Phase 2: The "Spotlight" Input UI (Visuals)
- [ ] **Configure MainWindow.xaml**
    - Set window style to `None` (Borderless).
    - Enable `AllowsTransparency`.
    - Set background to transparent (to allow the rounded corners/Mica effect to shine).
    - Center the window on screen.
- [ ] **Add Wpf.Ui Controls**
    - Add the Wpf.Ui namespace to the XAML.
    - Add a large, stylish `TextBox`.
    - Apply the "Mica" backdrop effect.
- [ ] **Input Logic (MainWindow.xaml.cs)**
    - Listen for the `KeyDown` event on the TextBox.
    - If `Enter` is pressed -> Trigger "Save Task" (placeholder) and Hide Window.
    - If `Escape` is pressed -> Hide Window.
    - Focus the TextBox automatically when the window appears.

## Phase 3: The Data Layer
- [ ] **Create Task Model**
    - Create file: `Models/TaskItem.cs`.
    - Properties: `Id`, `Description`, `CreatedAt`, `IsCompleted`.
- [ ] **Create Database Service**
    - Create file: `Services/DatabaseService.cs`.
    - Initialize LiteDB (`tasks.db`).
    - Methods: `AddTask(string text)`, `GetRecentTasks()`, `DeleteTask(int id)`.

## Phase 4: The "QuickLook" UI
- [ ] **Create QuickLookWindow**
    - Run: `dotnet new page -n QuickLookWindow` (or create .xaml/.cs manually).
- [ ] **Style the Window**
    - Make it a semi-transparent overlay.
    - Remove borders.
    - Add a `ListView` or `ItemsControl` to display the tasks.
- [ ] **Data Binding**
    - Load tasks from `DatabaseService` when the window becomes visible.

## Phase 5: Low-Level Keyboard Hooks (The Core)
- [ ] **Create Hook Service**
    - Create file: `Services/KeyboardHookService.cs`.
    - Import `user32.dll` methods (`SetWindowsHookEx`, `CallNextHookEx`, etc.).
- [ ] **Implement Logic**
    - **Hook 1 (Add Task):** Detect single press of "Muhenkan" (or chosen key). Toggle `MainWindow` visibility.
    - **Hook 2 (QuickLook):** Detect "Key Down" on chosen key -> Show `QuickLookWindow`. Detect "Key Up" -> Hide `QuickLookWindow`.
    - *Important:* Ensure these keys are blocked from the rest of Windows so typing doesn't happen elsewhere.

## Phase 6: System Tray & Application Lifecycle
- [ ] **Add Tray Icon**
    - Update `App.xaml` to include the `TaskbarIcon` resource.
    - Add context menu: "Open Dashboard", "Settings", "Exit".
- [ ] **Prevent Shutdown**
    - Ensure closing a window acts like "Minimize" (hiding it) rather than killing the app.
    - Only `Exit` from the tray should kill the app.

## Phase 7: Polish & Settings
- [ ] **Key Binding Settings**
    - Create a simple settings file (JSON) to store which keys the user wants to use (e.g., VK_NONCONVERT).
- [ ] **UI Polish**
    - Add shadows and acrylic effects.