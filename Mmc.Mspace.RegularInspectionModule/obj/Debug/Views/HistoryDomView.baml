   M S B A M L   `   `   `  ����]  YMmc.Mspace.RegularInspectionModule, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullc6clr-namespace:Mmc.Mspace.RegularInspectionModule.Views(Mmc.Mspace.RegularInspectionModule.Views  5      K GMmc.Mspace.Theme, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null[?clr-namespace:Mmc.Mspace.Theme.Helper;assembly=Mmc.Mspace.ThemeMmc.Mspace.Theme.Helper 5      _Aclr-namespace:Mmc.Mspace.Theme.Controls;assembly=Mmc.Mspace.ThemeMmc.Mspace.Theme.Controls 5	      =    7Mmc.Mspace.RegularInspectionModule.Views.HistoryDomView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Hlocal6clr-namespace:Mmc.Mspace.RegularInspectionModule.Views     5      c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      Lhelper?clr-namespace:Mmc.Mspace.Theme.Helper;assembly=Mmc.Mspace.Theme  5      OcontrolAclr-namespace:Mmc.Mspace.Theme.Controls;assembly=Mmc.Mspace.Theme  5	      .��5      � ���#1E2026  � ColumnDefinitions  5   
   �� 5      $
��1.5* �6   �� 5      $
��8.5* �6   .+�5   
   � $��0��6    � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6      5      �� 5      $
��Auto �6#   �� 5      $��* �6#   .+�5      �� $��0��6   $
��Left=�6%   $
��10,0q�6@      TextBlock12Style#���   6N     InspectRegion2#��[ 6y   �� 5      -   5         ��Name$ InspectRegion��5   4   $��0��6   $��1��6$     PleaseChoose+  %Mmc.Mspace.Theme.Helper.TextBoxHelper   	Watermark# [ 6K   $
��10,5q�6�     TextBoxContextMenu � ContextMenu# �  6�    �� SelectedValuePath$	 Key��6�    �� DisplayMemberPath$ Value��6�     DefaultComboBoxStyle#���  6&  ��6�   �� *InspectRegions+ �� SelectedItem 6U  �� *SelectedRegion+.  (System.Windows.Interactivity.Interaction   Triggers 5      /  )System.Windows.Interactivity.EventTrigger  5      	   	EventName$	 SelectionChanged��6%   .  (System.Windows.Interactivity.TriggerBase
   Actions.
 5      6  0System.Windows.Interactivity.InvokeCommandAction     Command 60   ��  �� Path$% DataContext.SelectRegionCommand� �� RelativeSource ��  �� AncestorType" >�   CommandParameter 6�   �� *SelectedItem+ �� ElementName$ InspectRegion��5       5"      �� 5#      $��1��6   $��0��6%   $
��Left=�65   $
��10,0q�6P   #���   6_     InspectTimeUnit2#��[ 6�   �� 5$      -   5        $ InspectTimeRange��5$   C   $��1��6   $��1��6$   $
��10,5q�65   # [ 6]   # �  6�    �� SelectedIndex$ 0��6�   $	 Key��6�   $ Value��6�   #���  6<  ��6  �� *InspectTimeRange+ 6m  �� *SelectedTimeRange+ 5%        5&      $	 SelectionChanged��6%   .
 5'         60   �� $( "DataContext.SelectTimeRangeCommand� �� " >� 6�   �� *SelectedItem+$ InspectTimeRange��5(      5*      5+      � 5-   
   $��1��6   $��Stretch=�6!     5.      �� 5/      $
��Auto �6#   �� 50      $��* �6#   �� 51      $
��Auto �6#   .+�53      '  !Mmc.Mspace.Theme.Controls.Buttons  $��0��6      HasPathData . $��32��54      $��81��6)   $	��Top=�55      $��0,6,0,0q�56        HomeLeftButtonStyle#���  59       	 LMove   RIcon# [ 5:        LMove_C   MouseOverRIcon# [ 5;       	 LMove_H   PressedRIcon# [	 5<      ��57      �� *LeftMoveCmd+{�58      �� *LeftBtnIsEnable+5<      �� 5=      -   5         ��Name$ DisplayDomBox��5=   D   $��1��6   $��Center=�6'   ���#1E2026 J� 	AllowDrop . ��6[   �� *DisplayDomList+ 6�   �� *SelectedDom+��5>      �� 5?      .R�5@      ��  �� Orientation$ 
Horizontal=�6%   $
��Left=�6>   5A      ��5C      �� 5D       
 ListBoxItemStyle �� BasedOn# [
 5E       �� 
TargetType" ��5F      .�5G      ��  �� Property �@��
IsSelected �� Value 5I      �� *	IsChecked+�� 5J       �@J�	AllowDrop . 5L      5N      ��5P      �� 5Q      .R�5R      � .+�5U      �� $ Vertical=�6)   $��3q�6@   $
��Left=�6K   .+�5W   "   �� $	��Top=�6(   $	��144��6>   $��81��6K   $��UniformToFill=�6W   $
��Left=�6p   $��Center=�6�   ��6�   �� *	Thumbnail+�� 5X   "   $��Center=�6(   
���������6W   �� *Time+5[      �� 5]       ��Name$ 
Dom_Border��6%    . $��Stretch=�6J   
������ $��Stretch=�6�    � Cursor$
 Hand��6�    5^   "     5_   &   $ 	 PreviewMouseLeftButtonDown��65   .
 5`   *      6@   �� $" DataContext.DragDomToOpenCmd� �� " >� 6�   �� *.+5a   '   5c      5d      5e       5g        5h      $	 MouseDoubleClick��6%   .
 5i         60   �� $ DataContext.ChoosedDomCmd� �� " >� 6�   �� *SelectedItem+$ DisplayDomBox��5j      5n        5o      $��2��6   $��32��60   $��81��6;   $	��Top=�6G   $��0,6,0,0q�6_   #���  5p       	 RMove# [ 5q        RMove_C# [ 5r        RMove_H# [ 5s      {�5o   q   �� *RightBtnIsEnable+��6�   �� *RightMoveCmd+5s      5t      5u      5v      