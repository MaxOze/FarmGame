using System;

namespace Generics
{
       public class Logger
       {
              public event Action<string> Notify;

              public void OnNotify(string message)
              {
                     Notify?.Invoke($"[{DateTime.Now:G}] >> " + message);
              }

              public int Count()
              {
                     if (Notify != null) 
                            return Notify.GetInvocationList().Length;
                     return 0;
              }
       }
}

