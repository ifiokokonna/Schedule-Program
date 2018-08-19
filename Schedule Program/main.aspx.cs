using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Schedule_Program
{
    public partial class Main : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            hmisc.Value = "origin";

            showAlert();
            
            hincludesaturdays.Value = includesaturdays.Checked.ToString();
            hincludesundays.Value = includesundays.Checked.ToString();
            hfullday.Value = fullday.Checked.ToString();
            hshowddates.Value = showddates.Checked.ToString();

            if (weeks.Items.Count == 0)
            {
                weeks.Items.Insert(0, new ListItem("Select One", "-1"));
                weeks.Items.Insert(1, new ListItem("1 Week", "1"));
                weeks.Items.Insert(2, new ListItem("2 Weeks", "2"));
                weeks.Items.Insert(3, new ListItem("3 Weeks", "3"));
                weeks.Items.Insert(4, new ListItem("4 Weeks", "4"));
                weeks.Items.Insert(5, new ListItem("5 Weeks", "5"));
            }

            if (theme.Items.Count == 0)
            {
                theme.Items.Insert(0, new ListItem("Dark", "0"));
                theme.Items.Insert(1, new ListItem("Light", "1"));
            }
        }
        protected void startdate_SelectionChanged(object sender, EventArgs e)
        {
            hstartdate.Value = startdate.SelectedDate.ToShortDateString();
        }

        protected void enddate_SelectionChanged(object sender, EventArgs e)
        {
            henddate.Value = enddate.SelectedDate.ToShortDateString();
        }

        protected void startdate2_SelectionChanged(object sender, EventArgs e)
        {
            hstartdate2.Value = startdate2.SelectedDate.ToShortDateString();
        }

        protected void includesaturdays_CheckedChanged(object sender, EventArgs e)
        {
            if (includesaturdays.Checked == true)
                hincludesaturdays.Value = "true";
            else
                hincludesaturdays.Value = "false";
        }

        protected void includesundays_CheckedChanged(object sender, EventArgs e)
        {
            if (includesundays.Checked)
                hincludesundays.Value = "true";
            else
                hincludesundays.Value = "false";
        }

        protected void fullday_CheckedChanged(object sender, EventArgs e)
        {
            if (fullday.Checked)
                hfullday.Value = "true";
            else
                hfullday.Value = "false";
        }

        protected void showddates_CheckedChanged(object sender, EventArgs e)
        {
            if (showddates.Checked)
                hshowddates.Value = "true";
            else
                hshowddates.Value = "false";
        }

        protected void refresh_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void startdate_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.Date < DateTime.Now.Date)
            {
                e.Day.IsSelectable = false;
                e.Cell.ForeColor = System.Drawing.Color.DimGray;
            }
        }

        protected void enddate_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.Date < DateTime.Now.Date)
            {
                e.Day.IsSelectable = false;
                e.Cell.ForeColor = System.Drawing.Color.DimGray;
            }
        }

        protected void startdate2_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.Date < DateTime.Now.Date)
            {
                e.Day.IsSelectable = false;
                e.Cell.ForeColor = System.Drawing.Color.DimGray;
            }
        }
        private void showAlert()
        {
            try
            {
                if (Session["alert"].Equals(true)
                        && !String.IsNullOrEmpty(Session["title"].ToString())
                        && !String.IsNullOrEmpty(Session["msg"].ToString()))
                {
                    HtmlGenericControl h4 = new HtmlGenericControl("h4");
                    h4.InnerHtml = Session["title"].ToString();
                    h4.Attributes.Add("class", "modal-title");
                    hdr.Controls.Add(h4);

                    HtmlGenericControl p = new HtmlGenericControl("p");
                    p.InnerHtml = Session["msg"].ToString();
                    msg.Controls.Add(p);

                    ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:$('#alert').modal('show');", true);
                }
                Session["alert"] = false;
                Session["title"] = "";
                Session["msg"] = "";
            }
            catch (Exception)
            {
            }
        }

    }
}