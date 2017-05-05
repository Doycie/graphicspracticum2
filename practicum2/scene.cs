using System.Collections.Generic;
using OpenTK;
namespace Template
{
    class Scene
    {
        private List<Primitive> objects;
        private List<Light> lightSources;

        public Scene()
        {
            objects = new List<Primitive>();
            objects.Add(new Sphere(new Vector3(0,0,5), 2.0f));
            
        }

        public float intersectWithScene(Ray ray)
        {
            float distance = -2.0f;

            foreach (var primitive in objects)
            {
                distance = primitive.intersect(ray);
            }

            return distance;
        }
    }
}