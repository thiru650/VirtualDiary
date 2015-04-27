<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="dbVirtualDiary.Login" MasterPageFile="~/VirtualDiary.Master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title></title>
    <link href="styles.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="70%" align="center" cellspacing="0" cellpadding="0" border="0">
        <tr>
            <td>    
                <div class="login">            
                    <asp:Label ID="ErrorLabel" runat="server" ForeColor="Red" />
                    <h1>Login to Virtual Diary</h1>
                    <asp:TextBox ID="userNameTextBox" runat="server" placeholder="User Name" />
                    <br />
                    <asp:TextBox ID="passwordTextBox" runat="server" TextMode="Password" placeholder="Password" />
                    <br />
                    <br />
                    <asp:Button Text="Login" runat="server" ID="LoginButton" OnClick="LoginButton_Click" />
                    </div>
            </td>
        </tr>
    </table>
</asp:Content>
<%--<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="styles.css" rel="stylesheet" />
</head>
<body class="paper">
    <p style="font-size:3em; font-weight:bold; text-align:center; color: #555;text-shadow: 0 1px white;">Virtual Diary</p>
    <!--<div style="border:1px solid #000; vertical-align:central">-->
    <form id="LoginForm" runat="server" class="login">
    <asp:Label ID="ErrorLabel" runat="server" ForeColor="Red"/>
    <h1>Login to Virtual Diary</h1>
    <asp:TextBox ID="userNameTextBox" runat="server" placeholder="User Name"/>
    <br/>    
    <asp:TextBox ID="passwordTextBox" runat="server" TextMode="Password" placeholder="Password"/>
    <br />
    <br />
    <asp:Button Text="Login" runat="server" ID="LoginButton" OnClick="LoginButton_Click"/>
        </form>
        <!--</div>-->
</body>
</html>--%>
