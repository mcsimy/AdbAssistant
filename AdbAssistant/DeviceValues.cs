using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdbAssistant
{
    internal class DeviceValues
    {
        Dictionary<string, string> devicesConnectedList;
        string androidId;
        string androidVersion;
        string fireVersion;
        string buildNumber;

        public Dictionary<string, string> getDeviceConnectedList()
        {
            //TODO
            var deviceList = new Dictionary<string, string>();
            string getDevices = ProcessBuilder.ProcessNew("/c adb devices");
            string[] deviceListTemp = getDevices.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in deviceListTemp)
            {
                string[] temp = line.Split();
                if (temp[1].Equals("device"))
                {
                    string model = ProcessBuilder.ProcessNew("cmd.exe", "shell getprop ro.product.model", temp[0]);
                    deviceList.Add(model.Trim(), temp[0]);
                }

                else
                    continue;
            }
            return deviceList;
        }

        public string getAndroidID()
        {
            //TODO
            return "value";
        }

        public string getAndroidVersion()
        {
            //TODO
            return "value";
        }

        public string getFireVersion()
        {
            //TODO
            return "value";
        }

        public string getBuildNumber()
        {
            //TODO
            return "value";
        }

        //Constructor
        internal DeviceValues()
        {
            devicesConnectedList = new Dictionary<string, string> { { "No device", "No device" } };
            androidId = "data is inaccessable";
            androidVersion = "data is unaccessable";
            fireVersion = "data is unaccessable";
            buildNumber = "data is unaccessable";
        }
    }
}
