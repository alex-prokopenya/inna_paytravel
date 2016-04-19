namespace LiderPayPaymentSys.PaymentSys.Common
{
    using System;
    using System.Xml;

    public abstract class Respond
    {
        protected Respond()
        {
        }

        protected string correctSum(string strSum)
        {
            if ((strSum == "0") || (strSum.IndexOf("-") > -1))
            {
                strSum = "0.00";
            }
            strSum = strSum.Replace(",", ".");
            int index = strSum.IndexOf(".");
            if (index < 0)
            {
                return (strSum + ".00");
            }
            if ((strSum.Length - index) > 3)
            {
                strSum = strSum.Remove(index + 3);
            }
            return strSum;
        }

        public virtual XmlDocument GetOutXml()
        {
            return null;
        }

        public static XmlDocument RespondDefault(string Comments)
        {
            if (Comments.Length < 1)
            {
                Comments = "Incomplete request";
            }
            XmlDocument document = new XmlDocument();
            document.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?><response></response>");
            XmlNode documentElement = document.DocumentElement;
            XmlElement newChild = document.CreateElement("osmp_txn_id");
            documentElement.AppendChild(newChild);
            XmlElement element2 = document.CreateElement("result");
            element2.InnerText = "300";
            documentElement.AppendChild(element2);
            XmlElement element3 = document.CreateElement("comment");
            element3.InnerText = Comments;
            documentElement.AppendChild(element3);
            return document;
        }
    }
}

