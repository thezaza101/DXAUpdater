using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
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

        [JsonProperty("UpdateDescription")]
        public string UpdateDescription {get;set;}

        [JsonProperty("UpdatedDomain")]
        public string UpdatedDomain {get;set;}

        [JsonProperty("UpdateDateTimeTicks")]
        public string UpdateDateTimeTicks {get; set;} 

        [JsonProperty("UpdatedIdentifiers", ObjectCreationHandling = ObjectCreationHandling.Replace)]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public List<string> UpdatedIdentifiers { 
            
            get{
                try{
                    if (string.IsNullOrWhiteSpace(iUpdatedIdentifiers)){iUpdatedIdentifiers="";}
                    return iUpdatedIdentifiers.Split(@"###").ToList();
                }catch{
                    return new List<string>();
                }
            }
            set{
                string s = "";
                foreach (string x in value)
                {s = s + x + "###";}
                iUpdatedIdentifiers = s;
            }
            
         }

        [JsonProperty("Active")]
        public string Active {get; set;}
        
        [JsonProperty("PayloadType")]
        public string PayloadType {get; set;}

        [JsonProperty("Payload")]
        public string Payload {get; set;}

        [JsonIgnore]
        public string iUpdatedIdentifiers {get;set;}     

          
    }
    
}