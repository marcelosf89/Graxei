using FluentNHibernate.Mapping;
using Graxei.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentNHibernate.Configuracao
{
    public class LojaMap : ClassMap<Loja>
    {
        public LojaMap()
        {
            Id(p => p.Id).Column(Constantes.ID_LOJA);
            Map(p => p.Nome).Column(Constantes.NOME);
            Map(p => p.Logotipo).Column(Constantes.LOGOTIPO);
            HasMany(p => p.Enderecos).KeyColumn(Constantes.ID_LOJA);
        }
    }
}
