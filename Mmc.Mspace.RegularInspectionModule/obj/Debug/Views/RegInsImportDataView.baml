   M S B A M L   `   `   `  ����]  YMmc.Mspace.RegularInspectionModule, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullc6clr-namespace:Mmc.Mspace.RegularInspectionModule.Views(Mmc.Mspace.RegularInspectionModule.Views  5   	   K GMmc.Mspace.Theme, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_Aclr-namespace:Mmc.Mspace.Theme.Controls;assembly=Mmc.Mspace.ThemeMmc.Mspace.Theme.Controls 5   	   [?clr-namespace:Mmc.Mspace.Theme.Helper;assembly=Mmc.Mspace.ThemeMmc.Mspace.Theme.Helper 5	   	   C    =Mmc.Mspace.RegularInspectionModule.Views.RegInsImportDataView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Hlocal6clr-namespace:Mmc.Mspace.RegularInspectionModule.Views     5   	   Q MGFramework.BlankWindow, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'blankWindowhttp://GFramework.org  5   	   OcontrolAclr-namespace:Mmc.Mspace.Theme.Controls;assembly=Mmc.Mspace.Theme  5   	   Lhelper?clr-namespace:Mmc.Mspace.Theme.Helper;assembly=Mmc.Mspace.Theme  5	   	     � Title$  ImportRegInsDataView��5   	   $	��230��6&   $	��580��63   .��5      �  � RowDefinitions 5   
   �� 5      $��32 �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   ���#292C35$��0��63   $��2��6C    �� CornerRadius$ 5,5,0,0��6W   (  "GFramework.BlankWindow.BlankWindow   	Draggable . 6n   �� 5   
   -   5         ��Name$ 	TitleName��5       . $��0��6Q   $��0��6^   $��2��6n      LoadData#��[  6�   $��18,0,0,0q�6�   
�������$��14(�6�   $
��Left=�6�   $��Center=�6�   �� 5   
   -   5         ��Name$ CloseButton��5      $��0��6   $��0��63   $��2��6C     ButtonCloseStyle#���  6X   $��24��6�   $��24��6�   $��Right=�6�   $	��Top=�6�   $��0,2,8,0q�6�   ��6�   �� *	CancelCmd+� 5   
   $��1��6    5      �� 5      $
��auto �6    �� 5      $�� auto �6     � ColumnDefinitions 5      �� 5      $
��auto �6#   �� 5      $
��auto �6#   .+�5!      �� $��0��6   $��0��6!   
�������$��Center=�6D   $��8q�6_     ResultDataType#��[ 6j   �� 5"      $��0��6   $��1��6$   $��12q�64   $	��460��6@     PleaseChoose+  %Mmc.Mspace.Theme.Helper.TextBoxHelper   	Watermark# [ 6L     DefaultComboBoxStyle#���  6�    �� SelectedValuePath$	 Key��6�   	 �� DisplayMemberPath$	 Value��6�   
 �� SelectedItem
 6�   �� *SelectedItem+��6  �� *InspectionTypes+5#      �� 5%      $��1��6   $��0��6!   
�������$��Center=�6D   $��8q�6_     ResultDataLoad#��[ 6j   �� 5&      $��1��6   $��1��6#   $	��400��63   $
��Left=�6?   $
��12.5q�6Z     EditableTextBoxStyle#���  6h   ��6�   �� *DataFileName+'  !Mmc.Mspace.Theme.Controls.Buttons  5'      $��1��6   $��1��6+   $��Right=�6;   $��0,4,10,4q�6W   $��40��6i      HasPathData . 
�������  HomeLeftButtonStyle#���  6�     
Fileforder   RIcon# [ 6�    	 Fileforder_C   MouseOverRIcon# [	 6   
 Fileforder_H   PressedRIcon# [
 6?  ��6�   �� *ChooseFileCmd+6?  5)      �� 5+   
   -   5        $ okBtn��5+      $��2��6      OK#���  6-   $��Right=�6L   $��36��6h   $��0,20,130,30q�6t   $��Bottom=�6�   $��80��6�     DefaultButtonStyle#���  6�   {�6�   �� *IsSave+��6�   �� *	SaveCmd+�� 5,   
   -   5        $ 	cancelBtn��5,      $��2��6$    
 Cancel#���  61   $��Right=�6T   $��36��6p   $��
0,20,30,30q�6|   $��Bottom=�6�   $��80��6�   #���  6�   ��6�   �� *	CancelCmd+5-      5.      