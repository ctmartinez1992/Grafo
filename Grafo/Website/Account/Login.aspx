<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Login.aspx.cs" Inherits="Website.Login" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <table width="100%">
            <tr>
                <td width="50%" align="right">
        <asp:Label ID="Label2" runat="server" Text="UserName: "></asp:Label>
                </td>
                <td  width="50%">
        <asp:TextBox ID="username" runat="server" Height="16px" Width="231px"></asp:TextBox>
    <asp:Label ID="Label5" runat="server" Font-Bold="true" Font-Size="Larger" Font-Underline="true" style="font-size: large"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" >
        <asp:Label ID="Label3" runat="server" Text="PassWord: "></asp:Label>
                </td>
                <td >
        <asp:TextBox ID="password" TextMode="password" runat="server" Width="233px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" >
        <asp:Button ID="log" runat="server" onclick="Log_Click" Text="Log In" />
    
                </td>
                <td >
        <asp:Button ID="reg" runat="server" onclick="Reg_Click" Text="Register" />
    
                </td>
            </tr>
        </table>
    
    <br />
    
    </asp:Content>
<asp:Content ID="Content1" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        .auto-style1 {
            width: 102%;
        }
        .auto-style2 {
            width: 76px;
            text-align: right;
        }
        .auto-style3 {
            width: 224px;
        }
    </style>
</asp:Content>

