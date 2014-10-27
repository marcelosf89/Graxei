using System;
using Graxei.Aplicacao.Contrato.Transacionais;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;

namespace Graxei.Aplicacao.Implementacao.Transacionais
{
    public class GerenciamentoEnderecos : PadraoTransacao, IGerenciamentoEnderecos
    {
        public GerenciamentoEnderecos(IServicoEnderecos servicoEnderecos)
        {
            _servicoEnderecos = servicoEnderecos;
        }

        public Endereco Salvar(Endereco endereco, Usuario usuario)
        {
            IniciarTransacao();
            try
            {
                endereco = _servicoEnderecos.Salvar(endereco);
                Confirmar();
                return endereco;
            }
            catch (Exception)
            {
                Desfazer();
                throw;
            }
        }

        private IServicoEnderecos _servicoEnderecos;
    }
}