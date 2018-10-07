<%@ Page Language="C#" MasterPageFile ="~/MasterPage.master" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 100%;">
        <tr>
            <td>
            
                <asp:CheckBoxList ID="CheckBoxList2" runat="server" AutoPostBack="True" 
            onselectedindexchanged="CheckBoxList2_SelectedIndexChanged">
        </asp:CheckBoxList>
        <asp:ListBox ID="ListBox1" runat="server" AutoPostBack="True" Height="107px" 
            onselectedindexchanged="ListBox1_SelectedIndexChanged" 
            style="margin-right: 2px" Width="100px"></asp:ListBox>
        &nbsp;<br />
        <br />
        <asp:Label ID="Label1" runat="server" 
            Text="Spisak pacijenata izabranog lekara:"></asp:Label>
        <br />
        <br />
        <asp:ListBox ID="ListBox2" runat="server" AutoPostBack="True" Height="107px" 
            onselectedindexchanged="ListBox2_SelectedIndexChanged" Width="102px">
        </asp:ListBox>
    
    
    <p>
        <asp:Label ID="Label2" runat="server" Text="Detalji o izabranom pacijentu:" 
            Visible="False"></asp:Label>
        </p>
    <p>
        <asp:DetailsView ID="DetailsView1" runat="server" Height="60px" Width="345px" 
            BackColor="#99CCFF" BorderColor="#333399" Visible="False">
        </asp:DetailsView>
        </p>
    <p>
        <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">Pregled svih kartona</asp:LinkButton>
    </p>
    <p>
        <asp:LinkButton ID="LinkButton2" runat="server" onclick="LinkButton2_Click">Pogledajte spisak lekara</asp:LinkButton>
    </p>
    

&nbsp;
            </td>
            <td>
                <asp:Image ID="Image1" runat="server" 
                    ImageUrl="~/Slike/Bolnica-Sveti-Sava.jpg" ImageAlign="Right" />
            </td>
            
        </tr>
       
    </table>
 <
</asp:Content>
    
       