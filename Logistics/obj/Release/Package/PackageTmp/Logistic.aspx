<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logistic.aspx.cs" Inherits="Logistics.Logistic" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <globalization requestEncoding="gb2312" responseEncoding="gb2312" />
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title>���ҵ��Ԥ¼�嵥</title>
<link href="/EMS_MODEL/css/main.css" rel="stylesheet" type="text/css"/>
<link href="/EMS_MODEL/css/exCSS.css" rel="stylesheet" type="text/css" />
<script language="javascript" type="text/javascript" src="/EMS_MODEL/My97DatePicker/WdatePicker.js"></script>
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
    
        border:1px solid #CCCCCC;
        text-decoration:none;
        color:#000;
        padding:5px 10px;
        margin:0px 5px;
    }
        .page:hover {
        border:1px solid #75d6f6;
        color:#75d6f6;
        }

-->
</style>

<script type="text/javascript">

    var exArray=new Array();

    document.onkeydown = keyDownSearch;

    function keyDownSearch(e) {
        // ����FF��IE��Opera    
        var theEvent = e || window.event;
        var code = theEvent.keyCode || theEvent.which || theEvent.charCode;
        if (code == 13) {
            var expcode = $("#expnumber").val();
            if (expcode.length == 0) {
                return false;
            }
            $.get("ajax.aspx", { expcode: expcode }, function (data) {
                var array = data.split(",")
                
                if (data != "0") {
                    var c = "";
                    for (var i = 0; i < exArray.length; i++) { 
                        if (exArray[i].indexOf(array[1]) > -1) {
                            c = exArray[i]; 
                        }   
                    } 
                    if (array[0] =="0"||c.length<=0) {
                        alert("�ö���û�в�ѯ��������ȷ���룡");
                        return false;
                    }
                    var str = "/cgi-bin/GInfo.dll?EmsPrintBatch&w=au-express&ntype=0&ntable=1&cmodel=" + c + "&iid=" + array[0];
                    window.open(str, "_blank");
                    $("#expnumber").val("");
                }
            })
            //for (var i = 0; i < exArray.length; i++) {
            //    alert(exArray[i]);
            //} 
            //���崦����
            return false;
        }
        return true;
    }

    var strBase = '/cgi-bin/GInfo.dll?';
    var strVali = '&w=au-express';
    var request = false;
    try { request = new XMLHttpRequest(); }
    catch (trymicrosoft) {
        try { request = new ActiveXObject('Msxml2.XMLHTTP'); }
        catch (othermicrosoft) {
            try { request = new ActiveXObject('Microsoft.XMLHTTP'); }
            catch (failed) { request = false; }
        }
    }

    var cHasSRIPic = '���ϴ�';
    function SetPrint1(o) { o.innerHTML = '�ٴ�ӡ'; }
    function SetPrintLab(o) { o.innerHTML = '�ٴ�ӡ��ǩ'; }
    //function SetPrintInv(o) { o.innerHTML = '�ٴ�ӡ��Ʊ'; }

    function PrintTab() {
        var n = 0;
        var oid = document.getElementsByName("iid");
        for (var i = 0; i < oid.length; i++) {
            if (oid[i].checked) { n++; }
        }
        if (n == 0) { alert("û��ѡ���˵�,�뷵����д"); return false; }

        theList.cmodel.value = "batchTab";
        theList.ntype.value = 0;
        theList.submit();
    }
    function PrintLab() {
        var n = 0;
        var oid = document.getElementsByName("iid");
        for (var i = 0; i < oid.length; i++) {
            if (oid[i].checked) { n++; }
        }
        if (n == 0) { alert("û��ѡ���˵�,�뷵����д"); return false; }
        theList.cmodel.value = "batchLab";
        theList.ntype.value = 1;
        theList.submit();
    }
    function PrintInv() {
        var n = 0;
        var oid = document.getElementsByName("iid");
        for (var i = 0; i < oid.length; i++) {
            if (oid[i].checked) { n++; }
        }
        if (n == 0) { alert("û��ѡ���˵�,�뷵����д"); return false; }
        theList.cmodel.value = "batchInv";
        theList.ntype.value = 2;
        theList.submit();
    }
    function CheckAll(ischeck) {
        $("input[name='iid']").prop("checked",ischeck);
    }

    var trid = 0;
    function DeleteItRow(iid) {
        trid = iid;
        var url = "/cgi-bin/GInfo.dll?RecPreInputDel&w=au-express&iid=" + iid;
        request.open("GET", url, false);
        request.onreadystatechange = DeleteItOK;
        request.setRequestHeader("If-Modified-Since", "0");
        request.send(null);
    }
    function DeleteItOK() {
        if (request.readyState == 4) {
            if (request.status == 200) {
                var res = request.responseText;
                if (res.search("ɾ���ɹ�") == -1) { document.getElementById("notice_" + trid).innerHTML = "����ɾ��"; return; }
                var i = document.getElementById("tr_" + trid).rowIndex;
                document.getElementById("Tlist").deleteRow(i);
                return;
            }
        }
    }

    function FillModel() {
        var url = "/cgi-bin/GInfo.dll?ajxEmsQueryPDFLabel&r=" + Math.random();
        request.open("GET", url, true);
        request.onreadystatechange = FillModelOK;
        request.send(null);
    }

    function FillModelOK() {
        if (request.readyState == 4) {
            if (request.status == 200) {
                eval("var res = " + request.responseText);
                if (res.ReturnValue != 1) { return; }
                var str = "<select id='emslab'><option value='' selected>========ѡ�񶩵���ǩ��ʽ========</option>";
                for (i = 0; i < res.iTotalRec; i++) {
                    exArray[i] = res.RecList[i].cName;
                    str += "<option value='";
                    str += res.RecList[i].cName;
                    str += "'>";
                    str += res.RecList[i].cName;
                    str += "</option>";

                }
                str += "</select>";
                o = document.getElementById("oModel");
                if (o == null) return;
                o.innerHTML = str;
            }
        }
    }

    function prtVali() {
        var theList = document.getElementById("emslab").value;
        var n = 0;
        var oid = document.getElementsByName("iid");
        $("input[name='cmodel']").val(theList);
        for (var i = 0; i < oid.length; i++) {
            if (oid[i].checked) { n++; }
        }
        if (n == 0) { alert("û��ѡ���˵�,�뷵����д"); return false; } 
        $("#theList").submit();
    }

    function BatchDelete() {
        var oid = document.getElementsByName("iid");
        var a = new Array();
        var n = 0;
        for (var i = 0; i < oid.length; i++) {
            if (oid[i].checked) { a[n] = oid[i].value; n++; }
        }
        if (n == 0) { alert("��ѡ���˵�"); return false; }
        else {
            for (var j = 0; j < a.length; j++) {
                DeleteItRow(a[j]);
            }
        }
        window.location.reload(true);
    }

    function prtOne(o) { 
        if (document.getElementById("emslab").value == "") { alert("��ѡ�񶩵���ǩ��ʽ"); return; }
        var c = document.getElementById("emslab").value;  
        var str = "/cgi-bin/GInfo.dll?EmsPrintBatch&w=au-express&ntype=0&ntable=1&cmodel=" + c + "&iid=" + o;
        window.open(str, "_blank");
    }

    var iModKind = 0;
    var strKindInfo = "";
    function ModKindFunc() {
        iModKind = document.getElementById("ModKind").value;
        strKindInfo = document.getElementById("KindInfo").value;
        var oid = document.getElementsByName("iid");
        var a = new Array();
        var n = 0;
        for (var i = 0; i < oid.length; i++) {
            if (oid[i].checked) { a[n] = oid[i].value; n++; }
        }
        if (n == 0) { alert("��ѡ���˵�"); return false; }
        else {
            for (var j = 0; j < a.length; j++) {
                ModInfo(a[j]);
            }
        }
        var x = document.getElementById("ModKind");
        x.options[0].selected = true;
    }

    function checkOrder() {

        if (document.getElementById("cemskindType").value == "") {
            alert("��ѡ��Ҫ��ȡ���˵��Ĺ�˾");
            return;
        }
        var va = document.getElementById("cemskindType").value; 
        var oid = document.getElementsByName("iid");
        var a = new Array();
        var n = 0;
        var par = "";
        for (var i = 0; i < oid.length; i++) {
            if (oid[i].checked)
            {
                a[n] = oid[i].value;
                n++;
            }
        }
        if (n == 0) {
            alert("��ѡ���˵�"); return false;
        } else {
            for (var j = 0; j < a.length; j++) {
                par+=a[j]+",";
            }
            par = par.substring(0, par.length - 1);
        } 
        var url = "/SynchronousOrder.aspx?iid=" + par + "&extype=" + encodeURIComponent(va)

        request.open("GET", url, false);
        request.onreadystatechange = AjaxoOK;
        request.setRequestHeader("If-Modified-Since", "0");
        request.send(null);

        location.reload();
    }

    function ModInfo(iid) {
        trid = iid;
        var url = "/cgi-bin/GInfo.dll?ajxPreMod&w=au-express&iid=" + iid + "&ntype=" + iModKind + "&cvalue=" + strKindInfo;
        request.open("GET", url, false);
        request.onreadystatechange = ModInfoOK;
        request.setRequestHeader("If-Modified-Since", "0");
        request.send(null);
    }
    function ModInfoOK() {
        if (request.readyState == 4) {
            if (request.status == 200) { 
                eval("var res = " + request.responseText);
                if (res.ReturnValue > 0) { document.getElementById("td_" + iModKind + "_" + trid).innerHTML = strKindInfo; return; }
                else if (res.ReturnValue == -1) { str = "��¼���Ǹÿͻ���"; return; }
                else if (res.ReturnValue == -2) { str = "��¼������"; return; }
                else if (res.ReturnValue == -9) { str = "ϵͳ����"; return; }
                else if (res.ReturnValue == -12) { str = "���ǿ�ݿͻ�"; return; }
                else if (res.ReturnValue == -13) { str = "��¼id����"; return; }
                else if (res.ReturnValue == -14) { str = "ntype����"; return; }
                else if (res.ReturnValue == -15) { str = "cvalue����"; return; }
                else if (res.ReturnValue == -16) { str = "cvalue���ȴ���"; return; }
                alert(str);
            }
        }
    }

    function AjaxoOK() { 
        if (request.readyState == 4) {
            if (request.status == 200) {
                var str = "";
                debugger;
                var s = request.responseText ;
                if (s =="-2") {
                    str = "�������ݴ���"; 
                } else if (s == "0") {
                    str = "����ʧ�ܣ�����ѡ��Ķ����Ƿ����µ�,������������ϵ����Ա"; 
                } else {
                    str = "�Զ��µ���ɣ��ɹ�" + s + "������δȫ���ɹ�����ѡ�������Ƿ������µ�����";
                }
                alert(str);
                
            }
        }
    }

    function Ajaxcheck() {
        if (request.readyState == 4) {
            if (request.status == 200) {
                var str = "";
                debugger;
                var s = request.responseText;
                if (s == "-2") {
                    str = "�������ݴ���";
                } else if (s == "0") {
                    str = "����ʧ�ܣ�����ѡ��Ķ����Ƿ����µ�";
                } else {
                    str = "��ַУ����ɣ����ɹ�У��"+s+"��������δУ��ɹ�����������ϵ����˾��";
                }
                alert(str);

            }
        }
    }

    function QueryEmskind() {
        var url = "/cgi-bin/GInfo.dll?ajxEmsQueryEmsKind&w=au-express";
        request.open("GET", url, false);
        request.onreadystatechange = QueryEmskindOK;
        request.setRequestHeader("If-Modified-Since", "0");
        request.send(null); 
    }

    function QueryEmskindOK() {
        if (request.readyState == 4) {
            if (request.status == 200) {
                eval("var res = " + request.responseText);
                if (res.ReturnValue <= 0) { oemskind.innerHTML = "δ�����������"; return; };

                var str = "<select size=1 name='cemskind' id='cemskind'><option value='' selected>--��ѡ��--</option>";
                for (var i = 0; i < res.ReturnValue; i++) {
                    str += "<option value='";
                    str += res.List[i].oName;
                    str += "'>";
                    str += res.List[i].cName;
                    str += "</option>";

                } 
                str += "</select>";  
                var o = document.getElementById("oemskind"); 
                if (o == null) return;
                o.innerHTML = str; 
                FillEmsModel();
                return;
            }
        }
    }
    function addcheck() { 
        var oid = document.getElementsByName("iid");
        var a = new Array();
        var n = 0;
        var par = "";
        for (var i = 0; i < oid.length; i++) {
            if (oid[i].checked) {
                a[n] = oid[i].value;
                n++;
            }
        }
        if (n == 0) {
            alert("��ѡ���˵�"); return false;
        } else {
            for (var j = 0; j < a.length; j++) {
                par += a[j] + ",";
            }
            par = par.substring(0, par.length - 1);
        }
        alert(par);
        var url = "/CheckArea.aspx?iid=" + par;

        request.open("GET", url, false);
        request.onreadystatechange = Ajaxcheck;
        request.setRequestHeader("If-Modified-Since", "0");
        request.send(null);

        location.reload();
    }

