using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace MapView.Forms.Error
{
    public interface IErrorHandler
    {
        void HandleException(Exception exception);
    }
}
