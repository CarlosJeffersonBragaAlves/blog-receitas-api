namespace blog_receitas_api.Models
{
    public class ModoDePreparo
    {
        public int Id { get; set; }

        public string Desc { get; set; }

        public virtual Receita Receita { get; set; }
    }
}
