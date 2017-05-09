using Extensions;
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
        //Translates world space coordinates to screen space coordinates.
        public delegate int T(float t);

        public abstract float Intersect(Ray ray);

        public abstract void RenderDebug(Surface surface, T TX, T TY);
    }

    class Sphere : Primitive
    {
        public Vector3 location
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

        public Sphere(Vector3 location, float radius)
        {
            color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
            this.location = location;
            this.radius = radius;
        }

        public override float Intersect(Ray ray)
        {
            Vector3 rayOriginToSphereOrigin = location - ray.origin;

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

        public override void RenderDebug(Surface surface, T TX, T TY)
        {
            float angle = (float)Math.PI / 100 * 2;
            Vector2 v1 = new Vector2(radius, 0);
            Vector2 v2 = v1.Rotate(angle);
            v2.Rotate(angle);
            for(float a = 0; a < 99; a += 1)
            {
                surface.Line(TX(v1.X + location.X), TY(v1.Y + location.Y), TX(v2.X + location.X), TY(v2.Y + location.Y), 0xFFFFFF);
                v1 = v2;
                v2 = v2.Rotate(angle);
            }
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

        //We can;t render a plane because it would fill the entire screen of be a flat line.
        public override void RenderDebug(Surface surface, T TX, T TY)
        {
            return;
        }
    }
}