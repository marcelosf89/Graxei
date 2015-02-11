using Graxei.Transversais.Utilidades;
using Graxei.Transversais.Utilidades.Data;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.AlteracaoProduto
{
    public class VisitorFuncoesComVetorDeTipos : IVisitorCriacaoFuncao
    {
        [Dependency]
        public IDataSistema DataSistema { get; set; }

        public string GetResultado()
        {
            string criar = _criar.ToString();
            criar = criar.Remove(criar.Length - 1);
            criar = "SELECT";
            return string.Empty;
        }

        public string Visit(CriarProdutoVendedor funcao)
        {
            _criar.Append(string.Format("row({0}, {1}, '{2}', {3}, {4}, '{5}')::produto_criacao,",
                                        funcao.IdProduto,
                                        funcao.IdEndereco,
                                        funcao.DescricaoProdutoVendedor,
                                        funcao.Preco.ToString("0.00", CultureInfo.InvariantCulture),
                                        funcao.Usuario.Id,
                                        PostgresComum.DataValida(DataSistema.Agora)));
            string resultado = _criar.ToString();
            resultado = resultado.Remove(_criar.Length - 1);
            return string.Format("SELECT criar_produto_vendedor(array[{0}])", resultado);
        }

        public string Visit(AlterarProdutoVendedor funcao)
        {
            _alterar.Append(string.Format("row({0}, '{1}', {2}, {3}, '{4}')::produto_modificacao,",
                                          funcao.IdProdutoVendedor,
                                          funcao.DescricaoProdutoVendedor,
                                          funcao.Preco.ToString("0.00", CultureInfo.InvariantCulture),
                                          funcao.Usuario.Id,
                                          PostgresComum.DataValida(DataSistema.Agora)));
            string resultado = _alterar.ToString();
            resultado = resultado.Remove(_alterar.Length - 1);
            return string.Format("SELECT alterar_produto_vendedor(array[{0}])", resultado);
        }

        public string Visit(ExcluirProdutoVendedor funcao)
        {
            _excluir.Append(string.Format("row({0}, {1}, '{2}')::produto_exclusao,",
                                          funcao.IdProdutoVendedor,
                                          funcao.Usuario.Id,
                                          PostgresComum.DataValida(DataSistema.Agora)));
            string resultado = _excluir.ToString();
            resultado = resultado.Remove(_excluir.Length - 1);
            return string.Format("SELECT excluir_produto_vendedor(array[{0}])", resultado);
        }

        private string _resultadoChamadaFuncoes;

        private StringBuilder _criar = new StringBuilder();

        private StringBuilder _alterar = new StringBuilder();

        private StringBuilder _excluir = new StringBuilder();
    }
}
