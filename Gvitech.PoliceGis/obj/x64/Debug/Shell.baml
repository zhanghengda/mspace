   M S B A M L   `   `   `  ����A  =Mspace, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:MMC.MSpace.ViewsMMC.MSpace.Views  5      K GMmc.Mspace.Theme, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_Aclr-namespace:Mmc.Mspace.Theme.Controls;assembly=Mmc.Mspace.ThemeMmc.Mspace.Theme.Controls 5      U<clr-namespace:Mmc.Mspace.Theme.Pop;assembly=Mmc.Mspace.ThemeMmc.Mspace.Theme.Pop 5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089I2clr-namespace:System.Collections;assembly=mscorlibSystem.Collections 5      _Aclr-namespace:Mmc.Mspace.Theme.Controls;assembly=Mmc.Mspace.ThemeMmc.Mspace.Theme.Controls 5          MMC.MSpace.Shell   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      J FMmc.Wpf.Toolkit, Version=1.0.0.1, Culture=neutral, PublicKeyToken=null&gvifmhttp://schemas.mmc.com/wpf  5      *viewsclr-namespace:MMC.MSpace.Views  5      OconrolsAclr-namespace:Mmc.Mspace.Theme.Controls;assembly=Mmc.Mspace.Theme  5      FPub<clr-namespace:Mmc.Mspace.Theme.Pop;assembly=Mmc.Mspace.Theme  5      j	 fGalaSoft.MvvmLight.Extras.WPF4, Version=4.3.31.28629, Culture=neutral, PublicKeyToken=1673db7d5906b0ad+mvvm http://www.galasoft.ch/mvvmlight 	 5	      c
 _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity 
 5
      =dic22clr-namespace:System.Collections;assembly=mscorlib 
 5      PcontrolsAclr-namespace:Mmc.Mspace.Theme.Controls;assembly=Mmc.Mspace.Theme 
 5      -   5          � AllowsTransparency  . ���	#0000172b � 
ResizeMode$ NoResize=�5       � ShowInTaskbar .   � WindowStartupLocation$ CenterOwner=�5       � WindowStyle$
 None=�5       � 	Resources 5         NoMouseOverButtonStyle �� 
TargetType �� Property �� Value	 �� 
TargetType
 ��Name J� SnapsToDevicePixels"  System.Collections.ArrayList  AL_MoreInbfo  ViewDataTemplate  IconBtnDataTemplate)  #Mmc.Wpf.Toolkit.Controls.IconButton  RigthIconStyle  BarIconBtnDataTemplate  IconRadioBtnDataTemplate.  (Mmc.Wpf.Toolkit.Controls.IconRadioButton�� 5   
   %�  &	        &	 �    &	 �    &	     &	 F    &	 {    �� 5      " ��63   .�5      ��  � ��$ 1q�64   �� 5       � ��# � ��6/   �� 5       � ��$ Center=�6?   �� 5       � ��$ Center=�6=   �� 5       � ��$ 1q�6,   �� 5       � �� 5      �� 5      "	 ��6*   .R�5       �� $
 Chrome��6%   #��z��65   #��z��6a    . ._�5!   "   �� #��z��63   #��z��6v   ��. # z 6�   #��z��6�   5"      5#      5%      5&        5'      5*      �� 5+      .R�5,      �� ��6!   �� *View+5-      �� 5.      .R�5/        #���  6$   50      �� 51      .R�52        $��2q�6#   53      �� 54      .R�55        56      57      .��5:      �  � RowDefinitions 5;   
   �� 5<      $
��Auto �6   �� 5=      �� 5>      $
��Auto �6    � ColumnDefinitions 5@   
   �� 5A      $
��Auto �6   �� 5B      �� 5C      $
��Auto �6   .+�5F   
   �� -   5        $��contentView��5F      $��0��6-   $��0��6=   $��3��6J   $��3��6[   �� 5G   
   -   5        $��popView��5G      $��0��6    $��0��60   $��3��6=   $��3��6N   #   MMC.MSpace.Views.HomeToolView  5I   
   -   5          Name$
 tool��5I      $��0��6,   $��0��6<   $��3��6I   $��3��6Z   �� 5K   
   -   5         ��Name$ leftViewPanel��5K      $��0��6,   
������ $��1��6V   $y�Visible=�6c   $��3��6y   $��1��6�    K� ZIndex$ 20��6�    �� Orientation$ 
Horizontal=�6�   .+�5L         MMC.MSpace.Views.LeftView  -   5          Name$ leftView��5L      ���#1E2026 J� Opacity$
 0.95Q�6D   $	��363��6S   $��Stretch=�6_   $��Stretch=�6}   $ 20��6�   5N      d� 5O      -   5         d�Name$ 
leftStatus��5O        LeftWindowStyle#��[ 6/   $
��Left=�6u   $��Center=�6�   6�   5P      �� 5Q   
   -   5        $
 	DropPanel��5Q      $��1��6$   
������  J� 	AllowDrop . $��1��6_   $��1��6l   $��1��6}   $ 23��6�   ._�5R      �� 
������ 6   . 
 (System.Windows.Interactivity.Interaction   Triggers 5S      / 
 )System.Windows.Interactivity.EventTrigger  5T         	EventName$
 Drop��6!   . 
 (System.Windows.Interactivity.TriggerBase   Actions. 5U      /	 	 )GalaSoft.MvvmLight.Command.EventToCommand	   	  PassEventArgsToCommand .  	  Command 6*   �� *DragDomToOpenCmd+6O   5V        5W      $ 	DragEnter��6!   . 5X      	   .  6*   �� *DragDomToOpenCmd+6O   5Y      5[      ]� 5\   
   $��2��6   $��0��6%   $��3��62   $ 22��6C   .+�5]      �� $	��120��6   $��Center=�6%   $
��Left=�6@   .+�5^      �� $��50��6   $��54��6$   62   �� 5_      $��50��6   $��54��6$   62   �� 5`      $��50��6   $��54��6$   62   �� 5a      $��50��6   $��54��6$   62   �� 5b      $��50��6   $��54��6$   62   �� 5c      $��50��6   $��54��6$   62   �� 5d      -	   5        $ 
devicetool��5d      $ 
Horizontal=�61   $y�Hidden=�6J   ���#000000$
��Left=�6t   $��Center=�6�   $��54��6�   $	��120��6�   .+�5e      '
  !Mmc.Mspace.Theme.Controls.Buttons
  -
   5         
  HasPathData . $��10 0 0 0q�5e   ;    
 Filter 
  RIcon# �  5f   %   $
