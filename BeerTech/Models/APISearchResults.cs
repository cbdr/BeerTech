using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeerTech.Models
{
    [Serializable]
    public class Images
    {
        public string icon { get; set; }
        public string medium { get; set; }
        public string large { get; set; }
    }
    [Serializable]
    public class Labels
    {
        public string icon { get; set; }
        public string medium { get; set; }
        public string large { get; set; }
    }
    [Serializable]
    public class Glass
    {
        public int id { get; set; }
        public string name { get; set; }
        public string createDate { get; set; }
    }
    [Serializable]
    public class Available
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }
    [Serializable]
    public class Srm
    {
        public int id { get; set; }
        public string name { get; set; }
        public string hex { get; set; }
    }
    [Serializable]
    public class Datum
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string website { get; set; }
        public string established { get; set; }
        public string isOrganic { get; set; }
        public Images images { get; set; }
        public string status { get; set; }
        public string statusDisplay { get; set; }
        public string createDate { get; set; }
        public string updateDate { get; set; }
        public string type { get; set; }
        public string abv { get; set; }
        public int? glasswareId { get; set; }
        public int? styleId { get; set; }
        public Labels labels { get; set; }
        public Glass glass { get; set; }
        public Style style { get; set; }
        public string ibu { get; set; }
        public int? availableId { get; set; }
        public Available available { get; set; }
        public int? srmId { get; set; }
        public string servingTemperature { get; set; }
        public string servingTemperatureDisplay { get; set; }
        public Srm srm { get; set; }
        public int? year { get; set; }
    }
    [Serializable]
    public class SearchResults
    {
        public int currentPage { get; set; }
        public int numberOfPages { get; set; }
        public int totalResults { get; set; }
        public List<Datum> data { get; set; }
        public string status { get; set; }
    }
}