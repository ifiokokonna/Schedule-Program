<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="load.aspx.cs" Inherits="Schedule_Program.load" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Generate Schedules</title>
    <link rel="stylesheet" href="css/style.css" type="text/css"/>
    <link rel="stylesheet" href="css/bootstrap.css" type="text/css"/>
    
    <script type="text/javascript" src="js/bootstrap.js" ></script>
    <script type="text/javascript" src="js/jquery-3.3.1.min.js" ></script>
</head>
<body class="nbody">
    <form id="form1" runat="server">
        <!-- #Include virtual="require/header.obx" -->
    <div class="container">
        <div class="row">
            <div class="col-sm-12">
                <center><h2>Company X1</h2><br />
                <asp:PlaceHolder ID="dtitle" runat="server"></asp:PlaceHolder></center>
            </div>
            <div class="container-fluid">
                <center><asp:PlaceHolder ID="container" runat="server"></asp:PlaceHolder></center>
            </div>
            
        </div>

        <div class="row bottom">
            <div style="padding-left:0px;" class="col-sm-4">
                <asp:Button ID="back" runat="server" Text="Back" CssClass="btn btn-warning btn-block left btn-lg" OnClientClick="window.history.go(-1); return false;" />
            </div>
            <div class="col-sm-4">
                <asp:Button ID="Button2" runat="server" Text="Randomize"  CssClass="btn btn-dark right button-margin btn-block btn-lg" OnClientClick="location.reload();return false;" /> 
            </div>
            <div style="padding-right:0px;" class="col-sm-4">
                <asp:Button ID="print" runat="server" Text="Print" CssClass="btn btn-dark right btn-block btn-lg" OnClientClick="window.open('pdf/print.pdf', '_blank');return false;" />
            </div>
        </div>

    </div>
        <!-- #Include virtual="require/footer.obx" -->
    </form>
</body>
</html>
