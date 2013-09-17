using FluentNHibernate.Mapping;
using Graxei.Modelo;
namespace Graxei.FluentNHibernate.Mapeamento
{
    class LojaUsuarioMap : ClassMap<LojaUsuario>
    {
        public LojaUsuarioMap()
        {
            Id(p => p.Id);
            Map(p => p.DataRegistro);
            References(p => p.Loja);
            References(p => p.Usuario);
        }
    }
}
