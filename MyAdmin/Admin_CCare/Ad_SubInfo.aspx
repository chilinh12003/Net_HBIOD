<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ad_SubInfo.aspx.cs" Inherits="MyAdmin.Admin_CCare.Ad_SubInfo" %>

<%@ Register Src="../Admin_Control/Admin_Paging_VNP.ascx" TagName="Admin_Paging_VNP" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <title>VAS151</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="author" content="">
    <!-- Le styles -->
    <link href="../CSS/bootstrap.css" rel="stylesheet" type="text/css" />
    <style>
        body {
            padding-top: 60px; /* 60px to make the container go all the way to the bottom of the topbar */
        }
    </style>
    <link href="http://vinabox.vinaphone.com.vn/static/css/bootstrap-responsive.css" rel="stylesheet">
    <style type="text/css">
        .sidebar-nav {
            padding: 9px 0;
        }

        .navbar .brand {
            padding: 5px 20px !important;
        }

        .hero-unit {
            padding: 9px !important;
        }

        li.active, p.vas-header {
            background-color: #0088CC;
            color: #FFFFFF;
        }

        p.vas-header {
            padding-left: 5px;
        }

        .hero-unit1 {
            padding: 9px !important;
        }

        .local-active {
            background-color: #F5F5F5;
            color: #C8A999;
        }

        .local-disable {
            background-color: #F5F5F5;
            color: #C8A999;
        }
    </style>
    <!-- Le HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>      <script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>    <![endif]-->
</head>
<body>
    <form id="form1" runat="server">
        <div class="navbar navbar-inverse navbar-fixed-top">
            <div class="navbar-inner">
                <div class="container-fluid">
                    <a data-target=".nav-collapse" data-toggle="collapse" class="btn btn-navbar"><span class="icon-bar"></span><span class="icon-bar"></span><span class="icon-bar"></span></a><a class="brand" href="javascript:void(0);">
                        <img src="http://vinabox.vinaphone.com.vn/static/web/images/vinabox_logo.png" alt="VINABOX" width="41" height="33" />CSKH DỊCH VỤ IOD</a>
                </div>
            </div>
        </div>
        <div class="container-fluid">
            <div class="row-fluid">
                <div class="span12">
                    <div class="alert alert-error" style="display: none">
                    </div>
                    <div class="hero-unit">
                        <p class="vas-header">
                            Thông tin thuê bao
                        <%=MSISDN %>
                        </p>
                        <table class="table table-striped">
                            <tr>
                                <th>Số điện Thoại
                                </th>
                                <th>Gói dịch vụ
                                </th>
                                <th>Tình trạng
                                </th>
                                <th>Ngày đầu tiên sử dụng
                                </th>
                                <th>Ngày cuối cùng sử dụng
                                </th>
                                <th>Trừ tiền gần nhất</th>
                            </tr>
                            <asp:Repeater runat="server" ID="rpt_Sub">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <%#(Container.ItemIndex + PageIndex_Sub).ToString()%>
                                        </td>
                                        <td><%#Eval("MSISDN")%></td>
                                        <td><%#Eval("ServiceName")%></td>
                                        <th>
                                            <%#Eval("StatusName")%>
                                        </th>
                                        <td>
                                            <%#Eval("FirstDate") == DBNull.Value ? string.Empty : ((DateTime)Eval("FirstDate")).ToString(MyUtility.MyConfig.LongDateFormat)%>
                                        </td>
                                        <td>
                                            <%#Eval("LastUpdate") == DBNull.Value ? string.Empty : ((DateTime)Eval("LastUpdate")).ToString(MyUtility.MyConfig.LongDateFormat)%>
                                        </td>
                                        <td>
                                            <%#Eval("ChargeDate") == DBNull.Value ? string.Empty : ((DateTime)Eval("ChargeDate")).ToString(MyUtility.MyConfig.LongDateFormat)%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                    <div class="hero-unit">
                        <p class="vas-header">
                            Lịch sử mua nội dung của thuê bao
                        <%=MSISDN %>
                        </p>
                        <table class="table table-striped">
                            <tr>
                                <th>STT
                                </th>
                                <th>Thời gian giao dịch</td>
                                <th>Loại giao dịch
                                </th>
                                    <th>Tên gói cước
                                    </th>
                                    <th>Trạng thái
                                    </th>
                                    <th>Kênh thực hiện
                                    </th>
                                    <th>Cước phí
                                    </th>
                            </tr>
                            <asp:Repeater runat="server" ID="rpt_BuyContent">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <%#(Container.ItemIndex + PageIndex_BuyContent).ToString()%>
                                        </td>
                                        <td>
                                            <%#Eval("ChargeDate") == DBNull.Value ? string.Empty : ((DateTime)Eval("ChargeDate")).ToString(MyUtility.MyConfig.LongDateFormat)%>
                                        </td>
                                        <td>Mua dữ kiện
                                        </td>
                                        <td>
                                            <%#Eval("ServiceName")%>
                                        </td>
                                        <td>
                                            <%#Eval("ChargeStatusName")%>
                                        </td>
                                        <td>
                                            <%#Eval("ChannelTypeName")%>
                                        </td>
                                        <td>
                                            <%#Eval("Price")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                        <uc1:Admin_Paging_VNP ID="Admin_Paging_VNP_BuyContent" runat="server" />
                    </div>

                    <div class="hero-unit">
                        <p class="vas-header">
                            Lịch sử MO/MT của thuê bao
                        <%=MSISDN %>
                        </p>
                        <table class="table table-striped">
                            <tr>
                                <th>STT
                                </th>
                                <th>Thời gian nhận
                                <th>MO
                                </th>
                                    <th>Trạng thái
                                    </th>
                                    <th>Đầu số
                                    </th>
                                    <th>Thời gian gửi
                                    </th>
                                    <th width="40%">MT
                                    </th>
                                    <th>Trạng thái
                                    </th>
                                    <th>Cước phí
                                    </th>
                            </tr>
                            <asp:Repeater runat="server" ID="rpt_MOLog">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <%#(Container.ItemIndex + PageIndex_MOLog).ToString()%>
                                        </td>
                                        <td>
                                            <%#Eval("ReceiveDate") == DBNull.Value ? string.Empty : ((DateTime)Eval("ReceiveDate")).ToString(MyUtility.MyConfig.LongDateFormat)%>
                                        </td>
                                        <td>
                                            <%#Eval("MO")%>
                                        </td>
                                        <td>Đã xử lý
                                        </td>
                                        <td>9315
                                        </td>
                                        <td>
                                            <%#Eval("SendDate") == DBNull.Value ? string.Empty : ((DateTime)Eval("SendDate")).ToString(MyUtility.MyConfig.LongDateFormat)%>
                                        </td>
                                        <td>
                                            <%#Eval("MT")%>
                                        </td>
                                        <td>Đã gửi
                                        </td>
                                        <td>0
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                        <uc1:Admin_Paging_VNP ID="Admin_Paging_VNP_MoLog" runat="server" />
                        <p>
                            &nbsp;
                        </p>
                    </div>
                    <!--/span-->
                </div>
                <p>
                    <!--/row-->
                </p>
                <footer>        <p>&copy; HB 2014</p>      </footer>
            </div>
            <!--/.fluid-container-->
            <!-- Le javascript    ================================================== -->
            <script src="http://vinabox.vinaphone.com.vn/static/js/jquery-1.7.2.min.js"></script>
            <script src="http://vinabox.vinaphone.com.vn/static/js/bootstrap.min.js"></script>
    </form>
</body>
</html>
