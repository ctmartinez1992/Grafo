<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" MasterPageFile="~/Site.Master" Inherits="Website.Register" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
        <h2>Use the form below to create a new account.</h2>
    <div id="registerForm">
        <table>
        <br />
        <tr>
            <td>
                <asp:Label ID="Name" runat="server" Text="Name:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="Nome" runat="server"></asp:TextBox>
            </td>
            <td class="auto-style4">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Username:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="Username" runat="server"></asp:TextBox>
            </td>
            <td class="auto-style4">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="Password:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="Password" runat="server" TextMode="password"></asp:TextBox>
            </td>
            <td class="auto-style4">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1">
                <asp:Label ID="Label6" runat="server" Text="Repeat password:"></asp:Label>
            </td>
            <td class="auto-style1">
                <asp:TextBox ID="PassWord_C" runat="server" TextMode="password"></asp:TextBox>
            </td>
            <td class="auto-style5">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" Text="E-mail:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="Email" runat="server"></asp:TextBox>
            </td>
            <td class="auto-style4">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label7" runat="server" Text="Date of birth:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="Birth" runat="server" OnTextChanged="Username0_TextChanged"></asp:TextBox>
            </td>
            <td class="auto-style4">
                (dd-mm-yyy)</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label8" runat="server" Text="Phone:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="Phone" runat="server"></asp:TextBox>
            </td>
            <td class="auto-style4">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="Registar" runat="server" onclick="Registar_Click" Text="Confirmar" />
            </td>
            <td class="auto-style4">
                &nbsp;</td>
        </tr>
    </table>
        </div>
    <br />
    <asp:Label ID="Label5" runat="server" Font-Bold="true" Font-Size="Larger" Font-Underline="true"></asp:Label>
</asp:Content>
<asp:Content ID="Content1" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        .auto-style1 {
            height: 26px;
        }
        .auto-style4 {
            width: 79px;
        }
        .auto-style5 {
            height: 26px;
            width: 79px;
        }
    </style>
</asp:Content>

