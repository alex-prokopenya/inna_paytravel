namespace LiderPayPaymentSys.PaymentSys
{
    using LiderPayPaymentSys.PaymentSys.Common;
    using System;

    internal class LiderPay_Pay : PayRespond
    {
        public LiderPay_Pay(string TxnID, string Dogovor, string Sum, string TxnDate, string PayType, string User, string Password) : base(PaymentSys.LiderPay, TxnID, Dogovor, Sum, TxnDate, PayType, User, Password)
        {
        }
    }
}

