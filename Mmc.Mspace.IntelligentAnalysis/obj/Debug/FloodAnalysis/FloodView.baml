   M S B A M L   `   `   `  ����K  GMmc.Mspace.Theme, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null[?clr-namespace:Mmc.Mspace.Theme.Helper;assembly=Mmc.Mspace.ThemeMmc.Mspace.Theme.Helper  5   	   _ [Mmc.Mspace.IntelligentAnalysisModule, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null[2clr-namespace:Mmc.Mspace.IntelligentAnalysisModule$Mmc.Mspace.IntelligentAnalysisModule 5      4   .Mmc.Mspace.IntelligentAnalysisModule.FloodView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   RHelper?clr-namespace:Mmc.Mspace.Theme.Helper;assembly=Mmc.Mspace.Theme     5   	   Dlocal2clr-namespace:Mmc.Mspace.IntelligentAnalysisModule     5      Q MGFramework.BlankWindow, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'blankWindowhttp://GFramework.org  5        � 
ResizeMode$  NoResize=�5	       � Title$ Flood��5   	   $��301.2��6   $��456.8��6&    � WindowStartupLocation$ CenterScreen=�64      baseWindowStyle#���   6Z   {�5
      �� *IsLoginFailed+ �� Mode$ OneWay=� � 	Resources 5       �� 
TargetType  ToggleButtonStyle �� Property �� Value  DefaultButtonBackground  DefaultButtonForeground �� 
TargetType	 ��Name
 �� Padding �� CornerRadius �� Triggers P� Property P� Value �� 
TargetName  DefaultButtonMouseOver  DefaultButtonPressed  DefaultButtonIsEnabled  test �� BasedOn  StartSimulation �� Triggers 	 PauseSimulation 
 
ReAnalysis��%�  &	       &	 R    7  &	
     �� 5      " d�6
   .�5      ��  � ��$
 14px(�6    �� 5       � ��$ 36��6   �� 5       � ��$ 80��6   �� 5       �@�FocusVisualStyle 6(   Z� �� 5       � ��$ 0q�6'   �� 5       � ��$ 0q�6   �� 5       � ��$	 2,1q�6   �� 5       �@J�SnapsToDevicePixels . 6+   �� 5       � ��# �  6"   �� 5       � ��
 ���� 6#   �� 5       � ��# �  6"   �� 5       � �� 5      �� 5      " d�6   .R�5      �� $	 Border��6   #
 z��6   #��z��6C   $ 2��6c   #��z��6t   #��z��6�   #��z��6�   #��z��6�   ._�5    	   �� $��Center=�6   #��z��68   $��Center=�6\   #��z��6w   5!   	    5"      P� 5#   	    �@J�IsMouseOver . . �5$   
   �� $ Border��6   # �  6;    � ��5%   
   P� 5&   	    � �� . . �5'   
   �� $ Border��6   # �  6;    � ��5(   
   P� 5)   	    � {� .  . �5*   
   �� $ Border��6    � ��$ 0q�6@   �� 5+   
   $ Border��6   # �  6;    � ��5,   
   5.      50      51      �� 52      " d�6   8   .�54      ��  � ��# �  6    55      P� 56       �@d�	IsChecked$
 TrueY�6#   . �57      ��  � ��# � 	 6!   58      5:      �� 5;      " ��6    5=      P� 5>       �@J�IsMouseOver . . �5?      ��  � ��# � 	 6!   5@      5D      5E      .��5F      � $��0,0,3.4,-0.8q�6    � ColumnDefinitions 5G      �� 5H      $	��69* �6   �� 5I      $��9* �6   �� 5J      $	��77* �6   �� 5K      $	��69* �6   �� 5L      $	��69* �6   �� 5M      $	��86* �6   �� 5N      $	��69* �6    � RowDefinitions 5P      �� 5Q      $��40 �6   6!   �� 5R      $
��Auto �6   $��20��6!   62   �� 5S      $
��Auto �6   �� 5T      $
��Auto �6   $��20��6!   62   �� 5U      $
��Auto �6   $��20��6!   62   �� 5V      $
��Auto �6   $��20��6!   62   �� 5W      $
��Auto �6   $��20��6!   62   �� 5X      $
��Auto �6   $��20��6!   62   �� 5Y      $
��Auto �6   6$   �� 5Z      $
��Auto �6   $��20��6!   62   �� 5[      $
��Auto �6   6$   �� 5\      .+�5^      ��  �� Orientation$ 
Horizontal=�6   $��2��6(   $��0��65   $��4��6E   .+�5_      ��   
UpEveTimes#���  6   $��2��63   $��0��6@   $��11(�6Q     DefaultTextBlockStyle#���  6_   $��Center=�6�   $��Center=�6�   $��5,0,5,0q�6�   �� 5`      $��2��6   $��1��6    �� TextWrapping$
 Wrap=�6+   $��60��6?   +   %Mmc.Mspace.Theme.Helper.TextBoxHelper   IsNeedClearButton .  $��5,0,5,0q�6�     TbxWaterStyle#���  6�   ��6y   �� *	CurHeight+$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5a        SV_Meter#���  6   $��2��61   $��2��6>   $��11(�6O   #���  6]   $��Center=�6�   $��5,0,5,0q�6�   5b      �� 5d      $��0��6   ���#292C35$��0��6.   $��7��6>   $ 5,5,0,0��6R   (  "GFramework.BlankWindow.BlankWindow   	Draggable . $��0,0,-1,0q�6�   �� 5e      -   5         ��Name$ 	TitleName��5e       . $��0��6K   $��0��6X    
 SysSet#���  6h   $��18,8,0,8q�6�   
