   M S B A M L   `   `   `  ����V  RMmc.Mspace.PoiManagerModule, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU/clr-namespace:Mmc.Mspace.PoiManagerModule.Views!Mmc.Mspace.PoiManagerModule.Views  5   	   K GMmc.Mspace.Theme, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_Aclr-namespace:Mmc.Mspace.Theme.Controls;assembly=Mmc.Mspace.ThemeMmc.Mspace.Theme.Controls 5   	   [?clr-namespace:Mmc.Mspace.Theme.Helper;assembly=Mmc.Mspace.ThemeMmc.Mspace.Theme.Helper 5	   	   9    3Mmc.Mspace.PoiManagerModule.Views.TileLayerPropView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Alocal/clr-namespace:Mmc.Mspace.PoiManagerModule.Views     5   	   VcontrolsAclr-namespace:Mmc.Mspace.Theme.Controls;assembly=Mmc.Mspace.Theme     5   	   RHelper?clr-namespace:Mmc.Mspace.Theme.Helper;assembly=Mmc.Mspace.Theme     5	   	   Q MGFramework.BlankWindow, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'blankWindowhttp://GFramework.org  5
   	     � Title$  TileLayerPropView��5   	   $	��448��6#   $	��278��60   .��5      � ���#1E2026 � RowDefinitions 5   
   �� 5      $��40 �6   �� 5      $��48 �6   �� 5      $	��312��6    � ColumnDefinitions 5   
   �� 5      $	��90* �6   6-   .+�5   
   �� $��0��6   ���#292C35$��0��63   $��2��6C    �� CornerRadius$ 5,5,0,0��6W   (  "GFramework.BlankWindow.BlankWindow   	Draggable . 6n   �� 5   
   -   5         ��Name$ 	TitleName��5       . $��0��6Q   $��0��6^   $��2��6n      TLPV_TiltFlattening#���   6�   $��18,0,0,0q�6�   
�������$��18(�6�   $
��Left=�6�   $��Center=�6�   �� 5   
   -   5         ��Name$ CloseButton��5      $��0��6   $��0��63   $��2��6C     ButtonCloseStyle#���  6v   $��36��6�   $��36��6�   $��Right=�6�   $	��Top=�6�   $��0,2,8,0q�6�   ��6W   �� *	CancelCmd+6�   �� 5   
   $��1��6    �� Orientation$ 
Horizontal=�6"   $
��Left=�6;   $��8,10,8,6q�6V   .+�5      �� -   5        $��txtSerachDataName��5      
�������$	��136��6u   $��31��6�   $��Center=�6�   ��60   �� *SearchTileCondition+ �� Mode$ TwoWay=�.��6�   	 � 	Resources	 5        	HelpBrush
 j� TileMode j� Stretch j� 
AlignmentX !� Visual " TileLayerPropView_SearchByName��%�   &	       !� 5      $

 None=�64   $
 None=�6E   $
 Left=�6T    5      �� 5      $��Normal$�6(   
���iii�#���  6Q   5       5!      .��6%   ��5"      �� 5#       �� 
TargetType" ��6   .�5$      ��  �� Property � �� �� Value$ 23��63   6@   �� 5%       � ��$
 Left=�6@   6O   �� 5&       � ��$ Center=�6>   6P   �� 5'       � ��
 �   �6I    �� Triggers 5(      ]� 5)       ]� 
Conditions 5*   "   �� 5+   &    �� Property �@J�	IsFocused �� Value .  6E   �� 5,   &    � ��$  ��6@   /�5.   "   �� 5/   &    � ��# [ 6C   51      ]� 52       53   "   �� 54   &    �@J�	IsFocused .  6E   �� 55   &    � �� 6@   Z� /�57   "   �� 58   &    � ��# [ 6C   5:      5<      .��5=   !   5>      �� 5?        TLPV_Search#���  6   $��31��6_   $��48��6k   $��8,0,16,0q�6v     DefaultButtonStyle#��[ 6�   ��6=   �� *SearchTileCmd+6�   �� 5@        TLPV_Add#���  6   $��31��6]   $��48��6i   #��[ 6t   ��6;   �� *AddPolygonCmd+6t   5B      &   System.Windows.Controls.DataGrid  5F   
   -   5        $��tiledatagrid��5H      $��2��5F      $��Center=�6!    �� AlternationCount$ 2��6E      HorizontalGridLinesBrush
 ����    GridLinesVisibility$
 None=�6�   $��1q�6�      CanUserAddRows .     AutoGenerateColumns .     CanUserResizeColumns . 
���   �$	��258��5H   R    �� SelectedItem 5G   ~   �� *	SelectGeo+$ TwoWay=���5H   /   �� *
GeoVModels+   ColumnHeaderStyle 5I      �� 5J      =  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6   .�5K      ��  � ��$ Center=�6C   �� 5L       � �� �#1E202663   �� 5M       � �� �#B3BBC563   �� 5N       � ��$ 0,0,1,1q�68   �� 5O       � �� �#00000064   �� 5P       � ��$ 36��6/   �� 5Q       � ��$ 12(�61   5R         RowStyle 5T      �� 5U      )  #System.Windows.Controls.DataGridRow"  6   .�5V      ��  � ��$ 36��6/    5W      P� 5X       P� Property �@ AlternationIndex P� Value$ 0��6>   . �5Y      ��  � �� �#0A0B0D6;   5Z      P� 5[       �@ AlternationIndex$ 1��6>   . �5\      ��  � �� �#14151A6;   5]      P� 5^       �@J�IsMouseOver . . �5_      ��  � �� �#50ABFF6;   5`      P� 5a       �@ 
IsSelected . . �5b      ��  � �� �#1890FF6;   �� 5c       � �� �#EBF5FF6;   5d      5f          Columns  5h      4  .System.Windows.Controls.DataGridTemplateColumn  5r      ,  &System.Windows.Controls.DataGridColumn!   Width5  /System.Windows.Controls.DataGridLengthConverter$	! 120 6)     	TLPV_Name"   Header#" �  65   #   CellTemplate# 5s      �� 5t      .R�5u      �� ���#B3BBC5$��12(�6>   $��WordEllipsis=�6L   $ �� TextAlignment$$ Center=�6�   % � ToolTip% 6h   �� *
Label.Text+��6�   �� *
Label.Text+6�   5v      5x        5y        TLPV_Operator#" �  6)   $	! 136 6R   # 5z      �� 5{      .R�5|      �� $ 
Horizontal=�6)   .+�5}   "   '  !Mmc.Mspace.Theme.Controls.Buttons  &  Name$	& bs1��63   $��16��6@   $��16��6L   $��8,0,4,0q�6W    	 
TLPV_FlyTo'   RIcon#' � 	 5~   1    
 TLPV_FlyTo_H(   MouseOverRIcon#( � 
 5   1     TLPV_FlyTo_C)   PressedRIcon#) �  5�   1   
