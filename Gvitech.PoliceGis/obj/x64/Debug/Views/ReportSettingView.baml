   M S B A M L   `   `   `  ����A  =Mspace, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:MMC.MSpace.ViewsMMC.MSpace.Views  5      (    "MMC.MSpace.Views.ReportSettingView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:MMC.MSpace.Views     5      Q MGFramework.BlankWindow, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'blankWindowhttp://GFramework.org  5      $	��480��5      $	��280��6+     � WindowStartupLocation$  CenterScreen=�68    � 
ResizeMode$ NoResize=�6^    � AllowsTransparency . 
������ .��5
      �� ���#1E2026$��5q�6"   ._�5   
   �  � RowDefinitions 5      �� 5      $��36 �6    �� 5      .+�5      �� $��0��6   ���#292C35 �� CornerRadius$ 5��67   (  "GFramework.BlankWindow.BlankWindow   	Draggable . 6H   �� 5      -   5         ��Name$ 	TitleName��5       . $��0��6U   $��报告设置��6b   $��10,0,0,0q�6n   
�������$��16(�6�   $
��Left=�6�   $��Center=�6�   �� 5      -   5         ��Name$ btnTopClose��5   "   $��0��6      	Closeable .    ButtonCloseStyle#���   6`   $��Right=�6�   $	��Top=�6�   $��2q�6�   �� 5      $��0��6   
���   �$��0,0,0,1q�66   �� 5      	 �� Orientation$	 
Horizontal=�6   $��1��62   $��Center=�6?   .+�5      �� $��报告模板:��6   $��15(�6(   
�������$��30,0,0,0q�6I   $��Center=�6[   �� 5      -   5        
 ��Name$
 ComboBox��5        DefaultComboBoxStyle#��[ 6-   $	��5,2q�6[   $	��240��6h   $��30��6t   $��14(�6�   $
��Bold"�6�   �� 5      $
��10,2q�6   $��Center=�6*   �� 5      -   5        .�5   :   .�5      �� $�查看示例��6   5      5      5      5        J� Effect 5!   
   3  -System.Windows.Media.Effects.DropShadowEffect  5"         Color$ Black��6      	Direction$	 315Q�6-      ShadowDepth$ 2Q�6=      
BlurRadius$ 10Q�6M      Opacity$ 1Q�6]      RenderingBias$ Quality=�6i   5$      5%      