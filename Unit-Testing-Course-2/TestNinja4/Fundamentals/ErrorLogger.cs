
using System;

namespace TestNinja4.Fundamentals
{
    public class ErrorLogger
    {
        public string LastError { get; set; }

        public event EventHandler<Guid> ErrorLogged; 
        
        public void Log(string error)
        {
            // null
            // ""
            // " "
            if (String.IsNullOrWhiteSpace(error))
                throw new ArgumentNullException();

            LastError = error;

            // Write the log to a storage
            // ...

            ErrorLogged?.Invoke(this, Guid.NewGuid());
        }
    }
}