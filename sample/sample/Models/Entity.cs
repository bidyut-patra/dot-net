using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sample.Models
{
    public class Entity<T>
    {
        [JsonProperty("id")]
        public T Id { get; set; }
    }
}