   M S B A M L   `   `   `  ����F  BTechSupport, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI.clr-namespace:TechSupport.Converters;assembly=TechSupport.Converters  5   E       TechSupport.App   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      E
converters.clr-namespace:TechSupport.Converters;assembly=     6E     �� ShutdownMode$  OnMainWindowClose=�5       �� 	Resources 5      3   -TechSupport.Converters.StatusToColorConverter   StatusToColorConverter6   0TechSupport.Converters.BoolToVisibilityConverter  BoolToVisibilityConverter �� 
TargetType  NavButtonStyle �� Property �� Value �� 
TargetType �� Triggers P� Property P� Value(  "System.Windows.Controls.DatePicker��%�  &	        &	      &	 &     '  H      5   
   6,     5   
   6/   �� 5   
   " ��6   .�5      ��  � ��
 ���� 6+   �� 5       � ��
 �����6+   �� 5       � ��$ 0q�60   �� 5       � ��$
 Left=�64   �� 5       � ��$
 10,5q�6(   �� 5       � ��$	 5,2q�6'   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� 
������ ._�5      �� $
��Left=�6/   $��Center=�5   '   5      5      5       5      P� 5       �@J�IsMouseOver$
 True��61   . �5      ��  � �� �#3E4E5F63   5       5"      �� 5$   
   "  6   .�5%      ��  � ��$ 5q�6(   �� 5&       � ��$ Center=�69   5'      5)      5*      