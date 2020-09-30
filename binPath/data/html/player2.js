var addPresetPosWin;
var PGIS_sysname = "";
var PGIS_citizenid = "";
var PGIS_password = "";
var PGIS_policeID = "";
//历史视频检索窗口
var VideoForPGIS_HistoryWin;
Ext.onReady(function () {
    hSpeed = new Ext.form.ComboBox({
        store: new Ext.data.SimpleStore({
            fields: ['value'],
            data: [['1'], ['2'], ['3'], ['4'], ['5'], ['6'], ['7'], ['8']]
        }),
        displayField: 'value',
        valueField: 'value',
        labelSeparator: '',
        name: 'hSpeed',
        fieldLabel: '水平速度',
        width: 35,
        triggerAction: 'all',
        forceSelection: true,
        editable: true,
        mode: 'local',
        value: '1'
    });

    vSpeed = new Ext.form.ComboBox({
        store: new Ext.data.SimpleStore({
            fields: ['value'],
            data: [['1'], ['2'], ['3'], ['4'], ['5'], ['6'], ['7'], ['8']]
        }),
        displayField: 'value',
        valueField: 'value',
        name: 'vSpeed',
        fieldLabel: '垂直速度',
        triggerAction: 'all',
        labelSeparator: '',
        forceSelection: true,
        width: 35,
        editable: true,
        mode: 'local',
        value: '1'
    });

    lensSpeed = new Ext.form.ComboBox({
        store: new Ext.data.SimpleStore({
            fields: ['value'],
            data: [['1'], ['2'], ['3'], ['4'], ['5'], ['6'], ['7'], ['8']]
        }),
        displayField: 'value',
        valueField: 'value',
        name: 'lensSpeed',
        fieldLabel: '镜头速度',
        labelSeparator: '',
        width: 35,
        triggerAction: 'all',
        forceSelection: true,
        editable: true,
        mode: 'local',
        value: '1'
    });

    /*	//2008.12.11 刘峰去掉控制策略，为OSD让出位置
        controlPolicy = new Ext.form.ComboBox({
             store: new Ext.data.SimpleStore({
                    fields:['id','value'],
                    data:[['0','不可抢占'],['1','可抢占']]
               }),
             displayField:'value',
             valueField:'value',
             name:'controlPolicy',
             fieldLabel:'控制策略',
             triggerAction: 'all',
             forceSelection: true,
             editable:true,
             width:100,
             mode:'local'
        });
        controlPolicy.value = '不可抢占';
        controlPolicy.disable();
    */
    switchCombo = new Ext.form.ComboBox({
        store: new Ext.data.Store(
				     {
				         url: PROJECT_NAME + '/monitoring/PGISVideoManage.do?operation=loadSwitchType',
				         reader: new Ext.data.JsonReader(
                             { root: 'data' }, [{ name: 'id' }, { name: 'value' }]
                  )
				     }),
        displayField: 'value',
        valueField: 'value',
        name: 'switch',
        fieldLabel: '开关类型',
        triggerAction: 'all',
        width: 100,
        forceSelection: true,
        editable: true,
        mode: 'local'
    });

    //云台控制的图标按钮面板	
    var iconPanel = new Ext.Panel({
        width: 70,
        items: [{
            layout: 'column',
            items: [{ columnWidth: .3, items: [{ html: '<div onMouseUp="speedConstrolUp(\'leftUp\',this)" onMouseDown="speedConstrolDown(\'leftUp\',this)"><img src="../monitoring/image/leftup.gif" style="display:block" alt="左上"/><img style="display:none" src="/PMPlatForm/scene/common/images/leftUp_2.gif" alt="左上"/></div>' }] },
                    { columnWidth: .3, items: [{ html: '<div onMouseUp="speedConstrolUp(\'up\',this)"  onMouseDown="speedConstrolDown(\'up\',this)"><img src="../monitoring/image/up.gif" style="display:block" alt="上"/><img style="display:none" src="/PMPlatForm/scene/common/images/up_2.gif" alt="上"/></div>' }] },
                    { columnWidth: .3, items: [{ html: '<div onMouseUp="speedConstrolUp(\'leftUp\',this)"  onMouseDown="speedConstrolDown(\'rightUp\',this)"><img src="../monitoring/image/rightup.gif" style="display:block" alt="右上"/><img style="display:none" src="/PMPlatForm/scene/common/images/rightUp_2.gif" alt="右上"/></div>' }] }]
        }, {
            layout: 'column',
            items: [{ columnWidth: .3, items: [{ html: '<div onMouseUp="speedConstrolUp(\'left\',this)"  onMouseDown="speedConstrolDown(\'left\',this)"><img src="../monitoring/image/left.gif" style="display:block" alt="左"/><img style="display:none" src="/PMPlatForm/scene/common/images/left_2.gif" alt="左"/></div>' }] },
                    { columnWidth: .3, items: [{ html: '<div ><img src="../monitoring/image/center.gif"/></div>' }] },
                    { columnWidth: .3, items: [{ html: '<div onMouseUp="speedConstrolUp(\'right\',this)"  onMouseDown="speedConstrolDown(\'right\',this)"><img src="../monitoring/image/right.gif" style="display:block" alt="右"/><img style="display:none" src="/PMPlatForm/scene/common/images/right_2.gif" alt="右"/></div>' }] }]
        }, {
            layout: 'column',
            items: [{ columnWidth: .3, items: [{ html: '<div onMouseUp="speedConstrolUp(\'leftDown\',this)"  onMouseDown="speedConstrolDown(\'leftDown\',this)"><img src="../monitoring/image/leftdown.gif" style="display:block" alt="左下"/><img style="display:none" src="/PMPlatForm/scene/common/images/leftDown_2.gif" alt="左下"/></div>' }] },
                    { columnWidth: .3, items: [{ html: '<div onMouseUp="speedConstrolUp(\'Down\',this)"  onMouseDown="speedConstrolDown(\'down\',this)"><img src="../monitoring/image/down.gif" style="display:block" alt="下"/><img style="display:none" src="/PMPlatForm/scene/common/images/down_2.gif" alt="下"/></div>' }] },
                    { columnWidth: .3, items: [{ html: '<div onMouseUp="speedConstrolUp(\'rightDown\',this)"  onMouseDown="speedConstrolDown(\'rightDown\',this)"><img src="../monitoring/image/rightdown.gif" style="display:block" alt="右下"/><img style="display:none" src="/PMPlatForm/scene/common/images/rightDown_2.gif" alt="右下"/></div>' }] }]
        }, {},
			  {
			      layout: 'column',
			      items: [{ columnWidth: .3, items: [{ html: '<div onMouseUp="lensConstrolUp(\'ZoomIn\',this)"  onMouseDown="lensConstrolDown(\'ZoomIn\',this)"><img src="../monitoring/image/b01.gif" style="display:block" alt="镜头放大"/><img style="display:none" src="/PMPlatForm/scene/common/images/zoomIn_2.gif" alt="镜头放大"/></div>' }] },
						 { columnWidth: .3, items: [{ html: '<div onMouseUp="lensConstrolUp(\'IrisOpen\',this)"  onMouseDown="lensConstrolDown(\'IrisOpen\',this)"><img src="../monitoring/image/b03.gif" style="display:block" alt="光圈放大"/><img style="display:none" src="/PMPlatForm/scene/common/images/irisOpen_2.gif" alt="光圈放大"/></div>' }] },
						 { columnWidth: .3, items: [{ html: '<div onMouseUp="lensConstrolUp(\'FocusNear\',this)"  onMouseDown="lensConstrolDown(\'FocusNear\',this)"><img src="../monitoring/image/b05.gif" style="display:block" alt="聚焦近"/><img style="display:none" src="/PMPlatForm/scene/common/images/focusNear_2.gif" alt="焦距近"/></div>' }] }]
			  }, {
			      layout: 'column',
			      items: [{ columnWidth: .3, items: [{ html: '<div onMouseUp="lensConstrolUp(\'ZoomOut\',this)"  onMouseDown="lensConstrolDown(\'ZoomOut\',this)"><img src="../monitoring/image/b02.gif" style="display:block" alt="镜头缩小"/><img style="display:none" src="/PMPlatForm/scene/common/images/zoomOut_2.gif" alt="镜头缩小"/></div>' }] },
						 { columnWidth: .3, items: [{ html: '<div onMouseUp="lensConstrolUp(\'IrisClose\',this)"  onMouseDown="lensConstrolDown(\'IrisClose\',this)"><img src="../monitoring/image/b04.gif" style="display:block" alt="光圈缩小"/><img style="display:none" src="/PMPlatForm/scene/common/images/irisClose_2.gif" alt="光圈缩小"/></div>' }] },
						 { columnWidth: .3, items: [{ html: '<div onMouseUp="lensConstrolUp(\'FocusFar\',this)"  onMouseDown="lensConstrolDown(\'FocusFar\',this)"><img src="../monitoring/image/b06.gif" style="display:block" alt="聚焦远"/><img style="display:none" src="/PMPlatForm/scene/common/images/focusFar_2.gif" alt="焦距远"/></div>' }] }]
			  }]
    });

    //云台控制的下拉框面板	

    var comboPanel = new Ext.Panel({
        width: 100,
        layout: 'form',
        labelAlign: 'right',
        items: [hSpeed, vSpeed, lensSpeed]
    });

    //预置位置表格		
    presetPositionGridStore = new Ext.data.Store({
        url: PROJECT_NAME + '/monitoring/PGISVideoManage.do?operation=loadPresetPosition',
        reader: new Ext.data.JsonReader({ root: 'data' }, [
			{ name: 'id' }, { name: 'position' }
        ])
    });


    var sm = new Ext.grid.CheckboxSelectionModel();

    presetPositionGrid = new Ext.grid.GridPanel({
        store: presetPositionGridStore,
        cm: new Ext.grid.ColumnModel([
            sm,
            { id: 'id', header: "编号", width: 35, dataIndex: 'id' },
            { id: 'position', header: "预置位描述", width: 125, dataIndex: 'position' }
        ]),
        sm: sm,
        viewConfig: {
            enableRowBody: true
        },
        //title:'预置位列表',
        autoScroll: true,
        height: 150,
        autoExpandColumn: 'position',
        stripeRows: true,
        //width:215,
        width: 160,
        bbar: [{ text: '调', handler: callPresetPosition },
			  //{text:'新增',handler:function(){Ext.MessageBox.prompt('','请输入预置位:',addPresetPosition);
			  {
			      text: '增', handler: function () {
			          if (getCurrentWinDeviceID() == "") {
			              Ext.Msg.alert('提示', '请选择一个摄像机进行操作'); //葛昊 2009-8-12 8:55:10
			              return;
			          }
			          showAddPresetPosWin();
			      }
			  },
			  { text: '删', handler: delePresetPosition },
			  {
			      text: '守', handler: function () {
			          if (getCurrentWinDeviceID() == "") {
			              Ext.Msg.alert('提示', '请选择一个摄像机进行操作'); //葛昊 2009-8-12 8:55:10
			              return;
			          }
			          if (Ext.getCmp("video_CameraGuard")) {
			              Ext.getCmp("video_CameraGuard").show();
			          } else {
			              new AXY.Video.CameraGuard({
			                  id: "video_CameraGuard"
			              }).show();
			          }
			          log.info("获取到摄像机的ID为:" + getCurrentWinDeviceID() + ",准备读取该摄像机的看守位数据。");
			          //Ext.getCmp("video_CameraGuard").loadData("13030000001340000001");
			          Ext.getCmp("video_CameraGuard").loadData(getCurrentWinDeviceID());
			      }
			  }
        ]
    });


    //前端设备控制区      
    deviceControlPanel = new Ext.FormPanel({
        title: '前端设备控制',
        region: 'east',
        frame: true,
        //width:250,
        //width:195,
        width: 300,
        //collapseMode:'mini',
        height: 700,
        collapsible: true,
        defaults: { xtype: 'fieldset' },
        items: [{
            title: '云台控制',
            width: 183,
            labelAlign: 'left',
            labelWidth: 55,
            items: [{
                layout: 'column',
                //labelWidth:60,
                items: [{ columnWidth: .4, items: iconPanel },
                    { columnWidth: .6, items: comboPanel }]
            }
                   // ,{},controlPolicy
            ]
        }, {
            title: '开关操作',
            width: 183,
            labelWidth: 55,
            labelAlign: 'left',
            items: switchCombo,
            buttonAlign: 'left',
            buttons: [{
                text: '开启',
                handler: switchOperate
            }, {
                text: '关闭',
                handler: switchOperate
            }]
        }, {
            title: '预置位操作',
            width: 183,
            items: presetPositionGrid
        }, {
            title: '登录/播放控制',
            width: 183,
            layout: 'column',
            items: [
                  { columnWidth: .15, items: [{ html: '<div onMouseDown="contorlPanelDown(this)" onMouseUp="contorlPanelUp(this)" onclick="onPlayControl(\'login\')"><img src="images/login.bmp" style="display:block" alt="登录视综平台"/><img src="images/logoff.bmp" style="display:none" alt="注销"/></div>' }] },
                  { columnWidth: .02, items: [{ html: '' }] },
                  { columnWidth: .15, items: [{ html: '<div onMouseDown="contorlPanelDown(this)" onMouseUp="contorlPanelUp(this)" onclick="onPlayControl(\'logout\')"><img src="images/logoff.bmp" style="display:block" alt="登出视综平台"/><img src="images/logoff.bmp" style="display:none" alt="注销"/></div>' }] },
                  { columnWidth: .02, items: [{ html: '' }] },
                  { columnWidth: .15, items: [{ html: '<div onMouseDown="contorlPanelDown(this)" onMouseUp="contorlPanelUp(this)" onclick="vlcplayer.ScreenPartition1()"><img src="images/1.jpg" style="display:block" alt="单屏"/><img src="images/1_2.gif" style="display:none" alt="单屏"/></div>' }] },
                  { columnWidth: .02, items: [{ html: '' }] },
                  { columnWidth: .15, items: [{ html: '<div onMouseDown="contorlPanelDown(this)" onMouseUp="contorlPanelUp(this)" onclick="vlcplayer.ScreenPartition2()"><img src="images/4.jpg" style="display:block" alt="四分屏"/><img src="images/4-2.gif" style="display:none" alt="四分屏"/></div>' }] },
                  { columnWidth: .02, items: [{ html: '' }] },
                  { columnWidth: .15, items: [{ html: '<div onMouseDown="contorlPanelDown(this)" onMouseUp="contorlPanelUp(this)" onclick="onPlayControl(\'stop\')"><img src="images/stop.gif" style="display:block" alt="停止播放"/><img src="images/stop_2.gif" style="display:none" alt="停止播放"/></div>' }] },
                  { columnWidth: .02, items: [{ html: '' }] },
                  { columnWidth: .15, items: [{ html: '<div onMouseDown="contorlPanelDown(this)" onMouseUp="contorlPanelUp(this)" onclick="onPlayControl(\'history\')"><img src="images/history.gif" style="display:block" alt="历史回放"/><img src="images/history_2.gif" style="display:none" alt="历史回放"/></div>' }] },
                  { columnWidth: .02, items: [{ html: '' }] }
            ]
        }
        ]
    });

    var basePanel = new Ext.Panel({
        width: 948,
        height: 600,
        title: 'PGIS视频',
        layout: 'column',
        items: [{ columnWidth: .75, items: [{ html: "<OBJECT ID='vlcplayer' CLASSID='CLSID:E6963FEA-95F6-4AA7-8C2C-44D5FB979B9A' width='704' height='576' align=><param name='url' value='D:\1.264'></OBJECT>" }] },
				{ columnWidth: .25, items: [deviceControlPanel] }
        ],
        renderTo: 'basePanel'
    });


    //2010年5月6日10:48:03 by liufeng 为PGIS调用页面，增加VLC播放插件的升级、下载功能
    if (vlcplayer.url == undefined) {
        Ext.Msg.alert("下载提示", "您的浏览器没有安装系统所必须的ActiveX控件,<a href='../mainframe/PlayerX_Setup.exe' target='_blank'>点击下载</a>,安装完毕后请重新启动IE浏览器!");
    } else if (vlcplayer.VerSion == undefined) {
        //vlcplayer.ScreenPartition1();
        Ext.Msg.alert("更新提示", "您安装的播放器插件不是最新版本,请下载安装最新版本,<a href='../mainframe/PlayerX_Setup.exe' target='_blank'>点击下载</a>,安装完毕后请重新启动IE浏览器!");
    } else if (vlcplayer.VerSion != VLC_PUGIN_VERSION) {
        //vlcplayer.ScreenPartition1();
        var constVersion = VLC_PUGIN_VERSION;  //VLC_PUGIN_VERSION 定义在 common/js/SysConstants.js
        var vlcplsyerVersion = vlcplayer.VerSion;
        var C_Version = constVersion.substring(0, 1) + constVersion.substring(2, 3) + constVersion.substring(4, 5) + constVersion.substring(6)
        var V_Version = vlcplsyerVersion.substring(0, 1) + vlcplsyerVersion.substring(2, 3) + vlcplsyerVersion.substring(4, 5) + vlcplsyerVersion.substring(6);
        log.info("PGIS调用页面VLC插件，C_Version=" + C_Version + ",V_Version=" + V_Version);
        if (C_Version > V_Version) {
            Ext.Msg.alert("更新提示", "您安装的播放器插件不是最新版本,请下载安装最新版本,<a href='../mainframe/PlayerX_Setup.exe' target='_blank'>点击下载</a>,安装完毕后请重新启动IE浏览器!");
        }
    } else {
        //vlcplayer.ScreenPartition1();
    }

});  //onReady end








