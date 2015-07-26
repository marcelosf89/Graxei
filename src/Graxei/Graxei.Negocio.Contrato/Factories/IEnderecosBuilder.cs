using Graxei.Transversais.ContratosDeDados;
using System;
using System.Collections.Generic;
namespace Graxei.Negocio.Contrato.Factories
{
    public interface IEnderecosBuilder
    {
        Graxei.Modelo.Endereco Build();

        IEnderecosBuilder SetBairro(Graxei.Modelo.Bairro bairro);

        IEnderecosBuilder SetCnpj(string cnpj);

        IEnderecosBuilder SetComplemento(string complemento);

        IEnderecosBuilder SetId(long id);

        IEnderecosBuilder SetLogradouro(string logradouro);

        IEnderecosBuilder SetLoja(Graxei.Modelo.Loja loja);

        IEnderecosBuilder SetNumero(string numero);

        IEnderecosBuilder SetTelefones(IList<TelefoneContrato> telefones);
    }
}
