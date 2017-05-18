using OpenTK;
using System.Collections.Generic;
using OpenTK.Input;
using System;

namespace Template
{
    internal class Game
    {
        // member variables
        public Surface screen;

        public Camera camera;
        public Scene scene;

        private float d_scale;
        private int d_width;
        private int d_height;
        private int d_offsetX;
        private int d_offsetY;

        // initialize
        public void Init()
        {
            scene = new Scene();
            d_scale = 16.0f;
            d_width = screen.width / 2;
            d_height = screen.height;
            d_offsetX = screen.width / 2;
            d_offsetY = 0;

            camera = new Camera(new Vector3(0, 0, 0), new Vector3(0, 0, 1), Camera.GetAspectRatio(d_width, d_height));

            camera.LookAt(new Vector3(0.0f, 1.0f, 0.0f));
        }

        public void Tick()
        {
            screen.Clear(0);

            RenderRaycastScene();

            RenderDebugProps();

            
        }

        private int TX(float x)
        {
            return (int)((x + d_scale / 2.0f) * d_width / d_scale) + d_offsetX;
        }

        private int TY(float y)
        {
            return (int)((-y + d_scale / 1.2f) * d_height / d_scale) + d_offsetY;
        }

        public void Input(KeyboardState keyboard)
        {
            if (keyboard[OpenTK.Input.Key.A])
            {
                camera.MovePosition(new Vector3(-0.1f, 0 , 0));
            }
            if (keyboard[OpenTK.Input.Key.W])
            {
                camera.MovePosition(new Vector3(0, 0.1f, 0));
            }
            if (keyboard[OpenTK.Input.Key.S])
            {
                camera.MovePosition(new Vector3(0, -0.1f, 0));
            }
            if (keyboard[OpenTK.Input.Key.D])
            {
                camera.MovePosition(new Vector3(0.1f, 0, 0));
            }
        }

        private void RenderRaycastScene()
        {
            int viewportWidth = screen.width / 2;
            int viewportHeight = screen.height;

            Vector3 origin = camera.GetPosition();
            Vector3 direction;

            for (int x = 0; x < viewportWidth; x++)
            {
                for (int y = 0; y < viewportHeight; y++)
                {
                    Intersect intersect = new Intersect();

                    direction = camera.GetRayDirection(x / (float)viewportWidth, y / (float)viewportHeight);
                    Ray r = new Ray(origin, direction);
                    intersect.PrimaryRay = r;

                    scene.PrimaryRayIntersectWithScene(intersect);
                   // scene.LightRayIntersectWithScene(intersect);
                
                    screen.Plot(x, y, ((int)(intersect.Col.X * 255) << 16) + ((int)(intersect.Col.Y * 255) << 8) + ((int)(intersect.Col.Z * 255)) );

                    if (y == viewportHeight / 2 && x % 32 == 0)
                    {
                        Vector3 d = (intersect.PrimaryRay.distance) * intersect.PrimaryRay.direction;

                        if (x == 256)
                        {

                            Console.Clear();
                            Console.WriteLine(intersect.PrimaryRay.distance);
                            Console.WriteLine(intersect.PrimaryRay.direction);
                            Console.WriteLine(origin);
                             Console.WriteLine(d);
                        }

                     

                        screen.Line(
                           TX(origin.X),
                           TY(origin.Y),
                           TX(d.X),
                           TY(d.Y),
                        (int)x/2 << 8);

                        //Primary Rays


                        //Light Rays
                        //screen.Line(TX(intersect.LightRay.origin.X), TY(intersect.LightRay.origin.Y),
                        //    TX(intersect.LightRay.distance * intersect.LightRay.direction.X),
                        //    TY(intersect.LightRay.distance * intersect.LightRay.direction.Y), 255 );
                    }
                }
            }
        }

        private void RenderDebugProps()
        {
            List<Primitive> props = scene.GetObjects();
            for (int i = 0; i < props.Count; i++)
                props[i].RenderDebug(screen, TX, TY);
        }

        private void RenderDebug()
        {

        }
    }
}