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
            camera = new Camera(new Vector3(0, 0, 0), new Vector3(0, 0, 1));
            scene = new Scene();
            d_scale = 24.0f;
            d_width = screen.width / 2;
            d_height = screen.height;
            d_offsetX = screen.width / 2;
            d_offsetY = 0;

            camera.LookAt(new Vector3(1.0f, 0.0f, 2.0f));
        }

        public void Tick()
        {
            screen.Clear(0);

            RenderRaycastScene();
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
            if (keyboard[OpenTK.Input.Key.D])
            {
            }
            if (keyboard[OpenTK.Input.Key.A])
            {
            }
        }

        private void RenderRaycastScene()
        {
            int viewportWidth = screen.width / 2;
            int viewportHeight = screen.height;

            Vector3 origin = camera.GetPosition();
            Vector3 direction;
            float t = 0.0f;
            int col = 0;

            for (int x = 0; x < viewportWidth; x++)
            {
                for (int y = 0; y < viewportHeight; y++)
                {
                    direction = camera.GetRayDirection(x / (float)viewportWidth, y / (float)viewportHeight);
                    col = 0;

                    Intersect intersect = new Intersect();
                    t = scene.IntersectWithScene(new Ray(origin, direction));
                    if (t > 0)
                    {
                        col = ((int)((1 / (t * t) * 255)) << 16) + ((int)((1 / (t * t) * 255)) << 8);
                    }
                    screen.pixels[x + y * screen.width] = col;
                    if (y == viewportHeight / 2 && x % 32 == 0)
                    {
                        if (t <= 0)
                        {
                            t = 100.0f;
                        }
                        screen.Line(TX(camera.GetPosition().X), TY(-camera.GetPosition().Z), TX(((t) * direction).X), TY(-((t) * direction).Z), 255 << 8);
                    }
                }
            }
        }
    }
}