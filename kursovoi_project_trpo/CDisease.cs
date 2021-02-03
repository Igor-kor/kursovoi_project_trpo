using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kursovoi_project_trpo
{
    public static class CDisease
    {
        public static string GetNameById(int id)
         {
            if (id < 0) return "";
            string names = "";
            using (OleDbConnection connection = new OleDbConnection(Properties.Settings.Default.DatabaseConnectionString))
            {
                string query = "SELECT NameDisease  FROM Disease WHERE  Код = @id;";

                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    OleDbDataAdapter sqlDataAdap = new OleDbDataAdapter(command);
                    DataTable dtRecord = new DataTable();
                    sqlDataAdap.Fill(dtRecord);
                    foreach (DataRow row in dtRecord.Rows)
                    {
                        names = row[0].ToString();
                    }
                    connection.Close();
                }
            }
            return names;
        }

        public static int getId(string name)
        {
            string names = "";
            using (OleDbConnection connection = new OleDbConnection(Properties.Settings.Default.DatabaseConnectionString))
            {
                string query = "SELECT Код FROM Disease WHERE NameDisease = @id;";

                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", name);
                    connection.Open();
                    OleDbDataAdapter sqlDataAdap = new OleDbDataAdapter(command);
                    DataTable dtRecord = new DataTable();
                    sqlDataAdap.Fill(dtRecord);
                    foreach (DataRow row in dtRecord.Rows)
                    {
                        names = row[0].ToString();
                    }
                    connection.Close();
                }
            }
            return Convert.ToInt32(names);
        }
        public static List<string> GetListName()
        {
            List<string> names = new List<string>();
            using (OleDbConnection connection = new OleDbConnection(Properties.Settings.Default.DatabaseConnectionString))
            {
                string query = "SELECT NameDisease, Код FROM Disease;";

                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    connection.Open();
                    OleDbDataAdapter sqlDataAdap = new OleDbDataAdapter(command);
                    DataTable dtRecord = new DataTable();
                    sqlDataAdap.Fill(dtRecord);
                    foreach (DataRow row in dtRecord.Rows)
                    {
                        if (!names.Contains(row[0].ToString())) names.Add(row[0].ToString());
                    }
                    connection.Close();
                }
            }
            return names;
        }
    }

    public static class CDepartment
    {
        // список всех отделений
        public static List<string> ListNumberOtd()
        {
            List<string> names = new List<string>();
            using (OleDbConnection connection = new OleDbConnection(Properties.Settings.Default.DatabaseConnectionString))
            {
                string query = "SELECT NumberDepartment, Код FROM Department;";

                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    connection.Open();
                    OleDbDataAdapter sqlDataAdap = new OleDbDataAdapter(command);
                    DataTable dtRecord = new DataTable();
                    sqlDataAdap.Fill(dtRecord);
                    foreach (DataRow row in dtRecord.Rows)
                    {
                        if (!names.Contains(row[0].ToString())) names.Add(row[0].ToString());
                    }
                    connection.Close();
                }
            }
            return names;
        }


        // Код отделения оп имени
        public static int getId(string name)
        {
            string names = "";
            using (OleDbConnection connection = new OleDbConnection(Properties.Settings.Default.DatabaseConnectionString))
            {
                string query = "SELECT Код FROM Department WHERE NumberDepartment = @id;";

                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", name);
                    connection.Open();
                    OleDbDataAdapter sqlDataAdap = new OleDbDataAdapter(command);
                    DataTable dtRecord = new DataTable();
                    sqlDataAdap.Fill(dtRecord);
                    foreach (DataRow row in dtRecord.Rows)
                    {
                        names = row[0].ToString();
                    }
                    connection.Close();
                }
            }
            return Convert.ToInt32(names);
        }
    }
}


