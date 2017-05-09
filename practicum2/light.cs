using OpenTK;

namespace Template
{
    class Light
    {
        private Vector3 _loc;
        private Vector3 _intensity;
     

        public Light(Vector3 loc)
        {
            _loc = loc;
        }

        public Vector3 GetPosition()
        {
            return _loc;
        }
    }
}