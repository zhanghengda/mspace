   M S B A M L   `   `   `  ����  Mmc.Mspace.UavModule4    .Mmc.Mspace.UavModule.UavTracing.UavTracingView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      J FMmc.Wpf.Toolkit, Version=1.0.0.1, Culture=neutral, PublicKeyToken=null&gvifmhttp://schemas.mmc.com/wpf  5      Q MGFramework.BlankWindow, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'blankWindowhttp://GFramework.org  5      $	��340��5         baseWindowStyle#���   6     � WindowStartupLocation$  Manual=�6C    � 
ResizeMode$ NoResize=�6c    � 	Resources 5	        ExpanderArrowHoverFill �� EndPoint �� 
StartPoint � Color � Offset  ExpanderArrowPressedFill  ExpanderArrowFill  VideoScreenTemplate  DefaultTextBlockStyle  ExpanderRightHeaderStyle �� 
TargetType �� Property	 �� Value
 �� 
TargetType �� Padding J� SnapsToDevicePixels � RowDefinitions � LayoutTransform �� Angle A�Name F�Name �� Triggers P� Property P� Value �� 
TargetName  ExpanderUpHeaderStyle � ColumnDefinitions  ExpanderLeftHeaderStyle 	 ExpanderHeaderFocusVisual �� StrokeDashArray 
 ExpanderDownHeaderStyle  up_N 
 down_H  ExpanderDownHeaderStyle2  Mmc_uav  
packdown_N �� Orientation  	Dronelist �� RelativeSource �� AncestorType(  "GFramework.BlankWindow.BlankWindow  packup_H  StretchExpanderStyle �� CornerRadius d�Name � FocusVisualStyle d� 	IsChecked �� Path  �� Mode! ��Name  StretchExpanderStyle2/  )Mmc.Wpf.Toolkit.Controls.IconToggleButton  UavTrackTemplate" 9� 
IsExpanded#  Name$   Icon%   MouseOverIcon&   CheckedIcon  RealtimeTrajectory' � ToolTip( � DataContext  Realtimevideo  DefaultComboBoxStyle   BooleanToVisibilityConverter) �� 	Converter* �� SelectedItem  Perspectivetofollow<  6Mmc.Wpf.Toolkit.Controls.Panels.UniformGridWithSpacing+ A� Rows, A� Columns- �� Mode  
FontSize15  DefaultButtonForeground 	 Model  Type  
Connection 	 Mount   Videostream. �� Triggers/ �� Value0 �� Binding ! ListBoxItemMouseOverStyle1 �� Triggers�� 5
   
   %,;  &	       &	 �     &	 �    &	 Z    7  &	 �    &	 Z    &	 �    &		 2    &	
 �    &	 �    7  7  &	 Q    7 	 7 
 7  7  7  &	 d#    7 	 7  7  7  7  '  x*    &	 �+    7  7  7  &	! s9    �� 5      $	 0,1&�6A   $	 0,0&�6P   .��5      � $ 	#FFF0F8FE��6   $ 0Q�61   � 5      $ 	#FFE0F3FE��6   $	 0.3Q�61   � 5      $ 	#FF6FA7C5��6   $ 1Q�61   5      �� 5      $	 0,1&�6C   $	 0,0&�6R   .��5      � $ 	#FFDCF0FA��6   $ 0Q�61   � 5      $ 	#FFC5E6F7��6   $	 0.2Q�61   � 5      $ 	#FF5690D0��6   $ 1Q�61   5      �� 5      $	 0,1&�6<   $	 0,0&�6K   .��5      � $ White��6   $ 0Q�6-   � 5      $ 	#FFBFBFBF��6   $	 0.5Q�61   � 5      $ 	#FF878787��6   $ 1Q�61   5      �� 5      .R�5      � .+�5      �� 
�������$
��Bold"�6P   8��  $��Center=�6�   ��6    �� *
ScreenItem+6�   5      5      �� 5!      " d�65   .�5"      ��  � ��	 5#      �� 5$      "
 d�6*   .R�5%      �� # z��6%   ._�5&   "   �  .  
������  5'   &   �� 5(   *   $��19 �68   �� 5)   *   $��* �68   .+�5+   &   �  5,   *   V� 5-   .   ~�5/   6   X� �� 50   :   $	 -90Q�6J   51   7   53   /   .+�55   *   A� $ circle��6�   #���  62   
�������$��Center=�6o   $��Center=�6�   $��19��6�   $��19��6�   F� 56   *   $ arrow��6n   ���#666$��2��6=   $��Center=�6Q   $��Center=�6}    .  "����@B `� � �D @KL � z `� 	6�   57   '   �� 58   &   $��Center=�67   $��0,4,0,0q�6T   $	��Top=�6e    . $��1��6�   ��. 6�   59   #   5:       5;      P� 5<   "    �@d�	IsChecked$
 trueY�6?   . �5=   &   �� $ arrow��6=    � ��"	 ��@B  �D � �D @B � z  �D 	6P   5>   #   P� 5?   "    �@J�IsMouseOver . . �5@   &   �� $ circle��6?    � ��	 �	#FF3C7FB16S   �� 5A   &   $ circle��6=   #	 �  6Q    � ���� 5B   &   $ arrow��6?    � ��	 �#2226R   5C   #   P� 5D   "    � �� . . �5E   &   �� $ circle��6?    � ��	 �	#FF526C7B6S   �� 5F   &   $ circle��6H    � ��$		 1.5��6\   �� 5G   &   $ circle��6=   #	 �  6Q    � ���� 5H   &   $ arrow��6?    � ��	 �	#FF0033666R   5I   #   5K      5M      5N      �� 5O      " d�62   .�5P      ��  � ��	 5Q      �� 5R      "
 d�6*   .R�5S      �� # z��6%   ._�5T   "   �  .  
������  5U   &   �� 5V   *   $��19 �6;   �� 5W   *   $��* �6;   .+�5Y   &   �  5Z   *   V� 5[   .   ~�5]   6   X� �� 5^   :   $	 180Q�6J   5_   7   5a   /   .+�5c   *   A� $ circle��6�   #���  62   
�������$��Center=�6o   $��Center=�6�   $��19��6�   $��19��6�   F� 5d   *   $ arrow��6n   ���#666$��2��6=   $��Center=�6Q   $��Center=�6}    .  "����@B `� � �D @KL � z `� 	6�   5e   '   �� 5f   &   $��Stretch=�67   $��4,0,19,0q�6U   $��Center=�6g    . $��1��6�   ��. 6�   5g   #   5h       5i      P� 5j   "    �@d�	IsChecked$
 trueY�6?   . �5k   &   �� $ arrow��6=    � ��"	 ��@B  �D � �D @B � z  �D 	6P   5l   #   P� 5m   "    �@J�IsMouseOver . . �5n   &   �� $ circle��6?    � ��	 �	#FF3C7FB16S   �� 5o   &   $ circle��6=   #	 �  6Q    � ���� 5p   &   $ arrow��6?    � ��	 �#2226R   5q   #   P� 5r   "    � �� . . �5s   &   �� $ circle��6?    � ��	 �	#FF526C7B6S   �� 5t   &   $ circle��6H    � ��$		 1.5��6\   �� 5u   &   $ circle��6=   #	 �  6Q    � ���� 5v   &   $ arrow��6?    � ��	 �	#FF0033666R   5w   #   5y      5{      5|      �� 5}      " d�64   .�5~      ��  � ��	 5      �� 5�      "
 d�6*   .R�5�      �� # z��6%   ._�5�   "   �  .  
������  5�   &   �� 5�   *   $��19 �68   �� 5�   *   $��* �68   .+�5�   &   �  5�   *   V� 5�   .   ~�5�   6   X� �� 5�   :   $ 90Q�6J   5�   7   5�   /   .+�5�   *   A� $ circle��6�   #���  62   
�������$��Center=�6o   $��Center=�6�   $��19��6�   $��19��6�   F� 5�   *   $ arrow��6n   ���#666$��2��6=   $��Center=�6Q   $��Center=�6}    .  "����@B `� � �D @KL � z `� 	6�   5�   '   �� 5�   &   $��Center=�67   $��0,4,0,0q�6T   $	��Top=�6e    . $��1��6�   ��. 6�   5�   #   5�       5�      P� 5�   "    �@d�	IsChecked$
 trueY�6?   . �5�   &   �� $ arrow��6=    � ��"	 ��@B  �D � �D @B � z  �D 	6P   5�   #   P� 5�   "    �@J�IsMouseOver . . �5�   &   �� $ circle��6?    � ��	 �	#FF3C7FB16S   �� 5�   &   $ circle��6=   #	 �  6Q    � ���� 5�   &   $ arrow��6?    � ��	 �#2226R   5�   #   P� 5�   "    � �� . . �5�   &   �� $ circle��6?    � ��	 �	#FF526C7B6S   �� 5�   &   $ circle��6H    � ��$		 1.5��6\   �� 5�   &   $ circle��6=   #	 �  6Q    � ���� 5�   &   $ arrow��6?    � ��	 �	#FF0033666R   5�   #   5�      5�      5�      �� 5�      .�5�      ��  � ��	 5�      �� 5�      .R�5�      �� ._�5�   "   �� 
���   �$	 1 2R�6;   $��1��6Q   $��0q�6e    . 6p   5�      5�      5�      5�      �� 5�      " d�66   .�5�      ��  � ��	 5�      �� 5�      "
 d�6*   .R�5�      �� # z��6%   ._�5�   "   �  .  
������  5�   &   �� 5�   *   $
��Auto �6;   �� 5�   *   $��* �6;   .+�5�   &   �� $
��img1��6   $��0��6,   $��0��69   $
��Left=�6I   $��Center=�6d   $��10,0,0,0q�6�   $	��7.2��6�   $	��4.6��6�   #���  6�   $��Uniform=�6�   6�   �� 5�   #   $��Stretch=�64   $��4,0,19,0q�6R   $��Center=�6d    . $��1��6�   ��. 6�   5�   #   5�       5�      P� 5�   "    �@d�	IsChecked$
 trueY�6?   . �5�   &   �� $
 img1��6?   #	 �  6Q    � ��5�   #   P� 5�   "    �@J�IsMouseOver . 5�   #   P� 5�   "    � �� . 5�   #   5�      5�      5�      �� 5�      " d�67   .�5�      ��  � ��	 5�      �� 5�      "
 d�6*   .R�5�      �� # z��6%   ._�5�   "   �  .  ���#1E2026 5�   &   �� 5�   *   $
��Auto �6;   �� 5�   *   $��* �6;   �� 5�   *   $
��Auto �6;    5�   &   �� 5�   *   $��36 �68   �� 5�   *   .+�5�   &   �� $
��img1��6   $��0��6,   $��0��69   $
��Left=�6I   $��Center=�6d   $��10,0,0,0q�6�   $��24��6�   $��24��6�   #���  6�   $��Uniform=�6�   6�   �� 5�   &   $
��img2��6   $��0��6,   $��2��69   $
��Left=�6I   $��Center=�6d   $��0,0,0,0q�6�   $��24��6�   $��24��6�   #���  6�   $��Uniform=�6�   6�   �� 5�   &   $��0��61   $��1��6>   $ 
Horizontal=�6O   $��Center=�6h   .+�5�   *   �� #���  64   8��  $��Center=�6�   �� 5�   *   8�� $��0,0,0,0q�6h   $��Center=�6y   �� 5�   *   $�(��6/    �� 5�   .   �64   �� *DataContext.socketState+ �� "   �� 5�   .   $	�）��63   5�   +   5�   '   �� 5�   &   $��1��67   $��3��6D   $��Stretch=�6X   $��0q�6v   $��Center=�6�    . $��1��6�   ��. 6�   5�   #   5�       5�      P� 5�   "    �@d�	IsChecked$
 trueY�6?   . �5�   &   �� $
 img2��6?   #	 �  6Q    � ��5�   #   P� 5�   "    �@J�IsMouseOver . 5�   #   P� 5�   "    � �� . 5�   #   5�      5�      5�      �� 5�      " 9�61   .�5      ��  � ��#	 � ��6/   �� 5      � ��
	 ���� 6/   �� 5      � ��$	 Stretch=�6?   �� 5      � ��$	 Stretch=�6=   �� 5      � ��
	 ���� 60   �� 5      � ��$	 1q�64   �� 5      � ��	 5     �� 5     "
 9�6*   .R�5	     ��  . #��z��6@   #��z��6j   #��z��6�   $ 3��6�   ._�5
  "   ]� .+�5  &   d� $ 
HeaderSite��6�   8   $��1q�6q   $��0��6|   $��0��6�   8�� #��z��6�   #��z��6�   #��z��62  #��z��6u  #��z��6�  #��z��6�  #��z��6�  #��z��6  #��z��6C  #��z��6m  #��z��6�  #��z��6�  $	��Top=�6!   66  �� $ 
IsExpanded�$  TwoWay=� �� *TemplatedParent+�� 5  &   $! 
ExpandSite��6�   #��z��67   #��z��6z   #��z��6�   |�.  $y�	Collapsed=�6  $��Bottom=�6  5  #   5      5     P� 5  "    �@9�
IsExpanded . . �5  &   �� $ 
ExpandSite��6C    � y�$	 Visible=�6[   5  #   P� 5  "    �@9�ExpandDirection$ Right=�6E   . �5  &   �� $ 
ExpandSite��6G    � ��$	 Right=�6_   �� 5  &   $ 
HeaderSite��6G    � ��$
	 Left=�6_   �� 5  &   $ 
HeaderSite��6>   8	   � ��6V   5  #   P� 5  "    �@9�ExpandDirection$ Up=�6E   . �5  &   �� $ 
ExpandSite��6G    � ��$		 Top=�6_   �� 5  &   $ 
HeaderSite��6G    � ��$	 Bottom=�6_   �� 5  &   $ 
HeaderSite��6>   8	   � ��6V   5  #   P� 5  "    �@9�ExpandDirection$
 Left=�6E   . �5  &   �� $ 
ExpandSite��6G    � ��$
	 Left=�6_   �� 5  &   $ 
HeaderSite��6G    � ��$	 Right=�6_   �� 5   &   $ 
HeaderSite��6>   8	   � ��6V   5!  #   P� 5"  "    � {� .  . �5#  &   �� #	 � ��6C    � ��5$  #   5&     5(     5)     �� 5*     " 9�62   .�5+     ��  � ��#	 � ��6/   �� 5,      � ��
	 ���� 6/   �� 5-      � ��$	 Stretch=�6?   �� 5.      � ��$	 Stretch=�6=   �� 5/      � ��
	 ���� 60   �� 50      � ��$	 1q�64   �� 51      � ��	 52     �� 53     "
 9�6*   .R�54  "   ��  . #��z��6D   #��z��6n   #��z��6�   $ 3��6�   ._�55  &   ]� .+�56  *   d� $ 
HeaderSite��6�   8   $��1q�6u   $��36��6�   $��0��6�   8�� #��z��6�   #��z��6  #��z��68  #��z��6{  #��z��6�  #��z��6�  #��z��6�  #��z��6  #��z��6I  #��z��6s  #��z��6�  #��z��6�  $	��Top=�6'   6<  �� $ 
IsExpanded�$  TwoWay=� �� *TemplatedParent+�� 57  *   $! 
ExpandSite��6�   #��z��6;   #��z��6~   #��z��6�   |�.  $y�	Collapsed=�6  $��Bottom=�6  58  '   59  #    5:     P� 5;  "    �@9�
IsExpanded . . �5<  &   �� $ 
ExpandSite��6C    � y�$	 Visible=�6[   5=  #   P� 5>  "    �@9�ExpandDirection$ Right=�6E   . �5?  &   �� $ 
ExpandSite��6G    � ��$	 Right=�6_   �� 5@  &   $ 
HeaderSite��6G    � ��$
	 Left=�6_   �� 5A  &   $ 
HeaderSite��6>   8	   � ��6V   5B  #   P� 5C  "    �@9�ExpandDirection$ Up=�6E   . �5D  &   �� $ 
ExpandSite��6G    � ��$		 Top=�6_   �� 5E  &   $ 
HeaderSite��6G    � ��$	 Bottom=�6_   �� 5F  &   $ 
HeaderSite��6>   8	   � ��6V   5G  #   P� 5H  "    �@9�ExpandDirection$
 Left=�6E   . �5I  &   �� $ 
ExpandSite��6G    � ��$
	 Left=�6_   �� 5J  &   $ 
HeaderSite��6G    � ��$	 Right=�6_   �� 5K  &   $ 
HeaderSite��6>   8	   � ��6V   5L  #   P� 5M  "    � {� .  . �5N  &   �� #	 � ��6C    � ��5O  #   5Q     5S     5T     �� 5V     "  6   .�5W     ��  � ��	 6,   �� *	Content+�� 5X      �@ Icon	 6@   �� *Icon+�� 5Y      �@ MouseOverIcon	 6I   �� *MouseOverIcon+�� 5Z      �@ CheckedIcon	 6G   �� *CheckedIcon+�� 5[      �@�ToolTip	 6=   �� *	ToolTip+�� 5\      �@d�	IsChecked	 6E   �� *	IsChecked+5]     �� 5^     .R�5_     9� #���  6>   $	��375��6m   $��Stretch=�6z   $��Stretch=�6�   " 6   �� *
IsExpanded+��5`     � 5a     $��Stretch=�6!   $��Stretch=�6?   .+�5i     �� $��10,0,0,0q�6B   $
��Bold"�6T   8��  $
��Left=�6�   ��6(   �� *
UavTitle+6�   ]� 5j     $��Right=�6*   .+�5k  "     $# tractBtn��6:   $��24��6M   $��24��6X   $��5,0,0,0q�6d   $
��Left=�6u   $9$ 3pack://siteoforigin:,,,/Resources/Uav/轨迹@3x.png��5m  :   $:% 4pack://siteoforigin:,,,/Resources/Uav/轨迹H@3x.png��5n  :   $:& 4pack://siteoforigin:,,,/Resources/Uav/轨迹C@3x.png��5o  :   #' �  5p  :   ( 5l  :   �� *UavTracingModel+5p  l     5q  "   $# 	videotBtn��6:   $��24��6N   $��24��6Y   $��5,0,0,0q�6f   $��Center=�6w   $9$ 3pack://siteoforigin:,,,/Resources/Uav/录像@3x.png��5s  :   $:% 4pack://siteoforigin:,,,/Resources/Uav/录像H@3x.png��5t  :   $:& 4pack://siteoforigin:,,,/Resources/Uav/录像H@3x.png��5u  :   #' �  5v  :   ( 5r  :   �� *VideoControllerVM+5v  g   �� 5w  "   #���  6,   $��5,0,0,0q�6�   $	��100��5x  ,   8�� $��12(�6�   y�5w  [   �� $ IsMultiScreen�8)  ��5x  l   �� *VideoScreenModels+* 5y  ,   �� *SelectedScreenModel+{�6Y   �� $ IsComboxEnabled�  5�  "   $# 	followBtn��6:   $��24��6N   $��24��6Y   $��5,0,0,0q�6f   $��Right=�6w   $9$ 3pack://siteoforigin:,,,/Resources/Uav/定位@3x.png��5�  :   $2% ,pack://lisi:,,,/Resources/Uav/定位H@3x.png��5�  :   $:& 4pack://siteoforigin:,,,/Resources/Uav/定位H@3x.png��5�  :   #' �  5�  :    5�  :   �� *UavTracingModel.IsFollow+5�  m   5�     5�     ��5�     ]� 5�     .+�5�     �� $
��cmds��6+   $
��20,3q�67   $��Right=�6E   $��Bottom=�6a   $y�	Collapsed=�6y   ��5�  "   �� 5�  &   .R�5�  *     $��Center=�6G   $+ 1��6u   , 5�  
   �� *Items.Count+ �� $ ItemsControlL�$- FindAncestor=�5�  '   5�     � 5�     $��
