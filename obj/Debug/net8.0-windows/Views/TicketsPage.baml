   M S B A M L   `   `   `  ����F  BTechSupport, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:TechSupport.ViewModelsTechSupport.ViewModels  5   >   ?$clr-namespace:TechSupport.ConvertersTechSupport.Converters  6n   #    TechSupport.Views.TicketsPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      3vm$clr-namespace:TechSupport.ViewModels     6>   6local$clr-namespace:TechSupport.Converters     6n     N� Title$  
TicketPage��5       � 	Resources 5      2   ,TechSupport.Converters.UserIdToNameConverter   UserConverter��%   &	          5   
   6&   5      .��5	      � $
��1009��6    � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6    �� Orientation$ 
Horizontal=�6"   $��10q�6;   .+�5      �� -   5         ��Name$ 	searchBox��5      $	��200��6)   $��0,0,10,0q�65   ��5      �� *
SearchText+ �� UpdateSourceTrigger$ PropertyChanged=��� 5      -   5         ��Name$ 	cmbStatus��5      $	��150��6*   $��5q�66   ��5      �� *
Statuses+ �� SelectedItem 5      �� *SelectedStatus+(  "System.Windows.Controls.DatePicker  5      $	��120��6   $��0,0,10,0q�6%    � ToolTip$ Дата с��5      	   SelectedDate	 5      �� *
DateFrom+5        5      $	��120��6   $��0,0,10,0q�6%   $ Дата по��5      	 5      �� *DateTo+5      �� 5      $��Фильтрация��6   $
��10,5q�5       ��5   *   �� *SearchCommand+5       �� 5"      $��
Сброс��6   $
��10,5q�5#      $��80��6    ��5"   %   �� *ResetSearchCommand+5#       �� 5$      $��
Отчет��6   $
��10,5q�5&      $	��120��6    $��40��6,   $F @Создать PDF отчет по текущим заявкам��5'      ��5%      �� *GeneratePdfCommand+5'      55      �� 59   
   $��1��6   ��6    �� *	Tickets+ 5:      �� *SelectedTicket+
 �� View
 5;      �� 5<      .P�5=      �� $��ID��6%    �� Width$ 50��6U    �� DisplayMemberBinding 61   �� *Id+6U   �� 5>      $��Тема��6%   $	 200��6\    63   �� *	Subject+6\   �� 5?      $��Фамилия��6%   $	 100��6b    66   �� *
ClientName+6b   �� 5@      $��Статус��6%   $	 100��65    �� CellTemplate 5A      �� 5B      .R�5C   "   �� ��6,   �� *Status+��5D   ,   �� *Status+  StatusToColorConverter �� 	Converter# [ 5E      5G      �� 5H      $��Дата создания��6%   $	 100��6�    6<   �� *
CreateDate+ �� StringFormat$ 
dd.MM.yyyy��6�   �� 5I      $#��Дата обновления��6%   $	 100��6�    6>   �� *LastUpdated+$ 
dd.MM.yyyy��6�   �� 5J      $0��*Отвественный за работу��6%   $	 150��6x    6E   �� *AssignedUser.Name+6x   5K      5M      5O      5P      