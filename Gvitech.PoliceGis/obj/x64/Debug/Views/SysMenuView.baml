   M S B A M L   `   `   `  ����A  =Mspace, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:MMC.MSpace.ViewsMMC.MSpace.Views  5      K GMmc.Mspace.Theme, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_Aclr-namespace:Mmc.Mspace.Theme.Controls;assembly=Mmc.Mspace.ThemeMmc.Mspace.Theme.Controls 5      U<clr-namespace:Mmc.Mspace.Theme.Pop;assembly=Mmc.Mspace.ThemeMmc.Mspace.Theme.Pop 5	      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089I2clr-namespace:System.Collections;assembly=mscorlibSystem.Collections 5
      "    MMC.MSpace.Views.SysMenuView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      J FMmc.Wpf.Toolkit, Version=1.0.0.1, Culture=neutral, PublicKeyToken=null&gvifmhttp://schemas.mmc.com/wpf  5      *viewsclr-namespace:MMC.MSpace.Views  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      OconrolsAclr-namespace:Mmc.Mspace.Theme.Controls;assembly=Mmc.Mspace.Theme  5      FPub<clr-namespace:Mmc.Mspace.Theme.Pop;assembly=Mmc.Mspace.Theme  5	      =dic22clr-namespace:System.Collections;assembly=mscorlib  5
      ���#1E2026$
��Left=�5      $	��Top=�5        � 	Resources  5      "  System.Collections.ArrayList   AL_MoreInfo/  )Mmc.Wpf.Toolkit.Controls.SimpleIconButton �� 
TargetType  UserButtonStyle �� Property �� Value �� 
TargetType ��Name �� Padding � ColumnDefinitions ��Name	 �� RelativeSource
 �� Mode �� Path ��Name �� Triggers P� Property P� Value)  #Mmc.Wpf.Toolkit.Controls.IconButton  UserButtonStyle2 F�Name �� Stretch  glyphicon-user  glyphicon-down �� Value �� Binding  glyphicon-up �� 
TargetName��%�  &	        &	      &	 �    7  7  7  7    5      5      �� 5   
   "  6   .�5      ��  � �� �#3A89FF6+   �� 5       � ��$ Stretch=�64   �� 5       � ��$
 Auto��6'   �� 5       � ��$
 Auto��6&   �� 5       � ��$
 14px(�6)   �� 5       � �� �#ffffff6+   �� 5       � �� 5      �� 5      "  6&   .R�5      �� $ PART_Background��6!   #��z��6:   # z��6f   #��z��6�   #��z��6�   ._�5      � $��Center=�6#   #��z��6>    5    "   �� 5!   &   $��* �67   �� 5"   &   $
��Auto �67   .+�5%   "   �� $ PART_Pcontent��6J   $��0��6,   $
��10,0q�6<   #��z��6b   $��Stretch=�6�   $��Center=�6�   ��6�   �� 	 �� $
 TemplatedParent=�$ Content�6  �� 5&   "   $ 
PART_PData��69   $��1��6)   $��Uniform=�6N   $��36��6`   $��Center=�6l   $��Right=�6�   ��6�   �� 	 �� *TemplatedParent+$
 Icon�5(      5)       5*      P� 5+       �@J�IsMouseOver . . �5,   "   ��  � �� �#2579F46?   5-      P� 5.       � �� . . �5/   "   ��  � �� �#2579F46?   50      52      54      �� 55       � ��$ 1q�6(   �� 56       � �� 6(   Z� �� 57       �@�Cursor$
 Hand��6'   58      �� 59   
   "  6   .�5:      ��  � �� �#3385FF6+   �� 5;       � ��$ Stretch=�64   �� 5<       � ��$
 Auto��6'   �� 5=       � ��$
 Auto��6&   �� 5>       � ��$
 16px(�6)   �� 5?       � �� �#ffffff6+   �� 5@       � �� �#4E95FF6,   �� 5A       � ��$ 1,0,0,0q�60   �� 5B       � �� 5C      �� 5D      "  6&   .R�5E      �� $ PART_Background��6!   #��z��6:   # z��6f   #��z��6�   #��z��6�   ._�5F      � $��Center=�6#   #��z��6>    5G   "   �� 5H   &   $
