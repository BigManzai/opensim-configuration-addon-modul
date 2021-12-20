using System;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;

namespace OpenSim.Configuration
{
	public class ClassConfigureosslEnable
	{
		#region ConfigureosslEnable
		public static void ConfigureosslEnable()
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
							/*
							 * OSFunctionThreatLevel = VeryLow > VeryHigh
							 * 
							  osslParcelO = "" > osslParcelO = "PARCEL_OWNER,"
							  osslParcelOG = "" > osslParcelOG = "PARCEL_GROUP_MEMBER,PARCEL_OWNER,"
							*/
							if (line.Contains("OSFunctionThreatLevel"))
								line = line.Replace("OSFunctionThreatLevel = VeryLow", "OSFunctionThreatLevel = VeryHigh");

							if (line.Contains("osslParcelO"))
								line = line.Replace("osslParcelO = \"\"", "osslParcelO = \"PARCEL_OWNER, \"");

							if (line.Contains("osslParcelOG"))
								line = line.Replace("osslParcelOG = \"\"", "osslParcelOG = \"PARCEL_GROUP_MEMBER, PARCEL_OWNER, \"");

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
		#endregion
	}
}