25,10,5,10q�6$    5�  "   �� 5�  &   �� 5�  *   '��       " ��60   '��       .�5�  .   ��  � ��$	 Microsoft YaHei)�6K   �� 5�  .    � ��#	 �  6I   �� 5�  .    � ��#	 �  6K   �� 5�  .    � ��$		 0,5q�6G   �� 5�  .    � ��$	 Center=�6R   �� 5�  .    � ��$
	 Left=�6T   5�  +   5�  '    5�  "   �� 5�  &   $
��auto �64   �� 5�  &   $
��auto �64   �� 5�  &   $
��auto �64   �� 5�  &   $
��auto �64   �� 5�  &   $
��auto �64   �� 5�  &   $
��auto �64    5�  "   �� 5�  &   $
��Auto �69   �� 5�  &   .+�5�  "   �� $��1��6,   #���  69   �� 5�  "   $��1��6,   $��1��69   ��6I   �� *ground_serial+�� 5�  "   $��2��6,   #���  69   �� 5�  "   $��2��6,   $��1��69   ��6I   �� *
uav_type+�� 5�  "   $��3��6,   #���  69   $	��Top=�6]   �� 5�  "   $��3��6,   $��1��69   ��6J   �� *GroundState+�� 5�  "   $��4��6,   #���  69   $	��Top=�6X   �� 5�  "   $��4��6,   $��1��69   ��6J   �� *
mount_type+�� 5�  "   $��5��6,   #���   69   $y�	Collapsed=�6^   �� 5�  "   $��6��6,   $��1��69   $y�	Collapsed=�6f   ��6I   �� *CameraState+6f   5�     5�     5�     . 5�     �� 5�     $
/ true��6"   0 6/   �� *
IsSelected+ �� $- FindAncestor=�$ ListBoxItemL�.a�5�     �� $
 cmds��6!    � y�$	 	Collapsed=�6I   5�     5�     �� 5�     " ��66   1 5�     P� 5�      �@J�IsMouseOver . . �5�     ��  � ��
	 ���� 67   5�     P� 5�      �@��
