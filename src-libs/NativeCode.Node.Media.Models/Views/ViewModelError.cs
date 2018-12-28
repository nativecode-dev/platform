namespace NativeCode.Node.Media.Models.Views
{
    using System;

    public class ViewModelError : ViewModelMessage
    {
        public string ExceptionSource { get; set; }

        public string ExceptionStackTrace { get; set; }

        public string ExceptionType { get; set; }

        public static ViewModelError FromException(Exception exception)
        {
            return new ViewModelError
                       {
                           Message = exception.Message,
                           ExceptionSource = exception.Source,
                           ExceptionStackTrace = exception.StackTrace,
                           ExceptionType = exception.GetType()
                               .FullName,
                       };
        }
    }
}
