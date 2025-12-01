using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TaskBender.Services;
using TextBox = System.Windows.Controls.TextBox;

namespace TaskBender
{
    public partial class SettingsWindow : Window
    {
        private readonly SettingsService _settingsService;
        private int _tempSpotlightKey;
        private int _tempQuickLookKey;

        public SettingsWindow(SettingsService settingsService)
        {
            InitializeComponent();
            _settingsService = settingsService;
            _tempSpotlightKey = _settingsService.Settings.SpotlightKey;
            _tempQuickLookKey = _settingsService.Settings.QuickLookKey;

            UpdateKeyDisplay();
        }

        private void UpdateKeyDisplay()
        {
            SpotlightKeyBox.Text = $"VK Code: {_tempSpotlightKey}";
            QuickLookKeyBox.Text = $"VK Code: {_tempQuickLookKey}";
        }

        private void KeyBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            e.Handled = true;
            
            // We need to get the virtual key code. 
            // KeyInterop.VirtualKeyFromKey converts WPF Key to Win32 Virtual Key.
            int vkCode = KeyInterop.VirtualKeyFromKey(e.Key);

            if (sender is TextBox textBox)
            {
                if (textBox.Tag.ToString() == "Spotlight")
                {
                    _tempSpotlightKey = vkCode;
                }
                else if (textBox.Tag.ToString() == "QuickLook")
                {
                    _tempQuickLookKey = vkCode;
                }
                UpdateKeyDisplay();
            }
        }

        private void KeyBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.Background = System.Windows.Media.Brushes.LightYellow;
            }
        }

        private void KeyBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.Background = System.Windows.Media.Brushes.Transparent;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            _settingsService.Settings.SpotlightKey = _tempSpotlightKey;
            _settingsService.Settings.QuickLookKey = _tempQuickLookKey;
            _settingsService.SaveSettings();
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
