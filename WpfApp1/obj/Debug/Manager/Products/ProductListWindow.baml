   M S B A M L   `   `   `  ����=  9PL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:PL.ProductsPL.Products  5   	   #    PL.Products.ProductListWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:PL.Products     5   	     � Title$  ProductListWindow��5   	   $	��640��6#   $	��800��60    � DataContext 5	   	   ��  �� RelativeSource �� *Self+.��5
      � 
���   � � RowDefinitions 5   
   �� 5      $	��40* �6   �� 5      $
��500* �6   �� 5      $
��Auto �6   .+�5   
   �� -   5        
����p��
�������$��3q�5   @   
���   �$��Add new Product��6q   $��2��6�   $��Right=�6�   $��5q�6�   $��5q�6�   6�   � 5   
   $��Stretch=�6   $
��auto��6-   $��0��6;   $��Stretch=�6H   $
��auto��6d    � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $��* �6#   .+�5      �� -   5        $��Stretch=�5   f   $��Stretch=�6�   $��1��6�    �� SelectedItem 6   �� *selectedCategory+��6C   �� *
categories+6�   �� 5      $��0��6   $��Category��6$   
�������$
��Bold"�6T   �� 5      -   5        
����p�� �� Left$ 20��5      
 �� Top$ 10��6   $��without filtering��6.   $��15(�5      $��253,16,380,-16q�6   $��2��64    d� 	IsChecked 6H   �� *	filtering+	 �� Mode$	 TwoWay=�6v   5      �� 5   
   -   5        
���   �
�������$��1��5   �   ��6C   �� *productForList+
 �� SelectedValue
 6l   �� *selectedProduct+6�   5      5       