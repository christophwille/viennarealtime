ReviewNotifier 1.0.0 | Rajen Kishna
===============================================================================

ReviewNotifier is a helper class for Windows Phone and Windows 8 apps to easily
ask users to rate and review your app after a number of launches.

Any questions/feedback/suggestion, let me know on Twitter:
@rajen_k

===============================================================================
Usage:
===============================================================================

1. In the launch event of your App.xaml.cs, insert the following three lines of
code.
-------------------------------------------------------------------------------
Windows Phone 7:
-------------------------------------------------------------------------------
private void Application_Launching(object sender, LaunchingEventArgs e)
{
    ReviewNotification.Initialize();

    // Rest of your Application_Launching method
    ...
-------------------------------------------------------------------------------
Windows Phone 8:
-------------------------------------------------------------------------------
private async void Application_Launching(object sender, LaunchingEventArgs e)
{
    await ReviewNotification.InitializeAsync();

    // Rest of your Application_Launching method
    ...
-------------------------------------------------------------------------------
Windows 8:
-------------------------------------------------------------------------------
protected async override void OnLaunched(LaunchActivatedEventArgs args)
{
    await ReviewNotification.InitializeAsync();

    // Rest of your OnLaunched method
    ...
-------------------------------------------------------------------------------

2. In the Loaded event of your first page, use the following syntax. The count 
will specify how many launches are needed before the user is presented with a 
review notification reminder (default 5).
-------------------------------------------------------------------------------
Windows Phone 7 (message, title, count):
-------------------------------------------------------------------------------
ReviewNotification.Trigger("MESSAGE", "TITLE");
ReviewNotification.Trigger("MESSAGE", "TITLE", 5);
-------------------------------------------------------------------------------
Windows Phone 7 (message, title, count):
-------------------------------------------------------------------------------
ReviewNotification.TriggerAsync("MESSAGE", "TITLE");
ReviewNotification.TriggerAsync("MESSAGE", "TITLE", 5);
-------------------------------------------------------------------------------
Windows 8 (message, title, ok button, cancel button, count):
-------------------------------------------------------------------------------
ReviewNotification.TriggerAsync("MESSAGE", "TITLE");
ReviewNotification.TriggerAsync("MESSAGE", "TITLE", 5);
ReviewNotification.TriggerAsync("MESSAGE", "TITLE", "OK", "CANCEL");
ReviewNotification.TriggerAsync("MESSAGE", "TITLE", "OK", "CANCEL", 5);
-------------------------------------------------------------------------------