using System;

namespace BinaryTrees
{
    public class BinaryTree<T>
        where T : IComparable
    {
        private BinaryTreeNode<T> root;
        public void Add(T value)
        {
            if (root == null)
                root = new BinaryTreeNode<T>(value);
            var subtreeRoot = root;
            while (true)
            {
                if (value.CompareTo(subtreeRoot.Value) < 0)
                    if (subtreeRoot.Left == null)
                    {
                        subtreeRoot.Left = new BinaryTreeNode<T>(value);
                        return;
                    }
                    else
                        subtreeRoot = subtreeRoot.Left;
                else if (subtreeRoot.Right == null)
                {
                    subtreeRoot.Right = new BinaryTreeNode<T>(value);
                    return;
                }
                else
                    subtreeRoot = subtreeRoot.Right;
            }
        }

        public bool Contains(T value)
        {
            var subtreeRoot = root;
            while (true)
            {
                if (subtreeRoot == null)
                    return false;
                if (subtreeRoot.Value.CompareTo(value) > 0)
                    subtreeRoot = subtreeRoot.Left;
                else if (subtreeRoot.Value.CompareTo(value) < 0)
                    subtreeRoot = subtreeRoot.Right;
                else
                    return true;
            }
        }
    }
    
    public class BinaryTreeNode<T>
        where T : IComparable
    {
        public BinaryTreeNode<T> Left;
        public BinaryTreeNode<T> Right;
        public readonly T Value;
        public BinaryTreeNode(T value) => Value = value;
    }
}
