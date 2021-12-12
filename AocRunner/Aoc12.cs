namespace AocRunner
{
    internal class Aoc12
    {
        /*
         * --- Day 12: Passage Pathing ---
         * https://adventofcode.com/2021/day/12
         *
         */

        Graph graph = new Graph();
        List<Path> possiblePaths = new();
        public Aoc12()
        {
            //var input = File.ReadLines("inputs\\sample12-1.txt");
            //var input = File.ReadLines("inputs\\sample12-2.txt");
            //var input = File.ReadLines("inputs\\sample12-3.txt");
            var input = File.ReadLines("inputs\\day12.txt");
            foreach (var line in input)
            {
                var parts = line.Split('-');
                graph.AddVertex(parts[0], parts[1]);
                graph.AddVertex(parts[1], parts[0]);
            }
        }

        public long RunPuzzle1()
        {
            graph.Start.VisitedTimes = 1;
            FindNextMove(graph.Start, new Path("start"));

            return possiblePaths.Count;
        }

        public long RunPuzzle2()
        {
            graph.Start.VisitedTimes = 1;
            FindNextMovePart2(graph.Start, new Path("start"));

            return possiblePaths.Count;
        }

        private void FindNextMove(Vertex vertex, Path currentPath)
        {
            if (vertex == graph.End)
            {
                possiblePaths.Add(currentPath);
            }

            foreach (var nextMove in vertex.Neighbours.Where(neighbour => graph.GetByName(neighbour).VisitedTimes < graph.GetByName(neighbour).AllowedVisits))
            {
                if (nextMove != null)
                {
                    // branching, create new path
                    currentPath = new Path(currentPath.Current);

                    var nextVertex = graph.GetByName(nextMove);
                    nextVertex.VisitedTimes += 1;

                    currentPath.Add($"-{nextMove}");
                    FindNextMove(nextVertex, currentPath);
                    nextVertex.VisitedTimes -= 1;
                    currentPath.RemoveLast();
                }
                else
                {
                    // invalid move
                    return;
                }
            }
        }

        private void FindNextMovePart2(Vertex vertex, Path currentPath)
        {
            if (vertex == graph.End)
            {
                possiblePaths.Add(currentPath);
                return;
            }

            foreach (var nextMove in vertex.Neighbours)
            {
                if (nextMove != null)
                {
                    var nextVertex = graph.GetByName(nextMove);

                    if (currentPath.CanAdd(nextMove))
                    {
                        nextVertex.VisitedTimes += 1;

                        // branching, create new path
                        currentPath = new Path(currentPath.Current);

                        currentPath.Add($"-{nextMove}");
                        FindNextMovePart2(nextVertex, currentPath);
                        nextVertex.VisitedTimes -= 1;
                        currentPath.RemoveLast();
                    }
                }
                else
                {
                    // invalid move
                    return;
                }
            }
        }

        private class Path
        {
            public string Current { get; set; }
            public bool IsValid => Current.StartsWith("start") && Current.EndsWith("end");

            public Path(string current)
            {
                Current = current;
            }

            public void Add(string element)
            {
                Current = string.Concat(Current, element);
            }

            public void RemoveLast()
            {
                Current = Current.Substring(0, Current.LastIndexOf('-'));
            }

            public bool CanAdd(string element)
            {
                var allElements = Current.Split('-').ToList();
                if ((element == "start" && allElements.Contains("start")) ||
                    (element == "end" && allElements.Contains("end")))
                {
                    return false;
                }

                allElements.Add(element);
                var onlySmall = allElements.Where(e => char.IsLower(e[0])).ToList();
                onlySmall.Sort();

                string previous = string.Empty;
                bool hasSame = false;
                foreach (var el in onlySmall)
                {
                    if (previous == el)
                    {
                        if (hasSame)
                        {
                            return false;
                        }
                        hasSame = true;
                    }

                    previous = el;
                }
                return true;
            }

            public override string ToString()
            {
                return Current;
            }
        }

        private class Graph
        {
            private LinkedList<Vertex> vertices = new();

            public void AddVertex(string name, string neighbour)
            {
                if (vertices.Any(v => v.Name == name))
                {
                    vertices.Single(v => v.Name == name).AddNeighbour(neighbour);
                }
                else
                {
                    var vertex = vertices.AddLast(new Vertex(name));
                    vertex.Value.AddNeighbour(neighbour);
                }
            }

            public Vertex Start => vertices.Single(v => v.Name == "start");
            public Vertex End => vertices.Single(v => v.Name == "end");

            public Vertex GetByName(string name)
            {
                return vertices.Single(v => v.Name == name);
            }
        }

        private class Vertex
        {
            public string Name { get; }
            public int VisitedTimes { get; set; }
            public List<string> Neighbours { get; }
            public int AllowedVisits => char.IsUpper(Name[0]) ? int.MaxValue : 1;

            public Vertex(string name)
            {
                Name = name;
                Neighbours = new List<string>();
            }

            public void AddNeighbour(string neighbour)
            {
                Neighbours.Add(neighbour);
            }

            public override string ToString()
            {
                return $"{Name} - {VisitedTimes}";
            }
        }
    }
}