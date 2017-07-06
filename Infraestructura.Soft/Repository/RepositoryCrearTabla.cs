using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Soft.Repository
{
    public class RepositoryCrearTabla
    {
        protected bool ExistTablas(string nametable)
        {
            bool result = false;
            string sql = "SELECT 1 FROM sys.sysobjects WHERE [name]=@tabla AND [type]='U'";
            using (DbCommand cmd = Connection.DataBase.GetSqlStringCommand(sql))
            {
                Connection.DataBase.AddInParameter(cmd, "@tabla", DbType.String, nametable);
                var s = Connection.DataBase.ExecuteScalar(cmd);
                if (s != null && s.ToString() != "")
                {
                    result = Convert.ToInt32(s) > 0;
                }
            }
            return result;
        }
        protected void CreateTable_Doctors()
        {
            string sql = @"CREATE TABLE doctors
                            (
	                            id INT NOT NULL
	                            ,url VARCHAR(200) NULL
	                            ,identification VARCHAR(50) NOT NULL
	                            ,first_name VARCHAR(50) NOT NULL
	                            ,last_name VARCHAR(50)NULL
	                            ,blood_type VARCHAR(10)NULL
	                            ,idspecialties int null
	                            ,CONSTRAINT PK_doctors PRIMARY KEY(id)
	                            ,CONSTRAINT FK_doctorsspecialties FOREIGN KEY(idspecialties) REFERENCES specialties(id) 
                            )";
            using (DbCommand cmd = Connection.DataBase.GetSqlStringCommand(sql))
            {
                var s = Connection.DataBase.ExecuteNonQuery(cmd);
            }
        }
        protected void CreateTable_Specialties()
        {
            string sql = @"CREATE TABLE specialties
                            (
	                            id INT NOT NULL
	                            ,specialty_type VARCHAR(80) NOT NULL
	                            ,url VARCHAR(200) NULL
	                            ,CONSTRAINT PK_specialties PRIMARY KEY(id) 
                            )";
            using (DbCommand cmd = Connection.DataBase.GetSqlStringCommand(sql))
            {
                var s = Connection.DataBase.ExecuteNonQuery(cmd);
            }
        }
        protected void CreateTable_Patients()
        {
            string sql = @"CREATE TABLE patients
                            (
	                            id INT NOT NULL
	                            ,history VARCHAR(50)NULL
	                            ,identification VARCHAR(50)NOT NULL
	                            ,first_name VARCHAR(50)NOT NULL
	                            ,last_name VARCHAR(50)NULL
	                            ,genre VARCHAR(10) NULL
	                            ,civil_status VARCHAR(10)NULL
	                            ,blood_type VARCHAR(10)NULL
	                            ,date_birth DATETIME NULL
	                            ,city_birth VARCHAR(50) NULL
	                            ,url VARCHAR(200) NULL
                                ,CONSTRAINT PK_patients PRIMARY KEY(id)
                            )";
            using (DbCommand cmd = Connection.DataBase.GetSqlStringCommand(sql))
            {
                var s = Connection.DataBase.ExecuteNonQuery(cmd);
            }
        }
        protected void CreateTable_Quotes()
        {
            string sql = @"CREATE TABLE quotes
                            (
	                            id int not null identity(1,1)
	                            ,iddoctor int not null
	                            ,idpaciente int not null
	                            ,titulo varchar(40)not null
	                            ,descripcion varchar(40)not null
	                            ,fechainicio datetime not null
	                            ,fechafin datetime not null
	                            ,txticono varchar(50) null
	                            ,txtcolor varchar(50) null
	                            ,estado int CONSTRAINT df_quotes_estado default(1)
	                            ,CONSTRAINT PK_quotes PRIMARY KEY(id)
	                            ,CONSTRAINT FK_quotes_doctors foreign key(iddoctor) references doctors(id)
	                            ,CONSTRAINT FK_quotes_patients foreign key(idpaciente) references patients(id)
                            )";
            using (DbCommand cmd = Connection.DataBase.GetSqlStringCommand(sql))
            {
                var s = Connection.DataBase.ExecuteNonQuery(cmd);
            }
        }
        private void DropTables()
        {
            string sql = @"BEGIN
	                        IF EXISTS(SELECT 1 FROM sys.sysobjects WHERE [name]='quotes' AND [type]='U')
		                        DROP TABLE quotes;
                            IF EXISTS(SELECT 1 FROM sys.sysobjects WHERE [name]='doctors' AND [type]='U')
		                        DROP TABLE doctors;
                            IF EXISTS(SELECT 1 FROM sys.sysobjects WHERE [name]='specialties' AND [type]='U')
		                        DROP TABLE specialties;
	                        IF EXISTS(SELECT 1 FROM sys.sysobjects WHERE [name]='patients' AND [type]='U')
		                        DROP TABLE patients;
                        END";
            using (DbCommand cmd = Connection.DataBase.GetSqlStringCommand(sql))
            {
                var s = Connection.DataBase.ExecuteNonQuery(cmd);
            }
        }
        public void CreaTablas()
        {
            //DropTables();
            if (!ExistTablas("specialties"))
                CreateTable_Specialties();
            if (!ExistTablas("doctors"))
                CreateTable_Doctors();
            if (!ExistTablas("patients"))
                CreateTable_Patients();
            if (!ExistTablas("quotes"))
                CreateTable_Quotes();
        }
    }
}
