<%@ Page Title="" Language="C#" MasterPageFile="~/themes/Excellence/site.master" AutoEventWireup="true" CodeFile="search.aspx.cs" Inherits="themes_Excellence_search" %>

<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <div class="searchContent">
        <h1>B&uacute;squeda</h1>
        <div class="searchButtons">
            <obout:OboutRadioButton runat="server" ID="rbContent" GroupName="options" Text="Buscar por contenido" FolderStyle="OboutRadioButton" Checked="true"/>
            <obout:OboutRadioButton runat="server" ID="rbTags" GroupName="options" Text="Buscar por etiqueta" FolderStyle="OboutRadioButton"/>
            <div style="margin-top:10px;">
                <asp:TextBox ID="txtSearch" runat="server" Text=""></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="Search" onclick="btnSearch_Click" />
            </div>
        </div>
        <div>
            <asp:repeater runat="server" id="results">
              <ItemTemplate>
                <div class="searchresult">                    
                    <div>
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/themes/Excellence/img/fav_icon.png" />
                        <a href="<%# Eval("AbsoluteLink") %>"><%# Eval("Title") %></a>           
                    </div>
                    <div>
                        <%# getContent_Summary(Eval("Content").ToString())%>
                    </div>
                    <div>
                        Etiquetas: <%# getTags(Eval("Id").ToString())%>
                    </div>
                </div>
              </ItemTemplate>
            </asp:repeater>
        </div>
    </div> 
</asp:Content>

