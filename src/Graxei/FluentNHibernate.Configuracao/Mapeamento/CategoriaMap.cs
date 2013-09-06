using FluentNHibernate.Mapping;
using Graxei.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.FluentNHibernate.Mapeamento
{
    public class CategoriaMap : ClassMap<Categoria>
    {
        public CategoriaMap()
        {
            Id(p => p.Id);
            Map(p => p.Nome).Column(Constantes.NOME);
            References(p => p.CategoriaPai).Column(Constantes.ID_CATEGORIA_PAI);
        }
    }
}
