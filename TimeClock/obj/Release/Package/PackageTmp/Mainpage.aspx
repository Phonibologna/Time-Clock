<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Mainpage.aspx.cs" Inherits="TimeClock.Mainpage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 98px;
        }
        .auto-style3 {
            width: 144px;
        }
        .auto-style4 {
            width: 139px;
        }
        .auto-style5 {
            width: 100%;
            height: 282px;
        }
        .auto-style6 {
            width: 163px;
        }
    </style>
</head>
<body style="height: 415px">
    <form id="form1" runat="server">
        <div>
        </div>
        <table style="width:100%;">
            <tr>
                <td class="auto-style1">
                    <asp:Button ID="PunchInButton" runat="server" OnClick="PunchInButton_Click" Text="Start Work" />
                </td>
                <td class="auto-style4">
                    <asp:Label ID="Label2" runat="server" Text="Enter Id/Name:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="IDBox" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:Button ID="PunchOutButton" runat="server" OnClick="PunchOutButton_Click" Text="Stop Work" Width="77px" />
                </td>
                <td class="auto-style4">&nbsp;</td>
                <td>
                    <asp:Button ID="OtherButton" runat="server" OnClick="OtherButton_Click" Text="Time Adjustment" Width="143px" />
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    &nbsp;</td>
                <td class="auto-style4">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
        <table class="auto-style5">
            <tr>
                <td class="auto-style6">
                    <asp:Label ID="Label1" runat="server" Text="Time Worked:"></asp:Label>
                    <br/>
                    <asp:Button ID="UpdateButton" runat="server" OnClick="UpdateButton_Click" Text="Refresh" />
                    
                    
                </td>
                <td class="auto-style3">
                    <asp:Label ID="TimeLabel" runat="server" Text="00:00:00"></asp:Label>
                </td>
                <td>
                    <asp:Calendar ID="WorkCalendar" runat="server" OnSelectionChanged="WorkCalendar_SelectionChanged"></asp:Calendar>
                </td>
            </tr>
            <tr>
                <td class="auto-style6">
                    STATUS:</td>
                <td class="auto-style3">
                    <asp:Label ID="StatusLabel" runat="server"></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style6">Last Punch:</td>
                <td class="auto-style3">
                    <asp:Label ID="LastPunchLbl" runat="server"></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </form>
</body>
</html>
