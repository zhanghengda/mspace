   M S B A M L   `   `   `  ����  Mmc.Mspace.Theme-    'Mmc.Mspace.Theme.Pop.MmcConfirmationBox   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	     � Title$  MmcConfirmationBox��5   	   $	��240��6$   $	��480��61    � WindowStyle$
 None=�6>   
������  � ShowInTaskbar .   � AllowsTransparency .  � WindowStartupLocation$ CenterScreen=�5	   #    � WindowState$ Normal=�6H    � 	Resources 5
         ShowMessage1 �� 
TargetName �� TargetProperty	 W� From	
 W� To g� Duration g� 	BeginTime g� AutoReverse��%p  &	        �� 5   
   .��5      W� $	 Br1��6   $( "RenderTransform.Children[0].ScaleX�5      $	 1X�5      $	
 1.1X�6#   $ 0:0:0.05D�6,   $ 0:0:0.1X�6@    . 5      W� 5      $	 Br1��6   $( "RenderTransform.Children[0].ScaleY�5      $	 1X�5      $	
 1.1X�6#   $ 0:0:0.05D�6,   $ 0:0:0.1X�6@    . 5      5      5       � Triggers 5      :� 5   
    :� 
SourceName$	 Br1��6    :� RoutedEvent$ Window.Loaded��6(   .[�5      �� #��[  6   5      .��5      � -   5        $	��Br1��5       K� ZIndex$ 10��6   ���#1E2026$��5q�67   z�5   
   V� 5      .~�5      ��  �� CenterX$	 100Q�6!    �� CenterY$ 50Q�6/   6>   5      .+�5!   
   �� 
���   �$��1q�6%   ._�5"      � ���#1E2026 � RowDefinitions 5#      �� 5$      $��36 �6$   �� 5%      $��3* �6$   �� 5&      $��2* �6$   .+�5(      �� ���#292C356   �� 5)      -   5        $��Right=�5)      $��36��6N   $��36��6Z     ButtonCloseStyle#��[ 6e   5+      �� 5,      -   5         ��Name$ tbTitle��5,      $��0��6-   $�� ��6:   $��16(�6B     DefaultForeground#��[ 6P   $��20,0,0,0q�6�     DefaultTextBlockStyle#��[ 6�   �� 5/      -   5        $ 	tbMessage��5/      $��1��6/   $�� ��6<   $��18(�6D   $
��Wrap=�6R   #��[ 6f   $��Center=�6�   #��[ 6�   $
��20,0q�6�   �� 50      $��2��6    �� Orientation$ 
Horizontal=�6*   $��Bottom=�6C   $��Center=�6^   $��	0,10,0,30q�6{   .+�51      �� -   5          OK#���  51      $
��20,2q�6<     DefaultButtonStyle#��[ 6J    � Cursor$
 Hand��6�   �� 52      -   5         ��Name$ 	BtnCancle��52   N    
 Cancel#���  6   $
��20,2q�6@   #��[ 6a   $
 Hand��6�   53      54      55       J� Effect 56   
   3  -System.Windows.Media.Effects.DropShadowEffect  57         Color$ Black��6      	Direction$	 315Q�6-      ShadowDepth$ 2Q�6=      
BlurRadius$ 10Q�6M      Opacity$ 1Q�6]      RenderingBias$ Quality=�6i   59      5:      