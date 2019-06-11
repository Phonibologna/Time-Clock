<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TimeAdjustment.aspx.cs" Inherits="TimeClock.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 514px;
        }
        .auto-style2 {
            width: 348px;
        }
        .auto-style3 {
            width: 514px;
            height: 30px;
        }
        .auto-style4 {
            width: 348px;
            height: 30px;
        }
        .auto-style5 {
            height: 30px;
        }
        .auto-style6 {
            width: 513px;
        }
    </style>
</head>
<body style="height: 174px">
    <form id="form1" runat="server">
        <div>
            <table style="width:100%;">
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="Label1" runat="server" Text="Please Enter the Time to be Added/Subtracted:"></asp:Label>
                    </td>
                    <td class="auto-style2">
                        <asp:Label ID="Label6" runat="server" Text="Hours:   "></asp:Label>
                        <asp:TextBox ID="HourBox" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label7" runat="server" Text="Minutes:   "></asp:Label>
                        <asp:TextBox ID="MinuteBox" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="Label2" runat="server" Text="The First Box is for Hours, the Second for Minutes."></asp:Label>
                    </td>
                    <td class="auto-style2">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style3">
                        <asp:Label ID="Label5" runat="server" Text="Please Enter the Reason for Time Adjustment Here:"></asp:Label>
                    </td>
                    <td class="auto-style4">
                        <asp:TextBox ID="ReasonBox" runat="server" Width="335px"></asp:TextBox>
                    </td>
                    <td class="auto-style5">&nbsp;</td>
                </tr>
            </table>
        </div>
        <table style="width:100%;">
            <tr>
                <td class="auto-style6">
                    <asp:Label ID="Label3" runat="server" Text="When Finished, Press One of These Buttons to Add or Subtract Time."></asp:Label>
                </td>
                <td>
                    <asp:Button ID="AddButton" runat="server" OnClick="AddButton_Click" Text="Add" />
                </td>
                <td>
                    <asp:Button ID="SubtractButton" runat="server" OnClick="SubtractButton_Click" Text="Subtract" />
                </td>
            </tr>
            <tr>
                <td class="auto-style6">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style6">
                    <asp:Button ID="ReturnButton" runat="server" OnClick="ReturnButton_Click" Text="Close" Width="122px" />
                </td>
                <td>
                    <asp:Label ID="FeedbackLabel" runat="server"></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </form>
</body>
</html>
