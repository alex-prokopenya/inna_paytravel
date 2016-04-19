namespace LiderPayPaymentSys.PaymentSys.Common
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Web.Configuration;

    public sealed class Check : DataGateWay
    {
        public DataRow GetRow(string DG_CODE)
        {
            return null;
            //using (SqlConnection connection = new SqlConnection(base.ConnectionString))
            //{
            //    connection.Open();
            //    using (SqlCommand command = new SqlCommand("LP_FIN_OSMP_DOGOVOR_CHECK", connection))
            //    {
            //        command.CommandType = CommandType.StoredProcedure;
            //        SqlParameter parameter = new SqlParameter {
            //            ParameterName = "@DG_CODE",
            //            SqlDbType = SqlDbType.VarChar,
            //            SqlValue = DG_CODE,
            //            Direction = ParameterDirection.Input
            //        };
            //        command.Parameters.Add(parameter);
            //        if (WebConfigurationManager.AppSettings["UsePercentForConvertRates"] != null)
            //        {
            //            SqlParameter parameter2 = new SqlParameter {
            //                ParameterName = "@UsePercentForConvertRates",
            //                SqlDbType = SqlDbType.Bit,
            //                SqlValue = WebConfigurationManager.AppSettings["UsePercentForConvertRates"],
            //                Direction = ParameterDirection.Input
            //            };
            //            command.Parameters.Add(parameter2);
            //        }
            //        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
            //        {
            //            DataTable table = new DataTable();
            //            table.Load(reader);
            //            foreach (DataRow row in table.Rows)
            //            {
            //                reader.Close();
            //                if (connection.State == ConnectionState.Open)
            //                {
            //                    connection.Close();
            //                }
            //                return row;
            //            }
            //        }
            //    }
            //}
            //return null;
        }
    }
}

