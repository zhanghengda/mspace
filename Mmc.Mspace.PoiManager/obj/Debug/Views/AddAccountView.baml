   M S B A M L   `   `   `  ����K  GMmc.Mspace.Theme, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null[?clr-namespace:Mmc.Mspace.Theme.Helper;assembly=Mmc.Mspace.ThemeMmc.Mspace.Theme.Helper  5   	    Mmc.Mspace.PoiManagerModule6   0Mmc.Mspace.PoiManagerModule.Views.AddAccountView   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   RHelper?clr-namespace:Mmc.Mspace.Theme.Helper;assembly=Mmc.Mspace.Theme     5   	     � Title$  AddAccountView��5   	   $	��665��6    $	��480��6-    � ShowInTaskbar .   � WindowStyle$
 None=�6O    � AllowsTransparency .  � WindowState$ Normal=�5	       � 
ResizeMode$ NoResize=�6     � WindowStartupLocation$ CenterOwner=�68   ��5   |   Z� .��5
      �� $��1��6   
���   �$��1,0,1,1q�6.   ._�5   
   � ���#1E2026 � RowDefinitions 5   
   �� 5      $��40 �6   �� 5      .+�5   
   �� -   5        $��0��5      ���#292C35$��0��6k   $��2��6{    �� CornerRadius$ 5,5,0,0��6�   �� 5   
   -   5        	 ��Name$	 	TitleName��5      $��0��6`   $��0��6m   $��2��6}      
AddAccount#���   6�   $��18,0,0,0q�6�   
�������$��18(�6�   $
��Left=�6�   $��Center=�6  �� 5   
   -   5        
 ��Name$
 CloseButton��5      $��0��6   $��0��63   $��2��6C     ButtonCloseStyle#���  6Y   $��36��6�   $��36��6�   $��Right=�6�   $	��Top=�6�   $��0,2,8,0q�6�   ��6�   �� *CloseCommand+6�   � 5   
   $��1��6    5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $��* �6     � ColumnDefinitions 5!      �� 5"      $
��Auto �6#   �� 5#      .+�5'      �� $��10,5,0,5q�6   $��0��6*   $��0��67     DefaultTextBlockStyle#��[ 6G     AccountTitle#���  6w   6�   �� 5)      $��10,5,0,5q�6   $��1��6*   $��0��67   #��[ 6G     Accountaddress#���  6w   6�   �� 5+      $��10,5,0,5q�6   $��2��6*   $��0��67   #��[ 6G     Accountarea#���  6w   6�   �� 5-      $��10,5,0,5q�6   $��3��6*   $��0��67   #��[ 6G     Accountpersonnel#���  6w   6�   �� 5/      $��10,5,0,5q�6   $��4��6*   $��0��67   #��[ 6G     AccountpersonnelPhone#���  6w   6�   �� 51      $��10,5,0,5q�6   $��5��6*   $��0��67   #��[ 6G     Accountstate#���  6w   6�   �� 53      $��	10,15,0,5q�6   $	��Top=�6+   $��6��6C   $��0��6P   #��[ 6`    	 AccountPicurl#��� 	 6�   6�   �� 55      $��10,5,0,5q�6   $��7��6*   $��0��67   #��[ 6G    
 Accountvideourl#��� 
 6w   6�   �� 59      $
��10,5q�6   $��0��6&   $��1��64   $
��Auto��6D     TbxWaterStyle#���  6Q   $��Center=�6y   +   %Mmc.Mspace.Theme.Helper.TextBoxHelper   IsNeedClearButton .  ��5:   `   �� *AccountModel.Title+ �� UpdateSourceTrigger$ PropertyChanged=��� 5<      $
��10,5q�6   $��1��6&   $��1��64   $
��Auto��6D    �� TextWrapping$ NoWrap=�6�   #���  6�   $��Center=�6�    .   � ToolTip 6R   �� *AccountModel.Site+$ PropertyChanged=���5=   `   �� *AccountModel.Site+$ PropertyChanged=��� 5>      $
��10,5q�6   $��2��6&   $��1��64   $
��Auto��6D   #���  6Q   $��Center=�6y    .  ��5?   `   �� *AccountModel.Area+$ PropertyChanged=��� 5@      $
��10,5q�6   $��3��6&   $��1��64   $
��Auto��6D   #���  6Q   $��Center=�6y    .  ��5A   `   �� *AccountModel.Operator+$ PropertyChanged=��� 5B      $
��10,5q�6   $��4��6&   $��1��64   $
��Auto��6D   #���  6Q   $��Center=�6y    .  ��5C   `   �� *AccountModel.OperatorPhone+$ PropertyChanged=��� 5E      $
��10,5q�6   $��5��6%   $��1��62    �� SelectedValuePath$	 Key��6B    �� DisplayMemberPath$ Value��6Z    �� SelectedIndex$ 0��6�     DefaultComboBoxStyle#���  6�   ��6t   �� *AccountStatusSource+ �� SelectedItem 6�   �� *SelectStatus+5F      � 5H      $��6��6   $��1��6     5I      �� 5J      �� 5K       5M      �� 5N      $
��Auto �6'   �� 5O      �� 5P      $
��Auto �6'   .+�5R      	� -   5         	�Name$ radio1��5R      $��0��6Q   $
��10,5q�6^    d� 	IsChecked$
 TrueY�6l   $��0��6}   $
