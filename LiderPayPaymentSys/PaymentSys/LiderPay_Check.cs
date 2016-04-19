namespace LiderPayPaymentSys.PaymentSys
{
    using LiderPayPaymentSys.PaymentSys.Common;
    using System;

    public class LiderPay_Check : CheckRespond
    {
        public LiderPay_Check(string TxnID, string Account, string Sum, string User, string Password) : base(TxnID, Account, Sum, User, Password)
        {
        }
    }
}

