   M S B A M L   `   `   `  ����S  OMmc.Mspace.RoutePlanning, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:Mmc.Mspace.RoutePlanningMmc.Mspace.RoutePlanning  5   	   K GMmc.Mspace.Theme, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null[?clr-namespace:Mmc.Mspace.Theme.Helper;assembly=Mmc.Mspace.ThemeMmc.Mspace.Theme.Helper 5   	   3    -Mmc.Mspace.RoutePlanning.MappingCameraAddView   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:Mmc.Mspace.RoutePlanning     5   	   Rhelper?clr-namespace:Mmc.Mspace.Theme.Helper;assembly=Mmc.Mspace.Theme     5   	   Q MGFramework.BlankWindow, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'blankWindowhttp://GFramework.org  5   	   P LMmc.Windows.Framework, Version=1.0.0.1, Culture=neutral, PublicKeyToken=null'Customhttp://schemas.mmc.com/wpf  5	   	   -   5        $	��280��5
      $	��300��6'      baseWindowStyle#���   6J     � WindowStyle$
  None=�6u    � 
ResizeMode$ NoResize=�6�    � WindowStartupLocation$ CenterOwner=�6�   .��5      �  � RowDefinitions 5   
   �� 5      $��40 �6   �� 5      $
��auto �6   �� 5      $��50 �6   .+�5   
   �� $��0��6   ���#292C35$��0��63    �� CornerRadius$ 5,5,0,0��6D   (  "GFramework.BlankWindow.BlankWindow   	Draggable . 6[   �� 5   
   -   5         ��Name$ 	TitleName��5       . $��0��6Q   $��0��6^     Cameraconfiguration#���  6n   $��10,0,0,0q�6�   
�������$��18(�6�   $
��Left=�6�   $��Center=�6�   �� 5   
   -   5         ��Name$ CloseButton��5      $��0��6   $��0��63      	Closeable .   ButtonCloseStyle#���  6�   $��36��6�   $��36��6�   $��Right=�6�   $	��Top=�6�   $��0,2,8,0q�6�   ��6D   �� 6�   � 5   
   $��1��6   $��
10 10 0 10q�6    5      �� 5      $��40 �6    �� 5      $��40 �6    �� 5      $��40 �6!   �� 5      $��40 �6!   	 � ColumnDefinitions	 5      �� 5      $
��Auto �6#   �� 5      $��* �6#   .+�5      �� $��0��6   $��0��6%     
CameraName#���  65   $��Center=�6Y     DefaultTextBlockStyle#���  6t   �� 5       $��0��6   $��1��6#   $��Stretch=�63   +  %Mmc.Mspace.Theme.Helper.TextBoxHelper
   IsNeedClearButton
 .  $
��Auto��6�   $��36��6�   $��0,0,10,0q�6�     TbxWaterStyle#���  6�   ��6�   �� *Name+ �� UpdateSourceTrigger$ PropertyChanged=��� 5!      $��1��6   $��0��6%   $��Center=�65     Focallength#���  6Q   #���  6v   �� 5"      $��1��6   $��1��6#   $
��Auto��63   $��36��6@   $��0,0,10,0q�6L   $��Stretch=�6^   
 .  #���  6�   ��6�   �� *Focus+$ PropertyChanged=��� 5#      $��2��6   $��0��6%   $��Center=�65     Picturelong#���  6Q   #���  6v   �� 5$      $��2��6   $��1��6#   
 .  $��0,0,10,0q�6b   $��Stretch=�6t   $
��Auto��6�   $��36��6�   #���  6�   ��6�   �� *Width+$ PropertyChanged=��� 5%      $��3��6   $��0��6%   $��Center=�65     Picturewidth#���  6Q   #���  6w   �� 5&      $��3��6   $��1��6#   
 .  $��0,0,10,0q�6b   $��Stretch=�6t   $
��Auto��6�   $��36��6�   #���  6�   ��6�   �� *Height+$ PropertyChanged=�5'      �� 5(   
   $��2��6    	 Save#��� 	 6   $	��100��6@   $��36��6L    
 DefaultButtonStyle#��� 
 6�   ��6X   �� *cmdSaveMappingCamera+6�   5)      5*      