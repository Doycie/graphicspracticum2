using OpenTK;
using System;

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
           for(int i = 0; i < screen.width; i++)
            {
                for (int j = 0; j < screen.height; j++)
                {
                    Vector3 origin = camera.getPosition() ;
                    Vector3 direction = camera.getRayDirection(i / (float)screen.width, j / (float)screen.height);
                 
                    // Console.WriteLine(scene.intersectWithScene(new Ray(origin, direction)));
                    if (i == screen.width / 2 && j == screen.height / 2)
                        i = i;

                    int col =  0;
                    float t = scene.intersectWithScene(new Ray(origin, direction));
                  //  Console.WriteLine(t);
                    if (t > 0)
                    {
                        col = (int)(255/((t *t)* .2f)) <<16;
                    }
                    screen.pixels[i + j * screen.width] = col;
                }
            }
           

        }

        private void Render()
        {
        }
    }
} // namespace Template