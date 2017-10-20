<%@ Page Language="CS" MasterPageFile="~/Views/Shared/Light.Master" Inherits="System.Web.Mvc.ViewPage<MyBlog.Models.ChangePasswordModel>" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="accountHeader">
    <h2>
        Change Password</h2>
    <p>Use the form below to change your password.</p>
    </div>
<% using (Html.BeginForm()) { %>
        <%: Html.AntiForgeryToken() %>
    
    <% Html.DevExpress().Label(settings => {
        settings.Name = "OldPasswordLabel";
        settings.Text = "Old Password";
        settings.AssociatedControlName = "OldPassword";
    }).Render();
        %>
    <div class="form-field">
        <%: Html.EditorFor(m => m.OldPassword) %>
        <%: Html.ValidationMessageFor(m => m.OldPassword) %>
    </div>
                    
    <% Html.DevExpress().Label(settings => {
        settings.Name = "NewPasswordLabel";
        settings.Text = "New Password";
        settings.AssociatedControlName = "NewPassword";
    }).Render();
        %>
    <div class="form-field">
        <%: Html.EditorFor(m => m.NewPassword)%>
        <%: Html.ValidationMessageFor(m => m.NewPassword) %>
    </div>
                    
    <% Html.DevExpress().Label(settings => {
        settings.Name = "ConfirmPasswordLabel";
        settings.Text = "Confirm Password";
        settings.AssociatedControlName = "ConfirmPassword";
    }).Render();
        %>
    <div class="editor-field">
        <%: Html.EditorFor(m => m.ConfirmPassword)%>
        <%: Html.ValidationMessageFor(m => m.ConfirmPassword) %>
    </div>
    <% Html.DevExpress().Button(settings => {
        settings.Name = "Button";
        settings.Text = "Change Password";
        settings.UseSubmitBehavior = true;
    }).Render(); %>
<% } %>
</asp:Content>