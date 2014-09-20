<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.Master" AutoEventWireup="true" CodeBehind="Ad_KPI_Sub.aspx.cs" Inherits="MyAdmin.Admin_Report.Ad_KPI_Sub" %>

<%@ Register Src="../Admin_Control/Admin_Paging.ascx" TagName="Admin_Paging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_Header" runat="server">
    <link href="../Calendar/dhtmlgoodies_calendar/dhtmlgoodies_calendar.css" rel="stylesheet" type="text/css" />
    <script src="../Calendar/dhtmlgoodies_calendar/dhtmlgoodies_calendar.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Tools" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_ToolBox" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cph_Search" runat="server">
    <label>
        Từ ngày:</label>
    <input type="text" runat="server" id="tbx_FromDate" style="width: 70px;" />
    <input type="button" value="..." onclick="displayCalendar(document.getElementById('<%=tbx_FromDate.ClientID %>'),'dd/mm/yyyy',this)" />
    <label>
        Đến ngày:</label>
    <input type="text" runat="server" id="tbx_ToDate" style="width: 70px;" />
    <input type="button" value="..." onclick="displayCalendar(document.getElementById('<%=tbx_ToDate.ClientID %>'),'dd/mm/yyyy',this)" />

    <asp:Button runat="server" ID="btn_Search" Text="Tìm kiếm" OnClick="btn_Search_Click" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cph_Content" runat="server">
    <table border="0" cellpadding="5" cellspacing="0" style="margin-right: 5px; text-align: center; vertical-align: text-top; border-style: solid none none solid; border-width: 1px; border-color: #000000; font-family:Times New Roman; font-size:12px;">
        <tbody>           
            <tr style="background-color: #DADADA; font-weight:bold;">
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" rowspan="3">STT</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000; width:100px;" rowspan="3">Ngày báo cáo</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" colspan="24">Thuê bao</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" colspan="5">Tần suất sử dụng</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" colspan="4">Doanh thu</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" colspan="4">Đánh Giá</td>
            </tr>
            <tr style="background-color: #DADADA; font-weight:bold;">
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" colspan="9">Đăng ký</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" colspan="9">Hủy</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;">Sử dụng dịch vụ trong ngày</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;">Sử dụng dịch vụ lũy kế từ đầu tháng</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" colspan="4">Gia hạn</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" rowspan="2">[Không sử dụng]</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" rowspan="2">[Ít]</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" rowspan="2">[Vừa]</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" rowspan="2">[Nhiều]</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" rowspan="2">[Rất nhiều]</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" rowspan="2">Doanh thu từ đăng ký mới</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" rowspan="2">Doanh thu từ gia hạn</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" rowspan="2">Doanh thu cước nội dung</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" rowspan="2">Tổng doanh thu</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" rowspan="2">Tỷ lệ % TB sử dụng dịch vụ trong ngày</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" rowspan="2">Tỷ lệ % TB sử dụng dịch vụ lũy kế từ đầu tháng</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" rowspan="2">Tăng trưởng DT so với cùng kỳ (ngày trước)</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" rowspan="2">Nguyên nhân tăng/giảm</td>
            </tr>
            <tr style="background-color: #DADADA; font-weight:bold;">
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;">Tổng Đăng ký gói cước hiện tại</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;">Đăng ký mới gói cước</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;">Đăng ký mới gói cước thành công</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;">Đăng ký mới gói cước thành công qua kênh SMS</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;">Đăng ký mới gói cước thành công qua kênh USSD</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;">Đăng ký mới gói cước thành công qua kênh IVR</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;">Đăng ký mới gói cước thành công qua kênh Web</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;">Đăng ký mới gói cước thành công qua kênh Wap</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;">Đăng ký mới gói cước thành công qua kênh App</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;">Tổng Hủy thành công (Hủy gia hạn+tự hủy)</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;">Tự hủy</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;">Hủy do gia hạn</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;">Hủy gói cước thành công qua kênh SMS</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;">Hủy gói cước thành công qua kênh USSD</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;">Hủy gói cước thành công qua kênh IVR</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;">Hủy gói cước thành công qua kênh Web</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;">Hủy gói cước thành công qua kênh Wap</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;">Hủy gói cước thành công qua kênh App</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;">Tổng số TB Sử dụng dịch vụ trong ngày</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;">Tổng số TB Sử dụng dịch vụ lũy kế từ đầu tháng</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;">Tổng TB gia hạn (thành công+ không thành công)</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;">Tổng TB Gia hạn thành công</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;">Tổng TB Gia hạn không thành công</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;">Tỷ lệ % gia hạn thành công (Gia hạn thành công/Tổng SL gia hạn)</td>
            </tr>
            <asp:Repeater runat="server" ID="rpt_Data">
                <ItemTemplate>
                    <tr style="background-color:#ffffff;">
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"><%#(Container.ItemIndex + PageIndex).ToString()%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%# ((DateTime) Eval("ReportDay")).ToString(MyUtility.MyConfig.ShortDateFormat)%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("SubTotal")%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("SubNew")%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("SubSuccess")%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("SubSMS")%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("SubUSSD")%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("SubIVR")%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("SubWEB")%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("SubWAP")%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("SubAPP")%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("UnsubSuccess")%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("UnsubSelf")%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("UnsubExtend")%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("UnSubSMS")%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("UnSubUSSD")%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("UnSubIVR")%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("UnSubWEB")%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("UnSubWAP")%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("UnSubAPP")%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("UseByDay")%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("UseByMonth")%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("RenewTotal")%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("RenewSuccess")%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("RenewFail")%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%# ((double) Eval("RenewRate")).ToString(MyUtility.MyConfig.DoubleFormat)%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("UseNot")%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("UseFew")%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("UseModerate")%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("UseMuch")%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("UserVeryMuch")%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#((double)Eval("SaleReg")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%# ((double)Eval("SaleRenew")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#((double)Eval("SaleContent")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#((double)Eval("SaleTotal")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#((double)Eval("RateUseByDay")).ToString(MyUtility.MyConfig.DoubleFormat)%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#((double)Eval("RateUseByMonth")).ToString(MyUtility.MyConfig.DoubleFormat)%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#((double)Eval("RateSaleDay")).ToString(MyUtility.MyConfig.DoubleFormat)%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("Note")%></td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                 <tr style="background-color:#F1F1F1;">
                         <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"><%#(Container.ItemIndex + PageIndex).ToString()%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%# ((DateTime) Eval("ReportDay")).ToString(MyUtility.MyConfig.ShortDateFormat)%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("SubTotal")%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("SubNew")%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("SubSuccess")%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("SubSMS")%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("SubUSSD")%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("SubIVR")%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("SubWEB")%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("SubWAP")%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("SubAPP")%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("UnsubSuccess")%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("UnsubSelf")%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("UnsubExtend")%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("UnSubSMS")%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("UnSubUSSD")%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("UnSubIVR")%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("UnSubWEB")%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("UnSubWAP")%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("UnSubAPP")%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("UseByDay")%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("UseByMonth")%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("RenewTotal")%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("RenewSuccess")%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("RenewFail")%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%# ((double) Eval("RenewRate")).ToString(MyUtility.MyConfig.DoubleFormat)%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("UseNot")%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("UseFew")%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("UseModerate")%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("UseMuch")%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("UserVeryMuch")%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#((double)Eval("SaleReg")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%# ((double)Eval("SaleRenew")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#((double)Eval("SaleContent")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#((double)Eval("SaleTotal")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#((double)Eval("RateUseByDay")).ToString(MyUtility.MyConfig.DoubleFormat)%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#((double)Eval("RateUseByMonth")).ToString(MyUtility.MyConfig.DoubleFormat)%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#((double)Eval("RateSaleDay")).ToString(MyUtility.MyConfig.DoubleFormat)%></td>
                        <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> <%#Eval("Note")%></td>
                    </tr>
                </AlternatingItemTemplate>
            </asp:Repeater>
            <tr style="padding: 5px 0 5px 0; background-color: #E2E2E2; height: 30px;">
                <td style="border-style: none solid solid none; border-width: 1px;" colspan="39">
                    <uc1:Admin_Paging ID="Admin_Paging1" runat="server" />
                </td>
            </tr>
        </tbody>
    </table>
    <div class="Div_Hidden">
        <input type="hidden" runat="server" id="hid_ListCheckAll" />
    </div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cph_Javascript" runat="server">
</asp:Content>
