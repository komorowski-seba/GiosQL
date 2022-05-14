﻿using Newtonsoft.Json;

namespace Application.Models.GiosStation
{
    public class IndexLevel
    {
        [JsonProperty("id")] 
        public int Value { get; set; }
        [JsonProperty("indexLevelName")] 
        public string IndexLevelName { get; set; }
    }
}