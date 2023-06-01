using System;
using System.Data.SqlClient;
using System.Data.Common;
using System.Configuration;

namespace DZ_1_Savchuk
{
    class Program
    {
        static void Main()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                //conn.ConnectionString = @"Data Source=DESKTOP-HI6BINM\SQLEXPRESS; Initial Catalog = books; Integrated Security = true;";
                string cs = ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString;
                conn.ConnectionString = cs;
                Table_records(conn);
                Console.WriteLine();
                Price_sum(conn);
                Console.WriteLine();
                Pages_sum(conn);
                Console.ReadLine();
            }
        }
        static void Table_records(SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM books");
            cmd.Connection = conn;

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.Write(reader.GetName(i) + "\t");
                }
                Console.WriteLine();

                while(reader.Read()) 
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write(reader[i] + "\t");
                    }
                    Console.WriteLine();
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exeption: " + ex.Message);
            }
        }
        static void Price_sum(SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand("SELECT SUM(Price) FROM books");
            cmd.Connection = conn;
            
            try
            {
                conn.Open();
                decimal result = (decimal)cmd.ExecuteScalar();
                Console.WriteLine($"Price of books: {result}");
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exeption: " + ex.Message);
            }
        }
        static void Pages_sum(SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand("SELECT SUM(Pages) FROM books");
            cmd.Connection = conn;


            try
            {
                conn.Open();
                int result = Convert.ToInt32(cmd.ExecuteScalar());
                Console.WriteLine($"Total number of pages: {result}");
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exeption: " + ex.Message);
            }
        }
    }
}
