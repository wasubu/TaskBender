# TaskBender

> **Write up tasks quickly without disrupting your workflow.**

![Windows 11](https://img.shields.io/badge/Platform-Windows%2011-blue) ![Status](https://img.shields.io/badge/Status-In%20Development-orange)

**TaskBender** is a native Windows 11 utility designed to reclaim unused keyboard keys and turn them into powerful productivity triggers. Built with the "Flow State" in mind, it allows you to capture ideas and check your to-do list without your fingers ever leaving the keyboard or your eyes leaving the screen.

---

## ⚡ Why TaskBender?

Standard todo lists are distracting. You have to open an app, find the "Add" button, type, and click save. By the time you're done, you've lost your train of thought.

TaskBender runs silently in the background. It is designed for keyboard power users—specifically those with **Japanese Layout keyboards** who have keys they rarely use (like `Muhenkan` or `Henkan`)—though it works on any keyboard.

## ✨ Features

### 1. Spotlight Entry
**Tap. Type. Done.**
Press your custom hotkey (e.g., `Muhenkan`) to summon a centered, "Spotlight-style" input bar. Type your task and hit `Enter`. The window vanishes, and the task is saved. You never lose focus on your main work.

### 2. QuickLook (Hold-to-Show)
**Peek without clicking.**
Assign a secondary key (e.g., `Henkan`) to QuickLook.
*   **Hold the key:** Your task list floats over your screen.
*   **Release the key:** The list disappears instantly.
No minimize buttons. No window management. Just information when you need it.

### 3. Native Windows 11 Design
Built with native Windows styling, TaskBender features Mica material, rounded corners, and seamless integration with the OS aesthetic. It uses minimal RAM and feels like a part of Windows.

---

## ⚙️ Management & Customization

For more control, simply **click the TaskBender icon in your System Tray** (near the clock).

Opening the dashboard allows you to:
*   **Manage Tasks:** Edit, delete, or reorganize your full list of tasks.
*   **Customize Controls:** Assign any key to the *Spotlight* or *QuickLook* features.
*   **Adjust Settings:** Toggle the specific visual effects or startup behavior.

---

## 🎮 Usage Guide

| Feature | Default Key (JP Layout) | Default Key (US Layout) | Action |
| :--- | :--- | :--- | :--- |
| **Add Task** | `Muhenkan` (NonConvert) | `Alt` + `Space` | Toggles the input bar. Type and press `Enter` to save. |
| **QuickLook** | `Henkan` (Convert) | `Right Ctrl` (Hold) | Hold to view tasks, Release to hide. |

---

## 🛠️ Technology Stack

TaskBender is built for performance and native integration.

*   **Language:** C# (.NET 8)
*   **Framework:** WPF (Windows Presentation Foundation)
*   **Styling:** [WPF UI](https://wpfui.lepo.co/) (Fluent Design System)
*   **Database:** [LiteDB](https://www.litedb.org/) (Serverless NoSQL)
*   **System Integration:** Win32 API (User32.dll) for Low-Level Keyboard Hooks.

---

## 🗺️ Roadmap

- [ ] Core UI Implementation (Spotlight & QuickLook windows)
- [ ] Database integration (LiteDB)
- [ ] Low-Level Keyboard Hooks implementation
- [ ] System Tray integration
- [ ] Settings Menu (Custom Keybinding UI)
- [ ] Task completion & deletion logic