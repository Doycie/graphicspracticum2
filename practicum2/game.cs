using OpenTK;
using System;
using System.Collections.Generic;

namespace Template
{
    class Game
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
        }

        // tick: renders one framed

        public void Tick()
        {
            screen.Clear(0);
            // screen.Print("hello world", 2, 2, 0xffffff);
            // screen.Line(2, 20, 160, 20, 0xff0000);
            // screen.Line(2, 20, 160, 20, 0xff0000);

            
            RenderRaycastScene( screen.width / 2, screen.height);

    
            RenderDebug(screen.width / 2,0, screen.width / 2, screen.height,32);
        }
        
        private void RenderDebug(int x, int y, int w, int h,int scale)
        {

            int xc = (int)((camera.getPosition().X + 10.0f) * w / scale) + x;
            int yc = (int)((camera.getPosition().Z + 10.0f) * h / scale) + y;
            float rad = 2.0f / scale;
            screen.Circle(xc, yc, rad, 255<< 16);

            

            List<Primitive> entities = scene.getObjects();
            foreach(Primitive e in entities)
            {
                if (e is Sphere)
                {
                    //Console.WriteLine((e as Sphere).getPosition().X);
                     xc = (int)(((e as Sphere).getPosition().X+10.0f)*w /scale)+ x;
                     yc = (int)((-(e as Sphere).getPosition().Z+10.0f )*h/scale)+ y;
                     rad = (e as Sphere).getRadius()/scale;
                    screen.Circle(xc, yc, rad, 255);
                   
                }
            }
           

        }
        private void RenderRaycastScene(int w, int h)
        {
            for (int i = 0; i < w ; i++)
            {
                for (int j = 0; j < h ; j++)
                {
                    Vector3 origin = camera.getPosition();
                    Vector3 direction = camera.getRayDirection(i / (float)w, j / (float)h);
                    

                    int col = 0;
                    float t = scene.intersectWithScene(new Ray(origin, direction));
                    if (t > 0)
                    {
                        col = (int)((1 / (t * t) * 1000)) << 16;
                    }
                    screen.pixels[i + j * screen.width] = col;
                }
               
            }
        }
    }
} // namespace Template