</script>
</head>

<body  onload="FillModel();">
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td align='left'>&nbsp;</td>
  </tr>
  <tr>
    <td height="50" align='left' bgcolor="#48a1fb"><span class='kindc2'>������/�������б�</span></td>
  </tr>
  <tr>
    <td >&nbsp;</td>
  </tr>
  <tr>
    <td height="110" bgcolor="#f0f0f0"  style="background-image:url(/EMS_MODEL/pic/login_03.jpg); background-position:left top; background-repeat:no-repeat">
     <form method='get' action='Logistic.aspx' target='main'>
	<input TYPE='hidden' NAME='w' VALUE='au-express'>
	<input TYPE='hidden' NAME='nper' VALUE='40'>
	<table width="100%" border="0" cellspacing="0" cellpadding="0">
	  <tr>
	    <td width="837" height="50" align="left">������<span id="oemskind"></span> 
          ��
          �� �� �ţ�
          <input name="cnum" type="text" class="myinput" value="" />
	       
	       ��Ŀ �� �أ�
	         <input name="cdes" type="text" class="myinput1" value="" />
	         ��
	         ���� ǩ��
	         <input name="cmark" type="text" class="myinput1" value="" />
	         ��
	         ¼�����ڣ�
             <input name='bdate' type='text' class="Wdate" id="cdateb" onclick="WdatePicker()" size="14" />
