using System.Data;
using System.Data.SqlClient;

namespace MathMindCare.DAL
{
    public class DataFactory
    {
        public SqlConnection SQLCon = null;

        public SqlConnection GetDBConnection()
        {
            try
            {
                //SQLCon = new SqlConnection("Data Source=103.21.58.193;Initial Catalog=ksale;User ID=ksale; Password=Jss1234$$");
                SQLCon = new SqlConnection("data source=LAPTOP-AICCRGK7\\SQLEXPRESS;Initial Catalog=MATHMINDCARE;Integrated Security=True");

            }
            catch (Exception ex)
            {

                throw ex;

            }
            return SQLCon;
        }


    }
}
