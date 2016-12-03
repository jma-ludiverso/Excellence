using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BlogEngine.Core;
using System.Net.Mail;
using Obout.Ajax.UI.Captcha;

public partial class themes_Excellence_contact : BlogEngine.Core.Web.Controls.BlogBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Captcha1.TextLength = 5;
        Captcha1.FontFamily = "Verdana";
        Captcha1.ForeColor = System.Drawing.Color.Black;
        Captcha1.BackColor = System.Drawing.Color.White;
        Captcha1.BrushFillerColor = System.Drawing.Color.White;
        Captcha1.TextBrush = BrushType.Solid;
        Captcha1.BackBrush = BrushType.Confetti;
        Captcha1.LineNoise = NoiseLevel.None;
        Captcha1.BackgroundNoise = NoiseLevel.Low;
        Captcha1.FontWarpLevel = NoiseLevel.Low;
        Captcha1.BorderWidth = Unit.Empty;
        Captcha1.BorderColor = System.Drawing.Color.Empty;
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.Page.IsValid)
            { 
                string body = this.EmailBody(this.txtEmail.Text, this.txtName.Text, this.txtMessage.Text);
                this.SendEmail(this.txtEmail.Text, this.txtName.Text, this.txtSubject.Text, body);
                this.SendConfirmation(this.txtEmail.Text, this.txtName.Text, this.txtSubject.Text, body);
                this.pContactData.Visible = false;
                this.pContactMsg.Visible = true;
                CaptchaIn.Text = "";
            }
        }
        catch (Exception ex)
        {
            if (Security.IsAuthorizedTo(Rights.ViewDetailedErrorMessages))
            {
                if (ex.InnerException != null)
                {
                    lblStatus.Text = ex.InnerException.Message;
                }
                else
                {
                    lblStatus.Text = ex.Message;
                }
            }
        }
    }

    private string EmailBody(string email, string name, string message)
    {
        string ret = "<div style=\"font: 11px verdana, arial\">";
        ret += Server.HtmlEncode(message).Replace("\n", "<br />") + "<br /><br />";
        ret += "<hr /><br />";
        ret += "<h3>" + Resources.labels.contactAuthorInformation + "</h3>";
        ret += "<div style=\"font-size:10px;line-height:16px\">";
        ret += "<strong>" + Resources.labels.name + ":</strong> " + Server.HtmlEncode(name) + "<br />";
        ret += "<strong>" + Resources.labels.email + ":</strong> " + Server.HtmlEncode(email) + "<br />";

        if (ViewState["url"] != null)
            ret += string.Format("<strong>" + Resources.labels.website + ":</strong> <a href=\"{0}\">{0}</a><br />", ViewState["url"]);

        if (ViewState["country"] != null)
            ret += "<strong>" + Resources.labels.countryCode + ":</strong> " + ((string)ViewState["country"]).ToUpperInvariant() + "<br />";

        if (HttpContext.Current != null)
        {
            ret += "<strong>" + Resources.labels.contactIPAddress + ":</strong> " + Utils.GetClientIP() + "<br />";
            ret += "<strong>" + Resources.labels.contactUserAgent + ":</strong> " + HttpContext.Current.Request.UserAgent;
        }
        return ret;
    }

    private void SendConfirmation(string email, string name, string subject, string message)
    {
        try
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(BlogSettings.Instance.Email, name);
                mail.To.Add(email);
                mail.Subject = "Excellence - Hemos recibido tu mensaje";
                message = "<br/>Hemos recibido tu mensaje. En breve nos pondremos en contacto contigo.<br/>Gracias!!<br/><br/>" +
                        "---------------------------------------------------------------------" +
                        "Mensaje original: " +
                        "---------------------------------------------------------------------" + message;
                mail.Body = message;
                Utils.SendMailMessage(mail);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void SendEmail(string email, string name, string subject, string message)
    {
        try
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(BlogSettings.Instance.Email, name);
                mail.ReplyToList.Add(new MailAddress(email, name));
                mail.To.Add(BlogSettings.Instance.Email);
                mail.Subject = BlogSettings.Instance.EmailSubjectPrefix + " " + Resources.labels.email.ToLower() + " - " + subject;
                mail.Body = message;
                Utils.SendMailMessage(mail);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

}