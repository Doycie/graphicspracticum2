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

        // initialize
        public void Init()
        {
            camera = new Camera(new Vector3(0, 0, 0), new Vector3(0, 0, 1));
            scene = new Scene();
            d_scale = 24.0f;
            d_width = screen.width / 2;
            d_height = screen.height;
            d_offsetX = screen.width / 2;
            d_offsetY = 0;

            camera.lookAt(new Vector3(1.0f,0.0f, 2.0f));
        }

        // tick: renders one framed

        public void Tick()
        {
            screen.Clear(0);
            // screen.Print("hello world", 2, 2, 0xffffff);
            // screen.Line(2, 20, 160, 20, 0xff0000);
            // screen.Line(2, 20, 160, 20, 0xff0000);

            RenderRaycastScene(screen.width / 2, screen.height);

            RenderDebug();
        }

        private float d_scale;
        private int d_width;
        private int d_height;
        private int d_offsetX;
        private int d_offsetY;

        private int TX(float x)
        {
            return (int)((x + d_scale / 2.0f) * d_width / d_scale) + d_offsetX;
        }

        private int TY(float y)
        {
            return (int)((y + d_scale / 1.2f) * d_height / d_scale) + d_offsetY;
        }

        private void RenderDebug()
        {
            int xc = TX(camera.getPosition().X);
            int yc = TY(-camera.getPosition().Z);
            float rad = 1.0f * d_scale;
            screen.Circle(xc, yc, rad, 255 << 16);

            List<Primitive> entities = scene.getObjects();
            foreach (Primitive e in entities)
            {
                if (e is Sphere)
                {
                    //Console.WriteLine((e as Sphere).getPosition().X);
                    xc = (int)(TX((e as Sphere).getPosition().X));
                    yc = (int)(TY(-(e as Sphere).getPosition().Z));
                    rad = (e as Sphere).getRadius() * d_scale;
                    screen.Circle(xc, yc, rad, 255);
                }
            }
        }


        float x = 0;
        public void Input(KeyboardState keyboard)
        {
            if (keyboard[OpenTK.Input.Key.D])
            {
                x += 0.1f;
                camera.lookAt(new Vector3(x, 0.0f, 2.0f));
            }
            if (keyboard[OpenTK.Input.Key.A])
            {
                x -= 0.1f;
                camera.lookAt(new Vector3(x, 0.0f, 2.0f));
            }
        }

        private void RenderRaycastScene(int w, int h)
        {
            Vector3 origin = camera.getPosition();
            Vector3 direction = new Vector3(0, 0, 0);
            float t = 0.0f;
            int col = 0;
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    direction = camera.getRayDirection(i / (float)w, j / (float)h);
                    col = 0;
                    t = scene.intersectWithScene(new Ray(origin, direction));
                    if (t > 0)
                    {
                        col = ((int)((1 / (t * t) * 255)) << 16) + ((int)((1 / (t * t) * 255)) << 8) ;
                    }
                    screen.pixels[i + j * screen.width] = col;
                    if (j == h / 2 && i % 32 == 0)
                    {
                        if(t <= 0){
                            t = 100.0f; 
                        }
                        screen.Line(TX(camera.getPosition().X), TY(-camera.getPosition().Z), TX(((t) * direction).X), TY(-((t) * direction).Z), 255 << 8);
                    }
                }
            }
        }
    }
} // namespace Template