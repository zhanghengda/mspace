   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   Cameraconfiguration  
CameraName  Focallength  Picturelong  Picturewidth  
RouteInput 
 Export  	KMLImport  MissionImport 	 
Regularine 
 	ExportKML  ExportDoubleKML  ExportGeoJson  ExportMMCMission  Drawtheroute  Drawthearea  Stickto  Editwaypoint 
 Upload  
Routesshow  
Courselist  Parameterconfiguration 
 Camera  Imagecoverage  Positive  Side  Thegrid  	Gridpoint  Flowarea  OtherSettings 
 Highly  Groundresolution   Drawpolygon ! Waypoint 	" Speed 	# Hover $ 
Thetrigger % Cameratrigger & Uploadtheroutes ' ConfirmUpload ( 
Coursename ) RoutePlan_ID * RoutePlan_ZoneType + RoutePlan_RouteType , RoutePlan_RouteName - RoutePlan_NumberofWayPoints . RoutePlan_WorkingArea / RoutePlan_EstimatedTime 0 RoutePlan_EstimatedRange 1 RoutePlan_AddTime 2 RoutePlan_DownloadRoute 3 RoutePlan_Detail 4 RoutePlan_Back 5 RoutePlan_Search 6 RoutePlan_SendEmail 7 RoutePlanning_RouteID 8 RoutePlanning_RouteName 9 RoutePlanning_TimeSaved : RoutePlanning_RouteType #; RoutePlanning_NumberofWaypoints < RoutePlanning_WorkingArea = RoutePlanning_EstimatedTime  > RoutePlanning_EstimatedRange ? RoutePlanning_Operating @ StartSimulation A FollowerPerspective B OverlookingAngle C SettingFlightSpeed D FlightParameterSimulation E CannotBeEmpty! F EnterIntegersOnly! G FlyingTooFast! H FlyViewPerspective I CurrentFlightSpeed J PauseSimulation K ReSimulation L RouteDetails M RouteSimulation N DeleteRoute O UploadRouteError�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      .sys&clr-namespace:System;assembly=mscorlib  5      %�  &	        &	 ,     &	 I     &	 n     &	 �     &	 �     &	 �     &	 �     &	     &		 G    &	
 k    &	 �    &	 �    &	 �    &	     &	 4    &	 Y    &	 y    &	 �    &	 �    &	 �    &	     &	 1    &	 P    &	 w    &	 �    &	 �    &	 �    &	 �    &	     &	 >    &	 ]    &	  �    &	! �    &	" �    &	# �    &	$     &	% '    &	& M    &	' r    &	( �    &	) �    &	*     &	+ #    &	, E    &	- g    &	. �    &	/ �    &	0 �    &	1     &	2 4    &	3 Z    &	4     &	5 �    &	6 �    &	7 �    &	8 �    &	9     &	: 9    &	; [    &	< �    &	= �    &	> �    &	? �    &	@ 	    &	A 5	    &	B U	    &	C u	    &	D �	    &	E �	    &	F 
    &	G /
    &	H U
    &	I t
    &	J �
    &	K �
    &	L �
    &	M �
    &	N     &	O ?    ��5      Camera configuration��6C   ��5      	Name:��6+   ��5      Focal length:��64   ��5      Picture long:��64   ��5	      Picture width:��66   ��5      
Import��6,   ��5      
Export��6(   ��5      KML file import��64   ��5      Mission file import��6<   ��5      Regular line��62   ��5      Single point KML file��6:   ��5      Two point KML file��6=   ��5      GeoJson file��65   ��5      MMC Mission file��6<   ��5      Draw the route��66   ��5      Draw the area��64   ��5      Stick to��6+   ��5      Edit waypoint��65   ��5      
Upload��6(   ��5      Routes show��61   ��5      Course list��61   ��5      Parameter configuration��6I   ��5      Camera:��6)   ��5      Image coverage:��68   ��5      	Positive:��6-   ��5      	Side:��6%   ��5       	Grid:��6(   ��5!      Grid point:��60   ��5"      
Flow area:��6.   ��5#      Other Settings:��68   ��5$      Highly:��6)   ��5%      Ground resolution:��6>   ��5&      Draw polygon��63   ��5'      Waypoint��6,   ��5(      
Speed:��6'   ��5)      
Hover:��6'   ��5*      Trigger:��6.   ��5+      Camera trigger��67   ��5,      Upload routes��68   ��5-      >:Verify that the current route is uploaded to the server？��6a   ��5.      Course name:��62   ��51      ID��6*   ��52      	Zone Type��67   ��53      
Route Type��69   ��54      
Route Name��69   ��55      Number of Waypoints��6J   ��56      Working Area(m²)��6>   ��57      Estimated Time（min）��6F   ��58      Estimated Range(m)��6F   ��59      Add Time��65   ��5:      Download Route��6A   ��5;      Route Details��69   ��5=      Back��6.   ��5>      
Search��62   ��5?      Send��63   ��5A      Route ID��69   ��5B      
Route Name��6=   ��5C      
Time Saved��6=   ��5D      
Route Type��6=   ��5E      Number of Waypoints��6N   ��5F      Working Area��6A   ��5G      Estimated Time��6E   ��5H      Estimated Range��6G   ��5I      	Operating��6<   ��5J      	Start��6-   ��5K      Follower��64   ��5L      Overlook��61   ��5M      .*SettingFlightSpeed（suggest:1~25m/s）：��6O   ��5N      FlightSimulation��6B   ��5O      CannotBeEmpty!��65   ��5P      EnterIntegersOnly!��6=   ��5Q      FlyingTooFast!��65   ��5R      FlyView��62   ��5S      CurrentSpeed��67   ��5T      	Pause��6-   ��5U      ReDo��6)   ��5V      RouteDetails��61   ��5W      RouteSimulation��67   ��5X      DeleteRoute��6/   ��5Y      Upload Route Error��6;   5[      