   M S B A M L   `   `   `  ����V  RMmc.Mspace.PoiManagerModule, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU/clr-namespace:Mmc.Mspace.PoiManagerModule.Views!Mmc.Mspace.PoiManagerModule.Views  5   	   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity 5      K GMmc.Mspace.Theme, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_Aclr-namespace:Mmc.Mspace.Theme.Controls;assembly=Mmc.Mspace.ThemeMmc.Mspace.Theme.Controls 5
   	   9    3Mmc.Mspace.PoiManagerModule.Views.ReportRankingView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Alocal/clr-namespace:Mmc.Mspace.PoiManagerModule.Views     5   	   ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     5      Q MGFramework.BlankWindow, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'blankWindowhttp://GFramework.org  5	   	   PcontrolsAclr-namespace:Mmc.Mspace.Theme.Controls;assembly=Mmc.Mspace.Theme  5
   	     � Title$  ReportRankingView��5   	   $	��380��6#   $	��300��60   .��5      �  � RowDefinitions 5   
   �� 5      $��32 �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   ���#292C35$��0��63   $��2��6C    �� CornerRadius$ 5,5,0,0��6W   (  "GFramework.BlankWindow.BlankWindow   	Draggable . 6n   �� 5   
   -   5         ��Name$ 	TitleName��5       . $��0��6Q   $��0��6^   $��2��6n   $��报告标注排序��6�   $��18,0,0,0q�6�   
�������$��14(�6�   $
��Left=�6�   $��Center=�6�   �� 5   
   -   5         ��Name$ CloseButton��5      $��0��6   $��0��63   $��2��6C      ButtonCloseStyle#���   6z   $��24��6�   $��24��6�   $��Right=�6�   $	��Top=�6�   $��0,2,8,0q�6�   ��6W   �� *CancelCommand+6�   �� 5      $��1��6    �� Orientation$ 
Horizontal=�6H   $��
12,20,12,4q�6a   y�6   �� *IsSizeCtrVisibility+.+�5      �� $��60��6     DefaultTextBlockStyle#��[ 6   $��排序方式��6K   6Y   �� 5      $	��120��6   $��24��6   $
��10,0q�6&    �� DisplayMemberPath$ Value��64     DefaultComboBoxStyle#���  6�   ��6N   �� *RankConditionSet+ �� SelectedItem 6w   �� *SelectedCondFirst+.  (System.Windows.Interactivity.Interaction	   Triggers	 5      /  )System.Windows.Interactivity.EventTrigger  5      
   	EventName$
 SelectionChanged��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6   �� *RankConditionSetChangedCmd+5!      5#      �� 5$      -   5         ��Name$ 
RankMethod��5$      $��75��6"   $��24��6-   $��0,0,10,0q�6:   $ Value��6L   #���  6�   ��6f   �� *RankMethodSet+ 6�   �� *SelectedMethodFirst+	 5%        5&      $
 SelectionChanged��6   . 5'         6   �� *SortChangedCmd+5(      5*      5+      &   System.Windows.Controls.DataGrid  59      -   5        $��Reportdg��5;      $��2��59      $
��Left=�6   $	��Top=�6<   $��2��6[    �� AlternationCount$ 2��6p      VerticalGridLinesBrush
 �   �   HorizontalGridLinesBrush
 ����    CanUserAddRows .     AutoGenerateColumns .     CanUserResizeColumns .  
���   �$	��220��5;   Q   ��6'   �� *PreviewCollection+   ColumnHeaderStyle 5A      �� 5B       �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6   .�5C      ��  �� Property � �� �� Value$ Center=�64   �� 5D       � �� �#1E20266$   �� 5E       � �� �#B3BBC56$   �� 5F       � ��$ 0,0,1,0q�6)   �� 5G       � �� �#0000006%   �� 5H       � ��$ 36��6    �� 5I       � ��$ 12(�6"   5J         RowStyle 5L      �� 5M      )  #System.Windows.Controls.DataGridRow"  6    �� Triggers 5N      P� 5O       P� Property �@ AlternationIndex P� Value$ 0��6,   . �5P   	   ��  � �� �#0A0B0D6&   �� 5Q   	    � �� �#C6CCD46&   5R   	   P� 5S       �@ AlternationIndex$ 1��6,   . �5T   	   ��  � �� �#14151A6&   �� 5U   	    � �� �#C6CCD46&   5V   	   P� 5W       �@J�IsMouseOver . . �5X   	   ��  � �� �#50ABFF6&   �� 5Y   	    � ��
 �����6&   5Z   	   P� 5[       �@ 
IsSelected . . �5\   	   ��  � �� �#1890FF6&   �� 5]   	    � ��
 �����6&   5^   	   5`         Columns 5b      4	  .System.Windows.Controls.DataGridTemplateColumn	  5c      ,
  &System.Windows.Controls.DataGridColumn 
  Header$ 标注名称��6    
  Width5  /System.Windows.Controls.DataGridLengthConverter$	 150 6+    	  CellTemplate 5d      �� 5e      .R�5f   	   ��   �� TextAlignment$  Center=�6   $��WordEllipsis=�6+   $��12(�6y   ! � ToolTip! 6G   �� *Title+��6a   �� *Title+6y   5g   	   5i      	  5j      $ 	创建人��6   $	 150 6*    5k      �� 5l      .R�5m   	   �� $  Center=�6   $��WordEllipsis=�6*   $��12(�6~   ! 6F   �� *
Operator+��6c   �� *
Operator+6~   5n   	   5p      5�      �� 5�      $��3��6   $��10,10q�6   $ 
Horizontal=�6+   $��Center=�6D   .+�5�      �� -   5        $ okBtn��5�        OK#���  6%   $��Right=�6D   $��24��6`   $
��20,0q�6l   $��Bottom=�6|   $��46��6�   $��10(�6�     DefaultButtonStyle#���  6�   ��6�   �� *SaveCommand+{�6�   �� *IsOkBtnEnable+�� 5�      -   5        $ 	cancelBtn��5�       
 Cancel#���  6)   $��Right=�6L   $��24��6h   $��10(�6t   $
��20,0q�6�   $��Bottom=�6�   $��46��6�   #���  6�   ��6�   �� *CancelCommand+5�      5�      5�      