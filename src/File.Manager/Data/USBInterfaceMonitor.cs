using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace File.Manager
{
    /// <summary>
    /// Listens out for when a device is plugged into host machine
    /// </summary>
    public static class USBInterfaceMonitor
    {
        /// <summary>
        /// Event that fires when device is inserted in the host machine
        /// </summary>
        /// <param name="sender">The origin of this event</param>
        /// <param name="e">The event argument</param>
        private static void DeviceInsertedEvent(object sender, EventArrivedEventArgs e)
        {
            //ManagementBaseObject instance = (ManagementBaseObject)e.NewEvent["TargetInstance"];
            //foreach (var property in instance.Properties)
            //{
            //    MessageBox.Show(property.Name + " = " + property.Value);
            //}
        }

        /// <summary>
        /// Event that fires when a currently inserted device is removed from the host machine
        /// </summary>
        /// <param name="sender">The origin of this event</param>
        /// <param name="e">The event argument</param>
        private static void DeviceRemovedEvent(object sender, EventArrivedEventArgs e)
        {
            //ManagementBaseObject instance = (ManagementBaseObject)e.NewEvent["TargetInstance"];
            //foreach (var property in instance.Properties)
            //{
            //    MessageBox.Show(property.Name + " = " + property.Value);
            //}
        }

        /// <summary>
        /// Sets up the usb monitor query and events
        /// </summary>
        /// <param name="sender">The origin of this event</param>
        /// <param name="e">The event argument</param>
        public static void ScanForAvailableDrives(object sender, DoWorkEventArgs e)
        {
            WqlEventQuery insertQuery = new WqlEventQuery("SELECT * FROM __InstanceCreationEvent WITHIN 2 WHERE TargetInstance ISA 'Win32_USBHub'");

            ManagementEventWatcher deviceInsertWatcher = new ManagementEventWatcher(insertQuery);
            deviceInsertWatcher.EventArrived += new EventArrivedEventHandler(DeviceInsertedEvent);
            deviceInsertWatcher.Start();

            WqlEventQuery removeQuery = new WqlEventQuery("SELECT * FROM __InstanceDeletionEvent WITHIN 2 WHERE TargetInstance ISA 'Win32_USBHub'");
            ManagementEventWatcher deviceRemoveWatcher = new ManagementEventWatcher(removeQuery);
            deviceRemoveWatcher.EventArrived += new EventArrivedEventHandler(DeviceRemovedEvent);
            deviceRemoveWatcher.Start();
        }

    }
}
