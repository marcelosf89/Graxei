namespace Graxei.Search.Tests.Query
{
    using NHibernate.Transform;
    using System.Collections;

    public class ProjectionToDelimStringResultTransformer : IResultTransformer
    {
        public object TransformTuple(object[] tuple, string[] aliases)
        {
            string s = tuple[0].ToString();
            for (int i = 1; i < tuple.Length; i++)
            {
                s += ", " + tuple[i];
            }

            return s;
        }

        public IList TransformList(IList collection)
        {
            return collection;
        }
    }
}