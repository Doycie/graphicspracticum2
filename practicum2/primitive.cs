using OpenTK;
using System;

namespace Template
{
    struct Ray
    {
        public Ray(Vector3 origin, Vector3 direction)
        {
            Origin = origin;
            Direction = direction;
            Distance = float.MaxValue ;
        }
        public Vector3 Origin;
        public Vector3 Direction;
        public float Distance;
    }

    class Primitive
    {
        public virtual float intersect(Ray ray)
        {

            return -3.0f;
        }
    }

    class Sphere : Primitive
    {
        private Vector3 _pos;
        private float _rad;
        private Vector4 _col;

        public Sphere(Vector3 pos, float rad)
        {
            _col = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
            _pos = pos;
            _rad = rad;
        }

        public override float intersect(Ray ray)
        {
            Vector3 c = _pos - ray.Origin;
            float t = Vector3.Dot(c, ray.Direction);
            Vector3 q = c - t * ray.Direction;
            float p2 = Vector3.Dot(q, q);
            if (p2 > _rad * _rad)
                return -5.0f;
            t -= (float)Math.Sqrt(_rad * _rad - p2);
            if ((t < ray.Distance) && (t > 0))
                return t;
            return -6.0f;
        }
    }

    class Plane : Primitive
    {
        private Vector3 _nor;
        private float _dis;
    }
}