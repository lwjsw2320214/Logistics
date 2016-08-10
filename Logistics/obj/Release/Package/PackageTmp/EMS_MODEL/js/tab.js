// JavaScript Document
var ilast = 0;
function tabitOn(n)
{
	var oTabL=document.getElementById("otab"+ilast);
	var oTabC=document.getElementById("otab"+n);
		oTabL.style.display 	= "none";
		oTabC.style.display		= "block";
	
	var oL=document.getElementById("otdClass"+ilast);
	var oC=document.getElementById("otdClass"+n);
	
		oL.className	= "classOut";
		oC.className	= "classOn";
		
	ilast = n;
}
var ilast2 = 1;
function tabitOn2(n)
{
	var oTabL2=document.getElementById("otab2"+ilast2);
	var oTabC2=document.getElementById("otab2"+n);
		oTabL2.style.display 	= "none";
		oTabC2.style.display	= "block";
	
	var oL2=document.getElementById("otdClass2"+ilast2);
	var oC2=document.getElementById("otdClass2"+n);
	
		oL2.className	= "classOut2";
		oC2.className	= "classOn2";
		
	ilast2 = n;
}

