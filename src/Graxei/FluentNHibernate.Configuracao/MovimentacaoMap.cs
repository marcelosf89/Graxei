using FluentNHibernate.Mapping;
using Graxei.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentNHibernate.Configuracao
{
    public class MovimentacaoMap : ClassMap<Movimentacao>
    {
        public MovimentacaoMap()
        {
            Id(p => p.Id).Column(Constantes.ID_MOVIMENTACAO);
            Map(p => p.Quantidade).Column(Constantes.QUANTIDADE);
            Map(p => p.Data).Column(Constantes.DATA);
            Map(p => p.Sentido).Column(Constantes.SENTIDO);
            References(p => p.Produto).Column(Constantes.ID_PRODUTO);
            References(p => p.Endereco).Column(Constantes.ID_ENDERECO);
        }
    }
}
