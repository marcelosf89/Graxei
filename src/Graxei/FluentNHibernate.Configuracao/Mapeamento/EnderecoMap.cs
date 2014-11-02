using FluentNHibernate.Mapping;
using Graxei.Modelo;

namespace Graxei.FluentNHibernate.Mapeamento
{
    public class EnderecoMap : ClassMap<Endereco>
    {
        public EnderecoMap()
        {
            Id(p => p.Id);
            Map(p => p.Logradouro);
            Map(p => p.Numero);
            Map(p => p.Complemento);
            Map(p => p.Excluida);
            References(p => p.Loja);
            References(p => p.Bairro).Cascade.SaveUpdate().Fetch.Join();
            HasMany(p => p.Telefones).KeyColumn(Constantes.ID_ENDERECO).Cascade.SaveUpdate().Fetch.Join();
            Where("excluida = false");
        }
    }
}
