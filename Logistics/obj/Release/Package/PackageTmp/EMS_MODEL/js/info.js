// JavaScript Document

var request = false;
try {request = new XMLHttpRequest();} 
catch (trymicrosoft) {
try{request = new ActiveXObject('Msxml2.XMLHTTP');}
catch (othermicrosoft) {
try{request = new ActiveXObject('Microsoft.XMLHTTP');}
catch (failed){request = false;}}}

function GO(o){
if(event.keyCode==13){ event.keyCode=9;o.focus();}}

function QueryDes()
{
var url = "/cgi-bin/GInfo.dll?ajxEmsQueryDes&cdes="+theForm.cdes.value;
request.open("GET", url, true);
request.onreadystatechange = QueryDesOK;
request.send(null);
}

function QueryDesOK()
{
if (request.readyState == 4)
{
if (request.status == 200)
{
if(request.responseText.charAt(0) == "-") return ;
var response = request.responseText.split("|");
theForm.cdes.value=response[2].replace(/&#124;/g,"|");
return ;
}
}
}
function stInfo()
{
    var niid;//记录ID,&nid=
    var curl;//如果为静态链接，链接串
    var bstatic;//是否为静态链接：true:false
    var cpicture;//如果有图片，&pid=
    var ddate;//日期，YYYY.mm.dd HH:MM,自行处理
    var ctitle;//标题
}

function ellipTitle(str,n)
{
var ilen = str.length;
if(ilen*2 <= n) return str;
n -= 3;
var i = 0;
while(i < ilen && n > 0)
{
if(escape(str.charAt(i)).length>4) n--;
n--;
i++;
}
if( n > 0) return str;
return str.substring(0,i)+"...";
}

//--初始化信息空间及状态--
var nTotalFrame =2;//有多少组frame，数字需要改动！！！
var anInfoGet = new Array(nTotalFrame);//新闻接收状态
var anTopicGet = new Array(nTotalFrame);//热点接收状态

for(i=0;i<nTotalFrame;i++) 
{
anInfoGet[i] = -1;
anTopicGet[i] = -1;   
}

function FuncInfo(ic,n)
{
if(n > 0) anInfoGet[ic] = n;
else anInfoGet[ic] = 0; 
if(anTopicGet[ic] >= 0) display(n,ic);
}

function FuncTopic(ic,n)
{
if(n > 0) anTopicGet[ic] = n;
else anTopicGet[ic] = 0; 
if(anInfoGet[ic] >= 0) display(n,ic);

}

function display(n,ic)
{

var str="";
var o;
var aID = new Array();
var bDup;
var MaxNum = 7;//需要修改，最多显示几条
var TotalNum = 0;

for(j=0;j<2;j++)
{
if(j == 0 && anTopicGet[ic] <= 0) continue;
if(j == 1 && anInfoGet[ic] <= 0) continue;

if(j == 0) //如果大于6组，需要改动！！！
{
if(ic == 0) o=otopic0;
else if(ic == 1) o=otopic1;
else if(ic == 2) o=otopic2;
else if(ic == 3) o=otopic3;
else if(ic == 4) o=otopic4;
else o=otopic5;
}
else
{
if(ic == 0) o=oinfo0;
else if(ic == 1) o=oinfo1;
else if(ic == 2) o=oinfo2;
else if(ic == 3) o=oinfo3;
else if(ic == 4) o=oinfo4;
else o=oinfo5; 
}



for(i=0;i<o.length;i++)
{
bDup = false;
for(x=0;x<aID.length;x++)
{
if(aID[x] == o[i].niid)
{
bDup = true;
break;
}
}
if(bDup) continue;
aID[aID.length] = o[i].niid;

if((++TotalNum) > MaxNum) break;

	str	 +=	"<div id='newsBox'>";
	str	 +="<div id='newsList'>";
	str += "<a href='";
	if(o[i].bstatic) str += o[i].curl;
	else
	{ 
	str += "/cgi-bin/GInfo.dll?DispInfo&w=au-express&nid=";
	str += o[i].niid;
	}
	str += "' target='_blank' class='";
	if(j == 0) str += "fred'>";
	else str += "newsList'>";
	str +=	 ellipTitle(o[i].ctitle,30);
	str +=	 "</a></div>";
	/*str +=	 "<div id='newsData'>[";
	str += o[i].ddate.substring(0,10); 
	str += "]</div>";*/
	str += "</div>";
		
}
}

o = document.getElementById("news"+ic);
if(o == null) return ;
o.innerHTML=str;

}

var widths=651;              
var heights=216;  
var counts=3;
var a_img=new Array(3);
var a_url=new Array(3);

function DispWebPic(n)
{
		if(n<=0) return;
		counts=n;
		for(i=0;i<counts;i++)
		{
			a_img[i]=new Image();
			a_img[i].src='/cgi-bin/GInfo.dll?WebPic&w=au-express&order=' +i+ '&len=' + a_oWebPic[i].len;
			a_img[i].alt=a_oWebPic[i].intro;
		}
	
}