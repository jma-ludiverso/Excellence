using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class themes_Excellence_archive : BlogEngine.Core.Web.Controls.BlogBasePage
{

    bool bRecursive = false;
    DateTime minDate = DateTime.Parse(System.Configuration.ConfigurationManager.AppSettings["minDate"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadCategories();
            LoadYears();
        }
        this.calendar.DateMin = minDate;
    }

    protected void changeView(object sender, EventArgs e)
    {
        Obout.Interface.OboutRadioButton rd = (Obout.Interface.OboutRadioButton)sender;
        if (rd.Checked)
        {
            switch (rd.ID)
            {
                case "rbCategory":
                    this.treePosts.Nodes.Clear();
                    this.pCalendar.Visible = false;
                    this.LoadCategories();
                    break;
                case "rbDates":
                    this.treePosts.Nodes.Clear();
                    this.pCalendar.Visible = true;
                    this.drplYears.Attributes["onChange"] = "change_year();";
                    this.calendar.SelectedDate = DateTime.Now; 
                    this.LoadDates();
                    break;
                case "rbSearch":
                    Response.Redirect("search.aspx", false);
                    break;
            }
        }
    }

    protected void change_year(object sender, EventArgs e)
    {
        if (!bRecursive)
        {
            bRecursive = true;
            DateTime selDate = this.calendar.SelectedDate;
            DateTime newDate = new DateTime(int.Parse(this.drplYears.SelectedItem.Text), selDate.Month, selDate.Day);
            if (newDate < calendar.DateMin)
            {
                newDate = calendar.DateMin;
            }
            calendar.SelectedDate = newDate;
            bRecursive = false;
        }
    }

    protected void date_Changed(object sender, EventArgs e)
    {
        if (!bRecursive)
        {
            bRecursive = true;
            OboutInc.Calendar2.Calendar cal = (OboutInc.Calendar2.Calendar)sender;
            DateTime calendarDate = cal.SelectedDate;
            if (!calendarDate.Year.ToString().Equals(this.drplYears.SelectedItem.Text))
            {
                this.drplYears.SelectedIndex = -1;
                ListItem item = this.drplYears.Items.FindByText(calendarDate.Year.ToString());
                item.Selected = true;
            }
            string nodeValue = calendarDate.Year.ToString() + "-" + calendarDate.Month.ToString() + "-1";
            Boolean bExistingNode = false;
            for (int i = 0; i <= this.treePosts.Nodes.Count - 1; i++)
            {
                if (this.treePosts.Nodes[i].Value.Equals(nodeValue))
                {
                    bExistingNode = true;
                    break;
                }
            }
            if (!bExistingNode)
            {
                String nodeName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(calendarDate.Month).ToUpper();
                nodeName += " - " + calendarDate.Year.ToString();
                Obout.Ajax.UI.TreeView.Node nod = new Obout.Ajax.UI.TreeView.Node(nodeName, "img/TextBoxHS.png");
                nod.Value = nodeValue;
                nod.ExpandMode = Obout.Ajax.UI.TreeView.NodeExpandMode.ServerSideCallback;
                this.treePosts.Nodes.Add(nod);
                nod.ExpandAll();
                this.LoadMonthPosts(nod);
            }
            bRecursive = false;
        }
    }

    protected void expandTreeNode(object sender, Obout.Ajax.UI.TreeView.NodeEventArgs e)
    {
        if (!e.Node.Value.Substring(0, 5).Equals("POST-"))
        {
            List<BlogEngine.Core.Post> postList;
            if (!pCalendar.Visible)
            {
                Guid gd = new Guid(e.Node.Value);
                BlogEngine.Core.Category cat = BlogEngine.Core.Category.GetCategory(gd);
                postList = cat.Posts;
            }
            else
            {
                string[] dateValues = e.Node.Value.Split('-');
                DateTime refDate = new DateTime(int.Parse(dateValues[0]), int.Parse(dateValues[1]), int.Parse(dateValues[2]));
                DateTime initDate = new DateTime(refDate.Year, refDate.Month, 1);
                DateTime endDate = new DateTime(refDate.Year, refDate.Month, DateTime.DaysInMonth(refDate.Year, refDate.Month));
                postList = BlogEngine.Core.Post.GetPostsByDate(initDate, endDate);
            }
            this.LoadPosts(postList, e.Node);
        }
    }

    private void LoadCategories()
    {
        foreach (BlogEngine.Core.Category cat in BlogEngine.Core.Category.Categories)
        {
            Obout.Ajax.UI.TreeView.Node nod = new Obout.Ajax.UI.TreeView.Node(cat.Title, "img/TextBoxHS.png");
            nod.Value = cat.Id.ToString();
            nod.ExpandMode = Obout.Ajax.UI.TreeView.NodeExpandMode.ServerSideCallback;
            this.treePosts.Nodes.Add(nod); 
        }
    }

    private void LoadDates()
    {
        DateTime curDate = DateTime.Now;
        DateTime endDate = curDate.AddMonths(-6);
        while (endDate < curDate)
        {
            String nodeName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(curDate.Month).ToUpper();
            nodeName += " - " + curDate.Year.ToString();
            Obout.Ajax.UI.TreeView.Node nod = new Obout.Ajax.UI.TreeView.Node(nodeName, "img/TextBoxHS.png");
            nod.Value = curDate.Year.ToString() + "-" + curDate.Month.ToString() + "-1";
            nod.ExpandMode = Obout.Ajax.UI.TreeView.NodeExpandMode.ServerSideCallback;
            this.treePosts.Nodes.Add(nod);
            if (curDate == DateTime.Now)
            {
                LoadMonthPosts(nod);
                nod.ExpandAll();
            }
            curDate = curDate.AddMonths(-1); 
        }
    }

    private void LoadMonthPosts(Obout.Ajax.UI.TreeView.Node nod)
    {
        string[] dateValues = nod.Value.Split('-');
        DateTime refDate = new DateTime(int.Parse(dateValues[0]), int.Parse(dateValues[1]), int.Parse(dateValues[2]));
        DateTime initDate = new DateTime(refDate.Year, refDate.Month, 1);
        DateTime endDate = new DateTime(refDate.Year, refDate.Month, DateTime.DaysInMonth(refDate.Year, refDate.Month));
        List<BlogEngine.Core.Post> postList = BlogEngine.Core.Post.GetPostsByDate(initDate, endDate);
        if (postList.Count > 0)
        {
            this.LoadPosts(postList, nod);
        }
        else
        {
            this.treePosts.Nodes.Remove(nod);
        }
    }

    private void LoadPosts(List<BlogEngine.Core.Post> postList, Obout.Ajax.UI.TreeView.Node refNode)
    {
        foreach (BlogEngine.Core.Post pst in postList)
        {
            Obout.Ajax.UI.TreeView.Node nod = new Obout.Ajax.UI.TreeView.Node(pst.Title, "img/fav_icon.png");
            nod.Value = "POST-" + pst.Id.ToString();
            nod.NavigateUrl = pst.AbsoluteLink.ToString();
            String postText = pst.Content;
            if (postText.Length > 200)
            {
                postText = postText.Substring(0, 200);
            }
            Obout.Ajax.UI.TreeView.Node nodResumen = new Obout.Ajax.UI.TreeView.Node(BlogEngine.Core.Utils.StripHtml(postText) + " ...");
            nodResumen.SelectMode = Obout.Ajax.UI.TreeView.NodeSelectMode.Expand;
            nod.ChildNodes.Add(nodResumen);
            refNode.ChildNodes.Add(nod);
            nod.ExpandAll();
        }
    }

    private void LoadYears()
    {
        int currentYear = DateTime.Now.Year;
        while (currentYear >= minDate.Year)
        {
            drplYears.Items.Add(currentYear.ToString());
            currentYear -= 1;
        }
    }

}