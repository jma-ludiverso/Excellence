<%@ Page Title="" Language="C#" MasterPageFile="~/themes/Excellence/site.master" AutoEventWireup="true" CodeFile="archive.aspx.cs" Inherits="themes_Excellence_archive" %>

<%@ Register Assembly="obout_Calendar2_Net" Namespace="OboutInc.Calendar2" TagPrefix="obout" %>
<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.TreeView" TagPrefix="obout" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
        <script type="text/javascript">
		    function change_year() {
		        var drplyear = document.getElementById("drplYears");
		        var year = drplyear.options[drplyear.selectedIndex].innerHTML;
		        var calendarDate = calendar.selectedDate;
		        var month = calendarDate.getMonth();
		        var day = calendarDate.getDate();
		        var selectedDate = new Date(year, month, day);
		        calendar.setDate(selectedDate, selectedDate);
		    }
		</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <asp:ScriptManager runat="Server" EnablePartialRendering="true" ID="ScriptManager1" />
    <div class="archiveContent">
        <h1>Archivo</h1>
        <table width="100%">
            <tr>
                <td valign="top">
                    <div class="archiveButtons1">
                        <obout:OboutRadioButton runat="server" ID="rbCategory"  AutoPostBack="true" GroupName="options" Text="Ver por categorías" Checked="true" OnCheckedChanged="changeView" FolderStyle="OboutRadioButton" />
                        <obout:OboutRadioButton runat="server" ID="rbDates"  AutoPostBack="true" GroupName="options" Text="Ver por fecha" OnCheckedChanged="changeView" FolderStyle="OboutRadioButton"/>
                        <obout:OboutRadioButton runat="server" ID="rbSearch"  AutoPostBack="true" GroupName="options" Text="Buscar" OnCheckedChanged="changeView" FolderStyle="OboutRadioButton"/>
                    </div>
                </td>
                <td>
                    <asp:Panel ID="pCalendar" runat="server" Visible="false" CssClass="archiveButtons2">
                        <table width="100%">
                            <tr>
                                <td valign="top">
                                    <div style="margin-bottom:5px;">
                                        <asp:Label ID="calendarLabel" runat="server" Text="Ir a otra fecha ..." Font-Bold="true"></asp:Label>
                                    </div> 
                                    <div>
                                        <asp:Label ID="lblYear" runat="server" Text="Año: "></asp:Label>
                                        <asp:DropDownList ID="drplYears" runat="server" AutoPostBack="true" ClientIDMode="Static" 
                                            onselectedindexchanged="change_year">
                                        </asp:DropDownList>
                                    </div>
                                </td>
                                <td align="right">
                                    <div>
                                        <obout:Calendar runat="server" id="calendar" Columns="1" AutoPostBack="true" ClientIDMode="Static"
                                            TitleText="Seleccione fecha" OnDateChanged="date_Changed" ViewStateMode="Disabled" >
                                        </obout:Calendar>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>                       
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div class="archiveData">
                        <obout:Tree runat="server" ID="treePosts" LoadingMessage="cargando ..." CssClass="vista"
                            ontreenodeexpanded="expandTreeNode" >
                        </obout:Tree>
                    </div>        
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

