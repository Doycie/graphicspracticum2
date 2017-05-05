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

        public Vector3 getPosition()
        {
            return _pos;
        }
        public float getRadius()
        {
            return _rad;
        }

        public override float intersect(Ray ray)
        {
            Vector3 c = _pos - ray.Origin;
            float t = Vector3.Dot(c, ray.Direction);
            Vector3 q = c - t * ray.Direction;
            float p2 = Vector3.Dot(q, q);
            if (p2 > _rad * _rad)
                return -1.0f;
            t -= (float)Math.Sqrt(_rad * _rad - p2);
            if ((t < ray.Distance) && (t > 0))
                return t;
            return -1.0f;
        }
    }

    class Plane : Primitive
    {
        private Vector3 _nor;
        private float _dis;

        public Plane(Vector3 nor,  float dis)
        {
            _nor = nor;
            _dis = dis;
        }

        public override float intersect(Ray ray)
        {
            float t = -(Vector3.Dot(ray.Origin, _nor) + _dis);
            t /= Vector3.Dot(ray.Direction, _nor);
            if (t < ray.Distance)
                return t;
            else
                return -1.0f;
        }
    }
}