&nbsp;��
<input name='edate' type='text'  class="Wdate" id="cdateb" onclick="WdatePicker()" size="14" />
	    <button type='submit'  class='btn btn-primary'>��ѯ</button></td>
	    </tr>
    </table> 
    </form></td>
  </tr>
</table>
 <form method='POST' action='/cgi-bin/GInfo.dll?EmsPrintBatch' id='theList' target="_blank">

<input TYPE='hidden' NAME='w' VALUE='au-express'>
<input TYPE='hidden' NAME='ntype' VALUE='0'>
<input TYPE='hidden' NAME='ntable' VALUE='1'>
<input TYPE='hidden' NAME='cmodel' VALUE=''>

<table border='0' cellspacing='0' class='recPreInputListTable'>
<tr>
  <td>&nbsp;</td>
</tr>
<tr>
<td height="110"  bgcolor="#f0f0f0"  style=" background-position:left top; background-repeat:no-repeat">
    <div style="float:left; padding-left:30px"> 
    <button type='button' value='��ַУ��' onclick="addcheck()" class='btn btn-primary'>��ַУ��</button> 
        </div>
    <div style="float:left; padding-left:30px">
    <div id="oemskindType">
        <select size=1 name='cemskindType' id='cemskindType'>
            <option value='' selected>--��ѡ��--</option> 
                <% foreach (var e in emsList){ %>
                 <option value="<%=e.cemskind %>"><%=e.cemskind %></option>
                <%} %>
        </select>

    </div>
    <button  type="button" class='btn btn-primary' value="��ȡ�˵�"  onclick="checkOrder()" >��ȡ�˵�</button>
        </div>
    <div style="float:left;padding-left:30px"">  
        <div id="oModel"></div>
    <button type='button' value='ɾ������' onclick="prtVali();" class='btn btn-primary'>������ӡ</button> 

        </div>
    <div style="float:left;padding-left:30px"">
       ��ע��ѯ��<input type="text" id="expnumber"  value="" class="myinput1" style="width:150px" maxlength="20"/>
        </div>
  