//以下四个方法是云台控制区的图标对应的鼠标按下,松开的处理
function speedConstrolDown(opt, dom) {
    log.info("按下云台控制按钮");
    dom.childNodes[0].style.display = "none";
    dom.childNodes[1].style.display = "block";
    //	    var deviceID = getCurrentWinDeviceID();
    var deviceID = getCurrentWinDeviceID();  //调用plugin.js的此方法
    if (deviceID != null && deviceID != "") {
        var requestTime = (new Date()).getTime();
        Ext.Ajax.request({
            url: PROJECT_NAME + '/monitoring/PGISVideoManage.do?operation=speedConstrol',
            params: {
                commond: opt,
                detailOperation: 'start',
                sysname: sysname,
                password: password,
                citizenid: citizenid,
                policeID: policeID,
                hSpeed: hSpeed.getValue(),
                vSpeed: vSpeed.getValue(),
                deviceID: deviceID,
                //2009年10月28日16:12:50 by liufeng 测试性能，将请求时间发送到服务器端
                requestTime: requestTime
            },
            success: function (response) {
                if (response.responseText != "") {//2009-11-05 冯鑫统一修改 提交成功后，服务器响应消息
                    Ext.Msg.show({ title: SysConst.PROMPTINFOTEXT, msg: response.responseText, buttons: Ext.MessageBox.OK, icon: Ext.MessageBox.WARNING, minWidth: SysConst.MESSAGEBOXMINWIDTH });
                }
            },
            failure: function () {
                Ext.Msg.show({ title: SysConst.PROMPTINFOTEXT, msg: SysConst.OPERATEFAILUREINFOTEXT, buttons: Ext.MessageBox.OK, icon: Ext.MessageBox.INFO, minWidth: SysConst.MESSAGEBOXMINWIDTH });
            }
        });
    }
}

