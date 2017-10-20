<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>    <%-- DXCOMMENT: Configure GridView --%>
    <%
    Html.DevExpress().GridView(settings => {
        settings.Name = "GridView";
        settings.CallbackRouteValues = new { Controller = "Home", Action = "GridViewPartialView" };

        settings.Width = System.Web.UI.WebControls.Unit.Percentage(100);
        settings.SettingsPager.PageSize = 32;
        settings.Settings.VerticalScrollBarMode = ScrollBarMode.Visible;
        settings.Settings.VerticalScrollableHeight = 350;
        settings.ControlStyle.Paddings.Padding = System.Web.UI.WebControls.Unit.Pixel(0);
        settings.ControlStyle.Border.BorderWidth = System.Web.UI.WebControls.Unit.Pixel(0);
        settings.ControlStyle.BorderBottom.BorderWidth = System.Web.UI.WebControls.Unit.Pixel(1);
        
        // DXCOMMENT: Configure grid's columns in accordance with data model fields
        settings.Columns.Add("ContactName");
        settings.Columns.Add("CompanyName");
        settings.Columns.Add("ContactTitle");
        settings.Columns.Add("City");
        settings.Columns.Add("Phone");
    }).Bind(Model).Render();
    %>
