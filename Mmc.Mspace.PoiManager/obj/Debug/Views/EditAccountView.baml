   M S B A M L   `   `   `  ����V  RMmc.Mspace.PoiManagerModule, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU/clr-namespace:Mmc.Mspace.PoiManagerModule.Views!Mmc.Mspace.PoiManagerModule.Views  5   	   K GMmc.Mspace.Theme, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null[?clr-namespace:Mmc.Mspace.Theme.Helper;assembly=Mmc.Mspace.ThemeMmc.Mspace.Theme.Helper 5   !   7    1Mmc.Mspace.PoiManagerModule.Views.EditAccountView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Alocal/clr-namespace:Mmc.Mspace.PoiManagerModule.Views     5   	   RHelper?clr-namespace:Mmc.Mspace.Theme.Helper;assembly=Mmc.Mspace.Theme     5   !   Q MGFramework.BlankWindow, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'blankWindowhttp://GFramework.org  5   	     � Title$  EditAccountView��5
   	   $	��605��6!   $	��480��6.   .��5      � ���#1E2026 � RowDefinitions 5   
   �� 5      $��40 �6   �� 5      .+�5   
   �� $��0��6   ���#292C35$��0��63   $��2��6C    �� CornerRadius$ 5,5,0,0��6W   (  "GFramework.BlankWindow.BlankWindow   	Draggable . 6n   �� 5   
   -   5         ��Name$ 	TitleName��5       . $��0��6Q   $��0��6^   $��2��6n      EditAccount#���   6�   $��18,0,0,0q�6�   
�������$��18(�6�   $
��Left=�6�   $��Center=�6�   �� 5   
   -   5         ��Name$ CloseButton��5      $��0��6   $��0��63   $��2��6C      	Closeable .   ButtonCloseStyle#���  6�   $��36��6�   $��36��6�   $��Right=�6�   $	��Top=�6�   $��0,2,8,0q�6�   � 5   
   $��1��6    5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5       $��* �6     � ColumnDefinitions 5"      �� 5#      $
��Auto �6#   �� 5$      �� 5%      $
��Auto �6#   .+�5)      �� $��10,5,0,5q�6   $��0��6*   $��0��67     DefaultTextBlockStyle#��[ 6G     AccountTitle#���  6w   6�   �� 5+      $��10,5,0,5q�6   $��1��6*   $��0��67   #��[ 6G     Accountaddress#���  6w   6�   �� 5-      $��10,5,0,5q�6   $��2��6*   $��0��67   #��[ 6G     Accountarea#���  6w   6�   �� 5/      $��10,5,0,5q�6   $��3��6*   $��0��67   #��[ 6G     Accountpersonnel#���  6w   6�   �� 51      $��10,5,0,5q�6   $��4��6*   $��0��67   #��[ 6G     AccountpersonnelPhone#���  6w   6�   �� 52      $��10,5,0,5q�6   $��5��6*   $��0��67   #��[ 6G     Accountstate#���  6w   6�   �� 54      $��	10,15,0,5q�6   $	��Top=�6+   $��6��6C   $��0��6P   #��[ 6`   #���  6�   6�   �� 56      $��10,5,0,5q�6   $��7��6*   $��0��67   #��[ 6G    	 AccountPicurl#��� 	 6w   6�   �� 58      $��10,5,0,5q�6   $��8��6*   $��0��67   #��[ 6G    
 Accountvideourl#��� 
 6w   6�   �� 5:      $��10,5,0,5q�6   $��0��6*   $��1��68   $��2��6H   #��[ 6\   ��6�   �� *AccountModel.MarkerId+6�   �� 5<      $
��10,5q�6   $��1��6&   $��1��64   $��2��6D   $
��Auto��6X     TbxWaterStyle#���  6e   $��Center=�6�   +  %Mmc.Mspace.Theme.Helper.TextBoxHelper   IsNeedClearButton .  ��5=   `   �� *AccountModel.Title+	 �� UpdateSourceTrigger$	 PropertyChanged=��� 5>      $
��10,5q�6   $��2��6&   $��1��64   $��2��6D   $
��Auto��6X   #���  6e   $��Center=�6�    .  ��5?   `   �� *AccountModel.Site+$	 PropertyChanged=��� 5@      $
��10,5q�6   $��3��6&   $��1��64   $��2��6D   $
��Auto��6X   #���  6e   $��Center=�6�    .  ��5A   `   �� *AccountModel.Area+$	 PropertyChanged=��� 5B      $
��10,5q�6   $��4��6&   $��1��64   $��2��6D   $
��Auto��6X   #���  6e   $��Center=�6�    .  ��5C   `   �� *AccountModel.Operator+$	 PropertyChanged=��� 5E      $
��10,5q�6   $��5��6&   $��1��64   $��2��6D   $
��Auto��6X   #���  6e   $��Center=�6�    .  ��5F   `   �� *AccountModel.OperatorPhone+$	 PropertyChanged=��� 5H      -   5        $��poitypesBox��5H   V   $
��10,5q�6   $��6��6%   $��1��62   $��2��6B   
 �� SelectedValuePath$	
 Key��6i    �� DisplayMemberPath$ Value��6�    �� SelectedIndex$ 0��6�     DefaultComboBoxStyle#���  6  ��6�   �� *AccountStatusSource+ �� SelectedItem 6�   �� *AccountModel.Status+5I      �� 5K      $��7��6   $��1��6#   $
��10,5q�64   $
��Auto��6D   #���  6Q   $��Stretch=�5L   '    .  ��6}   �� *AccountModel.Img+$	 PropertyChanged=��� 5M      $��7��6   $��2��6"     Accountupload#���  63     DefaultButtonStyle#��[ 6]   $��Right=�6�   $��36��6�   $��	10,5,10,5q�6�   $��Bottom=�6�   $��80��6�   �� 5O      $��8��6   $��1��6%   $
��10,5q�65   $
��Auto��6C   #���  6P   $��Center=�6x   $��Stretch=�5P   *    .  ��6   �� *AccountModel.Video+$	 PropertyChanged=��� 5Q      #���  6   $��8��6@   $��2��6M   #��[ 6]   $��Right=�6�   $��36��6�   $��	10,5,10,5q�6�   $��Bottom=�6�   $��80��6�   �� 5V      ���#08080B$��0��6+   $��1��6;   $��3��6F   $��
10,0,10,90q�6Z   $��9��6n   $��Bottom=�6{   �� 5X        OK#���  6   #��[ 6T   $��0��6�   $��3��6�   $��Center=�6�   $��36��6�   $��
0,0,100,30q�6�   $��9��6�   $��Bottom=�6�   $��80��6
  ��64   �� *SaveCommand+6
  �� 5Y       
 Cancel#���  6   $��0��68   $��3��6H    . #��[ 6�   $��Center=�6�   $��36��6�   $��
100,0,0,30q�6�   $��9��6�   $��Bottom=�6�   $��80��6  5[      5\      5]      