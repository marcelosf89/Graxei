using FluentNHibernate.Mapping;
using Graxei.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentNHibernate.Configuracao
{
    public class CategoriaMap : ClassMap<Categoria>
    {
        public CategoriaMap()
        {
            Table(Constantes.CATEGORIAS);
            Id(p => p.Id).Column(Constantes.ID_CATEGORIA);
            Map(p => p.Nome).Column(Constantes.NOME);
            References(p => p.CategoriaPai).Column(Constantes.ID_CATEGORIA_PAI);
        }
    }
}
