   M S B A M L   `   `   `  ����F  BTechSupport, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:TechSupport.ViewModelsTechSupport.ViewModels  5      ?$clr-namespace:TechSupport.ViewModelsTechSupport.ViewModels  6?   %    TechSupport.Views.NewTicketPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      ;
viewModels$clr-namespace:TechSupport.ViewModels     5      <viewModels1$clr-namespace:TechSupport.ViewModels     6?   @d2http://schemas.microsoft.com/expression/blend/2008     6x     N� Title$  Новая заявка��5       N� 
Background
 �����$	��370��6/   .��5      � $��10q�6    � RowDefinitions 5	   
   �� 5
      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��	Тема:��6!   $��0,0,0,5q�6.   �� 5   
   $��1��6   $��0,0,0,10q�5      ��5      �� *	Subject+ �� UpdateSourceTrigger$ PropertyChanged=�5      �� 5   
   $��2��6   $��Фамилия:��6!   $��0,0,0,5q�61   �� 5   
   $��3��6   $��0,0,0,10q�5      ��5      �� *
ClientName+$ PropertyChanged=�5      �� 5    
   $��4��6   $��Email:��6!   $��0,0,0,5q�6/   �� 5!   
   $��5��6   $��0,0,0,10q�5"      ��5!      �� *Email+$ PropertyChanged=�5"      �� 5%   
   $��6��6   $!��Ответственный:��6!   $��0,0,0,5q�67   �� 5&   
   $��7��6    �� DisplayMemberPath$
 Name��5(       �� SelectedValuePath$ Id��5)      $��0,0,0,10q�5+      ��5'      �� *TechSupportUsers+ �� SelectedValue 5*      �� *AssignedUserId+5+      �� 5.   
   $��8��6   $��Описание:��6!   $��0,0,0,5q�62   �� 5/   
   $��9��6    �� AcceptsReturn$
 True��6<   $	��100��50      $��0,0,0,10q�6   ��5/      �� *Description+50      �� 53   
   $��10��6    �� Orientation$ 
Horizontal=�6#   $��Right=�6<   .+�54      �� $#��Прикрепить файл��6   $��0,0,10,0q�55      $
��10,5q�6'   $��46��66   ��54   /   �� *AttachFileCommand+55   6   �� 56      $!��Создать заявку��6   $
��10,5q�57      $��47��6$   ��56   .   �� *CreateTicketCommand+57   $   58      59      5:      