using FluentNHibernate.Mapping;
using Graxei.Modelo;
namespace Graxei.FluentNHibernate.Mapeamento
{
    class LojaUsuarioMap : ClassMap<LojaUsuario>
    {
        public LojaUsuarioMap()
        {
            Table(Constantes.LOJAS_USUARIOS);
            Id(p => p.Id);
            Map(p => p.DataRegistro, Constantes.DATA_REGISTRO);
            References(p => p.Loja);
            References(p => p.Usuario);
            References(p => p.UsuarioLog).Column(Constantes.ID_USUARIO_LOG);
        }
    }
}
