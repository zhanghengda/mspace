   M S B A M L   `   `   `  ����K  GMmc.Mspace.Theme, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Mmc.Mspace.Theme.PopMmc.Mspace.Theme.Pop  5   	   #    Mmc.Mspace.Theme.Pop.MmcMsBox   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:Mmc.Mspace.Theme.Pop     5   	     � Title$  MmcMsBox��5   	   $	��200��6   $��70��6)   $��98��68   $	��332��6D    � WindowStyle$
 None=�6P   
������  � AllowsTransparency .  � WindowStartupLocation$ CenterScreen=�6�    � WindowState$ Normal=�6�    � ShowInTaskbar .   � 	Resources 5	         ShowMessage1 �� 
TargetName �� TargetProperty	 W� From	
 W� To g� Duration g� 	BeginTime g� AutoReverse��%p  &	        �� 5
   
   .��5      W� $	 Br1��6   $( "RenderTransform.Children[0].ScaleX�5      $	 1X�5      $	
 1.1X�6#   $ 0:0:0.05D�6,   $ 0:0:0.1X�6@    . 5      W� 5      $	 Br1��6   $( "RenderTransform.Children[0].ScaleY�5      $	 1X�5      $	
 1.1X�6#   $ 0:0:0.05D�6,   $ 0:0:0.1X�6@    . 5      5      5       � Triggers 5      :� 5   
    :� 
SourceName$	 Br1��6    :� RoutedEvent$ Window.Loaded��6(   .[�5      �� #��[  6   5      .��5      � -   5        $	��Br1��5       K� ZIndex$ 10��6   ���#1E2026z�5   
   V� 5      .~�5      ��  �� CenterX$	 100Q�6!    �� CenterY$ 50Q�6/   6>   5      .+�5    
   �� $��5q�6   ���#292C35._�5!      � ���#1E2026$��8q�6(   .+�5"      �� -   5         ��Name$ 	tbMessage��5"      ���#B3BBC5$
��Wrap=�6D     tblMessageContent#��[ 6Y   5#      5$      5%      5&      