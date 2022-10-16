using System.Collections.Specialized;
using System.Configuration;

namespace Generics
{
    public class Config
    {
        private static readonly NameValueCollection AppSettings = ConfigurationManager.AppSettings;
        public static readonly string LOG_PATH = AppSettings["LogFile"];
        public static readonly string XML_PATH = AppSettings["XmlFile"];
        public static readonly string FRUIT_PATH = AppSettings["JsonFruitFile"];
        public static readonly string VEGETABLE_PATH = AppSettings["JsonVegetableFile"];
        public static readonly string BERRY_PATH = AppSettings["JsonBerryFile"];
        public static readonly string XML_FRUIT_PATH = AppSettings["XmlFruitFile"];
        public static readonly string XML_VEGETABLE_PATH = AppSettings["XmlVegetableFile"];
        public static readonly string XML_BERRY_PATH = AppSettings["XmlBerryFile"];
        public static readonly string ERRORTEXT = AppSettings["ErrorText"];
    }
}