   M S B A M L   `   `   `  ????P  Lsemestralka_scholzova, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:semestralka_scholzova.Viewssemestralka_scholzova.Views  5   	   Q-clr-namespace:semestralka_scholzova.ViewModelsemestralka_scholzova.ViewModel  5   	   I)clr-namespace:semestralka_scholzova.Modelsemestralka_scholzova.Model  5   	   E'clr-namespace:semestralka_scholzova.VMLsemestralka_scholzova.VML  5	   	   -    'semestralka_scholzova.Views.ProgramView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ;local)clr-namespace:semestralka_scholzova.Views     5   	   C	viewModel-clr-namespace:semestralka_scholzova.ViewModel     5   	   :data)clr-namespace:semestralka_scholzova.Model     5   	   7vml'clr-namespace:semestralka_scholzova.VML     5	   	   0   *semestralka_scholzova.VML.ViewModelLocator    AutoHookedUpViewModel$
  True??5
   	   $	??800??5   	   $
??1200??6    ? 	Resources 5       ?? DataType)   #semestralka_scholzova.Model.Program ?? Orientation ?? SelectionTextBrush ? Cursor ? ForceCursor ?? SelectionBrush ?? 
AcceptsTab	 ?? AcceptsReturn
 ?? Path ?? Mode ?? 
CaretBrush ?? 
IsReadOnly??%i  (??       5   -   *, +)?? 6
   UserControl??"  6-   .R?5      ?? $ Vertical=?6   .+?5      ?? $
??1050??6K   $	??370??6X   
 ????????#283845$
{?True??6?   $ Arrow??6?   $
 True??6?   
 ?????
???????$
 True??6  $
	 True??61  ??6   ?? $
 	ReadeText?$ TwoWay=???6?   Z?  6	  Z? 61  ?? 5      $
??1050??6O   $	??170??6\   
 ????????#283845$??0,10,0,0q?6?   $ Arrow??6?   $
 True??6?   ??6   ?? $
 CustomConsole?$ OneWay=???6?   Z? 6?   ?? 5      $??Vstup??6   
???????6)   ?? 5      $
??1050??6O   $	??170??6\   
 ????????#283845$??0,10,0,0q?6?   $ Arrow??6?   
???????
 ?????$
 True??6I  ??6   ?? $
 ImportConsole?$ TwoWay=???6?   ?? $
 Color? 6?   ?? $
 Editable? 6	  Z? 6I  5      5      5      .??5      ]? $??0,0,0,0q?6   4  .System.Windows.Automation.AutomationProperties   IsOffscreenBehavior$ Onscreen=?6!   .+?5   
   ?? $	??790??6   $ Vertical=?6"   .+?5      ?? $ Vertical=?6   $	??115??62   $	??380??6>   $??Center=?6K   $	??Top=?6h   .+?5      ?? -   5         ??Name$ 	btnImport??5      
???????$??30??6a   $??Bahnschrift SemiBold)?6m   $??14(?6?   $
??Bold"?6?   $??Normal$?6?   $??90??6?   ???	#FFB7D5D4$??Import??6?   $??90??6?   $??30??6  $??90??6  $??30??6!  $??0,10,0,0q?60  ??6,   ?? *ImportCommand+60  ?? 5      -   5        $ btnSave??5      
???????$??30??6]   $??Bahnschrift SemiBold)?6i   $??14(?6?   $
??Bold"?6?   $??Normal$?6?   $??90??6?   ???	#FFB7D5D4$
??Save??6?   $??90??6?   $??90??6?   $??30??6  $??30??6  $??0,10,0,0q?6*  ??6*   ?? *SaveCommand+6*  ?? 5       -   5        $ btnClear??5       
???????$??30??6_   $??Bahnschrift SemiBold)?6k   $??14(?6?   $
??Bold"?6?   $??Normal$?6?   $??90??6?   ???	#FFB7D5D4$??Clear??6?   $??90??6?   $??90??6  $??30??6  $??30??6  $??0,10,0,0q?6-  ??6+   ?? *ClearComannd+6-  5"      ?? 5#      $ Vertical=?6   $	??405??62   $??Bottom=?6?   $??Center=?6Z   .+?5$      ?? -   5        $ btnRun1??5$      
???????$??30??6\   $??Bahnschrift SemiBold)?6h   $??14(?6?   $
??Bold"?6?   $??Normal$?6?   $??90??6?   ???	#FFB7D5D4$	??Run??6?   $??90??6?   $??90??6?   $??30??6
  $??30??6  $??Bottom=?6(  ??6*   ?? *
RunCommand+6(  5&      5'      ?? 5*   
   $
??1066??65   $	??796??6B   ???#202C39 ?? SelectedIndex$ 0??6?   ??6   ?? *
Programs+??6d   Z? ??6z   Z? 6?   5-      5/      