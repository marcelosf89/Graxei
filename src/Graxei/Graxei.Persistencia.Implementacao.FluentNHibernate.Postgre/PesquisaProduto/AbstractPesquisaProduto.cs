using Graxei.FluentNHibernate.UnitOfWork;
using Graxei.Persistencia.Contrato.PesquisaProduto;
using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.ContratosDeDados.Listas;
using Graxei.Transversais.ContratosDeDados.TinyTypes;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.PesquisaProduto
{
    public abstract class AbstractPesquisaProduto : IPesquisaProdutoRepositorio
    {
        public AbstractPesquisaProduto(string criterio)
        {
            _criterio = criterio;
        }

        public ListaPesquisaContrato GetUltimaPagina(int tamanhoPagina)
        {
            string descricaoTrim = _criterio.Replace(" ", "");
            double criterioSimilaridade = descricaoTrim.Length * 0.0074;

            string sql = @"
                select count(p.id_produto) from produtos p 
                  join produtos_vendedores pv on p.id_produto = pv.id_produto
                 where similarity(p.descricao || ' ' || p.codigo,:descricao)  > :val";
            long total = SessaoAtual.CreateSQLQuery(sql)
              .SetParameter<String>("descricao", descricaoTrim)
              .SetParameter<double>("val", criterioSimilaridade)
              .UniqueResult<long>();

            int ultimaPagina = Convert.ToInt32(total / tamanhoPagina);

            IList<PesquisaContrato> lista = Get(ultimaPagina);

            TotalElementosLista totalElementos = new TotalElementosLista(total);
            PaginaAtualLista elementoAtual = new PaginaAtualLista(ultimaPagina);
            return new ListaPesquisaContrato(lista, totalElementos, elementoAtual);
        }

        public abstract IList<PesquisaContrato> Get(int pagina);

        public ISession SessaoAtual
        {
            get
            {
                if (_sessao == null)
                {
                    _sessao = UnitOfWorkNHibernate.GetInstancia().SessaoAtual;
                }

                return _sessao;
            }
            set
            {
                _sessao = value;
            }
        }

        protected string _criterio;

        private ISession _sessao;
    }
}
