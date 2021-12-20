using System;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;

namespace OpenSim.Configuration
{
	public class ClassConfigureFlotsamCache
	{
		#region ConfigureFlotsamCache
		///ConfigureFlotsamCache
		public static void ConfigureFlotsamCache()
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
							//LogLevel = 0

							if (line.Contains("LogLevel")) if (line.Contains("LogLevel")) line = line.Replace("LogLevel = 0", "LogLevel = 0");

							tw.WriteLine(line);
						}
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("Error configuring FlotsamCache.ini " + e.Message);
				return;
			}

			Console.WriteLine("FlotsamCache.ini has been successfully configured");
		}
		#endregion
	}
}