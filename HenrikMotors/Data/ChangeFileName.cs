namespace ChangeFileName
{
    using System;
    using System.Collections.Generic;
    using System.Net;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class SortFileName
    {
        [JsonProperty("caption")]
        public string Caption { get; set; }

        [JsonProperty("filename")]
        public string Filename { get; set; }

        [JsonProperty("key")]
        public int Key { get; set; }

        [JsonProperty("extra")]
        public Extra Extra { get; set; }
    }

    public partial class Extra
    {
        [JsonProperty("fileName")]
        public string FileName { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
