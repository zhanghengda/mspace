   M S B A M L   `   `   `  ����K  GMmc.Mspace.Theme, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU<clr-namespace:Mmc.Mspace.Theme.Pop;assembly=Mmc.Mspace.ThemeMmc.Mspace.Theme.Pop  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089I2clr-namespace:System.Collections;assembly=mscorlibSystem.Collections 5      _Aclr-namespace:Mmc.Mspace.Theme.Controls;assembly=Mmc.Mspace.ThemeMmc.Mspace.Theme.Controls  5	      [?clr-namespace:Mmc.Mspace.Theme.Helper;assembly=Mmc.Mspace.ThemeMmc.Mspace.Theme.Helper  5
       Mmc.Mspace.PoiManagerModule;   5Mmc.Mspace.PoiManagerModule.Views.LabelManagementView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      LPub<clr-namespace:Mmc.Mspace.Theme.Pop;assembly=Mmc.Mspace.Theme     5      Bdic2clr-namespace:System.Collections;assembly=mscorlib     5      c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      OconrolsAclr-namespace:Mmc.Mspace.Theme.Controls;assembly=Mmc.Mspace.Theme  5	      LHelper?clr-namespace:Mmc.Mspace.Theme.Helper;assembly=Mmc.Mspace.Theme  5
      .  (System.Windows.Interactivity.Interaction    Triggers  5      /  )System.Windows.Interactivity.EventTrigger  5   
      	EventName$ Loaded��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6$   �� *LoadedCommand+5        5   
   $ Unloaded��6   . 5         6$   �� *UnloadedCommand+5       � 	Resources 5         ConsoleTaskListGroup��%  &	        �� 5   
   .R�5      � $��Center=�6   $��Center=�61   .+�5      �� $
��Left=�6    $��Center=�6;   $��0��6V   .+�5      �� $
��Left=�6"   
�������$��12(�6P   $��Center=�6u   ��6^   �� *name+6u   5      5      5      5      .��5    
   �  � RowDefinitions 5!   
   �� 5"      $
��Auto �6   �� 5#      $
��Auto �6   �� 5$      $
��Auto �6   �� 5%      �� 5&      $
��Auto �6   .+�5)   
   �� $
��16,8q�6   $��1��6!   $	��331��6/   $��Stretch=�5*        AddressTbxSearchStyle#���  5+      +   %Mmc.Mspace.Theme.Helper.TextBoxHelper   IsNeedClearButton .     ButtonCommand 5-      �� *SearchCommand+��5.      �� *
SearchText+ �� UpdateSourceTrigger$ PropertyChanged=��� 50   
   $��2��6   $	��363��6   
���   �$��0,1,0,0q�6?   � 52   
   $��2��6   	 � ColumnDefinitions	 53      �� 54      �� 55      .+�57      '   !Mmc.Mspace.Theme.Controls.Buttons    LMV_AddTags#���  6   $��0��58      $��	16,7,16,8q�6*   $	��150��59      $��36��6&     AddressAddNewBtnStyle#��[ 5:      $
��Left=�5<      {�59   2   �� *AddressBtnEnable+��5;      �� *CreateCommand+5<        5>      $��标签管理��6   $��1��5?      $��	16,7,16,8q�6.   $	��150��5@      $��36��6*   #��[ 66   $
��Left=�5B      ��5A      �� *LabelTypeMgtCommand+5B      5C      �� 5D   
   $��2��6   $��0,49,0,0q�6   $	��363��60   
���   �$��0,1,0,0q�6P   �� 5H   
   $��0,16,0,4q�6   $��3��6$   ._�5I      �� -   5        
 ��Name$
 
tagListBox��5I      
������  �� SelectionMode$ Single=�6e    �� SelectedIndex$ -1��5J        ListBoxItemNewStyle#���  6)   ��5I   C   �� *	TagSource+  5K        5L      $ SelectionChanged��6%   . 5M         60   �� *SelectTagCommand+   CommandParameter 6U   ��  �� ElementName$ 
tagListBox�� �� Path$ SelectedItem�5N      5P      5Q      5T      5U      