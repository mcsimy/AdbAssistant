   M S B A M L   `   `   `  ���� G  CAdbAssistant, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:AdbAssistantAdbAssistant      AdbAssistant.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ,localclr-namespace:AdbAssistant     -     � Title$  AdbAssistant��$	��420��$	��270��$	��270��$	��420�� � Icon$ 
saturn.ico�� � WindowStartupLocation$ CenterScreen=����	#FF641D1D.��� ���	#FF252527 � RowDefinitions �� $	��150 ��� $	��48* ��� $	��87* ��� $��50 ��� $��50 ��� $	��10* ��� $
��Auto � � ColumnDefinitions �� $
��Auto ��� $	��88* ��� $	��44* ��� $	��44* ��� $	��87* �.+��� -    ��Name$ buttonLaunch��$��Launch��$��10,4.333,5,10.333q� � ToolTip$ Launch Heart of Vegas�����	#FF935BC1$��1��$��2���� -   $ 
buttonKill��$
��Kill��$��5,4.333,5,10.333q�$2 ,Stop Heart of Vegas and accociated processes�����	#FF935BC1$��2��$��2��$��2���� -   $ buttonRestart��$��Restart��$��5,4.333,10.333,10.333q�$ Restart Heart of Vegas�����	#FF935BC1$��4��$��2���� -   $ buttonInstall��$��Install��$��	10,11,5,4q�$5 /Select build and install it to connected device�����	#FF935BC1$��1��$��4���� -   $ buttonUninstall��$��	Uninstall��$��	5,11,10,4q����	#FF935BC1$��4��$��4���� -   $ buttonGetIds��$��
Screenshot��$��10,4,5,8q����	#FF935BC1$7 1Captures screenshot and saves it to your computer��$��1��$��1���� -    ��Name$ textBoxAndroidId��$��23��$��	80,37,8,0q�$��
Android ID��$	��Top=� �� 
IsReadOnly . $��4��$��1���� -	   $ textBoxAndroidVersion��$��23��$��80,66,8.333,0q�$��Android Version��$	��Top=� . $��4��$��1���� -
   $ textBoxFireOS��$��80,94,8.333,0q�$��Fire OS�� . $	��Top=�$��23��$��1��$��4���� -   	 ��Name$	 labelAndroidId��$��
Android ID��$��	10,37,0,0q�$	��Top=����	#FFCDCDCD$��24��$
��Left=�$��1��$��62��$��2q��� -   $	 LableDevice��$��Device��$��10,8,0,0q�$	��Top=����	#FFCDCDCD$��24��$
��Left=�$��1��$��47��$��2q��� -   $	 labelAndroidOS��$��
Android OS��$��	10,67,0,0q�$	��Top=����	#FFCDCDCD$��24��$
��Left=�$��1��$��65��$��2q��� -   $	 labelFireOS��$��Fire OS��$��	10,94,0,0q����	#FFCDCDCD$
��Left=�$	��Top=�$��1��$��23��$��47��$��2q��� -   $ textBoxAPKName��$��23��$��10,26.667,8.333,0q�
 �� TextWrapping$

 Wrap=�$��APK to install��$	��Top=�$��5��$��3�� . �� -   $	 labelAPKName��$��Name of .apk file��$��10,2.667,0,0q����	#FFCDCDCD$��3��$
��Left=�$��3��$��96��$��2q�$	��Top=��� -   $ textBoxBuild��$��23��$��80,122,8.333,0q�$��Build��$	��Top=�$��1��$��4�� . �� -   $	 
labelBuild��$��Build��$��
10,121,0,0q�$	��Top=����	#FFCDCDCD$��26��$
��Left=� J� RenderTransformOrigin$ 2.573,0.641&�$��1��$��37��$��2q��� -    ��Name$ comboBoxDevice��$��4��$��1��$��80,8,8,0q�$	��Top=�$��23�� �� SelectedIndex$ 0���� -   $ buttonRefresh��$��Refresh��$��5,4,8,8q����	#FF935BC1$ Recollect IDs from device��$��2��$��1��$��2���� -   $ 
buttonPull��$
��Pull��$��5,11,5,4q����	#FF935BC1$��2��$��4��$��2���� -    ��Name$ checkBox��$��TopMost��$��4��$
��Left=�$��5,15,0,0q�$��1��$	��Top=����	#FFCDCDCD� -    �Name$ 
taskStatus�� � Minimum$ 0Q� � Maximum$	 100Q�$��3��$
��Left=�$��10��$��	10,10,0,0q�$��3��$	��Top=�$	��100��$��2�� � IsIndeterminate . $y�Hidden=