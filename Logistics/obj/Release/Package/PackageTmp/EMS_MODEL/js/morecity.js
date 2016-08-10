// JavaScript Document
var china = [
 //ֱϽ��
 ['������'],
 ['�Ϻ���'],
 ['�����'],
 ['������'],
 //��������
 ['�ӱ�ʡ','ʯ��ׯ','��ɽ','�ػʵ�','����','��̨','����','�żҿ�','�е�','����','�ȷ�','��ˮ'],
 ['ɽ��ʡ','̫ԭ','��ͬ','��Ȫ','����','����','˷��','����','�˳�','����','�ٷ�','����'],
 ['���ɹ�������','���ͺ���','��ͷ','�ں�','���','ͨ��','������˹','���ױ���','�����׶�','�����첼','�˰�','���ֹ���','������'],
 //��������
 ['����ʡ','����','����','��ɽ','��˳','��Ϫ','����','����','Ӫ��','����','����','�̽�','����','����','��«��'],
 ['����ʡ','����','����','��ƽ','��Դ','ͨ��','��ɽ','��ԭ','�׳�','�ӱ�'],
 ['������','������','�������','����','�׸�','˫Ѽɽ','����','����','��ľ˹','��̨��','ĵ����','�ں�','�绯','���˰���'],
 //��������
 ['����ʡ','�Ͼ�','����','����','����','����','��ͨ','���Ƹ�','����','�γ�','����','��','̩��','��Ǩ'],
 ['�㽭ʡ','����','����','����','����','����','����','��','����','��ɽ','̨��','��ˮ'],
 ['����ʡ','�Ϸ�','�ߺ�','����','����','��ɽ','����','ͭ��','����','��ɽ','����','����','����','����','����','����','����','����'],
 ['����ʡ','����','����','����','����','Ȫ��','����','��ƽ','����','����'],
 ['����ʡ','�ϲ�','������','Ƽ��','�Ž�','����','ӥ̶','����','����','�˴�','����','����'],
 ['ɽ��ʡ','����','�ൺ','�Ͳ�','��ׯ','��Ӫ','��̨','Ϋ��','����','����','̩��','����','����','����','����','�ĳ�','����','����'],
 //���ϵ���
 ['����ʡ','֣��','����','����','ƽ��ɽ','����','�ױ�','����','����','���','���','���','����Ͽ','����','����','����','�ܿ�','פ���'],
 ['����ʡ','�人','��ʯ','�差','ʮ��','����','�˲�','����','����','Т��','����','����','��ʩ'],
 ['����ʡ','��ɳ','����','��̶','����','����','����','����','�żҽ�','����','����','����','����','¦��','����'],
 ['�㶫ʡ','����','����','�麣','��ͷ','�ع�','��ɽ','����','տ��','ï��','����','����','÷��','��β','��Դ','����','��Զ','��ݸ','��ɽ','����','����','�Ƹ�'],
 ['����������','����','����','����','����','����','���Ǹ�','����','���','����','��ɫ','����','�ӳ�','����','����'],
 ['����ʡ','����','����'],
 //���ϵ���
 ['�Ĵ�ʡ','�ɶ�','�Թ�','��֦��','����','����','����','��Ԫ','����','�ڽ�','��ɽ','�ϳ�','�˱�','�㰲','����','üɽ','�Ű�','����','����',"����","����","��ɽ"],
 ['����ʡ','����',"����ˮ","����","��˳","ͭ��","�Ͻ�","ǭ����","ǭ����","ǭ��"],
 ['����ʡ','����','����','��Ϫ',"��ɽ","��ͨ","����","�ն�","�ٲ�","��ɽ","���","��˫����","����","����","�º�","ŭ��","����"],
 ['����������',"����","����","ɽ��","�տ���","����","����","��֥"],
 //��������
 ['����ʡ','����','ͭ��','����','����','μ��','�Ӱ�','����','����','����','����'],
 ['����ʡ',"����","������","���","����","��ˮ","����","��Ҵ","ƽ��","��Ȫ","����","����","¤��","����","����"],
 ['�ຣʡ',"����","����","����","����","����","����","����","����"],
 ['����������','����',"ʯ��ɽ","����","��ԭ","����"],
 ['�½�������','��³ľ��',"��������","��³��","����","����","������","��ʲ","�������տ¶�����","���������ɹ�","����","���������ɹ�","���������","����","����̩"]
 ];

 function BindProvince(){
    var opt0 = "ʡ��";
    var ProvinceCount=china.length;
    var cprovince = document.getElementById("cprovince");
    cprovince.innerHTML = "";
    cprovince.options[0] = new Option(opt0,"");
    for(var i=0; i<ProvinceCount; i++){
        cprovince.options[i+1] = new Option(china[i][0],china[i][0]);
    }
 }

 function BindCity(City){
    var opt0 = "ʡ��";
    var ProvinceCount=china.length;
    var cprovince = document.getElementById("cprovince");
    cprovince.innerHTML = "";
    cprovince.options[0] = new Option(opt0,"");
    
    var opt0City = "����";
    var ccity = document.getElementById("ccity");
    ccity.innerHTML = "";
    ccity.options[0] = new Option(opt0City,"");

    var flag=false;
    var chose=true;
    var selectProvinceIndex=0;
    for(var i=0; i<ProvinceCount; i++){
        if(!flag){
            var cityCount = china[i].length;
            for(var j=1; j<cityCount; j++){
                if(china[i][j]==City)
                {
                    flag=true;
                    selectProvinceIndex=i;
                    break;
                }
            }
        }
        cprovince.options[i+1] = new Option(china[i][0],china[i][0]);
        if(flag && chose){
            cprovince.options[i+1].selected = true;
            chose=false;
        }
    }
    var cityCount = china[selectProvinceIndex].length;
    for(var i=0; i<cityCount; i++){
        if(cityCount == 1 && i == 0){
            ccity.options[i+1] = new Option(china[selectProvinceIndex][i],china[selectProvinceIndex][i]);
            i = 1;
        }
        else if(cityCount > 1 && i == 0){
            i = 1;
            ccity.options[i] = new Option(china[selectProvinceIndex][i],china[selectProvinceIndex][i]);
        }
        else{
            ccity.options[i] = new Option(china[selectProvinceIndex][i],china[selectProvinceIndex][i]);
        }
        if(china[selectProvinceIndex][i]==City){
            ccity.options[i].selected=true;
        }
    }

 }

 function selectMoreCity(sbj){
        var opt0 = "����";
        if(sbj.selectedIndex==0){
            var ccity = document.getElementById("ccity");
            ccity.innerHTML = "";
            ccity.options[0] = new Option(opt0,"");
            return;
        }
        var selectProvince = sbj.options[sbj.selectedIndex].value;
        var ProvinceCount = china.length;
        for(var i=0; i<ProvinceCount; i++){
            if(china[i][0] == selectProvince){
                var cityCount = china[i].length;
                var ccity = document.getElementById("ccity");
                ccity.innerHTML = "";
                ccity.options[0] = new Option(opt0,"");
                for(var j=0; j<cityCount; j++){
                    if(cityCount == 1 && j == 0){
                        ccity.options[j+1] = new Option(china[i][j],china[i][j]);
                        j = 1;
                    }
                     else if(cityCount > 1 && j == 0){
                        j = 1;
                         ccity.options[j] = new Option(china[i][j],china[i][j]);
                     }
                     else{
                         ccity.options[j] = new Option(china[i][j],china[i][j]);
                    }
                    if(j == 1){
                         ccity.options[1].selected = true;
                    }
                }
                break;
            }
        }
    }
