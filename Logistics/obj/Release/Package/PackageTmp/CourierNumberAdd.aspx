<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CourierNumberAdd.aspx.cs" Inherits="Logistics.CourierNumberAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head> 
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title>快件业务预录清单</title>
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
             var expressType = $("#cemskind").val();
             if (expressType.length <= 1) {  
                 alert("请选择要查询的快递！");
                 return false;
             }

             var order = $("#numberlist").val(); 
             orderarray = order.split("\n");
             var o = "";
             $(orderarray).each(function (index, value) {
                 if (value.trim().length > 0) { 
                     o+=value+","
                 }
             })
             if (o.length > 0) {
                 o = o.substring(0, o.length - 1);
                 $("#hnumberlist").val(o);
             } else {
                 alert("请输入正确的快递号");
                 return false;
             }
         }); 
     })

 </script>
 
</head>

<body >
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td align='left'>&nbsp;</td>
  </tr>
  <tr>
    <td height="50" align='left' bgcolor="#48a1fb"><span class='kindc2'>预存快递号</span></td>
  </tr>
  <tr>
    <td >&nbsp;</td>
  </tr> 
</table>  
    <form method="post" action="" id="form">
    <table  width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
    <td align='left'>&nbsp;</td>
  </tr>
  <tr>
    <td align='left' style="padding-left:20PX; padding-right:20px"> 
        <table  width="100%" border="0" cellspacing="1" cellpadding="0" style="background:#f0f0f0">
            <tr style=" height:40px">
                <td style="padding-left:20px; width:100px">快递类别</td>
                <td >
                    <select  size=1 name='cemskind' id='cemskind'>
                <option value="">请选择</option>
                <% foreach (var e in emsList){ %>
                 <option value="<%=e.cemskind %>"><%=e.cemskind %></option>
                <%} %>
            </select> 
                </td>
            </tr>
            <tr>
                <td style="padding-left:20px" >快递号码</td>
                <input type="hidden" name="numberlist" id="hnumberlist" />
                <td ><textarea id="numberlist" rows="20" style="height:400px; width:300px"></textarea></td>
            </tr>
            <tr> 
                <td></td>
	    <td  height="50" align="left">
            <span id="oemskind"> 
	          </span>  
	    <button type="submit" id="submit"  class='btn btn-primary'>保存</button> 
            <button type="reset"  class='btn btn-primary'>重置</button> 
	    </td>
                
	    </tr>
        </table>
    </td>
  </tr>
  <tr>
    <td >&nbsp;</td>
  </tr> 
    </table>
        </form>
</body>
</html>