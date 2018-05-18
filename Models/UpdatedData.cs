using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using DXANET;
using System.ComponentModel.DataAnnotations;

namespace DXAUpdater.Models
{
    public class UpdatedData
    {

        [Key]
        [JsonProperty("DataID")]
        public string DataID {get;set;}
        
        [JsonProperty("NextDataID")]
        public string NextDataID {get;set;}
        
        [JsonProperty("Payload")]
        public List<DataElement> Payload {get; set;}

        
    }
}