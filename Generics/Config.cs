using System.Collections.Specialized;
using System.Configuration;

namespace Generics
{
    public class Config
    {
        private readonly static NameValueCollection AppSettings = ConfigurationManager.AppSettings;
        public readonly static string LOGPATH = AppSettings["LogFile"];
        public readonly static string JSONPATH = AppSettings["JSONFile"];
    }
}