IsSelected . . �5�     ��  � ��
	 ���� 67   5�     5�     5�     .��5�     �  5�  
   �� 5�     $��40 �6   �� 5�     .+�5�  
   �� $��0��6   ���#292C35$��0��63   $��2��6C   $ 5,5,0,0��6W   2   	Draggable2 . 6n   �� 5�  
   -   5        3 ��Name$3 	TitleName��5�     2 . $��0��6Q   $��0��6^   $��2��6n   $��18,0,0,0q�6�   
�������$��18(�6�   $
��Left=�6�   $��Center=�6�   ��6�   �� *WindowTitle+6�   �� 5�  
   -   5        4 ��Name$4 CloseButton��5�     $��0��6   $��0��63   $��2��6C    " ButtonCloseStyle#��� " 6u   $��36��6�   $��36��6�   $��Right=�6�   $	��Top=�6�   $��0,2,8,0q�6�   ��6W   �� *
CloseCmd+6�   �� 5�  
   $��1��6   .+�5�     9� " . #���  6*   ��5�     � 5�     $��0,0,0,0q�6    5�     �� 5�     �� 5�     .+�5�     � $��1��6!   $
��Left=�6.    5�     �� 5�  "   $
��Auto �63   �� 5�  "   $
��Auto �63   �� 5�  "   $��* �63   .+�5�     �� -   5        $4 connectSocketBtn��5�  &   $��28��6@    # DefaultButtonStyle#��� # 6L   $��0��6y   $��0��6�   $��0,0,10,0q�6�    $ ConnectionContent#��� $ 6�   ��6�   �� *connectSocketCMD+6�   �� 5�     -   5        $4 loginSocketBtn��5�  &   $��28��6>   #��� # 6K   $��0��6x   $��1��6�   $��0,0,10,0q�6�    	% Login#��� % 6�   ��6�   �� *loginSocketCMD+6�   5�     5�     ��5�     �� 5�     -   5        $��ListBox��5�     $
��Auto=�6-   $��Stretch=�6]   #��[! 5�     $	��350��6^   ��5�  {   �� *UavItemAreaModels+��5�     �� 5�     .R�5�  "   � .+�5�  &   9� $��Expander��6/   #���  6?   ��5�  *   �� 5�  .   $ 
Horizontal=�69   $��Center=�6R   $��Stretch=�6m   .+�5�  2   �� $��10,0,0,0q�6V   $
��Bold"�6h   $��18(�6z   #��[ 6�   $
��Left=�6�   ��6<   �� *
AreaName+6�   �� 5�  2   $
��10,0q�6<   #��[ 6J   $
��Left=�6y   $��16(�6�   �� 5�  6   �6:   �� *OnLine+5 �� StringFormat$5 
({0}在线�� �� 5   6   �6:   �� *	OffLine+$5 
{0}离线)��5  3   5  /   ��5  *   �� 5  .   -   5        #��[ 5  6   $
��Auto=�6g   $��Stretch=�6�   ��5  6   �� *UavItemModels+* 6\   �� *" DataContext.SelectedUavItemModel+ �� "  5  /   5
  '   5  #   5     5     5     5     5     5     