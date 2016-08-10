<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CourierNumber.aspx.cs" Inherits="Logistics.CourierNumber" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head> 
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title>���ҵ��Ԥ¼�嵥</title>
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
        font-family: "΢���ź�", Arial, Helvetica, sans-serif;
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
        font-family: "΢���ź�", Arial, Helvetica, sans-serif;
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
             var state = $("#state").val();
             var userid = $("#userid").val();
             var par = "";
             if (expressType.length >= 1) {
                 par = "?expressType=" + expressType;
             } else {
                 alert("��ѡ��Ҫ��ѯ�Ŀ�ݣ�");
                 return false;
             }

             if (state.length >= 1) {
                 par += "&state=" + state;
             }
             if (userid.length >= 1) {
                 par += "&userid=" + userid;
             }

             location.href = par;
         });
         $("#add").click(function () {

             location.href = "CourierNumberAdd.aspx";
         });

     })
     function DeleteItRow(id) {


         $.post("delete.aspx", { id: id }, function (data) {
             if (data == "1") {
                 alert("ɾ���ɹ���");
                 location.reload();
             }
         })
     }
 </script>
 
</head>

<body >
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td align='left'>&nbsp;</td>
  </tr>
  <tr>
    <td height="50" align='left' bgcolor="#48a1fb"><span class='kindc2'>Ԥ���ݺ�</span></td>
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
<td height="110"  bgcolor="#f0f0f0"">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
	  <tr>
	    <td width="837" height="50" align="left">������
            <span id="oemskind">
            <select  size=1 name='cemskind' id='cemskind'>
                <option value="">��ѡ��</option>
                <% foreach (var e in emsList)
                   { %>
                <% if (e.cemskind.Equals(expressType))
                   { %>
                 <option selected="selected"  value="<%=e.cemskind %>"><%=e.cemskind %></option>
                <%}
                   else
                   { %>
                <option value="<%=e.cemskind %>"><%=e.cemskind %></option>
                     <%} %>
                <%} %>
            </select> 
               <span style="padding-left:30px"></span> ʹ��״̬��
            <span id="oemskind">
            <select  size=1 name='state' id='state'>
                <option value="">��ѡ��</option>
                <option 
                <% if (1 == states)
                   { %>
                    selected="selected"
                <%} %>value="1">
                    ��ʹ��</option> 
                <option 
               <% if (0 == states)
                  { %>
                    selected="selected"
                <%} %> value="0">
                    δʹ��</option> 
            </select> 
	          </span>  
                <span style="padding-left:30px"></span> ʹ����ID��
            <span id="oemskind">
                <input type="text" id="userid" value="<%=updateUser %>" />
	          </span>  
	  <span style="padding-left:30px"></span>  <button type='button' id="submit"  class='btn btn-primary'>��ѯ</button>
            <button type='button' id="add"  class='btn btn-primary'>Ԥ�浥��</button>
	    </td>
	    </tr>
    </table> 
  </td>
 </tr>
      <tr>
    <td align="left">&nbsp;</td>
  </tr>
    <tr>
    <td align="left">
        <table width='100%' border='0'align='left' cellpadding='2' cellspacing='1'   bgcolor="#CCCCCC" id="slist">
            <tr class="recPreInputListTitle"> 
                <td  align="center">������</td>
                <td  align="center">��Ч����</td> 
                <td  align="center">��ʹ������</td> 
                <td  align="center">����</td>  
            </tr>
            <%foreach (var item in countList) {%>
            <tr align="center" class="recPreInputListEven" >
                <td  align="center"><%=item.expressType %></td>
                <td  align="center"><%=item.notUsed %></td> 
                <td  align="center"><%=item.alreadyUsed %></td> 
                <td  align="center"><%=(item.notUsed+item.alreadyUsed) %></td>  
            </tr>
            <%} %>
        </table>
    </td>
  </tr>
  <tr>
    <td align="left">&nbsp;</td>
  </tr>
  <tr>
    <td align="left">
        <table width='100%' border='0'align='left' cellpadding='2' cellspacing='1'   bgcolor="#CCCCCC" id="Tlist">
            <tr class="recPreInputListTitle"> 
                <td  align="center">��ݺ���</td>
                <td  align="center">��ݺ�������˾</td> 
                <td  align="center">״̬</td> 
                <td  align="center">ʹ��ID</td> 
                <td  align="center">ʹ������</td>
                <td align="left">����</td> 
            </tr> 
            <%foreach (var s in list)
              {%>
        <tr align="center" class="recPreInputListEven" id="tr_11085"> 
            <td align="center"><%=s.courierNumber %></td>
            <td align="center"><%=s.expressType %></td>
            <td  align="center"><%=s.state==0?"δʹ��":"��ʹ��" %></td>  
            <td  align="center"><%=s.updateUser %></td>
            <td  align="center"><%=s.updateDate==null?"":s.updateDate.Value.ToString("yyyy-MM-dd") %></td> 
             <td align="left"><a href="javascript:DeleteItRow(<%=s.id %>);"><img src="/EMS_MODEL/pic/login_09.jpg" width="16" height="18"></a> </td>
      </tr> 
            <%} %>
    </table></td>
  </tr>
  <tr align="left">
    <td height="40">(��<font color=green><%=rowCount %></font>��)<font color=red><%=page %></font>/<%=pageCount %>ҳ
        <a class="page" href="?page=1<%=par %>">��ҳ</a> 
         <a class="page" href="?page=<%=page>1?page-1:1 %><%=par %>">��һҳ</a>
         <a class="page" href="?page=<%=page>=pageCount?pageCount:page+1 %><%=par %>">��һҳ</a>
         <a class="page" href="?page=<%=pageCount%><%=par %>">ĩҳ</a>
    </td>
  </tr> 
</table>  
</body>
</html>
