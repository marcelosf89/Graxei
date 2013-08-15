using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Modelo
{

    public class Produto
    {

        public virtual string Codigo { get; set; }
        public virtual string Descricao { get; set; }
        public virtual double FatorConversao { get; set; }
        public virtual Categoria Categoria { get; set; }
        public virtual UnidadeMedida UnidadeEntrada { get; set; }
        public virtual UnidadeMedida UnidadeSaida { get; set; }

    }

}
