有BarMenuItems，BottomToolMenuItems，RightToolMenuItems，ScaleViewModelMenu四个MenuName分别对应顶部菜单栏，底部工具栏，右侧工具栏和比例尺。需要配置的工具模块分别设置对应的MenuName属性和其他模块具有的各自的属性。（如下每个以“{”开头，以“}”结尾的部分为一项）
在权限管理页面配置的时候需要配置的第一栏配置名称需以“ShellMenu_”开头加上自定义名称，在第四栏配置名称中需要选择ShellModel下对应的BarMenu,RightToolMenu,BottomToolMenu或者ScaleViewModelMenu。

//BarMenuItems

	{   "$type": "Mmc.Mspace.CityViewModule.CityView.CityViewViewModel, Mmc.Mspace.CityViewModule","Type": "CityViewViewModel","Content": "Cityoverview","Icon": "ShellM_City.png","MouseOverIcon": "ShellM_City_H.png","PressedOverIcon": "ShellM_City_C.png","Background": "#ff3a89ff","GroupName": "城市总览","ToolTip":"城市总览","MenuName":"BarMenuItems"}
	{   "$type": "Mmc.Mspace.CityViewModule.CityView.CityViewViewModel, Mmc.Mspace.CityViewModule","Type": "CityViewViewModel","Content": "UAV","Icon": "ShellM_Uav.png","MouseOverIcon": "ShellM_Uav_H.png","PressedOverIcon": 	"ShellM_Uav_C.png", "Background": "#ff3a89ff","GroupName": "UavModule","ToolTip":"无人机","MenuName":"BarMenuItems" }
	{   "$type": "Mmc.Mspace.CityViewModule.CityView.CityViewViewModel, Mmc.Mspace.CityViewModule","Type": "CityViewViewModel","Content": "Intelligentanalysis","Icon": "ShellM_Analysis.png","MouseOverIcon": "ShellM_Analysis_H.png","PressedOverIcon": "ShellM_Analysis_C.png", "Background": "#ff3a89ff","GroupName": "IntelligentAnalysis","ToolTip":"智能分析","MenuName":"BarMenuItems" }
    {   "$type": "Mmc.Mspace.CityViewModule.CityView.CityViewViewModel, Mmc.Mspace.CityViewModule","Type": "CityViewViewModel","Content": "RegularInspection","Icon": "ShellM_Inspection.png","MouseOverIcon": "ShellM_Inspection_H.png","PressedOverIcon": "ShellM_Inspection_C.png", "Background": "#ff3a89ff","GroupName": "RegularInspection","ToolTip":"常态化巡检","MenuName":"BarMenuItems" }
	{   "$type": "Mmc.Mspace.CityViewModule.CityView.CityViewViewModel,Mmc.Mspace.CityViewModule","Type":"CityViewViewModel","Content":"Smartdevices","Icon": "ShellM_SmartDevices.png","MouseOverIcon":"ShellM_SmartDevices_H.png","PressedOverIcon":"ShellM_SmartDevices_C.png","Background":"#ff3a89ff","GroupName":"Cityoverview","MenuName":"BarMenuItems" }



//BottomToolMenuItems

{"$type": "FireControlModule.ReturnOrigin.ReturnOriginViewModel, FireControlModule","Type": "ReturnOriginViewModel","Content": "BackStart","Icon": "ShellB_BackStart.png","PressedOverIcon":"ShellB_BackStart_C.png","MouseOverIcon": "ShellB_BackStart_H.png","ToolTip":"回到原点","MenuName":"BottomToolMenuItems"}
{"$type": "Mmc.Mspace.ToolModule.LocatingViewModel, Mmc.Mspace.ToolModule","Type": "LocatingViewModel","Content": "Location","Icon":"ShellB_Location.png","PressedOverIcon":"ShellB_Location_C.png","MouseOverIcon": "ShellB_Location_H.png","ToolTip":"坐标定位", "MenuName":"BottomToolMenuItems"}
{"$type": "Mmc.Mspace.ToolModule.Search.KeyWordSearchViewModel, Mmc.Mspace.ToolModule","Type": "KeyWordSearchViewModel","Content": "Query","Icon":"ShellB_Query.png","PressedOverIcon":"ShellB_Query_C.png","MouseOverIcon": "ShellB_Query_H.png","ToolTip":"查询","MenuName":"BottomToolMenuItems"}

