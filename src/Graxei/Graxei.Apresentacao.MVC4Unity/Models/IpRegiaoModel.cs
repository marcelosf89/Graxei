using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Graxei.Apresentacao.MVC4Unity.Models
{
    public class IpRegiaoModel
    {
        public string Pais { get; set; }
        public string Cidade { get; set; }
        public int EstadoCodigo { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is IpRegiaoModel))
            {
                return false;
            }

            IpRegiaoModel that = (IpRegiaoModel)obj;
            return this.Pais == that.Pais && this.Cidade == that.Cidade && this.EstadoCodigo == that.EstadoCodigo;
        }
    }
}