namespace CloudDT.Models
{
    public class CodeSnippet
    {
        public string? CodeName { get; set; }

        public string? CodeLanguage { get; set; }

        public string? Description { get; set; }

        public CodeSnippet(string codeName, string codeLanguage, string description)
        {
            CodeName = codeName;
            CodeLanguage = codeLanguage;
            Description = description;
        }
    }
}