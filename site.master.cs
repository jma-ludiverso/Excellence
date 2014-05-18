using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class themes_Excellence_site : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.EasyMenu1.AddMenuItem("menu_1", "Inicio", "", BlogEngine.Core.Utils.AbsoluteWebRoot.ToString(), "", "");
        this.EasyMenu1.AddSeparator("sep_1", "|");
        this.EasyMenu1.AddMenuItem("menu_2", "Archivo", "", BlogEngine.Core.Utils.AbsoluteWebRoot.ToString() + "archive", "", "");
        this.EasyMenu1.AddSeparator("sep_2", "|");
        string[] arrSegments = BlogEngine.Core.Utils.AbsoluteWebRoot.Segments;
        string urlAlternative = "";
        for (int i = 0; i <= arrSegments.Length - 2; i++)
        {
            urlAlternative += arrSegments[i];
        }
        this.EasyMenu1.AddMenuItem("menu_3", "M&aacute;s sobre m&iacute;", "", urlAlternative + "excellence/home.aspx", "", "");
        this.EasyMenu1.AddSeparator("sep_3", "|");
        this.EasyMenu1.AddMenuItem("menu_4", "Contacto", "", BlogEngine.Core.Utils.AbsoluteWebRoot.ToString() + "contact", "", "");
        if (BlogEngine.Core.Security.IsAuthenticated)
        {
            this.EasyMenu1.AddSeparator("sep_l", "|");
            this.EasyMenu1.AddMenuItem("menu_n", "Cerrar sesi&oacute;n", "", BlogEngine.Core.Utils.RelativeWebRoot + "Account/login.aspx?logoff", "", "");
            this.pAdmin.Visible = true;
        }
        else
        {
            this.EasyMenu1.AddSeparator("sep_l", "|");
            this.EasyMenu1.AddMenuItem("menu_n", "Iniciar sesi&oacute;n", "", BlogEngine.Core.Utils.RelativeWebRoot + "Account/login.aspx", "", "");
        }
    }

    protected override void OnInit(EventArgs e)
    {
        SwitchPage();
        base.OnInit(e);
    }

    private void SetAlternatePage(string alternate)
    {
        string themePath = BlogEngine.Core.Utils.ApplicationRelativeWebRoot + "themes/" + BlogEngine.Core.BlogSettings.Instance.Theme;
        var newPath = themePath + "/" + alternate;
        var currentPage = HttpContext.Current.CurrentHandler as System.Web.UI.Page;
        var path = BlogEngine.Core.Utils.ApplicationRelativeWebRoot + alternate;  
        if (currentPage.Request.CurrentExecutionFilePath.ToLower() == path.ToLower())
            Response.Redirect(newPath, false);
    }

    private void SwitchPage()
    {
        Raisr.BE.ThemeHelper.PageType CurrentPageType = Raisr.BE.ThemeHelper.GetCurrentPageType();
        switch (CurrentPageType)
        {
            case Raisr.BE.ThemeHelper.PageType.Archive:
                SetAlternatePage("archive.aspx");
                break;
            case Raisr.BE.ThemeHelper.PageType.Contact:
                SetAlternatePage("contact.aspx");
                break;
            case Raisr.BE.ThemeHelper.PageType.Search:
                SetAlternatePage("search.aspx");
                break;
        }
    }

}
