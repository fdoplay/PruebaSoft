using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Soft.Repository
{
    public class RepositorySpecialties
    {
        public bool Save(Domain.Soft.specialties spe)
        {
            bool result = false;
            string sql = @"BEGIN
	IF NOT EXISTS(SELECT 1 FROM specialties WHERE id = @id)
		BEGIN
			INSERT INTO specialties
			(
				id
				,specialty_type
				,url
			)
			VALUES
			(
				@id
				,@specialty_type
				,@url
			);
		END
	ELSE
		BEGIN
			UPDATE specialties SET
				specialty_type = @specialty_type
				,url = @url
			WHERE id = @id;
		END
END";
            using (DbCommand cmd = Connection.DataBase.GetSqlStringCommand(sql))
            {
                Connection.DataBase.AddInParameter(cmd, "@id", DbType.Int32, spe.id);
                Connection.DataBase.AddInParameter(cmd, "@specialty_type", DbType.String, spe.specialty_type);
                Connection.DataBase.AddInParameter(cmd, "@url", DbType.String, spe.url);
                var i = Connection.DataBase.ExecuteNonQuery(cmd);
                result = i > 0;
            }
            return result;
        }
        public Domain.Soft.specialties GetId(Int32 id)
        {
            Domain.Soft.specialties result = new Domain.Soft.specialties();
            string sql = "SELECT id, specialty_type, url FROM specialties WHERE id = @id";
            using (DbCommand cmd = Connection.DataBase.GetSqlStringCommand(sql))
            {
                Connection.DataBase.AddInParameter(cmd, "@id", DbType.Int32, id);
                using (IDataReader dr = Connection.DataBase.ExecuteReader(cmd))
                {
                    if (dr.Read())
                    {
                        result.id = Convert.ToInt32(dr["id"]);
                        result.specialty_type = Convert.ToString(dr["specialty_type"]);
                        result.url = Convert.ToString(dr["url"]);
                    }
                }
            }
            return result;
        }
        public List<Domain.Soft.specialties> Get()
        {
            List <Domain.Soft.specialties> result = new List<Domain.Soft.specialties>();
            string sql = "SELECT id, specialty_type, url FROM specialties";
            using (DbCommand cmd = Connection.DataBase.GetSqlStringCommand(sql))
            {
                using (IDataReader dr = Connection.DataBase.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        var spe = new Domain.Soft.specialties();
                        spe.id = Convert.ToInt32(dr["id"]);
                        spe.specialty_type = Convert.ToString(dr["specialty_type"]);
                        spe.url = Convert.ToString(dr["url"]);
                        result.Add(spe);
                    }
                }
            }
            return result;
        }
    }
}
