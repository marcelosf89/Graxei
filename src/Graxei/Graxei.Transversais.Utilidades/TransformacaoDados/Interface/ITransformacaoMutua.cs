using FAST.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Transversais.Utilidades.TransformacaoDados.Interface
{
    public interface ITransformacaoMutua <TEntidade, TContrato> where TEntidade: Entidade
    {   
        TEntidade Transformar(TContrato contrato);
        TContrato Transformar(TEntidade entidade);
    }
}
