   M S B A M L   `   `   `  ����  Mmc.Mspace.IotModule+    %Mmc.Mspace.IotModule.Views.DeviceView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      Q MGFramework.BlankWindow, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'blankWindowhttp://GFramework.org  5      c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   
   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5        � Title$  
DevcieView��5   J   $	��450��5      $	��435��6    � WindowStyle$
 None=�5   0    � 
ResizeMode$ NoResize=�6C    � 	Resources 5         tagTemplate  LeftWindowBg �� CornerRadius �� Padding � ToolTip �� Orientation  CheckBoxStyle2 �� Path	 �� RelativeSource
 �� AncestorType d� 	IsChecked �� UpdateSourceTrigger��%  &	        7  �� 5   
   .R�5	      �� $��	TagBorder��6   ���	#FF71757E8��  $	��0.5q�6i   $ 2��6   $	 3,2q�6�   $	��3,2q�6�    6�   �� *
showname+._�5
      �� $ Vertical=�6   $	��120��64   .+�5      �� $��1��6   $��1��6,   $
��Left=�6<   $��16,4,0,0q�6W   $��Center=�6�   #���  6  ��6j   �� $  DataContext.CheckedCommand�	 �� "
 ���6�   �� *.+��6>  �� *
showname+ 6[  �� *
IsSelected+$ PropertyChanged=�5      5      5      5      .��5      � ���	#FF292C35 � RowDefinitions 5   
   �� 5      $��40 �6   �� 5      .+�5   
   �� $��0��6   $ 5,5,0,0��6   (  "GFramework.BlankWindow.BlankWindow   	Draggable . ���	#FF292C35���	#FF000000$��0 0 0 1q�6�   �� 5   
   -   5        $��	TitleName��5      $��0��6%    . $��8,0,0,0q�6[   $��Center=�6l   $��Center=�6�   ���	#FFFFFFFF$��14(�6�   $��摄像头筛选��6�   �� 5   
   -   5        $��CloseButton��5      $��0��6%     ButtonCloseStyle#���  62   $��24��6]   $��24��6i    K� ZIndex$ 10��6t   $��Right=�6�   $��Center=�6�   $��0,0,8,0q�6�   ��6�   �� *
CloseCmd+� 5   
   $��1��6   .+�5      �� ���	#FF000000$��1q�6-   $��0��6A   $��0��6N   $��2��6^   $��2��6r   $��10q�6�   ._�5      � ���	#FF16181B 5      �� 5      $��35 �6(   �� 5       � ColumnDefinitions 5      �� 5       $	��128 �6+   �� 5!      .+�5#      �� ���#1E2026$��0��63   $��1��6@   ���	#FF000000$��0 0 0 1q�6h   ._�5$      �� $��查看全部��6!   $��Right=�60   $��80��6L     SelectButtonStyle#��[ 6W   $��5q�6�   ��6�   �� *SelectAllCmd+5%      �� 5&      ���#1E2026$��0��62   ���	#FF000000$��0 0 1 1q�6W   ._�5'      �� $��Center=�6$   $��Center=�6A   ���#FFFFFF$��14(�6{   $��类别��6�   5(      �� 5)      $��0��6   ���#1E2026$��1��6B   ._�5*      �� -   5         ��Name$ 
tagListBox��5*   "   
������ $��Stretch=�6O    �� SelectionMode$ Single=�6�    �� SelectedIndex$ -1��5+   #     ListBoxItemGridNormolStyle#���  67   ��5*   m   �� *DeviceTypes+.  (System.Windows.Interactivity.Interaction   Triggers 5,      /  )System.Windows.Interactivity.EventTrigger  5-   "      	EventName$ SelectionChanged��61   .  (System.Windows.Interactivity.TriggerBase   Actions. 5.   &   6  0System.Windows.Interactivity.InvokeCommandAction     Command 6<   �� $" DataContext.SelectTagCommand�	 �� "
 �   CommandParameter 6�   ��  �� ElementName$ 
tagListBox��$ SelectedItem�5/   #   51      52      �� 53      $��1��6   $��1��6*   ���#000000._�54      �� $��1��6'   $��1��64   #���   6D   $	��4,2q�6q   ��6~   �� *	MenuItems+��55      �� 56   "   .R�57   &   � $��wp��60    � Orientation$ 
Horizontal=�6:   58   #   5:      5;      5<      5=      5>      5C      5D      