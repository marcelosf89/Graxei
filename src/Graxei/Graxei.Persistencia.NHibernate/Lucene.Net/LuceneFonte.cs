
using Graxei.Modelo;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using Lucene.Net.Util;
namespace Graxei.Persistencia.Implementacao.NHibernate.Lucene
{
    public class LuceneAdmin
    {

        private LuceneAdmin()
        {
 

        }

        public static LuceneAdmin Instance
        {
            get
            {
                if (_luceneFonte == null)
                {
                    _luceneFonte = new LuceneAdmin();
                }
                return _luceneFonte;
            }
        }

        public void AdicionarElemento(Produto produto)
        {
            Directory directory = FSDirectory.Open(new System.IO.DirectoryInfo(@"C:\Desenvolvimento\Lucene\Directory"));
            Analyzer analyzer = new StandardAnalyzer(Version.LUCENE_29);

            IndexWriter writer = new IndexWriter(directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED);

            Document doc = new Document();
            doc.Add(new Field("ID", produto.Id.ToString(), Field.Store.YES, Field.Index.NO));
            doc.Add(new Field("DESCRICAO", produto.Descricao, Field.Store.YES, Field.Index.ANALYZED));

            writer.AddDocument(doc);

            writer.Optimize();
            writer.Commit();
            writer.Dispose();
        }

        private static LuceneAdmin _luceneFonte;
    }
}
