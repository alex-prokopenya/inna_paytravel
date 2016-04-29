using LiderPayPaymentSys.PaymentSys;
using LiderPayPaymentSys.PaymentSys.Common;
using System;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Xml;
using Megatec.MasterTour.DataAccess;
using Megatec.MasterTour.BusinessRules;


public class _Default : Page, IRequiresSessionState
{
    private string Account;
    protected HtmlForm form1;
    private string lpPass;
    private string lpUser;
    private string PayType;
    private string Sum;
    private string TxnDate;
    private string TxnID;

    private void GetRev()
    {
        base.Response.Write("LiderPay v1  - Best Regards 20.07.2012<br/>");
    }



    protected void Page_Load(object sender, EventArgs e)
    {
      //  Config.ReadFile(AppDomain.CurrentDomain.BaseDirectory + Const.ConfigFile);
       // Manager.ConnectionString = Config.ConnectionString;


        base.Response.Cache.SetNoServerCaching();
        if (base.Request.QueryString.Get("command") == null)
        {
            this.RespondDefault("Argument 'Command' is null");
        }
        switch (base.Request.QueryString.Get("command").ToLower())
        {
            case "check":
                this.SetCheckParams();
                this.RespondXml(new LiderPay_Check(this.TxnID, this.Account, this.Sum, this.lpUser, this.lpPass).GetOutXml());
                return;

            case "pay":
                this.SetPayParam();
                this.RespondXml(new LiderPay_Pay(this.TxnID, this.Account, this.Sum, this.TxnDate, this.PayType, this.lpUser, this.lpPass).GetOutXml());
                return;

            case "rev":
                this.GetRev();
                return;
        }
        this.RespondDefault("Unknown command");
    }

    private void RespondDefault(string Comment)
    {
        this.RespondXml(Respond.RespondDefault(Comment));
    }

    private void RespondXml(XmlDocument doc)
    {
        base.ContentType = "text/xml";
        base.Response.Clear();
        base.Response.Write(doc.OuterXml);
        base.Response.End();
    }

    private void SetCheckParams()
    {
        this.TxnID = base.Request.QueryString.Get("txn_id");
        this.Account = base.Request.QueryString.Get("account");
        this.Sum = base.Request.QueryString.Get("sum");

	    this.Sum = this.Sum.Replace(".",",");

        this.lpUser =  base.Request.QueryString.Get("user");
        this.lpPass =  base.Request.QueryString.Get("password");
        if (string.IsNullOrEmpty(this.TxnID))
        {
            this.RespondDefault("Argument 'txn_id' is null");
        }
        else if (string.IsNullOrEmpty(this.Account))
        {
            this.RespondDefault("Argument 'account' is null");
        }
        else if (string.IsNullOrEmpty(this.Sum))
        {
            this.RespondDefault("Argument 'sum' is null");
        }
        
        else if (string.IsNullOrEmpty(this.lpUser))
        {
            this.RespondDefault("Argument 'user' is null");
        }
        else if (string.IsNullOrEmpty(this.lpPass))
        {
            this.RespondDefault("Argument 'password' is null");
        }
        
    }

    private void SetPayParam()
    {
        this.SetCheckParams();
        this.TxnDate = base.Request.QueryString.Get("txn_date");
        this.PayType = base.Request.QueryString.Get("pay_type");
        this.lpUser =  base.Request.QueryString.Get("user");
        this.lpPass =  base.Request.QueryString.Get("password");
        if (string.IsNullOrEmpty(this.TxnDate))
        {
            this.RespondDefault("Argument 'txn_date' is null");
        }
        else if (base.Request.QueryString.Get("txn_date").Length < "yyyyMMddHHmmss".Length)
        {
            this.RespondDefault("Argument 'txn_date' bad format. Format example yyyyMMddHHmmss.");
        }
        else if (string.IsNullOrEmpty(this.PayType))
        {
            this.PayType = "0";
        }
        
        else if (string.IsNullOrEmpty(this.lpUser))
        {
            this.RespondDefault("Argument 'user' is null");
        }
        else if (string.IsNullOrEmpty(this.lpPass))
        {
            this.RespondDefault("Argument 'password' is null");
        }
        
    }

    protected HttpApplication ApplicationInstance
    {
        get
        {
            return this.Context.ApplicationInstance;
        }
    }

    protected DefaultProfile Profile
    {
        get
        {
            return (DefaultProfile)this.Context.Profile;
        }
    }
}

