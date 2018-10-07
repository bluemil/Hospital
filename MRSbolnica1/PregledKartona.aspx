<%@ Page Language="C#" MasterPageFile ="~/MasterPage.master" AutoEventWireup="true" CodeFile="PregledKartona.aspx.cs" Inherits="PregledKartona" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<asp:Label ID="Label1" runat="server" Text="Odaberi sifru kartona: "></asp:Label>
        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" 
            onselectedindexchanged="DropDownList1_SelectedIndexChanged">
        </asp:DropDownList>
        &nbsp;<asp:Label ID="Label4" runat="server" Text="Odaberi pacijenta:"></asp:Label>
&nbsp;<asp:DropDownList ID="ddlPacijent" runat="server" AutoPostBack="True" 
            onselectedindexchanged="ddlPacijent_SelectedIndexChanged">
        </asp:DropDownList>
        <br />
        <asp:DetailsView ID="DetailsView1" runat="server" Height="50px" 
        Width="125px" BackColor="#66CCFF" BorderColor="White">
        </asp:DetailsView>
        <br />
        <asp:GridView ID="GridView1" runat="server" BackColor="White" 
        BorderColor="#003399">
        </asp:GridView>
    
</asp:Content>
    
        
   
