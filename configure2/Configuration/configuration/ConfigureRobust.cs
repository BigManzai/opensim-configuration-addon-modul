using System;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;

namespace OpenSim.Configuration
{
	public class ClassConfigureRobust
	{
		#region ConfigureRobust
		public static void ConfigureRobust()
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
							//	BaseURL = "http://127.0.0.1"
							if (line.Contains("BaseURL"))
								if (line.Contains("http://127.0.0.1"))
									line = line.Replace("http://127.0.0.1", "http://" + Configure.ipAddress + "");

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

							//	ConnectionString = "Data Source=localhost;Database=opensim;User ID=opensim;Password=*****;Old Guids=true;"
							if (line.Contains("ConnectionString"))
								if (line.Contains("ConnectionString"))
									line = line.Replace("Data Source=localhost;Database=opensim;User ID=opensim;Password=*****;Old Guids=true;", "\"Data Source=" + Configure.dbHost + ";Database=" + Configure.dbSchema + ";User ID=" + Configure.dbUser + ";Password=" + Configure.dbPasswd + ";Old Guids=true;\"");

							// ; Region_Welcome_Area = "DefaultRegion, FallbackRegion"
							if (line.Contains("Region_Welcome_Area"))
								if (line.Contains("Region_Welcome_Area"))
									line = line.Replace("; Region_Welcome_Area", "Region_" + Configure.regionName);

							//	gridname = "the lost continent of hippo"
							if (line.Contains("gridname"))
								if (line.Contains("gridname"))
									line = line.Replace("the lost continent of hippo", Configure.worldName);

							//	gridnick = "hippogrid"
							if (line.Contains("gridnick"))
								if (line.Contains("gridnick"))
									line = line.Replace("gridnick = \"hippogrid\"", "gridnick = \"" + Configure.worldName + "\"");

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
		#endregion
	}
}