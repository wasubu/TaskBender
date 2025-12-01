using System.Windows;

namespace TaskBender
{
    public partial class QuickLookWindow : Window
    {
        private readonly Services.DatabaseService _db;

        public QuickLookWindow()
        {
            InitializeComponent();
            _db = new Services.DatabaseService();
            this.IsVisibleChanged += QuickLookWindow_IsVisibleChanged;
        }

        private void QuickLookWindow_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                RefreshTasks();
            }
        }

        private void RefreshTasks()
        {
            var tasks = _db.GetActiveTasks();
            TaskList.Items.Clear();
            foreach (var task in tasks)
            {
                TaskList.Items.Add(new System.Windows.Controls.ListViewItem { Content = task.Description });
            }
        }
    }
}
