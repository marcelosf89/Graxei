using FluentNHibernate.Mapping;
using Graxei.Modelo;

namespace Graxei.FluentNHibernate.Mapeamento
{
    public class EnderecoMap : ClassMap<Endereco>
    {
        public EnderecoMap()
        {
            Id(p => p.Id).Column(Constantes.ID_ENDERECO);
            Map(p => p.Logradouro).Column(Constantes.LOGRADOURO);
            Map(p => p.Numero).Column(Constantes.NUMERO);
            Map(p => p.Complemento).Column(Constantes.COMPLEMENTO);
            References(p => p.Loja);
            References(p => p.Bairro).Fetch.Join();
            HasMany(p => p.Telefones).KeyColumn(Constantes.ID_ENDERECO);
        }
    }
}
