using Graxei.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Infraestutura.Cache
{
    public interface ICacheElementosEndereco
    {
        IList<Bairro> GetBairros();

        IList<Cidade> GetCidades();
        
        IList<Logradouro> GetLogradouros();

        void SetCidades(IList<Cidade> cidades);
        
        void SetBairros(IList<Bairro> bairros);
        
        void SetLogradouros(IList<Logradouro> logradouros);

    }
}
