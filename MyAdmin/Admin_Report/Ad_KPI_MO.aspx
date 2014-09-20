<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.Master" AutoEventWireup="true" CodeBehind="Ad_KPI_MO.aspx.cs" Inherits="MyAdmin.Admin_Report.Ad_KPI_MO" %>


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
     <table border="0" cellpadding="5" cellspacing="0" style="margin-right: 5px; text-align: center; vertical-align: text-top; border-style: solid none none solid; border-width: 1px; border-color: #000000;">
        <tbody>
            <tr style="background-color: #DADADA; font-weight:bold;">
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" rowspan="2">STT</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" rowspan="2">Ngày báo cáo</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" colspan="5">Sản lượng chi tiết</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" colspan="5">Sản lượng tổng</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" colspan="5">Tần suất sử dụng</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" colspan="2">Đánh giá</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" rowspan="2">Ghi chú</td>
            </tr>
           <tr style="background-color: #DADADA; font-weight:bold;">
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000; width:100px;">Cú pháp</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;">Số lượng thuê bao nhắn tin đúng cú pháp</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;">Sản lượng MO đúng cú pháp</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;">Sản lượng MO đúng cú pháp &amp; trừ tiền thành công</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;">Doanh thu (VNĐ)</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;">Tổng sản lượng MO</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;">Tổng sản lượng MO đúng cú pháp</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;">Tổng số lượng thuê bao nhắn tin đúng cú pháp</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;">Trung bình số lượng MO đúng cú pháp/thuê bao</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;">Tổng doanh thu (VNĐ)</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;">Số lượng thuê bao nhắn 1 MO đúng cú pháp lũy kế từ khi khai trương DV</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;">Số lượng thuê bao nhắn 2 - 3 MO đúng cú pháp lũy kế từ khi khai trương DV</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;">Số lượng thuê bao nhắn 4 - 5 MO đúng cú pháp lũy kế từ khi khai trương DV</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;">Số lượng thuê bao nhắn 5 - 10 MO đúng cú pháp lũy kế từ khi khai trương DV</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;"> Số lượng thuê bao nhắn > 10 MO đúng cú pháp lũy kế từ khi khai trương DV</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;">Tăng trưởng số lượng thuê bao sử dụng dịch vụ so với ngày trước (%)</td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;">Tăng trưởng DT so với ngày trước (%)</td>
            </tr>
               <asp:Repeater runat="server" ID="rpt_Data">
                <ItemTemplate>
            <tr style="background-color:#ffffff;">
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" rowspan="6"><%#(Container.ItemIndex + PageIndex).ToString()%></td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" rowspan="6"><%# ((DateTime)Eval("ReportDay")).ToString(MyUtility.MyConfig.ShortDateFormat)%></td>
                <%# this.GetValue((DateTime)Eval("ReportDay"),MyHBIOD.Report.RP_MO.MOType.Register)%>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" rowspan="6"><%# ((double)Eval("MOTotal")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" rowspan="6"><%# ((double)Eval("MOCorrectTotal")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" rowspan="6"><%# ((double)Eval("SubCorrectTotal")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" rowspan="6"><%# ((double)Eval("MOCorrectRate")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" rowspan="6"><%# ((double)Eval("MOSaleTotal")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" rowspan="6"><%# ((double)Eval("Sub1Correct")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" rowspan="6"><%# ((double)Eval("Sub2Correct")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" rowspan="6"><%# ((double)Eval("Sub4Correct")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" rowspan="6"><%# ((double)Eval("Sub5Correct")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" rowspan="6"><%# ((double)Eval("Sub10Correct")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" rowspan="6"><%# ((double)Eval("RateSubByDay")).ToString(MyUtility.MyConfig.DoubleFormat)%></td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" rowspan="6"><%# ((double)Eval("RateSaleByDay")).ToString(MyUtility.MyConfig.DoubleFormat)%></td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" rowspan="6"><%#Eval("Note")%></td>
            </tr>
             <tr style="background-color:#ffffff;">
               <%# this.GetValue((DateTime)Eval("ReportDay"),MyHBIOD.Report.RP_MO.MOType.Deregister)%>
            </tr>
            <tr style="background-color:#ffffff;">
               <%# this.GetValue((DateTime)Eval("ReportDay"),MyHBIOD.Report.RP_MO.MOType.BuyContent)%>
            </tr>
            <tr style="background-color:#ffffff;">
               <%# this.GetValue((DateTime)Eval("ReportDay"),MyHBIOD.Report.RP_MO.MOType.Answer)%>
            </tr>
            <tr style="background-color:#ffffff;">
               <%# this.GetValue((DateTime)Eval("ReportDay"),MyHBIOD.Report.RP_MO.MOType.Other)%>
            </tr>
            <tr style="background-color:#ffffff;">
               <%# this.GetValue((DateTime)Eval("ReportDay"),MyHBIOD.Report.RP_MO.MOType.Invalid)%>
            </tr>
             </ItemTemplate>
                <AlternatingItemTemplate>
                <tr style="background-color:#F1F1F1;">
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" rowspan="6"><%#(Container.ItemIndex + PageIndex).ToString()%></td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" rowspan="6"><%# ((DateTime)Eval("ReportDay")).ToString(MyUtility.MyConfig.ShortDateFormat)%></td>
                <%# this.GetValue((DateTime)Eval("ReportDay"),MyHBIOD.Report.RP_MO.MOType.Register)%>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" rowspan="6"><%# ((double)Eval("MOTotal")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" rowspan="6"><%# ((double)Eval("MOCorrectTotal")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" rowspan="6"><%# ((double)Eval("SubCorrectTotal")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" rowspan="6"><%# ((double)Eval("MOCorrectRate")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" rowspan="6"><%# ((double)Eval("MOSaleTotal")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" rowspan="6"><%# ((double)Eval("Sub1Correct")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" rowspan="6"><%# ((double)Eval("Sub2Correct")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" rowspan="6"><%# ((double)Eval("Sub4Correct")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" rowspan="6"><%# ((double)Eval("Sub5Correct")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" rowspan="6"><%# ((double)Eval("Sub10Correct")).ToString(MyUtility.MyConfig.IntFormat)%></td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" rowspan="6"><%# ((double)Eval("RateSubByDay")).ToString(MyUtility.MyConfig.DoubleFormat)%></td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" rowspan="6"><%# ((double)Eval("RateSaleByDay")).ToString(MyUtility.MyConfig.DoubleFormat)%></td>
                <td style="border-style: none solid solid none; border-width: 1px; border-color: #000000;" rowspan="6"><%#Eval("Note")%></td>
            </tr>
             <tr style="background-color:#F1F1F1;">
               <%# this.GetValue((DateTime)Eval("ReportDay"),MyHBIOD.Report.RP_MO.MOType.Deregister)%>
            </tr>
            <tr style="background-color:#F1F1F1;">
               <%# this.GetValue((DateTime)Eval("ReportDay"),MyHBIOD.Report.RP_MO.MOType.BuyContent)%>
            </tr>
            <tr style="background-color:#F1F1F1;">
               <%# this.GetValue((DateTime)Eval("ReportDay"),MyHBIOD.Report.RP_MO.MOType.Answer)%>
            </tr>
            <tr style="background-color:#F1F1F1;">
               <%# this.GetValue((DateTime)Eval("ReportDay"),MyHBIOD.Report.RP_MO.MOType.Other)%>
            </tr>
            <tr style="background-color:#F1F1F1;">
               <%# this.GetValue((DateTime)Eval("ReportDay"),MyHBIOD.Report.RP_MO.MOType.Invalid)%>
            </tr>
                </AlternatingItemTemplate>
            </asp:Repeater>
            <tr style="padding: 5px 0 5px 0; background-color: #E2E2E2; height: 30px;">
                <td style="border-style: none solid solid none; border-width: 1px;" colspan="20">
                    <uc1:Admin_Paging ID="Admin_Paging1" runat="server" />
                </td>
            </tr>          
        </tbody>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cph_Javascript" runat="server">
</asp:Content>
