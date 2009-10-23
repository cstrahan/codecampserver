<%@ Page Title="" Language="C#" Inherits="ViewPage<PropertyViewModel<DateTime>>" %>

<%@ Import Namespace="MvcContrib.UI.InputBuilder" %>
<asp:content id="Content2" contentplaceholderid="Input" runat="server">
<%=Html.Hidden(Model.Name,Model.Value) %>
<%=Html.TextBox(Model.Name+"_date",Model.Value.ToString("MM/dd/yyyy"),new {@id=Model.Name+"_date",@style="width:110px", @class="datapicker"}) %>
<%=Html.DropDownList(Model.Name + "_hour", GetHourSelectItems(Model.Value.Hour), new { @id = Model.Name + "_hour" })%>
<%=Html.DropDownList(Model.Name + "_minute", GetMinuteSelectItems(Model.Value.Minute), new { @id = Model.Name + "_minute" })%>
<%=Html.DropDownList(Model.Name + "_noon", GetNoonSelectItems(Model.Value.TimeOfDay), new { @id = Model.Name + "_noon" })%>

	<script language="javascript" type="text/javascript">

$(function() {
    $('#<%=Model.Name %>_hour').blur(function() {
        updateDateTime<%=Model.Name%>();
    })
});
$(function() {
    $('#<%=Model.Name %>_minute').blur(function() {
        updateDateTime<%=Model.Name%>();
    })
});
$(function() {
    $('#<%=Model.Name %>_noon').blur(function() {
        updateDateTime<%=Model.Name%>();
    })
});
$(function() {
    $('#<%=Model.Name %>_date').change(function() {
        updateDateTime<%=Model.Name%>();
    })
});
function updateDateTime<%=Model.Name%>()
{
    $('#<%=Model.Name%>').val($('#<%=Model.Name %>_date').val()+' '+(parseInt($('#<%=Model.Name %>_hour').val(),10)+parseInt($('#<%=Model.Name %>_noon').val(),10))+ ':' + $('#<%=Model.Name %>_minute').val());
}
	</script>
<script runat=server>
  private IEnumerable<SelectListItem> GetNoonSelectItems(TimeSpan time)
  {
    yield return new SelectListItem()
    {
      Selected = time.Hours < 13,
      Text = "A.M.",
      Value = "0"
    };
    yield return new SelectListItem()
    {
      Selected = time.Hours >= 13,
      Text = "P.M.",
      Value = "12"
    };
  }

  public IEnumerable<SelectListItem> GetHourSelectItems(int hour)
  {
    if (hour == 0)
    {
      hour = 12;
    }
    if (hour > 12)
    {
      hour -= 12;
    }
    for (int i = 1; i <= 12; i++)
    {
      yield return new SelectListItem()
                       {
                         Selected = i == hour,
                         Text = i.ToString(),
                         Value = i.ToString()
                       };
    }
  }
  public IEnumerable<SelectListItem> GetMinuteSelectItems(int minute)
  {
    for (int i = 0; i < 60; i = i + 15)
    {
      yield return new SelectListItem()
      {
        Selected = i == minute,
        Text = i.ToString(),
        Value = i.ToString()
      };
    }
  }
    </script>
</asp:content>
