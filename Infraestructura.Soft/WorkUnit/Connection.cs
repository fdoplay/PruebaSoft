using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;

using Microsoft.Practices.EnterpriseLibrary.Data;


namespace Infraestructura.Soft
{
    class Connection
    {
        private static Database database;
        public static Database DataBase
        {
            get
            {
                if (database == null)
                {
                    DatabaseProviderFactory factory = new DatabaseProviderFactory();
                    database = factory.Create(DatabaseHelper.ConexionData);
                }
                return database;
            }
        }

        public static DbConnection Connetion()
        {
            if (database == null)
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                database = factory.Create(DatabaseHelper.ConexionData);
            }
            return database.CreateConnection();
        }
    }
}
