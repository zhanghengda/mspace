   M S B A M L   `   `   `  ����A  =Mspace, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:MMC.MSpace.ViewsMMC.MSpace.Views  5      %    MMC.MSpace.Views.MessageBoxView   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:MMC.MSpace.Views     5      $	��240��5   7   $	��480��6D     � WindowStyle$
  None=�6Q   
������  � ShowInTaskbar .   � AllowsTransparency .  � WindowStartupLocation$ CenterScreen=�5	   (    � WindowState$ Normal=�6M    � 	Resources 5
         ShowMessage1 �� 
TargetName �� TargetProperty W� From		 W� To
 g� Duration g� 	BeginTime g� AutoReverse��%p  &	        �� 5   
   .��5      W� $	 Br1��6   $( "RenderTransform.Children[0].ScaleX�5      $ 1X�5      $		 1.1X�6#   $
 0:0:0.05D�6,   $ 0:0:0.1X�6@    . 5      W� 5      $	 Br1��6   $( "RenderTransform.Children[0].ScaleY�5      $ 1X�5      $		 1.1X�6#   $
 0:0:0.05D�6,   $ 0:0:0.1X�6@    . 5      5      5       � Triggers 5      :� 5   
    :� 
SourceName$	 Br1��6    :� RoutedEvent$ Window.Loaded��6(   .[�5      �� #��[  6   5      .��5      � -   5        $	��Br1��5       K� ZIndex$ 10��6   ���#1E2026z�5   
   V� 5      .~�5      ��  �� CenterX$	 100Q�6!    �� CenterY$ 50Q�6/   6>   5      .+�5!   
   �� ._�5"      � ���#1E2026 � RowDefinitions 5#      �� 5$      $��36 �6$   �� 5%      $��3* �6$   �� 5&      $��2* �6$   .+�5(      �� ���#292C356   �� 5)      -   5        $��Right=�5)      $��36��6M   $��36��6Y     ButtonCloseStyle#��[ 6d   5+      �� 5,      -   5         ��Name$ tbTitle��5,      $��0��6-   $�� ��6:   $��16(�6B     DefaultForeground#��[ 6P   $��20,0,0,0q�6�     DefaultTextBlockStyle#��[ 6�   �� 5.      -   5        $ 	tbMessage��5.      $��1��6/   $�� ��6<   $��18(�6D   $
��Wrap=�6R   #��[ 6f   $��Center=�6�   #��[ 6�   $
��20,0q�6�   �� 5/      $��2��6    �� Orientation$ 
Horizontal=�6*   $��Bottom=�6C   $��Center=�6^   $��	0,10,0,30q�6{   .+�50      �� -   5         ��Name$ 
btnConfirm��50        OK#���  61   $
��20,2q�6P     DefaultButtonStyle#��[ 6^    � Cursor$
 Hand��6�   �� 51      -   5        $ 	btnCancel��51       
 Cancel#���  60   $
��20,2q�6S   #��[ 6a   $
 Hand��6�   52      53      54      55      56      