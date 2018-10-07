<%@ Page Language="C#"MasterPageFile ="~/MasterPage.master" AutoEventWireup="true" CodeFile="SpisakLekara.aspx.cs" Inherits="SpisakLekara" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <p>
        Spisak lekara:</p>
    
    <p>
        <asp:GridView ID="GridView1" runat="server" BackColor="White" 
            BorderColor="#000099">
        </asp:GridView>
    </p>
    <p>
        <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">Unesite novog lekara</asp:LinkButton>
    </p>

</asp:Content>
   
    
