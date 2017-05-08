using System;
using OpenTK;

namespace Template
{
    public class Camera
    {
        private Vector3 _pos;
        private Vector3 _dir;
        private Vector3[] _spCorners;

        public Camera(Vector3 pos, Vector3 dir)
        {
            _pos = pos;
            _dir = dir;
            CalculateCorners();
        }

        public void LookAt(Vector3 p)
        {
            _dir = p - _pos;
            _dir.Normalize();
            CalculateCorners();
        }

        private void CalculateCorners()
        {
            Vector3 _spCenter = _pos + _dir;
            _spCorners = new Vector3[3];
            _spCorners[0] = _spCenter + new Vector3(-1.0f, -1.0f, 0.0f);
            _spCorners[1] = _spCenter + new Vector3(1.0f, -1.0f, 0.0f);
            _spCorners[2] = _spCenter + new Vector3(-1.0f, 1.0f, 0.0f);
        }

        public void Transform(Matrix4 M)
        {
            // Matrix4 n = new Vector4(_pos,1.0f) * M);
        }

        public Vector3 GetRayDirection(float u, float v)
        {
            Vector3 dir = (new Vector3(_spCorners[0] + u * (_spCorners[1] - _spCorners[0]) + v * (_spCorners[2] - _spCorners[0])) - _pos);
            dir.Normalize();
            return dir ;
        }

        public Vector3 GetPosition()
        {
            return _pos;
        }
    }
}