using System;
using System.Collections.Generic;
using OpenTK;
namespace Template
{
    struct Intersect{
        public float Distance;
        public float Col;
        public Ray OriginalRay;

    }
    class Scene
    {
        private List<Primitive> entities;
        private List<Light> lightSources;

        public Scene()
        {
            entities = new List<Primitive>();
            entities.Add(new Sphere(new Vector3(0,0,5), 1.5f));
            entities.Add(new Sphere(new Vector3(-2, 0, 4), 1.5f));
            entities.Add(new Sphere(new Vector3(2, 0, 6), 1.5f));
            entities.Add(new Plane(new Vector3(0, -10, 0), 10.0f));
        }

        public float intersectWithScene(Ray ray)
        {
            float distance = 0;

            foreach (var primitive in entities)
            {
                float dis = primitive.intersect(ray);
                if(dis > 0)
                {
                    ray.Distance = dis;
                    distance = dis;
                }
            }

            return distance;
        }

        public List<Primitive> getObjects()
        {
            return entities;
        }
    }
}