function speedConstrolUp(opt, dom) {
    log.info("松开云台控制按钮");
    dom.childNodes[0].style.display = "block";
    dom.childNodes[1].style.display = "none";
    //	    var deviceID = getCurrentWinDeviceID();
    var deviceID = getCurrentWinDeviceID();
    if (deviceID != null && deviceID != "") {
        Ext.Ajax.request({
            url: PROJECT_NAME + '/monitoring/PGISVideoManage.do?operation=speedConstrol',
            params: {
                commond: opt, detailOperation: 'stop', deviceID: deviceID,
                sysname: sysname, password: password, citizenid: citizenid, policeID: policeID
            },
            success: function (response) {
                if (response.responseText != "") {//2009-11-05 冯鑫统一修改 提交成功后，服务器响应消息
                    Ext.Msg.show({ title: SysConst.PROMPTINFOTEXT, msg: response.responseText, buttons: Ext.MessageBox.OK, icon: Ext.MessageBox.WARNING, minWidth: SysConst.MESSAGEBOXMINWIDTH });
                }
            },
            failure: function () {
                Ext.Msg.show({ title: SysConst.PROMPTINFOTEXT, msg: SysConst.OPERATEFAILUREINFOTEXT, buttons: Ext.MessageBox.OK, icon: Ext.MessageBox.INFO, minWidth: SysConst.MESSAGEBOXMINWIDTH });
            }
        });
    }
}

function lensConstrolDown(opt, dom) {
    log.info("按下镜头控制按钮,开始发送请求");
    dom.childNodes[0].style.display = "none";
    dom.childNodes[1].style.display = "block";
    //	    var deviceID = getCurrentWinDeviceID();
    var deviceID = getCurrentWinDeviceID();
    if (deviceID != null && deviceID != "") {
        Ext.Ajax.request({
            url: PROJECT_NAME + '/monitoring/PGISVideoManage.do?operation=lensConstrol',
            params: {
                commond: opt, detailOperation: 'start', lensSpeed: lensSpeed.getValue(), deviceID: deviceID,
                sysname: sysname, password: password, citizenid: citizenid, policeID: policeID
            },
            success: function (response) {
                if (response.responseText != "") {//2009-11-05 冯鑫统一修改 提交成功后，服务器响应消息
                    Ext.Msg.show({ title: SysConst.PROMPTINFOTEXT, msg: response.responseText, buttons: Ext.MessageBox.OK, icon: Ext.MessageBox.WARNING, minWidth: SysConst.MESSAGEBOXMINWIDTH });
                }
                log.info("按下镜头控制按钮调用完毕，Ajax请求成功!");
            },
            failure: function () {
                Ext.Msg.show({ title: SysConst.PROMPTINFOTEXT, msg: SysConst.OPERATEFAILUREINFOTEXT, buttons: Ext.MessageBox.OK, icon: Ext.MessageBox.INFO, minWidth: SysConst.MESSAGEBOXMINWIDTH });
            }
        });
    }
}

