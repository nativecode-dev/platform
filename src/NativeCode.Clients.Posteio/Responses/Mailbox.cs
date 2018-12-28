namespace NativeCode.Clients.Posteio.Responses
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    public class Mailbox
    {
        [DataType(DataType.EmailAddress)]
        public string Address { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; }

        public bool Disabled { get; set; }

        [CanBeNull]
        public string Home { get; set; }

        [CanBeNull]
        public string Name { get; set; }

        [JsonProperty("redirect_only")]
        public bool RedirectOnly { get; set; }

        [JsonProperty("redirect_to")]
        public List<string> RedirectTo { get; } = new List<string>();

        [JsonProperty("super_admin")]
        public bool SuperAdmin { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Updated { get; set; }

        public string User { get; set; }
    }
}
