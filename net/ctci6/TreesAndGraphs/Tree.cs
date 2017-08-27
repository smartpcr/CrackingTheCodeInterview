// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Tree.cs" company="Microsoft Corporation">
//   Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TreesAndGraphs
{
    using System;
    using System.Collections.Generic;

    public class Tree
    {
        public static List<IComparable> InOrderTraversal(BinaryTreeNode treeNode)
        {
            List<IComparable> path = new List<IComparable>();
            if (treeNode != null)
            {
                var leftPath = InOrderTraversal(treeNode.LeftChild);
                if (leftPath != null && leftPath.Count > 0)
                {
                    path.AddRange(leftPath);
                }

                path.Add(Visit(treeNode));

                var rightPath = InOrderTraversal(treeNode.RightChild);
                if (rightPath != null && rightPath.Count > 0)
                {
                    path.AddRange(rightPath);
                }
            }
            return path;
        }

        public static List<IComparable> PreOrderTraversal(BinaryTreeNode treeNode)
        {
            List<IComparable> path = new List<IComparable>();
            if (treeNode != null)
            {
                path.Add(Visit(treeNode));

                var leftPath = PreOrderTraversal(treeNode.LeftChild);
                if (leftPath != null && leftPath.Count > 0)
                {
                    path.AddRange(leftPath);
                }
                
                var rightPath = PreOrderTraversal(treeNode.RightChild);
                if (rightPath != null && rightPath.Count > 0)
                {
                    path.AddRange(rightPath);
                }
            }
            return path;
        }

        public static List<IComparable> PostOrderTraversal(BinaryTreeNode treeNode)
        {
            List<IComparable> path = new List<IComparable>();
            if (treeNode != null)
            {
                var leftPath = PostOrderTraversal(treeNode.LeftChild);
                if (leftPath != null && leftPath.Count > 0)
                {
                    path.AddRange(leftPath);
                }

                var rightPath = PostOrderTraversal(treeNode.RightChild);
                if (rightPath != null && rightPath.Count > 0)
                {
                    path.AddRange(rightPath);
                }

                path.Add(Visit(treeNode));
            }
            return path;
        }

        public static Dictionary<int, List<IComparable>> BreadthFirstTraversal(BinaryTreeNode treeNode)
        {
            var output = new Dictionary<int,List<IComparable>>();
            Queue<Tuple<BinaryTreeNode, int>> queue = new Queue<Tuple<BinaryTreeNode, int>>();

            if (treeNode == null)
                return output;

            int depth = 0;
            queue.Enqueue(new Tuple<BinaryTreeNode, int>(treeNode, depth));
            while (queue.Count > 0)
            {
                var n = queue.Dequeue();
                if (!output.ContainsKey(n.Item2))
                {
                    output.Add(n.Item2, new List<IComparable>() {n.Item1.Value});
                }
                else
                {
                    output[n.Item2].Add(n.Item1.Value);
                }

                if (n.Item1.LeftChild != null)
                {
                    queue.Enqueue(new Tuple<BinaryTreeNode, int>(n.Item1.LeftChild, n.Item2+1));
                }
                if (n.Item1.RightChild != null)
                {
                    queue.Enqueue(new Tuple<BinaryTreeNode, int>(n.Item1.RightChild, n.Item2 + 1));
                }
            }

            return output;
        }

        public static int GetDepth(BinaryTreeNode tree)
        {
            int depth = 0;
            if (tree == null)
                return depth;

            depth++;
            int childPath = 0;
            if (tree.LeftChild != null)
            {
                var leftDepth = GetDepth(tree.LeftChild);
                if (leftDepth > childPath)
                    childPath = leftDepth;
            }
            if (tree.RightChild != null)
            {
                var rightDepth = GetDepth(tree.RightChild);
                if (rightDepth > childPath)
                    childPath = rightDepth;
            }

            return childPath + depth;
        }

        public static bool IsBalanced(BinaryTreeNode tree)
        {
            var balanced = Math.Abs(GetDepth(tree.LeftChild) - GetDepth(tree.RightChild)) <= 1;
            if (!balanced)
                return false;

            if (tree.LeftChild != null)
            {
                if (!IsBalanced(tree.LeftChild))
                    return false;
            }
            if (tree.RightChild != null)
            {
                if (!IsBalanced(tree.RightChild))
                    return false;
            }

            return true;
        }

        public static BinaryTreeNode GetNextInOrderNode(BinaryTreeNode current)
        {
            if (current == null)
                return null;
            if (current.Parent == null)
            {
                // current is root
                if (current.RightChild == null)
                    return null;

                BinaryTreeNode next = current.RightChild;
                while (next.LeftChild != null)
                {
                    next = next.LeftChild;
                }
                return next;
            }
            else
            {
                if (current.IsRightChild)
                {
                    // current is right child
                    if (current.RightChild != null)
                    {
                        BinaryTreeNode next = current.RightChild;
                        while (next.LeftChild != null)
                        {
                            next = next.LeftChild;
                        }
                        return next;
                    }

                    var parent = current.Parent;
                    while (parent != null && !parent.IsLeftChild)
                    {
                        parent = parent.Parent;
                    }
                    if (parent != null && parent.IsLeftChild)
                    {
                        return parent.Parent;
                    }

                    return null;
                }
                else
                {
                    // current is left child
                    BinaryTreeNode next;
                    if (current.RightChild != null)
                    {
                        next = current.RightChild;
                        while (next.LeftChild != null)
                        {
                            next = next.LeftChild;
                        }
                    }
                    else
                    {
                        next = current.Parent;
                    }
                    
                    return next;
                }
            }
        }

        public static BinaryTreeNode BuildBinarySearchTree(List<int> input)
        {
            if (input == null || input.Count == 0)
                return null;

            input.Sort();
            return BuildBST(input, 0, input.Count-1);
        }

        private static BinaryTreeNode BuildBST(List<int> input, int start, int end)
        {
            if (start > end)
                return null;

            var mid = start + (end - start) / 2;
            BinaryTreeNode root = new BinaryTreeNode(input[mid]);
            var leftChild = BuildBST(input, start, mid - 1);
            if (leftChild != null)
            {
                leftChild.Parent = root;
                root.LeftChild = leftChild;
            }
            var rightChild = BuildBST(input, mid + 1, end);
            if (rightChild != null)
            {
                rightChild.Parent = root;
                root.RightChild = rightChild;
            }
            return root;
        }

        private static IComparable Visit(BinaryTreeNode treeNode)
        {
            return treeNode.Value;
        }
    }
}