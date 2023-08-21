using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day7ass7
{
    internal class Program
    {

        static SqlConnection con;
        static SqlCommand cmd;
        static SqlDataAdapter adapter;
        static DataSet ds;
        static string connection = "Server=LAPTOP-N8306ABM;Database=LibraryDB;Trusted_Connection=true";
        public static void LoadData()
        {
            con = new SqlConnection(connection);
            cmd = new SqlCommand("Select * From Books", con);
            con.Open();
            adapter = new SqlDataAdapter(cmd);
            ds = new DataSet();
            adapter.Fill(ds, "Books");
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            con.Close();
        }
        public static void DisplayAll()
        {
            Console.WriteLine("BookId\tTitle\t\t\t\t\tAuthor\t\t\tGenre\t\tQuantity");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Console.Write(ds.Tables[0].Rows[i]["BookId"] + "\t");
                Console.Write(ds.Tables[0].Rows[i]["Title"] + "\t\t");
                Console.Write(ds.Tables[0].Rows[i]["Author"] + "\t");
                Console.Write(ds.Tables[0].Rows[i]["Genre"] + "\t");
                Console.Write(ds.Tables[0].Rows[i]["Quantity"]);
                Console.WriteLine();
            }
        }
        public static void AddRow()
        {
            DataRow newRow = ds.Tables["Books"].NewRow();
            Console.WriteLine("Enter New Book Id");
            newRow["BookId"] = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter New Title");
            newRow["Title"] = Console.ReadLine();
            Console.WriteLine("Enter New Author");
            newRow["Author"] = Console.ReadLine();
            Console.WriteLine("Enter New Genre");
            newRow["Genre"] = Console.ReadLine();
            Console.WriteLine("Enter New Quantity");
            newRow["Quantity"] = int.Parse(Console.ReadLine());
            ds.Tables["Books"].Rows.Add(newRow);
            adapter.Update(ds, "Books");
            Console.WriteLine("1 Row is Affected");
        }
        public static void UpdateRow(int id)
        {
            DataTable dt = ds.Tables[0];
            DataRow dr = null;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if ((int)dt.Rows[i]["BookId"] == id)
                {
                    dr = dt.Rows[i];
                    break;
                }
            }
            if (dr != null)
            {
                Console.WriteLine("Enter New Quantity");
                dr["Quantity"] = int.Parse(Console.ReadLine());
                adapter.Update(ds, "Books");
                Console.WriteLine("1 Row is Updated");
            }
            else
            {
                Console.WriteLine("No Such Book Id to Update");
            }
        }
        static void Main(string[] args)
        {
            try
            {
            again:
                LoadData();
                Console.WriteLine("Available Operations : \n1. Display All Data\n2. Add New Book\n3. Update Book Quantity");
                Console.WriteLine("Enter Which Operation You would like to Perform");
                switch (int.Parse(Console.ReadLine()))
                {
                    case 1:
                        {
                            DisplayAll();
                            break;
                        }
                    case 2:
                        {
                            AddRow();
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("Enter Book Id to Update the Quantity");
                            int id = int.Parse(Console.ReadLine());
                            UpdateRow(id);
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Errrr.....Entered Wrong Operation.....\nChoose Right Operation");
                            goto again;
                        }
                }
                Console.WriteLine("\nWould u like to Continue?\nIf 'Yes' Press 1");
                int ch = int.Parse(Console.ReadLine());
                if (ch == 1)
                {
                    Console.Clear();
                    goto again;
                }

                else
                    Console.WriteLine("End Of the Program\n\nTOODALOOO");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
    

