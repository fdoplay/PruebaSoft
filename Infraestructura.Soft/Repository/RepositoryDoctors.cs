using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Soft.Repository
{
    public class RepositoryDoctors
    {
        public bool Save(Domain.Soft.doctors doc)
        {
            bool result = false;
            string sql = @"BEGIN
	IF NOT EXISTS(SELECT 1 FROM doctors WHERE id = @id)
		BEGIN
			INSERT INTO doctors
			(
				id
				,url
				,identification
				,first_name
				,last_name
				,blood_type
				,idspecialties
			)
			VALUES
			(
				@id
				,@url
				,@identification
				,@first_name
				,@last_name
				,@blood_type
				,@idspecialties
			);
		END
	ELSE
		BEGIN
			UPDATE doctors SET
				url = @url
				,identification = @identification
				,first_name = @first_name
				,last_name = @last_name
				,blood_type = @blood_type
				,idspecialties = @idspecialties
			WHERE id = @id;
		END
END";
            using (DbCommand cmd = Connection.DataBase.GetSqlStringCommand(sql))
            {
                Connection.DataBase.AddInParameter(cmd, "@id", DbType.Int32, doc.id);
                Connection.DataBase.AddInParameter(cmd, "@url", DbType.String, doc.url);
                Connection.DataBase.AddInParameter(cmd, "@identification", DbType.String, doc.identification);
                Connection.DataBase.AddInParameter(cmd, "@first_name", DbType.String, doc.first_name);
                Connection.DataBase.AddInParameter(cmd, "@last_name", DbType.String, doc.last_name);
                Connection.DataBase.AddInParameter(cmd, "@blood_type", DbType.String, doc.blood_type);
                Connection.DataBase.AddInParameter(cmd, "@idspecialties", DbType.Int32, doc.idspecialties);


                var i = Connection.DataBase.ExecuteNonQuery(cmd);
                result = i > 0;
            }
            return result;
        }
        public Domain.Soft.doctors GetId(Int32 id)
        {
            Domain.Soft.doctors result = new Domain.Soft.doctors();
            string sql = @"SELECT a.id, a.url, a.identification, a.first_name, a.last_name, a.blood_type, a.idspecialties 
                ,(SELECT id, specialty_type, url FROM specialties WHERE id = a.idspecialties FOR XML PATH('specialties')) specialties
                FROM doctors a WHERE a.id = @id";
            using (DbCommand cmd = Connection.DataBase.GetSqlStringCommand(sql))
            {
                Connection.DataBase.AddInParameter(cmd, "@id", DbType.Int32, id);
                using (IDataReader dr = Connection.DataBase.ExecuteReader(cmd))
                {
                    if (dr.Read())
                    {
                        result.id = Convert.ToInt32(dr["id"]);
                        result.url = Convert.ToString(dr["url"]);
                        result.identification = Convert.ToString(dr["identification"]);
                        result.first_name = Convert.ToString(dr["first_name"]);
                        result.last_name = Convert.ToString(dr["last_name"]);
                        result.blood_type = Convert.ToString(dr["blood_type"]);
                        if(dr["idspecialties"].ToString()!="") result.idspecialties = Convert.ToInt32(dr["idspecialties"]);
                        result.specialty_field = dr["specialties"].ToString().DeserializeXML<Domain.Soft.specialties>();
                    }
                }
            }
            return result;
        }
        public List<Domain.Soft.doctors> Get()
        {
            List<Domain.Soft.doctors> result = new List<Domain.Soft.doctors>();
            string sql = @"SELECT a.id, a.url, a.identification, a.first_name, a.last_name, a.blood_type, a.idspecialties 
                    ,(SELECT id, specialty_type, url FROM specialties WHERE id = a.idspecialties FOR XML PATH('specialties')) specialties
                FROM doctors a ";
            using (DbCommand cmd = Connection.DataBase.GetSqlStringCommand(sql))
            {
                using (IDataReader dr = Connection.DataBase.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        var spe = new Domain.Soft.doctors();
                        spe.id = Convert.ToInt32(dr["id"]);
                        spe.url = Convert.ToString(dr["url"]);
                        spe.identification = Convert.ToString(dr["identification"]);
                        spe.first_name = Convert.ToString(dr["first_name"]);
                        spe.last_name = Convert.ToString(dr["last_name"]);
                        spe.blood_type = Convert.ToString(dr["blood_type"]);
                        if (dr["idspecialties"].ToString() != "") spe.idspecialties = Convert.ToInt32(dr["idspecialties"]);
                        spe.specialty_field = dr["specialties"].ToString().DeserializeXML<Domain.Soft.specialties>();
                        result.Add(spe);
                    }
                }
            }
            return result;
        }
    }
}
