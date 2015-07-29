using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Graxei.Transversais.Idiomas;
using Graxei.Transversais.ContratosDeDados;
using Graxei.Modelo;
using Graxei.Apresentacao.Models;

namespace Graxei.Apresentacao.Areas.Administrativo.Models
{
    public class ListaEnderecoModel
    {
        public ListaEnderecoModel(long idLoja, Plano plano, IList<Endereco> enderecos)
        {
            IdLoja = idLoja;
            QuantidadeEndereco = plano.QuantidadeFilial;
            Enderecos = new List<EnderecoListaContrato>();
            foreach (Endereco end in enderecos)
            {
                Enderecos.Add(new EnderecoListaContrato(end.Id, end.ToString(), end.Cnpj));
            }
        }

        public ListaEnderecoModel(Loja loja)
        {
            IdLoja = loja.Id;
            QuantidadeEndereco = loja.Plano.QuantidadeProduto;
            Enderecos = new List<EnderecoListaContrato>();
            foreach (Endereco end in loja.Enderecos)
            {
                Enderecos.Add(new EnderecoListaContrato(end.Id, end.ToString(), end.Cnpj));
            }
        }

        public List<EnderecoListaContrato> Enderecos { get; set; }

        public long IdLoja { get; set; }

        public long QuantidadeEndereco { get; set; }

        public NovoEnderecoModel NovoEnderecoModel { get; set; }
    }
}
