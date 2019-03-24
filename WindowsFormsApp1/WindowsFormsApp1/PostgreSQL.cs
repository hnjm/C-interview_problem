using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace WindowsFormsApp1
{
    public class PostgreSQL
    {
        List<string> dataItems = new List<string>();
        public PostgreSQL()
        {

        }
        public List<string> PostgreSQLtest()
        {
            try
            {
                string connStr = "Server=127.0.0.1; Port=5432; User Id=postgres; Password=0000; Database=postgres;";
                NpgsqlConnection npgsqlconnection = new NpgsqlConnection(connStr);
                npgsqlconnection.Open();
                NpgsqlCommand npgsqlCommand = new NpgsqlCommand("SELECT * FROM public.\"Student\"", npgsqlconnection);
                NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader();
                while (npgsqlDataReader.Read()){
                    dataItems.Add(npgsqlDataReader[0].ToString() + "," + npgsqlDataReader[1].ToString() + ","
                        + npgsqlDataReader[2].ToString() + "," + npgsqlDataReader[3].ToString() + "\r\n");
                }
                Console.WriteLine("Done");
                npgsqlconnection.Close();
                return dataItems;
            }
            catch(Exception msg)
            {
                Console.WriteLine(msg);
                throw;
            }
        }
    }
}
