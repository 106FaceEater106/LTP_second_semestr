using System;
using System.Collections;
using System.Collections.Generic;

namespace BinaryTrees
{
    public class BinaryTree<T> : IEnumerable<T>
        where T : IComparable
    {
        private BinaryTreeNode<T> root;
        private Queue<T> queue = new Queue<T>();
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

        public IEnumerator<T> GetEnumerator()
        {
            CircumventTree(root);
            while (queue.Count != 0)
                yield return queue.Dequeue();
        }

        private void CircumventTree(BinaryTreeNode<T> node)
        {
            if (node == null) return;
            CircumventTree(node.Left);
            if (!queue.Contains(node.Value))
                queue.Enqueue(node.Value);
            CircumventTree(node.Right);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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

