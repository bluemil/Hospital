<%@ Page Language="C#" MasterPageFile ="~/MasterPage.master" AutoEventWireup="true" CodeFile="NoviKarton.aspx.cs" Inherits="NoviKarton" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<p>
        Sifra novog kartona je:
        <asp:Label ID="lblsifkart" runat="server" Text="Label"></asp:Label>
    </p>
    <p>
        Broj zdravstvene knjizice je:
        <asp:Label ID="lblbrzdrknj" runat="server" Text="Label"></asp:Label>
    </p>
    <p>
        Odaberi datum otvaranja:</p>
    <p>
        <asp:Calendar ID="Calendar1" runat="server" BackColor="White" 
            BorderColor="#990000"></asp:Calendar>
    </p>
    <p>
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
            Text="Evidentiraj" />
    </p>
    <p>
        <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">Pregled svih kartona</asp:LinkButton>
    </p>
    <p>
        &nbsp;</p>
   


</asp:Content>
    