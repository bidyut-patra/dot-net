using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace sample.Models
{
    public class TrainingData : Entity<Guid>
    {
        [Required]
        [JsonProperty("trainingName")]
        public string Name { get; set; }

        [Required]
        [JsonProperty("startDate")]
        public DateTime StartDate { get; set; }

        [Required]
        [JsonProperty("endDate")]
        public DateTime EndDate { get; set; }
    }
}