{"$type": "Mmc.Mspace.ToolModule.ViewControl.HMeasurationViewModel,Mmc.Mspace.ToolModule","Type": "HMeasurationViewModel","Content": "Levelmeasurement","Icon": "ShellB_HMeasuration.png","PressedOverIcon":"ShellB_HMeasuration_C.png","MouseOverIcon": "ShellB_HMeasuration_H.png","ToolTip":"水平测量","GroupName": "CommomTool","MenuName":"BottomToolMenuItems"}
{"$type": "Mmc.Mspace.ToolModule.ViewControl.VMeasurationViewModel,Mmc.Mspace.ToolModule","Type": "VMeasurationViewModel","Content": "Verticalmeasurement","Icon": "ShellB_VMeasuration.png","MouseOverIcon": "ShellB_VMeasuration_H.png","ToolTip":"垂直测量","PressedOverIcon":"ShellB_VMeasuration_C.png","GroupName": "CommomTool","MenuName":"BottomToolMenuItems"}
{"$type": "Mmc.Mspace.ToolModule.ViewControl.AreaMeasurationViewModel, Mmc.Mspace.ToolModule","Type": "AreaMeasurationViewModel","Content": "Areameasurement","Icon": "ShellB_AreaMeasuration.png","MouseOverIcon": "ShellB_AreaMeasuration_H.png","PressedOverIcon":"ShellB_AreaMeasuration_C.png","ToolTip":"面积测量","GroupName": "CommomTool","MenuName":"BottomToolMenuItems"}

{"$type": "Mmc.Mspace.ToolModule.ViewControl.VolumeCalcViewModel, Mmc.Mspace.ToolModule","Type": "VolumeCalcViewModel","Content": "Volumecalculating","Icon": "ShellB_VolumeCalculating.png","MouseOverIcon": "ShellB_VolumeCalculating_H.png","PressedOverIcon":"ShellB_VolumeCalculating_C.png","ToolTip":"体积测算","GroupName":"CommomTool","MenuName":"BottomToolMenuItems"}
{"$type": "Mmc.Mspace.ToolModule.ViewControl.TopViewViewModel, Mmc.Mspace.ToolModule","Type": "TopViewViewModel","Content": "Topview","Icon": "ShellB_TopView.png","PressedOverIcon":"ShellB_TopView_C.png","MouseOverIcon": "ShellB_TopView_H.png",ToolTip:"顶视","MenuName":"BottomToolMenuItems"}
{"$type": "Mmc.Mspace.ToolModule.ViewControl.PerspectiveViewModel, Mmc.Mspace.ToolModule","Type": "PerspectiveViewModel","Content": "Lookingdown","Icon": "ShellB_LookingDown.png","MouseOverIcon": "ShellB_LookingDown_H.png","PressedOverIcon":"ShellB_LookingDown_C.png","ToolTip":"俯视","MenuName":"BottomToolMenuItems"}

{"$type": "Mmc.Mspace.ToolModule.ViewControl.LookAtNorthViewModel, Mmc.Mspace.ToolModule","Type": "LookAtNorthViewModel","Content": "Northarrow","Icon": "ShellB_Northarrow.png","PressedOverIcon":"ShellB_Northarrow_C.png","MouseOverIcon": "ShellB_Northarrow_H.png","ToolTip":"指北","MenuName":"BottomToolMenuItems"}
{"$type": "Mmc.Mspace.ToolModule.ViewControl.PersonAngleViewModel, Mmc.Mspace.ToolModule","Type": "PersonAngleViewModel","Content": "Peopleview","Icon": "ShellB_Peopleview.png","MouseOverIcon": "ShellB_Peopleview_H.png","PressedOverIcon":"ShellB_Peopleview_C.png","ToolTip":"人视角","GroupName": "CommomTool","MenuName":"BottomToolMenuItems"}
{"$type": "Mmc.Mspace.ToolModule.PaintViewModel, Mmc.Mspace.ToolModule","Type": "PaintViewModel","Content": "Drawingboard","Icon": "ShellB_Drawingboard.png","PressedOverIcon":"ShellB_Drawingboard_C.png","MouseOverIcon": "ShellB_Drawingboard_H.png","ToolTip":"画板","GroupName": "CommomTool","MenuName":"BottomToolMenuItems"}
{"$type": "Mmc.Mspace.ToolModule.ViewControl.FullScreenViewModel, Mmc.Mspace.ToolModule","Type": "FullScreenViewModel","Content": "Fullscreen","Icon": "ShellB_FullScreen.png","PressedOverIcon":"ShellB_FullScreen_C.png","MouseOverIcon": "ShellB_FullScreen_H.png","ToolTip":"全屏","MenuName":"BottomToolMenuItems"}




//RightToolMenuItems

{"$type": "Mmc.Mspace.RoutePlanning.RoutePlanViewModel, Mmc.Mspace.RoutePlanning","Type": "RoutePlanViewModel","Content": "Pathplanning","Icon": "ShellR_PathPlanning.png","MouseOverIcon": "ShellR_PathPlanning_H.png","PressedOverIcon":"ShellR_PathPlanning_C.png","ToolTip":"航迹显示","Visible":"false","GroupName": "UavModule","MenuName":"RightToolMenuItems"}
{"$type": "Mmc.Mspace.UavModule.UavTracing.UavListViewModel,Mmc.Mspace.UavModule","Type": "UavListViewModel","Content": "Traildisplay","Icon": "ShellR_TrailDisplay.png","MouseOverIcon": "ShellR_TrailDisplay_H.png","PressedOverIcon":"ShellR_TrailDisplay_C.png","ToolTip":"航线规划","Visible":"false","GroupName": "UavModule","MenuName":"RightToolMenuItems"}

