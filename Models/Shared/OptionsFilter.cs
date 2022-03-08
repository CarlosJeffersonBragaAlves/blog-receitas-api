namespace blog_receitas_api.Models.Shared
{
    public class OptionsFilter
    {
        public int Page { get; set; } = 1;

        public int Size { get; set; } = 10;

        public int Status { get; set; } = 0;

        public string Filter { get; set; } = "";

        public string Time { get; set; } = "";

        public string Portions { get; set; } = "";

        public int Tipo { get; set; } = 0;

        public int Limit { get; set; } = 20;
    }
}
