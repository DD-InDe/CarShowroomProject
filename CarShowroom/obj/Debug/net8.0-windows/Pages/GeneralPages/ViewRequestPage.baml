   M S B A M L   `   `   `  ����F  BCarShowroom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:CarShowroom.PagesCarShowroom.Pages  5      '    !CarShowroom.Pages.ViewRequestPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:CarShowroom.Pages     5      -   5          N� Title$  Заявки��5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��0��5      .+�5      �� $��Поиск:��6      TextBlock-Style#��[  5      $��Center=�5      �� 5      -   5         ��Name$ SearchTextBox��5        TextBox-Style#��[ 5      $��Center=�5      �� 5      $��Статус:��6   $��Center=�5      #��[  5      �� 5      -   5         ��Name$ StatusComboBox��5       �� DisplayMemberPath$
 Name��5        ComboBox-Style#��[ 5!      $��Center=�5"      5#      &   System.Windows.Controls.DataGrid  5%   
   -   5          Name$ RequestDataGrid��5%      $��1��5&         AutoGenerateColumns$ False��5'      $��Stretch=�5(         
IsReadOnly$
 True��5)      	   Columns	 5*      0  *System.Windows.Controls.DataGridTextColumn  5+      ,  &System.Windows.Controls.DataGridColumn
   Width5  /System.Windows.Controls.DataGridLengthConverter$
 * 6%      Header$ Клиент��5-   %   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5,   %   �� *Customer.FullName+5-   %     5.      $
 * 6%   $ Сотрудник��50   %    5/   %   �� *Employee.FullName+50   %     51      $
 * 6%   $ Дата создания��53   %    52   %   �� *
DateString+53   %     54      $
 * 6%   $ Статус��56   %    55   %   �� *Status.Name+56   %   4  .System.Windows.Controls.DataGridTemplateColumn  57      $
 * 6)   $ Авто��58   )      CellTemplate 59      �� 5:      .R�5;      �� ��5<   "   `� 5=   &    �� StringFormat$ {0} {1}��63   .1�5>   *   ��  �� Path$ Car.Model.Brand.Name�62   �� 5?   *   $ Car.Model.Name�62   5@   '   5B      5C      5E        5F      $
 * 6)    5G      �� 5H      .R�5I      ��  ��Name$ 
InfoButton��6%   -   5        $��Подробнее��5J   %   $��0q�5K   %     Button-Style#��[ 5L   %   5M   %   5N      5P      5R      5S      5T      