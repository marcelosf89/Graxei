using System.Collections.Generic;
using FluentNHibernate.Utils;
using Graxei.FluentNHibernate.UnitOfWork;
using Graxei.Modelo;
using Graxei.Persistencia.Contrato;
using Graxei.Transversais.ContratosDeDados;
using NHibernate;
using NHibernate.Linq;
using System.Linq;
using NHibernate.Criterion;
using NHibernate.Transform;
using Graxei.Transversais.ContratosDeDados.TinyTypes;
using Graxei.Transversais.ContratosDeDados.Listas;

namespace Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre
{
    public class ListaLojasRepositorio : IRepositorioListaLojas
    {
        public ListaLojas Get(int pagina, int tamanhoPagina, Usuario usuario)
        {
            int total = SessaoAtual
                .QueryOver<Loja>().JoinQueryOver<Usuario>(p => p.Usuarios).Where(q => q.Id == usuario.Id).RowCount();

            IList<ListaLojasContrato> lista =
                            (from l in SessaoAtual.Query<Loja>()
                             from u in l.Usuarios
                             where u.Id == usuario.Id orderby l.Id
                             select new ListaLojasContrato()
                             {
                                 Id = l.Id,
                                 Nome = l.Nome,
                                 NomePlano = l.Plano.Nome
                             } 
                            )
                            .Skip((pagina - 1) * tamanhoPagina)
                        .Take(tamanhoPagina).ToList<ListaLojasContrato>();
            TotalElementosLista totalElementos = new TotalElementosLista(total);
            PaginaAtualLista elementoAtual = new PaginaAtualLista(pagina);
            return new ListaLojas(lista, totalElementos, elementoAtual);
        }

        public ISession SessaoAtual
        {
            get { return UnitOfWorkNHibernate.GetInstancia().SessaoAtual; }
        }
    }
}