��Left=�6F   $��Center=�6a    	 Filter_C 
  MouseOverRIcon# � 	 5g   %    
 Filter_S 
  PressedRIcon# � 
 5h   %     HomeTopButtonStyle#��[ 6h   6�   5i      �� 5j      $��50��6   $��54��6$   62   5l      $   MMC.MSpace.Views.RightToolView  5m      -   5          Name$ rightToolView��5m   "   5n      �� 5o   
   $ Vertical=�6   $��Stretch=�6,   $��0��6J   $��2��6Z   $��3��6h   $ 22��6|   .+�5p      �� $ 
Horizontal=�6   $��Center=�62   .+�5q      %   MMC.MSpace.Views.BootomToolView  -   5           Name$  
bottomTool��5q   '   5r      �� 5t      -   5        ! ��Name$! 
BottomMenu��5t      $��Stretch=�61   $��Bottom=�6O   5u      �� 5w   
   $��0��6   $��3��6%   $��3��69   $��Right=�6F   .+�5x      �� $��	0,10,30,5q�6   $��Right=�63   $��Bottom=�6O   ��6j   �� * ShellModel.ScaleViewModel.View+�� 5y      $ Vertical=�6   $	��560��60   $��0,0,0,3q�6<   $��Right=�6M   .+�5z      �� $ 
Horizontal=�6   .+�5{      %   MMC.MSpace.Views.CoordinateView  -   5        "  Name$" 
coordinate��5{   +   5|      �� 5}      -   5        $! 
Coordinate��5}   !   $��Stretch=�65   $��Bottom=�6S   5~      5      � 5�   
   $��0��6   $��0��6    $��3��6.   $��64��6?   $
��Left=�6J   $��Stretch=�6e   .+�5�      �� -   5        $! EditwaypointMenu��5�      $��64��67   5�      � 5�   
   -   5        # �Name$# _Menu��5�      $��48��6   $��0��6+   $��0��6;   $��3��6I   ���#1E2026 5�      �� 5�      $�� Auto �6#   �� 5�      $�� Auto �6#   �� 5�      .+�5�      �� -   5        $��imgdock��5�   )   $��0��6   $	��363��68   $ 
Horizontal=�6G   $
��Left=�6`   .+�5�      �� -   5        $
��img1��5�   3   $
��Left=�6   $��16,12q�6?   $��48��6N   $��48��6\     Mmc_Logo#��[ 6k   $��Uniform=�6�   $ �� StretchDirection$$ DownOnly=�6�   6�   �� 5�      -   5        % ��Name$% topTitle��5�      $
��Left=�6*   ���#ffffff$��16(�6a   $��Center=�6o     LogoText#���  6�   $��0,0,10,0q�6�   6�   5�      #   MMC.MSpace.Views.MenuToolView  5�      -   5        &  Name$& menutool��5�   !   $��1��63   6E   �� 5�      -   5        $ TopMenu��5�      $��Right=�6"   $��2��6>   $ 
Horizontal=�6O   
������ .+�5�      
   . $��48��68   $��40��6D    
 Notice# �  5�   %     Notice_H# �  5�   %     Notice_C# �  5�   %   #��[ 6�   ��6O   �� *CheckNoticeCommand+y�6v   �� *IsNoticeVisiable+6�   �� 5�      $��48��6   $	��140��6   $ Vertical=�6*   .+�5�      �� -   5        $	��140��5�      $��48��6-   $��0,0,10,0q�6:   ���#1E2026#��[  6�   .��5�      �� $ 
Horizontal=�6   .+�5�   	   �� -   5        ' ��Name$' useImg��5�      $��24��6   $��24��6+    
 user_D#���  66   �� 5�      -   5        ( ��Name$( userName��5�   (   $
��Wrap=�6:   $��60��6O   $��24��6^   ���#B3BBC5$��16(�6�   $��Center=�6�   ) �� Padding$) 0,0,0,0q�6�   $��	5,10,0,10q�6�   ��6k   �� *
UserName+6�   �� 5�      -   5        $' 	directImg��5�   $   $��24��67   $��24��6C    
 buttom#���  6N   5�   	   5�      5�      
  5�       . $��48��6,   $��40��68    	 set_D# �  5�   %    	 set_N# �  5�   %    	 set_M# �  5�   %   #��[ 6k   ��6L   �� *
SetCommand+6�   
  5�       . $��48��6,   $��40��68    	 min_D# �  5�   %    	 min_N# �  5�   %    	 min_M# �  5�   %   #��[ 6k   ��6L   �� *
MinCommand+6�   
  5�       . $��48��6,   $��40��68     close_D# �  5�   %     close_N# �  5�   %     close_M# �  5�   %   #��[ 6o   ��6N   �� *CloseCommand+6�   5�      5�      �� 5�      -   5        $��progressView��5�      $��0��6'   $��0��67   $��3��6D   $��3��6U   5�      5�      