{"$type": "Mmc.Mspace.IntelligentAnalysisModule.CompareViewExModel,Mmc.Mspace.IntelligentAnalysisModule","Type": "CompareViewExModel","Content": "Splitscreenlinkage","Icon": "ShellR_SplitScreenLinkage.png","MouseOverIcon": "ShellR_SplitScreenLinkage_H.png","PressedOverIcon":"ShellR_SplitScreenLinkage_C.png","ToolTip":"分屏对比","Visible":"false","GroupName": "IntelligentAnalysis","MenuName":"RightToolMenuItems"}

{"$type": "Mmc.Mspace.IntelligentAnalysisModule.CharacterAnalysis.CharactAnalysViewModel, Mmc.Mspace.IntelligentAnalysisModule","Type": "CharactAnalysViewModel","Content": "Characteristicsanalysis","Icon": "ShellR_CharacteristicsAnalysis.png","MouseOverIcon": "ShellR_CharacteristicsAnalysis_H.png","PressedOverIcon":"ShellR_CharacteristicsAnalysis_C.png","ToolTip":"特征分析","Visible":"false","GroupName": "IntelligentAnalysis","MenuName":"RightToolMenuItems"}

{"$type": "Mmc.Mspace.IntelligentAnalysisModule.DemCompare.DemCompareViewModel,Mmc.Mspace.IntelligentAnalysisModule","Type": "DemCompareViewModel","Content": "Elevationthan","Icon": "ShellR_Elevationthan.png","MouseOverIcon": "ShellR_Elevationthan_H.png","PressedOverIcon":"ShellR_Elevationthan_C.png","ToolTip":"高程比对","Visible":"false","GroupName": "IntelligentAnalysis","MenuName":"RightToolMenuItems"}
{"$type": "Mmc.Mspace.IntelligentAnalysisModule.FloodViewModel,Mmc.Mspace.IntelligentAnalysisModule","Type": "FloodViewModel","Content": "FloodAnalysis","Icon": "ShellR_FloodAnalysis.png","MouseOverIcon": "ShellR_FloodAnalysis_H.png","PressedOverIcon":"ShellR_FloodAnalysis_C.png","ToolTip":"淹没分析","Visible":"false","GroupName": "IntelligentAnalysis","MenuName":"RightToolMenuItems"}
{"$type": "Mmc.Mspace.ToolModule.DynamicClip.DynamicClipVModel,Mmc.Mspace.ToolModule","Type": "DynamicClipVModel","Content": "DynamicClip","Icon": "ShellR_DynamicClip.png","MouseOverIcon": "ShellR_DynamicClip_H.png","PressedOverIcon":"ShellR_DynamicClip_C.png","ToolTip":"动态剖面","Visible":"false","GroupName": "IntelligentAnalysis","MenuName":"RightToolMenuItems"}


