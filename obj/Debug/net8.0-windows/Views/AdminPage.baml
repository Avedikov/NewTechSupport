   M S B A M L   `   `   `  ����F  BTechSupport, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:TechSupport.ViewModelsTechSupport.ViewModels  5   >   !    TechSupport.Views.AdminPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      3vm$clr-namespace:TechSupport.ViewModels     6>     N� Title$(  "Администрирование��5      $	��750��6!   $	��200��6-   .��5      � .+�5	   
   �� .@�5
      �� $��Пользователи��6   .��5      �  � RowDefinitions 5      �� 5      $
��Auto �6(   �� 5      $��* �6(   �� 5      $
��Auto �6(   .+�5      �� $��0��6!    �� Orientation$ 
Horizontal=�6.   $��5q�6G   .+�5      �� $��Добавить��6!   $��0,0,10,0q�5      $
��10,5q�6/   ��5   4   �� *AddUserCommand+5   /   �� 5      $��Сохранить��6!   $��0,0,10,0q�5      $
��10,5q�6/   ��5   5   �� *SaveUsersCommand+5   /   �� 5      $#��Сделать админом��6!   $��0,0,10,0q�5      $
��10,5q�6/   ��5   ;   �� *MakeAdminCommand+5   /   �� 5      $��Экспорт в PDF��6!   $
��10,5q�5      ��5   9   �� *ExportUsersCommand+5      5      &   System.Windows.Controls.DataGrid  5      $��1��6      AutoGenerateColumns$ False��5!         CanUserAddRows$ False��5"      $��0,0,0,10q�5#      ��5   ,   �� *Users+ �� SelectedItem 5       �� *SelectedUser+   Columns 5$      0  *System.Windows.Controls.DataGridTextColumn  5%      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��61      
IsReadOnly$
 True��6T   	   Width5  /System.Windows.Controls.DataGridLengthConverter$
	 Auto 6f   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6=   �� *Id+6f     5&      $ 
Логин��61   $	 * 6]   
 6@   �� *
Username+6]     5'      $ Имя��61   $	 * 6W   
 6>   �� *Name+6W     5*      $ 
Отдел��61   $	 * 6�   
 6@   �� *
Department+ �� UpdateSourceTrigger$ PropertyChanged=�6�     5+      $ Должность��61   $	 * 6�   
 6D   �� *
Position+$ PropertyChanged=�6�   4  .System.Windows.Controls.DataGridTemplateColumn  5-      $ Роль��65   $	 * 6C      CellTemplate 5.   "   �� 5/   &   .R�50   *   ��  �� DisplayMemberPath$ .��53      ��50   3   �� *DataContext.Roles+ �� RelativeSource ��  �� AncestorType DataGrid 52      �� *Role+ �� Mode$ TwoWay=�$ PropertyChanged=�53      54   '   56        58      $ Email��61   $	 * 6Z   
 6@   �� *Email+6Z   4  .System.Windows.Controls.DataGridCheckBoxColumn  59      $ Активен��65   $
	 Auto 6c   
 6F   �� *
IsActive+6c   5;      �� 5>      $��2��6    .@�5?      �� ��6(   �� *StatusMessage+5@      5A      5B      �� 5D      $��Статистика��6   .��5E      �� .��5F      �� .+�5G      �� $.��(Статистика по заявкам��6$   $��16(�6A   $��10q�6O   �� 5J      $��Фильтры��6#   $��10q�64   .��5K      �� $ 
Horizontal=�6)   .+�5L   "   (  "System.Windows.Controls.DatePicker  -   5          Name$ statsFromDate��5L   -   $��5q�6D   $	��120��6O      SelectedDate 5M   )   �� *StatsDateFrom+  5N   "   -   5        $ statsToDate��5N   -   $��5q�6B   $	��120��6M    5O   )   �� *StatsDateTo+�� 5P   "   $��Обновить��6)   $��5q�5Q   %   $
��10,5q�60   ��5P   <   �� *RefreshStatsCommand+5Q   0   5R      5S      � 5V       � ColumnDefinitions 5W      �� 5X   "   $��* �63   �� 5Y   "   $��* �63   .+�5\      �� $��По статусам��6'   $��0��6<   $��5q�6L   .��5]   "   �� $)��#Диаграмма статусов��6,   $��Center=�6F   $��Center=�6c   5^      �� 5`      $#��По исполнителям��6'   $��1��6@   $��5q�6P   .��5a   "   �� $1��+Диаграмма исполнителей��6,   $��Center=�6J   $��Center=�6g   5b      5c      5d      5e      5f      5g      5h      5i      