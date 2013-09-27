using System.Collections.Generic;
using Graxei.Modelo;

namespace Graxei.Aplicacao.Implementacao.MVC4Unity.Areas.Administrativo.Models
{
    public class NovaLojaEnderecosModel
    {
        public NovaLojaEnderecosModel()
        {
            Loja = new Loja();
            NovosEnderecosModel = new NovosEnderecosModel();
        }
        public Loja Loja { get; set; }
        public NovosEnderecosModel NovosEnderecosModel { get; set; }

    }
}