   M S B A M L   `   `   `  ����  FireControlModule*    $FireControlModule.VideoMonitorExView   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   J FMmc.Wpf.Toolkit, Version=1.0.0.1, Culture=neutral, PublicKeyToken=null&gvifmhttp://schemas.mmc.com/wpf  5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � AllowsTransparency  . ���	#00FFFFFF � ShowInTaskbar .   � SizeToContent$ WidthAndHeight=�5       � WindowStyle$
 None=�6!   .��5      �� -   5           P75BlackBrush#���   5      ��5   
   �� 5	        BorderPanelStyle �� BasedOn# [ 6    �� 
TargetType" ��6@    �� Triggers 5
      �� 5       �� Binding 6/   �� *
IsActive+ �� RelativeSource �� 	 �� Mode$	 FindAncestor=�
 �� AncestorType$
 WindowL� �� Value true6"   .a�5      ��  �� Property � ��  HighlightBackBrush �� Value# �  68   5      5      ._�5   
   �� $��Stretch=�6   ��5      ]� 5      .+�5      �� $��Right=�6     CloseWndButtonStyle#��[ 64   ��6a   �� *
CloseCmd+�� 5      $��Stretch=�6      ViewHeaderStyle#���  6>   ��6h   �� *	Content+5      .��5      � .+�5      �� $	��400��6   .+�5      ]� .+�5      ]� .+�5      �� $��0,0,5,0q�6(     HRVC@TextBlockStyle#���  6:   $��
关键字:��6h   $
��Left=�6t   $��50��6�   ,  &Mmc.Wpf.Toolkit.Controls.SearchTextBox  5         
IsShowIcon .    TextBoxStyle#���  6Y   $��Stretch=�5   Z   $	��300��6   ��66   �� *	QueryKeys+6   5       �� 5!      $��6,0,0,0q�6!   $��查询��62     ButtonStyle#���  6@   $��Right=�6�   $��50��6�   ��6f   �� *
QueryCmd+6�   5"      �� 5#      $��Bottom=�6    #���  68   y�5$   	   �� *
HasItems+   BooleanToVisibilityConverter �� 	Converter# [  �� ElementName$ resultDG��共查询到 �� 6~   ���	#FFFF0000�5&      �� *Items.Count+$ resultDG�� �� Mode$ OneWay=�  条记录6O   &   System.Windows.Controls.DataGrid  5'      -   5        $��resultDG��5'      $	��400��6/   $	��400��6@   $	��0,2q�6O   ��5(      �� *ResultsSource+ �� SelectedItem 6E   �� *SelectedPOIItem+y�5)      �� *
HasItems+# [  �� *Self+5*      5+      5-      5.      5/      50      