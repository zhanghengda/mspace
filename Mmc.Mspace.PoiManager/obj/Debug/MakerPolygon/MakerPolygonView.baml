   M S B A M L   `   `   `  ����V  RMmc.Mspace.PoiManagerModule, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:Mmc.Mspace.PoiManagerModuleMmc.Mspace.PoiManagerModule  5   	   K GMmc.Mspace.Theme, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_Aclr-namespace:Mmc.Mspace.Theme.Controls;assembly=Mmc.Mspace.ThemeMmc.Mspace.Theme.Controls 5   	   [?clr-namespace:Mmc.Mspace.Theme.Helper;assembly=Mmc.Mspace.ThemeMmc.Mspace.Theme.Helper 5   	   2    ,Mmc.Mspace.PoiManagerModule.MakerPolygonView   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   X TXceed.Wpf.Toolkit, Version=3.3.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   5local)clr-namespace:Mmc.Mspace.PoiManagerModule  5   	   OconrolsAclr-namespace:Mmc.Mspace.Theme.Controls;assembly=Mmc.Mspace.Theme  5   	   LHelper?clr-namespace:Mmc.Mspace.Theme.Helper;assembly=Mmc.Mspace.Theme  5   	   -   5        $	��452��5   A   $	��580��6N     � ShowInTaskbar  .   � WindowStyle$
 None=�6q    � AllowsTransparency .  � WindowState$ Normal=�5	       � 
ResizeMode$ NoResize=�6     � WindowStartupLocation$ CenterOwner=�68   ��5   �   Z� .��5
      � ���#1E2026 � RowDefinitions 5   
   �� 5      $��40 �6   �� 5      .+�5   
   �� -   5        $��0��5      ���#292C35$��0��6k   $��2��6{    �� CornerRadius$ 5,5,0,0��6�   �� 5   
   -   5         ��Name$ 	TitleName��5      $��0��6_   $��0��6l   $��2��6|   $��18,0,0,0q�6�   
�������$��18(�6�   $
��Left=�6�   $��Center=�6�   ��6�   �� *
WinTitle+6�   �� 5   
   -   5        	 ��Name$	 CloseButton��5      $��0��6   $��0��63   $��2��6C      ButtonCloseStyle#���   6u   $��36��6�   $��36��6�   $��Right=�6�   $	��Top=�6�   $��0,2,8,0q�6�   ��6W   �� *
CloseCmd+6�   � 5   
   $��1��6   $��15q�6   $��Stretch=�6(    5      �� 5      $
��Auto �6    �� 5      �� 5      $
��Auto �6!   .+�5      �� $��1q�6     HighlightBackBrush#���  6)   $��5q�6\   ._�5      �� 
 �� Orientation$
 
Horizontal=�6   .+�5      �� -   5        $��3q�5      $	��240��6B   $	��108��6N   $
��Fill=�6[   ��6(   �� *ImgUrl+6�   �� 5      $
 Vertical=�6!   $��3q�68   $��Center=�6C   $��Center=�6`   .+�5      '  !Mmc.Mspace.Theme.Controls.Buttons     HasPathData . $��24��6@   $��24��6L   $
��10,5q�6W     Update_N   RIcon# �  5    %     Update_H   MouseOverRIcon# �  5!   %     Updatelocation � ToolTip# �  6Q      PressedRIcon# �  5"   %     HomeLeftButtonStyle#��[ 6t   ��6O   �� *GetLocalCommand+6�     5#       . $��24��6@   $��24��6L   $
��10,5q�6W     Screenshots_N# �  5$   %     Screenshots_H# �  5%   %     Screenshots# �  6V   # �  5&   %   #��[ 6{   ��6T   �� *GetPictureCommand+6�     5(       . $��24��6@   $��24��6L   $
��10,5q�6W    	 	Flyinto_N# � 	 5)   %    
 	Flyinto_H# � 
 5*   %     Flyinto# �  6R   # � 
 5+   %   #��[ 6r   ��6P   �� *FlytoCommand+6�   5,      5-      5.      �� 50      $��1��6   $��5q�6&   ���#1E2026$	��200��6F   $��Stretch=�6V     TabControlStyle#���  6t   .@�51      ��   Information#���  6     TabItemStyle#���  6B   .��52      � $��5q�6    53      �� 54      $
��Auto �6,   �� 55      $
��Auto �6,   �� 56      $
��Auto �6,   �� 57      $
��Auto �6,   �� 58      $
��Auto �6,    � ColumnDefinitions 5:      �� 5;      $
��Auto �6/   6?   �� 5<      $��* �6/   6;   �� 5=      $
��Auto �6/   6?   �� 5>      $��* �6/   6;   .+�5A      �� $	��0,5q�6$   $��0��61   $��0��6>     DefaultTextBlockStyle#��[ 6N    	 Title#���  6~   6�   �� 5B      $��	10,0,10,5q�6#   $��0��66   $��1��6D   $��3��6T   $
��Auto��6h     TbxWaterStyle#���  6u   +  %Mmc.Mspace.Theme.Helper.TextBoxHelper   IsNeedClearButton .  $��Bottom=�5C   �   ��6a   �� *
PoiTitle+ �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5E      $	��0,5q�6$   $��1��61   $��0��6?   #��[ 6P     	Longitude#���  6�   6�   �� 5F      $
��10,5q�6$   $��1��63   $��1��6A   #��[ 6Q   ��6�   �� *Lng+ �� StringFormat$ {0:N5}°��6�   �� 5H      $	��0,5q�6$   $��1��61   $��2��6>   #��[ 6N     Latitude#���  6~   6�   �� 5I      $
��10,5q�6$   $��1��62   $��3��6?   #��[ 6O   ��6   �� *Lat+$ {0:N5}°��6�   �� 5K      $	��0,5q�6$   $��2��61   $��0��6>   #��[ 6N    
 Height#���  6~   6�   �� 5L      $
