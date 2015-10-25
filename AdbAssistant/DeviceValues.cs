using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdbAssistant
{
    internal class DeviceValues
    {
        public Dictionary<string, string> devicesConnectedList;
        public string activeDeviceModel;
        public string activeDeviceNo;
        public string androidId;
        public string androidVersion;
        public string fireVersion;
        public string buildNumber;


        public Dictionary<string, string> getDeviceConnectedList()
        {
            bool firstTime = true;
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
                    if (firstTime)
                    {
                        activeDeviceModel = model.Trim();
                        activeDeviceNo = temp[0];
                        firstTime = false;
                    }
                }
            }
            if (firstTime)
            {
                deviceList.Add("No device", "No deviceNo");
                activeDeviceNo = "No deviceNo";
                activeDeviceModel = "No device";
            }
            return deviceList;
        }

        public string getAndroidID(string currentDevice)
        {
            if (currentDevice.Equals("No deviceNo")) return "data is inaccessable";
            string androidId = ProcessBuilder.ProcessNew("cmd.exe", "shell settings get secure android_id", currentDevice);
            return androidId.Trim();
        }

        public string getAndroidVersion(string currentDevice)
        {
            if (currentDevice.Equals("No deviceNo")) return "data is inaccessable";
            string androidVersion = ProcessBuilder.ProcessNew("cmd.exe", "shell getprop ro.build.version.release", currentDevice);
            return androidVersion.Trim();
        }

        public string getFireVersion(string currentDevice)
        {
            if (currentDevice.Equals("No deviceNo")) return "data is inaccessable";
            string fireVersion = ProcessBuilder.ProcessNew("cmd.exe", "shell getprop ro.build.version.fireos", currentDevice);
            return fireVersion.Trim();
        }

        public string getBuildNumber(string currentDevice)
        {
            if (currentDevice.Equals("No deviceNo")) return "data is inaccessable";
            string buildNumber = ProcessBuilder.ProcessNew("cmd.exe", "shell dumpsys package com.productmadness.hovmobile", currentDevice);
            return buildNumber = MainWindow.Formatter(buildNumber, "versionName=");
        }

        public void FullReinit()
        {
            devicesConnectedList = getDeviceConnectedList();
            androidId = getAndroidID(activeDeviceNo);
            androidVersion = getAndroidVersion(activeDeviceNo);
            fireVersion = getFireVersion(activeDeviceNo);
            buildNumber = getBuildNumber(activeDeviceNo);
        }

        public void PartialReinit()
        {
            androidId = getAndroidID(activeDeviceNo);
            androidVersion = getAndroidVersion(activeDeviceNo);
            fireVersion = getFireVersion(activeDeviceNo);
            buildNumber = getBuildNumber(activeDeviceNo);
        }

        //Constructor
        internal DeviceValues()
        {
            devicesConnectedList = new Dictionary<string, string> { { "No device", "No device" } };
            activeDeviceModel = "No device";
            activeDeviceNo = "No deviceNo";
            androidId = "data is inaccessable";
            androidVersion = "data is unaccessable";
            fireVersion = "data is unaccessable";
            buildNumber = "data is unaccessable";
        }
    }
}
