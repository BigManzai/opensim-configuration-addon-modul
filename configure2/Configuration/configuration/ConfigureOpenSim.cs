using System;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;

namespace OpenSim.Configuration
{
	public class ClassConfigureOpenSim
	{
		#region ConfigureOpenSim
		///ConfigureOpenSim()
		public static void ConfigureOpenSim()
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
							//	   BaseHostname = "127.0.0.1"
							if (line.Contains("BaseHostname"))
								if (line.Contains("127.0.0.1"))
									line = line.Replace("127.0.0.1", Configure.ipAddress);

							//	   ; http_listener_port = 9000
							if (line.Contains("http_listener_port"))
								line = line.Replace("; http_listener_port = 9000", "http_listener_port = \"" + Configure.portAddress + "\"");

							//	   ; user_agent = "OpenSim LSL (Mozilla Compatible)"
							if (line.Contains("user_agent"))
								line = line.Replace("; user_agent", "user_agent");

							//	   [ClientStack.LindenUDP]
							//	   ; DisableFacelights = "false"
							// client_throttle_max_bps = 400000
							// scene_throttle_max_bps = 75000000
							if (line.Contains("DisableFacelights"))
								line = line.Replace("; DisableFacelights = \"false\"", "DisableFacelights = \"true\"\n    client_throttle_max_bps = 400000\n    scene_throttle_max_bps = 75000000");


							if (line.Contains("Startup"))
								line = line.Replace("; SearchServerURI =", "SearchServerURI = ${Const|BaseURL}:${Const|PublicPort}");
							if (line.Contains("Startup"))
								line = line.Replace("; SearchServerURI =", "SearchServerURI = ${Const|BaseURL}:${Const|PublicPort}");
							if (line.Contains("Startup"))
								line = line.Replace("; SearchServerURI =", "SearchServerURI = ${Const|BaseURL}:${Const|PublicPort}");
							if (line.Contains("Startup"))
								line = line.Replace("; SearchServerURI =", "SearchServerURI = ${Const|BaseURL}:${Const|PublicPort}");

							/*
							; the default view range. Viewers override this (no major effect still )
							DefaultDrawDistance = 128.0

							; limit the maximum view range (no effect still(does limit MaxRegionsViewDistance) )
							MaxDrawDistance = 128

							; Other regions visibility depends on avatar position and view range
							; the view range considered is limited the maximum and minimum distances:
							MaxRegionsViewDistance = 128
							MinRegionsViewDistance = 48
							*/
							if (line.Contains("NoVerifyCertChain"))
								line = line.Replace("; NoVerifyCertChain = true", "; NoVerifyCertChain = true\n\n    ; the default view range limited\n    DefaultDrawDistance = 128.0\n    MaxDrawDistance = 128\n    MinRegionsViewDistance = 48\n    MaxRegionsViewDistance = 128");
							
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
								line = line.Replace("; gridname = \"OSGrid\"", "gridname = \"" + Configure.worldName + "\"");

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
								if (Configure.modus == "Standalone")
								{
									line = line.Replace("config-include/Standalone.ini", "config-include/Standalone.ini");
								}
							if (Configure.modus == "StandaloneHG")
							{
								line = line.Replace("config-include/Standalone.ini", "config-include/StandaloneHypergrid.ini");
							}
							if (Configure.modus == "Grid")
							{
								line = line.Replace("config-include/Standalone.ini", "config-include/Grid.ini");
							}
							if (Configure.modus == "GridHG")
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
				Console.WriteLine("Error configuring OpenSim.ini " + e.Message);
				return;
			}

			Console.WriteLine("OpenSim.ini has been successfully configured");
		}
		#endregion
	}
}