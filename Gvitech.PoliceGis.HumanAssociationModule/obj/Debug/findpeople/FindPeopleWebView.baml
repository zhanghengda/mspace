   M S B A M L   `   `   `  ����\  XMmc.Mspace.HumanAssociationModule, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullk:clr-namespace:Mmc.Mspace.HumanAssociationModule.findpeople,Mmc.Mspace.HumanAssociationModule.findpeople  5      D    >Mmc.Mspace.HumanAssociationModule.findpeople.FindPeopleWebView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      Llocal:clr-namespace:Mmc.Mspace.HumanAssociationModule.findpeople     5        � 
ResizeMode$  NoResize=�5	       � WindowStartupLocation$ CenterScreen=�5
      ���	#00FFFFFF � ShowInTaskbar .   � WindowStyle$
 None=�5       � Title$ FindPeopleWebView��5   	   $	��800��5   	   $
��1200��5   	    J� Opacity$	 0.7Q�5   	    � Left 5      �� *Left+
 � Top 5      �� *Top+.��5      �� -   5        $��1q�5      ��5   
   �� 5         BorderPanelStyle �� BasedOn# [  6   	 �� 
TargetType"	 ��6@   
 �� Triggers
 5      �� 5       �� Binding 5      �� *
IsActive+ �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType$ WindowL� �� Value true5      .a�5      ��  �� Property � ��  HighlightBackBrush �� Value# �  5       5!      5#      ._�5%   
   �� $��Stretch=�5&      $��5q�5'      ��5(      ]� 5)      .+�5*      �� $��Right=�5+        ButtonCloseStyle#��[ 5,      ��5-      �� *
CloseCmd+�� 5.      -   5        $��txtCtl��5.         ViewHeaderStyle#���  5/      $��建筑档案册��50      51      .��53      � .+�54      (  "System.Windows.Controls.WebBrowser  -   5        $��webCtrl��54      6,   55      56      58      59      