��10,5q�6$   $��2��62   $��1��6?   #��[ 6O   ��6   �� *Alt+$ {0:N3}m��6�   �� 5N      $	��0,5q�6$   $��2��61   $��2��6>   #��[ 6N     MakerPolygon_Area#���  6~   6�   �� 5O      $
��10,5q�6$   $��2��62   $��3��6?   #��[ 6O   ��6   �� *AreaTranfer+6�   �� 5Q      $��2��6!   $
��10,5q�6.   $��3��6<   $
��Auto��6L     DefaultButtonStyle#���  6Y   $��Right=�6�   $��20��6�     
GetAddress#���  6�   ��6�   �� *GetAddressCommand+6�   �� 5U      $	��0,5q�6$   $��3��61   $��0��6>   #��[ 6N     Address#���  6~   6�   �� 5V      $
��10,5q�6"   $��3��60   $��1��6>   $��3��6N   $��50��6b    �� TextWrapping$
 Wrap=�6s    �� AcceptsReturn .   TbxWaterMaxStyle#���  6�   ��5W   2   �� *	Address+ 6K   �� *	Address+�� 5Y      $	��0,5q�6$   $��4��61   $��0��6>   #��[ 6N     Tags#���  6~   6�   5   /Mmc.Mspace.PoiManagerModule.MultiSelectComboBox  5Z      -   5          Name$ MC��5Z   �   $��4��64   $��1��6A   $��3��6Q   $
��10,5q�6e      ItemsSource 6s   �� *
TagItems+   SelectedItems 6�   �� *SelectdTagItems+ 6�   ��  �� Path$
 Text� �� RelativeSource �� *Self+5[      5\      �� 5]        StyleSet#���  6   #���  6>   .��5^      � $��5q�6   $	��Top=�6&    5_      �� 5`      �� 5a      �� 5b      �� 5c       5e      �� 5f      $
��Auto �6/   6?   �� 5g      $��* �6/   6<   .+�5j      �� $	��0,5q�6$   $��0��61   $��0��6>   #��[ 6N     SLLinewidth#���  6~   6�   �� 5k      $
��10,5q�6#   $��0��61   $��1��6?   #���  6O   $
��Auto��6x   $��Stretch=�6�   ��5l   2   �� *SLLineWidth+$ PropertyChanged=��� 5m      $	��0,5q�6$   $��1��61   $��0��6>   #��[ 6N     SLColors#���  6~   6�   #  Xceed.Wpf.Toolkit.ColorPicker  5n      -   5          Name$ SLColorPicker��5n   :   $
��10,5q�6,   $��1��6Q   $��1��6^     P60BlackBrush#��[ 6n   6�   �� 5p      $	��0,5q�6$   $��2��61   $��0��6>   #��[ 6N    
 Colors#���  6~   6�     5q      -   5        $ ColorPicker��5q   :   $
��10,5q�6,   $��2��6O   $��1��6\   #��[ 6l   6�   �� 5r      $	��0,5q�6$   $��3��61   $��0��6>   #��[ 6N      	Elevation#���   6~   6�   �� 5s      -	   5        $��HeightTypesBox��5s   m   $
��10,5q�6#   $��3��61   $��1��6>    �� DisplayMemberPath$ 	HeighName��6O    ! DefaultComboBoxStyle#��� ! 6�   ��6�   �� *HeightTypes+ �� SelectedItem 6�   �� *SelectedHeightType+5u      5w      5x      �� 5y       	" Descr#��� " 6    # TabItemStyle2#��� # 6<   .��5z      � $��5q�6    5{      �� 5|       5~      �� 5      $
��Auto �6/   6?   �� 5�      $��* �6/   6<   .+�5�      �� $	��0,5q�6$   $��0��61   $	��Top=�6>   $��0��6V   #��[ 6f    	$ Notes#��� $ 6�   6�   �� 5�      $
��10,5q�6"   $��0��60   $��1��6=   $	��150��6M   #���  6[   $
 Wrap=�6�    . $	��Top=�5�   3   ��6   �� *Detial+63   5�      5�      5�      �� 5�      $��2��6   $��	0,10,0,20q�6"    % OK#��� % 65   $��Center=�6�   $	��100��6�   $��36��6�   #���  6�   ��6V   �� *	CreateCmd+{�6t   �� *IsSave+6�   5�      5�      5�      