{"$type": "Mmc.Mspace.RegularInspectionModule.ViewModels.NewInspectionVModel, Mmc.Mspace.RegularInspectionModule","Type": "NewInspectionVModel","Content": "AddInspection","Icon": "ShellR_AddInspection.png","MouseOverIcon": "ShellR_AddInspection_H.png","PressedOverIcon":"ShellR_AddInspection_C.png","ToolTip":"新增巡检","Visible":"false","GroupName": "RegularInspection","MenuName":"RightToolMenuItems"}
{"$type": "Mmc.Mspace.RegularInspectionModule.ViewModels.HistoryDomVModel,Mmc.Mspace.RegularInspectionModule","Type": "HistoryDomVModel","Content": "HistoryDom","Icon": "ShellR_HistoryDom.png","MouseOverIcon": "ShellR_HistoryDom_H.png","PressedOverIcon":"ShellR_HistoryDom_C.png","ToolTip":"历史正射","Visible":"false","GroupName": "RegularInspection","MenuName":"RightToolMenuItems"}
{"$type": "Mmc.Mspace.RegularInspectionModule.ViewModels.PhotosToTraceVModel,Mmc.Mspace.RegularInspectionModule","Type": "PhotosToTraceVModel","Content": "PhotosTrace","Icon": "ShellR_PhotosTrace.png","MouseOverIcon": "ShellR_PhotosTrace_H.png","PressedOverIcon":"ShellR_PhotosTrace_C.png","ToolTip":"照片轨迹","Visible":"false","GroupName": "RegularInspection","MenuName":"RightToolMenuItems"}
{"$type": "Mmc.Mspace.RegularInspectionModule.ViewModels.ScreenCompareVModel,Mmc.Mspace.RegularInspectionModule","Type": "ScreenCompareVModel","Content": "ScreenCompare","Icon": "ShellR_ScreenCompare.png","MouseOverIcon": "ShellR_ScreenCompare_H.png","PressedOverIcon":"ShellR_ScreenCompare_C.png","ToolTip":"分屏标注","Visible":"false","GroupName": "RegularInspection","MenuName":"RightToolMenuItems"}
{"$type": "Mmc.Mspace.RegularInspectionModule.ViewModels.RoneAnalysisVModel,Mmc.Mspace.RegularInspectionModule","Type": "RoneAnalysisVModel","Content": "RoneAnalysis","Icon": "ShellR_RoneAnalysis.png","MouseOverIcon": "ShellR_RoneAnalysis_H.png","PressedOverIcon":"ShellR_RoneAnalysis_C.png","ToolTip":"区域分析:点开工具->选择绘制方法->绘制图形->闭合->确定（统计日期和统计方式可选）","Visible":"false","GroupName": "RegularInspection","MenuName":"RightToolMenuItems"}





	{"$type": "Mmc.Mspace.IotModule.ViewModels.PatrolmanListVModel,Mmc.Mspace.IotModule","Type": "PatrolmanListVModel","Content": "GridPatrol","Icon": "ShellR_GridPatrol.png","MouseOverIcon": "ShellR_GridPatrol_H.png","PressedOverIcon":"ShellR_GridPatrol_C.png","ToolTip":"保安人员","Visible":"false","GroupName": "Cityoverview","IsConflictFlag": "true","MenuName":"RightToolMenuItems"}
	{"$type": "Mmc.Mspace.IotModule.ViewModels.VideoStreamViewModel,Mmc.Mspace.IotModule","Type": "VideoStreamViewModel","Content": "Videomonitoring","Icon": "ShellR_VideoMonitoring.png","MouseOverIcon": "ShellR_VideoMonitoring_H.png","PressedOverIcon":"ShellR_VideoMonitoring_C.png","ToolTip":"视频监测","Visible":"false","GroupName": "Cityoverview","IsConflictFlag": "true","MenuName":"RightToolMenuItems"}
	{"$type": "Mmc.Mspace.IotModule.GpsViewModel, Mmc.Mspace.IotModule","Type": "GpsViewModel","Content": "securityguards","Icon": "ShellR_SecurityGuards.png","MouseOverIcon": "ShellR_SecurityGuards_H.png","PressedOverIcon":"ShellR_SecurityGuards_C.png","ToolTip":"保安人员","Visible":"false","GroupName": "Cityoverview","IsConflictFlag": "true","MenuName":"RightToolMenuItems"}
	{"$type": "Mmc.Mspace.IotModule.WindMonitorViewModel,Mmc.Mspace.IotModule","Type": "WindMonitorViewModel","Content": "windspeeddata","Icon": "ShellR_WindSpeedData.png","MouseOverIcon": "ShellR_WindSpeedData_H.png","PressedOverIcon":"ShellR_WindSpeedData_C.png","ToolTip":"风速数据","Visible":"false","GroupName": "Cityoverview","IsConflictFlag": "true","MenuName":"RightToolMenuItems"}
	{"$type": "Mmc.Mspace.IotModule.TempAndHumViewModel, Mmc.Mspace.IotModule","Type": "TempAndHumViewModel","Content": "Temperaturedata","Icon": "ShellR_Temperaturedata.png","MouseOverIcon": "ShellR_Temperaturedata_H.png","PressedOverIcon":"ShellR_Temperaturedata_C.png","ToolTip":"温湿度数据","Visible":"false","GroupName": "Cityoverview","IsConflictFlag": "true","MenuName":"RightToolMenuItems"}
	
	{"$type": "Mmc.Mspace.IotModule.ViewModels.StatisticsVModel,Mmc.Mspace.IotModule","Type": "StatisticsVModel","Content": "Statistics","Icon": "ShellR_Statistics.png","MouseOverIcon": "ShellR_Statistics_H.png","PressedOverIcon":"ShellR_Statistics_C.png","ToolTip":"统计数据","Visible":"false","GroupName": "Cityoverview","IsConflictFlag": "true","MenuName":"RightToolMenuItems"}

//ScaleViewModelMenu

{"$type": "Mmc.Mspace.ToolModule.Scale.MeasuringScaleViewModel,Mmc.Mspace.ToolModule","Type": "MeasuringScaleViewModel","Content": "Scale","Icon": "比例尺.png","MouseOverIcon": "比例尺H.png","ToolTip":"比例尺","MenuName":"ScaleViewModelMenu"}
























