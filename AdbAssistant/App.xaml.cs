using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using Microsoft.Shell;
using System.Windows.Interop;

namespace AdbAssistant
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// http://jkshay.com/creating-restarting-single-instance-wpf-application-singleinstance-cs/
    /// 
    public partial class App : ISingleInstanceApp
    {
        private const string Unique = "UniqueStringForSingleInstanceApp";
        
        [STAThread]
        public static void Main()
        {
            if (SingleInstance<App>.InitializeAsFirstInstance(Unique))
            {
                Splasher.Splash = new SplashScreen();
                Splasher.ShowSplash();
                var application = new App();
                application.Run();
                
                // Allow single instance code to perform cleanup operations
                SingleInstance<App>.Cleanup();
            }
        }

        public App()
        {
            InitializeComponent();
            Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //TODO: Make combobox public in order to init it here, or 
            bool isInstaled = CheckEngine.checkInstalled("Android SDK Tools");
            if (!CheckEngine.SystemValidation(isInstaled)) Application.Current.Shutdown();
            MainWindow win = new MainWindow();
            //var comboBoxDevice = sender as ComboBox;
            //win.vault = new Dictionary<string, string>();
            win.vault = CheckEngine.DevicesAttached();
            win.comboBoxDevice.ItemsSource = win.vault.Keys;
            //Set current device device number based on model (it's key in dictionary)
            //win.defaultDevice = win.vault[comboBoxDevice.SelectedItem as string];
            //win.GetSomeIds();            
            //ProcessBuilder.ProcessNew("adb devices");
            System.Threading.Thread.Sleep(2000);
            Splasher.CloseSplash();
            win.Show();
        }

        
        //Shows message in case the second instance of the app was launched

        public bool SignalExternalCommandLineArgs(System.Collections.Generic.IList<string> args)
        {
            //cv.Show();
            MessageBoxResult mb = MessageBox.Show("SingleInstanceApp is already running","The thing is...", MessageBoxButton.OK);
            Current.MainWindow.WindowState = WindowState.Normal;
            return true;
        }
    }
}
