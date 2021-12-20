/*
 *THIS SOFTWARE IS PROVIDED ''AS IS'' AND ANY
 * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL THE CONTRIBUTORS BE LIABLE FOR ANY
 * DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 *LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
 * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

// version = "Version 0.0.5";

using System;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Xml;

using Nini.Config;
using OpenMetaverse;



namespace OpenSim.Configuration
{
	public class Configure
    {
		public static string worldName = "MyVirtualWorld";
        public static string dbHost = "localhost";
        public static string dbSchema = "opensim";
        public static string dbUser = "opensim";
        public static string dbPasswd = "secret";
		public static string ipAddress = "127.0.0.1";
		public static string modus = "GridHG";
		public static string regionSize = "256";
		public static string regionName = "Welcome";
		public static string location = "2500,2500";
		public static string portAddress = "9010";
		public static string osPort = "9010";
		public static string regionPort = "9100";

		#region GetPublicIP
		/// Determine IP
		public static string GetPublicIP()
		{
			//youreip.php
			//<? php
			//$ip = $_SERVER["REMOTE_ADDR"];
			//echo "Current IP Address: $ip";  
			//?>

			// Insert configuration setting here
			string url = "http://checkip.dyndns.org";
			//string url = "http://opensimulator.org/youreip.php";

			System.Net.WebRequest req = System.Net.WebRequest.Create(url);
			System.Net.WebResponse resp = req.GetResponse();
			System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
			string response = sr.ReadToEnd().Trim();
			string[] a = response.Split(':');
			string a2 = a[1].Substring(1);
			string[] a3 = a2.Split('<');
			string a4 = a3[0];
			return a4;
		}
		#endregion

		#region GetNextFreePort
		/// find the next available TCP port within a given range
		public static int GetNextFreePort(int min, int max)
		{
			if (max < min)
				throw new ArgumentException("Max cannot be less than min.");

			var ipProperties = IPGlobalProperties.GetIPGlobalProperties();

			var usedPorts =
				ipProperties.GetActiveTcpConnections()
					.Where(connection => connection.State != TcpState.Closed)
					.Select(connection => connection.LocalEndPoint)
					.Concat(ipProperties.GetActiveTcpListeners())
					.Concat(ipProperties.GetActiveUdpListeners())
					.Select(endpoint => endpoint.Port)
					.ToArray();

			var firstUnused =
				Enumerable.Range(min, max - min)
					.Where(port => !usedPorts.Contains(port))
					.Select(port => new int?(port))
					.FirstOrDefault();

			if (!firstUnused.HasValue)
				throw new Exception($"All local TCP ports between {min} and {max} are currently in use.");

			return firstUnused.Value;
		}
		#endregion

		#region SaveIni
		/// Save Config to *.ini.old.dd-MM-yyyy-hh-mm-ss
		private static void SaveIni()
		{
			string Timestamp = DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss");

			string path1 = "config-include/FlotsamCache.ini";
			bool result1 = File.Exists(path1);
			if (result1 == true)
			{
				System.IO.File.Move("config-include/FlotsamCache.ini", "config-include/FlotsamCache.ini.old" + "." + Timestamp);
			}	
			string path2 = "MoneyServer.ini";
			bool result2 = File.Exists(path2);
			if (result2 == true)
			{
				System.IO.File.Move("MoneyServer.ini", "MoneyServer.ini.old" + "." + Timestamp);
			}
			string path3 = "OpenSim.ini";
			bool result3 = File.Exists(path3);
			if (result3 == true)
			{
				System.IO.File.Move("OpenSim.ini", "OpenSim.ini.old" + "." + Timestamp);
			}
			string path4 = "Robust.ini";
			bool result4 = File.Exists(path4);
			if (result4 == true)
			{
				System.IO.File.Move("Robust.ini", "Robust.ini.old" + "." + Timestamp);
			}
			string path5 = "Robust.HG.ini";
			bool result5 = File.Exists(path5);
			if (result5 == true)
			{
				System.IO.File.Move("Robust.HG.ini", "Robust.HG.ini.old" + "." + Timestamp);
			}
			string path6 = "config-include/StandaloneCommon.ini";
			bool result6 = File.Exists(path6);
			if (result6 == true)
			{
				System.IO.File.Move("config-include/StandaloneCommon.ini", "config-include/StandaloneCommon.ini.old" + "." + Timestamp);
			}
			string path7 = "config-include/GridCommon.ini";
			bool result7 = File.Exists(path7);
			if (result7 == true)
			{
				System.IO.File.Move("config-include/GridCommon.ini", "config-include/GridCommon.ini.old" + "." + Timestamp);
			}
			string path8 = "config-include/osslEnable.ini";
			bool result8 = File.Exists(path8);
			if (result8 == true)
			{
				System.IO.File.Move("config-include/osslEnable.ini", "config-include/osslEnable.ini.old" + "." + Timestamp);
			}
			string path9 = "Regions/Regions.ini";
			bool result9 = File.Exists(path9);
			if (result9 == true)
			{
				System.IO.File.Move("Regions/Regions.ini", "Regions/Regions.ini.old" + "." + Timestamp);
			}
		}
		#endregion

		#region GetUserInput
		public static void GetUserInput()
        {
			string tmp;
			string myIP = GetPublicIP();
			

			Console.Write("Standalone, StandaloneHG, Grid, [GridHG]: ");
			modus = Console.ReadLine();
			if (modus == string.Empty)
				modus = "GridHG";

			Console.Write("Name of Your Virtual World: ");
            worldName = Console.ReadLine();
            if (worldName == string.Empty)
                worldName = "My Virtual World";
            else
                worldName = worldName.Trim();

            Console.Write("MySql database host: [localhost] ");
            tmp = Console.ReadLine();
            if (tmp != string.Empty)
                dbHost = tmp;

            Console.Write("MySql database name: [opensim] ");
            tmp = Console.ReadLine();
            if (tmp != string.Empty)
                dbSchema = tmp;

            Console.Write("MySql database user account: [opensim] ");
            tmp = Console.ReadLine();
            if (tmp != string.Empty)
                dbUser = tmp;

            Console.Write("MySql database password: ");
            dbPasswd = Console.ReadLine();

			Console.Write("Your external domain name or IP address: [" + myIP + "] ");
            ipAddress = Console.ReadLine();
            if (ipAddress == string.Empty)
                ipAddress = myIP;

			int Port = GetNextFreePort(9010, 9099);
			string osPort = Convert.ToString(Port);
			Console.Write("Your OpenSim Port: [" + osPort + "] ");
			portAddress = Console.ReadLine();
			if (portAddress == string.Empty) portAddress = osPort;

			Console.Write("region name [Welcome]: ");
			tmp = Console.ReadLine();
			if (tmp != string.Empty)
				regionName = tmp;

			Console.Write("Location = [2500,2500]: ");
			tmp = Console.ReadLine();
			if (tmp != string.Empty)
				location = tmp;

			Console.Write("region size [256]: ");
			tmp = Console.ReadLine();
			if (tmp != string.Empty)
				regionSize = tmp;

			int RPort = GetNextFreePort(9100, 9199);
			string osrPort = Convert.ToString(RPort);
			Console.Write("Region Port: [" + osrPort + "] ");
			regionPort = Console.ReadLine();
			if (regionPort == string.Empty)	regionPort = osrPort;

			Console.WriteLine("\n***************************************************");
        }
		#endregion

		#region DisplayInfo
		private static void DisplayInfo()
        {
            Console.WriteLine("\n***************************************************");
            Console.WriteLine("Your Virtual World is " + worldName);
            Console.WriteLine("Your loginuri is http://" + ipAddress + ":8002");
			Console.WriteLine("You start region is:");
			Console.WriteLine("  Region name: " + regionName);
			Console.WriteLine("  Region Port: " + regionPort);
			Console.WriteLine("  Region Size: " + regionSize);
			Console.WriteLine("  Location:   " + location);
			Console.WriteLine("***************************************************\n");
            Console.Write("<Press enter to exit>");
            Console.ReadLine();
        }
        #endregion

        #region Main
        public static void Main(string[] args)
		{
			GetUserInput();

			SaveIni(); // Save Config to *.ini.old.dd-MM-yyyy-hh-mm-ss

			ClassConfigureFlotsamCache.ConfigureFlotsamCache(); //OK
			ClassConfigureMoneyServer.ConfigureMoneyServer(); //OK
			ClassConfigureOpenSim.ConfigureOpenSim(); //OK
			ClassConfigureRobust.ConfigureRobust(); //OK
			ClassConfigureRobustHG.ConfigureRobustHG(); //OK
			ClassConfigureStandaloneCommon.ConfigureStandaloneCommon(); //OK
			ClassConfigureGridCommon.ConfigureGridCommon(); //OK
			ClassConfigureosslEnable.ConfigureosslEnable(); // OK
			ClassConfigureRegions.ConfigureRegions(); //OK

			DisplayInfo();

		}
		#endregion
	}
}