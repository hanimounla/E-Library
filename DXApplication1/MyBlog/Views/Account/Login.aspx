<%@ Page Language="CS" MasterPageFile="~/Views/Shared/Light.Master" Inherits="System.Web.Mvc.ViewPage<MyBlog.Models.LoginModel>" %>

<asp:content id="ClientArea" contentplaceholderid="MainContent" runat="server">
    <div class="accountHeader">
    <h2>
        Log In</h2>
    <p>
        Please enter your username and password. <%= Html.ActionLink("Register", "Register") %>
        if you don't have an account.</p>
</div>
<% using (Html.BeginForm()) { %>
        <%: Html.AntiForgeryToken() %>
        <% Html.DevExpress().Label(settings => {
        settings.Name = "UserNameLabel";
        settings.Text = "User Name";
        settings.AssociatedControlName = "UserName";
    }).Render();
     %>
    <div class="form-field">
        <%: Html.EditorFor(m => m.UserName)%>
        <%: Html.ValidationMessageFor(m => m.UserName) %>
    </div>
            
    <% Html.DevExpress().Label(settings => {
        settings.Name = "PasswordLabel";
        settings.Text = "Password";
        settings.AssociatedControlName = "Password";
    }).Render(); %>
    <div class="form-field">
        <%: Html.EditorFor(m => m.Password)%>
        <%: Html.ValidationMessageFor(m => m.Password) %>
    </div>
            
    <div class="form-field">
        <% Html.DevExpress().CheckBox(settings => {
            settings.Name = "RememberMe";
            settings.Text = "Remember me?";
           }).Render(); %>
    </div>
    <% Html.DevExpress().Button(settings => {
        settings.Name = "Button";
        settings.Text = "Log On";
        settings.UseSubmitBehavior = true;
    }).Render(); %>
<% } %>
</asp:content>