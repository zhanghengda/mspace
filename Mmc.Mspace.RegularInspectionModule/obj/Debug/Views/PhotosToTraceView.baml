   M S B A M L   `   `   `  ����K  GMmc.Mspace.Theme, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_Aclr-namespace:Mmc.Mspace.Theme.Controls;assembly=Mmc.Mspace.ThemeMmc.Mspace.Theme.Controls  5      ] YMmc.Mspace.RegularInspectionModule, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullc6clr-namespace:Mmc.Mspace.RegularInspectionModule.Views(Mmc.Mspace.RegularInspectionModule.Views 5   	   [?clr-namespace:Mmc.Mspace.Theme.Helper;assembly=Mmc.Mspace.ThemeMmc.Mspace.Theme.Helper  5   
   @   :Mmc.Mspace.RegularInspectionModule.Views.PhotosToTraceView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   UcontrolAclr-namespace:Mmc.Mspace.Theme.Controls;assembly=Mmc.Mspace.Theme     5      Hlocal6clr-namespace:Mmc.Mspace.RegularInspectionModule.Views     5   	   Rhelper?clr-namespace:Mmc.Mspace.Theme.Helper;assembly=Mmc.Mspace.Theme     5   
   Q MGFramework.BlankWindow, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'blankWindowhttp://GFramework.org  5	   	   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5
   	     � Title$  PhotosToTrackView��5   	   $	��450��6#   $	��850��60   .��5      �  � RowDefinitions 5   
   �� 5      $��32 �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   .+�5   
   �� $��0��6   ���#292C35$��0��63   $��2��6C    �� CornerRadius$ 5,5,0,0��6W   (  "GFramework.BlankWindow.BlankWindow   	Draggable . 6n   �� 5   
   -   5         ��Name$ 	TitleName��5       . $��0��6Q   $��0��6^   $��2��6n      TracePhotoTitle#��[  6�   $��18,0,0,0q�6�   
�������$��14(�6�   $
��Left=�6�   $��Center=�6�   �� 5   
   -   5         ��Name$ CloseButton��5      $��0��6   $��0��63   $��2��6C     ButtonCloseStyle#���  6v   $��24��6�   $��24��6�   $��Right=�6�   $	��Top=�6�   $��0,2,8,0q�6�   ��6W   �� *	CancelCmd+6�   � 5   
   $��1��6   $	��0,8q�6    5      �� 5      $
��Auto �6    �� 5      $
��Auto �6     � ColumnDefinitions 5      �� 5      $��1* �6#   �� 5       $��1* �6#   .+�5#      �� $��0��6   $��0��6!   $
��Left=�61   $
��16,0q�6L   $��Center=�6Z     LabelTextStyle#���  6u     InspectObject#��[ 6�   �� 5$      -   5         ��Name$ InspectRegion��5$   4   $��0��6   $��0��6$     PleaseChoose+   %Mmc.Mspace.Theme.Helper.TextBoxHelper   	Watermark# [ 6K   $
��Left=�6�   $��80,0,0,0q�6�   $��24��6�   $	��154��6�   $��Center=�6�     DefaultComboBoxStyle#��[ 6�   	 �� SelectedValuePath$	 Id��6  
 �� DisplayMemberPath$

 Name��6/   �� SelectedIndex$ 1��6�  ��6H  �� *InspectRegions+ �� SelectedItem 6o  �� *SelectedRegion+.  (System.Windows.Interactivity.Interaction   Triggers 5%      /  )System.Windows.Interactivity.EventTrigger  5&         	EventName$ SelectionChanged��6%   .  (System.Windows.Interactivity.TriggerBase   Actions. 5'      6  0System.Windows.Interactivity.InvokeCommandAction     Command 60   ��  �� Path$&  DataContext.SelectInspectCommand� �� RelativeSource ��  �� AncestorLevel$ 1�� �� AncestorType" �   CommandParameter 6�   �� *SelectedItem+ �� ElementName$ InspectRegion��5(      5*      �� 5+      $��0��6   $��0��6$   # [ 64   $
��Left=�6s   $��	240,0,0,0q�6�   $��24��6�   $	��154��6�   $��Center=�6�   #��[ 6�   $		 Key��6  $
 Value��6  $ 0��6�  ��64  �� *InspectTimeRange+ 6]  �� *SelectedUnit+{�6�  �� *IsUnitCbxEnable+6�  �� 5-      $��0��6   $��1��6!   $
��Left=�61   $��16,4,0,0q�6L   $��Center=�6_   #���  6z     PhotosFolder#��[ 6�   �� 5.      $��0��6   $��1��6#   $
��Left=�63   $��80,4,0,0q�6N   $	��280��6`   {�.  $��Center=�6     NotEditableTextBoxStyle#��[ 6�   ��6�   �� *PhotosFolder+'   !Mmc.Mspace.Theme.Controls.Buttons  5/      $��0��6   $��1��6+      HasPathData . $
��Left=�6N   $��
360,04,0,0q�6i   $��40��6}   $��24��6�   $��Center=�6�     HomeLeftButtonStyle#���  6�    	 
Fileforder   RIcon# [	 6�    
 Fileforder_C   MouseOverRIcon# [
 6
    Fileforder_H   PressedRIcon# [ 69  ��6f  �� *ChoosePhotosFolderCmd+�� 52      $��1��6   $��0��6!   $
