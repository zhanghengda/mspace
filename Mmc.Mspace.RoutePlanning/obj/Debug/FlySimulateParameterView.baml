   M S B A M L   `   `   `  ����S  OMmc.Mspace.RoutePlanning, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:Mmc.Mspace.RoutePlanningMmc.Mspace.RoutePlanning  5   	   7    1Mmc.Mspace.RoutePlanning.FlySimulateParameterView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:Mmc.Mspace.RoutePlanning     5   	   Q MGFramework.BlankWindow, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'blankWindowhttp://GFramework.org  5        � Title$  FlySimulateParameterView��5	   	   $	��260��6*   $	��400��67    � 
ResizeMode$ NoResize=�6C    � 	Resources 5
       �� 
TargetType   ToggleButtonStyle �� Property �� Value  DefaultButtonBackground  DefaultButtonForeground �� 
TargetType ��Name �� Padding	 �� CornerRadius
 �� Triggers P� Property P� Value �� 
TargetName  DefaultButtonMouseOver  DefaultButtonPressed  DefaultButtonIsEnabled  	situmlate �� BasedOn  PauseSimulation �� Triggers  StartSimulation��%   &	        &	 R    7   �� 5      " d�6
   .�5      ��  � ��$
 14px(�6    �� 5       � ��$ 36��6   �� 5       � ��$ 80��6   �� 5       �@�FocusVisualStyle 6(   Z� �� 5       � ��$ 0q�6'   �� 5       � ��$ 0q�6   �� 5       � ��$	 2,1q�6   �� 5       �@J�SnapsToDevicePixels . 6+   �� 5       � ��# �  6"   �� 5       � ��
 ���� 6#   �� 5       � ��# �  6"   �� 5       � �� 5      �� 5      " d�6   .R�5      �� $ Border��6   # z��6   #��z��6C   $	 2��6c   #��z��6t   #��z��6�   #��z��6�   #��z��6�   ._�5   	   �� $��Center=�6   #��z��68   $��Center=�6\   #��z��6w   5   	   
 5      P� 5   	    �@J�IsMouseOver . . �5   
   �� $ Border��6   # �  6;    � ��5    
   P� 5!   	    � �� . . �5"   
   �� $ Border��6   # �  6;    � ��5#   
   P� 5$   	    � {� .  . �5%   
   �� $ Border��6    � ��$ 0q�6@   �� 5&   
   $ Border��6   # �  6;    � ��5'   
   5)      5+      5,      �� 5-      " d�6   8   .�5.      ��  � ��# �  6    5/      P� 50       �@d�	IsChecked$
 TrueY�6#   . �51      ��  � ��# �  6!   52      54      55      .��56      � $��0q�6    � ColumnDefinitions 57      �� 58      $
��Auto �6   �� 59      $
��Auto �6   �� 5:      $
��Auto �6    � RowDefinitions 5<      �� 5=      $
��Auto �6   6$   �� 5>      $
��Auto �6   6$   �� 5?      $
��Auto �6   6$   �� 5@      $
��Auto �6   �� 5A      $
��Auto �6   .+�5C      �� $��0��6   ���#292C35$��0��6.   $��2��6>   $	 5,5,0,0��6R   (  "GFramework.BlankWindow.BlankWindow   	Draggable . $��0,0,0,0q�6�   $	��400��6�   �� 5D       �� Orientation$ 
Horizontal=�6   $��1��6(   $��0��65   $��2��6E   .+�5E      �� $��Center=�6    	 DefaultTextBlockStyle#��� 	 6.   $��
30,20,5,20q�6^    
 FlyViewPerspective#��� 
 6r   $��Center=�6�   	� 5F      $��20,20q�6     DefaultRadioButtonStyle#��[ 6    $��Center=�6R   $��Center=�6o     FollowerPerspective#���  6�    	� 	GroupName$ FlyViewPerspective��6�   
���������6�   �� *FollowerPerCmd+6  	� 5G      $��20,20q�6   #��[ 6    $��Center=�6R   $��Center=�6o     OverlookingAngle#���  6�   $ FlyViewPerspective��6�   
���������6�   �� *OverLookCmd+ d� 	IsChecked 6  �� *ViewIsChecked+66  5H      �� 5I      $ 
Horizontal=�6   $��4��6(   $��0��65   $��2��6E   $	��400��6Y   .+�5J      d� -   5         d�Name$ 
FlyStation��5J      $��80��6'   $��35��62   #��[ 6�   $��
60,20,30,0q�6�   ��6>   �� *StartFlyCmd+ 6_   �� *FlyIsChecked+��6�   �� *	IsChecked+ �� ElementName$ 
FlyStation��6�   �� 5K      -   5         ��Name$ ReStartFlyBtn��5K      $��80��6$   $��35��6/   $��
60,20,60,0q�6;     ReSimulation#���  6r     DefaultButtonStyle#��[ 6�   $��2��6�   ��6P   �� *ReStartFlyCmd+6�   5L      �� 5M        SettingFlightSpeed#���  6   $��2��6:   $��0��6G   $��11(�6X   #��� 	 6f   $��Center=�6�   $
��Left=�6�   $��10q�6�   $��1��6�   �� 5N      $ 
Horizontal=�6   $��3��6(   $��0��65   $��2��6E   $	��400��6Y   .+�5O      �� $��3��6   $��0��6    �� IsSnapToTickEnabled .  �� TickPlacement$
 Both=�6F    J� RenderTransformOrigin$ 0.5,0.5&�6[   $��1��6�    � Minimum$ 1Q�6�    � Maximum$ 50Q�6�   $��1��6�   $	��200��6�   $��10q�6�    � Value 6{   �� *
FlySpeed+ �� Mode$ TwoWay=�z�5P      V� 5Q      .~�5R      ��   �� ScaleY$  -1Q�6   �� 5S      �� 5T      T� 5U      5V      5X      �� 5Y        CurrentFlightSpeed#���  6   $��3��6;   $��1��6H   $��11(�6Y   #��� 	 6g   $��Center=�6�   $��Center=�6�   $��1��6�   $��5,5,5,5q�6�   �� 5Z      #��� 	 6)   $��3��6Y   $��1��6f   $��40��6v   $��20��6�   $��5,5,0,5q�6�   ��6   �� *
FlySpeed+6�   �� 5[      $	��m/s��6   $��3��6   $��1��6'   $��11(�68   #��� 	 6F   $��Center=�6v   $��Center=�6�   $��1��6�   $��5,5,5,5q�6�   5\      �� 5]        FlightParameterSimulation#���  6   $��0��6B   $��0��6O   $
��Left=�6_   $��Center=�6z   #��� 	 6�   $��10q�6�   �� 5^      -   5        $ CloseButton��5^      $��0��6   $��1��6-     ButtonCloseStyle#���  6a   $��36��6�   $��36��6�   $��Right=�6�   $	��Top=�6�   $��0,2,8,0q�6�   ��6>   �� *FlightCloseCmd+6�   5_      5`      