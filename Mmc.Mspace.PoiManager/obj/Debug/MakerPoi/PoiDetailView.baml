   M S B A M L   `   `   `  ����K  GMmc.Mspace.Theme, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_Aclr-namespace:Mmc.Mspace.Theme.Controls;assembly=Mmc.Mspace.ThemeMmc.Mspace.Theme.Controls  5   	   [?clr-namespace:Mmc.Mspace.Theme.Helper;assembly=Mmc.Mspace.ThemeMmc.Mspace.Theme.Helper  5   	   V RMmc.Mspace.PoiManagerModule, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:Mmc.Mspace.PoiManagerModuleMmc.Mspace.PoiManagerModule 5	   	   /   )Mmc.Mspace.PoiManagerModule.PoiDetailView   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   J FMmc.Wpf.Toolkit, Version=1.0.0.1, Culture=neutral, PublicKeyToken=null&gvifmhttp://schemas.mmc.com/wpf  5   	   OconrolsAclr-namespace:Mmc.Mspace.Theme.Controls;assembly=Mmc.Mspace.Theme  5   	   LHelper?clr-namespace:Mmc.Mspace.Theme.Helper;assembly=Mmc.Mspace.Theme  5   	   5local)clr-namespace:Mmc.Mspace.PoiManagerModule  5	   	   -   5        $	��452��5
   A   $	��580��6N     � ShowInTaskbar  .   � WindowStyle$
 None=�6q    � AllowsTransparency .  � WindowState$ Normal=�5       � 
ResizeMode$ NoResize=�6     � WindowStartupLocation$ CenterOwner=�68   ��5
   �   Z� .��5      � ���#1E2026 � RowDefinitions 5   
   �� 5      $��40 �6   �� 5      .+�5   
   �� -   5        $��0��5      ���#292C35$��0��6l   $��2��6|    �� CornerRadius$ 5,5,0,0��6�   �� 5   
   -   5         ��Name$ 	TitleName��5      $��0��6a   $��0��6n   $��2��6~   $��18,0,0,0q�6�   
�������$��18(�6�   $
��Left=�6�   $��Center=�6�   ��6�   �� *
WinTitle+6�   �� 5   
   -   5        	 ��Name$	 CloseButton��5      $��0��6   $��0��63   $��2��6C      ButtonCloseStyle#���   6u   $��36��6�   $��36��6�   $��Right=�6�   $	��Top=�6�   $��0,2,8,0q�6�   ��6W   �� *
CloseCmd+6�   � 5   
   $��1��6   $��15q�6    5      �� 5      $
��Auto �6    �� 5      �� 5      $
��Auto �6!   .+�5      �� $��1q�6     HighlightBackBrush#���  6)   $��5q�6\   ._�5      �� 
 �� Orientation$
 
Horizontal=�6   .+�5      �� -   5        $��3q�5      $	��240��6G   $	��108��6S   $
��Fill=�6`   ��6(   �� *PoiInfo.img+5      �� 5       $
 Vertical=�6!   $��3q�68   $��Center=�6C   $��Center=�6`   .+�5!      '   !Mmc.Mspace.Theme.Controls.Buttons     HasPathData . $��24��6@   $��24��6L   $
��10,5q�6W     Update_N   RIcon# �  5"   %     Update_H   MouseOverRIcon# �  5#   %     Updatelocation � ToolTip# �  6Q      PressedRIcon# �  5$   %     HomeLeftButtonStyle#��[ 6t   ��6O   �� *GetLocalCommand+6�     5%       . $��24��6@   $��24��6L   $
��10,5q�6W     Screenshots_N# �  5&   %     Screenshots_H# �  5'   %     Screenshots# �  6V   # �  5(   %   #��[ 6{   ��6T   �� *GetPictureCommand+6�     5*       . $��24��6@   $��24��6L   $
��10,5q�6W    	 	Flyinto_N# � 	 5+   %    
 	Flyinto_H# � 
 5,   %     Flyinto# �  6R   # � 
 5-   %   #��[ 6r   ��6P   �� *FlytoCommand+6�   5.      5/      50      �� 51      $��1��6   $��5q�6&   ���#1E2026$	��200��6F   $��Stretch=�6V     TabControlStyle#���  6t   .@�52      ��   Information#���  6     TabItemStyle#���  6A   .��53      � $��5q�6    54      �� 55      $
��Auto �6,   �� 56      $
��Auto �6,   �� 57      $
��Auto �6,   �� 58      $
��Auto �6,   �� 59      $
��Auto �6,    � ColumnDefinitions 5;      �� 5<      $
��Auto �6/   6?   �� 5=      $��* �6/   6<   �� 5>      $
��Auto �6/   6?   �� 5?      $��* �6/   6<   .+�5B      �� $	��0,5q�6$   $��0��61   $��0��6>     DefaultTextBlockStyle#��[ 6N    	 Title#���  6~   6�   �� 5C      $
��10,5q�6#   $��0��62   $��1��6@   $��3��6Q   $
��Auto��6e     TbxWaterStyle#���  6r   $��Center=�6�   +   %Mmc.Mspace.Theme.Helper.TextBoxHelper   IsNeedClearButton .  ��5D   `   �� *
PoiTitle+ �� UpdateSourceTrigger$ PropertyChanged=��� 5F      $	��0,5q�6$   $��1��62   $��0��6@   #��[ 6P     	Longitude#���  6�   6�   �� 5G      $
��10,5q�6$   $��1��63   $��1��6A   #��[ 6Q   ��6�   �� *Lng+ �� StringFormat$ {0:N5}°��6�   �� 5I      $	��0,5q�6$   $��1��61   $��2��6>   #��[ 6N     Latitude#���  6~   6�   �� 5J      $
��10,5q�6$   $��1��62   $��3��6?   #��[ 6O   ��6   �� *Lat+$ {0:N5}°��6�   �� 5M      $	��0,5q�6$   $��2��61   $��0��6>   #��[ 6N    
 Height#���  6~   6�   �� 5N      $
��10,5q�6$   $��2��62   $��1��6?   #��[ 6O   ��6   �� *Alt+$ {0:N3}m��6�   �� 5P      $��2��6!   $��3��6.   $
��10,5q�6>   $��Right=�6M   $��20��6i     
GetAddress#���  6w   $
��Auto��6�     DefaultButtonStyle#���  6�   ��6�   �� *GetAddressCommand+6   �� 5T      $	��0,5q�6$   $��3��61   $��0��6>   #��[ 6N     Address#���  6~   6�   �� 5U      $��3��6"   $
��10,5q�6/   $��1��6>   $��3��6O   $��50��6c     HighlightForeBrush#��[ 6s    �� TextWrapping$
 Wrap=�6�    �� AcceptsReturn .   TbxWaterMaxStyle#���  5V   g   ��62   �� *	Address+ 6K   �� *	Address+6g   �� 5X      $	��0,5q�6$   $��4��61   $��0��6>   #��[ 6N     Tags#���  6~   6�   5  /Mmc.Mspace.PoiManagerModule.MultiSelectComboBox  5Y      -   5          Name$ MC��5Y   �   $��4��64   $��1��6A   $
��10,5q�6Q   $��3��6_      ItemsSource 6s   �� *
TagItems+   SelectedItems 6�   �� *SelectdTagItems+ 6�   ��  �� Path$
 Text� �� RelativeSource �� *Self+5[      5\      �� 5]        StyleSet#���  6   #���  6?   .��5^      � $��5q�6   $	��Top=�6&    5_      �� 5`       5b      �� 5c      $
��Auto �6/   6?   �� 5d      $��* �6/   6;   .+�5f      �� $	��0,5q�6$   $��0��61   $��0��6>   #��[ 6O     Icon#���  6   6�   �� 5g      -   5        $��poitypesBox��5g   O   $
��10,5q�6#   $��0��61   $��1��6>    �� DisplayMemberPath$ cat_name��6b     DefaultComboBoxStyle#���  6�   ��6   �� *
PoiTypes+ �� SelectedItem 6�   �� *SelectedPoiType+5h      5i      5j      �� 5k       	 Descr#���  6     TabItemStyle2#���  6<   .��5l      � $��5q�6    5m      �� 5n       5p      �� 5q      $
��Auto �6/   6?   �� 5r      $
��140* �6/   6?   �� 5s      $	��61* �6/   .+�5u      �� $��0,5,0,0q�6$   $	��Top=�65   $��0��6M   $��0��6Z   #��[ 6j    	  Notes#���   6�   $��2��6�   6�   �� 5v      $��	10,5,10,0q�6"   $��0��65   $��1��6B   $	��100��6R   $
 Wrap=�6b    . $	��Top=�5w   :   #���  6R   $��2��6}   ��6   �� *PoiInfo.detail+6}   5x      5y      5z      �� 5{      $��2��6   $��	0,10,0,20q�6"    ! OK#��� ! 67   $��Center=�6�   $	��100��6�   $��36��6�   #���  6�   {�6V   �� *IsSave+��6t   �� *	CreateCmd+6�   5|      5}      5~      