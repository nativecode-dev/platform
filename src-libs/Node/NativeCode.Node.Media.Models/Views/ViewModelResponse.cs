namespace NativeCode.Node.Media.Models.Views
{
    using System.Collections.Generic;

    public abstract class ViewModelResponse : ViewModel
    {
        public IList<ViewModelError> Errors { get; } = new List<ViewModelError>();

        public IList<ViewModelMessage> Messages { get; } = new List<ViewModelMessage>();

        public bool Success { get; set; }
    }

    public abstract class ViewModelResponse<T> : ViewModelResponse
    {
        public T Result { get; set; }
    }
}
