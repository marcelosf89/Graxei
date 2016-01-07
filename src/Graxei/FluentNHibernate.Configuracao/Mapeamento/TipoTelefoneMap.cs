using FluentNHibernate.Mapping;
using Graxei.Modelo;

namespace Graxei.FluentNHibernate.Mapeamento
{
    public class TipoTelefoneMap : ClassMap<TipoTelefone>
    {
        public TipoTelefoneMap()
        {
            Id(p => p.Id).Column(Constantes.ID_TIPO_TELEFONE);
            Table(Constantes.TIPOS_TELEFONE);
            Map(p => p.Abreviacao).Column(Constantes.ABREVIACAO); 
            Map(p => p.Nome).Column(Constantes.NOME);
        }
    }
}
