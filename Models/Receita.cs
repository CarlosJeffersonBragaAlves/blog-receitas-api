namespace blog_receitas_api.Models
{
    public class Receita
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string Time { get; set; }

        public string Portions { get; set; }

        public int TipoId { get; set; }
        public virtual Tipo Tipo { get; set; }
    }
}