function lensConstrolUp(opt, dom) {
    log.info("松开镜头控制按钮,开始发送请求");
    dom.childNodes[0].style.display = "block";
    dom.childNodes[1].style.display = "none";
    //	    var deviceID = getCurrentWinDeviceID();
    var deviceID = getCurrentWinDeviceID();
    if (deviceID != null && deviceID != "") {
        Ext.Ajax.request({
            url: PROJECT_NAME + '/monitoring/PGISVideoManage.do?operation=lensConstrol',
            params: { commond: opt, detailOperation: 'stop', deviceID: deviceID, sysname: sysname, password: password, citizenid: citizenid, policeID: policeID },
            success: function (response) {
                if (response.responseText != "") {//2009-11-05 冯鑫统一修改 提交成功后，服务器响应消息
                    Ext.Msg.show({ title: SysConst.PROMPTINFOTEXT, msg: response.responseText, buttons: Ext.MessageBox.OK, icon: Ext.MessageBox.WARNING, minWidth: SysConst.MESSAGEBOXMINWIDTH });
                }
                log.info("松开镜头控制按钮调用完毕，Ajax请求成功!");
            },
            failure: function () {
                Ext.Msg.show({ title: SysConst.PROMPTINFOTEXT, msg: SysConst.OPERATEFAILUREINFOTEXT, buttons: Ext.MessageBox.OK, icon: Ext.MessageBox.INFO, minWidth: SysConst.MESSAGEBOXMINWIDTH });
            }
        });
    }
}
function contorlPanelDown(dom) {
    dom.childNodes[0].style.display = "none";
    dom.childNodes[1].style.display = "block";
}
function contorlPanelUp(dom) {
    dom.childNodes[0].style.display = "block";
    dom.childNodes[1].style.display = "none";
}
//预置位表格的'调用'按钮处理函数
function callPresetPosition() {
    var record = presetPositionGrid.getSelectionModel().getSelections();
    var sum = presetPositionGrid.getSelectionModel().getCount();
    var deviceID = getCurrentWinDeviceID();
    if (sum == 0) {
        Ext.Msg.show({ title: SysConst.PROMPTINFOTEXT, msg: "请选择一个预置位", buttons: Ext.MessageBox.OK, icon: Ext.MessageBox.WARNING, minWidth: SysConst.MESSAGEBOXMINWIDTH });
    } else if (sum > 1) {
        Ext.Msg.show({ title: SysConst.PROMPTINFOTEXT, msg: "每次只能调用一个预置位", buttons: Ext.MessageBox.OK, icon: Ext.MessageBox.WARNING, minWidth: SysConst.MESSAGEBOXMINWIDTH });
    } else {
        var id = record[0].get('id');
        Ext.Ajax.request({
            url: PROJECT_NAME + '/monitoring/PGISVideoManage.do?operation=presetPostionOperate',
            params: { detailOperation: 'call', id: id, deviceID: deviceID, sysname: sysname, password: password, citizenid: citizenid, policeID: policeID },
            success: function (response) {
                if (response.responseText != "") {//2009-11-05 冯鑫统一修改 提交成功后，服务器响应消息
                    Ext.Msg.show({ title: SysConst.PROMPTINFOTEXT, msg: response.responseText, buttons: Ext.MessageBox.OK, icon: Ext.MessageBox.WARNING, minWidth: SysConst.MESSAGEBOXMINWIDTH });
                }
            },
            failure: function () {
                Ext.Msg.show({ title: SysConst.PROMPTINFOTEXT, msg: SysConst.OPERATEFAILUREINFOTEXT, buttons: Ext.MessageBox.OK, icon: Ext.MessageBox.INFO, minWidth: SysConst.MESSAGEBOXMINWIDTH });
            }
        });
    }

}

//预置位表格的'添加'按钮处理函数
function addPresetPosition(btn, text) {
    if (btn == 'ok') {
        if (text == null || text == "") {
            log.info("未输入预置位名称,新增预置位失败");
            Ext.Msg.show({ title: SysConst.PROMPTINFOTEXT, msg: SysConst.VALIDATEFAILEDINFOTEXT, buttons: Ext.MessageBox.OK, icon: Ext.MessageBox.WARNING, minWidth: SysConst.MESSAGEBOXMINWIDTH });
        } else {
            var deviceID = getCurrentWinDeviceID();
            Ext.Ajax.request({
                url: PROJECT_NAME + '/monitoring/PGISVideoManage.do?operation=presetPostionOperate',
                paramse: { detailOperation: 'add', position: text, deviceID: deviceID, sysname: sysname, password: password, citizenid: citizenid, policeID: policeID },
                success: function (response) {
                    if (response.responseText != "") {//2009-11-05 冯鑫统一修改 提交成功后，服务器响应消息
                        Ext.Msg.show({ title: SysConst.PROMPTINFOTEXT, msg: response.responseText, buttons: Ext.MessageBox.OK, icon: Ext.MessageBox.WARNING, minWidth: SysConst.MESSAGEBOXMINWIDTH });
                    } else {
                        presetPositionGridStore.load({ params: { deviceID: deviceID, sysname: sysname, password: password, citizenid: citizenid, policeID: policeID } });
                    }
                },
                failure: function () {
                    Ext.Msg.show({ title: SysConst.PROMPTINFOTEXT, msg: SysConst.OPERATEFAILUREINFOTEXT, buttons: Ext.MessageBox.OK, icon: Ext.MessageBox.INFO, minWidth: SysConst.MESSAGEBOXMINWIDTH });
                }
            });
        }
    }
}

//定义新增预置位的对话框的面板
addPresetPosPanel = new Ext.form.FormPanel({
    bodyStyle: 'padding:20px;',
    frame: true,
    items: [{
        layout: 'form',
        items: [
		new Ext.form.NumberField({
		    fieldLabel: '预置位编号',
		    width: 190,
		    frame: true,
		    id: 'presetPosID',
		    name: 'presetPosID',
		    allowBlank: false,
		    maxValue: 255,
		    minValue: 1,
		    //validator:validatePosID,
		    labelSeparator: ''
		})]
    }, {
        layout: 'form',
        items: [
		new Ext.form.TextField({
		    fieldLabel: '预置位描述',
		    width: 190,
		    frame: true,
		    id: 'presetPosName',
		    name: 'presetPosName',
		    allowBlank: false,
		    blankText: '不许为空',
		    labelSeparator: ''
		})]
    }
    ]
});
function validatePosID(posID) {
    if (posID < 129) {
        return true;
    }
    else {
        return "预置位编号只能是两位数字";
    }
}

