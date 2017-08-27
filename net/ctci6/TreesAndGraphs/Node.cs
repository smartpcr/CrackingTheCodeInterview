// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Node.cs" company="Microsoft Corporation">
//   Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TreesAndGraphs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;

    public class Node
    {
        public IComparable Value { get; set; }
        public virtual Node[] Children { get; set; }

        public Node(string name)
        {
            Value = name;
        }

        public Node(int value)
        {
            Value = value;
        }
    }

    public class BinaryTreeNode : Node
    {
        public BinaryTreeNode Parent { get; set; }

        public BinaryTreeNode(string name) : base(name)
        {
        }

        public BinaryTreeNode(int name) : base(name)
        {
        }

        public BinaryTreeNode LeftChild { get; set; }
        public BinaryTreeNode RightChild { get; set; }

        public bool IsLeftChild
        {
            get
            {
                if (this.Parent != null)
                {
                    return this.Value.CompareTo(this.Parent.Value) < 0;
                }
                return false;
            }
        }

        public bool IsRightChild
        {
            get
            {
                if (this.Parent != null)
                {
                    return this.Value.CompareTo(this.Parent.Value) > 0;
                }
                return false;
            }
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static BinaryTreeNode FromJson(string json)
        {
            return JsonConvert.DeserializeObject<BinaryTreeNode>(json);
        }

        #region Overrides of Node

        public override Node[] Children
        {
            get
            {
                List<BinaryTreeNode> children = new List<BinaryTreeNode>();
                if (LeftChild != null) children.Add(LeftChild);
                if (RightChild != null) children.Add(RightChild);
                return children.OfType<Node>().ToArray();
            }
        }

        #endregion
    }

    public class Vertex : Node
    {
        public Vertex(string name) : base(name)
        {
        }

        public Vertex(int name) : base(name)
        {
        }

        public bool Visited { get; set; }

        public Vertex[] ConnectToNodes
        {
            get { return Children == null ? null : Children.OfType<Vertex>().ToArray(); }
            set { Children = value == null ? null : value.OfType<Node>().ToArray(); }
        }

        public Edge[] Edges
        {
            get
            {
                if (ConnectToNodes != null && ConnectToNodes.Length > 0)
                {
                    return ConnectToNodes.Select(c => new Edge(this, c)).ToArray();
                }
                return null;
            }
        }

    }

    public class Edge
    {
        public Vertex From { get; set; }
        public Vertex To { get; set; }

        public Edge(Vertex fromNode, Vertex toNode)
        {
            From = fromNode;
            To = toNode;
        }
    }

    public enum GraphSearchMode
    {
        DepthFirst,
        BreadthFirst
    }
}