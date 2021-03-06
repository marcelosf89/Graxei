﻿using Graxei.Modelo.Generico;

namespace Graxei.Transversais.Comum.TransformacaoDados.Interface
{
    public interface ITransformacaoMutua <TEntidade, TContrato> where TEntidade: Entidade
    {   
        TEntidade Transformar(TContrato contrato);
        TContrato Transformar(TEntidade entidade);
    }
}
