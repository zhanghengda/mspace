   M S B A M L   `   `   `  ����]  YMmc.Mspace.RegularInspectionModule, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullc6clr-namespace:Mmc.Mspace.RegularInspectionModule.Views(Mmc.Mspace.RegularInspectionModule.Views  5   	   K GMmc.Mspace.Theme, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_Aclr-namespace:Mmc.Mspace.Theme.Controls;assembly=Mmc.Mspace.ThemeMmc.Mspace.Theme.Controls 5   	   ?    9Mmc.Mspace.RegularInspectionModule.Views.RoneAnalysisView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Hlocal6clr-namespace:Mmc.Mspace.RegularInspectionModule.Views     5   	   UconrolsAclr-namespace:Mmc.Mspace.Theme.Controls;assembly=Mmc.Mspace.Theme     5   	   Q MGFramework.BlankWindow, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'blankWindowhttp://GFramework.org  5   	     � Title$  区域分析��5
   	   $��322.8��6   $��404.8��6%    � 
ResizeMode$ NoResize=�5      .��5      � $��0,0,3.4,3.6q�6    J� RenderTransformOrigin$ 0.453,0.528&�6   ��6A   Z�  � RowDefinitions 5      �� 5      $��40 �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6    � ColumnDefinitions 5      �� 5      .+�5      �� $��0��6   ���#292C35$��0��6.   $��3��6>    �� CornerRadius$ 5,5,0,0��6R   (  "GFramework.BlankWindow.BlankWindow   	Draggable . 6i   �� 5         Rone_analysis#���   6   $��0��66   $��0��6C   $
��Left=�6S   $��Center=�6n     DefaultTextBlockStyle#���  6�   $��20,5,5,5q�6�   �� 5      $
��Left=�6   $��1��6*   #���  67   $��	30,20,5,5q�6g     Datatime#���  6z   $��Center=�6�   (  "System.Windows.Controls.DatePicker  5      $
��Left=�6   $��1��6+   $��
150,20,5,5q�69      SelectedDate 6M   �� *
BeforeData+6s     5      $
��Left=�6   $��1��6+   $��
250,20,5,5q�69    6N   �� *	Nowdays+6q   �� 5!   
   $��2��6   $��0��6"   $��3��62   .+�5"      ��  �� Orientation$ 
Horizontal=�6   $��2��62   $��0��6?   $��3��6O   .+�5#      �� $��Center=�6   $��2��6:   #���  6G   $��
30,20,5,20q�6w     Representation#���  6�   $��Center=�6�   	� 5$      -   5        	 	�Name$		 pie��5$   �   $
��20,0q�6     DefaultRadioButtonStyle#��[ 6,   $��Center=�6^   $��Center=�6{     	pie_chart#���  6�   
 	� 	GroupName$
 AnalyseMidea��6�   
������� d� 	IsChecked 6�   �� *
PieCheck+6  	� 5%      -   5        $
	 rect��5%   �   $
��20,0q�6   #��[ 6,   $��Center=�6^   $��Center=�6{     
rect_chart#���  6�   $
 AnalyseMidea��6�   
�������$
 TrueY�6�   6  5&      5'      �� 5(   
   $
��Left=�6   $��3��60   #���  6=   $��	30,0,0,20q�6m     Select_draw#���  6�   $��Center=�6�   $��3��6�   $	��300��6�   �� 54      ���#08080B$��0��6!   $��2��61   $��3��6<   $��
10,0,10,90q�6P   $��6��6d   $��Bottom=�6q   �� 57      $ 
Horizontal=�6   $��4��6(   $��0��65   $��3��6E   .+�58      '  !Mmc.Mspace.Theme.Controls.Buttons    HomeLeftButtonStyle#���  6      HasPathData .    	GroupName$ roneAnalysis��6V      IsNeedSelected .  	 
circular_n   RIcon# � 	 6�    
 
circular_p   MouseOverRIcon# � 
 6�      PressedRIcon# � 
 6�   $��Center=�6(  $��	45,0,20,0q�6F  $��Bottom=�6Z  $��40��6u  $��40��6�  ��6  �� *Circle_Draw_Cmd+6�    59      #���  6    . $ roneAnalysis��6W    .   rectangle_n# �  6�     rectangle_p# �  6�   # �  6�   $��Center=�60  $��	60,0,20,0q�6O  $��Bottom=�6c  $��40��6~  $��40��6�  ��6  �� *Rectangle_Draw_Cmd+6�    5:      #���  6    . $ roneAnalysis��6X    .   	polygon_n# �  6�     	polygon_p# �  6�   # �  6�   $��Center=�6(  $��	55,0,20,0q�6F  $��Bottom=�6Z  $��40��6u  $��40��6�  ��6  �� *Polygon_Draw_Cmd+6�  5;      �� 5<   
   $ 
Horizontal=�6   $��5��6.   $��0��6;   $��3��6K   .+�5=      �� $
��Left=�6   $��3��64   #���  6A   $��	50,0,50,0q�6q    
 Circle#���  6�   $��Center=�6�   �� 5>      $
��Left=�6   $��3��64   #���  6A   $��	40,0,50,0q�6q     Rectang#���  6�   $��Center=�6�   �� 5?      $
��Left=�6   $��3��64   #���  6A   $��	30,0,30,0q�6q     DIY#���  6�   $��Center=�6�   5@      �� 5B   
   $ 
Horizontal=�6   $��6��6.   $��0��6;   $��3��6K   .+�5C      ��   OK#���  6     DefaultButtonStyle#��[ 6,   $��Center=�6u   $��
60,0,70,10q�6�   $��Bottom=�6�   $��70��6�   ��6X   �� *
Draw_Cmd+6�   �� 5D       
 Cancel#���  6   #��[ 60   $��Center=�6z   $��
50,0,30,10q�6�   $��Bottom=�6�   $��70��6�   ��6\   �� *	Close_Cmd+6�   5F      �� 5G      -   5         ��Name$ CloseButton��5G      $��0��6   $��1��6-     ButtonCloseStyle#��[ 6]   $��Right=�6�   $	��Top=�6�   $��0,0,0,0q�6�   $ 2.241,0.787&�6�   ��6>   �� *	Close_Cmd+6�   5J      5K      