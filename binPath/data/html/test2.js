//播放页面的句柄
var FRIAXYWin4PGIS = null;
var PROJECT_URL = 'http://10.197.1.216:7001/PMPlatForm';

function testShow(){
	showPlayerWin('ycpgis','ycpgis','640195412358742','64010400001310000150');
}

function showPlayerWin(sysname,password,citizenid,deviceID){
	//管理平台视频调用接口的url,用于直接打开播放页面
	var url = PROJECT_URL + '/VideoForPGIS/player2.jsp?sysname='+sysname
														+ "&citizenid="+citizenid
														+'&password='+password
														+'&deviceID='+deviceID;
	
	//如果播放页面句柄不为空且播放页面未被关闭，则点播视频到指定客户端
	//否则，直接打开播放页面
	if(FRIAXYWin4PGIS!=null&&FRIAXYWin4PGIS.closed==false){
		playVideo(sysname,password,citizenid,deviceID);	
		FRIAXYWin4PGIS.focus();	
	}
	else{			
	    //FRIAXYWin4PGIS = window.open(url);
	    window.location.href = url;
	    //window.open(url, "_blank", "width=1024,height=768,left=5,top=5");
	}
	
}

/*******************************************点播视频到指定客户端**********************************************************/
function playVideo(sysname,password,citizenid,id){	
	var url = PROJECT_URL + "/monitoring/PGISVideoManage.do?operation=playRealtimeVideo" 
														+ "&deviceID=" + id
														+ "&sysname="+sysname
														+ "&citizenid="+citizenid
														+ "&password="+password;
	var successFunc = function(response){	
			var text = response.responseText;
			if(text!=""){
				alert(text);
			}
	};
	
	sendAjax(url,successFunc);
}// end of playVideo

/*******************************************发送ajax请求****************************************************************/
function sendAjax(url,successFunc,failureFunc){
	 var xmlHttp;
     if(window.ActiveXObject){   
   	    xmlHttp=new ActiveXObject("Microsoft.XMLHTTP")   
     } 
     else if(window.XMLHttpRequest){   
   	    xmlHttp=new XMLHttpRequest()   
     }   
   	    
    
    xmlHttp.open("POST",url,true);
    
    xmlHttp.onreadystatechange=function(){
    	try{
       	 if((xmlHttp.readystate==4)&&(xmlHttp.status==200)){ 
       	 		if(successFunc){       	 		
            		successFunc(xmlHttp);
            	}
        	}
        }    
        catch(err){
        	if(failureFunc){
        		failureFunc();
        	}
        }     
    }
     xmlHttp.send(); 
}


