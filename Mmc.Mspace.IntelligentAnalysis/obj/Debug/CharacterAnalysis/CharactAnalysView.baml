   M S B A M L   `   `   `  ����(  $Mmc.Mspace.IntelligentAnalysisModuleN    HMmc.Mspace.IntelligentAnalysisModule.CharacterAnalysis.CharactAnalysView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Q MGFramework.BlankWindow, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'blankWindowhttp://GFramework.org  5   	   $	��450��5   	   $	��240��5   	     � WindowStartupLocation$  Manual=�5	         baseWindowStyle#��[  6%    � 	Resources 5
       �� 
TargetType  DefaultComboBoxStyle �� BasedOn��%0   '��       7  �� 5      " ��6
   8   6)   5      .��5      �  � RowDefinitions 5      �� 5      $��40 �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5       � ColumnDefinitions 5      �� 5      $
��Auto �6   �� 5      $��* �6   .+�5      �� $��0��6   ���#292C35$��0��6-   $��2��6=    �� CornerRadius$ 5,5,0,0��6Q   (  "GFramework.BlankWindow.BlankWindow   	Draggable . 6h   �� 5      -   5         ��Name$ 	TitleName��5       . $��0��6K   $��0��6X   $��2��6h   $��18,0,0,0q�6�   
�������$��18(�6�   $
��Left=�6�   $��Center=�6�   ��6|   �� *
WinTitle+6�   �� 5      -   5        	 ��Name$	 CloseButton��5      $��0��6   $��0��6-   $��2��6=     ButtonCloseStyle#��[ 6n   $��36��6�   $��36��6�   $��Right=�6�   $	��Top=�6�   $��0,2,8,0q�6�   ��6Q   �� *
CloseCmd+6�   �� 5      $��	20,20,0,0q�6   $��1��6!   $��0��6.     DefaultTextBlockStyle#���  6?   $��Center=�6o     Characteristicstype#���  6�   6�   �� 5      -   5        $��
analyseBox��5   L   $��10,20,20,10q�6   $��1��6"   $��36��6/   $��1��6;   
 �� DisplayMemberPath$

 Name��6�   ��6^   �� *AnalysModels+ �� SelectedItem 6�   �� *SelectedAnalysModel+6�   �� 5"      $��2��6   $��0��6   $��20,0,0,0q�6,   $��Center=�6>   #���  6Y     Analysisobject#���  6�   6�   � 5$      $��	20,0,20,0q�6	   $��2��6   $��1��6)   .+�5%      	� -   5         	�Name$ rBtnTif��5%   �   $	��0,5q�6     DefaultRadioButtonStyle#��[ 6   $
��Left=�6P   $��Center=�6k     Imagetif#���  6�    	� 	GroupName$ AnalyseMidea��6�   
������� d� 	IsChecked$
 TrueY�6�   6   	� 5&      -   5        $ 	rBtnVideo��5&   �   $	��0,5q�6   #��[ 6   $��Center=�6O    	 Video#���  6j   $ AnalyseMidea��6�   $��Right=�6�   
�������6�   5'      � 5(      $��	20,0,20,0q�6	   $��3��6   $��1��6)   $��1��69   .+�5)      ��  �� Orientation$ 
Horizontal=�6   $��2��6)   $��0��66   $��4��6F   .+�5*      	� -   5        $ stream_address��5*   �   $	��0,5q�6   #��[ 6    $
��Left=�6R   $��Center=�6m    	 stream_address#��� 	 6�   $ AnalyseMidea��6�   
�������6  �� 5+       #
 CharactAnalysView_StreamAddress#��� 
 6   #���  6U   $��Center=�6�   $��Center=�6�   $��0,0,10,0q�6�   �� 5,      $��0,0,20,0q�6/   $��Center=�6B   $��Center=�6]   $	��250��6z   ��6   �� *Stream_address+6z   5.      5/      � 50      $��20,0,0,0q�6	   $��4��6   $��2��6(   $��Bottom=�6<    51      �� 52      $	��40* �6   .+�55      �� $��3��6   $��0��6     DefaultButtonStyle#��[ 6)     OK#���  6U   $��0,0,102.6,29.6q�6�   $��Right=�6�   $��80��6�   $��36��6�   ��6u   �� *OKCmd+6�   �� 56      $��3��6   $��0��6   #��[ 6*    
 Cancel#���  6V   $��0,0,2.6,29.6q�6�   $��Right=�6�   $��80��6�   $��36��6�   ��6z   �� *
CloseCmd+6�   57      58      59      