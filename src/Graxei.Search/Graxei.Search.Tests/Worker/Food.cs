using Graxei.Search.Attributes;

namespace Graxei.Search.Tests.Worker
{
    [Indexed(Index="consumable")]
    public class Food
    {
        private int id;
        private string name;

        [DocumentId]
        public virtual int Id
        {
            get { return id; }
            set { id = value; }
        }

        [Field(Index.Tokenized)]
        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
