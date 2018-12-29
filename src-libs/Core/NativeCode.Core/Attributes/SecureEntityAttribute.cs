namespace NativeCode.Core.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.All)]
    public sealed class SecureEntityAttribute : Attribute
    {
        public SecureEntityAttribute(string name)
        {
            this.Name = name;
        }

        public string Name { get; }
    }
}
