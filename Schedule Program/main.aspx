<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="Schedule_Program.Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Company X1 Schedule Program</title>
    <link rel="stylesheet" href="css/style.css" type="text/css"/>
    <link rel="stylesheet" href="css/bootstrap.css" type="text/css"/>
    <script type="text/javascript" src="js/jquery-3.3.1.min.js" ></script>
    <script type="text/javascript" src="js/bootstrap.js" ></script>
    
</head>
<body class="nbody">
    <form id="form1" runat="server">
        <!-- #Include virtual="require/header.obx" -->
        <div class="container">
          
            <!-- Alert Start -->
          <div class="modal fade" id="alert" role="dialog">
            <div class="modal-dialog">

              <!-- Alert content-->
              <div class="modal-content">
                <div class="modal-header">
                    <asp:PlaceHolder ID="hdr" runat="server"></asp:PlaceHolder>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <asp:PlaceHolder ID="msg" runat="server"></asp:PlaceHolder>
                </div>
                <div class="modal-footer">
                  <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                </div>
              </div>
      
            </div>
          </div>
            <!-- Alert End -->

            <div class="row">

                <div class="col-sm-4">
                      <fieldset>
                        <legend>Employees:</legend>
                          <div class="form-group">
                            <label for="emp1">Employee 1:</label>
                            <asp:TextBox ID="emp1" runat="server" CssClass="form-control" Text="John Doe"></asp:TextBox>
                          </div>
                          <div class="form-group">
                            <label for="emp2">Employee 2:</label>
                            <asp:TextBox ID="emp2" runat="server" CssClass="form-control" Text="Martin Luther"></asp:TextBox>
                          </div>
                          <div class="form-group">
                            <label for="emp3">Employee 3:</label>
                            <asp:TextBox ID="emp3" runat="server" CssClass="form-control" Text="Kingsley Jones"></asp:TextBox>
                          </div>
                          <div class="form-group">
                            <label for="emp4">Employee 4:</label>
                            <asp:TextBox ID="emp4" runat="server" CssClass="form-control" Text="Mr Ford"></asp:TextBox>
                          </div>
                          <div class="form-group">
                            <label for="emp5">Employee 5:</label>
                            <asp:TextBox ID="emp5" runat="server" CssClass="form-control" Text="Steven Seagal"></asp:TextBox>
                          </div>
                          <div class="form-group">
                            <label for="emp6">Employee 6:</label>
                            <asp:TextBox ID="emp6" runat="server" CssClass="form-control" Text="Jonas Brothers"></asp:TextBox>
                          </div>
                          <div class="form-group">
                            <label for="emp7">Employee 7:</label>
                            <asp:TextBox ID="emp7" runat="server" CssClass="form-control" Text="Mark Tyson"></asp:TextBox>
                          </div>
                          <div class="form-group">
                            <label for="emp8">Employee 8:</label>
                            <asp:TextBox ID="emp8" runat="server" CssClass="form-control" Text="Arnold Stone"></asp:TextBox>
                          </div>
                          <div class="form-group">
                            <label for="emp9">Employee 9:</label>
                            <asp:TextBox ID="emp9" runat="server" CssClass="form-control" Text="Joseph Lawrence"></asp:TextBox>
                          </div>
                          <div class="form-group">
                            <label for="emp10">Employee 10:</label>
                            <asp:TextBox ID="emp10" runat="server" CssClass="form-control" Text="Godson Mattew"></asp:TextBox>
                          </div>
                      </fieldset>
                </div>
                <div class="col-sm-4">
                    <fieldset>
                        <legend>Timing:</legend>
                            <div class="form-group">
                                <label for="sdate">Start Date:</label>
                                <asp:Calendar ID="startdate" runat="server" Width="100%" DayHeaderStyle-CssClass="center" DayHeaderStyle-HorizontalAlign="Center" DayHeaderStyle-VerticalAlign="Middle" DayHeaderStyle-BorderStyle="NotSet" TitleStyle-BorderStyle="NotSet" ShowGridLines="True" OnSelectionChanged="startdate_SelectionChanged" NextPrevFormat="ShortMonth" OnDayRender="startdate_DayRender">
<DayHeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="center"></DayHeaderStyle>
                                    <NextPrevStyle CssClass="pleft pright" ForeColor="#CCCCCC" />
                                    <OtherMonthDayStyle ForeColor="#CCCCCC" />
                                    <TitleStyle BackColor="#003300" BorderStyle="None" ForeColor="#CCCCCC" />
                                </asp:Calendar>
                            </div>
                            <div class="form-group">
                                <label for="edate">End Date:</label>
                                <asp:Calendar ID="enddate" runat="server" Width="100%" DayHeaderStyle-CssClass="center" DayHeaderStyle-HorizontalAlign="Center" DayHeaderStyle-VerticalAlign="Middle" TitleStyle-BorderStyle="None" ShowGridLines="True" OnSelectionChanged="enddate_SelectionChanged" NextPrevFormat="ShortMonth" OnDayRender="enddate_DayRender">
<DayHeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="center"></DayHeaderStyle>

                                    <NextPrevStyle CssClass="pleft pright" ForeColor="#CCCCCC" />
                                    <OtherMonthDayStyle ForeColor="#CCCCCC" />

