using System;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;

namespace OpenSim.Configuration
{
	public class ClassConfigureRegions
	{
		#region ConfigureRegions
		///ConfigureRegions()
		public static void ConfigureRegions()
		{
			// Dateipruefung
			string path = "Regions.ini";
			bool result = File.Exists(path);
			int rPort = Configure.GetNextFreePort(9100, 9199);
			string regionPort = Convert.ToString(rPort);
			if (result == true)
			{
				Console.WriteLine("Regions.ini already exist");
			}
			else
			{
				Guid UUID = Guid.NewGuid();

				StreamWriter writer = File.CreateText("Regions/Regions.ini");

				// Insert entries
				writer.WriteLine("[" + Configure.regionName + "]");
				writer.WriteLine("RegionUUID = " + UUID);
				writer.WriteLine("Location = " + Configure.location);
				writer.WriteLine("InternalAddress = 0.0.0.0");
				writer.WriteLine("InternalPort = " + Configure.regionPort);
				writer.WriteLine("AllowAlternatePorts = False");
				writer.WriteLine("ResolveAddress = False");
				writer.WriteLine("ExternalHostName = " + Configure.ipAddress);
				writer.WriteLine("SizeX = " + Configure.regionSize);
				writer.WriteLine("SizeY = " + Configure.regionSize);
				writer.WriteLine("SizeZ = " + Configure.regionSize);
				writer.WriteLine("MaptileStaticUUID = " + UUID);
				writer.WriteLine("NonPhysicalPrimMax = " + Configure.regionSize);
				writer.WriteLine("PhysicalPrimMax = 64");
				writer.WriteLine("; ClampPrimSize = false");
				writer.WriteLine("MaxPrims = 100000");
				writer.WriteLine("MaxAgents = 50");
				writer.WriteLine("; MaxPrimsPerUser = -1");

				writer.Close();

				Console.WriteLine("Regions.ini has been successfully configured");
			}
		}
		#endregion
	}
}