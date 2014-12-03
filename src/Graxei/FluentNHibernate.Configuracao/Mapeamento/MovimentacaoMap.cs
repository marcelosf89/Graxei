using FluentNHibernate.Mapping;
using Graxei.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.FluentNHibernate.Mapeamento
{
    public class MovimentacaoMap : ClassMap<Movimentacao>
    {
        public MovimentacaoMap()
        {
            Table(Constantes.MOVIMENTACOES);
            Id(p => p.Id).GeneratedBy.Identity();
            Map(p => p.Quantidade).Column(Constantes.QUANTIDADE);
            Map(p => p.Data).Column(Constantes.DATA);
            Map(p => p.Sentido).Column(Constantes.SENTIDO);
            References(p => p.Produto).Column(Constantes.ID_PRODUTO);
            References(p => p.Endereco).Column(Constantes.ID_ENDERECO);
        }
    }
}
