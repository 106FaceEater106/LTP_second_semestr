using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BinaryTrees
{
    public class BinaryTreeNode<T>
        where T : IComparable
    {
        public BinaryTreeNode<T> Left;
        public BinaryTreeNode<T> Right;
        public readonly T Value;
        public int Count = 1;
        public BinaryTreeNode(T value) => Value = value;
    }

    public class BinaryTree<T> : IEnumerable<T>
        where T : IComparable
    {
        private BinaryTreeNode<T> root;
        private readonly Queue<T> queue = new Queue<T>();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public T this[int i] => GetValueByIndex(i);
        public void Add(T value)
        {
            if (root == null)
            {
                root = new BinaryTreeNode<T>(value);
                return;
            }
            
            SearchChildfreeNode(value);
        }

        private void SearchChildfreeNode(T value)
        {
            var subtreeRoot = root;
            while (true)
            {
                subtreeRoot.Count++;
                if (value.CompareTo(subtreeRoot.Value) < 0)
                    if (subtreeRoot.Left == null)
                    {
                        subtreeRoot.Left = new BinaryTreeNode<T>(value);
                        break;
                    }
                    
                    else
                        subtreeRoot = subtreeRoot.Left;
                else if (subtreeRoot.Right == null)
                {
                    subtreeRoot.Right = new BinaryTreeNode<T>(value);
                    break;
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
            queue.Enqueue(node.Value);
            CircumventTree(node.Right);
        }

        private T GetValueByIndex(int i)
        {
            var subtreeRoot = root;
            while (true)
            {
                var correctedCount = subtreeRoot.Left?.Count ?? 0;
                if (correctedCount == i)
                    return subtreeRoot.Value;
                if (correctedCount < i)
                {
                    i -= correctedCount + 1;
                    subtreeRoot = subtreeRoot.Right;
                }

                else
                    subtreeRoot = subtreeRoot.Left;
            }
        }
    }
}
