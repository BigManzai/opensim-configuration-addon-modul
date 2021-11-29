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


namespace OpenSim.Configuration
{
    public class Configure
    {
		/// voreinstellungen
		private static string worldName = "MyVirtualWorld";
        private static string dbHost = "localhost";
        private static string dbSchema = "opensim";
        private static string dbUser = "opensim";
        private static string dbPasswd = "secret";
		private static string ipAddress = "127.0.0.1";
		private static string modus = "GridHG";
		private static string regionSize = "256";
		private static string regionName = "Welcome";
		private static string location = "2500,2500";

		/// Determine IP 
		public static string GetPublicIP()
		{
			// Determine IP 
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

		/// start
		public static void Main(string[] args)
        {
			GetUserInput();

			SaveIni(); // Save Config to *.ini.old.dd-MM-yyyy-hh-mm-ss

			ConfigureFlotsamCache(); // OK
			ConfigureMoneyServer(); // OK
			ConfigureOpenSim(); // OK
			ConfigureRobust(); // OK
			ConfigureRobustHG(); // OK
			ConfigureStandaloneCommon(); // OK
			ConfigureGridCommon(); // OK
			ConfigureosslEnable(); // OK empty 
			ConfigureRegions(); // OK empty 

			DisplayInfo();
        }
		
		/// User input
        private static void GetUserInput()
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

			Console.Write("region name [Welcome]: ");
			tmp = Console.ReadLine();
			if (tmp != string.Empty)
				regionName = tmp;

			Console.Write("Location [2500,2500]: ");
			tmp = Console.ReadLine();
			if (tmp != string.Empty)
				location = tmp;

			Console.Write("region size [256]: ");
			tmp = Console.ReadLine();
			if (tmp != string.Empty)
				regionSize = tmp;

			Console.WriteLine("\n ");
        }

		// #############################################################################

		///ConfigureFlotsamCache
		private static void ConfigureFlotsamCache()
		{
			try
			{
				using (TextReader tr = new StreamReader("config-include/FlotsamCache.ini.example"))
				{
					using (TextWriter tw = new StreamWriter("config-include/FlotsamCache.ini"))
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
				Console.WriteLine("Error configuring FlotsamCache " + e.Message);
				return;
			}

			Console.WriteLine("FlotsamCache has been successfully configured");
		}

		///ConfigureMoneyServer OK
		private static void ConfigureMoneyServer()
		{
			// Dateiprüfung
			string path = "MoneyServer.ini.example";
			bool result = File.Exists(path);
			if (result == true)
			{
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

								if (line.Contains("localhost"))
									line = line.Replace("localhost", dbHost);

								//database = Database_name
								if (line.Contains("Database_name"))
									line = line.Replace("Database_name", dbSchema);

								//username = Database_user
								if (line.Contains("Database_user"))
									line = line.Replace("Database_user", dbUser);

								//password = Database_password
								if (line.Contains("Database_password"))
									line = line.Replace("Database_password", dbPasswd);

								//; EnableScriptSendMoney = true
								if (line.Contains(";EnableScriptSendMoney"))
									line = line.Replace(";EnableScriptSendMoney", "EnableScriptSendMoney");

								//; MoneyScriptAccessKey = "123456789"; ; Specify same secret key in include / config.php or WI(XoopenSim/ Modlos)
								if (line.Contains(";MoneyScriptAccessKey"))
									line = line.Replace(";MoneyScriptAccessKey", "MoneyScriptAccessKey");

								//; MoneyScriptIPaddress = "202.26.159.139"; ; Not use 127.0.0.1.This is used to generate Script key
								if (line.Contains(";MoneyScriptIPaddress"))
									line = line.Replace(";MoneyScriptIPaddress", "MoneyScriptIPaddress");
									line = line.Replace("202.26.159.139", ipAddress);

								tw.WriteLine(line);
							}
						}
					}
				}
				catch (Exception e)
				{
					Console.WriteLine("Error configuring MoneyServer.ini " + e.Message);
					return;
				}

