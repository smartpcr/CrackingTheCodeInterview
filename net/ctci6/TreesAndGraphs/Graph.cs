// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Graph.cs" company="Microsoft Corporation">
//   Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TreesAndGraphs
{
    using System;
    using System.Collections.Generic;

    public class Graph
    {
        public Vertex[] Vertices { get; set; }

        public void Reset()
        {
            foreach (var v in Vertices)
            {
                v.Visited = false;
            }
        }

        public static Vertex[] DepthFirstTraversal(Vertex vertex)
        {
            List<Vertex> path = new List<Vertex>();
            if (vertex==null || vertex.Visited)
            {
                return path.ToArray();
            }

            path.Add(vertex);
            vertex.Visited = true;

            if (vertex.ConnectToNodes != null)
            {
                foreach (var destination in vertex.ConnectToNodes)
                {
                    var childPath = DepthFirstTraversal(destination);
                    if (childPath != null && childPath.Length > 0)
                    {
                        path.AddRange(childPath);
                    }
                }
            }
            
            return path.ToArray();
        }

        public static Vertex[] BreadthFirstTraversal(Vertex vertex)
        {
            List<Vertex> path = new List<Vertex>();
            Queue<Vertex> queue = new Queue<Vertex>();

            if (vertex.Visited)
            {
                return path.ToArray();
            }

            vertex.Visited = true;
            queue.Enqueue(vertex);
            while (queue.Count > 0)
            {
                var v = queue.Dequeue();
                path.Add(v);

                if (v.ConnectToNodes == null)
                {
                    continue;
                }

                foreach (Vertex adjacent in v.ConnectToNodes)
                {
                    if (!adjacent.Visited)
                    {
                        adjacent.Visited = true;
                        queue.Enqueue(adjacent);
                    }
                }
            }

            return path.ToArray();
        }

        public static bool FindPath(Vertex fromNode, Vertex target, List<Vertex> path, GraphSearchMode searchMode = GraphSearchMode.DepthFirst)
        {
            Queue<Vertex> queue = new Queue<Vertex>();

            if (fromNode == null || fromNode.Visited)
            {
                return false; 
            }

            if (searchMode == GraphSearchMode.DepthFirst)
            {
                path.Add(fromNode);
                fromNode.Visited = true;
                if (fromNode.Value.CompareTo(target.Value) == 0)
                {
                    return true;
                }
                if (fromNode.ConnectToNodes != null)
                {
                    foreach (var destination in fromNode.ConnectToNodes)
                    {
                        var found = FindPath(destination, target, path, searchMode);
                        if (found)
                        {
                            return true;
                        }
                    }
                }
            }
            else
            {
                fromNode.Visited = true;
                queue.Enqueue(fromNode);

                while (queue.Count > 0)
                {
                    var v = queue.Dequeue();
                    path.Add(v);
                    if (v.Value.CompareTo(target.Value) == 0)
                    {
                        return true;
                    }

                    if (v.ConnectToNodes != null)
                    {
                        foreach (Vertex adjacent in v.ConnectToNodes)
                        {
                            if (!adjacent.Visited)
                            {
                                adjacent.Visited = true;
                                queue.Enqueue(adjacent);
                            }
                        }
                    }
                }
            }

            return false;
        }

    }
}