namespace blog_receitas_api.Models
{
    public class Ingrediente
    {
        public int Id { get; set; }

        public string Desc  { get; set; }

        public string Quatidade { get; set; }

        public virtual Receita Receita { get; set; }
    }
}
