using System;

namespace MapView.Forms.Error
{
    public class ErrorWindowAdapter : IErrorHandler
    {
        public void HandleException(Exception exception)
        {
            using (var window = new ErrorWindow(exception))
            {
                window.ShowDialog();
            }
        }
    }
}