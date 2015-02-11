using Graxei.Transversais.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.AlteracaoProduto
{
    public class VisitorFuncoesComVetorDeTipos
    {
        public string ResultadoChamadaFuncoes
        {
            get
            {
                return _resultadoChamadaFuncoes;
            }
        }

        public void Visit(CriarProdutoVendedor funcao)
        {
            _criar.Append(string.Format("row({0}, {1}, '{2}', {3}, {4}, '{5}')",
                                        funcao.IdProduto,
                                        funcao.IdEndereco,
                                        funcao.DescricaoProdutoVendedor,
                                        funcao.Preco,
                                        funcao.Usuario.Id,
                                        PostgresComum.DataValida(DateTime.Now)));
        }

        public void Visit(AlterarProdutoVendedor funcao)
        {
            _alterar.Append(string.Format("row({0}, '{1}', {2}, {3}, '{4}'))",
                                          funcao.IdProdutoVendedor,
                                          funcao.DescricaoProdutoVendedor,
                                          funcao.Preco,
                                          funcao.Usuario.Id,
                                          PostgresComum.DataValida(DateTime.Now)));
        }

        public void Visit(ExcluirProdutoVendedor funcao)
        {

        }

        private string _resultadoChamadaFuncoes;

        private StringBuilder _criar = new StringBuilder();

        private StringBuilder _alterar = new StringBuilder();

        private StringBuilder _excluir = new StringBuilder();
    }
}
