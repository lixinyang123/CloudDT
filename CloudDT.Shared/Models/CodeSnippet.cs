using System;

namespace CloudDT.Models
{
    public class CodeSnippet
    {
        public Guid? Id { get; set; }

        public string? Name { get; set; }

        public string? Language { get; set; }

        public string? Description { get; set; }
    }
}