* � Tag#* � 
 5�   1     TileLayerBtnStyle#��[ 5�   1   ��5�   1   �� *DataContext.FlytoCmd++ �� RelativeSource+ �� , �� AncestorLevel$, 1��- �� AncestorType$- DataGridL���5�   1   �� *SelectedItem+. �� ElementName$. tiledatagrid��  5�   "   $
& bs22��63   $��16��6A   $��16��6M   $��4,0,4,0q�6X     TLPVEdit#' �  5�   1     TLPV_Edit_H#( �  5�   1     TLPV_Edit_C#) �  5�   1   #* �  5�   1   #��[ 5�   1   ��5�   1   �� *DataContext.EditPolygonCmd++ �� $, 1��$- DataGridL���5�   1   �� *SelectedItem+$. tiledatagrid��  5�   "   $	& bs3��63   $��16��6@   $��16��6L   $��4,0,4,0q�6W     	TLPV_Move#' �  5�   1     TLPV_Move_H#( �  5�   1     TLPV_Move_C#) �  5�   1   #* �  5�   1   #��[ 5�   1   ��5�   1   �� *DataContext.MovePolygonCmd++ �� $, 1��$- DataGridL���5�   1   �� *SelectedItem+$. tiledatagrid��  5�   "   $	& bs4��63   $��16��6@   $��16��6L   $��4,0,8,0q�6W     TLPV_Del#' �  5�   1     
TLPV_Del_H#( �  5�   1     
TLPV_Del_C#) �  5�   1   #* �  5�   1   #��[ 5�   1   ��5�   1   �� *DataContext.DelPolygonCmd++ �� $, 1��$- DataGridL�5�      5�      5�      5�      5�      5�      