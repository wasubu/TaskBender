using System;
using System.Windows;
using System.Windows.Input;


namespace TaskBender
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Deactivated += MainWindow_Deactivated;
            TaskInput.KeyDown += TaskInput_KeyDown;
        }

        private void MainWindow_Deactivated(object? sender, EventArgs e)
        {
            this.Hide();
        }

        private void TaskInput_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Hide();
                e.Handled = true;
            }
            else if (e.Key == Key.Enter)
            {
                if (!string.IsNullOrWhiteSpace(TaskInput.Text))
                {
                    var db = new Services.DatabaseService();
                    db.AddTask(TaskInput.Text);
                }
                this.Hide();
                TaskInput.Clear();
                e.Handled = true;
            }
        }

        public new void Show()
        {
            base.Show();
            this.Activate();
            TaskInput.Focus();
        }
    }
}