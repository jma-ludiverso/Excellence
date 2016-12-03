<%@ Page Title="" Language="C#" MasterPageFile="~/themes/Excellence/site.master" AutoEventWireup="true" CodeFile="contact.aspx.cs" Inherits="themes_Excellence_contact" %>
<%@ Import Namespace="BlogEngine.Core" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Ajax.UI.Captcha" Assembly="Obout.Ajax.UI" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

    <script type="text/JavaScript">
        // on page load - called by ScriptManager
        function pageLoad() {
            // handler for request invoking
            function invokingRequest(sender, args) {
                // remove the request invoking handler
                // (it will be added again on the next pageLoad() call after partial postback)
                Sys.Net.WebRequestManager.remove_invokingRequest(invokingRequest);
                // activate the Captcha's progress
                $find("<%= Captcha1.ClientID %>").beginProgress();
            }
            // add the request invoking handler
            Sys.Net.WebRequestManager.add_invokingRequest(invokingRequest);
        }
    </script>

    <asp:ScriptManager runat="Server" EnablePartialRendering="true" ID="ScriptManager1" />
    <asp:Panel ID="pContactData" runat="server" Visible="true" CssClass="contactData">
        <h1>Contacta conmigo</h1>
        <div>
            ¿Necesitas elaborar una presentación con diapositivas que acompañe tu presentación comercial?<br />
            ¿Necesitas para gestionar mejor tu negocio una tabla de cálculo automática que controle tu stock, tu contabilidad..?<br />
            ¿No sabes si este es el servicio que buscas?<br />
            No dudes en contactar conmigo para plantear tus dudas y que podamos valorar tus necesidades. <br /><br />
        </div>      
        <table>
            <tr>
                <td>
                    <label for="<%=txtName.ClientID %>"><%=Resources.labels.name %></label>
                </td>
                <td>
                    <asp:TextBox runat="server" id="txtName" cssclass="field" />
                    <asp:requiredfieldvalidator ID="Requiredfieldvalidator1" runat="server" controltovalidate="txtName" ErrorMessage="<%$Resources:labels, required %>" Display="Dynamic" /><br />
                </td>
            </tr>
            <tr>
                <td>
                    <label for="<%=txtEmail.ClientID %>"><%=Resources.labels.email %></label>
                </td>
                <td>
                    <asp:TextBox runat="server" id="txtEmail" cssclass="field" />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail" display="dynamic" ErrorMessage="<%$Resources:labels, enterValidEmail %>" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                    <asp:requiredfieldvalidator ID="Requiredfieldvalidator2" runat="server" controltovalidate="txtEmail" ErrorMessage="<%$Resources:labels, required %>" Display="Dynamic" /><br />
                </td>
            </tr>
            <tr>
                <td>
                    <label for="<%=txtSubject.ClientID %>"><%=Resources.labels.subject %></label>
                </td>
                <td>
                    <asp:TextBox runat="server" id="txtSubject" cssclass="field" />
                    <asp:requiredfieldvalidator ID="Requiredfieldvalidator3" runat="server" controltovalidate="txtSubject" ErrorMessage="<%$Resources:labels, required %>" Display="Dynamic" /><br />
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <label for="<%=txtMessage.ClientID %>"><%=Resources.labels.message %></label>
                </td>
                <td>
                    <asp:TextBox runat="server" id="txtMessage" textmode="multiline" rows="5" columns="30" />
                    <asp:requiredfieldvalidator ID="Requiredfieldvalidator4" runat="server" controltovalidate="txtMessage" ErrorMessage="<%$Resources:labels, required %>" display="dynamic" />    
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left">
                    <a href="javascript: $find('<%= Captcha1.ClientID %>').getNewImage();">Generate a new image</a><br /><%--<br />--%>
                    <obout:CaptchaImage ID="Captcha1" runat="server" RelativeImageUrl="false" /><br /><%--<br />--%>
                    <obout:OboutTextBox runat="server" ID="CaptchaIn" Width="200" WatermarkText="Type the code from the image" FolderStyle="OboutTextBox" /><br />
                    <obout:CaptchaValidator runat="server" ControlToValidate="CaptchaIn" CaptchaImageID="Captcha1"
                        ErrorMessage="The code you entered is not valid." Display="Static">
                    </obout:CaptchaValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="right">
                    <asp:button runat="server" id="btnSend" Text="Enviar" onclick="btnSend_Click"  />    
                    <asp:label runat="server" id="lblStatus" visible="false"></asp:label>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pContactMsg" runat="server" Visible="false" CssClass="contactMsg">
        <h1>Gracias!!</h1>
        <div>
            Hemos recibido tu mensaje y en breve nos podremos en contacto contigo.
        </div>
    </asp:Panel>
</asp:Content>

