using OpenTK;
using System;

namespace Template
{
    struct Ray
    {
        public Vector3 origin;
        public Vector3 direction;
        public float distance;

        public Ray(Vector3 origin, Vector3 direction)
        {
            this.origin = origin;
            this.direction = direction;
            distance = float.MaxValue ;
        }
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
        public Vector3 position
        {
            get; private set;
        }
        public float radius
        {
            get; private set;
        }

        public Vector4 color
        {
            get; private set;
        }

        public Sphere(Vector3 position, float radius)
        {
            color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
            this.position = position;
            this.radius = radius;
        }

        public override float Intersect(Ray ray)
        {
            Vector3 rayOriginToSphereOrigin = position - ray.origin;

            //We calculate the distance from t to the center of the circle. 
            float t = Vector3.Dot(rayOriginToSphereOrigin, ray.direction);

            Vector3 q = rayOriginToSphereOrigin - t * ray.direction;
            float p2 = Vector3.Dot(q, q);
            if (p2 > radius * radius)
                return -1.0f;
            t -= (float)Math.Sqrt(radius * radius - p2);
            if ((t < ray.distance) && (t > 0))
                return t;
            return -1.0f;
        }
    }

    class Plane : Primitive
    {
        private Vector3 _normal;
        private float _distance;

        public Plane(Vector3 normal,  float distance)
        {
            _normal = normal;
            _distance = distance;
        }

        public override float Intersect(Ray ray)
        {
            //We first calculate the distance from the origin of the ray to the plane
            float t = Vector3.Dot(ray.origin, _normal) + _distance;

            //Next we divide this distance by the angle the plane and the normal vector of the plane, giving us the distance from the ray to the plane.
            t /= Vector3.Dot(ray.direction, _normal);

            if (t < ray.distance)
                return t;
            else
                return -1.0f;
        }
    }
}