using System;
using System.Windows;
using System.Drawing;
using Hardcodet.Wpf.TaskbarNotification;
using TaskBender.Services;

namespace TaskBender
{
    public partial class App : System.Windows.Application
    {
        public static MainWindow? MainInputWindow { get; private set; }
        public static QuickLookWindow? QuickViewWindow { get; private set; }
        private Services.KeyboardHookService? _hookService;
        private TaskbarIcon? _taskbarIcon;
        private SettingsService? _settingsService;

        public App()
        {
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            System.IO.File.WriteAllText("crash_log.txt", e.Exception.ToString());
            System.Windows.MessageBox.Show("App crashed: " + e.Exception.Message);
            e.Handled = true;
            Shutdown();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                base.OnStartup(e);

            _settingsService = new SettingsService();

            // Initialize windows but don't show them yet
            MainInputWindow = new MainWindow();
            QuickViewWindow = new QuickLookWindow();

            _hookService = new Services.KeyboardHookService(_settingsService);
            _hookService.OnSpotlightTriggered += (s, args) => ToggleMainWindow();
            _hookService.OnQuickLookShow += (s, args) => QuickViewWindow?.Show();
            _hookService.OnQuickLookHide += (s, args) => QuickViewWindow?.Hide();

            // Initialize Taskbar Icon from XAML resources
            _taskbarIcon = (TaskbarIcon?)TryFindResource("TrayIcon");
            if (_taskbarIcon == null)
            {
                // Fallback: Create manually if not found in resources
                _taskbarIcon = new TaskbarIcon();
                _taskbarIcon.Icon = System.Drawing.SystemIcons.Application;
                _taskbarIcon.ToolTipText = "TaskBender";
                _taskbarIcon.ContextMenu = (System.Windows.Controls.ContextMenu)TryFindResource("TrayMenu");
            }
            }
            catch (Exception ex)
            {
                System.IO.File.WriteAllText("startup_error.txt", ex.ToString());
                System.Windows.MessageBox.Show("Startup error: " + ex.Message);
                Shutdown();
            }
        }

        private void ToggleMainWindow()
        {
            if (MainInputWindow == null) return;

            if (MainInputWindow.IsVisible)
                MainInputWindow.Hide();
            else
            {
                MainInputWindow.Show();
                MainInputWindow.Activate();
            }
        }

        private void TrayIcon_DoubleClick(object sender, RoutedEventArgs e)
        {
            ToggleMainWindow();
        }

        private void ShowApp_Click(object sender, RoutedEventArgs e)
        {
            ToggleMainWindow();
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            if (_settingsService == null) return;
            var settingsWindow = new SettingsWindow(_settingsService);
            settingsWindow.ShowDialog();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Shutdown();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _taskbarIcon?.Dispose();
            _hookService?.Dispose();
            base.OnExit(e);
        }
    }
}
