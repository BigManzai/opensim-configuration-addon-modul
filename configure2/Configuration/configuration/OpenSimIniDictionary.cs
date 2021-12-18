using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSim.Configuration
{
    public class OpenSimIniDictionary
    {
        private string _filename = String.Empty;
        
        /// <summary>
        /// The OpenSimIniDictionary class let you use the ini file as dictionary. !Important! Comments are not included
        /// </summary>
        /// <param name="filename"></param>
        public OpenSimIniDictionary(string filename)
        {
            _filename = filename;

            using (OpenSimIniStream OpenSimIniStream = new OpenSimIniStream(filename))
            {
                IniFile.Add("", new Dictionary<string, string>());

                foreach (string key in OpenSimIniStream.GetAllKeys("", false))
                {
                    IniFile[""].Add(key, OpenSimIniStream.Read(key).AsString);
                }

                foreach (string section in OpenSimIniStream.GetAllSections(false))
                {
                    IniFile.Add(section, new Dictionary<string, string>());

                    foreach (string key in OpenSimIniStream.GetAllKeys(section, false))
                    {
                        IniFile[section].Add(key, OpenSimIniStream.Read(key, section).AsString);
                    }
                }
            }
        }

        /// <summary>
        /// Returns the IniFile as Dictionary
        /// </summary>
        public Dictionary<string, Dictionary<string, string>> IniFile { get; set; } = new Dictionary<string, Dictionary<string, string>>();

        /// <summary>
        /// Saves the changes from the IniFile property
        /// </summary>
        /// <param name="cleanFormat"></param>
        public void Save(bool cleanFormat = true)
        {
            StringBuilder sb = new StringBuilder();

            foreach (string section in IniFile.Keys)
            {
                if (section != "")
                {
                    sb.AppendLine($"[{section}]");
                }

                foreach (string key in IniFile[section].Keys)
                {
                    sb.AppendLine(key + "=" + IniFile[section][key]);
                }

                if (cleanFormat)
                {
                    sb.AppendLine();
                }
            }

            File.WriteAllText(_filename, sb.ToString());
        }
    }
}
