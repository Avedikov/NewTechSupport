   M S B A M L   `   `   `  ����F  BTechSupport, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:TechSupport.ViewsTechSupport.Views  5   	   (    "TechSupport.Views.EditTicketWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:TechSupport.Views     5   	     � Title$  EditTicketWindow��5   	   $	��450��6"   $	��800��6/   .��5	      � $��10q�6    � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��	Тема:��6   �� 5   
   $��1��6   $��0,0,0,10q�5      ��5      �� *Ticket.Subject+ �� UpdateSourceTrigger$ PropertyChanged=�5      �� 5   
   $��2��6   $��Описание:��6!   �� 5   
   $��3��6    �� AcceptsReturn$
 True��5      $
��Auto=�6'   $��0,0,0,10q�5      ��5      �� *Ticket.Description+5      �� 5   
   $��4��6    �� Orientation$ 
Horizontal=�6"   .+�5      �� $��Статус:��6   $��Center=�6'   $��0,0,10,0q�6B   �� 5      $	��150��5!      ��5      �� *AvailableStatuses+ �� SelectedItem 5       �� *Ticket.Status+5!      5"      �� 5%   
   $��5��6   $ 
Horizontal=�6"   $��Right=�6;   .+�5&      �� $��Сохранить��6   $	��100��5'      $��0,0,10,0q�6!   ��5&   )   �� *SaveCommand+5'   !   �� 5(      $��Отмена��6   $	��100��5)      ��5(   &   �� *CancelCommand+5)      5*      5+      5,      