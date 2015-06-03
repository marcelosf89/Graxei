using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.Comum.TransformacaoDados.Interface;
using System.Collections.Generic;
using System.IO;

namespace Graxei.Aplicacao.Implementacao.Consultas
{
    public class ConsultasLojas : IConsultasLojas
    {
        public ConsultasLojas(IServicoLojas servicoLojas, IServicoEnderecos servicoEnderecos, IServicoTelefones servicoTelefones, ITransformacaoMutua<Loja, LojaContrato> transformacao)
        {
            _servicoLojas = servicoLojas;
            _servicoEnderecos = servicoEnderecos;
            _transformacao = transformacao;
        }

        public Loja GetComEnderecosPlanos(long id)
        {
            return _servicoLojas.GetComEnderecosPlanos(id);
        }

        public Loja Get(long id)
        {
            return _servicoLojas.GetPorId(id);
        }

        public LojaContrato GetComEnderecos(long id)
        {
            Loja loja = _servicoLojas.GetComEnderecos(id);
            return _transformacao.Transformar(loja);
        }

        public Plano GetPlano(long idLoja)
        {
            return _servicoLojas.GetPlano(idLoja);
        }

        public Loja GetPorNome(string nome)
        {
            return _servicoLojas.Get(nome);
        }

        public Loja GetPorUrl(string nome)
        {
            return _servicoLojas.GetPorUrl(nome);
        }

        public long GetIdDoUnicoEndereco(long idLoja)
        {
            IList<long> enderecos = _servicoLojas.GetIdsEnderecos(idLoja);
            if (enderecos == null || enderecos.Count > 1)
            {
                return 0;
            }

            return enderecos[0];
        }

        public byte[] GetLogo(int idLoja, string caminhoImagem)
        {
            Loja loja = _servicoLojas.GetPorId(idLoja);
            DirectoryInfo dir = new DirectoryInfo(Path.Combine(caminhoImagem, loja.Id.ToString()));
            if (!dir.Exists)
                dir.Create();

            FileInfo[] files = dir.GetFiles("l.*");
            if (files.Length == 1)
            {
                return System.IO.File.ReadAllBytes(files[0].FullName);
            }
            return null;
        }

        public byte[] GetImageBackground(int idLoja, string caminhoImagem)
        {
            Loja loja = _servicoLojas.GetPorId(idLoja);
            DirectoryInfo dir = new DirectoryInfo(Path.Combine(caminhoImagem, loja.Id.ToString()));
            if (!dir.Exists)
                dir.Create();

            FileInfo[] files = dir.GetFiles("bg.*");
            if (files.Length == 1)
            {
                return System.IO.File.ReadAllBytes(files[0].FullName);
            }
            return null;
        }

        public Endereco GetEnderecoComTelefones(long idEndereco)
        {
            return _servicoLojas.GetEnderecoComTelefones(idEndereco);
        }

        private IServicoLojas _servicoLojas;
        private IServicoEnderecos _servicoEnderecos;
        private ITransformacaoMutua<Loja, LojaContrato> _transformacao;

        
    }
}