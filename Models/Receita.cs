using System.Collections.Generic;

namespace blog_receitas_api.Models
{
    public class Receita
    {
        public int Id { get; set; }

        public string UrlImg { get; set; }

        public int destaque { get; set; }

        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string Time { get; set; }

        public string Portions { get; set; }

        public string difficulty { get; set; }

        public int TipoId { get; set; }
        public virtual Tipo Tipo { get; set; }

        public int StatusId { get; set; }
        public virtual Status Status { get; set; }

        public virtual List<Ingrediente> Ingredientes { get; set; }
        public virtual List<ModoDePreparo> ModoDePreparos { get; set; }

    }
}
