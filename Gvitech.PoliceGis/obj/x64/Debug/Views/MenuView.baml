   M S B A M L   `   `   `  ����A  =Mspace, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:MMC.MSpace.ViewsMMC.MSpace.Views  5
          MMC.MSpace.Views.MenuView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      J FMmc.Mspace.Main, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null2
dtselector!http://schemas.mmc.com/dtselector  5      N JMmc.Mspace.Services, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null gvihttp://schemas.mmc.com  5      J FMmc.Wpf.Toolkit, Version=1.0.0.1, Culture=neutral, PublicKeyToken=null&gvifmhttp://schemas.mmc.com/wpf  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5	      *viewsclr-namespace:MMC.MSpace.Views  5
      ���	#FF180053  � 	Resources  5      8  2Mmc.Mspace.Main.DTSelector.BarDataTemplateSelector   BarDataTemplateSelector\?clr-namespace:Mmc.Wpf.Toolkit.Controls;assembly=Mmc.Wpf.ToolkitMmc.Wpf.Toolkit.Controls 5      )  #Mmc.Wpf.Toolkit.Controls.IconButton �� 
TargetType �� Property �� Value �� 
TargetType � RowDefinitions �� ElementName �� Triggers P� Property	 P� Value
 �� 
TargetName �� RelativeSource��5      %x  &	        '         5   
   6.   �� 5   
   Ncontrols?clr-namespace:Mmc.Wpf.Toolkit.Controls;assembly=Mmc.Wpf.Toolkit  6   "  5      .�5      ��  �@�FocusVisualStyle 6B   Z� �� 5       �@ Icon 69   �� *Icon+�� 5       � �� 6+   �� *
Background+�� 5       �@ MouseOverIcon 6B   �� *MouseOverIcon+�� 5       �@J�SnapsToDevicePixels . 6>   �� 5       � �� 6(   �� *	Command+�� 5       � �� 5      �� 5      "  5      .R�5      � .+�5      �� #��z��5   
   #��z��5   
   #��z��5   
   ._�5    "   �  5!   &   �� 5"   *   �� 5#   *   $
��0.2* �68   .+�5%   &   �� $	��bd1��6-   $
��0,50q�68   ._�5&   *   �� $	��img��5'      ��5(      �� *ActualWidth+$	 bd1����5)      �� *ActualHeight+$	 bd1����5*      �� *Icon+5+   '   �� 5,   &   $	��bd2��6-   $��1��68   $��0,0,0,10q�6E   ._�5-   *   �� $��Uniform=�5.      ��5/      �� *ActualWidth+$	 bd2����50      �� *ActualHeight+$	 bd2����51      �� *
IconText+52   '   53   #   54      �� 55      $��bd��6%   56       57      P� 58       �@J�IsMouseOver	 . . �59   "   �� $
 bd��6)    � ��$ 2q�6T   �� 5:   "   $
 bd��6)    � �� �	#FFFF45006P   �� 5;   "   $	
 img��6)    6L   �� *MouseOverIcon+ �� *TemplatedParent+ � ��5<      P� 5=       �@J�IsKeyboardFocused	 . . �5>   "   �� $
 bd��6)    � ��$ 2q�6T   �� 5?   "   $
 bd��6)    � �� �	#FFFF45006P   5@      5B      5D      5E      5F      .��5G      ]� ��6   0  *Mmc.Wpf.Toolkit.MarkupExtensions.SetMargin  *��   
MenuMargin �� Source# [ +.+�5H   
   <  6Mmc.Wpf.Toolkit.Controls.Panels.UniformGridWithSpacing  $	��Top=�5I       A� Columns$ 1��5J      $��Right=�5K       A� Rows$ 3��5L         SpaceBetweenColumns$ 2Q�5M        5N      -  'Mmc.Wpf.Toolkit.Controls.IconDataButton �� BasedOn�� 5O      %�   '        7 �� 5P      8   "  5R      .�5T      ��  �@ IconDataButtonType$
 Rect=�6T   �� 5U       � ��$ 36��62   �� 5V       � ��$ 36��63   5W      5X      .+�5Z          FullScreenGeoData   IconGeoData# �  5[      ��5\      �� *ShellModel.FullScreenCmd+  5]        
MinGeoData# �  5^      ��5_      �� *ShellModel.MinimizedCmd+  5`        CloseGeoData# �  5a      ��5b      �� *ShellModel.CloseCmd+5c      �� 5d   
   #���   6   ��6X   �� *ShellModel.BarMenuItems+  5e      ��%�   '��       �� 5f      " ��6   .�5g      ��  � �� 64   �� *ColumnIndex+�� 5h       � �� 61   �� *
RowIndex+�� 5i       � �� 68   �� *
ColumnSpan+�� 5j       � �� 65   �� *	RowSpan+5k      5l      ��5m      �� 5n      .R�5o      0  *Mmc.Wpf.Toolkit.Controls.Panels.ExtendGrid     ColumnCount 6'   �� *ShellModel.ColumnCount+   RowCount 6V   �� *ShellModel.RowCount+5p      5r      5t      5u      