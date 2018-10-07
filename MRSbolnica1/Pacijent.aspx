<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Pacijent.aspx.cs" Inherits="Pacijent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css">
    .style7
    {
        width: 176px;
    }
    .style8
    {
        height: 159px;
        width: 416px;
    }
</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 142%;">
        <tr>
            <td class="style8">
               <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" 
            onselectedindexchanged="DropDownList1_SelectedIndexChanged">
        </asp:DropDownList>
        <br />
        <asp:Label ID="lblbrkart" runat="server" Visible="False"></asp:Label>
        <br />
        <asp:Label ID="Label1" runat="server" Text="Br. zdr. knjizice:"></asp:Label>
&nbsp;<asp:TextBox ID="txtBrZdrKnj" runat="server"></asp:TextBox>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" 
            runat="server"  ControlToValidate ="txtBrZdrKnj"
            ErrorMessage="Mozete uneti maksimalno 10 karaktera" 
            ValidationExpression="^[\s\S]{0,10}$" Enabled="False"></asp:RegularExpressionValidator>
        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ErrorMessage="Polje ne sme biti prazno" ControlToValidate ="txtBrZdrKnj" 
            Enabled="False"></asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="Label2" runat="server" Text="Ime:"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtIme" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
            ErrorMessage="Polje ne sme biti prazno" ControlToValidate ="txtIme" 
            Enabled="False"></asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="Label3" runat="server" Text="Prezime:"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtPrezime" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
            ErrorMessage="Polje ne sme biti prazno" ControlToValidate ="txtPrezime" 
            Enabled="False"></asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="Label4" runat="server" Text="Adresa:"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtAdresa" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
            ErrorMessage="Polje ne sme biti prazno" ControlToValidate ="txtAdresa" 
            Enabled="False"></asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="Label5" runat="server" Text="Telefon:"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtTelefon" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
            ErrorMessage="Polje ne sme biti prazno" ControlToValidate ="txtTelefon" 
            Enabled="False"></asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="Label6" runat="server" Text="Id lekara:"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtIdLekara" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label8" runat="server">Cekirajte ako zelite da dodate/promenite lekara: </asp:Label>
        <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" 
            oncheckedchanged="CheckBox1_CheckedChanged" />
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" 
             Visible="False">
        </asp:RadioButtonList>
        <asp:Label ID="Label7" runat="server"></asp:Label>
                &nbsp;
            </td>
            <td align="center" class="style7">
                &nbsp; <br />
        
        <asp:Button ID="btnDodaj" runat="server" onclick="btnDodaj_Click" 
            Text="Dodaj" Visible="False" Height="26px" Width="78px" />
        <br />
        <br />
        <asp:Button ID="btnNovi" runat="server" onclick="btnNovi_Click" Text="Novi" 
                    Height="26px" Width="78px" />
        <br />
        <br />
        <asp:Button ID="btnIzmeni" runat="server" onclick="btnIzmeni_Click" 
            Text="Izmeni" Height="26px" Width="78px" />
        <br />
        <br />
        <asp:Button ID="btnObrisi" runat="server" onclick="btnObrisi_Click" 
            Text="Obrisi" Height="26px" Width="78px" />
    
        <br />
        <br />
    
        <asp:Button ID="btnNoviKarton" runat="server" onclick="btnNoviKarton_Click" 
            Text="Novi karton" Width="78px" Height="26px" />
    
        
    
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        
        
    
            </td>
            <td>
                <asp:Image ID="Image1" runat="server" ImageAlign="Baseline" 
                    ImageUrl="~/Slike/doktor-pacijent-1349077837-214279.jpg" Width="450px" />
            </td>
       </tr>
    </table>
</asp:Content>

    
        
       
    
