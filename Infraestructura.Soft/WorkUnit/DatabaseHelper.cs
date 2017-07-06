using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Soft
{
    public sealed class DatabaseHelper
    {
        public const string ConexionData = "DefaultConnection";
        public static string GetDbConnectionString(string ConnectionString)
        {
            return ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString;
        }

        public static string GetDbProvider(string ConnectionString)
        {
            string ss = ConfigurationManager.ConnectionStrings[ConnectionString].ProviderName;
            return ConfigurationManager.ConnectionStrings[ConnectionString].ProviderName;
        }

        public static string GetSchema()
        {
            return ConfigurationManager.AppSettings.Get("BDEsquema").ToString();
        }

        public static Int32 Timeout = 500000;
    }
}
