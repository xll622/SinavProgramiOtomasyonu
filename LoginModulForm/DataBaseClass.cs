using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginModulForm
{
    internal class DataBaseClass
    {
        private string connectionString;
        public DataBaseClass(string connStr)
        {
            connectionString = connStr;

        }
        //SELECT
        public DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            try
            {
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                conn.Open();
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata" + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Dispose();
                cmd.Dispose();
                da.Dispose();
            }
            return dt;

        }
        //MİN MAKS AVG
        public object ExecuteScaler(string query, SqlParameter[] parameters = null)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(query, conn);
            object sonuc = null;
            try
            {
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                conn.Open();
                sonuc = cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata" + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Dispose();
                cmd.Dispose();
            }
            return sonuc;
        }
        //İNSERT UPDATE DELETE
        public int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(query, conn);
            int rec_number = 0;
            try
            {
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);

                }
                conn.Open();
                rec_number = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata" + ex.Message);

            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Dispose();
                cmd.Dispose();

            }
            return rec_number;
        }
    }
}
