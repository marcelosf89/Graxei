using FluentNHibernate.Mapping;
using Graxei.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentNHibernate.Configuracao
{
    public class EnderecoMap : ClassMap<Endereco>
    {
        public EnderecoMap()
        {
            Table(Constantes.ENDERECOS);
            Id(p => p.Id).Column(Constantes.ID_ENDERECO);
            Map(p => p.Logradouro).Column(Constantes.LOGRADOURO);
            Map(p => p.Numero).Column(Constantes.NUMERO);
            Map(p => p.Complemento).Column(Constantes.COMPLEMENTO);
            References(p => p.TipoLogradouro).Column(Constantes.ID_TIPO_LOGRADOURO);
            References(p => p.Loja).Column(Constantes.ID_LOJA);
            References(p => p.Bairro).Column(Constantes.ID_BAIRRO);
            HasMany(p => p.Telefones).KeyColumn(Constantes.ID_ENDERECO);
        }
    }
}
