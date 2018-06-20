using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPT.Trees
{
    public class TreeNodeComparer : IEqualityComparer<ITreeNode>
    {
        public bool Equals(ITreeNode x, ITreeNode y)
        {
            return (string.Compare(x.Name, y.Name, ignoreCase: false) == 0);
        }

        public int GetHashCode(ITreeNode obj)
        {
            return obj.Name.GetHashCode() * 17;
        }
    }
}
