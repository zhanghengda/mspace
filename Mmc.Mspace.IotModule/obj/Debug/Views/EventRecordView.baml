   M S B A M L   `   `   `  ����O  KMmc.Mspace.IotModule, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:Mmc.Mspace.IotModule.ViewsMmc.Mspace.IotModule.Views  5   	   0    *Mmc.Mspace.IotModule.Views.EventRecordView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   :local(clr-namespace:Mmc.Mspace.IotModule.Views     5   	   Q MGFramework.BlankWindow, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'blankWindowhttp://GFramework.org  5   	     � Title$  EventRecordView��5	   	   $	��330��6"   $	��300��6/   ���#292C35 � 
ResizeMode$ NoResize=�6P   .��5
      �  � RowDefinitions 5   
   �� 5      $��40 �6   �� 5      $��* �6   .+�5   
   �� $��0��6    �� CornerRadius$ 5,5,0,0��6   (  "GFramework.BlankWindow.BlankWindow   	Draggable . ���#292C356_   �� 5   
   -   5         ��Name$ 	TitleName��5   !   $��0��6    . $��8,0,0,0q�6]   $
��Left=�6o   $��Center=�6�   
�������$��14(�6�   ��6�   �� *Title+�� 5   
   -   5         ��Name$ CloseButton��5      $��0��6      ButtonCloseStyle#���   6P   $��24��6{   $��24��6�    K� ZIndex$ 10��6�   $��Right=�6�   $��Center=�6�   $��0,0,8,0q�6�   ��63   �� *
CloseCmd+6�   &   System.Windows.Controls.Calendar  5   
   -   5        $��1��5         FirstDayOfWeek$ Sunday=�6!   	   SelectionMode$	 MultipleRange=�69   $
��Left=�6W   $	��324��6r   $��10q�6   $	��Top=�6�   $	��288��6�     CalendarStyle#���  6�     CalendarItemStyle
   CalendarItemStyle#
 �  6�     CalendarDayButtonStyle   CalendarDayButtonStyle# �  6  6Q  5      5      