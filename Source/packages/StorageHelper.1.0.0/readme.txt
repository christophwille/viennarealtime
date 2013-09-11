StorageHelper 1.0.0 | Rajen Kishna
===============================================================================

StorageHelper is a helper class for Windows Phone and Windows 8 apps to easily 
save, load and delete data from storage.

The Windows Phone methods save to the local storage of the device by default, 
as this is currently the only API available, while the Windows 8 methods 
default to roaming storage. You can point to local storage on Windows 8 by 
using the overloads of the methods outlined below.

Any questions/feedback/suggestion, let me know on Twitter:
@rajen_k

===============================================================================
Usage:
===============================================================================

Windows Phone 7 (synchronous):
-------------------------------------------------------------------------------
Application state (temporary):
------------------------------
Storage.SaveState("MYKEY", MYOBJ);
Storage.LoadState<TYPE>("MYKEY");
Storage.DeleteState("MYKEY");

Persistent storage:
-------------------
Storage.Save("MYKEY", MYOBJ);
Storage.Load<TYPE>("MYKEY");
Storage.Delete("MYKEY");
-------------------------------------------------------------------------------

Windows Phone 8 (asynchronous/awaitable):
-------------------------------------------------------------------------------
Application state (temporary):
------------------------------
Storage.SaveState("MYKEY", MYOBJ);
Storage.LoadState<TYPE>("MYKEY");
Storage.DeleteState("MYKEY");

Persistent storage:
-------------------
[await] Storage.SaveAsync("MYKEY", MYOBJ);
[await] Storage.LoadAsync<TYPE>("MYKEY");
[await] Storage.DeleteAsync("MYKEY");
-------------------------------------------------------------------------------

Windows 8 (asynchronous/awaitable):
-------------------------------------------------------------------------------
Persistent storage:
-------------------
[await] Storage.SaveAsync("MYKEY", MYOBJ);
[await] Storage.SaveAsync(ApplicationData.Current.LocalFolder, "MYKEY", MYOBJ);
[await] Storage.LoadAsync<TYPE>("MYKEY");
[await] Storage.LoadAsync<TYPE>(ApplicationData.Current.LocalFolder, "MYKEY");
[await] Storage.DeleteAsync("MYKEY");
[await] Storage.DeleteAsync(ApplicationData.Current.LocalFolder, "MYKEY");
-------------------------------------------------------------------------------