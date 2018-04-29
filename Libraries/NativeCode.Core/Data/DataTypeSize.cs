namespace NativeCode.Core.Data
{
    public static class DataTypeSize
    {
        public const int Domain = 256;

        public const int Email = 1024;

        public const int Endpoint = 2048;

        public const int Phone = 32;

        public const int PostalCode = 32;

        public const int Url = 2048;

        public static class Identifier
        {
            public const int Normal = 256;

            public const int Short = 128;

            public const int Tiny = 64;
        }

        public static class Name
        {
            public const int Normal = 128;

            public const int Short = 64;

            public const int Tiny = 32;
        }

        public static class Text
        {
            public const int Normal = 4096;

            public const int Short = 2048;

            public const int Tiny = 1024;
        }
    }
}