��Auto �67   �� 5I   &   $��* �67   �� 5J   &   $
��Auto �67   .+�5M   "   �� $ PART_Pcontent��6I   $��1��6,   $	��5,0q�6<   #��z��6a   $
��Left=�6�   $��Center=�6�   ��6�   �� 	 �� $
 TemplatedParent=�$ Content�6  F� 5N   "   $ 
PART_PData��69   $��0��6)   $ Uniform=�6N   ���#ffffff$��16��6o   $��Center=�6{   $
��Left=�6�   $
��10,0q�6�   8��  6�   F� 5O       $��path2��6%   $��2��62   $��10��6B   $��10��6N   8�� $ Uniform=�6�   ���#E2FFFF$��Center=�5P   F   $��Center=�6c   $��5q�6~   5Q      5R       5S      �� 5T      $
 true��6H    6*   �� *	IsChecked+.a�5U   "   �� 8  $ path2��6_    � ��5V      �� 5W      $ false��6H    6*   �� *	IsChecked+.a�5X   "   �� 8  $ path2��6a    � ��5Y      P� 5Z       �@J�IsMouseOver . . �5[   "   ��  � �� �#2579F46?   5\      P� 5]       � �� . . �5^   "   ��  � �� �#2579F46?   5_      5a      5c      �� 5d       � ��$ 1q�6(   �� 5e       � �� 6(   Z� �� 5f       �@�Cursor$
 Hand��6'   5g      5h      .��5i      � .+�5j   
   ]� .+�5k      ]� -   5        $��leftView��5k   .   $
��Left=�6   .+�5l      �� -   5        $��imgdock��5l   3   $
��Left=�6   $	��363��6B    �� Orientation$ 
Horizontal=�6Q   $
��Left=�6j   .+�5m      �� -   5        $
��img1��5m   7   $
��Left=�6   $��	20,0,10,0q�6C   $��46��6V   $��46��6a     Mmc_Logo#��[ 6m   $��Uniform=�6�   6�   �� 5n      $
��Left=�6    ���#ffffff$��16(�6P   $��Center=�6^     LogoText#���  6z   $��0,0,10,0q�6�   6�   5o      �� 5p      $ 
Horizontal=�6   .+�5q      #   MMC.MSpace.Views.MenuToolView  -   5          Name$ menutool��5q   )   6>   5r      5s      �� 5t      -   5         ��Name$ TopMenu��5t      $��Right=�6+   $��Right=�6G   $ 
Horizontal=�6^   .+�5u      $  Mmc.Mspace.Theme.Pop.MmcCombox  -   5          Name$
 Tool��5u        
 user_D   DataPath# �  6.   $	��140��6�   $��48��6�   $��0,0,10,0q�6�   ���#1E2026   DataContextSource# [  6�      SelectedMenuCommand 6R   �� $ SelMenuCommand�   UserName 6�   �� *
UserName+6�   '  !Mmc.Mspace.Theme.Controls.Buttons  5v         HasPathData . $��48��68   $��40��6D    		 set_N   RIcon# � 	 5w   %    	
 set_M   MouseOverRIcon# � 
 5x   %       PressedRIcon#  � 
 5y   %     HomeTopButtonStyle#��[ 6l   ��6L   �� *
SetCommand+6�     5z       . $��48��68   $��40��6D    	 min_N# �  5{   %    	 min_M# �  5|   %   #  �  5}   %   #��[ 6k   ��6L   �� *
MinCommand+6�     5~       . $��48��68   $��40��6D     close_N# �  5   %     close_M# �  5�   %   #  �  5�   %   #��[ 6o   ��6N   �� *CloseCommand+6�   5�      5�      5�      5�      