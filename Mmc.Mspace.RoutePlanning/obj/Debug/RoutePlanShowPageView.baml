   M S B A M L   `   `   `  ����S  OMmc.Mspace.RoutePlanning, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:Mmc.Mspace.RoutePlanningMmc.Mspace.RoutePlanning  5   	   K GMmc.Mspace.Theme, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null[?clr-namespace:Mmc.Mspace.Theme.Helper;assembly=Mmc.Mspace.ThemeMmc.Mspace.Theme.Helper 5   	   4    .Mmc.Mspace.RoutePlanning.RoutePlanShowPageView   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:Mmc.Mspace.RoutePlanning     5   	   Rhelper?clr-namespace:Mmc.Mspace.Theme.Helper;assembly=Mmc.Mspace.Theme     5   	   Q MGFramework.BlankWindow, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'blankWindowhttp://GFramework.org  5   	   P LMmc.Windows.Framework, Version=1.0.0.1, Culture=neutral, PublicKeyToken=null'Customhttp://schemas.mmc.com/wpf  5	   	   $	��490��5
      $	��360��6'      baseWindowStyle#���   63     � WindowStyle$
  None=�6^    � 
ResizeMode$ NoResize=�6q    � WindowStartupLocation$ CenterOwner=�6�   .��5      �  � RowDefinitions 5   
   �� 5      $��40 �6   �� 5      $	��61* �6   �� 5      $��4* �6   .+�5   
   �� $��0��6   ���#292C35$��0��63    �� CornerRadius$ 5,5,0,0��6C   (  "GFramework.BlankWindow.BlankWindow   	Draggable . 6Z   �� 5   
   -   5         ��Name$ 	TitleName��5       . $��0��6Q   $��0��6^     Parameterconfiguration#���  6n   $��10,0,0,0q�6�   
�������$��18(�6�   $
��Left=�6�   $��Center=�6�   �� 5   
   -   5         ��Name$ CloseButton��5      $��0��6   $��0��63     ButtonCloseStyle#���  6g   $��36��6�   $��36��6�   $��Right=�6�   $	��Top=�6�   $��0,2,8,0q�6�   ��6D   �� *cmdCloseWindow+6�   � 5   
   $��1��6   $��2��6    5      �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $��* �6     � ColumnDefinitions 5!      �� 5"      $
��auto �6#   �� 5#      �� 5$      $
��Auto �6$   �� 5%      �� 5&      $
��Auto �6$   �� 5'      .+�5*      �� $��0��6   	 �� Orientation$	 Vertical=�6&   $��6��6=   $��0 10 0 0q�6Q   .+�5+      ��  
 Camera#���  6   $
��10,2q�6<   $��6��6J     DefaultTextBlockStyle#���  6_   �� 5,      $	 
Horizontal=�6   .+�5-      �� -   5        $��	Id_Camera��5-   �   $
��10,5q�6   $��0��6-   $��6��6:   $��1��6N   $	��230��6^   $
��Left=�6j     DefaultComboBoxStyle#���  6   ��6�   �� *	Cameras+
 �� SelectedItem
 6�   �� *SelectCamera+ �� UpdateSourceTrigger$ PropertyChanged=�6   �� 5.      $��50��6   $��40��6(   $��+��65   $��16(�6A   
�������  DefaultButtonStyle#���  6c   ��6�   �� *cmdAddMappingCamera+5/      50      �� 52      $��1��6   $��6��6&   $��2��6:   $	 Vertical=�6K   .+�53      ��   Imagecoverage#���  6   $��1��6C   $
��10,2q�6P   $��4��6^   #���  6r   �� 54      $	 
Horizontal=�6   .+�55      ��   Positive#���  6    $��2��6B   $��0��6O   $
��10,2q�6_   #���  6m   �� 56      -   5         ��Name$ Id_ZM��56   �   $��2��6   $��1��6+   +  %Mmc.Mspace.Theme.Helper.TextBoxHelper   IsNeedClearButton .   	 TbxWaterStyle#��� 	 6j   $��0,2,0,2q�6�   $��65��6�   ��6�   �� *SelectFrontAngle+$ PropertyChanged=��� 57      $��2��6    $��2��6-   #���  6=   $��%��6m   $��8q�6v   �� 58       
 Side#��� 
 6    $
��10,2q�6>   $��2��6L   $��3��6Y   #���  6i   �� 59      -   5        $ Id_CM��59   �   $��2��6   $��4��6+    .  #��� 	 6j   $��0,2,0,2q�6�   $��65��6�   ��6�   �� *SelectSideAngle+$ PropertyChanged=��� 5:      $��2��6   $��5��6   #���  6/   $��%��6_   $��8q�6h   5;      5<      �� 5=      $��3��6   $��6��6&   $��3��6:   $	 Vertical=�6K   .+�5>      ��   Thegrid#���  6   $
��10,2q�6=   $��3��6K   $��0��6X   $��4��6h   #���  6|   �� 5?      $	 
Horizontal=�6   .+�5@      ��   	Gridpoint#���  6    $
��10,2q�6C   $��4��6Q   $��0��6^   $	��100��6n   #���  6z   �� 5A      -   5        $��Id_WGJD��5A     $��4��6   $��1��6,   $��0,2,10,2q�6<   $��4��6N   $	��150��6b   #���  6n   ��6�   �� *RevolveAngles+
 6�   �� *SelectRevoleAngle+$ PropertyChanged=�5B      �� 5D      $��°��6    $	��0,2q�6)   $��2��66   #���  6J   5E      �� 5F      $	 
Horizontal=�6   .+�5G      ��   Flowarea#���  6    $	��100��6B   $
��10,2q�6N   $��5��6\   $��0��6i   #���  6y   �� 5H      -   5        $ Id_ZZQ��5H   �   $��5��6   $��1��6+    .  $��4��6j   #��� 	 6~   $	��150��6�   $��0,2,10,2q�6�   ��6�   �� *SelectTurnoverArea+$ PropertyChanged=��� 5I      $��m��6    $	��0,2q�6)   $��5��66   $��5��6C   #���  6T   5J      5K      �� 5L      $��6��6   $��6��6&   $��3��6:   $	 Vertical=�6K   .+�5M      �� $��6��6   $��4��6)   $
��10,2q�6=     OtherSettings#���  6K   #���  6r   �� 5N      $	 
Horizontal=�6   .+�5O      	� $��7��6"   $��0��6/   $
��10 2q�6?   $��Center=�6M    
 Highly#���  6h     DefaultRadioButtonStyle#���  6�    d� 	IsChecked 6�   �� *SelectHeightOption+�� 5P      -   5        $ Id_GD��5P   �   $��7��6   $��1��6+   $	��150��6;    .  $��4��6v   #��� 	 6�   $��0,2,10,2q�6�   {�. ��6�   �� *SelectHeight+$ PropertyChanged=�6  �� 5Q      $��m��6    $��7��6)   $��5��66   #���  6F   5R      �� 5S      $	 
Horizontal=�6   .+�5T      	� $��8��6"   $��0��6/   $
��10,2q�6?   $��Center=�6M     Groundresolution#���  6h   #���  6�    6�   �� *SelectGroundPixelOption+�� 5U      -	   5        $ Id_DMFBL��5U   �   $��8��6   $��1��6+    .  $��3��6j   #��� 	 6~   $	��150��6�   $��0,2,10,2q�6�   {�.  ��6�   �� *SelectGroundPixel+$ PropertyChanged=�6  �� 5V      $��cm/px��6    $��8��6-   $��5��6:   #���  6J   5W      5X      �� 5Y      -
   5        $ Id_DrawPolygon��5Y     $��9��6   $��6��6"   $��Center=�66   $��Bottom=�6S   $	��100��6n   $��36��6z     Drawpolygon#���  6�   $��0 0 0 16q�6�   #���  6�   ��6�   �� *cmdCreatePolygon+6  5Z      5[      5\      