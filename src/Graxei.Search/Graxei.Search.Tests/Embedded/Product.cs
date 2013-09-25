using System.Collections.Generic;
using Iesi.Collections.Generic;
using Graxei.Search.Attributes;


namespace Graxei.Search.Tests.Embedded
{
    [Indexed]
    public class Product
    {
        [DocumentId]
        private int id;
        [Field(Index.Tokenized)]
        private string name;
        [IndexedEmbedded]
        private Iesi.Collections.Generic.ISet<Author> authors = new HashedSet<Author>();
        [IndexedEmbedded]
        private IDictionary<string, Order> orders = new Dictionary<string, Order>();

        public virtual int Id
        {
            get { return id; }
            set { id = value; }
        }

        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

        public virtual Iesi.Collections.Generic.ISet<Author> Authors
        {
            get { return authors; }
            set { authors = value; }
        }

        public virtual IDictionary<string, Order> Orders
        {
            get { return orders; }
            set { orders = value; }
        }
    }
}