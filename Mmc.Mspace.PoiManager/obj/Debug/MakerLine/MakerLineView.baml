   M S B A M L   `   `   `  ����K  GMmc.Mspace.Theme, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_Aclr-namespace:Mmc.Mspace.Theme.Controls;assembly=Mmc.Mspace.ThemeMmc.Mspace.Theme.Controls  5   	   [?clr-namespace:Mmc.Mspace.Theme.Helper;assembly=Mmc.Mspace.ThemeMmc.Mspace.Theme.Helper  5   	   V RMmc.Mspace.PoiManagerModule, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:Mmc.Mspace.PoiManagerModuleMmc.Mspace.PoiManagerModule 5	   	   /   )Mmc.Mspace.PoiManagerModule.MakerLineView   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   UconrolsAclr-namespace:Mmc.Mspace.Theme.Controls;assembly=Mmc.Mspace.Theme     5   	   RHelper?clr-namespace:Mmc.Mspace.Theme.Helper;assembly=Mmc.Mspace.Theme     5   	   J FMmc.Wpf.Toolkit, Version=1.0.0.1, Culture=neutral, PublicKeyToken=null&gvifmhttp://schemas.mmc.com/wpf  5   	   5local)clr-namespace:Mmc.Mspace.PoiManagerModule  5	   	   X TXceed.Wpf.Toolkit, Version=3.3.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5
   	   -   5        $	��452��5   A   $	��580��6N     � ShowInTaskbar  .   � WindowStyle$
 None=�6q    � AllowsTransparency .  � WindowState$ Normal=�5       � 
ResizeMode$ NoResize=�6     � WindowStartupLocation$ CenterOwner=�68   ��5   �   Z� .��5      � ���#1E2026 � RowDefinitions 5   
   �� 5      $��40 �6   �� 5      .+�5   
   �� -   5        $��0��5      ���#292C35$��0��6k   $��2��6{    �� CornerRadius$ 5,5,0,0��6�   �� 5   
   -   5         ��Name$ 	TitleName��5      $��0��6_   $��0��6l   $��2��6|   $��18,0,0,0q�6�   
�������$��18(�6�   $
��Left=�6�   $��Center=�6�   ��6�   �� *
WinTitle+6�   �� 5   
   -   5        	 ��Name$	 CloseButton��5      $��0��6   $��0��63   $��2��6C      ButtonCloseStyle#���   6u   $��36��6�   $��36��6�   $��Right=�6�   $	��Top=�6�   $��0,2,8,0q�6�   ��6W   �� *
CloseCmd+6�   � 5   
   $��1��6   $��15q�6    5      �� 5      $
��Auto �6    �� 5      �� 5      $
��Auto �6!   .+�5      �� $��1q�6     HighlightBackBrush#���  6)   $��5q�6\   ._�5      �� 
 �� Orientation$
 
Horizontal=�6   .+�5      �� -   5        $��3q�5      $	��240��6B   $	��108��6N   $
��Fill=�6[   ��6(   �� *ImgUrl+6�   �� 5!      $
 Vertical=�6!   $��3q�68   $��Center=�6C   $��Center=�6`   .+�5"      '   !Mmc.Mspace.Theme.Controls.Buttons     HasPathData . $��24��6@   $��24��6L   $
��10,5q�6W     Update_N   RIcon# �  5#   %     Update_H   MouseOverRIcon# �  5$   %     Updatelocation � ToolTip# �  6Q      PressedRIcon# �  5%   %     HomeLeftButtonStyle#��[ 6t   ��6O   �� *GetLocalCommand+6�     5&       . $��24��6@   $��24��6L   $
��10,5q�6W     Screenshots_N# �  5'   %     Screenshots_H# �  5(   %     Screenshots# �  6V   # �  5)   %   #��[ 6{   ��6T   �� *GetPictureCommand+6�     5+       . $��24��6@   $��24��6L   $
��10,5q�6W    	 	Flyinto_N# � 	 5,   %    
 	Flyinto_H# � 
 5-   %     Flyinto# �  6R   # � 
 5.   %   #��[ 6r   ��6P   �� *FlytoCommand+6�   5/      50      51      �� 52      $��1��6   $��5q�6&   ���#1E2026$	��200��6F     TabControlStyle#���  6V   $��Stretch=�6�   .@�53      ��   Information#���  6     TabItemStyle#���  6B   .��54      � $��5q�6    55      �� 56      $
