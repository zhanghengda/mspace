   M S B A M L   `   `   `  ����V  RMmc.Mspace.PoiManagerModule, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU/clr-namespace:Mmc.Mspace.PoiManagerModule.Views!Mmc.Mspace.PoiManagerModule.Views  5      K GMmc.Mspace.Theme, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_Aclr-namespace:Mmc.Mspace.Theme.Controls;assembly=Mmc.Mspace.ThemeMmc.Mspace.Theme.Controls 5      7    1Mmc.Mspace.PoiManagerModule.Views.AccountListView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Alocal/clr-namespace:Mmc.Mspace.PoiManagerModule.Views     5      c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      PcontrolsAclr-namespace:Mmc.Mspace.Theme.Controls;assembly=Mmc.Mspace.Theme  5      Q MGFramework.BlankWindow, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'blankWindowhttp://GFramework.org  5	        � Title$  AccountListView��5   	   $	��564��6!   $	��687��6.      baseWindowStyle#���   6:    � 
ResizeMode$ NoResize=�6d   y�5
      �� *AccountListVisibility+.��5      �  � RowDefinitions 5   
   �� 5      $��40 �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      .+�5      �� $��0��6   ���#292C35$��0��67   $��2��6G    �� CornerRadius$ 5,5,0,0��6[   (  "GFramework.BlankWindow.BlankWindow   	Draggable . $
��1200��6�   $��40��6�   �� 5   
   -   5         ��Name$ 	TitleName��5       . $��0��6Q   $��0��6^   $��2��6n     Account#���  6�   $��18,0,0,0q�6�   
�������$��18(�6�   $
��Left=�6�   $��Center=�6�   �� 5   
   -   5         ��Name$ CloseButton��5      $��0��6   $��0��63   $��2��6C     ButtonCloseStyle#���  6{   $��36��6�   $��36��6�   $��Right=�6�   $	��Top=�6�   $��0,2,8,0q�6�   ��6W   �� *CloseWindowCmd+6�   �� 5   
   $��5��6   $��1��6"     AccountListIsEnableBtn#��[ 6Z   
������  K� ZIndex$ 10��6�   y�6/   �� *AccountListIsEnable+6�   �� 5   
   $��1��6    �� Orientation$ 
Horizontal=�6"   .+�5      '  !Mmc.Mspace.Theme.Controls.Buttons  -   5        	  Name$		 bs1��5      $��新增台账��6,     AddNavigationPoint
   RIcon#
 �  5        AddNavigationPoint_H   MouseOverRIcon# �  5        AddNavigationPoint_C   PressedRIcon# �  5        AddNavigationPoint_D
 � Tag# �  5        AddandExportAccountBtnStyle#��[ 5      $��32��6y   $	��100��6�   ��6T   �� *AddNewAccountCmd+6�   5'      &   System.Windows.Controls.DataGrid  5(   
   -   5        $��routeplandg��5*      $��2��5(      $��Center=�6     �� AlternationCount$ 2��6D      VerticalGridLinesBrush
 �   �   HorizontalGridLinesBrush
 ����    CanUserAddRows .     AutoGenerateColumns .     CanUserResizeColumns .  
���   �$	��671��5)   |   $��5q�6�   ��5*   *   �� *AccountCollection+.  (System.Windows.Interactivity.Interaction   Triggers 5+      /  )System.Windows.Interactivity.EventTrigger  5,         	EventName$ MouseDoubleClick��6%   .  (System.Windows.Interactivity.TriggerBase   Actions. 5-      6  0System.Windows.Interactivity.InvokeCommandAction     Command 60   �� *LeftDoubleClickCmd+   CommandParameter 6W   �� *SelectedItem+ �� ElementName$ routeplandg�� �� Mode$ TwoWay=�5.         ColumnHeaderStyle 50      �� 51       �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6   .�52      ��  �� Property � �� �� Value$ Center=�6C   �� 53       � �� �#1E20266%   �� 54       � �� �#B3BBC563   �� 55       � ��$ 0,0,1,0q�68   �� 56       � �� �#00000064   �� 57       � ��$ 36��6/   �� 58       � ��$ 12(�61   59         RowStyle 5;      �� 5<      )	  #System.Windows.Controls.DataGridRow" 	 6     �� Triggers  5=      P� 5>      ! P� Property! �@	 AlternationIndex" P� Value$" 0��6>   . �5?      ��  � �� �#0A0B0D6;   �� 5@   "    � �� �#C6CCD46?   5A      P� 5B      ! �@	 AlternationIndex$" 1��6>   . �5C      ��  � �� �#14151A6;   �� 5D   "    � �� �#C6CCD46?   5E      P� 5F      ! �@J�IsMouseOver" . . �5G   "   ��  � �� �#50ABFF6?   �� 5H   "    � ��
 �����6?   5I      P� 5J      ! �@	 
IsSelected" . . �5K   "   ��  � �� �#1890FF6?   �� 5L   "    � ��
 �����6?   5M      5O      #   Columns# 5Q      4
  .System.Windows.Controls.DataGridTemplateColumn
  5R       	 AccountListView_Id,  &System.Windows.Controls.DataGridColumn$   Header#$ � 	 6-   %   Width5  /System.Windows.Controls.DataGridLengthConverter$% 78 6[   & 
  CellTemplate& 5S      �� 5T      .R�5U   "   �� ' �� TextAlignment$' Center=�6,   $��WordEllipsis=�6D   $��12(�6�   ( � ToolTip( 6`   �� *Id+��6w   �� *Id+6�   5V      5X      
  5Y       
 AccountListView_Title#$ � 
 6)   $	% 124 6Z   & 5Z      �� 5[      .R�5\   
   �� $' Center=�6   $��WordEllipsis=�6+   $��12(�6y   $	��132��6�   ( 6G   �� *Title+��6a   �� *Title+6�   5]      5_      
  5`        AccountListView_Img#$ �  6-   $	% 113 6\   & 5a      �� 5b      .R�5d      �� $' Center=�6(   $��WordEllipsis=�6?   $��12(�6�   ( 6[   �� *ImgNum+��6v   �� *ImgNum+6�   5e      5g      
  5h        AccountListView_Video#$ �  6)   $% 78 6Z   & 5i      �� 5j      .R�5k   
   �� $' Center=�6   $��WordEllipsis=�6+   $��12(�6y   ( 6G   �� *Video+��6a   �� *Video+6y   5l      5n      
  5o        AccountListView_AddedTime#$ �  6)   $% 98 6^   & 5p      �� 5q      .R�5r      �� $' Center=�6(   $��WordEllipsis=�6?   $��12(�6�   ( 6[   �� *	AddedTime+��6y   �� *ProblemTime+6�   5s      5u      
  5w      $$ 报告可见��6)   $% 78 67   & 5x      �� 5y      .R�5z      �� $' Center=�6(   $��WordEllipsis=�6?   $��12(�6�   ( 6[   �� *	AddedTime+��6y   �� *IsShowInReport+6�   5{      5}      
  5        AccountListView_Operation#$ �  6)   $	% 100 6^   & 5�      �� 5�      .R�5�      �� $ 
Horizontal=�6)   $��Center=�6B   .+�5�   "     $		 bs1��63   $��23��6@   $��30��6L   $�� ��6W     Account_Edit#
 �  5�   1     Account_Edit_H# �  5�   1     Account_Edit_C# �  5�   1   #��[ 5�   1   ��5�   1   �� *DataContext.EditAccountCmd+) �� RelativeSource) �� * �� AncestorLevel$* 1��+ �� AncestorType$+ DataGridL���5�   1   �� *SelectedItem+$ routeplandg��  5�   "   $		 bs2��63   $��23��6@   $��30��6L   $�� ��6W   $��20,0,0,0q�6b     DeleteAccountItem#
 �  5�   1     DeleteAccountItem_H# �  5�   1     DeleteAccountItem_C# �  5�   1   #��[ 5�   1   ��5�   1   �� *DataContext.DeleteAccountCmd+) �� $* 1��$+ DataGridL���5�   1   �� *SelectedItem+$ routeplandg��5�      5�      5�      5�      �� 5�   
   $��3��6   $ 
Horizontal=�6"   $��Right=�6;   $��500,10,16,10q�6X   .+�5�        -   5        $		 bs5��5�      $�� ��6,     FirstPageNew#
 �  5�   !     FirstPageNew_H# �  5�   !     FirstPageNew_C# �  5�   !     FirstPageNew_D# �  5�   !   #��[ 5�   !   $��32��6w   $��32��6�   ��6V   �� *FirstPageCmd+6�     5�      -   5        $		 bs3��5�      $�� ��6,     LastPageNew#
 �  5�   !     LastPageNew_H# �  5�   !     LastPageNew_C# �  5�   !     LastPageNew_D# �  5�   !   #��[ 5�   !   $��32��6v   $��32��6�   {�5�   !   �� *LastPageBtnEnable+��5�   V   �� *LastPageCmd+6�   �� 5�      $��14(�6F   ���#B3BBC5$��5,5,0,5q�6j   ��6   �� *	PageNum+, �� StringFormat$
, {0}/��6j   �� 5�      $��14(�63   ���#B3BBC5$��0,5,5,5q�6W   ��6   �� *	PageCount+6W     5�      -   5        $		 bs4��5�      $�� ��6,     NextPageNew#
 �  5�        NextPageNew_H# �  5�   !     NextPageNew_C# �  5�   !      NextPageNew_D# �   5�   !   #��[ 5�   !   $��32��6v   $��32��6�   {�5�      �� *NextPageBtnEnable+��5�   V   �� *NextPageCmd+6�     5�      -   5        $		 bs6��5�      $�� ��6,    ! FinalPageNew#
 � ! 5�       " FinalPageNew_H# � " 5�   !    # FinalPageNew_C# � # 5�   !    $ FinalPageNew_D# � $ 5�   !   #��[ 5�   !   $��32��6u   $��32��6�   ��6V   �� *
EndPageCmd+6�   5�      5�      5�      