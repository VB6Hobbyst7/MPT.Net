using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPT.Trees
{
    public class TreeNode : ITreeNode
    {
        private string _Name;
        public string Name
        {
            get { return _Name; }
            private set
            {
                if (!string.IsNullOrEmpty(value)) { _Name = value; }
            }
        }
        
        private List<ITreeNode> _Children = new List<ITreeNode>();
        public List<ITreeNode> Children { get { return _Children; } }

        public TreeNode() { }

        public TreeNode(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Adds child object.
        /// </summary>
        /// <param name="child"></param>
        public virtual void AddChild(ITreeNode child)
        {
            if (!string.IsNullOrEmpty(child.Name) && 
                !ContainsChild(child))
            {
                _Children.Add(child);
            }
        }

        /// <summary>
        /// Removes child object if it is a direct child.
        /// Children of children, etc. are not considered.
        /// </summary>
        /// <param name="child"></param>
        public virtual void RemoveChild(ITreeNode child)
        {
            if (!ContainsChild(child))
            {
                _Children.Remove(child);
            }
        }

        public bool ContainsChild(ITreeNode child)
        {
            return _Children.Contains(child, new TreeNodeComparer());
        }

        public ITreeNode GetChild(ITreeNode child)
        {
            foreach (ITreeNode currentChild in Children)
            {
                TreeNodeComparer comparer = new TreeNodeComparer();
                if (comparer.Equals(currentChild, child))
                {
                    return currentChild;
                }
            }
            return null;
        }

        public override string ToString()
        {
            return Name;
        }

   
    }
}
