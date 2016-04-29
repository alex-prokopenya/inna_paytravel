namespace LiderPayPaymentSys.PaymentSys.Common
{
    using System.Configuration;
    internal class CheckUserPass : DataGateWay
    {
        public bool CheckAccess(string lpuser, string lppassword)
        {
          //  return true;
            string check_login = ConfigurationManager.AppSettings["UserLogin"];
            string check_password = ConfigurationManager.AppSettings["UserPassword"];

            return ((lpuser == check_login) && (lppassword == check_password));
        }
    }
}

