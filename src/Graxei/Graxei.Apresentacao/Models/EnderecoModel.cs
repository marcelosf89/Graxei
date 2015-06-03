using Graxei.Modelo;
using Graxei.Transversais.ContratosDeDados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Graxei.Apresentacao.Models
{
    public class EnderecoModel
    {
        public virtual Endereco Endereco { get; set; }

        public string PartialRender
        {
            get
            {
                if (Endereco == null)
                {
                    return "NaoCadastrada";
                }

                return "Cadastrada";
            }
        }


        public override string ToString()
        {
            return Endereco.ToString();
        }
    }
}