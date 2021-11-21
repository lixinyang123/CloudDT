using System;
using BlazorFluentUI;

namespace CloudDT.Models
{
    public class DataItem
    {
        public string? CodeName{get; set;}
        public string? CodeLanguage { get; set; }
        public string? Description { get; set; }

        public DataItem()
        {

        }

        public DataItem(string codeName,string codeLanguage,string description)
        {
            CodeName = codeName;
            CodeLanguage = codeLanguage;
            Description = description;
        }
    }
}