				Console.WriteLine("MoneyServer.ini has been successfully configured");
			}
			else
			{
				Console.WriteLine("MoneyServer.ini.example Not Found");
			}// Dateiprüfung Ende
		}
		
		///ConfigureOpenSim()
		private static void ConfigureOpenSim()
		{
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
							// client_throttle_max_bps = 400000
							// scene_throttle_max_bps = 75000000
							if (line.Contains("DisableFacelights"))
                                line = line.Replace("; DisableFacelights = \"false\"", "DisableFacelights = \"true\"\n    client_throttle_max_bps = 400000\n    scene_throttle_max_bps = 75000000");

							// [SimulatorFeatures]
							// ; SearchServerURI = "http://127.0.0.1:9000/"
							// SearchServerURI = "${Const|BaseURL}:${Const|PublicPort}"
							// ; DestinationGuideURI = "http://127.0.0.1:9000/guide"
							// DestinationGuideURI = "${Const|BaseURL}:${Const|PublicPort}"
							if (line.Contains("enable_windlight"))
								line = line.Replace("; SearchServerURI =", "SearchServerURI = ${Const|BaseURL}:${Const|PublicPort}");
							if (line.Contains("enable_windlight"))
								line = line.Replace("; DestinationGuideURI =", "DestinationGuideURI = ${Const|BaseURL}:${Const|PublicPort}");

							// ; ObjectsCullingByDistance = false
							if (line.Contains("ObjectsCullingByDistance"))
								line = line.Replace("; ObjectsCullingByDistance = false", "ObjectsCullingByDistance = true");

							// [LandManagement]
							// ShowParcelBansLines = true
							if (line.Contains("ShowParcelBansLines"))
								line = line.Replace(";ShowParcelBansLines = false", "ShowParcelBansLines = true");

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

							// [UserProfiles]
							// ;; ProfileServiceURL = ${Const|BaseURL}:${Const|PublicPort}
							// ProfileServiceURL = ${Const|BaseURL}:${Const|PublicPort}
							// ; AllowUserProfileWebURLs = true
							// AllowUserProfileWebURLs = true
							if (line.Contains("ProfileServiceURL"))
								line = line.Replace(";; ProfileServiceURL = ${Const|BaseURL}:${Const|PublicPort}", "ProfileServiceURL = ${Const|BaseURL}:${Const|PublicPort}");
							if (line.Contains("AllowUserProfileWebURLs"))
								line = line.Replace("; AllowUserProfileWebURLs = true", "AllowUserProfileWebURLs = true");

							// [XBakes]
							// ;; URL = ${Const|PrivURL}:${Const|PrivatePort}
							if (line.Contains("XBakes"))
								line = line.Replace(";; URL = ${Const|PrivURL}:${Const|PrivatePort}", "URL = ${Const|PrivURL}:${Const|PrivatePort}");

							//	   [Architecture]
							if (line.Contains("Include-Architecture"))
								line = line.Replace("; Include-Architecture = \"config-include/StandaloneHypergrid.ini\"", " ");
								line = line.Replace("; Include-Architecture = \"config-include/Grid.ini\"", " ");
								line = line.Replace("; Include-Architecture = \"config-include/GridHypergrid.ini\"", " ");


							// modus = "Standalone, StandaloneHG, Grid, [GridHG]
							if (line.Contains("Include-Architecture"))
								if (modus == "Standalone")
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

							//	[GridService]
							// ; Region_Welcome_Area = "DefaultRegion, FallbackRegion"
							if (line.Contains("Region_Welcome_Area"))
								if (line.Contains("Region_Welcome_Area"))
									line = line.Replace("; Region_Welcome_Area", "Region_" + regionName);

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

							//	[GridService]
							// ; Region_Welcome_Area = "DefaultRegion, FallbackRegion"
							if (line.Contains("Region_Welcome_Area"))
								if (line.Contains("Region_Welcome_Area"))
									line = line.Replace("; Region_Welcome_Area", "Region_" + regionName);

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
			try
			{
				using (TextReader tr = new StreamReader("config-include/StandaloneCommon.ini.example"))
				{
					using (TextWriter tw = new StreamWriter("config-include/StandaloneCommon.ini"))
					{
						string line;
						while ((line = tr.ReadLine()) != null)
						{
							// 	       [Const]
							// 	       BaseURL = "http://127.0.0.1"
							if (line.Contains("BaseURL"))
								if (line.Contains("http://127.0.0.1"))
									line = line.Replace("http://127.0.0.1", "http://" + ipAddress + "");

							// 	       [ServiceList]
							// 	       ; OfflineIMServiceConnector = "${Const|PrivatePort}/OpenSim.Addons.OfflineIM.dll:OfflineIMServiceRobustConnector"
							if (line.Contains("OfflineIMServiceConnector"))
								if (line.Contains("OfflineIMServiceConnector"))
									line = line.Replace("; OfflineIMServiceConnector", "OfflineIMServiceConnector");
							// 	       ; GroupsServiceConnector = "${Const|PrivatePort}/OpenSim.Addons.Groups.dll:GroupsServiceRobustConnector"
							if (line.Contains("OfflineIMServiceConnector"))
								if (line.Contains("GroupsServiceConnector"))
									line = line.Replace("; GroupsServiceConnector", "GroupsServiceConnector");
							// 	       ; BakedTextureService = "${Const|PrivatePort}/OpenSim.Server.Handlers.dll:XBakesConnector"
							if (line.Contains("BakedTextureService"))
								if (line.Contains("BakedTextureService"))
									line = line.Replace("; BakedTextureService", "BakedTextureService");
							// 	       ; UserProfilesServiceConnector = "${Const|PublicPort}/OpenSim.Server.Handlers.dll:UserProfilesConnector"
							if (line.Contains("UserProfilesServiceConnector"))
								if (line.Contains("UserProfilesServiceConnector"))
									line = line.Replace("; UserProfilesServiceConnector", "UserProfilesServiceConnector");
							// 	       ; HGGroupsServiceConnector = "${Const|PublicPort}/OpenSim.Addons.Groups.dll:HGGroupsServiceRobustConnector"
							if (line.Contains("HGGroupsServiceConnector"))
								if (line.Contains("HGGroupsServiceConnector"))
									line = line.Replace("; HGGroupsServiceConnector", "HGGroupsServiceConnector");

							// 	       [Hypergrid]
							//	; HomeURI = "${Const|BaseURL}:${Const|PublicPort}"
							if (line.Contains("HomeURI"))
								if (line.Contains("HomeURI"))
									line = line.Replace("; HomeURI =", "HomeURI =");

							//	; GatekeeperURI = "${Const|BaseURL}:${Const|PublicPort}"
							if (line.Contains("GatekeeperURI"))
								if (line.Contains("GatekeeperURI"))
									line = line.Replace("; GatekeeperURI", "GatekeeperURI");

							//	[GridService]
							// ; Region_Welcome_Area = "DefaultRegion, FallbackRegion"
							if (line.Contains("Region_Welcome_Area"))
								if (line.Contains("Region_Welcome_Area"))
									line = line.Replace("; Region_Welcome_Area", "Region_" + regionName);

							// 	       [GridInfoService]
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
							// 	       ; gatekeeper = ${Const|BaseURL}:${Const|PublicPort}/ 
							// GatekeeperURIAlias
							if (line.Contains("GatekeeperURIAlias"))
								if (line.Contains("GatekeeperURIAlias"))
									line = line.Replace("GatekeeperURIAlias", "; GatekeeperURIAlias");

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
				Console.WriteLine("Error configuring StandaloneCommon " + e.Message);
				return;
			}
			Console.WriteLine("StandaloneCommon has been successfully configured");
		}
		
		///ConfigureGridCommon()
		private static void ConfigureGridCommon()
		{
			try
			{
				using (TextReader tr = new StreamReader("config-include/GridCommon.ini.example"))
				{
					using (TextWriter tw = new StreamWriter("config-include/GridCommon.ini"))
					{
						string line;
						while ((line = tr.ReadLine()) != null)
						{
							//[DatabaseService]
							//	ConnectionString = "Data Source=localhost;Database=opensim;User ID=opensim;Password=*****;Old Guids=true;"
							// "Data Source="+dbHost+";Database="+ dbSchema+";User ID="+dbUser+";Password="+dbPasswd+";Old Guids=true;"
							// dbHost dbSchema dbUser dbPasswd
							if (line.Contains("ConnectionString"))
								if (line.Contains("ConnectionString"))
									line = line.Replace("Data Source=localhost;Database=opensim;User ID=opensim;Password=*****;Old Guids=true;", "\"Data Source=" + dbHost + ";Database=" + dbSchema + ";User ID=" + dbUser + ";Password=" + dbPasswd + ";Old Guids=true;\"");

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
				Console.WriteLine("Error configuring GridCommon " + e.Message);
				return;
			}

			Console.WriteLine("GridCommon has been successfully configured");
		}

		///ConfigureosslEnable()
		private static void ConfigureosslEnable()
		{
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
			// Dateiprüfung
			string path = "Regions.ini";
			bool result = File.Exists(path);
			if (result == true)
			{
				Console.WriteLine("Regions.ini already exist");
			}
			else
			{
				Guid UUID = Guid.NewGuid();

				StreamWriter writer = File.CreateText("Regions/Regions.ini");
			
				// Ein paar Einträge hineinschreiben
				writer.WriteLine("[" + regionName + "]");
				writer.WriteLine("RegionUUID = " + UUID);
				writer.WriteLine("Location = " + location);
				writer.WriteLine("InternalAddress = 0.0.0.0");
				writer.WriteLine("InternalPort = 9100");
				writer.WriteLine("AllowAlternatePorts = False");
				writer.WriteLine("ResolveAddress = False");
				writer.WriteLine("ExternalHostName = " + ipAddress);
				writer.WriteLine("SizeX = " + regionSize);
				writer.WriteLine("SizeY = " + regionSize);
				writer.WriteLine("SizeZ = " + regionSize);
				writer.WriteLine("MaptileStaticUUID = " + UUID);
				writer.WriteLine("NonPhysicalPrimMax = " + regionSize);
				writer.WriteLine("PhysicalPrimMax = 64");
				writer.WriteLine("; ClampPrimSize = false");
				writer.WriteLine("MaxPrims = 100000");
				writer.WriteLine("MaxAgents = 50");
				writer.WriteLine("; MaxPrimsPerUser = -1");

				writer.Close(); // Den Dateizugriff beenden

				Console.WriteLine("Regions.ini has been successfully configured");
			}// Dateiprüfung Ende
		}

		// #############################################################################

		/// Display
		private static void DisplayInfo()
        {
            Console.WriteLine("\n ");
            Console.WriteLine("Your Virtual World is " + worldName);
            Console.WriteLine("Your loginuri is http://" + ipAddress + ":8002");
			Console.WriteLine(" ");
			Console.WriteLine("You start region is:");
			Console.WriteLine("  Region name: " + regionName);
			Console.WriteLine("  Location:   " + location);
			Console.WriteLine(" \n");
            Console.Write("<Press enter to exit>");
            Console.ReadLine();
        }
    }
}