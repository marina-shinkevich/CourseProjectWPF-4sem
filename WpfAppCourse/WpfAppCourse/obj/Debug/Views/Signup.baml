   M S B A M L   `   `   `  ����G  CWpfAppCourse, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:WpfAppCourseWpfAppCourse  5          WpfAppCourse.Views.Signup   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:WpfAppCourse     5        N� Title$  Signup��5	       N� 
Background �#15211B.��5      �  � RowDefinitions 5   
   �� 5      $
��589* �6   �� 5      $
��173* �6   .+�5   
   � $��	400,0,0,0q�6   $
��Left=�6"   $	��777��6=   $	��762��6I   $	��Top=�6V   $��2��6n   .+�5      �� $G��AD:\2курс4сем\картинки для всего\SignUp.jpg��6   $
��Fill=�6H   $��0,0,-83,-51q�6W   $��Right=�6l   $	��519��6�   �� 5      ���	#80000000$��341,0,-83,-51q�6)   �� 5      $I��CD:\2курс4сем\картинки для всего\лого.png��6   $
��Fill=�6F   $��	462,0,0,0q�6U   $	��262��6h   $��Center=�6u   $
��Left=�6�   $	��268��6�   5      �� 5   
   $��	YOUTH LAB��6   
�������$��Right=�68   $��
0,86,795,0q�6T   $	��Top=�6h   $��75��6�   $��Felix Titling)�6�   $��55(�6�   �� 5   
   $��SignUp��6   
�������$��239,184,795,0q�64   $	��Top=�6K   $��58��6c   $��Felix Titling)�6o   $��40(�6�   � 5   
   $��Center=�6   $
��1177��6,   $	��762��69   $	��Top=�6F   $��2��6^    5      �� 5      .+�5      �� $	��Top=�6   $��Right=�61   $��0,296,672,0q�6M   $	��495��6b    J� RenderTransformOrigin$ 
0.52,0.523&�6n   $	��269��6�   5      � 5   
   $	��814��6   $	��Top=�6   $��2��64   $
��Left=�6E   $
��1260��6`    5       �� 5!      �� 5"      $	��127 �6    .+�5$      ��    TextBoxContainerStyle#��[  6   $��0,344,914,279q�6D   ._�5%      �� .+�5&      �� -   5         ��Name$ SurnameText��5&        StyledTextBox#��[ 5'      $	��286��6:   ��5&   3   �� *	Surname+ �� UpdateSourceTrigger$ PropertyChanged=�5'   :   5)      5*      �� 5,      #��[  6   $��0,459,914,163q�6D   ._�5-      �� .+�5.      �� -   5        $ NameText��5.      #��[ 5/      ��5.   0   �� *Name+$ PropertyChanged=�5/      51      52      �� 53      #��[  6   $��0,579,914,43q�6D   ._�54      �� .+�55      �� -   5        $ 
NumberText��55      #��[ 56      ��55   2   �� *Phone+$ PropertyChanged=�56      58      59      �� 5:      #��[  6   $��0,343,581,279q�6D   ._�5;      �� .+�5<      �� -   5        $ 	EmailText��5<      #��[ 5=      ��5<   1   �� *Email+$ PropertyChanged=�5=      5?      5@      �� 5B      #��[  6   $��0,457,581,165q�6D   ._�5C      �� .+�5D      �� -   5        $ 	LoginText��5D      #��[ 5E      ��5D   1   �� *Login+$ PropertyChanged=�5E      5G      5I      �� 5L      #��[  6   $��0,579,592,43q�6D   ._�5M      �� .+�5N      �� -   5        $ PasswordText��5N      #��[ 5O      ��5N   4   �� *
Password+$ PropertyChanged=�5O      5Q      5R      �� 5T      $��Имя��6   
�������$��Century)�66   $��19(�6K   $��61,429,693,236q�6Y   �� 5U      -   5         ��Name$ 	NameError��5U      ���	#FFD9534F$
��Wrap=�6h   $��10,524,860,141q�6|   $��12(�6�   $y�Visible=�6�   ��6-   �� *Errors.NameError+6�   �� 5V      $��Фамилия��6   
�������$��Century)�6:   $��19(�6O   $��61,315,693,350q�6]   �� 5W      -   5        $ SurnameError��5W      ���	#FFD9534F$
��Wrap=�6m   $��10,408,860,257q�6�   $��12(�6�   $y�Visible=�6�   ��6/   �� *Errors.SurnameError+6�   �� 5X      $!��Номер телефона��6   
�������$��Century)�6B   $��19(�6W   $��61,546,693,119q�6e   �� 5Y      -	   5        $ 
PhoneError��5Y      ���	#FFD9534F$��12(�6g   $��23,645,985,0q�6u   $
��Wrap=�6�   $y�Visible=�6�   ��6,   �� *Errors.PhoneError+6�   �� 5Z      $��E-mail��6   
�������$��Felix Titling)�69   $��19(�6T   $��394,315,360,350q�6b   �� 5[      -
   5        $ 
EmailError��5[      ���	#FFD9534F$��465,416,360,230q�6f   $��12(�6   $
��Wrap=�6�   $y�Visible=�6�   ��6,   �� *Errors.EmailError+5]      �� 5^      $��
Логин��6   
�������$��Century)�68   $��19(�6M   $��394,429,360,236q�6[   �� 5_      -   5        $ 
LoginError��5_      $��405,522,520,126q�6P   ���	#FFD9534F$
��Wrap=�6�   $��12(�6�   $y�Visible=�6�   ��6,   �� *Errors.LoginError+6�   �� 5`      $��Пароль��6   
�������$��Century)�69   $��19(�6N   $��400,546,354,119q�6\   �� 5a      -   5        $ PasswordError��5a      ���	#FFD9534F$��488,644,520,107q�6m   $��12(�6�   $
��Wrap=�6�   $y�Visible=�6�   $��2��6�   ��6/   �� *Errors.PasswordError+z�5b      V� 5c      .~�5d      �� �� 5e      �� 5f      T� 5g      5h      5j      �� 5k      -   5         ��Name$ Button_back��5k      $��
Назад��6*   $��280,37,777,38q�5l        RoundedButtonStyle#���  5n      $��1��6=   ��5m      �� *BackCommand+5p      �� 5q      -   5        $ 	ButtonReg��5q      $*��$Зарегистрироваться��5r   	   $��280,654,777,107q�5s   	   #���  5t   	   $��2��5u   -   ��6	   �� *RegisterCommand+6-   5v      5w      5x      