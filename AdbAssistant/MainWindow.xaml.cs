using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
//using System.Windows.Shapes;
//using System.Management.Automation;
using System.Diagnostics;
using Microsoft.Win32;
using System.IO;
//using System.Runtime.Remoting;
using System.Windows.Interop;



namespace AdbAssistant
{

    public partial class MainWindow : Window   

    {

        //version 1.1
        //public string defaultDevice = "";
        //public Dictionary<string, string> vault = new Dictionary<string, string>();

        //Version 1.2
        internal DeviceValues device = new DeviceValues();
        
        public MainWindow()
        {
            InitializeComponent();           
        }

        //----------------------------------------------------------------------------------------------------
        // That block captures device been connected/disconnected
        //----------------------------------------------------------------------------------------------------
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            // Adds the windows message processing hook and registers USB device add/removal notification.
            HwndSource source = HwndSource.FromHwnd(new WindowInteropHelper(this).Handle);
            if (source != null)
            {
                var windowHandle = source.Handle;
                source.AddHook(HwndHandler);
                UsbNotification.RegisterUsbDeviceNotification(windowHandle);
            }
        }


        //Captures devices connect/disconnect
        //Version 1.1
        //private IntPtr HwndHandler(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam, ref bool handled)
        //{
        //    if (msg == UsbNotification.WmDevicechange)
        //    {
        //        switch ((int)wparam)
        //        {
        //            case UsbNotification.DbtDeviceremovecomplete:
        //                //device been DISCONNECTED
        //                switch (CheckEngine.DeviceListsDifferent(vault, defaultDevice))
        //                {
        //                    case 0:
        //                        break;
        //                    case 1:
        //                        {
        //                            vault = CheckEngine.DevicesAttached();
        //                            comboBoxDevice.ItemsSource = vault.Keys;
        //                            defaultDevice = vault[comboBoxDevice.SelectedItem as string];
        //                            break;
        //                        }
        //                    case -1:
        //                        {
        //                            vault = CheckEngine.DevicesAttached();
        //                            comboBoxDevice.ItemsSource = vault.Keys;
        //                            defaultDevice = vault[comboBoxDevice.SelectedItem as string];
        //                            break;
        //                        }
        //                    default:
        //                        break;
        //                }
        //                break;

        //            case UsbNotification.DbtDevicearrival:
        //                //device been CONNECTED
        //                MessageBoxResult deviceIsConnected = MessageBox.Show("The device was connected", "Hey! The thing is...", MessageBoxButton.OK);
        //                if (CheckEngine.DeviceListsDifferent(vault))
        //                {
        //                    RefreshDevicesAndInfo();
        //                }
        //                else
        //                {
        //                    MessageBox.Show("The device is not recognized as Android device", "Hey! The thing is...");
        //                    Application.Current.Shutdown();
        //                }
        //                break;
        //        }
        //    }

        //    handled = false;
        //    return IntPtr.Zero;
        //}
        ////----------------------------------------------------------------------------------------------------


        //Version 1.2
        private IntPtr HwndHandler(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam, ref bool handled)
        {
            if (msg == UsbNotification.WmDevicechange)
            {
                switch ((int)wparam)
                {
                    case UsbNotification.DbtDeviceremovecomplete:
                        //device been DISCONNECTED
                        switch (CheckEngine.DeviceListsDifferent(device.devicesConnectedList, device.activeDeviceNo))
                        {
                            case 0:
                                {
                                    //List of devices are simmilar
                                    //device.FullReinit();
                                    //MapValues();
                                    break;
                                }
                            case 1:
                                {
                                    //One of passive devices was disconnected
                                    device.FullReinit();
                                    MapValues();
                                    break;
                                }
                            case -1:
                                {
                                    //Active device was disconnected, new active device is set
                                    device.FullReinit();
                                    MapValues();
                                    MessageBox.Show("Active device was disconnected, heil new active device", "Hey! The thing is...", MessageBoxButton.OK);
                                    break;
                                }
                            case 2:
                                {
                                    //Last device was disconnected, no Android devices detected on the system
                                    device.FullReinit();
                                    MapValues();
                                    break;
                                }
                            default:
                                {
                                    MessageBoxResult deviceIsConnected = MessageBox.Show("Wow, something strange just happened. Please, contact the developer and let him know", "Hey! The thing is...", MessageBoxButton.OK);
                                    break;
                                }
                        }
                        break;

                    case UsbNotification.DbtDevicearrival:
                        //device been CONNECTED
                        //MessageBoxResult deviceIsConnected = MessageBox.Show("The device was connected", "Hey! The thing is...", MessageBoxButton.OK);
                        System.Threading.Thread.Sleep(1000);
                        if (CheckEngine.DeviceListsDifferent(device.devicesConnectedList))
                        {
                            //device.getDeviceConnectedList();
                            //device.activeDeviceModel = comboBoxDevice.SelectedItem.ToString();
                            //device.activeDeviceNo = device.devicesConnectedList[comboBoxDevice.SelectedItem.ToString()];
                            device.FullReinit();
                            MapValues();

                        }
                        else
                        {
                            //To debug device not being recognized as Android one uncomment line below
                            //MessageBox.Show("The device is not recognized as Android device", "Hey! The thing is...");
                        }
                        break;
                }
            }

            handled = false;
            return IntPtr.Zero;
        }

