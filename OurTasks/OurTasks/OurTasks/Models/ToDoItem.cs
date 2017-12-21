//using SQLite;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System;

namespace OurTasks
{
    public class ToDoItem
    {
        //[PrimaryKey, AutoIncrement]
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        [JsonProperty(PropertyName = "location")]
        public string Location { get; set; }

        [JsonProperty(PropertyName = "assignedTo")]
        public string AssignedTo { get; set; }

        [JsonProperty(PropertyName = "dueDate")]
        public DateTime DueDate { get; set; }

        [JsonProperty(PropertyName = "completed")]
        public bool Completed { get; set; }

        [JsonProperty(PropertyName = "recurrence")]
        public string Occurrence { get; set; } = "Once";

        [Version]
        public string Version { get; set; }
    }

}
