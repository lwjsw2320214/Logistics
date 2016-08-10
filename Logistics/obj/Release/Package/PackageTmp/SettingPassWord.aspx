<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SettingPassWord.aspx.cs" Inherits="Logistics.SettingPassWord" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head> 
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title>客户端登录管理</title>
<link href="/EMS_MODEL/css/main.css" rel="stylesheet" type="text/css"/>
<link href="/EMS_MODEL/css/exCSS.css" rel="stylesheet" type="text/css" /> 
 <script type="text/javascript" src="EMS_MODEL/jquery-1.12.3.min.js"></script>

<style type="text/css">
    <!--
    #oModel {
        float: left;
        margin-right: 20px;
    }

    #oemskindType {
        float: left;
        margin-right: 20px;
    }

    input {
        font-family: "微软雅黑", Arial, Helvetica, sans-serif;
        height: 18px;
        font-size: 12px;
        border-top-width: 0px;
        border-right-width: 0px;
        border-bottom-width: 0px;
        border-left-width: 0px;
        border-top-style: none;
        border-right-style: none;
        border-bottom-style: none;
        border-left-style: none;
    }

    .Wdate {
        font-family: "微软雅黑", Arial, Helvetica, sans-serif;
        font-size: 12px;
        height: 20px;
        line-height: 16px;
        border-top-width: 1px;
        border-right-width: 1px;
        border-bottom-width: 1px;
        border-left-width: 1px;
        border-top-style: solid;
        border-right-style: solid;
        border-bottom-style: solid;
        border-left-style: solid;
        border-top-color: #666666;
        border-right-color: #CCCCCC;
        border-bottom-color: #CCCCCC;
        border-left-color: #666666;
        width: 100px;
        background-image: url(/EMS_MODEL/My97DatePicker/skin/datePicker.gif);
        background-repeat: no-repeat;
        background-position: right;
        cursor: pointer;
    }

    .myinput {
        font-family: Arial, Helvetica, sans-serif;
        font-size: 12px;
        height: 20px;
        line-height: 20px;
        border-top-width: 1px;
        border-right-width: 1px;
        border-bottom-width: 1px;
        border-left-width: 1px;
        border-top-style: solid;
        border-right-style: solid;
        border-bottom-style: solid;
        border-left-style: solid;
        border-top-color: #666666;
        border-right-color: #CCCCCC;
        border-bottom-color: #CCCCCC;
        border-left-color: #666666;
        width: 225px;
    }

    .myinput1 {
        font-family: Arial, Helvetica, sans-serif;
        font-size: 12px;
        height: 20px;
        line-height: 20px;
        border-top-width: 1px;
        border-right-width: 1px;
        border-bottom-width: 1px;
        border-left-width: 1px;
        border-top-style: solid;
        border-right-style: solid;
        border-bottom-style: solid;
        border-left-style: solid;
        border-top-color: #666666;
        border-right-color: #CCCCCC;
        border-bottom-color: #CCCCCC;
        border-left-color: #666666;
        width: 100px;
    }

    .page {
        border: 1px solid #CCCCCC;
        text-decoration: none;
        color: #000;
        padding: 5px 10px;
        margin: 0px 5px;
    }

        .page:hover {
            border: 1px solid #75d6f6;
            color: #75d6f6;
        }

    -->
</style>
 <script type="text/javascript">

     $(function () {

         $("#submit").click(function () {
             var username = $("#username").val();
             var par = "ClienPassWord.aspx";
             if (username.length >= 1) {
                 par = "?username=" + username;
             }
             location.href = par;
         });

     })
 </script>
 
</head>

<body >
    <form method="post" action="/SettingPassWord.aspx" id="form1">
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td align='left'>&nbsp;</td>
  </tr>
  <tr>
    <td height="50" align='left' bgcolor="#48a1fb"><span class='kindc2'>客户端用户管理</span></td>
  </tr>
  <tr>
    <td >&nbsp;</td>
  </tr> 
</table> 
<table border='0' cellspacing='0' class='recPreInputListTable'>
<tr>
  <td>&nbsp;</td>
</tr> 
    <tr>
        <td>
            <table  width='100%' border='0'align='left' cellpadding='2' cellspacing='1'   bgcolor="#CCCCCC" id="Tlist">
                <tr  align="center" class="recPreInputListEven" id="tr_11085">
        <td style="text-align:right">用户名：</td>
        <td  style="text-align:left"><input type="text" style="width:200px" readonly class="myinput1" name="username" value="<%=username %>" /> </td>
    </tr>
    <tr  align="center" class="recPreInputListEven" id="tr_11085">
        <td style="text-align:right">密码：</td>
        <td style="text-align:left"><input type="password" style="width:200px" maxlength="18"  class="myinput1" name="password"  value=""/></td>
    </tr>
    <tr>
        <td colspan="2" style="text-align:center"><button type="submit">设置</button> </td> 
    </tr>
            </table>
        </td>
    </tr>
</table>
</form> 
</body>
</html>