//显示新增预置位的对话框
function showAddPresetPosWin() {
    if (!addPresetPosWin) {//设置windows的属性
        addPresetPosWin = new Ext.Window({
            el: 'hello-win',
            layout: 'fit',
            width: 400,
            height: 180,
            title: '添加预置位',
            items: [addPresetPosPanel],
            closeAction: 'hide',
            plain: true,
            buttonAlign: 'center',
            //添加两个buttons对象
            buttons: [{
                text: '添加',
                handler: function () {
                    if (!addPresetPosPanel.form.isValid()) {
                        Ext.Msg.show({ title: SysConst.PROMPTINFOTEXT, msg: SysConst.VALIDATEFAILEDINFOTEXT, buttons: Ext.MessageBox.OK, icon: Ext.MessageBox.WARNING, minWidth: SysConst.MESSAGEBOXMINWIDTH });
                    } else {
                        var presetPosID = Ext.getCmp('presetPosID').getValue();
                        var presetPosName = Ext.getCmp('presetPosName').getValue();
                        var deviceID = getCurrentWinDeviceID();
                        log.info("presetPosID:" + presetPosID + " presetPosName:" + presetPosName + " deviceID:" + deviceID);
                        addPresetPosWin.hide();
                        var deviceID = getCurrentWinDeviceID();
                        //葛昊修正，判断是否有相同的预置位 2009-8-6 15:24:38
                        //var number=presetPositionGridStore.find("id",presetPosID); //葛昊 2009-8-12 8:55:27
                        var isHave = false;
                        presetPositionGridStore.findBy(function (record, id) {
                            if (record.get("id") == presetPosID) {
                                isHave = true;
                            }
                        }, this);
                        if (isHave) {
                            Ext.Msg.alert("提示", "已经有相同名称的预置位");
                            return;
                        }

                        Ext.Ajax.request({
                            url: PROJECT_NAME + '/monitoring/PGISVideoManage.do?operation=presetPostionOperate',
                            params: { detailOperation: 'add', presetPosID: presetPosID, presetPosName: presetPosName, deviceID: deviceID, sysname: sysname, password: password, citizenid: citizenid, policeID: policeID },
                            success: function (response) {
                                if (response.responseText != "") {//2009-11-05 冯鑫统一修改 提交成功后，服务器响应消息
                                    Ext.Msg.show({ title: SysConst.PROMPTINFOTEXT, msg: response.responseText, buttons: Ext.MessageBox.OK, icon: Ext.MessageBox.WARNING, minWidth: SysConst.MESSAGEBOXMINWIDTH });
                                } else {
                                    presetPositionGridStore.load({ params: { deviceID: deviceID, sysname: sysname, password: password, citizenid: citizenid, policeID: policeID } });
                                }
                            },
                            failure: function () {
                                Ext.Msg.show({ title: SysConst.PROMPTINFOTEXT, msg: SysConst.OPERATEFAILUREINFOTEXT, buttons: Ext.MessageBox.OK, icon: Ext.MessageBox.INFO, minWidth: SysConst.MESSAGEBOXMINWIDTH });
                            }
                        });
                    }
                }
            }, {
                text: '取消',
                handler: function () {
                    addPresetPosPanel.form.reset();
                    addPresetPosWin.hide();
                }
            }]
        });
    }
    addPresetPosPanel.form.reset();
    addPresetPosWin.show();
};


//预置位表格的'删除'按钮处理函数
function delePresetPosition() {
    var record = presetPositionGrid.getSelectionModel().getSelections();
    //2009-09-26 冯鑫修改 添加删除提示
    if (record.length < 1) {///////
        Ext.Msg.show({ title: SysConst.PROMPTINFOTEXT, msg: SysConst.SELECTRECORDTODELETEINFOTEXT, buttons: Ext.MessageBox.OK, icon: Ext.MessageBox.WARNING, minWidth: SysConst.MESSAGEBOXMINWIDTH });///////
    } else {///////

        var sum = presetPositionGrid.getSelectionModel().getCount();
        var deviceID = getCurrentWinDeviceID();
        var deleteDevice = "";
        var msg = "";
        for (i = 0; i < sum; i = i + 1) {
            var id = record[i].get('id');
            //1,2,15,16,17号预置位为内置预置位，不允许删除,2010年3月26日9:13:30 zhf增加
            if (id == "1" || id == "2" || id == "15" || id == "16" || id == "17") {
                Ext.Msg.alert("提示", "1,2,15,16,17号预置位为保留预置位，不允许删除");
                return;



            }
            else {
                deleteDevice = deleteDevice + id + ",";
            }
        }

        Ext.Msg.confirm(SysConst.PROMPTINFOTEXT, SysConst.DELETECONFIRMINFOTEXT, function (_btn) {///////
            if (_btn == 'no') {///////
                return;///////
            } else {///////

                deleteDevice = deleteDevice.substring(0, deleteDevice.length - 1);
                Ext.Ajax.request({
                    url: PROJECT_NAME + '/monitoring/PGISVideoManage.do?operation=delPresetPositon',
                    params: { detailOperation: 'delete', id: deleteDevice, deviceID: deviceID, sysname: sysname, password: password, citizenid: citizenid, policeID: policeID },
                    success: function (response) {
                        if (response.responseText != "") {//2009-11-05 冯鑫统一修改 提交成功后，服务器响应消息
                            Ext.Msg.show({ title: SysConst.PROMPTINFOTEXT, msg: response.responseText, buttons: Ext.MessageBox.OK, icon: Ext.MessageBox.WARNING, minWidth: SysConst.MESSAGEBOXMINWIDTH });
                        } else {
                            presetPositionGridStore.load({ params: { deviceID: deviceID, sysname: sysname, password: password, citizenid: citizenid, policeID: policeID } });
                        }
                    },
                    failure: function () {
                        Ext.Msg.show({ title: SysConst.PROMPTINFOTEXT, msg: SysConst.OPERATEFAILUREINFOTEXT, buttons: Ext.MessageBox.OK, icon: Ext.MessageBox.INFO, minWidth: SysConst.MESSAGEBOXMINWIDTH });
                    }
                });
            }//end of second else
        })//end of ext.msg.confirm
        //presetPositionGridStore.load({params:{deviceID:deviceID}});

    }//end of first else
}//end of delePresetPosition()

//开关控制区，开启，关闭按钮的处理函数
function switchOperate(btn) {
    var opt;
    var id;
    if (btn.text == '开启') {
        opt = 'open';
    } else {
        opt = 'close';
    }
    var index = switchCombo.store.find('value', switchCombo.getValue());
    id = switchCombo.store.collect('id')[index];
    var deviceID = getCurrentWinDeviceID();
    if (deviceID != null && deviceID != "") {
        Ext.Ajax.request({
            url: PROJECT_NAME + '/monitoring/PGISVideoManage.do?operation=switchOperate',
            params: { detailOperation: opt, switchid: id, deviceID: deviceID, sysname: sysname, password: password, citizenid: citizenid, policeID: policeID },
            success: function (response) {
                if (response.responseText != "") {//2009-11-05 冯鑫统一修改 提交成功后，服务器响应消息
                    Ext.Msg.show({ title: SysConst.PROMPTINFOTEXT, msg: response.responseText, buttons: Ext.MessageBox.OK, icon: Ext.MessageBox.WARNING, minWidth: SysConst.MESSAGEBOXMINWIDTH });
                }
            },
            failure: function () {
                Ext.Msg.show({ title: SysConst.PROMPTINFOTEXT, msg: SysConst.OPERATEFAILUREINFOTEXT, buttons: Ext.MessageBox.OK, icon: Ext.MessageBox.INFO, minWidth: SysConst.MESSAGEBOXMINWIDTH });
            }
        });
    }
}













