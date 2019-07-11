using Newtonsoft.Json;
using Repository.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Layout.Service
{
    public class Component : BaseEntity<int>
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("values")]
        public List<string> Values { get; set; }

        [JsonProperty("required")]
        public bool Required { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("childs")]
        public List<Component> Childs { get; set; }

        public Component()
        {
            this.Childs = new List<Component>();
        }
    }
}
