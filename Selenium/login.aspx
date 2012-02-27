<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="SeleniumTalk.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Northwind Login</title>
    <link type="text/css" href="Style/main.css" rel="Stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="loginPosition">
        <h2>Northwind customer portal</h2>    
        <asp:Login ID="login" 
                   runat="server"
                   BackColor="#F7F7DE" 
                   BorderColor="#CCCC99" 
                   BorderWidth="1px" 
                   Font-Names="Verdana" 
                   Font-Size="10pt" >
                   
            <LabelStyle Font-Names="arial" 
                        HorizontalAlign="Left" />
                        
            <TitleTextStyle BackColor="#6B696B" 
                            Font-Bold="True" 
                            ForeColor="#FFFFFF" />
                            
        </asp:Login>
            
        </div>
    </form>
</body>
</html>
