using System.Data;
using System.Data.SqlClient;

namespace FedEx2
{
    public static class DbManager
    {
        // TO DO
        // Move to App.config
        //169.254.213.141
        //192.168.1.188 - local
        const string ConnectionString = "Server=DESKTOP-VCD50BA;Database=AdventureWorks2012;Trusted_Connection=True;";

        public static DataTable CalculateData(string fName, string lName, string eMail, string phone)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM ufn_PersonSalesTotal(@name,@lastname,@email,@phone)", conn);
                cmd.Parameters.AddWithValue("@name", fName);
                cmd.Parameters.AddWithValue("@lastname", lName);
                cmd.Parameters.AddWithValue("@email", eMail);
                cmd.Parameters.AddWithValue("@phone", phone);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            return dt;
        }
    }
}