using Schedule_Program.lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Schedule_Program
{
    public partial class load : System.Web.UI.Page
    {
        private DateTime fakeDate = DateTime.Parse("01/01/1900");
        protected void Page_Load(object sender, EventArgs e)
        {
            //***No Direct Access Start***//
            if (String.IsNullOrEmpty(Request.Form["hmisc"])) 
                Response.Redirect("main.aspx");
            //***No Direct Access End***//

            //***Employees variables start***//
            List<string> textboxes = new List<string> { Request.Form["emp1"], 
                Request.Form["emp2"], Request.Form["emp3"], Request.Form["emp4"], 
                Request.Form["emp5"], Request.Form["emp6"], Request.Form["emp7"], 
                Request.Form["emp8"], Request.Form["emp9"], Request.Form["emp10"] };
            
            List<HtmlInputText> employees = new List<HtmlInputText>();
            foreach (string emp in textboxes)
            {
                HtmlInputText temp = new HtmlInputText();
                temp.Value = emp;
                employees.Add(temp);
            }
            //***Employees variables end***//

            //***Timing Variables***//
            DateTime startdate;
            DateTime enddate;
            DateTime startdate2;
            try
            {
                startdate = DateTime.Parse(Request.Form["hstartdate"]);
            }
            catch (Exception)
            {
                startdate = fakeDate;
            }
            try
            {
                startdate = DateTime.Parse(Request.Form["hstartdate"]);
            }
            catch (Exception)
            {
                startdate = fakeDate;
            }
            try
            {
                enddate = DateTime.Parse(Request.Form["henddate"]);
            }
            catch (Exception)
            {
                enddate = fakeDate;
            }
            try
            {
                startdate2 = DateTime.Parse(Request.Form["hstartdate2"]);
            }
            catch (Exception)
            {
                startdate2 = fakeDate;
            }
            int numofweeks = int.Parse(Request.Form["weeks"]);
            //***Timing variables end***//

            //***Options variables start***//
            bool includesaturdays = bool.Parse(Request.Form["hincludesaturdays"]);
            bool includesundays = bool.Parse(Request.Form["hincludesundays"]);
            bool fullday = bool.Parse(Request.Form["hfullday"]);
            bool showdates = bool.Parse(Request.Form["hshowddates"]);

            String title = Request.Form["title"];
            String msst = Request.Form["msst"];
            String asst = Request.Form["asst"];
            String aset = Request.Form["aset"];
            String theme = Request.Form["theme"];
            //***Options variables end***//

            //***Validate Timings Start***//
            int numweeks = 0;
            int numdays = 0;
            int numweekdays = 0;
            int weekformat = 5;

            if (startdate != fakeDate && enddate != fakeDate)
            {
                if (includesaturdays)
                    weekformat++;
                if (includesundays)
                    weekformat++;

                numdays = Utility.GetNumberOfDays(startdate, enddate, weekformat, includesaturdays, includesundays);
                numweeks = Utility.GetNumberOfWeeks(numdays, weekformat);
                numweekdays = weekformat;
            }
            else
            {
                if (numofweeks > 0)
                {
                    if (includesaturdays)
                        weekformat++;
                    if (includesundays)
                        weekformat++;

                    numweeks = numofweeks;
                    numdays = weekformat * numweeks;
                    numweekdays = weekformat;
                }

            }

            HtmlGenericControl h4 = new HtmlGenericControl("h4");
            h4.InnerHtml = title;
            dtitle.Controls.Add(h4);
            //***Validate Timings End***//

            //***Validate Employees Start***//
            //Check Invalid Data
            if (numweeks == 0 || numdays == 0 || numweekdays == 0)
                Response.Redirect("main.aspx");

            //Check Date Range
            if (startdate != fakeDate && enddate != fakeDate)
            {
                if (numdays < 5)
                    Response.Redirect("main.aspx");
            }
            else
            {
                if (numofweeks <= 0 || startdate2.Equals(fakeDate))
                    Response.Redirect("main.aspx");
            }
            /**
             * 
             * TODO: Check Date Range
             * TODO: Check Number of Employees
             * TODO: Run No of Employees against date range 
             * 
             */
            employees = Utility.Clean(employees);
            employees = Utility.Shuffle(employees);
            List<HtmlInputText> distinct = new List<HtmlInputText>();
            //Copy
            distinct = new List<HtmlInputText>(employees);

            //Check Minimum Employees Allowed
            if (employees.Count < 5)
                Response.Redirect("main.aspx");
            
            //Check For Minimum Allowed Selection
            if (numdays < employees.Count)
                Response.Redirect("main.aspx");

            //Repopulate
            employees = Utility.Repopulate(employees, numdays);

            HashMap<int, List<HtmlInputText>> empdata = new HashMap<int, List<HtmlInputText>>();
            empdata = Utility.Split(employees, numweeks, numweekdays, fullday);
            //***Validate Employees End***//

            //***Shift Times and Dates Start***//
            string[] shifttime = fullday ? new string[] { msst + " - " + aset } : new string[] { msst + " - " + asst, asst + " - " + aset };

            List<DateTime> dates = startdate != fakeDate && enddate != fakeDate ?
                Utility.PopulateDates(startdate, enddate, numweekdays, includesaturdays, includesundays) : 
                Utility.PopulateDates(startdate2, numdays, includesaturdays, includesundays);
            //***Shift Times and Dates End

            //***Render Start***//

            //Implement Rules
            empdata = RuleBook.RuleCompile(empdata, RuleCheck.OneHalfDayShift, fullday, distinct);
            empdata = RuleBook.RuleCompile(empdata, RuleCheck.NoTwoNoonShift, fullday, distinct);
            
            /*empdata = RuleBook.RuleCompile(empdata, RuleCheck.TwoDaysExemption);*/

            //Tie Up Loose Ends
            //empdata = RuleBook.RuleCompile(empdata, RuleCheck.OneHalfDayShift, fullday, distinct);
            
            for (int i = 0; i < empdata.Size(); i++)
            {
                int start = numweekdays * i;
                int end = (start + numweekdays) > dates.Count ? (dates.Count - start) + start - 1 : (start + numweekdays) - 1;

                HtmlGenericControl h5 = new HtmlGenericControl("h5");
                h5.InnerHtml = "Week " + (i + 1) + (showdates ? " : " + dates.ElementAt(start).ToString("MMMM dd") + 
                                " - " + dates.ElementAt(end).ToString("MMMM dd") : "");
                container.Controls.Add(h5);
                
                FrameWork.GetTable(container, empdata.GetValue(i), showdates, numweekdays, i, dates, shifttime, theme, fullday);
            }
            //***Render End***//

            //***Secure PDF Start***//
            FrameWork.GetPDF(empdata, title, showdates, numweekdays, dates, shifttime, theme, fullday);
            //***Secure PDF End***//
        }

    }
}