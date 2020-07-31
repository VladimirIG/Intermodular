using System.Diagnostics;
using System.Windows;

namespace View
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_Exit(object sender, ExitEventArgs e)
        {
            /*
            foreach (var process in Process.GetProcessesByName("node"))
            {
                process.Kill();
            }
            */
        }
    }
}
