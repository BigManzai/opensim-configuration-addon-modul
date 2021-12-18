using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSim.Configuration
{
    public class OpenSimIni
    {
        public static (string AsString, bool AsBool, short AsShort, int AsInt, long AsLong, float AsFloat, double AsDouble) Read(string filename, string key, string section = null)
        {
            using (OpenSimIniStream OpenSimIniStream = new OpenSimIniStream(filename))
            {
                return OpenSimIniStream.Read(key, section);
            }
        }

        public static bool KeyExists(string filename, string key, string section = null)
        {
            using (OpenSimIniStream OpenSimIniStream = new OpenSimIniStream(filename))
            {
                return OpenSimIniStream.KeyExists(key, section);
            }
        }

        public static bool SectionExists(string filename, string section)
        {
            using (OpenSimIniStream OpenSimIniStream = new OpenSimIniStream(filename))
            {
                return OpenSimIniStream.SectionExists(section);
            }
        }

        public static void Write<T>(string filename, T content, string key, string section = null)
        {
            using (OpenSimIniStream OpenSimIniStream = new OpenSimIniStream(filename))
            {
                OpenSimIniStream.Write(content, key, section);
            }
        }

        public static bool DeleteKey(string filename, string key, string section = null)
        {
            using (OpenSimIniStream OpenSimIniStream = new OpenSimIniStream(filename))
            {
                return OpenSimIniStream.DeleteKey(key, section);
            }
        }

        public static bool DeleteSection(string filename, string section)
        {
            using (OpenSimIniStream OpenSimIniStream = new OpenSimIniStream(filename))
            {
                return OpenSimIniStream.DeleteSection(section);
            }
        }

        public static List<string> GetAllSections(string filename)
        {
            using (OpenSimIniStream OpenSimIniStream = new OpenSimIniStream(filename))
            {
                return OpenSimIniStream.GetAllSections();
            }
        }

        public static List<string> GetAllKeys(string filename, string section = "")
        {
            using (OpenSimIniStream OpenSimIniStream = new OpenSimIniStream(filename))
            {
                return OpenSimIniStream.GetAllKeys(section);
            }
        }
    }
}