//各窗口的流标识，含实时和历史流的流标识
var subjectArray = ["", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""];
//播放器的流标识
var playerSubject = "";
//播放器的播放状态
var playerStatus = "no";
var sipPluginPort = "5022";
var PROJECT_NAME = '/PMPlatForm';
var curWinDeviceID = "";
//当前用户选中的窗口
var currentChooseWindow = 1;



/*****************************************************播放实时视频******************************************************/
//id 设备ID
function playVideo(sysname, citizenid, password, id, policeID) {
    if (vlcplayer.url == undefined) {
        return;
    }
    vlcplayer.JsSleep();
    setTimeout(function () { vlcplayer.JsWakeUp(); }, 2000);
    var status = playerStatus;//获得播放状态
    var deviceID = id;
    playerStatus = "rtm_play";  //设置播放状态
    var url = PROJECT_NAME + "/monitoring/PGISVideoManage.do?operation=playRealtimeVideo"
														+ "&deviceID=" + deviceID
														+ "&sysname=" + sysname
														+ "&citizenid=" + citizenid
														+ "&password=" + password
														+ "&policeID=" + policeID;
    var successFunc = function (response) {
        var text = response.responseText;
        if (text != "") {
            alert(text);
        }
    };
    var failureFunc = function () {
        log.info('点播失败，与管理平台服务器连接异常');
    };
    sendAjax(url, successFunc, failureFunc);
}// end of playVideo

/*****************************************************停止指定窗口的实时视频*********************************************/
function stopRealtimeVideo(sysname, citizenid, password, winID, policeID, mode) {
    log.info("当前要停止的视频的窗口是：" + winID);
    //stopPlayerStream(winID);
    /*if(subjectArray[winID]==""){
 		return;
 	}*/
    subjectArray[winID] = "";

    var url = PROJECT_NAME + "/monitoring/PGISVideoManage.do?operation=stopRealtimeVideo"
														+ "&sysname=" + sysname
														+ "&citizenid=" + citizenid
														+ "&winID=" + winID
														+ "&password=" + password
														+ "&policeID=" + policeID
														+ "&onlySetWinStatus=" + mode;
    var successFunc = function (response) {
    };
    var failureFunc = function () {
        //alert('断开实时点播失败，与管理平台服务器连接异常');
    };
    sendAjax(url, successFunc, failureFunc);
    playerStatus = "no";
}//end of stopRealtimeVideo

/****************************************************停止播放器的流*****************************************************/
function stopPlayerStream(winID) {
    if (vlcplayer.url == undefined) {
        return;
    }
    var curWinID = vlcplayer.windowID;
    vlcplayer.windowID = winID;
    vlcplayer.StopStream(winID);
    vlcplayer.windowID = curWinID;
}
/************************************************浏览器收到SIP插件smp消息后的处理函数*******************************************/
function smp(commandOfSmp) {

    if (vlcplayer.url == undefined) {
        return;
    }
    log.info("收到的插件的smp是：" + commandOfSmp);
    var array = commandOfSmp.split(",");
    array = array[4].split(":");
    var port = array[1];

    if (port == '9910')
        return; //端口为9910,为下载，不播放



    var win_id = 1;
    for (var i = 1; i < 17; i++) {
        if (vlcplayer.getRecvPort(i) == port) {
            win_id = i;
            break;
        }
    }
    //var ip = LocalIP ;   //mainTab.jsp中定义的全局变量，是客户端的本地IP
    //2010.11.28 by liufeng @ wuhu
    array = commandOfSmp.split(",");
    array = array[3].split(":");
    var ip = array[1];

    var url = "udp://@" + ip + ":" + port;
    vlcplayer.windowID = win_id;  //把播放器窗口设置为smp中包含的窗口号
    vlcplayer.url = url;
    log.info("赋给播放器的url是：" + url + "将当前窗口设置为：" + win_id);
}// end of smp
/************************************************浏览器收到SIP插件subject消息后的处理函数***************************************/

function subject(subjectString) {
    if (subjectString != "") {
        var array = subjectString.split(",");
        var array2 = subjectString.split(":");
        curWinDeviceID = array2[0];//当前点播设备的ID
        log.info("当前点播设备的ID是：" + curWinDeviceID);
        array = array[1].split(":");
        var id = array[1];
        id = parseInt(id); //窗口ID    
        subjectArray[id] = subjectString;  //设置该窗口的流标识


        loadPresetAndSwitch();
    }

}// end of subject


/************************************************收到SIP插件bye消息后的处理函数*******************************************/
function onByeReceive(commandOfBye) {

    log.info("in onByeReceive");
    var array = commandOfBye.split(",");
    array = array[1].split(":");
    var win_ID = parseInt(array[1]);
    stopPlayerStream(win_ID);
    log.info("停掉流的窗口ID是：" + win_ID);
    subjectArray[win_ID] = "";   //用于非手动停止的流，比如电子巡逻

    //清空预置位信息
    clearPresetAndSwitch();
    stopRealtimeVideo(sysname, citizenid, password, win_ID, policeID, "onlySetWinStatus");
}// end of onByeReceive
/***********************************************普通的SIP消息处理函数**************************************************/
function MsgDispose(msg) {
    var str = msg;
    if ("XMLMSG" == str.substring(0, 6)) {
        var jsonStr = str.substring(6);
        var jsonData = eval("(" + jsonStr + ")");

        var msgType = jsonData.msgType;
        var msgContent;
        switch (parseInt(msgType)) {
            case 35:
                //播放器OSD信息
                if (vlcplayer.url == undefined) {
                    return;
                }

                vlcplayer.windowID = jsonData.playerWinID;
                vlcplayer.OSDCameraID = jsonData.OSDCameraID;
                vlcplayer.OSD = jsonData.OSD;
                break;
        }
    }
}
/*******************************************告知服务器sip插件的端口******************************************************/
function setSIPPluginPort(port) {
    var url = PROJECT_NAME + "/specialLogon.do?operation=setSIPPort" + "&port=" + port;
    var successFunc = function (response) {
    };
    var failureFunc = function () {
    };
    sendAjax(url, successFunc, failureFunc);
}// end of  setSIPPluginPort

function saveSIPPluginPort(port) {
    sipPluginPort = port;
}

function getSIPPluginPort(port) {
    return sipPluginPort;
}
/** 
	 * function setPGISParameter 设置PGIS访问平台的全局变量
	 * @param mapping PGIS登录标识
	 * @param form PGIS密码
	 * @param request 用户的身份证号码
	 */
