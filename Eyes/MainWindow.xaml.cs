using System.Windows;
using System.Windows.Navigation;

namespace Eyes
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            frame.Navigated += Frame_Navigated;
            frame.NavigationService.Navigate(new AgentsList());
        }

        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {
            var pageContent = frame.Content;
        }
    }
}
