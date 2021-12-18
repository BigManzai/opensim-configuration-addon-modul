using System;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;

namespace OpenSim.Configuration
{
	public class ClassConfigureMoneyServer
	{
		#region ConfigureMoneyServer
		///ConfigureMoneyServer OK
		public static void ConfigureMoneyServer()
		{
			// Dateipruefung
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
									line = line.Replace("localhost", Configure.dbHost);

								//database = Database_name
								if (line.Contains("Database_name"))
									line = line.Replace("Database_name", Configure.dbSchema);

								//username = Database_user
								if (line.Contains("Database_user"))
									line = line.Replace("Database_user", Configure.dbUser);

								//password = Database_password
								if (line.Contains("Database_password"))
									line = line.Replace("Database_password", Configure.dbPasswd);

								//; EnableScriptSendMoney = true
								if (line.Contains(";EnableScriptSendMoney"))
									line = line.Replace(";EnableScriptSendMoney", "EnableScriptSendMoney");

								//; MoneyScriptAccessKey = "123456789"; ; Specify same secret key in include / config.php or WI(XoopenSim/ Modlos)
								if (line.Contains(";MoneyScriptAccessKey"))
									line = line.Replace(";MoneyScriptAccessKey", "MoneyScriptAccessKey");

								//; MoneyScriptIPaddress = "202.26.159.139"; ; Not use 127.0.0.1.This is used to generate Script key
								if (line.Contains(";MoneyScriptIPaddress"))
									line = line.Replace(";MoneyScriptIPaddress", "MoneyScriptIPaddress");
								line = line.Replace("202.26.159.139", Configure.ipAddress);

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
			}
		}
		#endregion
	}
}