function setPGISParameter(sysname, citizenid, password, policeID) {
    PGIS_sysname = sysname;
    PGIS_citizenid = citizenid;
    PGIS_password = password;
    //2014年1月23日16:45:57 新增警号
    PGIS_policeID = policeID;
}
/*******************************************登录管理平台*****************************************************************/
function playVideoOfPMPlatform(sysname, citizenid, password, deviceID, policeID, onlyLogin) {

    var url = PROJECT_NAME + "/specialLogon.do?sysname=" + sysname + "&citizenid=" + citizenid
			 + "&password=" + password + "&port=" + sipPluginPort + "&policeID=" + policeID;

    var successFunc = function (response) {
        var text = response.responseText;
        if (text != null && text != "") {
            var tmp = text.split(",");
            if (tmp[0] == "success") {
                var userID = tmp[1];
                log.info("收到平台返回的用户ID:" + userID);
                UActr.uname = userID;
                sendHeartBeats();
                if (onlyLogin != true) {
                    playVideo(sysname, citizenid, password, deviceID, policeID);
                }
                //设置PGIS访问平台的全局变量
                //setPGISParameter(sysname,citizenid,password,policeID);
                log.info("登录管理平台成功，并点播视频");
            }
            else if (tmp[0] == "online") {
                var userID = tmp[1];
                //alert("收到平台返回的用户ID:"+userID);
                UActr.uname = userID;
                sendHeartBeats();
                if (onlyLogin != true) {
                    playVideo(sysname, citizenid, password, deviceID, policeID);
                }
                //设置PGIS访问平台的全局变量
                //setPGISParameter(sysname,citizenid,password,policeID);
                //alert("已经在线，直接点播视频");
            }
            else {
                log.info("in successFunc,response is:" + text);
                alert(text);
            }
        }
    };
    var failureFunc = function () {
        log.info('登录失败，与管理平台服务器连接异常');
        //alert('登录失败，与管理平台服务器连接异常');
    };
    sendAjax(url, successFunc, failureFunc);
}

/*******************************************登出管理平台*****************************************************************/
function logoutPMPlatform(sysname, citizenid, password, policeID) {
    var url = PROJECT_NAME + "/monitoring/PGISVideoManage.do?operation=logout"
														+ "&sysname=" + sysname
														+ "&citizenid=" + citizenid
														+ "&password=" + password
														+ "&policeID=" + policeID;
    var successFunc = function (response) {
        var text = response.responseText;
        if (text == "success") {
            //alert("登出管理平台成功！");
        }
        else {
            //alert('登出管理平台失败!');
        }
    };
    var failureFunc = function () {
        log.info('登出管理平台失败，与管理平台服务器连接异常');
    };
    sendAjax(url, successFunc, failureFunc);
}

/*******************************************发送ajax请求*****************************************************************/
function sendAjax(url, successFunc, failureFunc) {
    var xmlHttp;
    if (window.ActiveXObject) {
        xmlHttp = new ActiveXObject("Microsoft.XMLHTTP")
    }
    else if (window.XMLHttpRequest) {
        xmlHttp = new XMLHttpRequest()
    }


    xmlHttp.open("POST", url, true);

    xmlHttp.onreadystatechange = function () {
        try {
            if ((xmlHttp.readystate == 4) && (xmlHttp.status == 200)) {
                if (successFunc) {
                    successFunc(xmlHttp);
                }
            }
            else {
                if (failureFunc) {
                    failureFunc();
                }
            }
        }
        catch (err) {
            log.info(err);

        }
    }
    xmlHttp.send();
}

function contorlPanelDown(dom) {
    dom.childNodes[0].style.display = "none";
    dom.childNodes[1].style.display = "block";
}
function contorlPanelUp(dom) {
    dom.childNodes[0].style.display = "block";
    dom.childNodes[1].style.display = "none";
}
function onClose() {
    //stopPlayerStream();
    //关闭页面时，注销当前使用的用户		
    logoutPMPlatform(sysname, citizenid, password, policeID);
    UActr.Close();
    sleep(500);


    //alert("关闭视频播放页面");

}
function onPlayControl(opt) {
    switch (opt) {
        case "play":
            if (curWinDeviceID != "") {
                playVideo(sysname, citizenid, password, curWinDeviceID, policeID);
            }
            break;
        case "stop":
            //alert("停止"); 			
            stopRealtimeVideo(sysname, citizenid, password, getcurrentChooseWindow(), policeID);
            break;

        case "logout":
            //alert("登出管理平台");
            logoutPMPlatform(sysname, citizenid, password, policeID);
            break;

        case "login":
            playVideoOfPMPlatform(sysname, citizenid, password, "", policeID, true);
            break;


        case "history":
            if (!VideoForPGIS_HistoryWin) {
                var vs = new AXY.Monitoring.VideoSearch({
                    rowDoubleClickHandler: function () {
                        //此处添加双击处理逻辑
                        var selectedRec = this.getSelectedRecord()[0];
                        var prerecordTime = selectedRec.get("prerecordTime"); //预录时长
                        var startTimeValue = selectedRec.get("startTime");
                        var stopTimeValue = selectedRec.get("stopTime");
                        var sourceDeviceValue = selectedRec.get("sourceDeviceID");
                        var storageServerValue = selectedRec.get("storageServerID");
                        var sourceDeviceName = selectedRec.get("sourceDevice");
                        var sort = selectedRec.get("sort");
                        log.info("录像类型为:" + sort);
                        log.info("存储服务器ID 保存在数组中，storageServerValue：" + storageServerValue);
                        log.info("startTimeValue:" + startTimeValue);
                        log.info("stopTimeValue:" + stopTimeValue);
                        playHistoryVideo4GIS(startTimeValue, stopTimeValue, prerecordTime, sourceDeviceValue, sourceDeviceName, storageServerValue, sort);


                    },
                    getPageBar: function () {
                        return new Ext.PagingToolbar({
                            store: this.resultGridStore,
                            pageSize: this.pageSize,
                            displayInfo: false,
                            beforePageText: "第",
                            emptyMsg: "没有查询到记录"
                        });
                    }
                });

                VideoForPGIS_HistoryWin = new Ext.Window({
                    title: '',
                    layout: 'fit',
                    width: 610,
                    height: 506,
                    closeAction: 'hide',
                    items: vs
                });
            }
            VideoForPGIS_HistoryWin.show();
            break;

    }
}

function sleep(n) {
    var start = new Date().getTime();
    while (true) if (new Date().getTime() - start > n) break;
}

function getCurrentWinDeviceID() {
    return curWinDeviceID;
}

//从服务器端加载设备的预置位
function loadPresetAndSwitch() {
    var deviceID = curWinDeviceID;
    presetPositionGridStore.load({ params: { deviceID: deviceID, sysname: sysname, password: password, citizenid: citizenid, policeID: policeID } });
    switchCombo.store.load({ params: { deviceID: deviceID, sysname: sysname, password: password, citizenid: citizenid, policeID: policeID } });
}

function clearPresetAndSwitch() {
    presetPositionGridStore.removeAll();
    switchCombo.store.removeAll();
}
/***********************************************设置/获得用户当前选中的窗口***********************************************/
function setcurrentChooseWindow(win_id) {
    currentChooseWindow = win_id;
}
function getcurrentChooseWindow() {
    return currentChooseWindow;
}

/***********************************************处理用户点击VLC插件窗口的事件***********************************************/
function onClickVLCWindow() {
    log.info("in VideoForPGIS player2.js onClickVLCWindow");
    var newChooseWindow = vlcplayer.JsManualWinID();
    log.info("用户选择的当前窗口是：" + newChooseWindow);
    if (newChooseWindow != getcurrentChooseWindow()) {
        setcurrentChooseWindow(newChooseWindow);//设置用户当前选中的窗口
        var subjectStr = subjectArray[newChooseWindow];
        log.info("in VideoForPGIS player2.js onClickVLCWindow,subjectStr:" + subjectStr);
        if (subjectStr != "") {
            var array = subjectStr.split(",");
            array = array[0].split(":");
            var deviceID = array[0];//从用户新选择的窗口的媒体标识中解析出摄像机ID
            log.info("用户选择的当前窗口是：" + newChooseWindow + "对应的摄像机是：" + deviceID);
            curWinDeviceID = deviceID;//设置当前设备为上述摄像机ID
            loadPresetAndSwitch();//加载当前设备的预置位
        }
        else {
            clearPresetAndSwitch();
        }
    }
}

