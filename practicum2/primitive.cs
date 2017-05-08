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

    abstract class Primitive
    {
        public abstract float Intersect(Ray ray);

        public virtual void RenderDebug(Ray ray)
        {
            
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

        public Vector3 GetPosition()
        {
            return _pos;
        }
        public float GetRadius()
        {
            return _rad;
        }

        public override float Intersect(Ray ray)
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

        public override float Intersect(Ray ray)
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