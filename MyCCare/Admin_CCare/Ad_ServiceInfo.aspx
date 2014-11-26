<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.Master" AutoEventWireup="true" CodeBehind="Ad_ServiceInfo.aspx.cs" Inherits="MyCCare.Admin_CCare.Ad_ServiceInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_Header" runat="server">

    <style type="text/css">
        table.MsoNormalTable {
            line-height: 150%;
            font-size: 14.0pt;
            font-family: "Times New Roman","serif";
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Content" runat="server">
    <div id="menutabs1" class='mt10'>
        <a class="" href="Ad_SubInfo.aspx">
            <img class='icon1' src='../images/icon1.png'><span>Tra cứu thuê bao</span></a>
        <a class="" href="Ad_HistoryCharge.aspx">
            <img class='icon2' src='../images/icon2.png'><span>Tra cứu sử dụng dịch vụ</span></a>
        <a class="" href="Ad_ResendMT.aspx">
            <img class='icon3' src='../images/icon3.png'><span>Cài đặt dịch vụ</span></a>
        <a class="selected" href="Ad_ServiceInfo.aspx">
            <img class='icon4' src='../images/icon4.png'><span>Thông tin dịch vụ</span></a>
    </div>
    <div class='p10 bor'>
        <h4 class='titlecheck'>Mô tả dịch vụ:</h4>
        <p>
            Dịch vụ Truy vấn thông tin là một gói dịch vụ cung cấp các thông tin, kiến thức và giải trí cho các khách hàng của VinaPhone bao gồm các nội dung:
            <br />
            - Gia đình
            <br />
            - Sức khỏe
            <br />
            - Nôị trợ 
            <br />
            - Tình yêu
            <br />
            - Lịch vạn sự
            <br />
            - Sao Hoàng đạo
            <br />

        </p>
        <br />
        <h4 class='titlecheck'>Cách sử dụng dịch vụ:</h4>
        <p>- Để sử dụng dịch vụ, khách hàng truy cập vào menu dịch vụ IOD trên Simcard, lựa chọn mục thông tin cần truy vấn, Simcard sẽ tự động sinh ra tin nhắn SMS yêu cầu truy vấn nội dung và gửi tới Server dịch vụ.</p>
        <br />
        <p>- Sau khi Server dịch vụ nhận được yêu cầu bằng SMS, căn cứ theo cú pháp và các thông tin kèm theo tin nhắn, Server dịch vụ sẽ gửi trả nội dung khách hàng truy vấn bằng sms về máy điện thoại di động của khách hàng.</p>
        <br />
        <p>- Đối với dịch vụ Truy vấn thông tin "Tư vấn", khách hàng lựa chọn thông tin mong muốn trong menu sim. Sau đó, hệ thống sẽ gửi về thông tin trả lời bằng tin nhắn (SMS)</p>
        <h4 class='titlecheck'>Giá cước:</h4>
        <p>
            1000 đồng/bản tin yêu cầu (đã bao gồm thuế VAT)
        </p>
        <br />

    </div>
</asp:Content>
