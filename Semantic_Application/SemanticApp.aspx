<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SemanticApp.aspx.cs" Inherits="Semantic_Application.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: center;padding: 5px 5px 20px 0px; margin: 14px 0px 0px 0px; background-color: #9999dd;color: white; height: 51px;">
            <h1 style="text-align:center;position:center">
                <asp:Label ID="Label1" style="text-align:center;position:center" runat="server" Text="Restaurant SPARQL Query Editor"></asp:Label>
            </h1>
        </div>
         <div>
            <h3>
                <asp:Label ID="Label2" runat="server" Text="Enter the Query below"></asp:Label>
            </h3>
             <h3>
                 <asp:TextBox ID="TextBox1" runat="server" Height="198px" OnTextChanged="TextBox1_TextChanged" TextMode="MultiLine" Width="539px"></asp:TextBox>
            </h3>
        </div>
        
        <asp:Button ID="Button1" runat="server" style="margin-left: 9px" Text="Run Query" Width="118px" OnClick="Button1_Click1" />
        <asp:Button ID="Button2" runat="server" style="margin-left: 87px" Text="Clear" Width="142px" OnClick="Button2_Click" />
        <p style="margin-left: 520px">
            <asp:PlaceHolder ID="PlaceHolder1" runat="server" ></asp:PlaceHolder>
        </p>
    </form>
</body>
</html>