</td>
 </tr>
  <tr>
    <td align="left">&nbsp;</td>
  </tr>
  <tr>
    <td align="left">
        <table width='100%' border='0'align='left' cellpadding='2' cellspacing='1'   bgcolor="#CCCCCC" id="Tlist">
            <tr class="recPreInputListTitle">
        <td width="20" align="center"><input type="checkbox" onclick="CheckAll(this.checked)" name="checkall"></td>
        <td width="40" align="center">���</td>
        <td width="80" align="center">¼������</td>
        <td width="100" align="center">������</td>
        <td width="100" align="center">�˵���</td>
        <td width="60" align="center">Ŀ�ĵ�</td>
        <td width="100" align="center">����Ʒ��</td>
        <td width="70" align="center">����(kg)</td>
        <td width="70" align="center">�ջ���</td>
        <td width="100" align="center">�ջ��绰</td>
        <td width="180" align="center">�ջ�����ϸ��ַ</td>
        <td width="70" align="center">�ռ�ʡ��</td>
        <td width="70" align="center">�ռ�����</td>
        <td width="60" align="center">���˷�</td>
        <td align="left">����</td>
      </tr>
            <%var i = page;%>
            <%if (i > 1){
                 i= i * 10; 
              } %>
            <%foreach(var d in list){ %>
            <%i++; %>
            <tr align="center" class="recPreInputListEven" id="tr_11085">
        <td><input type="checkbox" name="iid" value="<%=d.iid %>"></td>
        <td align="center"><%=i %></td>
        <td align="center"><%=d.ddate.Value.ToString("yyyy��MM��dd��") %></td>
        <td id="td_0_11085"><%=d.cemskind %></td>
        <td align="center"><%=d.cnum %></td>
        <td align="center"><%=d.cdes %></td>
        <td id="td_4_11085"><%=d.cgoods %></td>
        <td id="td_3_11085"><%=d.fweight %></td>
        <td align="center"><%=d.creceiver %></td>
        <td align="center"><%=d.cphone %></td>
        <td align="center"><%=d.caddr %></td>
        <td align="center"><%=d.cprovince %></td>
        <td align="center"><%=d.ccity %></td>
        <td align="center">0.00</td>
        <td align="left"> <a href="javascript:prtOne(<%=d.iid %>);"><img src="/EMS_MODEL/pic/login_13.jpg" width="22" height="19"></a></td>
      </tr>
            <%} %>
      <tr align='center' class='recPreInputListTotal'>
        <td align='center'>&nbsp;</td>
        <td align='center'>�ϼ�</td>
        <td align="center">&nbsp;</td>
        <td  align='left'>&nbsp;</td>
        <td  align='left'>&nbsp;</td>
        <td align="left" >&nbsp;</td>
        <td>&nbsp;</td>
        <td><%=total %></td>
        <td align="center" >&nbsp;</td>
        <td align="center">&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td align="left">&nbsp;</td>
        <td align="left">&nbsp;</td>
      </tr>
	 
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
  <tr align="left">
    <td height="60">
    <button  type="button" class='btn btn-primary' value="��������"  onclick="window.open('/cgi-bin/GInfo.dll?RecPreInputListFile&w=au-express&cnum=&cdes=&bdate=&edate=&cemskind=&cmark=&creserve=&creceiver=&cphone=','_blank')">�����б�����ΪEXCEL���</button>	</td>
  </tr>
</table>
<input TYPE='hidden' NAME='xxx' VALUE='xxx'> 
     </form>
<script type="text/javascript">
    QueryEmskind();
</script>
 
</body>
</html>
