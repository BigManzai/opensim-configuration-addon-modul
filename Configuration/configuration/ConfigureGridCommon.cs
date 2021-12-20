using System;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;

namespace OpenSim.Configuration
{
	public class ClassConfigureGridCommon
	{
		#region ConfigureGridCommon
		///ConfigureGridCommon()
		public static void ConfigureGridCommon()
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
							//	ConnectionString = "Data Source=localhost;Database=opensim;User ID=opensim;Password=*****;Old Guids=true;"
								if (line.Contains(";StorageProvider = "))
									line = line.Replace(";StorageProvider = \"OpenSim.Data.MySQL.dll\"", "StorageProvider = \"OpenSim.Data.MySQL.dll\"");

							//	ConnectionString = "Data Source=localhost;Database=opensim;User ID=opensim;Password=*****;Old Guids=true;"
								if (line.Contains(";ConnectionString"))
									line = line.Replace(";ConnectionString = \"Data Source=localhost;Database=opensim;User ID=opensim;Password=***;Old Guids=true;SslMode=None;", "ConnectionString = \"Data Source=" + Configure.dbHost + ";Database=" + Configure.dbSchema + ";User ID=" + Configure.dbUser + ";Password=" + Configure.dbPasswd + ";Old Guids=true;\"");
							
							if (line.Contains("Include-Storage = "))
									line = line.Replace("Include-Storage = ", ";Include-Storage = ");

							// ; gatekeeper = ${Const|BaseURL}:${Const|PublicPort}/ 
							//if (line.Contains("Hypergrid"))
							if (line.Contains("GatekeeperURI"))
									line = line.Replace("; GatekeeperURI =", "GatekeeperURI =");

							// "${Const|BaseURL}:${Const|PublicPort}" ipAddress + 8002
							//if (line.Contains("PublicPort"))
								if (line.Contains("PublicPort"))
									line = line.Replace("${Const|BaseURL}:${Const|PublicPort}", Configure.ipAddress + ":8002");

							//	"${Const|PrivURL}:${Const|PrivatePort}" ipAddress + 8003
							//if (line.Contains("PrivatePort"))
								if (line.Contains("PrivatePort"))
									line = line.Replace("${Const|PrivURL}:${Const|PrivatePort}", Configure.ipAddress + ":8003");

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
		#endregion
	}
}