using Graxei.Modelo;
using System.Collections.Generic;

namespace Graxei.Apresentacao.Areas.Administrativo.Infraestutura.Cache
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
