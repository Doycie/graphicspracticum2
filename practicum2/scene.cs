using System;
using System.Collections.Generic;
using OpenTK;
namespace Template
{
    class Intersect{
        public int Col;
        public Ray PrimaryRay;
        public Vector3 PrimaryIntersectLocation;
        public Ray LightRay;

    }
    class Scene
    {
        private List<Primitive> entities;
        private List<Light> lightSources;

        public Scene()
        {
            lightSources = new List<Light>(); 
            entities = new List<Primitive>();

            entities.Add(new Sphere(new Vector3(0,5,0), 1.5f));
            entities.Add(new Sphere(new Vector3(-2, 3, 0), 1.5f));
            entities.Add(new Sphere(new Vector3(2, 6, 0), 1.5f));
            entities.Add(new Plane(new Vector3(0, 0, -10), 10.0f));

            lightSources.Add(new Light(new Vector3(0, 3.0f, 2.0f)));
        }

        public void PrimaryRayIntersectWithScene(Intersect intersect)
        {
            foreach (var primitive in entities)
            {
                float dis = primitive.Intersect(intersect.PrimaryRay);
                if (dis > 0)
                {
                    intersect.PrimaryRay.distance = dis;
                      intersect.PrimaryIntersectLocation = intersect.PrimaryRay.direction * dis;
                }
            }
            float t = intersect.PrimaryRay.distance;
            //  intersect.Col = ((int)((1 / (t * t) * 255)) << 16) + ((int)((1 / (t * t) * 255)) << 8);
            if(t < 100.0f)
            intersect.Col = 255 << 8 + 255 << 16;

        }

        public void LightRayIntersectWithScene(Intersect intersect)
        {
            if (intersect.PrimaryRay.distance > 100.0f)
                return;
            intersect.LightRay.origin = lightSources[0].GetPosition();

            Vector3 dir = intersect.PrimaryIntersectLocation- intersect.LightRay.origin;
            dir.Normalize();
            intersect.LightRay.direction = dir;

            foreach (var primitive in entities)
            {
                float dis = primitive.Intersect(intersect.LightRay);
                if (dis < 0)
                {
                    intersect.LightRay.distance = dis;
                }
            }
            float t = -intersect.LightRay.distance;

            intersect.Col = (int)( intersect.Col / (t+1));

        }

        public List<Primitive> GetObjects()
        {
            return entities;
        }
    }
}