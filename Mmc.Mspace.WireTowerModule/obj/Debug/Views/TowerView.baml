   M S B A M L   `   `   `  ����K  GMmc.Mspace.Theme, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_Aclr-namespace:Mmc.Mspace.Theme.Controls;assembly=Mmc.Mspace.ThemeMmc.Mspace.Theme.Controls  5   	   U QMmc.Mspace.WireTowerModule, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null[2clr-namespace:Mmc.Mspace.WireTowerModule.Converter$Mmc.Mspace.WireTowerModule.Converter 5
   	   0   *Mmc.Mspace.WireTowerModule.Views.TowerView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Q MGFramework.BlankWindow, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'blankWindowhttp://GFramework.org  5   	   PcontrolsAclr-namespace:Mmc.Mspace.Theme.Controls;assembly=Mmc.Mspace.Theme  5   	   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   B	converter2clr-namespace:Mmc.Mspace.WireTowerModule.Converter  5
   	     � Title$  	TowerView��5   	   $	��620��6   $	��600��6(    � 
ResizeMode$ NoResize=�64    � AllowsTransparency .  � 	Resources 5      <  6Mmc.Mspace.WireTowerModule.Converter.SignTypeConverter   SignTypeConverter��%   &	          5   
   6&   5      .��5      ��   LeftWindowBg#��[ 6    �� CornerRadius$ 5,5,5,5��68   ._�5   
   �  � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $��* �6    �� 5      $
��Auto �6     � ColumnDefinitions 5      �� 5      $��1* �6#   �� 5      $��1* �6#   .+�5      �� $��0��6   ���#292C35$��0��67   $��2��6G   $ 5,5,0,0��6[   (  "GFramework.BlankWindow.BlankWindow   	Draggable . 6r   �� 5      -   5         ��Name$ 	TitleName��5       . $��0��6U   $��0��6b   $��2��6r   $��18,0,0,0q�6�   
�������$��14(�6�   $
��Left=�6�   $��Center=�6�   ��6�   �� *	ViewTitle+6�   �� 5       -   5        	 ��Name$	 CloseButton��5    "   $��0��6   $��0��67   $��2��6G     ButtonCloseStyle#���  6~   $��24��6�   $��24��6�   $��Right=�6�   $	��Top=�6�   $��0,2,8,0q�6�   ��6[   �� *CancelCommand+6�   �� 5"      $��1��6   $��0��6&   .+�5#      �� $
��10,6q�6   
 �� Orientation$
 
Horizontal=�6,   $��Center=�6E   .+�5$      �� $��70��6     WTTowerName#��[ 6'     LabelTextStyle#��[ 6N   �� 5%      $	��150��6     EditableTextBoxStyle#��[ 6,   ��6Z   �� *TowerModel.Name+5&      �� 5(      $
��10,6q�6   $
 
Horizontal=�6,   $��Center=�6E   .+�5)      �� $��70��6     WTTowerSerial#��[ 6(   #��[ 6Q   �� 5*      $	��150��6   #��[ 6R   {�6,   �� *IsSerialEnabled+��6�   �� *TowerModel.Serial+5+      �� 5-      $
��10,6q�6   $
 
Horizontal=�6,   $��Center=�6E   .+�5.      �� $��70��6     WTSafeDistance#��[ 6'   #��[ 6Q   �� 5/      $	��150��6   #��[ 6*   ��6X   �� *TowerModel.SafeDistance+50      �� 52      $
��10,6q�6   $
 
Horizontal=�6+   $��Center=�6D   .+�54      �� $��70��6   #��[ 6'     WTTowerType#��[ 6O   �� 55      $	��150��6   $
��Left=�6+    �� SelectedIndex$ 0��6G    �� SelectedValuePath$	 Key��6Y    �� DisplayMemberPath$ Value��6q    	 DefaultComboBoxStyle#��� 	 6�   ��6�   �� *TowerTypeSet+ �� SelectedItem 6�   �� *SelectedType+6�   57      �� 59      $
��10,6q�6   $
 
Horizontal=�6,   $��Center=�6E   .+�5:      �� $��70��6    
 WTRelativeHeight#��[
 6'   #��[ 6S   �� 5;      $	��150��6   #��[ 6*   ��6X   �� *TowerModel.RelativeHeight+5<      5=      �� 5?      $��1��6   $��1��6&   $��Center=�67   .+�5A      �� $
��10,6q�6   $
 
Horizontal=�6+   $
��Left=�6D   .+�5B      �� $��70��6   #��[ 6'     WTTowerOwnerLine#��[ 6P   �� 5C      -   5         ��Name$ 
LineSetCmb��5C      $	��150��63   {�.  #��[	 6R   $ LineName��6�    6�   �� *SelectedLine+��6�   �� *	LineSet+.  (System.Windows.Interactivity.Interaction   Triggers 5D      /  )System.Windows.Interactivity.EventTrigger  5E         	EventName$ SelectionChanged��6-   .  (System.Windows.Interactivity.TriggerBase   Actions. 5F   "   6  0System.Windows.Interactivity.InvokeCommandAction     Command 68   ��  �� Path$# DataContext.SelectLineCommand� �� RelativeSource ��  �� AncestorLevel$ 1�� �� AncestorType" ��   CommandParameter 6�   �� *SelectedItem+ �� ElementName$ 
LineSetCmb��5G      5I      5J      �� 5L      $
��10,6q�6   $
 
Horizontal=�6+   $
��Left=�6D   .+�5M      �� $��70��6   #��[ 6'     WTLongitude#��[ 6P   �� 5N      $	��100��6    #��[ 6,   ��6Z   �� *TowerModel.X+5O      �� 5Q      $
��10,6q�6   $
 
Horizontal=�6+   $
��Left=�6D   .+�5R      �� $��70��6   #��[ 6(     
WTLatitude#��[ 6P   �� 5S      $	��100��6    #��[ 6-   ��6[   �� *TowerModel.Y+'   !Mmc.Mspace.Theme.Controls.Buttons  5T         HasPathData . $��24��6=   $��24��6I   $
��20,0q�6T     WT_Location   RIcon# �  5U   %     WT_Location_H   MouseOverRIcon# �  5V   %     
WTLocation � ToolTip# �  6V     WT_Location_C   PressedRIcon# �  5W   %     HomeLeftButtonStyle#��[ 6|   ��6T   �� *GetPositionCommand+6�   5X      �� 5Z      $
��10,6q�6   $
 
Horizontal=�6,   $
��Left=�6E   .+�5[      �� $��70��6   #��[ 6(     
WTAltitude#��[ 6P   �� 5\      $	��100��6    #��[ 6,   ��6Z   �� *TowerModel.Z+5]      5_      �� 5a      $��2��6   $��2��6&   $
 
Horizontal=�6:   $
��10,6q�6S   .+�5b      �� $��70��6     
WTSignList#��[ 6#   #��[ 6I     5d       . $��24��69   $��24��6E   $
��10,0q�6P    
 WT_Add# �  5e   !     WT_Add_H# �  5f   !    	 WTAdd# �  6M     WT_Add_C# �  5g   !   #��[ 6o   ��6K   �� *AddSignCommand+6�     5h       . $��24��69   $��24��6E   $
��10,0q�6P     	WT_UpMove# �  5i   !     WT_UpMove_H# �  5j   !     WTMoveUp# �  6P     WT_UpMove_C# �  5k   !   #��[ 6q   ��6N   �� *MoveUpCommand+6�     5l       . $��24��69   $��24��6E   $
��10,0q�6P     WT_DownMove# �  5m   !     WT_DownUpMove_H# �  5n   !     
WTMoveDown# �  6T      WT_DownMove_C# �   5o   !   #��[ 6u   ��6P   �� *MoveDownCommand+6�     5p       . $��24��69   $��24��6E   $
��10,0q�6P    ! WT_Edit# � ! 5q   &    " 	WT_Edit_H# � " 5r   &    # WTDetail# � # 6S    $ 	WT_Edit_C# � $ 5s   &   #��[ 6t   ��6Q   �� *DetailCommand+6t   5t      �� 5v      $��3��6   $��2��6&   $
��10,6q�6:   .+�5w      &   System.Windows.Controls.DataGrid  $   HorizontalScrollBarVisibility$ Hidden=�6   $	��270��6B   #��[ 6O       AutoGenerateColumns  .  
������ ��6z   �� *TowerModel.SignList+ 6�   �� *SelectedDataRow+!   Columns! 5x      0	  *System.Windows.Controls.DataGridTextColumn	  5y       % 
WTListName,
  &System.Windows.Controls.DataGridColumn" 
  Header#" [% 6-   # 
  Width5  /System.Windows.Controls.DataGridLengthConverter$# 60 6R    & DataGridColumnHeaderStyle$ 
  HeaderStyle#$ [& 6]    ' DataGridCellStyle% 
  	CellStyle#% [' 6�   1  +System.Windows.Controls.DataGridBoundColumn&   Binding& 6�   �� *name+	  5z       ( WTListSerial#" [( 6-   $# 80 6T   #$ [& 6_   #% [' 6�   & 6�   �� *serial+	  5{       ) 
WTListType#" [) 6-   $# 80 6R   #$ [& 6]   #% [' 6�   & 6�   �� *type+' �� 	Converter#' [  	  5|       
* PhotoX#" [* 6-   $# 80 6N   #$ [& 6Y   #% [' 6�   & 6�   �� *X+	  5}       
+ PhotoY#" [+ 6-   $# 80 6N   #$ [& 6Y   #% [' 6�   & 6�   �� *Y+	  5~       
, PhotoA#" [, 6-   $# 80 6N   #$ [& 6Y   #% [' 6�   & 6�   �� *Z+	  5       - 	WTListOpr#" [- 6-   $	# 120 6Q   #$ [& 6]    . DataGridCellOperateStyle#% [. 6�   5�      �� 5�      #��[ 65   ��6   �� *
HintText+65   5�      �� 5�      $��4��6   $��2��6&   $��
10,0,10,15q�6:   $
 
Horizontal=�6N   $��Center=�6g   .+�5�      �� -   5        $	 okBtn��5�       / OK#��� / 6)   $��Right=�6H   $��32��6d   $
��20,0q�6p   $��Bottom=�6�   $��60��6�   $��12(�6�    0 DefaultButtonStyle#��� 0 6�   ��6�   �� *SaveCommand+{�6  �� *IsOkBtnEnable+�� 5�      -   5        $	 	cancelBtn��5�       
1 Cancel#��� 1 6-   $��Right=�6P   $��32��6l   $
��20,0q�6x   $��Bottom=�6�   $��60��6�   $��12(�6�   #��� 0 6�   ��6�   �� *CancelCommand+5�      5�      5�      5�      