�������$��18(�6�   $
��Left=�6�   $��Center=�6�   $��24��6�   $��0��6�   �� 5f      -   5         ��Name$ CloseButton��5f      $��0��6   $��6��6-      	Closeable .   ButtonCloseStyle#��[ 6�   $��36��6�   $��36��6�   $��Right=�6�   $	��Top=�6�   $��0,2,5,0q�6�   ��6>   �� *
DisposeCmd+6�   �� 5h        Floodanalysis#���  6   $��0��66   $��0��6C   $��2��6S   $
��Left=�6g   $��Center=�6�   #���  6�   $��12,2,2,2q�6�   �� 5j        	Frequency#���  6   $��2��61   $��4��6>   $��11(�6O   #���  6]   $��Center=�6�   $��Center=�6�   $��7,2,7,1q�6�   �� 5k        LoopPlayback#���  6   $��6��67   $��1��6D     PoiCheckBoxStyle#���  6T   $��11(�6   $��2��6�    d� 	IsChecked 6�   �� *	Loop_play+6�   �� 5l        CurrentSurfaceHeight#���  6   $��11(�6=   $��6��6K   $��4��6X   #���  6h   �� 5m      -   5        $	��sli��5m      $��4��6   $��1��6#    �� IsSnapToTickEnabled .  �� TickPlacement$
 Both=�6O    J� RenderTransformOrigin$ 0.5,0.5&�6d   $��5��6�     � Maximum$	  100Q�6�   $��1��6�   ! � Value! 6�   �� *Slider_value+$ TwoWay=�z�5n      V� 5o      .~�5p      �� " �� ScaleY$" -1Q�6   �� 5q      �� 5r      T� 5s      5t      5v      �� 5x        TimesInSeconds#���  6   $��2��66   $��6��6C   $��11(�6T   #���  6c   $��Center=�6�   $
��21,0q�6�   �� 5y      #���  6   $��6��62   $��6��6?   $��12(�6P   #���  6^   $��Center=�6�   $��	29,0,28,0q�6�   �� 5z      $��2��6   $��5��6    .  $��60��6X   #���  6c   $
 Wrap=�6�   $ 0.5,0.5&�6�   $
��13,0q�6  ��6�   �� *	Playtimes+$ TwoWay=�$ PropertyChanged=�6  �� 5{      #���  6*   $��6��6Z   $��5��6g   ��6   �� *
lable_text+6g   d� 5|      -   5        # d�Name$# buttonMouseOver��5|      $��8��6+   $��1��6:   $��35��6n   $��80��6z   #��[ 6�   $��2��6  $	��3,0q�6!  ��6J   �� *StartAnylyseCmd+ 6�   �� *FloodIsChecked+��6�   �� *	IsChecked+$ �� ElementName$$ buttonMouseOver��6!  �� 5~      -   5        $ 
ReAnalysis��5~        ReStartAnylyse#���  6!   $��8��6M   $��5��6Z   $��35��6j   $��80��6v     DefaultButtonStyle#��[ 6�   ��6�   �� *ReStartAnylyseCmd+6�   5�      5�      