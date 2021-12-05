namespace AocRunner
{
    internal struct Line
    {
        public Point P1;
        public Point P2;

        public Line(int x1, int y1, int x2, int y2)
        {
            P1 = new Point(x1, y1);
            P2 = new Point(x2, y2);
        }

        public bool IsHorizontal => P1.Y == P2.Y;
        public bool IsVertical => P1.X == P2.X;
        public bool IsStraight => IsHorizontal || IsVertical;

        public override string ToString()
        {
            return $"{P1} -> {P2}";
        }

        public List<Point> PointsCovered
        {
            get
            {
                var result = new List<Point>();

                if (IsHorizontal)
                {
                    int y = P1.Y;
                    if (P1.X < P2.X)
                    {
                        for (int i = P1.X; i <= P2.X; i++)
                        {
                            result.Add(new Point(i, y));
                        }
                    }
                    else if (P1.X > P2.X)
                    {
                        for (int i = P2.X; i <= P1.X; i++)
                        {
                            result.Add(new Point(i, y));
                        }
                    }
                }
                else if (IsVertical)
                {
                    int x = P1.X;
                    if (P1.Y < P2.Y)
                    {
                        for (int i = P1.Y; i <= P2.Y; i++)
                        {
                            result.Add(new Point(x, i));
                        }
                    }
                    else if (P1.Y > P2.Y)
                    {
                        for (int i = P2.Y; i <= P1.Y; i++)
                        {
                            result.Add(new Point(x, i));
                        }
                    }
                }
                else // Diagonal
                {
                    if (P1.X < P2.X && P1.Y < P2.Y)
                    {
                        /*
                         *  p1 X
                         *       X
                         *         X
                         *          X p2
                         */

                        for (int i = P1.X, j = P1.Y; i <= P2.X; i++, j++)
                        {
                            result.Add(new Point(i, j));
                        }
                    }
                    else if (P1.X < P2.X && P1.Y > P2.Y)
                    {
                        /*
                          *         X p2
                          *        X
                          *       X  
                          *  p1 X        
                          */

                        for (int i = P1.X, j = P1.Y; i <= P2.X; i++, j--)
                        {
                            result.Add(new Point(i, j));
                        }
                    }
                    else if (P1.X > P2.X && P1.Y < P2.Y)
                    {
                        /*
                          *         X p1
                          *        X
                          *       X  
                          *  p2 X        
                          */

                        for (int i = P1.X, j = P1.Y; i >= P2.X; i--, j++)
                        {
                            result.Add(new Point(i, j));
                        }
                    }
                    else // if(P1.X > P2.X && P1.Y > P2.Y)
                    {
                        /*
                         *  p2 X
                         *       X
                         *         X
                         *          X p1
                         */

                        for (int i = P1.X, j = P1.Y; i >= P2.X; i--, j--)
                        {
                            result.Add(new Point(i, j));
                        }
                    }
                }

                return result;
            }
        }
    }
}