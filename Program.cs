using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Management;
using System.Net;
using System.Net.Sockets;

namespace JustNull
{


    internal static class Program
    {
        public static List<KageForm> dittos = new List<KageForm>();
        public static bool uses_date = false;
        public static bool uses_license = false;
        public static DateTime myDate = DateTime.ParseExact("2018-06-01 00:00:00,000", "yyyy-MM-dd HH:mm:ss,fff",
                                       System.Globalization.CultureInfo.InvariantCulture);
        public static string licensed_to = "2652626616161613161041212-45191120151619109184101118-45191120151619109184101118-16185692413";

        public static string Magnemite()
        {
            string str = string.Empty;
            using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = new ManagementClass("win32_processor").GetInstances().GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    str = enumerator.Current.Properties["processorID"].Value.ToString();
                }
            }
            string str2 = string.Concat(new object[] { str, Environment.ProcessorCount, "/", Environment.MachineName, "/", Environment.UserDomainName, "/", Environment.UserName, Environment.GetLogicalDrives().Length });
            str2.ToCharArray();
            string str3 = "";
            foreach (char ch in str2)
            {
                str3 = (ch != '/') ? (str3 + ((int)(char.ToUpper(ch) - '@')).ToString()) : (str3 + "/");
            }
            return str3.Replace("-", "").Replace("/", "-");
        }

        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (!licensed_to.Equals(Magnemite()) && uses_license)
            {
                MessageBox.Show("Program doesn't belong to you! If u think this is error contact the seller!");
                Application.Exit();
                return;
            }
            DateTime ctime = GetNetworkTime();
            DateTime temp;
            // Try to check if got time from NTP
            if(uses_date) { 
                if (DateTime.TryParse(ctime.ToString(), out temp))
                {
                    // compare dates
                    int result = DateTime.Compare(myDate, ctime);
                    if (result > 0)
                    {
                        MessageBox.Show(string.Format("Your copy is valid until: {0}", (string)myDate.ToString()));
                    }
                    else {
                        MessageBox.Show(string.Format("Your copy has expired! It was valid until {0}", (string)myDate.ToString()));
                        Application.Exit();
                        return;
                    }
                }
                else
                {
                    // If time failed
                    MessageBox.Show(string.Format("It was failure with date checkup! Program won't start!"));
                    Application.Exit();
                    return;
                }
            }

            KageControl mainForm = new KageControl();
            mainForm.Show();
            Application.Run(mainForm);

        }

        public static DateTime GetNetworkTime()
        {
            //default Windows time server
            const string ntpServer = "time.windows.com";

            // NTP message size - 16 bytes of the digest (RFC 2030)
            var ntpData = new byte[48];

            //Setting the Leap Indicator, Version Number and Mode values
            ntpData[0] = 0x1B; //LI = 0 (no warning), VN = 3 (IPv4 only), Mode = 3 (Client Mode)

            var addresses = Dns.GetHostEntry(ntpServer).AddressList;

            //The UDP port number assigned to NTP is 123
            var ipEndPoint = new IPEndPoint(addresses[0], 123);
            //NTP uses UDP

            using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
            {
                socket.Connect(ipEndPoint);

                //Stops code hang if NTP is blocked
                socket.ReceiveTimeout = 3000;

                socket.Send(ntpData);
                socket.Receive(ntpData);
                socket.Close();
            }

            //Offset to get to the "Transmit Timestamp" field (time at which the reply 
            //departed the server for the client, in 64-bit timestamp format."
            const byte serverReplyTime = 40;

            //Get the seconds part
            ulong intPart = BitConverter.ToUInt32(ntpData, serverReplyTime);

            //Get the seconds fraction
            ulong fractPart = BitConverter.ToUInt32(ntpData, serverReplyTime + 4);

            //Convert From big-endian to little-endian
            intPart = SwapEndianness(intPart);
            fractPart = SwapEndianness(fractPart);

            var milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);

            //**UTC** time
            var networkDateTime = (new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddMilliseconds((long)milliseconds);

            return networkDateTime.ToLocalTime();
        }

        // stackoverflow.com/a/3294698/162671
        static uint SwapEndianness(ulong x)
        {
            return (uint)(((x & 0x000000ff) << 24) +
                           ((x & 0x0000ff00) << 8) +
                           ((x & 0x00ff0000) >> 8) +
                           ((x & 0xff000000) >> 24));
        }

        public static KageForm newDitto()
        {
            KageForm item = new KageForm();
            dittos.Add(item);
            item.Show();
            return item;
        }

        private static void newThread()
        {
            KageForm item = new KageForm();
            dittos.Add(item);
            Application.Run(item);
        }
    }
}
