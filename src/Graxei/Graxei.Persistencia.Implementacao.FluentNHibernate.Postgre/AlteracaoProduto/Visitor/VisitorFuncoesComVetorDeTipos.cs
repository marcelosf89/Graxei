using Graxei.Transversais.Comum;
using Graxei.Transversais.Comum.Data;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.AlteracaoProduto.Visitor
{
    public class VisitorFuncoesComVetorDeTipos : IVisitorCriacaoFuncao
    {
        [Dependency]
        public IDataSistema DataSistema { get; set; }

        public string GetResultado()
        {
            string resultado = _selectCriar + _selectAlterar + _selectExcluir;
            if (string.IsNullOrEmpty(resultado))
            {
                return resultado;
            }

            int index = resultado.Length - 7;
            if (resultado.LastIndexOf(" UNION") == index)
            {
                resultado = resultado.Remove(index);
            }

            resultado = string.Format("SELECT * FROM ({0}) a WHERE a.id_produto_vendedor > 0", resultado);
            return resultado;
        }

        public string Visit(CriarProdutoVendedor funcao)
        {
            _criar.Append(string.Format("row({0}, {1}, '{2}', {3}, {4}, '{5}')::produto_criacao, ",
                                        funcao.IdProduto,
                                        funcao.IdEndereco,
                                        funcao.DescricaoProdutoVendedor,
                                        funcao.Preco.ToString("0.00", CultureInfo.InvariantCulture),
                                        funcao.Usuario.Id,
                                        PostgresComum.DataValida(DataSistema.Agora)));

            string select = _criar.ToString();
            select = select.Remove(select.Length - 2);
            _selectCriar = string.Format("SELECT * FROM {0}(array[{1}])", "criar_produto_vendedor", select) + " UNION ";
            return _selectCriar;

        }

        public string Visit(AlterarProdutoVendedor funcao)
        {
            _alterar.Append(string.Format("row({0}, '{1}', {2}, {3}, '{4}')::produto_modificacao, ",
                                          funcao.IdProdutoVendedor,
                                          funcao.DescricaoProdutoVendedor,
                                          funcao.Preco.ToString("0.00", CultureInfo.InvariantCulture),
                                          funcao.Usuario.Id,
                                          PostgresComum.DataValida(DataSistema.Agora)));

            _selectAlterar = Formatar(_selectAlterar, _alterar, "alterar_produto_vendedor") + " UNION ";
            return _selectAlterar;
        }

        public string Visit(ExcluirProdutoVendedor funcao)
        {
            _excluir.Append(string.Format("row({0}, {1}, '{2}')::produto_exclusao, ",
                                          funcao.IdProdutoVendedor,
                                          funcao.Usuario.Id,
                                          PostgresComum.DataValida(DataSistema.Agora)));
            _selectExcluir = Formatar(_selectExcluir, _excluir, "excluir_produto_vendedor");
            return _selectExcluir;
        }

        private string Formatar(string select, StringBuilder row, string funcao)
        {
            select = row.ToString();
            select = select.Remove(row.Length - 2);
            select = string.Format("SELECT {0}(array[{1}]) id_produto_vendedor, null", funcao, select);
            return select;
        }

        private string _resultadoChamadaFuncoes;

        private StringBuilder _criar = new StringBuilder();

        private StringBuilder _alterar = new StringBuilder();

        private StringBuilder _excluir = new StringBuilder();

        private string _selectCriar;

        private string _selectAlterar;

        private string _selectExcluir;
    }
}
