   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   
Dronetrack 
 Online  Offline  RealtimeTrajectory  Mountcontrol  Realtimevideo  RemoteControl  Perspectivetofollow 	 Model 	 Type 
 
Connection 	 Mount  HudPanel  Videostream  ConnectionContent  Joystick  	Dronelist 
 Logged  	Notlogged  	InService  	Leftlayer  
Rightlayer  
Powerroute  Routemapping  Logisticsroutes  ReStartAnylyse  	pie_chart  
rect_chart  Vehicle  BlockRouteManage  BlockTakeOff  BlockRouteModel   BlockVehicleLand ! BlockJoyBtn " BlockDropDumpBtn # 
CloudMount 	$ Pitch % Roll & Yaw ' Zoom ( Up ) Left 	* Rigth + Down , Amplification 
- Narrow . Lock / Control 0 	Intheback 1 Rate 2 Takingpictures 	3 Video 4 Gasdetection 5 textToGroundStation 6 textToVehicle 7 	textArmed 8 textTakeoff 9 textRouteModel : textLandSafty ; textRTL < UnmannedShip 
= shipCt 
> shipDo 
? shipPh @ shipTemp A shipTur B 	Panoramic C S D 	Numerical E Unit F Warninglevel G Doublescreen H Newannotation I Exitmultiscreen J Updateannotation K Removeannotation L 	NoControl M Nolocationretry N Creationfailed O Currentposition P CurrentpositionForShip Q Loginprompt�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      .sys&clr-namespace:System;assembly=mscorlib  5      %�  &	        &	 #     &	 A     &	 `     &	 �     &	 �     &	 �     &	 �     &	 +    &		 I    &	
 f    &	 �    &	 �    &	 �    &	 �    &	     &	 5    &	 W    &	 u    &	 �    &	 �    &	 �    &	     &	 +    &	 O    &	 s    &	 �    &	 �    &	 �    &	 �    &	      &	 A    &	  e    &	! �    &	" �    &	# �    &	$ �    &	%     &	& .    &	' I    &	( e    &	)     &	* �    &	+ �    &	, �    &	- �    &	.     &	/ 3    &	0 R    &	1 t    &	2 �    &	3 �    &	4 �    &	5 �    &	6     &	7 ;    &	8 X    &	9 x    &	: �    &	; �    &	< �    &	=     &	>     &	? 9    &	@ S    &	A o    &	B �    &	C �    &	���    &	D �    &	E 	    &	F 	    &	G B	    &	H g	    &	I �	    &	J �	    &	K �	    &	L 
    &	M I
    &	N �
    &	O �
    &	P �
    &	Q     ��5      Drone track��61   ��5      
Online��6(   ��5      Offline��6*   ��5      Real time Trajectory��6B   ��5      Mount Control��65   ��5	      Real time video��68   ��5
      Remote control��67   ��5      Perspective to follow��6D   ��5      
Model:��6'   ��5      	Type:��6%   ��5      Connection：��61   ��5      
Mount:��6'   ��5      Vehicle Hud��6/   ��5      Video stream：��64   ��5      
Connection��67   ��5      Joystick��6,   ��5      
Drone list��6/   ��5      
Logged��6(   ��5      
Not logged��6/   ��5      
In service��6/   ��5      左屏图层��6)   ��5      右屏图层��6*   ��5      电力巡检航线��6,   ��5      测绘航线��6,   ��5      物流航线��6/   ��5       ReStartAnylyse��65   ��5!      	pie_chart��6+   ��5"      
rect_chart��6-   ��5$      Vehicle��6*   ��5%      Route Manage:��69   ��5&      	Take Off:��61   ��5'      Route Model:��67   ��5(      Vehicle Land:��69   ��5)      JoyStick Info:��65   ��5*      
Drop Dump:��66   ��5,      Cloud Mount��61   ��5-      	Pitch��6&   ��5.      Roll��6$   ��5/      Yaw��6"   ��50      Zoom��6$   ��51      Up��6    ��52      Left��6$   ��53      	Rigth��6&   ��54      Down��6$   ��55      Amplification��66   ��56      
Narrow��6(   ��57      Lock��6$   ��58      Control��6*   ��59      
Inthe back��6/   ��5:      Rate：��6%   ��5;      Taking pictures��69   ��5<      	Video��6&   ��5=      Gas detection��65   ��5?      	To GS��64   ��5@      
To Vehicle��63   ��5A      	Armed��6*   ��5B      Take Off��6/   ��5C      Route Model��65   ��5D      
Land Safty��63   ��5E      Return Home��6.   ��5G      Unmanned Ship��65   ��5H      Ct��6$   ��5I      DO��6$   ��5J      PH��6$   ��5K      Temp��6(   ��5L      Tur��6&   ��5M      	Panoramic��6.   ��5P      S��6   ��5Q      Name��6$   ��5R      	numerical��6.   ��5S      Unit��6$   ��5T      warning grade��65   ��5V      Double screen��65   ��5W      New annotation��67   ��5X      Exit multiscreen��6;   ��5Y      Update annotation��6=   ��5Z      Remove annotation��6=   ��5[      .*无人机挂载请求失败，请重试！��63   ��5\      40无人机位置数据请求失败，请重试！��6;   ��5]      骨骼动画创建失败��62   ��5_      Drone current position��6A   ��5`      Ship current position��6G   ��5a      "Please connect before landing!��6E   5c      