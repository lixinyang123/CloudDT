namespace CloudDT.ContainerAPI.Models
{
    public class CodeSnippet
    {
        public CodeSnippet()
        {
            Language = Language.Bash;
            Code = string.Empty;
        }

        public Language Language { get; set; }

        public string Code { get; set; }
    }
}
