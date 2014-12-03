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

namespace Graxei.Persistencia.Implementacao.NHibernate
{
    public class ListaLojasNHibernatePostgre : IRepositorioListaLojas
    {
        public ListaLojas Get(int pagina, int tamanhoPagina, Usuario usuario)
        {
            int total = SessaoAtual
                .QueryOver<Loja>().JoinQueryOver<Usuario>(p => p.Usuarios).Where(q => q.Id == usuario.Id).RowCount();

            IList<ListaLojasContrato> lista =
                            (from l in SessaoAtual.Query<Loja>()
                             from u in l.Usuarios
                             where u.Id == usuario.Id
                             select new ListaLojasContrato()
                             {
                                 Id = l.Id,
                                 Nome = l.Nome,
                                 NomePlano = l.Plano.Nome
                             }
                            )
                            .Skip((pagina - 1) * tamanhoPagina)
                        .Take(tamanhoPagina).ToList<ListaLojasContrato>();
            //IList<ListaLojasContrato> lista =
            //    SessaoAtual.QueryOver<Loja>()
            //        .JoinQueryOver<Usuario>(p => p.Usuarios)
            //        .Where(q => q.Id == usuario.Id)
            //        .Select(Projections.ProjectionList()
            //                .Add(Projections.Property("Id"), "Id")
            //                .Add(Projections.Property("Nome"), "Nome")
            //                .Add(Projections.Alias(Projections.Property("Nome"), "Plano"), "NomePlano"))
            //        .TransformUsing(Transformers.AliasToBean<ListaLojasContrato>())
            //        .Skip(pagina)
            //        .Take(tamanhoPagina)
            //        .List<ListaLojasContrato>();
            ListaTotalElementos totalElementos = new ListaTotalElementos(total);
            ListaElementoAtual elementoAtual = new ListaElementoAtual(pagina);
            return new ListaLojas(lista, totalElementos, elementoAtual);
        }

        public ISession SessaoAtual
        {
            get { return UnitOfWorkNHibernate.GetInstancia().SessaoAtual; }
        }
    }
}
