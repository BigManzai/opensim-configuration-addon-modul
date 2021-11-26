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

using System;
using System.IO;
using System.Collections.Generic;
using System.Net;
using System.Text;
using OpenMetaverse;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace OpenSim.Configuration
{
    public class Configure
    {
        private static string worldName = "My Virtual World";
        private static string dbHost = "localhost";
        private static string dbSchema = "opensim";
        private static string dbUser = "opensim";
        private static string dbPasswd = "secret";
        private static string userFirst = "John";
        private static string userLast = "Doe";
        private static string userPasswd = "secret";
        private static string userEmail = "admin@localhost";
        private static string ipAddress = "127.0.0.1";
		private static string modus = "GridHG";

		private enum RegionConfigStatus : uint
        {
            OK = 0,
            NeedsCreation = 1,
            NeedsEditing = 2
        }

		/// <summary>
		/// IP ermitteln
		/// </summary>
		/// <returns></returns>
		public static string GetPublicIP()
		{
			// IP ermitteln
			string url = "http://checkip.dyndns.org";
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

		public static void Main(string[] args)
        {
            Console.WriteLine(Environment.Version);
			
            GetUserInput();

			ConfigureFlotsamCache(); // OK
			ConfigureMoneyServer(); // OK
			ConfigureOpenSim(); // OK
			ConfigureRobust(); // OK
			ConfigureRobustHG(); // OK
			//ConfigureStandaloneCommon();
			ConfigureGridCommon(); // OK
			ConfigureosslEnable(); // OK aber leer
			ConfigureRegions();

			DisplayInfo();
        }

        private static void GetUserInput()
        {
			// Benötigte Standart Vorgaben:
			// modus="Standalone, StandaloneHG, Grid, GridHG"X
			// worldName="My Virtual World"X
			// ipAddress="127.0.0.1"X
			// dbHost="localhost"X
			// dbUser="opensim"X
			// dbPasswd="secret"X
			// adminFirst="John"X
			// adminLast="Doe"X
			// adminPasswd="secret"X
			// adminEmail="admin@localhost"
			// adminUUID="00000000-0000-0000-0000-000000000000"


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

            Console.Write("User first name [John]: ");
            string input = Console.ReadLine();
            if (input != string.Empty)
                userFirst = input;

            Console.Write("User last name [Doe]: ");
            input = Console.ReadLine();
            if (input != string.Empty)
               userLast = input;

            Console.Write("User password [secret]: ");
            input = Console.ReadLine();
            if (input != string.Empty)
				userPasswd = input;

            Console.Write("User email [admin@localhost]: ");
            input = Console.ReadLine();
            if (input != string.Empty)
				userEmail = input;

            Console.WriteLine("");
        }

		// #############################################################################

		private static void CheckMyMoneyServerConfig()
		{
			if (File.Exists("MoneyServer.ini"))
			{
				try
				{
					File.Copy("MoneyServer.ini.example", "MoneyServer.ini");
				}
				catch
				{
					// ignore and proceed
				}
			}
		}

		private static void CheckMyFlotsamCacheConfig()
		{
			if (File.Exists("FlotsamCache.ini"))
			{
				try
				{
					File.Copy("FlotsamCache.ini.example", "FlotsamCache.ini");
				}
				catch
				{
					// ignore and proceed
				}
			}
		}

		private static void CheckMyOpenSimConfig()
		{
			if (File.Exists("OpenSim.ini"))
			{
				try
				{
					File.Copy("OpenSim.ini.example", "OpenSim.ini");
				}
				catch
				{
					// ignore and proceed
				}
			}
		}

		private static void CheckMyRobustConfig()
		{
			if (File.Exists("Robust.ini"))
			{
				try
				{
					File.Copy("Robust.ini.example", "Robust.ini");
				}
				catch
				{
					// ignore and proceed
				}
			}
		}

		private static void CheckMyRobustHGConfig()
		{
			if (File.Exists("Robust.ini"))
			{
				try
				{
					File.Copy("Robust.HG.ini.example", "Robust.ini");
				}
				catch
				{
					// ignore and proceed
				}
			}
		}

		private static void CheckMyStandaloneCommonConfig()
		{
			if (File.Exists("config-include/StandaloneCommon.ini"))
			{
				try
				{
					File.Copy("config-include/StandaloneCommon.ini.example", "config-include/StandaloneCommon.ini");
				}
				catch
				{
					// ignore and proceed
				}
			}
		}

		private static void CheckMyosslEnableConfig()
		{
			if (File.Exists("config-include/osslEnable.ini"))
			{
				try
				{
					File.Copy("config-include/osslEnable.ini.example", "config-include/osslEnable.ini");
				}
				catch
				{
					// ignore and proceed
				}
			}
		}

		private static void CheckMyGridCommonConfig()
		{
			if (File.Exists("config-include/GridCommon.ini"))
			{
				try
				{
					File.Copy("config-include/GridCommon.ini.example", "config-include/GridCommon.ini");
				}
				catch
				{
					// ignore and proceed
				}
			}
		}

		private static void CheckMyRegionsConfig()
		{
			if (File.Exists("Regions/Regions.ini"))
			{
				try
				{
					File.Copy("Regions/Regions.ini.example", "Regions/Regions.ini");
				}
				catch
				{
					// ignore and proceed
				}
			}
		}

		// #############################################################################

		///ConfigureFlotsamCache
		private static void ConfigureFlotsamCache()
		{
			// FlotsamCache.ini.example
			CheckMyFlotsamCacheConfig();
			try
			{
				using (TextReader tr = new StreamReader("config-include/FlotsamCache.ini.example"))
				{
					using (TextWriter tw = new StreamWriter("config-include/FlotsamCache.ini"))
					{
						string line;
						while ((line = tr.ReadLine()) != null)
						{
							//LogLevel = 0

							if (line.Contains("LogLevel"))
								if (line.Contains("LogLevel"))
									line = line.Replace("LogLevel = 0", "LogLevel = 0");

							tw.WriteLine(line);
						}
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("Error configuring FlotsamCache " + e.Message);
				return;
			}

			Console.WriteLine("FlotsamCache has been successfully configured");
		}

		///ConfigureMoneyServer OK
		private static void ConfigureMoneyServer()
		{
			CheckMyMoneyServerConfig();
			try
			{
				using (TextReader tr = new StreamReader("MoneyServer.ini.example"))
				{
					using (TextWriter tw = new StreamWriter("MoneyServer.ini"))
					{
						string line;
						while ((line = tr.ReadLine()) != null)
						{
							//hostname = localhost
							if (line.Contains("hostname"))
							if (line.Contains("localhost"))
								line = line.Replace("localhost", dbHost);

							//database = Database_name
							if (line.Contains("database"))
							if (line.Contains("Database_name"))
								line = line.Replace("Database_name", dbSchema);

							//username = Database_user
							if (line.Contains("username"))
							if (line.Contains("Database_user"))
								line = line.Replace("Database_user", dbUser);

							//password = Database_password
							if (line.Contains("password"))
							if (line.Contains("Database_password"))
								line = line.Replace("Database_password", dbPasswd);

							//; EnableScriptSendMoney = true
							if (line.Contains("EnableScriptSendMoney"))
							if (line.Contains("EnableScriptSendMoney"))
								line = line.Replace(";EnableScriptSendMoney", "EnableScriptSendMoney");

							//; MoneyScriptAccessKey = "123456789"; ; Specify same secret key in include / config.php or WI(XoopenSim/ Modlos)
							if (line.Contains("MoneyScriptAccessKey"))
							if (line.Contains("MoneyScriptAccessKey"))
								line = line.Replace(";MoneyScriptAccessKey", "MoneyScriptAccessKey");

							//; MoneyScriptIPaddress = "202.26.159.139"; ; Not use 127.0.0.1.This is used to generate Script key
							if (line.Contains("MoneyScriptIPaddress"))
							if (line.Contains("MoneyScriptIPaddress"))
								line = line.Replace(";MoneyScriptIPaddress", "MoneyScriptIPaddress");
								line = line.Replace("202.26.159.139", ipAddress);

							tw.WriteLine(line);
						}
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("Error configuring MyWorld " + e.Message);
				return;
			}

			Console.WriteLine("MoneyServer has been successfully configured");
		}
		
		///ConfigureOpenSim()
		private static void ConfigureOpenSim()
		{
			CheckMyOpenSimConfig();

			try
			{
				using (TextReader tr = new StreamReader("OpenSim.ini.example"))
				{
					using (TextWriter tw = new StreamWriter("OpenSim.ini"))
					{
						string line;
						while ((line = tr.ReadLine()) != null)
						{
							//	   [Const]
							//	   BaseHostname = "127.0.0.1"
							if (line.Contains("BaseHostname"))
							if (line.Contains("127.0.0.1"))
								line = line.Replace("127.0.0.1", ipAddress);

							//	   [Network]
							//	   ; http_listener_port = 9000
							if (line.Contains("http_listener_port"))
								line = line.Replace("; http_listener_port = 9000", "http_listener_port = 9010");

							//	   ; user_agent = "OpenSim LSL (Mozilla Compatible)"
							if (line.Contains("user_agent"))
								line = line.Replace("; user_agent", "user_agent");

							//	   [ClientStack.LindenUDP]
							//	   ; DisableFacelights = "false"
							if (line.Contains("DisableFacelights"))
                                line = line.Replace("; DisableFacelights = \"false\"", "DisableFacelights = \"true\"");
														
							//	   [LightShare]
							//	   ; enable_windlight = false
							if (line.Contains("enable_windlight"))
								line = line.Replace("; enable_windlight = false", "enable_windlight = true");

							//	   [Materials]
							//	   ; MaxMaterialsPerTransaction = 50
							if (line.Contains("MaxMaterialsPerTransaction"))
								line = line.Replace("; MaxMaterialsPerTransaction = 50", "MaxMaterialsPerTransaction = 250");

							//	   [DataSnapshot]
							//	   ; gridname = "OSGrid"
							if (line.Contains("gridname"))
								line = line.Replace("; gridname = \"OSGrid\"", "gridname = \""+ worldName+"\"");

							//	   [Terrain]
							//	   ; InitialTerrain = "pinhead-island"
							if (line.Contains("pinhead-island"))
								line = line.Replace("; InitialTerrain", "InitialTerrain");
								line = line.Replace("pinhead-island", "flat");

							// [XBakes]
							// ;; URL = ${Const|PrivURL}:${Const|PrivatePort}
							if (line.Contains("XBakes"))
								line = line.Replace(";; URL = ${Const|PrivURL}:${Const|PrivatePort}", "URL = ${Const|PrivURL}:${Const|PrivatePort}");							

							//	   [Architecture]
							// modus = "Standalone, StandaloneHG, Grid, [GridHG]
							if (line.Contains("Include-Architecture"))
								if ( modus == "Standalone" )
								{
									line = line.Replace("config-include/Standalone.ini", "config-include/Standalone.ini");
								}
								if (modus == "StandaloneHG")
								{
									line = line.Replace("config-include/Standalone.ini", "config-include/StandaloneHypergrid.ini");
								}
								if (modus == "Grid")
								{
									line = line.Replace("config-include/Standalone.ini", "config-include/Grid.ini");
								}
								if (modus == "GridHG")
								{
									line = line.Replace("config-include/Standalone.ini", "config-include/GridHypergrid.ini");
								}
							tw.WriteLine(line);
						}
					}
				}
			}


			catch (Exception e)
			{
				Console.WriteLine("Error configuring MyWorld " + e.Message);
				return;
			}

			Console.WriteLine("OpenSim has been successfully configured");
		}
		
		///ConfigureRobust()
		private static void ConfigureRobust()
		{
			CheckMyRobustConfig();

			string connString = String.Format("    ConnectionString = \"Data Source={0};Database={1};User ID={2};Password={3};Old Guids=true;Allow Zero Datetime=true;\"", dbHost, dbSchema, dbUser, dbPasswd);

			try
			{
				using (TextReader tr = new StreamReader("Robust.ini.example"))
				{
					using (TextWriter tw = new StreamWriter("Robust.ini"))
					{
						string line;
						while ((line = tr.ReadLine()) != null)
						{

							//	[Const]
							//	BaseURL = "http://127.0.0.1"
							if (line.Contains("BaseURL"))
								if (line.Contains("http://127.0.0.1"))
									line = line.Replace("http://127.0.0.1", "http://" + ipAddress + "");

							//	[ServiceList]
							//	; OfflineIMServiceConnector = "${Const|PrivatePort}/OpenSim.Addons.OfflineIM.dll:OfflineIMServiceRobustConnector"
							if (line.Contains("OfflineIMServiceConnector"))
								if (line.Contains("OfflineIMServiceConnector"))
									line = line.Replace("; OfflineIMServiceConnector", "OfflineIMServiceConnector");

							//	; GroupsServiceConnector = "${Const|PrivatePort}/OpenSim.Addons.Groups.dll:GroupsServiceRobustConnector"
							if (line.Contains("GroupsServiceConnector"))
								if (line.Contains("GroupsServiceConnector"))
									line = line.Replace("; GroupsServiceConnector", "GroupsServiceConnector");

							//	; BakedTextureService = "${Const|PrivatePort}/OpenSim.Server.Handlers.dll:XBakesConnector"
							if (line.Contains("BakedTextureService"))
								if (line.Contains("BakedTextureService"))
									line = line.Replace("; BakedTextureService", "BakedTextureService");

							//	; UserProfilesServiceConnector = "${Const|PublicPort}/OpenSim.Server.Handlers.dll:UserProfilesConnector"
							if (line.Contains("UserProfilesServiceConnector"))
								if (line.Contains("UserProfilesServiceConnector"))
									line = line.Replace("; UserProfilesServiceConnector", "UserProfilesServiceConnector");

							//	; GroupsServiceConnector = "${Const|PublicPort}/OpenSim.Addons.Groups.dll:HGGroupsServiceRobustConnector"
							if (line.Contains("GroupsServiceConnector"))
								if (line.Contains("GroupsServiceConnector"))
									line = line.Replace("; GroupsServiceConnector", "GroupsServiceConnector");

							//	[DatabaseService]
							//	StorageProvider = "OpenSim.Data.MySQL.dll"
							//	ConnectionString = "Data Source=localhost;Database=opensim;User ID=opensim;Password=*****;Old Guids=true;"
							// "Data Source="+dbHost+";Database="+ dbSchema+";User ID="+dbUser+";Password="+dbPasswd+";Old Guids=true;"
							// dbHost dbSchema dbUser dbPasswd
							if (line.Contains("ConnectionString"))
								if (line.Contains("ConnectionString"))
									line = line.Replace("Data Source=localhost;Database=opensim;User ID=opensim;Password=*****;Old Guids=true;", "\"Data Source=" + dbHost + ";Database=" + dbSchema + ";User ID=" + dbUser + ";Password=" + dbPasswd + ";Old Guids=true;\"");

							//	[GridInfoService]
							//	gridname = "the lost continent of hippo"
							if (line.Contains("gridname"))
								if (line.Contains("gridname"))
									line = line.Replace("the lost continent of hippo", worldName);

							//	gridnick = "hippogrid"
							if (line.Contains("gridnick"))
								if (line.Contains("gridnick"))
									line = line.Replace("gridnick = \"hippogrid\"", "gridnick = \"" + worldName + "\"");

							//	;welcome = ${Const|BaseURL}/welcome
							if (line.Contains("welcome"))
								if (line.Contains("welcome"))
									line = line.Replace(";welcome = ${Const|BaseURL}/welcome", "welcome = ${Const|BaseURL}/");

							//	;economy = ${Const|BaseURL}/economy
							if (line.Contains("economy"))
								if (line.Contains("economy"))
									line = line.Replace(";economy = ${Const|BaseURL}/economy", "economy = ${Const|BaseURL}/");

							//	;about = ${Const|BaseURL}/about
							if (line.Contains("about"))
								if (line.Contains("about"))
									line = line.Replace(";about = ${Const|BaseURL}/about", "about = ${Const|BaseURL}/");

							//	;register = ${Const|BaseURL}/register
							if (line.Contains("register"))
								if (line.Contains("register"))
									line = line.Replace(";register = ${Const|BaseURL}/register", "register = ${Const|BaseURL}/");

							//	;help = ${Const|BaseURL}/help
							if (line.Contains("help"))
								if (line.Contains("help"))
									line = line.Replace(";help = ${Const|BaseURL}/help", "help = ${Const|BaseURL}/");

							//	;password = ${Const|BaseURL}/password
							if (line.Contains("password"))
								if (line.Contains("password"))
									line = line.Replace(";password = ${Const|BaseURL}/password", "password = ${Const|BaseURL}/");



							tw.WriteLine(line);
						}
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("Error configuring Robust.HG.ini " + e.Message);
				return;
			}
			Console.WriteLine("Robust has been successfully configured");
		}

		///ConfigureRobustHG()
		private static void ConfigureRobustHG()
		{
			CheckMyRobustHGConfig();

			string connString = String.Format("    ConnectionString = \"Data Source={0};Database={1};User ID={2};Password={3};Old Guids=true;Allow Zero Datetime=true;\"", dbHost, dbSchema, dbUser, dbPasswd);

			try
			{
				using (TextReader tr = new StreamReader("Robust.HG.ini.example"))
				{
					using (TextWriter tw = new StreamWriter("Robust.HG.ini"))
					{
						string line;
						while ((line = tr.ReadLine()) != null)
						{

							//	[Const]
							//	BaseURL = "http://127.0.0.1"
							if (line.Contains("BaseURL"))
								if (line.Contains("http://127.0.0.1"))
									line = line.Replace("http://127.0.0.1","http://"+ipAddress+"");

							//	[ServiceList]
							//	; OfflineIMServiceConnector = "${Const|PrivatePort}/OpenSim.Addons.OfflineIM.dll:OfflineIMServiceRobustConnector"
							if (line.Contains("OfflineIMServiceConnector"))
								if (line.Contains("OfflineIMServiceConnector"))
									line = line.Replace("; OfflineIMServiceConnector", "OfflineIMServiceConnector");

							//	; GroupsServiceConnector = "${Const|PrivatePort}/OpenSim.Addons.Groups.dll:GroupsServiceRobustConnector"
							if (line.Contains("GroupsServiceConnector"))
								if (line.Contains("GroupsServiceConnector"))
									line = line.Replace("; GroupsServiceConnector", "GroupsServiceConnector");

							//	; BakedTextureService = "${Const|PrivatePort}/OpenSim.Server.Handlers.dll:XBakesConnector"
							if (line.Contains("BakedTextureService"))
								if (line.Contains("BakedTextureService"))
									line = line.Replace("; BakedTextureService", "BakedTextureService");

							//	; UserProfilesServiceConnector = "${Const|PublicPort}/OpenSim.Server.Handlers.dll:UserProfilesConnector"
							if (line.Contains("UserProfilesServiceConnector"))
								if (line.Contains("UserProfilesServiceConnector"))
									line = line.Replace("; UserProfilesServiceConnector", "UserProfilesServiceConnector");

							//	; HGGroupsServiceConnector = "${Const|PublicPort}/OpenSim.Addons.Groups.dll:HGGroupsServiceRobustConnector"
							if (line.Contains("HGGroupsServiceConnector"))
								if (line.Contains("HGGroupsServiceConnector"))
									line = line.Replace("; HGGroupsServiceConnector", "HGGroupsServiceConnector");
	
							//	[Hypergrid]
							//	; HomeURI = "${Const|BaseURL}:${Const|PublicPort}"
							if (line.Contains("HomeURI"))
								if (line.Contains("HomeURI"))
									line = line.Replace("; HomeURI", "HomeURI");

							//	; GatekeeperURI = "${Const|BaseURL}:${Const|PublicPort}"
							if (line.Contains("GatekeeperURI"))
								if (line.Contains("GatekeeperURI"))
									line = line.Replace("; GatekeeperURI", "GatekeeperURI");

							//	[DatabaseService]
							//	StorageProvider = "OpenSim.Data.MySQL.dll"
							//	ConnectionString = "Data Source=localhost;Database=opensim;User ID=opensim;Password=*****;Old Guids=true;"
							// "Data Source="+dbHost+";Database="+ dbSchema+";User ID="+dbUser+";Password="+dbPasswd+";Old Guids=true;"
							// dbHost dbSchema dbUser dbPasswd
							if (line.Contains("ConnectionString"))
								if (line.Contains("ConnectionString"))
									line = line.Replace("Data Source=localhost;Database=opensim;User ID=opensim;Password=*****;Old Guids=true;", "\"Data Source=" + dbHost + ";Database=" + dbSchema + ";User ID=" + dbUser + ";Password=" + dbPasswd + ";Old Guids=true;\"");

							//	[GridInfoService]
							//	gridname = "the lost continent of hippo"
							if (line.Contains("gridname"))
								if (line.Contains("gridname"))
									line = line.Replace("the lost continent of hippo", worldName);

							//	gridnick = "hippogrid"
							if (line.Contains("gridnick"))
								if (line.Contains("gridnick"))
									line = line.Replace("gridnick = \"hippogrid\"", "gridnick = \"" + worldName +"\"");

							//	;welcome = ${Const|BaseURL}/welcome
							if (line.Contains("welcome"))
								if (line.Contains("welcome"))
									line = line.Replace(";welcome = ${Const|BaseURL}/welcome", "welcome = ${Const|BaseURL}/");

							//	;economy = ${Const|BaseURL}/economy
							if (line.Contains("economy"))
								if (line.Contains("economy"))
									line = line.Replace(";economy = ${Const|BaseURL}/economy", "economy = ${Const|BaseURL}/");

							//	;about = ${Const|BaseURL}/about
							if (line.Contains("about"))
								if (line.Contains("about"))
									line = line.Replace(";about = ${Const|BaseURL}/about", "about = ${Const|BaseURL}/");

							//	;register = ${Const|BaseURL}/register
							if (line.Contains("register"))
								if (line.Contains("register"))
									line = line.Replace(";register = ${Const|BaseURL}/register", "register = ${Const|BaseURL}/");

							//	;help = ${Const|BaseURL}/help
							if (line.Contains("help"))
								if (line.Contains("help"))
									line = line.Replace(";help = ${Const|BaseURL}/help", "help = ${Const|BaseURL}/");

							//	;password = ${Const|BaseURL}/password
							if (line.Contains("password"))
								if (line.Contains("password"))
									line = line.Replace(";password = ${Const|BaseURL}/password", "password = ${Const|BaseURL}/");

							//	; gatekeeper = ${Const|BaseURL}:${Const|PublicPort}/
							if (line.Contains("gatekeeper"))
								if (line.Contains("gatekeeper"))
									line = line.Replace("; gatekeeper", "gatekeeper");

							// GatekeeperURIAlias
							if (line.Contains("GatekeeperURIAlias"))
								if (line.Contains("GatekeeperURIAlias"))
									line = line.Replace("GatekeeperURIAlias", "; GatekeeperURIAlias");

							tw.WriteLine(line);
						}
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("Error configuring Robust.HG.ini " + e.Message);
				return;
			}
			Console.WriteLine("RobustHG has been successfully configured");
		}
		
		///ConfigureStandaloneCommon()
		private static void ConfigureStandaloneCommon()
		{
			
			CheckMyStandaloneCommonConfig();

			string connString = String.Format("ConnectionString = \"Data Source={0};Database={1};User ID={2};Password={3};Old Guids=true;Allow Zero Datetime=true;\"", dbHost, dbSchema, dbUser, dbPasswd);

			try
			{
				using (TextReader tr = new StreamReader("config-include/StandaloneCommon.ini.example"))
				{
					using (TextWriter tw = new StreamWriter("config-include/StandaloneCommon.ini"))
					{
						string line;
						while ((line = tr.ReadLine()) != null)
						{
							/*
							[Const]
							BaseURL = "http://127.0.0.1"

							; The public port of the server
							PublicPort = "8002"

							; The private port of the server
							PrivatePort = "8003"

							[ServiceList]
							; OfflineIMServiceConnector = "${Const|PrivatePort}/OpenSim.Addons.OfflineIM.dll:OfflineIMServiceRobustConnector"
							; GroupsServiceConnector = "${Const|PrivatePort}/OpenSim.Addons.Groups.dll:GroupsServiceRobustConnector"
							; BakedTextureService = "${Const|PrivatePort}/OpenSim.Server.Handlers.dll:XBakesConnector"
							; UserProfilesServiceConnector = "${Const|PublicPort}/OpenSim.Server.Handlers.dll:UserProfilesConnector"
							; HGGroupsServiceConnector = "${Const|PublicPort}/OpenSim.Addons.Groups.dll:HGGroupsServiceRobustConnector"

							[Hypergrid]
							; HomeURI = "${Const|BaseURL}:${Const|PublicPort}"
							; GatekeeperURI = "${Const|BaseURL}:${Const|PublicPort}"
							; GatekeeperURIAlias = "login.osgrid.org"

							[DatabaseService]
							StorageProvider = "OpenSim.Data.MySQL.dll"
							ConnectionString = "Data Source=localhost;Database=opensim;User ID=opensim;Password=*****;Old Guids=true;"

							[GridInfoService]
							login = ${Const|BaseURL}:${Const|PublicPort}/
							gridname = "the lost continent of hippo"
							gridnick = "hippogrid"
							;welcome = ${Const|BaseURL}/welcome
							;economy = ${Const|BaseURL}/economy
							;about = ${Const|BaseURL}/about
							;register = ${Const|BaseURL}/register
							;help = ${Const|BaseURL}/help
							;password = ${Const|BaseURL}/password
							; gatekeeper = ${Const|BaseURL}:${Const|PublicPort}/ 
							*/

							// Hier einfügen

							tw.WriteLine(line);
						}
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("Error configuring MyWorld " + e.Message);
				return;
			}
			Console.WriteLine("StandaloneCommon has been successfully configured");
		}
		
		///ConfigureGridCommon()
		private static void ConfigureGridCommon()
		{

			CheckMyGridCommonConfig();

			string connString = String.Format("    ConnectionString = \"Data Source={0};Database={1};User ID={2};Password={3};Old Guids=true;Allow Zero Datetime=true;\"", dbHost, dbSchema, dbUser, dbPasswd);

			try
			{
				using (TextReader tr = new StreamReader("config-include/GridCommon.ini.example"))
				{
					using (TextWriter tw = new StreamWriter("config-include/GridCommon.ini"))
					{
						string line;
						while ((line = tr.ReadLine()) != null)
						{
							/*
							//[DatabaseService]
							//Include-Storage = "config-include/storage/SQLiteStandalone.ini";
							if (line.Contains("ConnectionString"))
								if (line.Contains("ConnectionString"))
									line = line.Replace("Include-Storage", "; Include-Storage");
									line = line.Replace(";StorageProvider", "StorageProvider");
									line = line.Replace(";ConnectionString", "ConnectionString");

							//	[DatabaseService]
							//	StorageProvider = "OpenSim.Data.MySQL.dll"
							//	ConnectionString = "Data Source=localhost;Database=opensim;User ID=opensim;Password=*****;Old Guids=true;"
							// "Data Source="+dbHost+";Database="+ dbSchema+";User ID="+dbUser+";Password="+dbPasswd+";Old Guids=true;"
							// dbHost dbSchema dbUser dbPasswd
							if (line.Contains("ConnectionString"))
								if (line.Contains("ConnectionString"))
									line = line.Replace("Data Source=localhost;Database=opensim;User ID=opensim;Password=*****;Old Guids=true;", "\"Data Source=" + dbHost + ";Database=" + dbSchema + ";User ID=" + dbUser + ";Password=" + dbPasswd + ";Old Guids=true;\"");
							*/

							if (line.Contains("GatekeeperURI"))
								if (line.Contains("GatekeeperURI"))
									line = line.Replace("; GatekeeperURI =", "GatekeeperURI =");

							// "${Const|BaseURL}:${Const|PublicPort}" ipAddress + 8002
							if (line.Contains("PublicPort"))
								if (line.Contains("PublicPort"))
									line = line.Replace("${Const|BaseURL}:${Const|PublicPort}", ipAddress + ":8002");

							//	"${Const|PrivURL}:${Const|PrivatePort}" ipAddress + 8003
							if (line.Contains("PrivatePort"))
								if (line.Contains("PrivatePort"))
									line = line.Replace("${Const|PrivURL}:${Const|PrivatePort}", ipAddress + ":8003");

							tw.WriteLine(line);
						}
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("Error configuring MyWorld " + e.Message);
				return;
			}

			Console.WriteLine("GridCommon has been successfully configured");
		}

		///ConfigureosslEnable()
		private static void ConfigureosslEnable()
		{
			CheckMyosslEnableConfig();

			try
			{
				using (TextReader tr = new StreamReader("config-include/osslEnable.ini.example"))
				{
					using (TextWriter tw = new StreamWriter("config-include/osslEnable.ini"))
					{
						string line;
						while ((line = tr.ReadLine()) != null)
						{

							tw.WriteLine(line);
						}
					}
				}
			}


			catch (Exception e)
			{
				Console.WriteLine("Error configuring osslEnable " + e.Message);
				return;
			}


			Console.WriteLine("osslEnable has been successfully configured");
		}

		///ConfigureRegions()
		private static void ConfigureRegions()
		{
			CheckMyRegionsConfig();
			try
			{
				using (TextReader tr = new StreamReader("Regions/Regions.ini.example"))
				{
					using (TextWriter tw = new StreamWriter("Regions/Regions.ini"))
					{
						string line;
						while ((line = tr.ReadLine()) != null)
						{
							//ExternalHostName = SYSTEMIP
							if (line.Contains("ExternalHostName"))
							if (line.Contains("ExternalHostName"))
								line = line.Replace("ExternalHostName = SYSTEMIP", "ExternalHostName = "+ipAddress);

							tw.WriteLine(line);
						}
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("Error configuring Regions.ini " + e.Message);
				return;
			}

			Console.WriteLine("Regions.ini has been successfully configured");
		}

			// #############################################################################

			private static void DisplayInfo()
        {
            Console.WriteLine("\n***************************************************");
            Console.WriteLine("Your Virtual World is " + worldName);
            Console.WriteLine("Your loginuri is http://" + ipAddress + ":8002");
            Console.WriteLine("You user account is:");
            Console.WriteLine("  username: " + userFirst + " " + userLast);
            Console.WriteLine("  passwd:   " + userPasswd +"\n");
            Console.WriteLine("***************************************************\n");
            Console.Write("<Press enter to exit>");
            Console.ReadLine();
        }
    }
}