<TitleStyle BorderStyle="None" BackColor="Maroon" ForeColor="#CCCCCC"></TitleStyle>
                                </asp:Calendar>
                            </div>

                            <center><h5>OR</h5></center>

                            <div class="form-group">
                                <label for="weeks">Select No. of Weeks:</label>
                                <asp:DropDownList ID="weeks" runat="server" AutoPostBack="True" CssClass="form-control"></asp:DropDownList>
                            </div>
                                                    
                            <div class="form-group">
                                <label for="sdate2">Start Date:</label>
                                <asp:Calendar ID="startdate2" runat="server" Width="100%" DayHeaderStyle-CssClass="center" DayHeaderStyle-HorizontalAlign="Center" DayHeaderStyle-VerticalAlign="Middle" ShowGridLines="True" OnSelectionChanged="startdate2_SelectionChanged" NextPrevFormat="ShortMonth" OnDayRender="startdate2_DayRender">
<DayHeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="center"></DayHeaderStyle>
                                    <NextPrevStyle CssClass="pleft pright" ForeColor="#CCCCCC" />
                                    <OtherMonthDayStyle ForeColor="#CCCCCC" />
                                    <TitleStyle BackColor="#003300" BorderStyle="None" ForeColor="#CCCCCC" />
                                </asp:Calendar>
                            </div>

                        </fieldset>
                </div>
                <%-- This system, by default is designed to provide the utmost flexibility in choosing the dates for generating schedules
                    which includes adding or subtracting saturdays and sundays as working days. Include saturdays and sundays option
                    has temporarily been disabled due to a known bug and glitch that affects the structural integrity of the application
                    during runtime. If given more time, will look into this problem.
                    
                    For now saturdays and sundays will be included in all generating requests until the bug has been resolved. --%>
                <div class="col-sm-4">
                      <fieldset>
                        <legend>Options:</legend>
                            <div class="form-group form-check">
                                <label class="form-check-label">
                                    <asp:CheckBox ID="includesaturdays" CssClass="form-check-input" runat="server" OnCheckedChanged="includesaturdays_CheckedChanged" AutoPostBack="True" Enabled="false" Checked="True" /> Include Saturdays
                                </label>
                            </div>
                            <div class="form-group form-check">
                                <label class="form-check-label">
                                    <asp:CheckBox ID="includesundays" CssClass="form-check-input" runat="server" OnCheckedChanged="includesundays_CheckedChanged" AutoPostBack="True" Checked="True" Enabled="false" /> Include Sundays
                                </label>
                            </div>  
                            <div class="form-group form-check">
                                <label class="form-check-label">
                                    <asp:CheckBox ID="fullday" CssClass="form-check-input" runat="server" OnCheckedChanged="fullday_CheckedChanged" AutoPostBack="True" /> Full Day Shifts
                                </label>
                            </div>  
                            <div class="form-group form-check">
                                <label class="form-check-label">
                                    <asp:CheckBox ID="showddates" CssClass="form-check-input" runat="server" OnCheckedChanged="showddates_CheckedChanged" AutoPostBack="True" Checked="True" /> Show Dates
                                </label>
                            </div>
                            <div class="form-group">
                                <label for="title">Title:</label>
                                <asp:TextBox ID="title" runat="server" CssClass="form-control" Text="Support Schedule"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="morn">Morning Shift Start Time:</label>
                                <asp:TextBox ID="msst" runat="server" CssClass="form-control" Text="8am"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="after">Afternoon Shift Start Time:</label>
                                <asp:TextBox ID="asst" runat="server" CssClass="form-control" Text="2pm"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="after">Afternoon Shift End Time:</label>
                                <asp:TextBox ID="aset" runat="server" CssClass="form-control" Text="8pm"></asp:TextBox>
                            </div>
                          <div class="form-group">
                              <label for="email">Select Table Theme:</label>
                              <asp:DropDownList ID="theme" runat="server" AutoPostBack="True" CssClass="form-control" EnableViewState="True"></asp:DropDownList>
                            </div>
                          <asp:HiddenField ID="hstartdate" runat="server" />
                          <asp:HiddenField ID="henddate" runat="server" />
                          <asp:HiddenField ID="hstartdate2" runat="server" />
                          <asp:HiddenField ID="hmisc" runat="server" />

                          <asp:HiddenField ID="hincludesaturdays" runat="server" />
                          <asp:HiddenField ID="hincludesundays" runat="server" />
                          <asp:HiddenField ID="hfullday" runat="server" />
                          <asp:HiddenField ID="hshowddates" runat="server" />
                          <asp:Button ID="generate" runat="server" CssClass="btn btn-dark btn-lg btn-block" Text="Generate" PostBackUrl="~/load.aspx" />   
                          <asp:Button ID="refresh" runat="server" CssClass="btn btn-warning btn-lg btn-block" Text="Refresh" OnClick="refresh_Click" />                                      
                      </fieldset>
                    
                </div>

            </div>
            
        </div>





        <!-- #Include virtual="require/footer.obx" -->        
        
    </form>

</body>
</html>
