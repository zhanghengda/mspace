   M S B A M L   `   `   `  ����O  KMmc.Mspace.IotModule, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:Mmc.Mspace.IotModule.ViewsMmc.Mspace.IotModule.Views  5   	   K GMmc.Mspace.Theme, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null[?clr-namespace:Mmc.Mspace.Theme.Helper;assembly=Mmc.Mspace.ThemeMmc.Mspace.Theme.Helper 5
   	   2    ,Mmc.Mspace.IotModule.Views.PatrolmanListView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   :local(clr-namespace:Mmc.Mspace.IotModule.Views     5   	   Q MGFramework.BlankWindow, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'blankWindowhttp://GFramework.org  5   	   j fGalaSoft.MvvmLight.Extras.WPF4, Version=4.3.31.28629, Culture=neutral, PublicKeyToken=1673db7d5906b0ad+mvvm http://www.galasoft.ch/mvvmlight  5   	   c	 _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity 	 5	   	   LHelper?clr-namespace:Mmc.Mspace.Theme.Helper;assembly=Mmc.Mspace.Theme 	 5
   	     � Title$  PatrolmanListView��5   	   $	��480��6#   $	��323��64      baseWindowStyle#��[  6@    � 
ResizeMode$ NoResize=�6i    � 	Resources 5        InspectorData ��Name � Cursor �� CornerRadius  LeftWindowBg � RowDefinitions  TextBlock12Style ��Name � ToolTip  FilterLabelCBStyle	 �� RelativeSource
 �� AncestorLevel �� AncestorType �� Triggers P� Property P� Value �� 
TargetName �� Property �� Value �� Value �� Binding  blackBackground �� 
TargetType J� SnapsToDevicePixels��%�  &	       7  7  7  7  7  7  7  7  7  &	 �    �� 5   
   .R�5      �� $ PersonBorder��6   $	��0,4q�6+   $	��100��68   $	��296��6E   $
 Hand��6Q   $ 3��6_   
����ح�$��1q�6�   8��  ._�5      �  5      �� 5      $
��Auto �6(   �� 5      $
��Auto �6(   �� 5      $
��Auto �6(   .+�5      �� $��0��6    $��	10,16,0,0q�6-   $
��Left=�6A   8�� ��6]   �� *Name+6s   �� 5      $��0��6    $��
60,16,16,0q�6-   8�� ��6m   �� *Status+�� 5      $ PeoplePosition��6    $��23��68   $��56��6D   $��
10,16,56,0q�6Q   $��位置��5   
   $ 位置��5      $��Right=�5       ���#50ABFF8�� ��5"      �� *DataContext.QueryPositionCmd+	 �� $
 1��" ���6�   �� *.+y�5$      �� *People_Visibility+�� 5%      $ Route��6    $��23��6/   $��56��6;   $��
30,16,16,0q�6H   $��轨迹��5&   
   $ 轨迹��5'      $��Right=�5(      ���#50ABFF8�� ��5*       �� *DataContext.QueryRouteCmd+	 �� $
 1��" ���5+   !   �� *.+5,       �� 5/      $��1��6    8�� $��10,4,0,4q�6X   $��	电话：��6k   �� 50      $��1��6    8�� $��56,4,0,4q�6X   ��6k   �� *Phone+�� 51      $��2��6    8�� $��10,4,0,4q�6X   $��打卡时间：��6k   �� 52      $��2��6    $��74,4,0,4q�6.   8�� ��6j   �� *
SignInTime+53      54       56      P� 57       �@J�IsMouseOver . . �58      �� $ PersonBorder��6D    � �� �#50ABFF�� 59      $ PersonBorder��6B    � ��$ 1q�68   6B   5:      �� 5;      $
 True��6=    6   �� *
IsSelected+.a�5=      �� $ PersonBorder��6D    � �� �#00F5FF�� 5>      $ PersonBorder��6B    � ��$ 1q�68   6B   5?      5A      �� 5B   
    � �� 5C      �� 5D      " ��6"   .R�5E      �� #��z��6   #��z��6J    . ._�5F      �� #��z��6+   #��z��6n   ��. # z 6�   #��z��6�   5G      5H      5J      5K      .��5N      �  5O   
   �� 5P      $��40 �6   �� 5Q      $��50 �6   �� 5R      $��* �6   .+�5T   
   �� $��0��6   $ 5,5,0,0��6   (  "GFramework.BlankWindow.BlankWindow   	Draggable . ���#292C356_   �� 5U   
   -   5         ��Name$ 	TitleName��5U   !   $��0��6    . $��8,0,0,0q�6]   $
��Left=�6o   $��Center=�6�   
�������$��14(�6�   ��6�   �� *PatrolmanTableTitle+�� 5V   
   -   5         ��Name$ CloseButton��5V      $��0��6     ButtonCloseStyle#���  6P   $��24��6{   $��24��6�    K� ZIndex$ 10��6�   $��Right=�6�   $��Center=�6�   $��0,0,8,0q�6�   ��63   �� *
CloseCmd+6�   �� 5W   
   -   5        $ 
foldButton��5W      $��0��6   $��0q�6O   $��24��6d   $��24��6p   $ 10��6{   $��Right=�6�   $��Center=�6�   $��0,0,40,0q�6�   ��62   �� *	FoldCmd+��5X      �� 5Y      -   5        	FoldImage�� j� Stretch$
 Fill=�5Y   0     
packdown_H �� ImageSource# [ 6?   5[      �� 5\   
   $��1��6   $��Stretch=�6      AddressTbxSearchStyle#���  6?   $	��265��5]      $��32��6"   +  %Mmc.Mspace.Theme.Helper.TextBoxHelper   IsNeedClearButton .  $��	16,8,16,8q�5_   V   $��2��6i      ButtonCommand 5^   E   �� *SearchCommand+��5_      �� *
SearchText+ �� UpdateSourceTrigger$ PropertyChanged=�6i   �� 5`   
   $��2��6   $��Visible=�6%   $��Stretch=�6K   $	��Top=�6i   .��5a      �� -   5        $��PatrolSV��5a      #��[ 6,   ��6Z   �� *PatrolmanSet+��5b      �� 5c      .R�5d      ��  �� Orientation$ Vertical=�6%   5e      5g      5h      5i      5j      