��Auto �6,   �� 57      $
��Auto �6,   �� 58      $
��Auto �6,   �� 59      $
��Auto �6,   �� 5:      $
��Auto �6,    � ColumnDefinitions 5<      �� 5=      $
��Auto �6/   6?   �� 5>      $��* �6/   6;   �� 5?      $
��Auto �6/   6?   �� 5@      $��* �6/   6;   .+�5C      �� $	��0,5q�6$   $��0��61   $��0��6>     DefaultTextBlockStyle#��[ 6N    	 Title#���  6~   6�   �� 5D      $
��10,5q�6#   $��0��61   $��1��6?   $��3��6O   $
��auto��6c   $��Stretch=�6q     TbxWaterStyle#���  6�   +   %Mmc.Mspace.Theme.Helper.TextBoxHelper   IsNeedClearButton .  ��5E   a   �� *
PoiTitle+ �� UpdateSourceTrigger$ PropertyChanged=��� 5F      $	��0,5q�6$   $��1��61   $��0��6>   #��[ 6N     	Longitude#���  6~   6�   �� 5G      $
��10,5q�6$   $��1��63   $��1��6@   #��[ 6P   ��6�   �� *Lng+ �� StringFormat$ {0:N5}°��6�   �� 5I      $	��0,5q�6$   $��1��61   $��2��6>   #��[ 6N     Latitude#���  6~   6�   �� 5J      $
��10,5q�6$   $��1��62   $��3��6?   #��[ 6O   ��6   �� *Lat+$ {0:N5}°��6�   �� 5L      $	��0,5q�6$   $��2��61   $��2��6>   #��[ 6N    
 Length#���  6~   6�   �� 5M      $
��10,5q�6$   $��2��62   $��3��6?   #��[ 6O   ��6   �� *
LenTranfer+�� 5O      $	��0,5q�6$   $��2��61   $��0��6>   #��[ 6N    
 Height#���  6~   6�   �� 5P      $
��10,5q�6$   $��2��62   $��1��6?   #��[ 6O   ��6   �� *Alt+$ {0:N3}m��6�   �� 5R      $��2��6!   $��3��6.   $
��10,5q�6>   $
��Auto��6L   $��Right=�6Z   $��20��6v     DefaultButtonStyle#���  6�     
GetAddress#���  6�   ��6�   �� *GetAddressCommand+6   �� 5T      $	��0,5q�6$   $��3��61   $��0��6>   #��[ 6N     Address#���  6~   6�   �� 5U      $
��10,5q�6#   $��3��61   $��1��6?   $��3��6O   $��50��6c   $
��Auto��6r     TbxWaterMaxStyle#���  6    �� TextWrapping$
 Wrap=�6�    �� AcceptsReturn . ��5V   2   �� *	Address+ 6K   �� *	Address+�� 5X      $	��0,5q�6$   $��4��61   $��0��6>   #��[ 6N     Tags#���  6~   6�   5  /Mmc.Mspace.PoiManagerModule.MultiSelectComboBox  5Y      -   5          Name$ MC��5Y   �   $��4��64   $��1��6A   $��3��6Q   $
��Auto��6e   $
��10,5q�6r      ItemsSource 6�   �� *
TagItems+   SelectedItems 6�   �� *SelectdTagItems+ 6�   ��  �� Path$
 Text� �� RelativeSource �� *Self+5[      5\      �� 5]        StyleSet#���  6   #���  6>   .��5^      � $��5q�6    5_      �� 5`      �� 5a      �� 5b      �� 5c       5e      �� 5f      $
��Auto �6/   6?   �� 5g      $	��45* �6/   6>   �� 5h      $	��89* �6/   .+�5j      �� $
��0,24q�6$   $��0��62   $��0��6?   #��[ 6O     	Linewidth#���  6   $��2��6�   6�   �� 5k      $��10,16,10,15q�6#   $��0��68   $��1��6F   #���  6V   $
��Auto��6   $��Stretch=�6�   $��2��5l   q   ��62   �� *	LineWidth+$ PropertyChanged=�6q   �� 5m      $
��0,24q�6$   $��1��62   $��0��6?   #��[ 6O    
 Colors#���  6   $��2��6�   6�   #  Xceed.Wpf.Toolkit.ColorPicker  5n      -   5          Name$ ColorPicker��5n   :   $
��10,5q�6,   $��1��6O   $��1��6\     P60BlackBrush#��[ 6l   $��2��6�   6�   �� 5o      $��	0,24,0,23q�6$   $��2��67   $��0��6D   #��[ 6T     	Elevation#���  6�   $��2��6�   6�   �� 5p      -   5        $��HeightTypesBox��5p   P   $��10,15q�6#   $��2��62   $��1��6?    �� DisplayMemberPath$ 	HeighName��6f      DefaultComboBoxStyle#���   6�   $��2��6  ��6�   �� *HeightTypes+ �� SelectedItem 6�   �� *SelectedHeightType+5q      5r      5s      �� 5t       	! Descr#��� ! 6    " TabItemStyle2#��� " 6;   .��5u      � $��5q�6    5v      �� 5w       5y      �� 5z      $
��Auto �6/   6?   �� 5{      $��* �6/   6<   .+�5}      �� $	��0,5q�6$   $��0��61   $��0��6>   $	��Top=�6N   #��[ 6f    	# Notes#��� # 6�   6�   �� 5~      $
��10,5q�6"   $��0��60   $��1��6=   $
��Auto��6M   $	��Top=�6Z   $	��150��6r   #���  6   $
 Wrap=�6�    . ��5      �� *Detial+5�      5�      5�      �� 5�      $��2��6   $��	0,10,0,20q�6#    $ OK#��� $ 67   $
��Auto��6W   $��Center=�6�   $	��100��6�   $��36��6�   #���  6�   {�6e   �� *IsSave+��6�   �� *	CreateCmd+6  5�      5�      5�      