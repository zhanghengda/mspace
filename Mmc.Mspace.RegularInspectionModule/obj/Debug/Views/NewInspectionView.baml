   M S B A M L   `   `   `  ����]  YMmc.Mspace.RegularInspectionModule, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullc6clr-namespace:Mmc.Mspace.RegularInspectionModule.Views(Mmc.Mspace.RegularInspectionModule.Views  5   	   K GMmc.Mspace.Theme, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null[?clr-namespace:Mmc.Mspace.Theme.Helper;assembly=Mmc.Mspace.ThemeMmc.Mspace.Theme.Helper 5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5
   	   @    :Mmc.Mspace.RegularInspectionModule.Views.NewInspectionView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Hlocal6clr-namespace:Mmc.Mspace.RegularInspectionModule.Views     5   	   Q MGFramework.BlankWindow, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'blankWindowhttp://GFramework.org  5   	   Lhelper?clr-namespace:Mmc.Mspace.Theme.Helper;assembly=Mmc.Mspace.Theme  5   	   c	 _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity 	 5	   	   3system&clr-namespace:System;assembly=mscorlib 	 5
   	     � Title$  NewInspectionView��5   	   $	��250��6#   $	��346��60    � WindowStartupLocation$ CenterOwner=�6<      baseWindowStyle#���   6a   . 	 (System.Windows.Interactivity.Interaction   Triggers 5      / 	 )System.Windows.Interactivity.EventTrigger  5   
      	EventName$ Loaded��6   . 	 (System.Windows.Interactivity.TriggerBase   Actions. 5      6 	 0System.Windows.Interactivity.InvokeCommandAction     Command 6$   �� *LoadedCommand+5        5   
   $ Unloaded��6   . 5         6$   �� *UnloadedCommand+5      .��5      �  � RowDefinitions 5   
   �� 5      $��40 �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   ���#292C35$��0��63   $��2��6C    �� CornerRadius$ 5,5,0,0��6W   (  "GFramework.BlankWindow.BlankWindow   	Draggable . 6n   �� 5   
   -   5        	 ��Name$	 	TitleName��5       . $��0��6Q   $��0��6^   $��2��6n     CreateRegular#���  6�   $��18,0,0,0q�6�   
�������$��14(�6�   $
��Left=�6�   $��Center=�6�   �� 5   
   -   5        
 ��Name$
 CloseButton��5      $��0��6   $��0��63   $��2��6C     ButtonCloseStyle#���  6Y   $��36��6�   $��36��6�   $��Right=�6�   $	��Top=�6�   $��0,2,0,0q�6�   ��6�   �� *CancelCommand+6�   � 5!   
   $��1��6   $
��0,20q�6    5"      �� 5#      $
��Auto �6    �� 5$       � ColumnDefinitions 5&      �� 5'      $
��Auto �6#   �� 5(      .+�5*      �� $��10,5,0,5q�6   $��0��6*   $��0��67     DefaultTextBlockStyle#��[ 6G     Regulargoal#���  6w   6�   �� 5+      $��10,5,0,5q�6   $��1��6*   $��0��67   #��[ 6G     RegularDate#���  6w   6�   �� 5,      $
��10,5q�6     CreateRegularWater+  %Mmc.Mspace.Theme.Helper.TextBoxHelper   	Watermark# �  6%     TextBoxContextMenu � ContextMenu# �  6k   $��0��6�   $��1��6�    �� SelectedValuePath$ Id��6�    �� DisplayMemberPath$
 Name��6�     DefaultComboBoxStyle#���  6�   �� 
IsEditable . ��6�   �� *InspectRegions+ �� Text 6  �� *	NewName+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=� �� SelectedItem 6\  �� *SelectedItem+5-      (  "System.Windows.Controls.DatePicker  5.      $��1��6    �� Today   SelectedDate# Z 6)   $��Stretch=�6Y   $
��10,0q�6�   $��1��6�   $��Center=�6�   $
��Auto��6�      Text 6x   �� *InspectionDate+$ TwoWay=�$ PropertyChanged=�6�   51      �� 53   
    	 OK#��� 	 6    
 DefaultButtonStyle#��[
 6R   $��0��6   $��Center=�6�   $��36��6�   $��
0,0,100,30q�6�   $��2��6�   $��Bottom=�6�   $��80��6�   ��60   �� *CreateCommand+6�   �� 54   
    
 Cancel#���  6   $��0��64   #��[
 6f   $��Center=�6�   $��36��6�   $��
100,0,0,30q�6�   $��2��6�   $��Bottom=�6�   $��80��6�   ��6D   �� *CancelCommand+6�   56      57      