/*******************************************************获得客户端IP***********************************************/
function getLocalIPAddr() {
    var oSetting = null;
    var ip = "";
    try {
        oSetting = new ActiveXObject("rcbdyctl.Setting");
        ip = oSetting.GetIPAddress;

        if (ip.length == 0) {
            return "没有连接到网络";
        }
        oSetting = null;
    } catch (e) {

    }
    log.info("客户端IP是：" + ip);
    return ip;
}
//向服务器端发送Ajax心跳消息，by zhf 2012年8月16日15:41:32
function sendHeartBeats() {
    log.info("告知服务器端客户端心跳");
    Ext.Ajax.request({
        url: PROJECT_NAME + '/specialLogon.do',
        params: { operation: 'sendHearBeats' }
    });
    setTimeout(function () { sendHeartBeats(); }, 60000);
}

//startTime 视频流的开始时间
//stopTime 视频流的结束时间
//prerecordTime 预录时长
//sourceDevice 摄像机的设备ID
//storageServerID 存储服务器的设备ID（一个域内可能包含多个存储服务器）
//sourceDeviceName 摄像机的名称（2008年11月20日，增加了一个name的参数，为了历史点播的时候传递设备名称，以显示在播放器窗口上）
//sort媒体类型
function playHistoryVideo4GIS(startTime, stopTime, prerecordTime, sourceDevice, sourceDeviceName, storageServerID, sort) {
    log.info("in player2.js playHistoryVideo4GIS");
    var url = PROJECT_NAME + "/monitoring/PGISVideoManage.do?operation=playback"
                                                + "&sysname=" + sysname
                                                + "&citizenid=" + citizenid
                                                + "&password=" + password
                                                + "&policeID=" + policeID;
    if (window.vlcplayer) {
        //log.info("媒体播放器已加载");
    } else {
        //log.info("媒体播放器还未加载，加载媒体播放器");	
        showPlayWin();
    }
    //如果经过上述处理，播放器仍未加载，说明用户没有权限或出现了其他问题，则直接返回
    if (!window.vlcplayer) {
        return;
    }

    /********************************************************************************************************/
    /*说明：  （1）给PGIS的实时视频，是服务器自动选择空闲窗口的。按照统一的风格，PGIS历史视频回放也应该是服务器自动选择空闲窗口的。
             （2）目前PGIS历史视频回放使用服务器自动选择空闲窗口可以正常运行。但如果加入进度控制可能会有问题，
             （3）此处注释掉的代码为以前采用当前窗口播放历史视频的代码。直接设置win_ID为1  2011年3月17日14:41:13  zhf  */

    /*//获得当前窗口
    var  win_ID = vlcplayer.windowID;
    playBackType[win_ID]="common";
    //锁定播放器的当前窗口
    vlcplayer.JsSleep();
    //延时5秒钟后，释放播放器当前窗口，目的是避免流如果没有被点播出来，造成窗口死锁
    setTimeout(function(){vlcplayer.JsWakeUp();},3000);*/
    var win_ID = 1;
    /********************************************************************************************************/


    /***************解析视频流的开始时间和结束时间，并计算视频流的长度***************/
    var period = 0;
    var sTime = new Date();
    var eTime = sTime;
    if (startTime != null && startTime != "") {
        sTime = new Date(startTime.split("-").join("/"));
    }
    if (stopTime != null && stopTime != "") {
        eTime = new Date(stopTime.split("-").join("/"));
    }
    log.info("startTime:" + startTime + "stopTime" + stopTime);
    log.info("起始时间：" + sTime + "结束时间:" + eTime);
    var num_prerecordTime = new Number(prerecordTime); //字符型转换成数值才能进行算数加运算
    /********************************************************************************************************/
    /*说明：  （1）给PGIS的实时视频，是服务器自动选择空闲窗口的。按照统一的风格，PGIS历史视频回放也应该是服务器自动选择空闲窗口的。
             （2）目前PGIS历史视频回放使用服务器自动选择空闲窗口可以正常运行。但如果加入进度控制可能会有问题，
             （3）此处注释掉的代码为以前采用当前窗口播放历史视频的代码。2011年3月17日14:41:13  zhf  */
    //periodArray[win_ID] = (eTime.getTime()-sTime.getTime())/1000 + num_prerecordTime; 

    //log.info("窗口"+win_ID+"播放的历史视频的时长是（含预录时间）："+periodArray[win_ID] + "，预录时长：" +prerecordTime+",录像类型:"+sort);
    /********************************************************************************************************/
    /*********************向服务器端发送建立历史视频链接的请求*******************/
    Ext.Ajax.request({
        url: url,
        params: {
            startTime: startTime,
            stopTime: stopTime,
            prerecordTime: prerecordTime,
            sourceDevice: sourceDevice,
            storageServerID: storageServerID,
            sort: sort,
            win_ID: win_ID
        },
        success: function (response) {
            if (response.responseText != "") {//2009-10-05 冯鑫统一修改 提交成功后，服务器相应消息
                Ext.Msg.show({ title: SysConst.PROMPTINFOTEXT, msg: response.responseText, buttons: Ext.MessageBox.OK, icon: Ext.MessageBox.WARNING, minWidth: SysConst.MESSAGEBOXMINWIDTH });
            }
            //2009年9月26日18:48:15 by liufeng 注释掉下行
            //subject(text);//目前统一在SIP插件收到invite消息后调用subject方法，此处调用可不要
        },
        failure: function (form, action) {
            Ext.Msg.show({ title: SysConst.PROMPTINFOTEXT, msg: SysConst.OPERATEFAILUREINFOTEXT, buttons: Ext.MessageBox.OK, icon: Ext.MessageBox.INFO, minWidth: SysConst.MESSAGEBOXMINWIDTH });
        }
    });
    /********************************************************************************************************/
    /*说明：  （1）给PGIS的实时视频，是服务器自动选择空闲窗口的。按照统一的风格，PGIS历史视频回放也应该是服务器自动选择空闲窗口的。
             （2）目前PGIS历史视频回放使用服务器自动选择空闲窗口可以正常运行。但如果加入进度控制可能会有问题，
             （3）此处注释掉的代码为以前采用当前窗口播放历史视频的代码。2011年3月17日14:41:13  zhf  */
    /*//此处的状态设置是为了控制滑竿，不让滑竿立即开始滑动，而是在插件收到smp消息后，才将状态置为"htm_play"，开始滑动
    statusArray[win_ID] = "htm_stop";
    ////showPlayWin();
    //把此窗口对应的滑竿位置放在起始0位置
    posArray[win_ID]=0;	
    deviceNameArray[win_ID] = sourceDeviceName;
    speedArray[win_ID]=1.0;
    storageServerIDArray[vlcplayer.windowID] = storageServerID; 
    log.info("存储服务器标识存储完毕");*/
    /********************************************************************************************************/
}