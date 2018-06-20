using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPT.Trees
{
    public interface ITreeNode
    {       
        string Name { get; }

        List<ITreeNode> Children { get;}
        
        void AddChild(ITreeNode child);

        void RemoveChild(ITreeNode child);

        bool ContainsChild(ITreeNode child);

        ITreeNode GetChild(ITreeNode child);
    }
}
