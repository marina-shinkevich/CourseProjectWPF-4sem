   M S B A M L   `   `   `  ����   WpfAppCourse*    $WpfAppCourse.Views.AppointmentWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$*  $Запись на процедуру��$	��350��$	��450�� � WindowStartupLocation$ CenterScreen=� � 
ResizeMode$ NoResize=� � WindowStyle$
 None=� � AllowsTransparency . 
������ .��� .+��� 
������� �� CornerRadius$ 15�����	#FFD4AF37$��2q�._�� $��25q� � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��* �.+��� $��Процедура:��$
��Bold"�$��14(�$��Century Gothic)��� -    ��Name$ ServiceNameTextBox��$��0��$��
100,0,0,15q� �� 
IsReadOnly . $��30��$��14(�$��Century Gothic)�
����������	#FFD4AF37$��1q����� *ServiceName+	 �� Mode$	 OneWay=��� $��	Дата:��$��1��$
��Bold"�$��14(�$��Century Gothic)�O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem (  "System.Windows.Controls.DatePicker  6sys&clr-namespace:System;assembly=mscorlib     -   
  Name$
 
DatePicker��$��1��$��
100,0,0,15q�$��30��$��14(�$��Century Gothic)����	#FFD4AF37$��1q�   SelectedDate �� *SelectedDate+�� $��Время:��$��2��$
��Bold"�$��14(�$��Century Gothic)��� -   $ TimeBox��$��2��$��
100,0,0,15q�$��30��$��14(�$��Century Gothic)����	#FFD4AF37$��1q�
����������� *	TimeInput+�� $��Мастер:��$��3��$
��Bold"�$��14(�$��Century Gothic)��� -    ��Name$ MasterCombo��$��3��$��
100,0,0,15q�$��30��$��14(�$��Century Gothic)����	#FFD4AF37$��1q� �� DisplayMemberPath$ FullName�� �� SelectedIndex$ 0������ *AvailableMasters+ �� SelectedItem �� *SelectedMaster+�� $	��150��$��35��$��
0,23,10,58q�$��Right=�$��5������ *ReserveCommand+����  �� 
StartPoint$	 0,0&� �� EndPoint$	 0,1&�.���  � Color$ 	#FFD4AF37�� � Offset$ 0Q�� $ 	#FFAA8C2D��$ 1Q�
�������$
��Bold"�$��0q�$)��#Подтвердить запись���� -   $	��✕��$��25��$��25��$��Right=�$	��Top=�$��0,5,5,0q�
������ $��0q�
�������$
��Bold"� � Cursor$
 Hand��