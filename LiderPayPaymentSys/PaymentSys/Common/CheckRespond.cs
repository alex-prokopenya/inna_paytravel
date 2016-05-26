namespace LiderPayPaymentSys.PaymentSys.Common
{
    using System;
    using System.Data;
    using System.Xml;
    using Megatec.MasterTour.BusinessRules;
    using Megatec.MasterTour.DataAccess;
    using Helpers;
    using InnaTourWebService.DataBase;

    public abstract class CheckRespond : Respond
    {
        protected object _feild1;
        protected object _feild2;
        protected object _feild3;
        protected object _feild4;
        protected object _feild5;
        protected object _feild6;
        protected object _feild7;
        protected object _feild8;
        protected string _result;
        protected string Account;
        protected int Filial;
        protected string Sum;
        protected string TxnID;

        public CheckRespond()
        {
            this.Filial = 0;
        }

        public CheckRespond(string TxnID, string Account, string Sum, string User, string Password)
        {
            this.Filial = 0;
            this.TxnID = TxnID;
            this.Account = Account;
            this.Sum = Sum;
            this._result = "0";
            if (!new CheckUserPass().CheckAccess(User, Password))
            {
                this._result = "11";
            }
            else
            {
                var masterHelper = new MasterTour();
                Dogovor dogovor = masterHelper.GetDogovorByCode(Account);

                DataRow row = new LiderPayPaymentSys.PaymentSys.Common.Check().GetRow(Account);

                try
                {
                    dogovor.Turists.Fill();

                    this._feild1 = "";
                    this._feild2 = "lider";
                    this._feild3 = dogovor.Turists[0].NameLat + ' ' + dogovor.Turists[0].SNameLat;

                    int toPaySumm = (int)Math.Round(dogovor.Price - dogovor.Payed);

                    Rates rates = new Rates(new Megatec.Common.BusinessRules.Base.DataContainer());
                    rates.Fill();

                    Logger.WriteToLog("Check: заказ проверен " + Account + " к оплате " + toPaySumm +" "+ dogovor.RateCode);

                    if (dogovor.RateCode != rates.NationalRate.Code)
                        toPaySumm = (int)Math.Round(RealCourse.Exchange(toPaySumm, dogovor.RateCode, rates.NationalRate.Code, DateTime.Today));


                    Logger.WriteToLog("Check: заказ проверен " + Account + " к оплате " + toPaySumm + " руб");

                    this._feild4 = toPaySumm;
                    this._feild5 = dogovor.TurList.Name;
                    this._feild6 = dogovor.Country.Name;
                    this._feild7 = dogovor.TurDate.ToString("yyyy.MM.dd");
                    this._feild8 = dogovor.DateEnd.ToString("yyyy.MM.dd");

                    this.Filial = dogovor.FilialKey; 
                }
                catch(Exception ex)
                {
                    Logger.WriteToLog("Check: ошибка при проверке " + Account + "\n" + ex.Message + "\n" + ex.StackTrace);
                    this._result = "5";
                }
            }
        }

        public override XmlDocument GetOutXml()
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?><response></response>");
            XmlNode documentElement = document.DocumentElement;
            XmlElement newChild = document.CreateElement("osmp_txn_id");
            newChild.InnerText = this.TxnID;
            documentElement.AppendChild(newChild);
            XmlElement element2 = document.CreateElement("sum");
            element2.InnerText = base.correctSum(this.Sum);
            documentElement.AppendChild(element2);
            XmlElement element3 = document.CreateElement("result");
            element3.InnerText = this._result;
            documentElement.AppendChild(element3);
            XmlElement element4 = document.CreateElement("comment");
            documentElement.AppendChild(element4);
            XmlElement element5 = document.CreateElement("filial");
            element5.InnerText = this.Filial.ToString();
            documentElement.AppendChild(element5);
            XmlDocument document2 = new XmlDocument();
            document2.LoadXml("<fields></fields>");
            XmlNode node = document2.DocumentElement;
            XmlElement element6 = document2.CreateElement("field1");
            if (this._feild1 != null)
            {
                element6.InnerText = this._feild1.ToString();
            }
            node.AppendChild(element6);
            XmlElement element7 = document2.CreateElement("field2");
            if (this._feild2 != null)
            {
                element7.InnerText = this._feild2.ToString();
            }
            node.AppendChild(element7);
            XmlElement element8 = document2.CreateElement("field3");
            if (this._feild3 != null)
            {
                element8.InnerText = this._feild3.ToString();
            }
            node.AppendChild(element8);
            XmlElement element9 = document2.CreateElement("field4");
            if (this._feild4 != null)
            {
                element9.InnerText = base.correctSum(this._feild4.ToString());
            }
            node.AppendChild(element9);
            XmlElement element10 = document2.CreateElement("field5");
            if (this._feild5 != null)
            {
                element10.InnerText = this._feild5.ToString();
            }
            node.AppendChild(element10);
            XmlElement element11 = document2.CreateElement("field6");
            if (this._feild6 != null)
            {
                element11.InnerText = this._feild6.ToString();
            }
            node.AppendChild(element11);
            XmlElement element12 = document2.CreateElement("field7");
            if (this._feild7 != null)
            {
                element12.InnerText = this._feild7.ToString();
            }
            node.AppendChild(element12);
            XmlElement element13 = document2.CreateElement("field8");
            if (this._feild8 != null)
            {
                element13.InnerText = this._feild8.ToString();
            }
            node.AppendChild(element13);
            documentElement.AppendChild(document.ImportNode(node, true));
            return document;
        }
    }
}

