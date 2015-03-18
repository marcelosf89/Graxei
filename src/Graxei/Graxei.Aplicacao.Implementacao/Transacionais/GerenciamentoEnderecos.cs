using System;
using Graxei.Aplicacao.Contrato.Transacionais;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Transversais.ContratosDeDados;
using Graxei.Negocio.Contrato.Factories;
using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Transversais.Utilidades.Excecoes;
using System.Collections.Generic;
using Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Models;

namespace Graxei.Aplicacao.Implementacao.Transacionais
{
    public class GerenciamentoEnderecos : PadraoTransacao, IGerenciamentoEnderecos
    {
        public GerenciamentoEnderecos(IServicoEnderecos servicoEnderecos, IBairrosBuilder bairroBuilder, IConsultasLojas consultasLojas, IEnderecosBuilder enderecoBuilder)
        {
            _servicoEnderecos = servicoEnderecos;
            _bairroBuilder = bairroBuilder;
            _consultasLojas = consultasLojas;
            _enderecoBuilder = enderecoBuilder;
        }

        public Endereco Salvar(EnderecoVistaContrato enderecoVistaContrato)
        {
            IniciarTransacao();
            try
            {
                Bairro bairro = _bairroBuilder
                                .SetNome(enderecoVistaContrato.Bairro)
                                .SetCidade(enderecoVistaContrato.Cidade)
                                .SetIdEstado(enderecoVistaContrato.IdEstado)
                                .Build();
                Loja loja = _consultasLojas.Get(enderecoVistaContrato.IdLoja);
                if (loja == null)
                {
                    throw new OperacaoEntidadeException(string.Format("Loja com id {0} não pôde ser encontrada",
                                                        enderecoVistaContrato.IdLoja));
                }
                Endereco endereco = _enderecoBuilder
                                    .SetId(enderecoVistaContrato.Id)
                                    .SetLogradouro(enderecoVistaContrato.Logradouro)
                                    .SetNumero(enderecoVistaContrato.Numero)
                                    .SetComplemento(enderecoVistaContrato.Complemento)
                                    .SetLoja(loja)
                                    .SetBairro(bairro)
                                    .SetCnpj(enderecoVistaContrato.Cnpj)
                                    .SetTelefones(enderecoVistaContrato.Telefones)
                                    .Build();
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

        public void Remover(Endereco endereco)
        {
            IniciarTransacao();
            try
            {
                endereco.Excluida = true;
                endereco = _servicoEnderecos.Salvar(endereco);
                Confirmar();
            }
            catch (Exception)
            {
                Desfazer();
                throw;
            }
        }

        private IServicoEnderecos _servicoEnderecos;

        private IBairrosBuilder _bairroBuilder;

        private IConsultasLojas _consultasLojas;

        private IEnderecosBuilder _enderecoBuilder;

    }
}