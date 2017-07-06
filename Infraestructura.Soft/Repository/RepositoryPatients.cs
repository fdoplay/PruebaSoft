using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Soft.Repository
{
    public class RepositoryPatients
    {
        public bool Save(Domain.Soft.patients pac)
        {
            bool result = false;
            string sql = @"BEGIN
	IF NOT EXISTS(SELECT 1 FROM patients WHERE id = @id)
		BEGIN
			INSERT INTO patients
			(
				id
				,history
				,identification
				,first_name
				,last_name
				,genre
				,civil_status
				,blood_type
				,date_birth
				,city_birth
				,url
			)
			VALUES
			(
				@id
				,@history
				,@identification
				,@first_name
				,@last_name
				,@genre
				,@civil_status
				,@blood_type
				,@date_birth
				,@city_birth
				,@url
			);
		END
	ELSE
		BEGIN
			UPDATE patients SET
				history = @history
				,identification = @identification
				,first_name = @first_name
				,last_name = @last_name
				,genre = @genre
				,civil_status = @civil_status
				,blood_type = @blood_type
				,date_birth = @date_birth
				,city_birth = @city_birth
				,url = @url
			WHERE id = @id;
		END
END";
            using (DbCommand cmd = Connection.DataBase.GetSqlStringCommand(sql))
            {
                Connection.DataBase.AddInParameter(cmd, "@id", DbType.Int32, pac.id);
                Connection.DataBase.AddInParameter(cmd, "@history", DbType.String, pac.history);
                Connection.DataBase.AddInParameter(cmd, "@identification", DbType.String, pac.identification);
                Connection.DataBase.AddInParameter(cmd, "@first_name", DbType.String, pac.first_name);
                Connection.DataBase.AddInParameter(cmd, "@last_name", DbType.String, pac.last_name);
                Connection.DataBase.AddInParameter(cmd, "@genre", DbType.String, pac.genre);
                Connection.DataBase.AddInParameter(cmd, "@civil_status", DbType.String, pac.civil_status);
                Connection.DataBase.AddInParameter(cmd, "@blood_type", DbType.String, pac.blood_type);
                Connection.DataBase.AddInParameter(cmd, "@date_birth", DbType.DateTime, pac.date_birth);
                Connection.DataBase.AddInParameter(cmd, "@city_birth", DbType.String, pac.city_birth);
                Connection.DataBase.AddInParameter(cmd, "@url", DbType.String, pac.url);

                var i = Connection.DataBase.ExecuteNonQuery(cmd);
                result = i > 0;
            }
            return result;
        }
        public Domain.Soft.patients GetId(Int32 id)
        {
            Domain.Soft.patients result = new Domain.Soft.patients();
            string sql = "SELECT id, history, identification, first_name, last_name, genre, civil_status, blood_type, date_birth, city_birth, url FROM patients WHERE id = @id";
            using (DbCommand cmd = Connection.DataBase.GetSqlStringCommand(sql))
            {
                Connection.DataBase.AddInParameter(cmd, "@id", DbType.Int32, id);
                using (IDataReader dr = Connection.DataBase.ExecuteReader(cmd))
                {
                    if (dr.Read())
                    {
                        result.id = Convert.ToInt32(dr["id"]);
                        result.history = Convert.ToString(dr["history"]);
                        result.identification = Convert.ToString(dr["identification"]);
                        result.first_name = Convert.ToString(dr["first_name"]);
                        result.last_name = Convert.ToString(dr["last_name"]);
                        result.genre = Convert.ToString(dr["genre"]);
                        result.civil_status = Convert.ToString(dr["civil_status"]);
                        result.blood_type = Convert.ToString(dr["blood_type"]);
                        if(dr["date_birth"].ToString()!="") result.date_birth = Convert.ToDateTime(dr["date_birth"]);
                        result.city_birth = Convert.ToString(dr["city_birth"]);
                        result.url = Convert.ToString(dr["url"]);
                    }
                }
            }
            return result;
        }
        public List<Domain.Soft.patients> Get()
        {
            List<Domain.Soft.patients> result = new List<Domain.Soft.patients>();
            string sql = "SELECT id, history, identification, first_name, last_name, genre, civil_status, blood_type, date_birth, city_birth, url FROM patients";
            using (DbCommand cmd = Connection.DataBase.GetSqlStringCommand(sql))
            {
                using (IDataReader dr = Connection.DataBase.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        var spe = new Domain.Soft.patients();
                        spe.id = Convert.ToInt32(dr["id"]);
                        spe.history = Convert.ToString(dr["history"]);
                        spe.identification = Convert.ToString(dr["identification"]);
                        spe.first_name = Convert.ToString(dr["first_name"]);
                        spe.last_name = Convert.ToString(dr["last_name"]);
                        spe.genre = Convert.ToString(dr["genre"]);
                        spe.civil_status = Convert.ToString(dr["civil_status"]);
                        spe.blood_type = Convert.ToString(dr["blood_type"]);
                        if (dr["date_birth"].ToString() != "") spe.date_birth = Convert.ToDateTime(dr["date_birth"]);
                        spe.city_birth = Convert.ToString(dr["city_birth"]);
                        spe.url = Convert.ToString(dr["url"]);
                        result.Add(spe);
                    }
                }
            }
            return result;
        }
    }
}
