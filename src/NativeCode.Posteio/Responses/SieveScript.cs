namespace NativeCode.Posteio.Responses
{
    using System.ComponentModel.DataAnnotations;

    public class SieveScript
    {
        [DataType(DataType.MultilineText)]
        public string Script { get; set; }
    }
}
