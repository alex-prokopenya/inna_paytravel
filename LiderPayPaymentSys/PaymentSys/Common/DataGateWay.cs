namespace LiderPayPaymentSys.PaymentSys.Common
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public abstract class DataGateWay
    {
        //private static string connectStr = WebConfigurationManager.AppSettings["connectionString"];

        protected DataGateWay()
        {
        }

        protected DataTable ExecQuery(string QueryString)
        {
            //DataTable table = new DataTable();
            //using (SqlConnection connection = new SqlConnection(connectStr))
            //{
            //    connection.Open();
            //    using (IDbCommand command = new SqlCommand(QueryString, connection))
            //    {
            //        table.Load(command.ExecuteReader());
            //    }
            //}
            //return table;
            return null;
        }

        //protected string ConnectionString
        //{
        //    get
        //    {
        //        return connectStr;
        //    }
        //}
    }
}

