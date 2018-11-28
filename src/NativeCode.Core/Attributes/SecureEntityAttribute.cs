namespace NativeCode.Core.Attributes
{
    using System;

    public sealed class SecureEntityAttribute : Attribute
    {
        public SecureEntityAttribute(string name)
        {
            this.Name = name;
        }

        public string Name { get; }
    }
}