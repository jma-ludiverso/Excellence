using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class themes_Excellence_search : BlogEngine.Core.Web.Controls.BlogBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (!this.txtSearch.Text.Equals(""))
        {
            string searchQuery = Server.HtmlEncode(this.txtSearch.Text);
            if (rbContent.Checked)
            {
                this.searchContent(searchQuery);
            }
            else
            {
                this.searchTags(searchQuery);
            }            
        }
    }

    protected string getContent_Summary(string content)
    {
        string text = text = BlogEngine.Core.Utils.StripHtml(content);
        if (text.Length > 200)
        {
            text = text.Substring(0, 200) + " ...";
        }
        text = "\"" + text.Trim() + "\"";
        return text;
    }

    protected string getTags(string Id)
    {
        string ret = "";
        Guid gui = new Guid(Id);
        BlogEngine.Core.Post pst = BlogEngine.Core.Post.GetPost(gui);
        if (pst != null)
        {
            BlogEngine.Core.StateList<string> tags = pst.Tags;
            foreach (string tag in tags)
            {
                ret += tag + ", ";
            }
            if (ret.EndsWith(", "))
            {
                ret = ret.Substring(0, ret.Length - 2);
            }
        }
        return ret;
    }

    private void searchContent(string query)
    {
        List<BlogEngine.Core.IPublishable> list = BlogEngine.Core.Search.Hits(this.txtSearch.Text, false);
        this.results.DataSource = list;
        this.results.DataBind();
    }

    private void searchTags(string query)
    {
        List<BlogEngine.Core.Post> listBase = BlogEngine.Core.Post.AllBlogPosts;
        List<BlogEngine.Core.IPublishable> list = new List<BlogEngine.Core.IPublishable>();
        foreach (BlogEngine.Core.Post pst in listBase)
        {
            BlogEngine.Core.StateList<string> tags = pst.Tags;
            foreach(string tag in tags)
            {
                if(tag.ToLower().Contains(query.ToLower()))
                {
                    BlogEngine.Core.IPublishable item = pst;
                    list.Add(item);
                    break;
                }
            }
        }
        this.results.DataSource = list;
        this.results.DataBind();
    }

}