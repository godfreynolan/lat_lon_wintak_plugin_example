using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Net;
using lat_lon_wintak_plugin_example.Views;
using WinTak.Framework.Docking;
using WinTak.Framework.Docking.Attributes;
using System.Threading.Tasks;

namespace lat_lon_wintak_plugin_example.DockPanes
{
    [DockPane(Id, "lat_lon_wintak_plugin_example", Content = typeof(lat_lon_wintak_plugin_exampleView))]
    internal class lat_lon_wintak_plugin_exampleDockPane : DockPane
    {
        internal const string Id = "lat_lon_wintak_plugin_example_DockPanes_lat_lon_wintak_plugin_exampleDockPane";

        private double _lon;
        private double _lat;

        [ImportingConstructor]
        public lat_lon_wintak_plugin_exampleDockPane()
        {
            // Run this in a new thread so we don't block UI stuff
            Task.Run(() =>
            {
                // When MAVLink forwarding is enabled in QGC this is the default port it forwards to.
                // 14550 is usually used otherwise.
                int port = 14445;
                var udpClient = new UdpClient(port);
                var endPoint = new IPEndPoint(IPAddress.Loopback, port);

                var mavlinkParser = new MAVLink.MavlinkParse();

                Debug.Print("Listening for MAVLink packets on UDP: " + endPoint);
                while (true)
                {
                    if (udpClient.Available == 0)
                    {
                        continue;
                    }
                    try
                    {
                        // Receive UDP packet
                        byte[] data = udpClient.Receive(ref endPoint);

                        // Wrap byte array in a MemoryStream for mavlinkParser 
                        using (MemoryStream stream = new MemoryStream(data))
                        {
                            // Parse MAVLink message
                            var packet = mavlinkParser.ReadPacket(stream);

                            if (packet != null)
                            {
                                if (packet.msgid == 24) // Indicates this is a GPS_RAW_INT type MAVLink message
                                                        // See MAVLink.message_info (mavlink.cs) for definitions of other message types
                                {
                                    var gps_data = packet.ToStructure<MAVLink.mavlink_gps_raw_int_t>();

                                    //Debug.Print("long: " + gps_data.lon + "\t lat: " + gps_data.lat);
                                    Lon = gps_data.lon;
                                    Lat = gps_data.lat;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            });
        }
        
        public double Lon
        {
            get { return _lon; }
            set { SetProperty(ref _lon, value); }
        }

        public double Lat
        {
            get { return _lat; }
            set { SetProperty(ref _lat, value); }
        }
    }
}
