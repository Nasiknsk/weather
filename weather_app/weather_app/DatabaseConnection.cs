using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace weather_app
{
    internal class DatabaseConnection
    {
        private string connect = "Host=localhost;Username=postgres;Password=nasik123;Database=weather";
        public  DatabaseConnection() 
        {
            try 
            {
                NpgsqlConnection connection = new NpgsqlConnection(connect);
                connection.Open();
                Console.WriteLine("DataBase Connected");
                connection.Close();
              
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }
        public DataTable LoadResults()
        {
            try
            {
                NpgsqlConnection connection = new NpgsqlConnection(connect);
                connection.Open();
                string query = "SELECT id, location, temp, country FROM public.results;";
                NpgsqlCommand cmd = new NpgsqlCommand(query,connection);
                cmd.ExecuteNonQuery();
                DataTable dt= new DataTable();
                dt.Load(cmd.ExecuteReader());
                connection.Close();
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public string Update(string location, string temp, string country ,string id)
        {
            try
            {
                NpgsqlConnection connection = new NpgsqlConnection(connect);
                connection.Open();
                int num = int.Parse(id);
                double tp=double.Parse(temp);
                string query = "UPDATE public.results SET location = @value1, temp = @value2 , country = @value3 WHERE id = @id";
                NpgsqlCommand cmd = new NpgsqlCommand(query,connection);
                cmd.Parameters.AddWithValue("@value1", location);
                cmd.Parameters.AddWithValue("@value2", tp);
                cmd.Parameters.AddWithValue("@value3", country);
                cmd.Parameters.AddWithValue("@id", num);
                cmd.ExecuteNonQuery();
                connection.Close();

                return "Updated";

            }
            catch (Exception ex)
            {         
                return ex.Message;
            }
        }
        public string Delete(string id)
        {
            int num=int.Parse(id);
            try
            {
                NpgsqlConnection connection = new NpgsqlConnection(connect);
                connection.Open();
                string query = "DELETE FROM public.results WHERE id=@id";
                NpgsqlCommand cmd = new NpgsqlCommand(query,connection);
                cmd.Parameters.AddWithValue("@id", num);
                cmd.ExecuteNonQuery ();
                connection.Close();
                
                return "Deleted Successfully";
                

            }
            catch (Exception ex)
            {
                
               return ex.Message;
            }

        }
        public string Insert(string location , string country, string temp)
        {
            double tp=double.Parse(temp);
            try
            {
                NpgsqlConnection connection = new NpgsqlConnection(connect);
                connection.Open();
                string query = "INSERT INTO public.results( location, temp, country)VALUES (@val1, @val2, @val3)";
                NpgsqlCommand cmd = new NpgsqlCommand(query,connection);
                cmd.Parameters.AddWithValue("@val1", location);
                cmd.Parameters.AddWithValue("@val2", tp);
                cmd.Parameters.AddWithValue("@val3", country);
                cmd.ExecuteNonQuery();
                connection.Close();
                return "Inserted Successfully!";
               

            }
            catch (Exception ex)
            {
               
                return ex.Message;
            }
        }
    }
}
