   M S B A M L   `   `   `  ����G  CWpfAppCourse, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:WpfAppCourseWpfAppCourse  5      $    WpfAppCourse.Views.MainReviews   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:WpfAppCourse     5        N� Title$  Reviews��5	         PrimaryStackPannel N� 
Background# �   6   .��5      � $��Right=�6   $
��1272��6'    � ColumnDefinitions 5   
   �� 5      $
��677* �6   �� 5      $
��595* �6   .+�5   
   �� $I��CD:\2курс4сем\картинки для всего\лого.png��6   $	��125��6B   $
��Left=�6N   $��612,688,0,10q�6i   $��2��6   �� 5   
   -   5         ��Name$ ReviewsListBox��5      $��
0,0,15,309q�5   
     PrimaryRewiewColor#���  5   
   $��0q�5   
   
�������$��Century)�5   
   $��16(�5   
   $��Hidden=�5   
   $��2��6<   ��5   *   �� *ReviewItems+��5      �� 5      .R�5      �� ���#1F332B �� CornerRadius$ 12��5       �� Padding$ 12q�5      $��0 0 0 12q�5       ���#3C5F4A$��1q�5"        
DropShadow J� Effect# �  5#      ._�5$      �� .+�5%      �� $
��Bold"�5&      ���#A2E4B8$��14(�5(      $��0 0 0 4q�5)      ��5%   (   �� *
UserName+5)      �� 5*      $
��Wrap=�5+       $��15(�5,       
�������$��0 0 0 6q�5.       ��5*   (   �� *Text+5.       �� 50      $��12(�51       
�������$��Right=�53       ��50   (   �� *	CreatedAt+ �� StringFormat$ dd.MM.yyyy HH:mm��53       54      55      56      58      �� 5:   
   $��22,510,22,156q�6   ���#2C4A39$ 10��5<      $��2��6   ._�5>      �� -   5         ��Name$ FeedbackTextBox��5>      $��18(�5?      $��Century)�5@      
������ $��0q�5C      	 �� TextWrapping$
	 Wrap=�5D      
 �� AcceptsReturn
 . " �� VerticalScrollBarVisibility$
 Auto=�5F      $��Stretch=�5G      $��Stretch=�5H      $��10q�5I      ���#A0A0A0��5>   /   �� *FeedbackText+ �� UpdateSourceTrigger$ PropertyChanged=�5K      5P      �� 5S   
   $ 10��6   ���#2C4A39$��	44,0,0,35q�68   $	��166��5T      $��66��6   $
��Left=�6   $��Bottom=�68   ._�5U      �� -   5         ��Name$ ButtHome��5U      $��На главную��6'   
������ $��0q�5W   	   $��16(�5X   	   $��Century)�5Y   	   
�������$��-3,0,3,0q�5Z      6.   5\      �� 5^   
   $ 10��6   ���#2C4A39$��	0,0,43,35q�68   $	��157��5_      $��58��6   $��Right=�6   $��Bottom=�69   $��1��6T   ._�5`      �� $��Отправить��6   
������ $��0q�5b   	   $��16(�5c   	   $��Century)�5d   	   
�������$��5,5,5,5q�5e      ��5`   )   �� *SubmitReviewCommand+5e      5g      5h      5i      