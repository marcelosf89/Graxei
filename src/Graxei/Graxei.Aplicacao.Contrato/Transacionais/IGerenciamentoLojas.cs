using System.Collections.Generic;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;

namespace Graxei.Aplicacao.Contrato.Transacionais
{
    public interface IGerenciamentoLojas : ITransacional
    {
        void SalvarLoja(string nomeLoja, string loginUsuario);
        void SalvarLoja(Loja loja, Usuario usuario);
        void SalvarLoja(Loja loja, IList<Usuario> usuarios, Usuario usuario);
        void SalvarLoja(Loja loja);
        void ExcluirLoja(Loja loja);
    }

}