using OpenTK;
using System;

namespace Template
{
    public class Camera
    {
        private Vector3 _position;
        private Vector3 _direction;
        
        //The aspect ratio between the width and height of the screen.
        private float _aspect;

        //the distance from the camera location to the plane.
        private float _distance;

        //precalculated to increase performance.
        private Vector3 _topLeftDirection;

        public Camera(Vector3 location, Vector3 direction, float aspect, float distance = 1)
        {
            _position = location;
            _aspect = aspect;
            _distance = distance;
            _direction = direction;
            CalculateBotLeftCorner();
        }

        public void LookAt(Vector3 location)
        {
            _direction = location - _position;
            _direction.Normalize();

            CalculateBotLeftCorner();
        }

        private void CalculateBotLeftCorner()
        {
            _topLeftDirection = _direction * _distance + new Vector3(-0.5f, 0, _aspect / 2);
        }

        public void Transform(Matrix4 M)
        {
            // Matrix4 n = new Vector4(_pos,1.0f) * M);
        }

        public Vector3 GetRayDirection(float x, float y)
        {
            Vector3 dir = _topLeftDirection + new Vector3(x, 0, -_aspect * y);
            dir.Normalize();
            return dir ;
        }

        public Vector3 GetPosition()
        {
            return _position;
        }

        public void ChangePosition(Vector3 newPosition)
        {
            _position = newPosition;
            CalculateBotLeftCorner();
        }

        public void MovePosition(Vector3 direction)
        {
            _position += direction;
            CalculateBotLeftCorner();
        }

        public static float GetAspectRatio(float width, float height)
        {
            return height / width;
        }
    }
}