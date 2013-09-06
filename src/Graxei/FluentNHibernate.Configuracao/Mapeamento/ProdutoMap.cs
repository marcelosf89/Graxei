using FluentNHibernate.Mapping;
using Graxei.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.FluentNHibernate.Mapeamento
{
    public class ProdutoMap : ClassMap<Produto>
    {
        public ProdutoMap()
        {
            Id(p => p.Id);
            Map(p => p.Codigo).Column(Constantes.CODIGO);
            Map(p => p.Descricao).Column(Constantes.DESCRICAO);
            Map(p => p.Preco).Column(Constantes.PRECO);
            Map(p => p.FatorConversao).Column(Constantes.FATOR_CONVERSAO);
            References(p => p.Categoria).Column(Constantes.ID_ENDERECO);
            References(p => p.Fabricante).Column(Constantes.ID_FABRICANTE);
            References(p => p.UnidadeEntrada).Column(Constantes.ID_UNIDADE_ENTRADA);
            References(p => p.UnidadeSaida).Column(Constantes.ID_UNIDADE_SAIDA);
        }
    }
}
