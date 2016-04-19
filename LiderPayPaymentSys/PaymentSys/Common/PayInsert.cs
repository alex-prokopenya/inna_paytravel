namespace LiderPayPaymentSys.PaymentSys.Common
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Data.SqlClient;
    using System.Globalization;

    internal class PayInsert : DataGateWay
    {
        private PaymentSys currSys;
        private string status = string.Empty;

        internal PayInsert(PaymentSys System)
        {
            this.currSys = System;
        }

        //public virtual bool ExistsTxn(string TxnID)
        //{
        //    bool flag;
        //    using (SqlConnection connection = new SqlConnection(base.ConnectionString))
        //    {
        //        connection.Open();
        //        using (SqlCommand command = connection.CreateCommand())
        //        {
        //            command.CommandText = string.Format("SELECT TOP 1 1 FROM LP_FIN_DOGOVOR_PAID WHERE DP_TXN_ID = '{0}' AND DP_PAYMENTSYS = '{1}'", TxnID, this.currSys);
        //            flag = command.ExecuteScalar() != null;
        //        }
        //    }
        //    return flag;
        //}

        //public DataRow GetRow(string TxnID)
        //{
        //    IEnumerator enumerator = base.ExecQuery(string.Format("SELECT TOP 1 DP_KEY,DP_TXN_ID,DP_TXN_DATE,DP_DGCOD,DP_SUM,DP_PAYTYPE,DG_FILIALKEY\r\n            FROM LP_FIN_DOGOVOR_PAID JOIN DOGOVOR ON DP_DGCOD = DG_CODE COLLATE Cyrillic_General_CI_AS WHERE DP_TXN_ID ='{0}'", TxnID)).Rows.GetEnumerator();
        //    while (enumerator.MoveNext())
        //    {
        //        return (DataRow) enumerator.Current;
        //    }
        //    return null;
        //}

        //public string Insert(string TxnID, string TxnDate, string Dogovor, string Sum, string PayType, string TxnResult, string User)
        //{
        //    Convert.ToString(TxnDate).Remove(8).Insert(4, "-").Insert(7, "-");
        //    Convert.ToDouble(Sum.ToString(), CultureInfo.GetCultureInfo("en-US"));
        //    TxnDate = string.Format("CONVERT(DATETIME, STUFF(STUFF(STUFF('{0}', 9, 0, ' '), 12, 0, ':'), 15, 0, ':'), 112)", TxnDate);
        //    string cmdText = string.Format("IF NOT EXISTS (SELECT 1 FROM LP_FIN_DOGOVOR_PAID WHERE DP_TXN_ID = '{0}' AND DP_PAYMENTSYS = '{6}')\r\n              INSERT INTO LP_FIN_DOGOVOR_PAID \r\n                    (DP_TXN_ID, DP_TXN_DATE, DP_DGCOD, DP_SUM, DP_PAYTYPE, DP_RESULT, DP_PAYMENTSYS,DP_USER)\r\n              VALUES('{0}', {1}, '{2}', {3}, {4}, {5}, '{6}','{7}')", new object[] { TxnID, TxnDate, Dogovor, Sum, PayType, TxnResult, this.currSys.ToString(), User });
        //    using (SqlConnection connection = new SqlConnection(base.ConnectionString))
        //    {
        //        connection.Open();
        //        try
        //        {
        //            using (SqlCommand command = new SqlCommand(cmdText, connection))
        //            {
        //                command.ExecuteNonQuery();
        //                command.Dispose();
        //            }
        //            connection.Close();
        //            this.status = string.Empty;
        //        }
        //        catch (Exception exception)
        //        {
        //            string status = this.status;
        //            this.status = status + " : Error: " + exception.Message + "<br />" + exception.StackTrace;
        //        }
        //    }
        //    return this.status;
        //}
    }
}

