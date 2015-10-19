using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AdbAssistant
{
    internal class CheckEngine
    {
        public static bool SystemValidation(bool adbInstalledCheck)
        {
            if (true)
            {
                int i = 0;
                string ifAnyDevicesCheck;
                do
                {
                    i++;
                    ifAnyDevicesCheck = ProcessBuilder.ProcessNew("/c adb devices");

                }
                while (ifAnyDevicesCheck.Trim().Contains("daemon") && i < 5);

                if (i >= 5)
                {
                    MessageBoxResult adbOutOfDate = MessageBox.Show("adb is out of date");
                    return false;
                }

                else if (ifAnyDevicesCheck.Trim().Equals("List of devices attached"))
                {
                    MessageBoxResult deviceIsNotConnected = MessageBox.Show("No device was found in your system. Try again?",
                        "Hey! The thing is...", MessageBoxButton.OKCancel, MessageBoxImage.Question);

                    switch (deviceIsNotConnected)
                    {
                        case MessageBoxResult.OK:
                            CheckEngine.SystemValidation(true);
                            break;
                        case MessageBoxResult.Cancel:
                            Application.Current.Shutdown();
                            break;
                    }
                    return false;
                }

                else return true;
            }
            else
            {
                MessageBoxResult adbIsNotInstalled = MessageBox.Show("ADB is not found in your system", "Hey! The thing is...");
                return false;
            }            
        }

        // Checks if application installed on the system
        public static bool checkInstalled(string c_name)
        {
            string displayName;

            string registryKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            RegistryKey key = Registry.LocalMachine.OpenSubKey(registryKey);
            if (key != null)
            {
                foreach (RegistryKey subkey in key.GetSubKeyNames().Select(keyName => key.OpenSubKey(keyName)))
                {
                    displayName = subkey.GetValue("DisplayName") as string;
                    if (displayName != null && displayName.Contains(c_name))
                    {
                        return true;
                    }
                }
                key.Close();
            }

            registryKey = @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall";
            key = Registry.LocalMachine.OpenSubKey(registryKey);
            if (key != null)
            {
                foreach (RegistryKey subkey in key.GetSubKeyNames().Select(keyName => key.OpenSubKey(keyName)))
                {
                    displayName = subkey.GetValue("DisplayName") as string;
                    if (displayName != null && displayName.Contains(c_name))
                    {
                        return true;
                    }
                }
                key.Close();
            }
            return false;
        }

        public static Dictionary<string, string> DevicesAttached()
        {
            var deviceNumberName = new Dictionary<string, string>();

            string getDevices = ProcessBuilder.ProcessNew("/c adb devices");
            string[] deviceList = getDevices.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in deviceList)
            {
                string[] temp = line.Split();
                if (temp[1].Equals("device"))
                {
                    string model = ProcessBuilder.ProcessNew("cmd.exe", "shell getprop ro.product.model", temp[0]);
                    deviceNumberName.Add(model.Trim(), temp[0]);
                }

                else
                    continue;
            }
            return deviceNumberName;
        }

        public static bool DeviceListsDifferent(Dictionary<string, string> currentList)
        {
            // b - a = c
            //If device is CONNECTED
            //Compares current device list and new list(DeviesAttatched method), in case they are equal returns false.
            var a = currentList.Keys;
            var b = DevicesAttached().Keys;
            var c = b.Except(a).ToList();
            if (c.Count == 0)
            {
                return false;
            }
            return true;
        }

        public static int DeviceListsDifferent(Dictionary<string, string> currentList, string isCurrent)
        {
            // a - b = c
            //If device is DISCONNECTED
            //Compares current device list and new list (DeviesAttatched method), if they are equal returns 0, if active device was disconnected returns -1;
            //if not active device was disconnected returns 1
            var freshDeviceList = new Dictionary<string, string>();
            freshDeviceList = DevicesAttached();
            var a = currentList.Values;
            var b = freshDeviceList.Values;
            var c = a.Except(b).ToList();
            if (c.Count == 0)
            {
                return 0;
            }
 
            if (c.Contains(isCurrent))
            {
                MessageBox.Show("Active device was disconnected, heil new active device", "Hey! The thing is...", MessageBoxButton.OK);
                return -1;
            }
            else
            {
                return 1;
            }
        }
    }
}