��Left=�6�     DefaultRadioButtonStyle#���  6�     Accountlocalupload#���  6�   ��6.   �� *CheckedCommand+6  	� 5S      -   5        $ radio2��5S      $��1��6Q   $
��10,5q�6_   $��0��6m   $
��Left=�6}   #���  6�     AccountUploadscr#���  6�   ��6.   �� *CheckedCommand+6�   �� 5T      $
��10,5q�6   {�.  $��0��6;   $��1��6I   $
��Auto��6Y   #���  6f   $��Center=�6�    .  ��5U   `   �� *AccountModel.Img+$ PropertyChanged=��� 5V        Accountupload#���  6   $��0��6v   $��2��6�     DefaultButtonStyle#��[ 6�   $��Right=�6�   $��36��6�   $��	10,5,10,5q�6
  $��Bottom=�6  $��80��69  {�6C   �� *	IsChecked+ �� ElementName$ radio1����6�   �� *UploadCommand+69  �� 5W      $��1��6   $��1��6*   $��2��6:    �� Orientation$ 
Horizontal=�6N   $��Center=�6g   $��0,0,10,0q�6�   .+�5X      ��   
Accountscr#���  6   $��1��6�   $��1��6�   #��[ 6�   $
��Left=�6�   $��36��6  $��10,5,0,5q�6  $��Center=�6&  $��80��6A  ��6D   �� *ScreenCaputredCommand+{�6n   �� *	IsChecked+$ radio2��6A  �� 5Y      $��10,0,0,0q�6   $��96��6�   $��64��6�   $
��Fill=�6�   ��6/   �� *AccountModel.Img+{�6S   �� *	IsChecked+$ radio2��5Z      5[      5\      �� 5_      $
��10,5q�6   $��7��6&   $��1��64   $
��Auto��6D   #���  6Q   $��Center=�6y    .  ��5`   `   �� *AccountModel.Video+$ PropertyChanged=��� 5f      ���#08080B$��0��6+   $��1��6;   $��3��6F   $��
10,0,10,90q�6Z   $��9��6n   $��Bottom=�6{   �� 5h        OK#���  6   #��[ 6V   $��0��6�   $��3��6�   $��Center=�6�   $��36��6�   $��
0,0,100,30q�6�   $��9��6�   $��Bottom=�6�   $��80��6  ��64   �� *CreateCommand+6  �� 5i       
 Cancel#���  6   $��0��68   $��3��6i   #��[ 6}   $��Center=�6�   $��36��6�   $��
100,0,0,30q�6�   $��9��6�   $��Bottom=�6�   $��80��6  ��6H   �� *CloseCommand+6  5k      5m      5n      5o      