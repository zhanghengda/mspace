   M S B A M L   `   `   `  ����U  QMmc.Mspace.WireTowerModule, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS.clr-namespace:Mmc.Mspace.WireTowerModule.Views Mmc.Mspace.WireTowerModule.Views  5   	   /    )Mmc.Mspace.WireTowerModule.Views.LineView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @local.clr-namespace:Mmc.Mspace.WireTowerModule.Views     5   	   Q MGFramework.BlankWindow, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'blankWindowhttp://GFramework.org  5   	     � Title$  LineView��5	   	   $	��290��6   $	��260��6'    � AllowsTransparency . .��5
      ��    LeftWindowBg#��[  6    �� CornerRadius$ 5,5,5,5��68   ._�5   
   �  � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    .+�5      �� $��0��6   ���#292C35$��0��67   $��2��6G   $ 5,5,0,0��6[   (  "GFramework.BlankWindow.BlankWindow   	Draggable . 6r   �� 5      -   5         ��Name$ 	TitleName��5       . $��0��6U   $��0��6b   $��2��6r   $��18,0,0,0q�6�   
�������$��14(�6�   $
��Left=�6�   $��Center=�6�   ��6�   �� *	ViewTitle+6�   �� 5      -   5         ��Name$ CloseButton��5   "   $��0��6   $��0��67   $��2��6G     ButtonCloseStyle#���  6~   $��24��6�   $��24��6�   $��Right=�6�   $	��Top=�6�   $��0,2,8,0q�6�   ��6[   �� *CancelCommand+6�   �� 5      $��1��6    �� Orientation$ 
Horizontal=�6&   $
��10,6q�6?   .+�5      �� $��70��6     
WTLineName#��[ 6$     LabelTextStyle#��[ 6J   �� 5      $	��150��6     EditableTextBoxStyle#��[ 6)   ��6W   �� *LineModel.LineName+5      �� 5      $��2��6   $ 
Horizontal=�6&   $
��10,6q�6?   .+�5      �� $��70��6     WTLineSerial#��[ 6$   #��[ 6L   �� 5       $	��150��6   #��[ 6*   ��6X   �� *LineModel.LineSerial+5!      �� 5#      $��3��6   $ 
Horizontal=�6&   $
��10,6q�6?   .+�5$      �� $��70��6     WTLineLenght#��[ 6$   #��[ 6L   �� 5%      $	��125��6   #��[ 6)   ��6W   �� *LineModel.LineLength+�� 5&      $��30��6   $��km��6$   #��[ 61   5'      �� 5)      $��4��6   $ 
Horizontal=�6&   $
��10,6q�6?   .+�5*      �� $��70��6     WTLineVolLeval#��[ 6$   #��[ 6N   �� 5+      $	��125��6   #��[ 6)   ��6W   �� *LineModel.VoltageLevel+�� 5,      $��30��6   $��kv��6$   #��[ 61   5-      �� 5/      $��5��6   $ 
Horizontal=�6&   $
��10,6q�6?   .+�50      �� $��70��6     WTLineTowerCount#��[ 6$   #��[ 6P   �� 51      $	��125��6   #��[ 6)   ��6W   �� *LineModel.TowerCount+53      �� 55      $��6��6   $��10,10q�6&   $ 
Horizontal=�65   $��Center=�6N   .+�56      �� -   5        $ okBtn��56       	 OK#��� 	 6)   $��Right=�6H   $��24��6d   $
��20,0q�6p   $��Bottom=�6�   $��46��6�   $��10(�6�    
 DefaultButtonStyle#��� 
 6�   ��6�   �� *SaveCommand+{�6  �� *IsOkBtnEnable+�� 57      -   5        $ 	cancelBtn��57       
 Cancel#���  6-   $��Right=�6P   $��24��6l   $��10(�6x   $
��20,0q�6�   $��Bottom=�6�   $��46��6�   #��� 
 6�   ��6�   �� *CancelCommand+58      5:      5;      5<      