   M S B A M L   `   `   `  ����F  BCarShowroom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:CarShowroomCarShowroom  5   	   $    CarShowroom.Windows.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:CarShowroom     5   	     � Title$  Автосалон��5   	   $	��450��5	   	   $	��800��5
   	   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $	��250��5      $��18(�5      $��Center=�5      $
��Wrap=�5      ��5      ��  �� ElementName$ 	MainFrame�� �� Path$ Content.Title�5      �� 5   
   -   5         ��Name$ 
BackButton��5         Button-Style#��[  5      $��
Назад��5      $��Right=�5       � 5   
   -   5          �Name$ 	MainFrame��5      $��1��5        � NavigationUIVisibility$ Hidden=�5        � Source$) #../Pages/GeneralPages/AuthPage.xaml?�5      5       5!      