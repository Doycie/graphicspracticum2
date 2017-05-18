using System;
using System.Collections.Generic;
using OpenTK;
namespace Template
{
    class Intersect{
        public Vector3 Col;
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
           // entities.Add(new Plane(new Vector3(0, -1, -10), 10.0f));

            lightSources.Add(new Light(new Vector3(0, 3.0f, 2.0f)));
        }

        public void PrimaryRayIntersectWithScene(Intersect intersect)
        {
            foreach (var primitive in entities)
            {
                primitive.Intersect(intersect.PrimaryRay);
            }

            float t = intersect.PrimaryRay.distance;

            if (t < 100.0f)
            {
               // intersect.PrimaryIntersectLocation = intersect.PrimaryRay.direction * t;
                intersect.Col = new Vector3((1 / t * 5.0f), 0.0f, 1.0f);
            }
            //  intersect.Col = ((int)((1 / (t * t) * 255)) << 16) + ((int)((1 / (t * t) * 255)) << 8);
            //intersect.Col = (int)( 2f / (t*t*t) * 255.0f ) << 8;


        }

        public void LightRayIntersectWithScene(Intersect intersect)
        {
            if (intersect.PrimaryRay.distance > 100.0f)
                return;


            Vector3 dir = intersect.PrimaryIntersectLocation - lightSources[0].GetPosition();
            dir.Normalize();
            intersect.LightRay = new Ray(lightSources[0].GetPosition(), dir);

            foreach (var primitive in entities)
            {
                primitive.Intersect(intersect.LightRay);
            }
            //float t = -intersect.LightRay.distance;
            //Console.WriteLine(t);
            //if (t > 2.0f)
             //   intersect.Col -= (int)(( (t*t*t)  * 50.0f)) << 8;

        }

        public List<Primitive> GetObjects()
        {
            return entities;
        }
    }
}