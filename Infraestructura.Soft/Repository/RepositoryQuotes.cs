using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Soft.Repository
{
    public class RepositoryQuotes
    {
        public bool Save(Domain.Soft.quotes pac)
        {
            bool result = false;
            string sql = @"BEGIN
	IF NOT EXISTS(SELECT 1 FROM quotes WHERE id = @id)
		BEGIN
			INSERT INTO quotes
			(
				iddoctor
				,idpaciente
				,titulo
				,descripcion
				,fechainicio
				,fechafin
				,txticono
				,txtcolor
			)
			VALUES
			(
				@iddoctor
				,@idpaciente
				,@titulo
				,@descripcion
				,@fechainicio
				,@fechafin
				,@txticono
				,@txtcolor
			);
		END
	ELSE
		BEGIN
			UPDATE quotes SET
				iddoctor = @iddoctor
				,idpaciente = @idpaciente
				,titulo = @titulo
				,descripcion = @descripcion
				,fechainicio = @fechainicio
				,fechafin = @fechafin
				,txticono = @txticono
				,txtcolor = @txtcolor
				,estado = @estado
			WHERE id = @id;
		END
END";
            using (DbCommand cmd = Connection.DataBase.GetSqlStringCommand(sql))
            {
                Connection.DataBase.AddInParameter(cmd, "@id", DbType.Int32, pac.id);
                Connection.DataBase.AddInParameter(cmd, "@iddoctor", DbType.Int32, pac.iddoctor);
                Connection.DataBase.AddInParameter(cmd, "@idpaciente", DbType.Int32, pac.idpaciente);
                Connection.DataBase.AddInParameter(cmd, "@titulo", DbType.String, pac.titulo);
                Connection.DataBase.AddInParameter(cmd, "@descripcion", DbType.String, pac.descripcion);
                Connection.DataBase.AddInParameter(cmd, "@fechainicio", DbType.DateTime, pac.fechainicio);
                Connection.DataBase.AddInParameter(cmd, "@fechafin", DbType.DateTime, pac.fechafin);
                Connection.DataBase.AddInParameter(cmd, "@txticono", DbType.String, pac.txticono);
                Connection.DataBase.AddInParameter(cmd, "@txtcolor", DbType.String, pac.txtcolor);
                Connection.DataBase.AddInParameter(cmd, "@estado", DbType.Int32, pac.estado);

                var i = Connection.DataBase.ExecuteNonQuery(cmd);
                result = i > 0;
            }
            return result;
        }

        public bool ExisteDoctor(Domain.Soft.quotes pac)
        {
            bool result = false;
            string sql = @"BEGIN
                                SELECT 
	                                1
                                FROM quotes
                                WHERE iddoctor = @iddoctor
                                    AND estado = 1
	                                AND CONVERT(varchar(10),fechainicio,112) = CONVERT(varchar(10),@fechainicio,112)
	                                AND CONVERT(varchar(10),fechafin,108) >= CONVERT(varchar(10),@fechainicio,108)
	                                AND CONVERT(varchar(10),fechafin,108) <= CONVERT(varchar(10),@fechafin,108)
                            END";
            using (DbCommand cmd = Connection.DataBase.GetSqlStringCommand(sql))
            {
                Connection.DataBase.AddInParameter(cmd, "@iddoctor", DbType.Int32, pac.iddoctor);
                Connection.DataBase.AddInParameter(cmd, "@fechainicio", DbType.DateTime, pac.fechainicio);
                Connection.DataBase.AddInParameter(cmd, "@fechafin", DbType.DateTime, pac.fechafin);

                var o = Connection.DataBase.ExecuteScalar(cmd);
                if (o != null && o.ToString() != "")
                {
                    result = Convert.ToInt32(o) > 0;
                }
            }
            return result;
        }
        public Domain.Soft.quotes GetId(Int32 id)
        {
            Domain.Soft.quotes result = new Domain.Soft.quotes();
            string sql = @"SELECT id
	,iddoctor
	,idpaciente
	,titulo
	,descripcion
	,fechainicio
	,fechafin
	,txticono
	,txtcolor
	,estado
	,(
        SELECT first_name + ' ' + last_name

        FROM doctors

        WHERE id = a.iddoctor
	) name_doctor
	,(
        SELECT first_name + ' ' + last_name

        FROM patients

        WHERE id = a.idpaciente
	) name_paciente
FROM quotes a
WHERE id = @id";
            using (DbCommand cmd = Connection.DataBase.GetSqlStringCommand(sql))
            {
                Connection.DataBase.AddInParameter(cmd, "@id", DbType.Int32, id);
                using (IDataReader dr = Connection.DataBase.ExecuteReader(cmd))
                {
                    if (dr.Read())
                    {
                        result.id = Convert.ToInt32(dr["id"]);
                        result.iddoctor = Convert.ToInt32(dr["iddoctor"]);
                        result.idpaciente = Convert.ToInt32(dr["idpaciente"]);
                        result.titulo = Convert.ToString(dr["titulo"]);
                        result.descripcion = Convert.ToString(dr["descripcion"]);
                        result.fechainicio = Convert.ToDateTime(dr["fechainicio"]);
                        result.fechafin = Convert.ToDateTime(dr["fechafin"]);
                        result.txticono = Convert.ToString(dr["txticono"]);
                        result.txtcolor = Convert.ToString(dr["txtcolor"]);
                        result.estado = Convert.ToInt32(dr["estado"]);
                        result.name_doctor = Convert.ToString(dr["name_doctor"]);
                        result.name_paciente = Convert.ToString(dr["name_paciente"]);
                    }
                }
            }
            return result;
        }
        public List<Domain.Soft.quotes> Get()
        {
            List<Domain.Soft.quotes> result = new List<Domain.Soft.quotes>();
            string sql = @"SELECT id
	,iddoctor
	,idpaciente
	,titulo
	,descripcion
	,fechainicio
	,fechafin
	,txticono
	,txtcolor
	,estado
	,(
        SELECT first_name + ' ' + last_name

        FROM doctors

        WHERE id = a.iddoctor
	) name_doctor
	,(
        SELECT first_name + ' ' + last_name

        FROM patients

        WHERE id = a.idpaciente
	) name_paciente
FROM quotes a";
            using (DbCommand cmd = Connection.DataBase.GetSqlStringCommand(sql))
            {
                using (IDataReader dr = Connection.DataBase.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        var spe = new Domain.Soft.quotes();
                        spe.id = Convert.ToInt32(dr["id"]);
                        spe.iddoctor = Convert.ToInt32(dr["iddoctor"]);
                        spe.idpaciente = Convert.ToInt32(dr["idpaciente"]);
                        spe.titulo = Convert.ToString(dr["titulo"]);
                        spe.descripcion = Convert.ToString(dr["descripcion"]);
                        spe.fechainicio = Convert.ToDateTime(dr["fechainicio"]);
                        spe.fechafin = Convert.ToDateTime(dr["fechafin"]);
                        spe.txticono = Convert.ToString(dr["txticono"]);
                        spe.txtcolor = Convert.ToString(dr["txtcolor"]);
                        spe.estado = Convert.ToInt32(dr["estado"]);
                        spe.name_doctor = Convert.ToString(dr["name_doctor"]);
                        spe.name_paciente = Convert.ToString(dr["name_paciente"]);
                        result.Add(spe);
                    }
                }
            }
            return result;
        }

        public List<Domain.Soft.quotes> Get(DateTime? start, DateTime? end)
        {
            List<Domain.Soft.quotes> result = new List<Domain.Soft.quotes>();
            string sql = @"SELECT 
	                    id
	                    ,iddoctor
	                    ,idpaciente
	                    ,titulo
	                    ,descripcion
	                    ,fechainicio
	                    ,fechafin
	                    ,txticono
	                    ,txtcolor
	                    ,estado
	                    ,(
		                    SELECT first_name+' '+ last_name
		                    FROM doctors 
		                    WHERE id = a.iddoctor
	                    ) name_doctor
	                    ,(
		                    SELECT first_name+' '+ last_name
		                    FROM patients 
		                    WHERE id = a.idpaciente
	                    ) name_paciente
                    FROM quotes a
                    WHERE CONVERT(varchar(10),fechainicio,112)>=CONVERT(VARCHAR(12),@fechainicio,112)
	                    AND CONVERT(VARCHAR(10),a.FechaInicio,112)<=CONVERT(VARCHAR(12),@fechafin,112)";
            using (DbCommand cmd = Connection.DataBase.GetSqlStringCommand(sql))
            {
                Connection.DataBase.AddInParameter(cmd, "@fechainicio", DbType.DateTime, start);
                Connection.DataBase.AddInParameter(cmd, "@fechafin", DbType.DateTime, end);
                using (IDataReader dr = Connection.DataBase.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        var spe = new Domain.Soft.quotes();
                        spe.id = Convert.ToInt32(dr["id"]);
                        spe.iddoctor = Convert.ToInt32(dr["iddoctor"]);
                        spe.idpaciente = Convert.ToInt32(dr["idpaciente"]);
                        spe.titulo = Convert.ToString(dr["titulo"]);
                        spe.descripcion = Convert.ToString(dr["descripcion"]);
                        spe.fechainicio = Convert.ToDateTime(dr["fechainicio"]);
                        spe.fechafin = Convert.ToDateTime(dr["fechafin"]);
                        spe.txticono = Convert.ToString(dr["txticono"]);
                        spe.txtcolor = Convert.ToString(dr["txtcolor"]);
                        spe.estado = Convert.ToInt32(dr["estado"]);
                        spe.name_doctor = Convert.ToString(dr["name_doctor"]);
                        spe.name_paciente = Convert.ToString(dr["name_paciente"]);
                        result.Add(spe);
                    }
                }
            }
            return result;
        }
        public bool Delete(Int32 id)
        {
            bool result = false;
            string sql = @"BEGIN
	                        DELETE FROM quotes WHERE id = @id;
                          END";
            using (DbCommand cmd = Connection.DataBase.GetSqlStringCommand(sql))
            {
                Connection.DataBase.AddInParameter(cmd, "@id", DbType.Int32, id);
                var i = Connection.DataBase.ExecuteNonQuery(cmd);
                result = i > 0;
            }
            return result;
        }
    }
}
