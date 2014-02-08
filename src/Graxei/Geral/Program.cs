using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graxei.Modelo;

namespace Geral
{
    class Program
    {
        static void Main(string[] args)
        {
            Estado e = new Estado() {Sigla = "RJ", Nome = "Rio de Janeiro"};
            Cidade c = new Cidade() {Nome = "Rio de Janeiro", Estado = e};
            Cidade c2 = new Cidade() { Nome = "Nova Iguaçu", Estado = e };
            Bairro b = new Bairro() {Nome = "Centro", Cidade = c};
            Bairro b2 = new Bairro() { Nome = "Centro", Cidade = c2 };
            Loja l = new Loja() { Nome="AutoPeças"};
            Endereco end1 = new Endereco()
                               {Bairro = b, Logradouro = "Rua B", Numero = "100", Complemento = "Loja A"};
            Endereco end2 = new Endereco() { Bairro = b2, Logradouro = "Rua A", Numero = "100"};
            Endereco end3 = new Endereco() { Bairro = b2, Logradouro = "Rua A", Numero = "100", Complemento = "Loja A" };
            Endereco end4 = new Endereco() { Bairro = b2, Logradouro = "Rua A", Numero = "100", Complemento = "Loja A" };
            Endereco end5 = new Endereco() { Bairro = b, Logradouro = "Rua B", Numero = "100", Complemento = "Loja A" };
            l.AdicionarEndereco(end1); l.AdicionarEndereco(end2); l.AdicionarEndereco(end3); l.AdicionarEndereco(end4);
            l.AdicionarEndereco(end5);

            IList<Endereco> repetidos = HaRepetidos(l.Enderecos);
            Console.ReadKey();
        }

        private static IList<Endereco> HaRepetidos(IList<Endereco> enderecos)
        {
            /*object a =
            (from e in enderecos
             group e by
                 new
                     {
                         e.Logradouro,
                         e.Numero,
                         e.Complemento,
                         Loja = e.Loja.Nome,
                         Bairro = e.Bairro.Nome,
                         Cidade = e.Bairro.Cidade.Nome,
                         Estado = e.Bairro.Cidade.Estado.Sigla
                     }
             into g
             select new {AA = g.Key, BB = g.Count()}).SingleOrDefault();*/
            IList<ContadorEnderecos> a =
                (from e in enderecos
                 group e by
                     e
                 into g
                 select new ContadorEnderecos() {Endereco = g.Key.ToString(), Contador = g.Count()}).Where(q => q.Contador > 1).ToList();
            if (!a.Any())
            {
                Console.WriteLine("Não há endereços repetidos");
            }
            IList<Endereco> resultado = new List<Endereco>();
            foreach (ContadorEnderecos c in a)
            {
                string msg = "Endereco repetido {0} vezes: {1}";
                msg = string.Format(msg, c.Contador, c.Endereco);
                Console.WriteLine(msg);
            }
            return resultado;
        }

        private class ContadorEnderecos
        {
            public string Endereco { get; set; }
            public int Contador { get; set; }
        }
    }
}
