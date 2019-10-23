using System;
using System.Collections.Generic;
using System.Text;
using Raylib;
using static Raylib.Raylib;

namespace ConsoleApp1
{
    class AABB2
    {
        public Vector2 min = new Vector2(float.NegativeInfinity, float.NegativeInfinity);
        public Vector2 max = new Vector2(float.PositiveInfinity, float.PositiveInfinity);

        public AABB2()
        {
        }

        public AABB2(Vector2 min, Vector2 max)
        {
            this.min = min;
            this.max = max;
        }

        public Vector2 Center()
        {
            return (min + max) * 0.5f;
        }

        public Vector2 Extents()
        {
            return new Vector2(Math.Abs(max.x - min.x) * 0.5f, Math.Abs(max.y - min.y) * 0.5f);
        }

        public List<Vector2> Corners()
        {
            List<Vector2> corners = new List<Vector2>(4);
            corners[0] = min;
            corners[1] = new Vector2(min.x, max.y);
            corners[2] = max;
            corners[3] = new Vector2(max.x, min.y);
            return corners;
        }

        public void Fit(List<Vector2> points)
        {
            min = new Vector2(float.PositiveInfinity, float.PositiveInfinity);
            max = new Vector2(float.NegativeInfinity, float.NegativeInfinity);

            foreach (Vector2 p in points)
            {
                min = Vector2.Min(min, p);
                max = Vector2.Max(max, p);
            }
        }

        public bool Overlaps(Vector2 p)
        {
            return !(p.x < min.x || p.y < min.y || p.x > max.x || p.y > max.y);
        }
        public bool Overlaps(AABB2 other)
        {
            return !(max.x < other.min.x || max.y < other.min.y || min.x > other.max.x || min.y > other.max.y);
        }

        public Vector2 ClosestPoint(Vector2 p)
        {
            return Vector2.Clamp(p, min, max);
        }
    }
    class AABB3
    {
        Vector3 min = new Vector3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);
        Vector3 max = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);

        public AABB3()
        {
        }

        public AABB3(Vector3 min, Vector3 max)
        {
            this.min = min;
            this.max = max;
        }

        public Vector3 Center()
        {
            return (min + max) * 0.5f;
        }

        public Vector3 Extents()
        {
            return new Vector3(Math.Abs(max.x - min.x) * 0.5f, Math.Abs(max.y - min.y) * 0.5f, Math.Abs(max.z - min.z) * 0.5f);
        }

        public List<Vector3> Corners()
        {
            List<Vector3> corners = new List<Vector3>(4);
            corners[0] = min;
            corners[1] = new Vector3(min.x, max.y, min.z);
            corners[2] = max;
            corners[3] = new Vector3(max.x, min.y, min.z);
            return corners;
        }

        public void Fit(List<Vector3> points)
        {
            min = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
            max = new Vector3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);

            foreach (Vector3 p in points)
            {
                min = Vector3.Min(min, p);
                max = Vector3.Max(max, p);
            }
        }

        public bool Overlaps(Vector3 p)
        {
            return !(p.x < min.x || p.y < min.y || p.x > max.x || p.y > max.y);
        }
        public bool Overlaps(AABB3 other)
        {
            return !(max.x < other.min.x || max.y < other.min.y || min.x > other.max.x || min.y > other.max.y);
        }

        public Vector3 ClosestPoint(Vector3 p)
        {
            return Vector3.Clamp(p, min, max);
        }
    }
}
