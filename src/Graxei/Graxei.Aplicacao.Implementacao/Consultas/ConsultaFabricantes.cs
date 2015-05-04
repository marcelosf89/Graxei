using Graxei.Aplicacao.Contrato;
using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using System.Collections.Generic;
using System.IO;

namespace Graxei.Aplicacao.Implementacao.Consultas
{
    public class ConsultaFabricantes : IConsultaFabricantes
    {
        public ConsultaFabricantes(IServicoFabricantes servicoFabricantes)
        {
            ServicoFabricantes = servicoFabricantes;
        }

        public IServicoFabricantes ServicoFabricantes { get; private set; }

        public IList<string> TodosNomes()
        {
            return ServicoFabricantes.TodosNomes();
        }



        public byte[] GetThumbnail(int idProduto, string caminhoImagem)
        {
            DirectoryInfo dir = new DirectoryInfo(Path.Combine(caminhoImagem, "pro", idProduto.ToString()));
            if (!dir.Exists)
                dir.Create();

            FileInfo[] files = dir.GetFiles("thumbnail.*");
            if (files.Length == 1)
            {
                return System.IO.File.ReadAllBytes(files[0].FullName);
            }
            return null;
        }
    }
}