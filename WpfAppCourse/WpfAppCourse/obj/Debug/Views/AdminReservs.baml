   M S B A M L   `   `   `  ����G  CWpfAppCourse, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:WpfAppCourse.ViewsWpfAppCourse.Views  5      %    WpfAppCourse.Views.AdminReservs   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:WpfAppCourse.Views     5        N� Title$  AdminReservs��5	       N� 
Background �#15211B.��5
      �� $
��Auto=�6   $��0,0,0,28q�66   .��5   
   � .+�5      � $	��800��6   $��0,10,0,0q�6#   $	��484��65   $	��Top=�6B    � ColumnDefinitions 5      �� 5      $��* �6'   �� 5      $��* �6'   �� 5      $��* �6'   �� 5      $��* �6'   �� 5      $��* �6'   �� 5      $��* �6'   .+�5      �� $I��CD:\2курс4сем\картинки для всего\лого.png��6   $��2��6K   $��
40,18,43,0q�6[   $��87��6o   $	��Top=�6{   �� 5      -   5         ��Name$ Users��5      $��Клиенты��6+   $��0��6:   $��24,46,24,376q�6J    �� TextAlignment$ Center=�6`   
�������$��Century Schoolbook)�5      $��20(�6<    � Cursor$
 Hand��6J   6X   �� 5      -   5        $ Services��5      $��Процедуры��6.   $��1��6?   $��30,46,18,347q�6O   $ Center=�6e   
�������$��Century Schoolbook)�5      $��20(�6<   6J   �� 5      -   5        $ Masters��5      $��Мастера��6-   $��3��6<   $��24,46,24,376q�6L   $ Center=�6b   
�������$��Century Schoolbook)�5      $��20(�6<   $
 Hand��6J   6X   �� 5      -   5        $ Reserv��5      $��Записи��6,   $��4��6:   $��24,46,24,392q�6J   $ Center=�6`   
�������$��Century Schoolbook)�5      $��20(�6<   �� 5      -   5        $ Review��5      $��Отзывы��6,   $��5��6:   $��24,46,24,392q�6J   $ Center=�6`   
�������$��Century Schoolbook)�5      $��20(�6.   6<   5       � 5"      $��
10,10,10,0q�6   $	��634��6'   $	��Top=�64    � RowDefinitions 5#      �� 5$      $��* �6$   �� 5%      $
��Auto �6$   .+�5(      &   System.Windows.Controls.DataGrid  -   5          Name$ ReservDataGrid��5(         AutoGenerateColumns .  	   CanUserAddRows	 .  
   
IsReadOnly
 .     GridLinesVisibility$	 All=�50         HeadersVisibility$ Column=�51         AlternatingRowBackground �	#FFEFEFEF���#FFD700$��1q�54      $��14(�55      $��Century)�56      $��0,179,420,20q�57      $��0��58      ��5+      �� *	Reservs+ �� SelectedItem 5,      �� *SelectedReserv+   Columns 59      0  *System.Windows.Controls.DataGridTextColumn  5:      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6-      
IsReadOnly .    Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6b   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   �� *ID+6b     5;      $ Дата��6-   $ * 6T    6;   �� *Date+6T     5<      $ 
Время��6-   $ 2* 6U    6<   �� *Time+6U     5=      $ ID Мастера ��6-   $
 Auto 6`    6B   �� *	Master_Id+6`     5>      $ ID Процедуры��6-   $
 Auto 6b    6C   �� *
Service_Id+6b     5?      $! ID Пользователя��6-   $ * 6b    6F   �� *	User_Id+6b   5B      �� 5D      $+��%Сохранить изменения��6   ���#FFD700$
��Bold"�5H      
�������$��Century)�5J      $��0,0,99,0q�5K      $��46��5L      $��Center=�5M      $��Right=�64   $	��145��6P   ��5E      �� *SaveChangesCommand+��5N      �� 5O       �� 
TargetType" ��6*   .R�5P      ��  �� CornerRadius$ 20��6%   #��z��67   $��0q�6a   ._�5Q   "   �� $��Center=�63   $��Center=�6P   5R      5S      5U      �� 5W      $��Удалить��6   ���#FF4444
�������$
��Bold"�5[   	   $��41��5\   	   $��
0,205,99,0q�5]   	   $	��Top=�5^   	   $��Right=�5_   	   $	��145��5`   	   ��5X   	   �� *DeleteReservCommand+��5a      �� 5b      " ��6*   .R�5c      �� $ 20��6%   #��z��67   $��0q�6a   ._�5d   "   �� $��Center=�63   $��Center=�6P   5e      5f      5h      5j      5k      5l      5m      