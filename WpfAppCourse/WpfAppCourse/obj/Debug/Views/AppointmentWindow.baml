   M S B A M L   `   `   `  ����  WpfAppCourse*    $WpfAppCourse.Views.AppointmentWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$*  $Запись на процедуру��5   	   $	��350��6%   $	��450��62    � WindowStartupLocation$ CenterScreen=�5   	    � 
ResizeMode$ NoResize=�6.    � WindowStyle$
 None=�5   	    � AllowsTransparency . 
������ .��5      � .+�5	   
   �� 
������� �� CornerRadius$ 15��6$   ���	#FFD4AF37$��2q�6N   ._�5
      � $��25q�6    � RowDefinitions 5      �� 5      $
��Auto �6$   �� 5      $
��Auto �6$   �� 5      $
��Auto �6$   �� 5      $
��Auto �6$   �� 5      $
��Auto �6$   �� 5      $��* �6$   .+�5      �� $��Процедура:��6   $
��Bold"�6.   $��14(�6A   $��Century Gothic)�6O   �� 5      -   5         ��Name$ ServiceNameTextBox��5      $��0��6a   $��
100,0,0,15q�6n    �� 
IsReadOnly . $��30��6�   $��14(�5      $��Century Gothic)�6(   
����������	#FFD4AF37$��1q�6t   ��5   6   �� *ServiceName+	 �� Mode$	 OneWay=�5   t   �� 5      $��	Дата:��6   $��1��6)   $
��Bold"�66   $��14(�6H   $��Century Gothic)�6V   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5      (  "System.Windows.Controls.DatePicker  5      6sys&clr-namespace:System;assembly=mscorlib     5      -   5        
  Name$
 
DatePicker��5      $��1��6W   $��
100,0,0,15q�6d   $��30��6x   $��14(�5      $��Century Gothic)�6+   ���	#FFD4AF37$��1q�6_      SelectedDate 5   1   �� *SelectedDate+5      �� 5      $��Время:��6   $��2��6*   $
��Bold"�67   $��14(�6I   $��Century Gothic)�6W   �� 5      -   5        $ TimeBox��5      $��2��6F   $��
100,0,0,15q�6S   $��30��6g   $��14(�5      $��Century Gothic)�6(   ���	#FFD4AF37$��1q�6\   
���������5   +   �� *	TimeInput+5       �� 5"      $��Мастер:��6   $��3��6,   $
��Bold"�69   $��14(�6K   $��Century Gothic)�6Y   �� 5#      -   5         ��Name$ MasterCombo��5#      $��3��5%      $��
100,0,0,15q�5&      $��30��5'      $��14(�5(      $��Century Gothic)�5)      ���	#FFD4AF37$��1q�5+       �� DisplayMemberPath$ FullName��5,       �� SelectedIndex$ 0��5-      ��5#   0   �� *AvailableMasters+ �� SelectedItem 5$      �� *SelectedMaster+5-      �� 50      $	��150��6   $��35��6H   $��
0,23,10,58q�6T   $��Right=�6h   $��5��6�   ��6%   �� *ReserveCommand+��51      �� 52       �� 
StartPoint$	 0,0&�6.    �� EndPoint$	 0,1&�6?   .��53      �  � Color$ 	#FFD4AF37��6+    � Offset$ 0Q�6=   � 54      $ 	#FFAA8C2D��6+   $ 1Q�6=   55      
�������$
��Bold"�58   @   $��0q�59   G   $)��#Подтвердить запись��5:   H   5;      5=      5>      �� 5@   
   -   5        $	��✕��5@      $��25��6   $��25��6(   $��Right=�64   $	��Top=�6P   $��0,5,5,0q�5A      
������ $��0q�6;   
�������$
��Bold"�5B       � Cursor$
 Hand��6#   61   5C      5D      