        //populates android versions and IDs on app start
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //device.devicesConnectedList = device.getDeviceConnectedList();
            //MapValues();

            //string version = null;
            //try
            //{

            //    //// get deployment version
            //    version = ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            //}
            //catch (InvalidDeploymentException)
            //{
            //    //// you cannot read publish version when app isn't installed 
            //    //// (e.g. during debug)
            //    version = "not installed";
            //}
        }


        //----------------------------------------------------------------------------------------------------
        /// Buttons functionality
        //----------------------------------------------------------------------------------------------------

        //Captures screenshot and saves it to the host. Sorce file is deleted from a device
        private void CaptureScreenshot(object sender, RoutedEventArgs e)
        {
            //Version 1.1
            //var sb = new StringBuilder();
            //sb.Append(DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss"));
            //ProcessBuilder.ProcessNew("shell screencap -p /sdcard/" + sb.ToString() + ".png", defaultDevice);
            //ProcessBuilder.ProcessNew("pull /sdcard/" + sb.ToString() + ".png screenshots", defaultDevice);
            //ProcessBuilder.ProcessNew("shell rm -r /sdcard/" + sb.ToString() + ".png", defaultDevice);

            //Version 1.2
            var sb = new StringBuilder();
            sb.Append(DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss"));
            sb.Append(".png");
            ProcessBuilder.ProcessNew("shell screencap -p /sdcard/" + sb.ToString(), device.activeDeviceNo);
            ProcessBuilder.ProcessNew("pull /sdcard/" + sb.ToString() + " Screenshots\\" + sb.ToString(), device.activeDeviceNo);
            ProcessBuilder.ProcessNew("shell rm -r /sdcard/" + sb.ToString(), device.activeDeviceNo);
        }

        // starts HoV
        private void LaunchHoV(object sender, RoutedEventArgs e)
        {
            //Version 1.1
            //ProcessBuilder.ProcessNew("shell am start -n com.productmadness.hovmobile/com.productmadness.hovmobile.AppEntry", defaultDevice);

            //Version 1.2
            ProcessBuilder.ProcessNew("shell am start -n com.productmadness.hovmobile/com.productmadness.hovmobile.AppEntry", device.activeDeviceNo);
        }

        // stops HoV
        private void KillHoV(object sender, RoutedEventArgs e)
        {
            //Version 1.1
            //ProcessBuilder.ProcessNew("shell am force-stop com.productmadness.hovmobile", defaultDevice);

            //Version 1.2
            ProcessBuilder.ProcessNew("shell am force-stop com.productmadness.hovmobile", device.activeDeviceNo);

        }

        // restarts HoV
        private void RestartHoV(object sender, RoutedEventArgs e)
        {
            //Version 1.1
            //var processInfo = new ProcessStartInfo("cmd.exe");
            //processInfo.RedirectStandardOutput = true;
            //processInfo.RedirectStandardInput = true;
            //processInfo.UseShellExecute = false;
            //processInfo.CreateNoWindow = true;
            //var process = new Process();
            //process.StartInfo = processInfo;
            //process.Start();

            //using (StreamWriter sw = process.StandardInput)
            //{
            //    if (sw.BaseStream.CanWrite)
            //    {
            //        sw.WriteLine("adb -s " + defaultDevice.ToString() + " shell am force-stop com.productmadness.hovmobile");
            //        sw.WriteLine("adb -s " + defaultDevice.ToString() + " shell input keyevent 82");
            //        sw.WriteLine("adb -s " + defaultDevice.ToString() + " shell am start -n com.productmadness.hovmobile/com.productmadness.hovmobile.AppEntry");
            //        sw.WriteLine("adb -s " + defaultDevice.ToString() + " shell input keyevent 82");
            //    }
            //}

            //Version 1.2
            var processInfo = new ProcessStartInfo("cmd.exe");
            processInfo.RedirectStandardOutput = true;
            processInfo.RedirectStandardInput = true;
            processInfo.UseShellExecute = false;
            processInfo.CreateNoWindow = true;
            var process = new Process();
            process.StartInfo = processInfo;
            process.Start();

            using (StreamWriter sw = process.StandardInput)
            {
                if (sw.BaseStream.CanWrite)
                {
                    sw.WriteLine("adb -s " + device.activeDeviceNo + " shell am force-stop com.productmadness.hovmobile");
                    //sw.WriteLine("adb -s " + device.activeDeviceNo + " shell input keyevent 82");
                    sw.WriteLine("adb -s " + device.activeDeviceNo + " shell am start -n com.productmadness.hovmobile/com.productmadness.hovmobile.AppEntry");
                    //sw.WriteLine("adb -s " + device.activeDeviceNo + " shell input keyevent 82");
                }
            }
        }

        // installs selected build to a device
        private void InstallBuild(object sender, RoutedEventArgs e)
        {            
            // opens "select apk" dialog
            string pathToApk = "buildLib";
            var openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"Builds";
            openFileDialog.Title = "Choose your .apk file...";
            openFileDialog.FileName = "Choose file";
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;
            openFileDialog.Filter = " .apk|*.APK";
            if (openFileDialog.ShowDialog() == true)
            {
                pathToApk = openFileDialog.FileName;
                textBoxAPKName.Text = openFileDialog.SafeFileName;
            }

            //Version 1.1
            //var processInfo = new ProcessStartInfo("cmd.exe", "/c adb " + "-s " + defaultDevice + " install " + pathToApk);

            //Version 1.2
            //taskStatus.Visibility = Visibility.Visible;
            var processInfo = new ProcessStartInfo("cmd.exe", "/c adb " + "-s " + device.activeDeviceNo + " install " + pathToApk);

            processInfo.RedirectStandardOutput = true;
            processInfo.UseShellExecute = false;
            processInfo.CreateNoWindow = true;
            var process = new Process();
            process.StartInfo = processInfo;
            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            
            device.buildNumber = device.getBuildNumber(device.activeDeviceNo);
            textBoxBuild.Text = device.buildNumber;
            if (result.Contains("Success"))
            {
                MessageBoxResult buildWasInstalled = MessageBox.Show("HoV .apk was successfully installed", "Hey! The thing is...", MessageBoxButton.OK);
            }
            //taskStatus.Visibility = Visibility.Hidden;

        }

        // pulls HoV from a device
        private void PullHoV(object sender, RoutedEventArgs e)
        {
            //Version 1.1
            //var pathToApk = ProcessBuilder.ProcessNew("cmd.exe", "shell pm path com.productmadness.hovmobile", defaultDevice);

            //Version 1.2
            var pathToApk = ProcessBuilder.ProcessNew("cmd.exe", "shell pm path com.productmadness.hovmobile", device.activeDeviceNo);

            var commandToAdb = new StringBuilder();
            //commandToAdb.Append("/c adb -s ");

            //Version 1.1
            //commandToAdb.Append(defaultDevice);

            //Version 1.2
            //commandToAdb.Append(device.activeDeviceNo);

            commandToAdb.Append("pull ");
            commandToAdb.Append(Formatter(pathToApk, "package:"));
            var str = textBoxBuild.Text;
            if (str.Equals("no desirable element found") || str.Equals(null))
            {
                MessageBoxResult buildWasNotFound = MessageBox.Show("HoV .apk was not found on selected device", "Hey! The thing is...", MessageBoxButton.OK);
                return;
            }
            commandToAdb.Append(" builds\\");
            commandToAdb.Append(str);
            str = textBoxFireOS.Text;
            if (!(str.Equals("")))
            {
                commandToAdb.Append("_amazon");
            }
            commandToAdb.Append(".apk");

            //var process = Process.Start("cmd.exe", commandToAdb.ToString());
            //process.WaitForExit();
            var pullInfo = ProcessBuilder.ProcessNew("cmd.exe", commandToAdb.ToString(), device.activeDeviceNo);
            MessageBoxResult buildWasPulled = MessageBox.Show(device.buildNumber + " build was successfully pulled", "Hey! The thing is...", MessageBoxButton.OK);
        }

        // uninstalls HoV from a device
        private void UninstallHoV(object sender, RoutedEventArgs e)
        {
            var str = textBoxBuild.Text;
            if (str.Equals("no desirable element found") || str.Equals(null))
            {
                MessageBoxResult buildWasNotFound = MessageBox.Show("HoV .apk was not found on selected device", "Hey! The thing is...", MessageBoxButton.OK);
                return;
            }

            //Version 1.1
            //ProcessBuilder.ProcessNew("shell pm uninstall com.productmadness.hovmobile", defaultDevice);

            //Version 1.2
            ProcessBuilder.ProcessNew("shell pm uninstall com.productmadness.hovmobile", device.activeDeviceNo);
            device.buildNumber = device.getBuildNumber(device.activeDeviceNo);
            textBoxBuild.Text = device.buildNumber;

            //var process = Process.Start("cmd.exe", "/c adb shell pm uninstall com.productmadness.hovmobile");
            //process.WaitForExit();
            MessageBoxResult buildWasInstalled = MessageBox.Show("HoV .apk was successfully uninstalled", "Hey! The thing is...", MessageBoxButton.OK);
        }

        //----------------------------------------------------------------------------------------------------
        /// Textboxes functionality
        //----------------------------------------------------------------------------------------------------

        private void comboBoxDevice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Version 1.1
            //defaultDevice = CheckEngine.DevicesAttached()[comboBoxDevice.SelectedItem as string];
            //GetSomeIds();

            //Version 1.2
            if (comboBoxDevice.SelectedItem != null)
            {
                device.activeDeviceModel = comboBoxDevice.SelectedItem.ToString();
                device.activeDeviceNo = device.devicesConnectedList[comboBoxDevice.SelectedItem.ToString()];
                device.PartialReinit();
                MapValues();
            }


        }

        private void DeviceListPopulate(object sender, RoutedEventArgs e)
        {
            Splasher.CloseSplash();
        }
        
        private void buttonRefresh_Click(object sender, RoutedEventArgs e)
        {
            //Version 1.1
            //RefreshDevicesAndInfo();

            //Version 1.2
            device.FullReinit();
            MapValues();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        //Refreshes device list and info on active device
        private void RefreshDevicesAndInfo()
        {
            //Version 1.1
            //vault = CheckEngine.DevicesAttached();
            ////Assign the ItemsSource to the List.
            //comboBoxDevice.ItemsSource = vault.Keys;
            ////Set current device device number based on model (it's key in dictionary)
            //defaultDevice = vault[comboBoxDevice.SelectedItem as string];
            //GetSomeIds();

            //Version 1.2
            device.FullReinit();
            MapValues();
        }

        //Version 1.1
        //// gets android versions and IDs
        //public void GetSomeIds()
        //{
        //    // GET ANDROID ID PROC
        //    string textBoxAndroidIdText = ProcessBuilder.ProcessNew("cmd.exe", "shell settings get secure android_id", defaultDevice);
        //    textBoxAndroidId.Text = textBoxAndroidIdText.Trim();

        //    // GET ANDROID VERSION PROC
        //    string textBoxAndroidVersionText = ProcessBuilder.ProcessNew("cmd.exe", "shell getprop ro.build.version.release", defaultDevice);
        //    textBoxAndroidVersion.Text = textBoxAndroidVersionText.Trim();

        //    // GET FIRE OS VERSION PROC
        //    string textBoxFireOSText = ProcessBuilder.ProcessNew("cmd.exe", "shell getprop ro.build.version.fireos", defaultDevice);
        //    textBoxFireOS.Text = textBoxFireOSText.Trim();

        //    // GET BUILD VERSION PROC
        //    string textBoxBuildText = ProcessBuilder.ProcessNew("cmd.exe", "shell dumpsys package com.productmadness.hovmobile", defaultDevice);
        //    textBoxBuild.Text = Formatter(textBoxBuildText, "versionName=");
        //}

        //Version 1.2
        public void MapValues()
        {
            comboBoxDevice.ItemsSource = device.devicesConnectedList.Keys;

            comboBoxDevice.SelectedItem = device.activeDeviceModel; 
               
            textBoxAndroidId.Text = device.androidId;

            textBoxAndroidVersion.Text = device.androidVersion;

            textBoxFireOS.Text = device.fireVersion;
          
            textBoxBuild.Text = device.buildNumber;
        }

        //Helper method that deletes empty spaces and argument string
        public static string Formatter(String rawInput, String findRemove)
        {
            var sb = new StringBuilder();
            String result;
            if (rawInput != null)
            {
                String[] lines = rawInput.Split('\n');
                foreach (String line in lines)
                {
                    String line1 = line.Trim();
                    if (line1.Contains(findRemove))
                    {
                        sb.Append(line);
                        sb.Replace(findRemove, "");
                        result = sb.ToString();
                        return result.Trim();
                    }
                }
                return "no desirable element found";
            }
            else return "output is NULL";
        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            Topmost = true;
        }

        private void checkBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Topmost = false;
        }
    }
}
