// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TreeTraversalTest.cs" company="Microsoft Corporation">
//   Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TreesAndGraphs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TreeTraversalTest
    {
        [TestMethod]
        public void TestTreeTraversal()
        {
            var tree = new BinaryTreeNode(10)
            {
                LeftChild = new BinaryTreeNode(5)
                {
                    LeftChild = new BinaryTreeNode(9),
                    RightChild = new BinaryTreeNode(18)
                },
                RightChild = new BinaryTreeNode(20)
                {
                    LeftChild = new BinaryTreeNode(3),
                    RightChild = new BinaryTreeNode(7)
                }
            };

            var inOrderPath = string.Join(",", Tree.InOrderTraversal(tree));
            Assert.AreEqual("9,5,18,10,3,20,7", inOrderPath);

            var preOrderPath = string.Join(",", Tree.PreOrderTraversal(tree));
            Assert.AreEqual("10,5,9,18,20,3,7", preOrderPath);

            var postOrderPath = string.Join(",", Tree.PostOrderTraversal(tree));
            Assert.AreEqual("9,18,5,3,7,20,10", postOrderPath);

            var breadthFirstPath = Tree.BreadthFirstTraversal(tree);
            Assert.AreEqual("10", string.Join(",", breadthFirstPath[0]));
            Assert.AreEqual("5,20", string.Join(",", breadthFirstPath[1]));
            Assert.AreEqual("9,18,3,7", string.Join(",", breadthFirstPath[2]));
        }

        [TestMethod]
        public void TestTreeBalance()
        {
            var tree = new BinaryTreeNode(10)
            {
                LeftChild = new BinaryTreeNode(5)
                {
                    LeftChild = new BinaryTreeNode(9),
                    RightChild = new BinaryTreeNode(18)
                },
                RightChild = new BinaryTreeNode(20)
                {
                    LeftChild = new BinaryTreeNode(3),
                    RightChild = new BinaryTreeNode(7)
                }
            };
            bool isBalanced = Tree.IsBalanced(tree);
            Assert.IsTrue(isBalanced);

            tree = new BinaryTreeNode(10)
            {
                LeftChild = new BinaryTreeNode(5)
                {
                    LeftChild = new BinaryTreeNode(9),
                    RightChild = new BinaryTreeNode(18)
                    {
                        RightChild = new BinaryTreeNode(11)
                        {
                            LeftChild = new BinaryTreeNode(23)
                        }
                    }
                },
                RightChild = new BinaryTreeNode(20)
                {
                    LeftChild = new BinaryTreeNode(3),
                    RightChild = new BinaryTreeNode(7)
                    {
                        RightChild = new BinaryTreeNode(11)
                        {
                            LeftChild = new BinaryTreeNode(23)
                        }
                    }
                }
            };
            isBalanced = Tree.IsBalanced(tree);
            Assert.IsFalse(isBalanced);
        }

        [TestMethod]
        public void TestTreeBuilder()
        {
            var input = Enumerable.Range(1, 10);
            var tree = Tree.BuildBinarySearchTree(input.ToList());
            var inOrderPath = Tree.InOrderTraversal(tree);
            Assert.AreEqual(string.Join(",", input), string.Join(",", inOrderPath));

            var isBalanced = Tree.IsBalanced(tree);
            Assert.IsTrue(isBalanced);
        }

        [TestMethod]
        public void TestGetNextTreeNode()
        {
            int start = 1, end = 20;
            var input = Enumerable.Range(start, end);
            var tree = Tree.BuildBinarySearchTree(input.ToList());

            var root = tree;
            var mid = start + (end - start) / 2;
            Assert.AreEqual(mid, root.Value);
            
            var left = root;
            while (left.LeftChild != null)
            {
                left = left.LeftChild;
            }
            Assert.AreEqual(1, left.Value);
            
            var current = left;
            for (int i = 1; i < 10; i++)
            {
                var expected = (int) current.Value + 1;
                var next = Tree.GetNextInOrderNode(current);
                if (expected == 11)
                {
                    Assert.IsNull(next);
                }
                else
                {
                    Assert.IsNotNull(next);
                    Assert.AreEqual(expected, next.Value);
                    current = next;
                }
            }
        }

        [TestMethod]
        public void TestGraphTraversal()
        {
            var v0 = new Vertex(0);
            var v1 = new Vertex(1);
            var v2 = new Vertex(2);
            var v3 = new Vertex(3);
            var v4 = new Vertex(4);
            var v5 = new Vertex(5);
            v0.ConnectToNodes=new Vertex[]{v1,v4,v5};
            v1.ConnectToNodes=new Vertex[]{v3,v4};
            v3.ConnectToNodes=new Vertex[]{v2,v4};
            v2.ConnectToNodes=new Vertex[]{v1};

            var g = new Graph()
            {
                Vertices = new Vertex[] {v0, v1, v2, v3, v4, v5}
            };

            var dfs = Graph.DepthFirstTraversal(v0).Select(v=>v.Value);
            Assert.AreEqual("0,1,3,2,4,5", string.Join(",", dfs));
            g.Reset();

            var bfs = Graph.BreadthFirstTraversal(v0).Select(v => v.Value);
            Assert.AreEqual("0,1,4,5,3,2", string.Join(",", bfs));
            g.Reset();
        }

        [TestMethod]
        public void TestPathFinding()
        {
            var v0 = new Vertex(0);
            var v1 = new Vertex(1);
            var v2 = new Vertex(2);
            var v3 = new Vertex(3);
            var v4 = new Vertex(4);
            var v5 = new Vertex(5);
            v0.ConnectToNodes = new Vertex[] { v1, v4, v5 };
            v1.ConnectToNodes = new Vertex[] { v3, v4 };
            v3.ConnectToNodes = new Vertex[] { v2, v4 };
            v2.ConnectToNodes = new Vertex[] { v1 };

            var g = new Graph()
            {
                Vertices = new Vertex[] { v0, v1, v2, v3, v4, v5 }
            };
            List<Vertex> path = new List<Vertex>();
            var found = Graph.FindPath(v0, v4, path, GraphSearchMode.DepthFirst);
            Assert.IsTrue(found);
            Assert.AreEqual("0,1,3,2,4", string.Join(",", path.Select(v=>v.Value)));

            g.Reset();
            path=new List<Vertex>();
            found = Graph.FindPath(v0, v4, path, GraphSearchMode.BreadthFirst);
            Assert.IsTrue(found);
            Assert.AreEqual("0,1,4", string.Join(",", path.Select(v=>v.Value)));

            g.Reset();
            path=new List<Vertex>();
            found = Graph.FindPath(v1, v0, path, GraphSearchMode.DepthFirst);
            Assert.IsFalse(found);
        }
    }
}