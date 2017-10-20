<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<div class="leftPanel">
        <%-- DXCOMMENT: Configure the left panel's menu --%>
    <% 
        Html.DevExpress().NavBar(settings => {
            settings.Name = "LeftNavBar";
            settings.AutoCollapse = true;
            settings.EnableAnimation = true;
            settings.Width = System.Web.UI.WebControls.Unit.Percentage(100);
            settings.ControlStyle.Border.BorderWidth = System.Web.UI.WebControls.Unit.Pixel(0);
            settings.ControlStyle.Paddings.Padding = System.Web.UI.WebControls.Unit.Pixel(0);
        }).BindToXML(HttpContext.Current.Server.MapPath("~/App_Data/SideMenu.xml"), "/menu/*").Render();
    %>
</div>