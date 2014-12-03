using FluentNHibernate.Mapping;
using Graxei.Modelo;
namespace Graxei.FluentNHibernate.Mapeamento
{
    public class UsuarioMap : ClassMap<Usuario>
    {
        public UsuarioMap()
        {
            Id(p => p.Id).GeneratedBy.Identity();
            Map(p => p.Login);
            Map(p => p.Email);
            Map(p => p.Nome);
            Map(p => p.Senha);
        }
    }
}
