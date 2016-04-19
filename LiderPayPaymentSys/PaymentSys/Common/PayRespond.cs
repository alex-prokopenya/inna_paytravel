namespace LiderPayPaymentSys.PaymentSys.Common
{
    using System;
    using System.Data;
    using System.Xml;
   // using Leader.MasterRules;
   // using Leader.Operations;
    using Megatec.MasterTour.BusinessRules;
    using Megatec.MasterTour.DataAccess;
    using InnaTourWebService;
    using InnaTourWebService.DataBase;
    using System.Configuration;
    using Helpers;

    public abstract class PayRespond : Respond
    {
        private string Comment = string.Empty;
        private string strDogovor;
        private int Filial;
        protected PaymentSys paymentSys;
        private string PayType;
        private string Sum;
        private string TxnDate;
        private string TxnID;
        private int TxnInternalKey = -1;
        private string TxnResult;

        public PayRespond(PaymentSys currSys, string TxnID, string strDogovor, string Sum, string TxnDate, string PayType, string User, string Password)
        {
            this.paymentSys = currSys;
            this.TxnID = TxnID;
            this.strDogovor = strDogovor;
            this.Sum = Sum;
            this.TxnDate = TxnDate;
            this.PayType = PayType;
            this.TxnResult = "0";

            var mfHelper = new MasterFinance();
            var mtHelper = new MasterTour();
            var prefix = ConfigurationManager.AppSettings["PaymentIdPrefix"];

            if (!new CheckUserPass().CheckAccess(User, Password))
            {
                this.TxnResult = "11";
            }
            else
            {
                // PayInsert infrastructure = this.GetInfrastructure();
                if (mfHelper.CheckIfNewPaymentId(prefix+TxnID) == String.Empty)
                {
                    if (strDogovor.Length > 10)
                    strDogovor = strDogovor.Substring(0, 10);

                    Dogovor dogovor = mtHelper.GetDogovorByCode(strDogovor);

                    try {
                        mfHelper.PayDogovor(strDogovor,
                                            0,
                                            ConfigurationManager.AppSettings["PaymentSystem"],
                                            Convert.ToDecimal(Sum.Replace(".", ",")),
                                            prefix + TxnID);

                    }
                    catch(Exception ex)
                    {
                        Logger.WriteToLog("Pay: ошибка при проведении оплаты " + strDogovor + " " + TxnID + "\n" + ex.Message + "\n" + ex.StackTrace);

                        this.Comment = "fail";
                        this.TxnResult = "300";
                        return;
                    }
                }

                try
                {
                    Dogovor dogovor = mtHelper.GetDogovorByCode(strDogovor);
                    string inId = mfHelper.CheckIfNewPaymentId(prefix+TxnID);

                    this.TxnInternalKey = (inId == "") ? 0 : Convert.ToInt32(inId);
                    // DataRow row = infrastructure.GetRow(TxnID);
                    // this.TxnInternalKey = (int) row["DP_KEY"];
                    // TxnDate = row["DP_TXN_DATE"].ToString();
                    // Dogovor = row["DP_DGCOD"].ToString();
                    // Sum = row["DP_SUM"].ToString();
                    // PayType = row["DP_PAYTYPE"].ToString();
                    this.Filial = dogovor.FilialKey;
                    
                }
                catch(Exception ex)
                {

                    Logger.WriteToLog("Pay: ошибка при проведении оплаты " + strDogovor + " " + TxnID + "\n" + ex.Message + "\n" + ex.StackTrace);

                    this.Comment = this.Comment + string.Format("Error: Record can't be read for 'DP_TXN_ID = {0}'", TxnID);
                    this.TxnResult = "300";
                }
            }
        }

        internal virtual PayInsert GetInfrastructure()
        {
            return new PayInsert(this.paymentSys);
        }

        public override XmlDocument GetOutXml()
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?><response></response>");
            XmlNode documentElement = document.DocumentElement;
            XmlElement newChild = document.CreateElement("osmp_txn_id");
            newChild.InnerText = this.TxnID;
            documentElement.AppendChild(newChild);
            XmlElement element2 = document.CreateElement("prv_txn");
            element2.InnerText = this.TxnInternalKey.ToString();
            documentElement.AppendChild(element2);
            XmlElement element3 = document.CreateElement("sum");
            element3.InnerText = base.correctSum(this.Sum);
            documentElement.AppendChild(element3);
            XmlElement element4 = document.CreateElement("result");
            element4.InnerText = this.TxnResult;
            documentElement.AppendChild(element4);
            XmlElement element5 = document.CreateElement("filial");
            element5.InnerText = this.Filial.ToString();
            documentElement.AppendChild(element5);
            if (this.Comment.Length > 0)
            {
                XmlElement element6 = document.CreateElement("comment");
                element6.InnerText = this.Comment;
                documentElement.AppendChild(element6);
            }
            return document;
        }
    }
}

