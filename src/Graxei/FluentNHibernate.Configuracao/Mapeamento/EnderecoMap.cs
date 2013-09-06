using FluentNHibernate.Mapping;
using Graxei.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.FluentNHibernate.Mapeamento
{
    public class EnderecoMap : ClassMap<Endereco>
    {
        public EnderecoMap()
        {
            Id(p => p.Id).Column(Constantes.ID_ENDERECO);
            Map(p => p.Logradouro);
            Map(p => p.Numero);
            Map(p => p.Complemento);
            References(p => p.TipoLogradouro);
            References(p => p.Loja);
            References(p => p.Bairro);
            HasMany(p => p.Telefones).KeyColumn(Constantes.ID_ENDERECO);
        }
    }
}
