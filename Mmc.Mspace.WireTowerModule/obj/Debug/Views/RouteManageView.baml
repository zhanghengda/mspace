   M S B A M L   `   `   `  ����U  QMmc.Mspace.WireTowerModule, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS.clr-namespace:Mmc.Mspace.WireTowerModule.Views Mmc.Mspace.WireTowerModule.Views  5   	   K GMmc.Mspace.Theme, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_Aclr-namespace:Mmc.Mspace.Theme.Controls;assembly=Mmc.Mspace.ThemeMmc.Mspace.Theme.Controls 5      6    0Mmc.Mspace.WireTowerModule.Views.RouteManageView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @local.clr-namespace:Mmc.Mspace.WireTowerModule.Views     5   	   Q MGFramework.BlankWindow, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'blankWindowhttp://GFramework.org  5      PcontrolsAclr-namespace:Mmc.Mspace.Theme.Controls;assembly=Mmc.Mspace.Theme  5      $	��600��5
   	   $	��400��6   .��5   
   �   � RowDefinitions  5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $��* �6    .+�5'      �� $��1��6   $��40��6"   ._�5(      �� $��0,6,10,6q�6   $
��Left=�6/   $��Center=�6J    �� Orientation$ 
Horizontal=�6e   .+�5)      '  !Mmc.Mspace.Theme.Controls.Buttons     HasPathData . $��24��6=   $��24��6I   $��10,0,5,0q�6T      WT_Input   RIcon# �   5*   &     
WT_Input_H   MouseOverRIcon# �  5+   &     WTInput � ToolTip# �  6T     
WT_Input_C   PressedRIcon# �  5,   &     HomeLeftButtonStyle#��[ 6v   ��6R   �� *ImportCommand+6v   51      52      �� 54      $��2��6   $��0,6,0,20q�6"     LeftWindowBg#��[ 65   ._�55      &   System.Windows.Controls.DataGrid  $
��10,0q�6   $   HorizontalScrollBarVisibility$ Hidden=�6%      AutoGenerateColumns .  
������ ��6L   �� *
RouteSet+	   Columns	 56      0  *System.Windows.Controls.DataGridTextColumn  58        
WTListName,  &System.Windows.Controls.DataGridColumn
   Header#
 [ 6)      Width5  /System.Windows.Controls.DataGridLengthConverter$	 100 6N     DataGridColumnHeaderStyle   HeaderStyle# [ 6Z     DataGridCellStyle   	CellStyle# [ 6�   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6�   �� *	RouteName+  59       	 WTListSerial#
 [	 6)   $ 80 6P   # [ 6[   # [ 6�    6�   �� *Serial+  5:       
 WTListDistance#
 [
 6)   $ 80 6R   # [ 6]   # [ 6�    6�   �� *	TowerType+  5<        	WTListOpr#
 [ 6)   $	 120 6M   # [ 6Y    " DataGridCellDetailOperateStyle# [ 6�   5C      5D      5F      5G      