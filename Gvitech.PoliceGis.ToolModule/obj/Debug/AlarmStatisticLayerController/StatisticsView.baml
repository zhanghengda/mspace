   M S B A M L   `   `   `  ����P  LMmc.Mspace.ToolModule, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullyAclr-namespace:Mmc.Mspace.ToolModule.AlarmStatisticLayerController3Mmc.Mspace.ToolModule.AlarmStatisticLayerController  5      H    BMmc.Mspace.ToolModule.AlarmStatisticLayerController.StatisticsView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      d `DevExpress.Xpf.Charts.v11.2, Version=11.2.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxc4http://schemas.devexpress.com/winfx/2008/xaml/charts  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      ealarmstatisticlayercontrollerAclr-namespace:Mmc.Mspace.ToolModule.AlarmStatisticLayerController  5      $	��400��5      $��0,100,100,100q�5	      $��Right=�5
      $	��Top=�5        � AllowsTransparency  . ���	#00FFFFFF � ShowInTaskbar .   � SizeToContent$ Height=�5       � WindowStartupLocation$ Manual=�5       � WindowStyle$
 None=�5      .��5      �� -   5        $��1q�5      ��5      �� 5         BorderPanelStyle �� BasedOn# [  5       �� 
TargetType" ��5       �� Triggers 5      �� 5       �� Binding 5      �� *
IsActive+	 �� RelativeSource	 �� 
 �� Mode$
 FindAncestor=� �� AncestorType$ WindowL� �� Value true5      .a�5      ��  �� Property � ��  HighlightBackBrush �� Value# �  5      5       5"      ._�5$      �� $��Stretch=�5%      $��5q�5&      ��5'      ]� 5(      ��. .+�5*      �� $��Right=�5+        ButtonCloseStyle#��[ 5,      ��5-      �� *
CloseCmd+	 �� $
 FindAncestor=�$ WindowL��� 5.        ViewHeaderStyle#���  5/      ��50      �� *Tittle+51      .��53      �� .+�54      (  "DevExpress.Xpf.Charts.ChartControl  -   5        $��pieChart��55         EnableAnimation . ��57      Z�    Diagram 58      +  %DevExpress.Xpf.Charts.SimpleDiagram2D  59      #  DevExpress.Xpf.Charts.Diagram   Series 5:      '  !DevExpress.Xpf.Charts.PieSeries2D  5;   
   -   5        $��Series��5<   
   "  DevExpress.Xpf.Charts.Series   AnimationAutoStartMode$ SetStartState=�5=   
      ArgumentDataMember$
 Item��5>   
      ArgumentScaleType$ Qualitative=�5?   
   %  DevExpress.Xpf.Charts.PieSeries   HoleRadiusPercent$ 0Q�5@   
      Rotation$ 0Q�5A   
      SweepDirection$ Counterclockwise=�5B   
      ValueDataMember$ Value��5C   
      
DataSource 5D   
   �� *
ChartItems+   PointAnimation 5E      /  )DevExpress.Xpf.Charts.Pie2DFlyInAnimation  5F      )  #DevExpress.Xpf.Charts.AnimationBase   Duration$ 0:0:1.5h�5G      4	  .DevExpress.Xpf.Charts.SeriesPointAnimationBase 	  
PointOrder$ Random=�5H         PointOptions 5J      (
  "DevExpress.Xpf.Charts.PointOptions
  5K       
  	PointView$ ArgumentAndValues=�5L       
  ValueNumericOptions 5M      *  $DevExpress.Xpf.Charts.NumericOptions  5N          Format$  Percent=�5O      !   	Precision$! 0��5P      5R      "   Label" 5T      '  !DevExpress.Xpf.Charts.SeriesLabel  5U      #   ConnectorVisible# . $ J� IsManipulationEnabled$ .  $��0q�5X      %   
RenderMode$ % RectangleConnectedToCenter=�5Y      ,  &DevExpress.Xpf.Charts.ChartTextElement&   Visible& . 1  +DevExpress.Xpf.Charts.BarSideBySideSeries2D'   LabelPosition$' Outside=�5[      5]      5_   	   (   Titles( 5a      !  DevExpress.Xpf.Charts.Title  5b      %  DevExpress.Xpf.Charts.TitleBase)   HorizontalAlignment$) Center=�5c      *   Dock$	* Top=�5d      $��22(�5e      ���	#FFF5F5F5+   Content+ 5g      �� *Tittle+5i        5j      -   5        $��barChart��5k      ���	#00FFFFFF$��0q�5m       .  5o      '  !DevExpress.Xpf.Charts.XYDiagram2D  5p      ���	#00FFFFFF,   BarDistance$
, 0.04Q�5r       5s      bHclr-namespace:DevExpress.Xpf.Charts;assembly=DevExpress.Xpf.Charts.v11.2DevExpress.Xpf.Charts 5{   -     5t   "   UchartsHclr-namespace:DevExpress.Xpf.Charts;assembly=DevExpress.Xpf.Charts.v11.2  5{   -   -   5        $��series��5u   
   $ SetStartState=�5v   
   $
 Item��5w   
   -   
Background- �	#00FFFFFF.   DisplayName$
. 1998��5y   
   /   ShowInLegend/ .  $ Value��5{   
    5|   
   �� *
ChartItems+" 5}   &     5~   *   0   ConnectorThickness$0 1��5      1   Indent$1 20��5�      2   ResolveOverlappingMode$2 Default=�5�      & . $' Outside=�5�      5�      5�   	   ( 5�        5�      $) Center=�5�      $	* Top=�5�      $��22(�5�      ���	#FFF5F5F5+ 5�      �� *Tittle+5�      5�      5�      5�      5�      5�      