��Left=�61   $��16,4,0,0q�6L   $��Center=�6_   #���  6z     OutputFile2#��[ 6�   �� 53      $��1��6   $��0��6%   $
��Left=�65   $��80,4,0,0q�6P   $	��280��6b   $��Center=�6p     DefaultTextBlockStyle#��[ 6�   ��6�   �� *	TraceFile+  54      $��1��6   $��0��6+    . $
��Left=�6N   $��	360,4,0,0q�6i   $��40��6|   $��24��6�   $��Center=�6�   #���  6�   # [	 6�   # [
 6	  # [ 68  ��6e  �� *ChooseTraceFileCmd+�� 56      $��1��6   $��1��6$   $
��Left=�65   $��16,4,0,0q�6P   $��Center=�6c     CheckBoxStyle#���  6~    d� 	IsChecked 6�   �� *ThumbnailChecked+�� 57      $��1��6   $��1��6!   $
��Left=�61   $��36,4,0,0q�6L   $��Center=�6_   #���  6{     CreateThumb#��[ 6�   �� 59      $��1��6   $��1��6$   $
��Left=�65   $��	160,4,0,0q�6P   $��Center=�6d   #���  6    6�   �� *TraceLineChecked+�� 5:      $��1��6   $��1��6!   $
��Left=�61   $��	180,4,0,0q�6L   $��Center=�6`   #���  6|     CreateTraceLine#��[ 6�   5;      &   System.Windows.Controls.DataGrid  5=   
   $��2��6   $��	16,4,16,4q�6    $	��250��63   ���#0A0B0D   AutoGenerateColumns .  
������ ��6U   �� *PhotoPosModelList+ 6�   �� *SelectedPhotoPoint+   Columns 5>      0	  *System.Windows.Controls.DataGridTextColumn	  5?        PhotoNum,
  &System.Windows.Controls.DataGridColumn 
  Header# [ 6%    
  Width5  /System.Windows.Controls.DataGridLengthConverter$ 40 6I     DataGridColumnHeaderStyle  
  HeaderStyle#  [ 6T     DataGridCellStyle! 
  	CellStyle#! [ 6�   1  +System.Windows.Controls.DataGridBoundColumn"   Binding" 6�   �� *Id+	  5@        	PhotoName# [ 6%   $ 80 6I   #  [ 6T   #! [ 6�   " 6�   �� *Name+	  5A        	PhotoTime# [ 6%   $	 150 6I   #  [ 6U   #! [ 6�   " 6�   �� *Time+	  5B       
 PhotoX# [ 6%   $ 80 6F   #  [ 6Q   #! [ 6�   " 6�   �� *X+	  5C       
 PhotoY# [ 6%   $ 80 6F   #  [ 6Q   #! [ 6�   " 6�   �� *Y+	  5D       
 PhotoA# [ 6%   $ 80 6F   #  [ 6Q   #! [ 6�   " 6�   �� *Z+	  5E        	PhotoPath# [ 6%   $	 290 6I   #  [ 6U   #! [ 6�   " 6�   �� *Path+ 5G        5H      $ MouseDoubleClick��6!   . 5I         6,   �� *
FlyToCmd+5J      5L      �� 5N   
   -   5        $ okBtn��5N      $��3��6      OK#���  6-   $��Right=�6L   $��36��6h   $��0,20,130,30q�6t   $��Bottom=�6�   $��80��6�     DefaultButtonStyle#���  6�   ��6�   �� *	SaveCmd+{�6�   �� *IsOkBtnEnable+�� 5O   
   -   5        $ 	cancelBtn��5O      $��3��6$    
 Cancel#���  61   $��Right=�6T   $��36��6p   $��
0,20,30,30q�6|   $��Bottom=�6�   $��80��6�   #���  6�   ��6�   �� *	CancelCmd+5P      5Q      