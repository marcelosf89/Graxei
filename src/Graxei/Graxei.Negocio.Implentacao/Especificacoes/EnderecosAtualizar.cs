using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Negocio.Contrato.Especificacoes;
using Graxei.Transversais.Idiomas;
using Graxei.Transversais.Comum.NHibernate;
using System;

namespace Graxei.Negocio.Implementacao.Especificacoes
{
    public class EnderecosAtualizar : IEspecificacao<Endereco>
    {
        public EnderecosAtualizar(IServicoEnderecos servicoEnderecos)
        {
            _servicoEnderecos = servicoEnderecos;
        }

        public ResultadoEspecificacao Satisfeita(Endereco endereco)
        {
            if (endereco == null)
            {
                throw new ArgumentNullException("endereco", "Endereço não pode ser nulo");
            }
            if (endereco.Loja == null || UtilidadeEntidades.IsTransiente(endereco.Loja))
            {
                return new ResultadoEspecificacao(false, Erros.EnderecoAssociadoLoja);
            }
            if (string.IsNullOrEmpty(endereco.Logradouro))
            {
                return new ResultadoEspecificacao(false, Erros.EnderecoDeveTerLogradouro);
            }
            if (string.IsNullOrEmpty(endereco.Numero))
            {
                return new ResultadoEspecificacao(false, Erros.EnderecoDeveTerNumero);
            }
            if (endereco.Bairro == null)
            {
                return new ResultadoEspecificacao(false, Erros.EnderecoDeveTerBairro);
            }

            if (!_servicoEnderecos.UsuarioAtualAssociado(endereco))
            {
                return new ResultadoEspecificacao(false, Erros.UsuarioSemAcessoLoja);
            }

            Endereco enderecoRepetido = _servicoEnderecos.Get(endereco.Loja.Id, endereco.Logradouro, endereco.Numero, endereco.Complemento, endereco.Bairro.Id);
            if (enderecoRepetido != null && enderecoRepetido.Id != endereco.Id)
            {
                return new ResultadoEspecificacao(false, Erros.EnderecoRepetidoLoja);
            }

            return new ResultadoEspecificacao();
        }

        private IServicoEnderecos _servicoEnderecos;
    }
}
