<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.Master" AutoEventWireup="true" CodeBehind="Ad_CheckSubInfo.aspx.cs" Inherits="MyAdmin.Admin_CCare.Ad_CheckSubInfo" %>

<%@ Register Src="../Admin_Control/Admin_Paging.ascx" TagName="Admin_Paging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_Header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Tools" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_ToolBox" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cph_Search" runat="server">
    <label>
        Số điện thoại:</label>
    <input type="text" runat="server" id="tbx_SearchContent" />   
    <asp:Button runat="server" ID="btn_Search" Text="Tìm kiếm" OnClick="btn_Search_Click" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cph_Content" runat="server">
     <table class="Data" border="0" cellpadding="0" cellspacing="0">
        <tbody>
            <tr class="Table_Header" style="height:40px;">
                <th class="Table_TL"></th>
                <th width="10">STT
                </th>
                <th>Số điện Thoại</th>
                <th>Gói dịch vụ</th>
                <th>Tình trạng</th>
                <th>Ngày đầu tiên sử dụng</th>
                <th>Ngày cuối cùng sử dụng</th>
                <th>Trừ tiền gần nhất</th>
                <th>Trừ tiền thành công gần nhất</th>
                <th>MO trong ngày</th>
                <th>MT trong ngày</th>
                <th>Tổng MO</th>
                <th>Tổng MT</th>
                <th>Mua trong ngày</th>
                <th>Mua TC trong ngày</th>
                <th>Tổng mua</th>
                <th>Tổng mua thành công</th>
                <th class="Table_TR"></th>
            </tr>
            <asp:Repeater runat="server" ID="rpt_Data">
                <ItemTemplate>
                    <tr class="Table_Row_1">
                        <td class="Table_ML_1"></td>
                        <td>
                            <%#(Container.ItemIndex + PageIndex).ToString()%>
                        </td>
                        <td>
                            <%#Eval("MSISDN")%>
                        </td>
                        <td>
                            <%#Eval("ServiceName")%>
                        </td>
                        <td>
                            <%#Eval("StatusName")%>
                        </td>
                         <td>
                            <%#Eval("FirstDate") == DBNull.Value ? string.Empty : ((DateTime)Eval("FirstDate")).ToString(MyUtility.MyConfig.LongDateFormat)%>
                        </td>
                        <td>
                            <%#Eval("LastUpdate") == DBNull.Value ? string.Empty : ((DateTime)Eval("LastUpdate")).ToString(MyUtility.MyConfig.LongDateFormat)%>
                        </td>
                        <td>
                            <%#Eval("ChargeDate") == DBNull.Value ? string.Empty : ((DateTime)Eval("ChargeDate")).ToString(MyUtility.MyConfig.LongDateFormat)%>
                        </td>
                         <td>
                            <%#Eval("ChargeSuccessDate") == DBNull.Value ? string.Empty : ((DateTime)Eval("ChargeSuccessDate")).ToString(MyUtility.MyConfig.LongDateFormat)%>
                        </td>
                        <td>
                             <%#Eval("MOByDay")%>
                        </td>
                        <td>
                            <%#Eval("MTByDay")%>
                        </td>
                        <td>
                            <%#Eval("TotalMO")%>
                        </td>
                        <td>
                            <%#Eval("TotalMT")%>
                        </td>

                        <td>
                             <%#Eval("ChargeByDay")%>
                        </td>
                        <td>
                            <%#Eval("ChargeSuccessByDay")%>
                        </td>
                        <td>
                            <%#Eval("TotalCharge")%>
                        </td>
                        <td>
                            <%#Eval("TotalChargeSuccess")%>
                        </td>
                        <td class="Table_MR_1"></td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="Table_Row_2">
                        <td class="Table_ML_2"></td>
                        <td>
                            <%#(Container.ItemIndex + PageIndex).ToString()%>
                        </td>
                       <td>
                            <%#Eval("MSISDN")%>
                        </td>
                        <td>
                            <%#Eval("ServiceName")%>
                        </td>
                        <td>
                            <%#Eval("StatusName")%>
                        </td>
                         <td>
                            <%#Eval("FirstDate") == DBNull.Value ? string.Empty : ((DateTime)Eval("FirstDate")).ToString(MyUtility.MyConfig.LongDateFormat)%>
                        </td>
                        <td>
                            <%#Eval("LastUpdate") == DBNull.Value ? string.Empty : ((DateTime)Eval("LastUpdate")).ToString(MyUtility.MyConfig.LongDateFormat)%>
                        </td>
                        <td>
                            <%#Eval("ChargeDate") == DBNull.Value ? string.Empty : ((DateTime)Eval("ChargeDate")).ToString(MyUtility.MyConfig.LongDateFormat)%>
                        </td>
                         <td>
                            <%#Eval("ChargeSuccessDate") == DBNull.Value ? string.Empty : ((DateTime)Eval("ChargeSuccessDate")).ToString(MyUtility.MyConfig.LongDateFormat)%>
                        </td>
                        <td>
                             <%#Eval("MOByDay")%>
                        </td>
                        <td>
                            <%#Eval("MTByDay")%>
                        </td>
                        <td>
                            <%#Eval("TotalMO")%>
                        </td>
                        <td>
                            <%#Eval("TotalMT")%>
                        </td>

                        <td>
                             <%#Eval("ChargeByDay")%>
                        </td>
                        <td>
                            <%#Eval("ChargeSuccessByDay")%>
                        </td>
                        <td>
                            <%#Eval("TotalCharge")%>
                        </td>
                        <td>
                            <%#Eval("TotalChargeSuccess")%>
                        </td>
                        <td class="Table_MR_2"></td>
                    </tr>
                </AlternatingItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>
    <div class="Table_Footer">
        <div class="Table_BL">
            <uc1:Admin_Paging ID="Admin_Paging1" runat="server" />
        </div>
        <div class="Table_BR">
        </div>
    </div>
    <div class="Div_Hidden">
        <input type="hidden" runat="server" id="hid_ListCheckAll" />
    </div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cph_Javascript" runat="server">
</asp:Content>


