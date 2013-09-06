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
            Id(p => p.Id);
            Map(p => p.Quantidade);
            Map(p => p.Data);
            Map(p => p.Sentido);
            References(p => p.Produto);
            References(p => p.Endereco);
        }
    }
}
