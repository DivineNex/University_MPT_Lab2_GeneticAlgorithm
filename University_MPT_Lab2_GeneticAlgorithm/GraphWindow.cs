using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_MPT_Lab2_GeneticAlgorithm
{
    internal class GraphWindow : GameWindow
    {
        private int _vertexBufferObject;
        private int _vertexArrayObject;
        private int _elementBufferObject;

        private Shader _shader;
        private Camera _camera;

        private bool _firstMove = true;
        private Vector2 _lastPos;
        private double _time;

        private float[] _vertices;
        private uint[] _indices;

        public GraphWindow(int width, int height, string title, List<Point3D> points)
            : base(GameWindowSettings.Default, new NativeWindowSettings()
            { 
                Size = (width, height), 
                Title = title
            })
        {
            _shader = new Shader(@"..\..\..\shader.vert", @"..\..\..\shader.frag");
            _camera = new Camera(new Vector3(0, 1, 2), Size.X / (float)Size.Y);
            CursorState = CursorState.Grabbed;

            var normalizedPoints = NormalizePoints(points);
            PointsToVertices(normalizedPoints, out _vertices);
        }

        private List<Point3D> NormalizePoints(List<Point3D> points)
        {
            double maxX = points[0].X;
            double minX = points[0].X;
            double maxY = points[0].Y;
            double minY = points[0].Y;
            double maxZ = points[0].Z;
            double minZ = points[0].Z;

            for (int i = 1; i < points.Count; i++)
            {
                if (points[i].X > maxX) maxX = points[i].X;
                if (points[i].X < minX) minX = points[i].X;
                if (points[i].Y > maxY) maxY = points[i].Y;
                if (points[i].Y < minY) minY = points[i].Y;
                if (points[i].Z > maxZ) maxZ = points[i].Z;
                if (points[i].Z < minZ) minZ = points[i].Z;
            }

            var coeff = new List<double> { maxX, maxY, maxZ }.Max();

            var result = new List<Point3D>();

            for (int i = 0; i < points.Count; i++)
                result.Add(new Point3D(points[i].X/coeff, points[i].Y/coeff, points[i].Z/coeff));

            return result;
        }

        private void PointsToVertices(List<Point3D> points, out float[] vertices)
        {
            var verticesList = new List<float>();
            verticesList.Capacity = points.Count * 3;

            for (int i = 0; i < points.Count; i++)
            {
                //Y и Z поменяны местами специально, для поворота всей поверхности
                verticesList.Add((float)points[i].X);
                verticesList.Add((float)points[i].Z);
                verticesList.Add((float)points[i].Y);
            }

            vertices = verticesList.ToArray();
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            //задаем цвет очистки
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            //генерируем буфер VBO
            _vertexBufferObject = GL.GenBuffer();

            //биндим буфер VBO и задаем его размер
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StreamDraw);

            //генерируем буфер VAO и биндим его
            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

            //генерируем EBO и биндим его
            //_elementBufferObject = GL.GenBuffer();
            //GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
            //GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StreamDraw);

            _shader.Use();

            //создаем указатель на позицию вершин и включаем атрибут
            var vertexLocation = _shader.GetAttribLocation("aPosition");
            GL.EnableVertexAttribArray(vertexLocation);
            GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, true, 3 * sizeof(float), 0);

            ////создаем указатель на цвет вершин и включаем атрибут
            //var colorLocation = _shader.GetAttribLocation("aColor");
            //GL.EnableVertexAttribArray(colorLocation);
            //GL.VertexAttribPointer(colorLocation, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            _time += 4.0 * e.Time;

            GL.Clear(ClearBufferMask.ColorBufferBit);

            _shader.Use();

            var model = Matrix4.Identity * Matrix4.CreateRotationY((float)MathHelper.DegreesToRadians(_time));
            _shader.SetMatrix4("model", model);
            _shader.SetMatrix4("view", _camera.GetViewMatrix());
            _shader.SetMatrix4("projection", _camera.GetProjectionMatrix());

            GL.BindVertexArray(_vertexArrayObject);

            GL.PointSize(3f);

            GL.DrawArrays(PrimitiveType.Points, 0, _vertices.Length / 3);

            //GL.DrawElements(PrimitiveType.Points, _indices.Length, DrawElementsType.UnsignedInt, 0);

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (!IsFocused) // Check to see if the window is focused
            {
                return;
            }

            KeyboardState input = KeyboardState;

            if (input.IsKeyPressed(OpenTK.Windowing.GraphicsLibraryFramework.Keys.Escape))
            {
                Close();
            }
            if (input.IsKeyPressed(OpenTK.Windowing.GraphicsLibraryFramework.Keys.F))
            {
                if (WindowState == WindowState.Normal)
                    this.WindowState = WindowState.Fullscreen;
                else if (WindowState == WindowState.Fullscreen)
                    this.WindowState = WindowState.Normal;
            }

            const float cameraSpeed = 1.5f;
            const float sensitivity = 0.2f;

            if (input.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.W))
            {
                _camera.Position += _camera.Front * cameraSpeed * (float)e.Time; // Forward
            }
            if (input.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.S))
            {
                _camera.Position -= _camera.Front * cameraSpeed * (float)e.Time; // Backwards
            }
            if (input.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.A))
            {
                _camera.Position -= _camera.Right * cameraSpeed * (float)e.Time; // Left
            }
            if (input.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.D))
            {
                _camera.Position += _camera.Right * cameraSpeed * (float)e.Time; // Right
            }
            if (input.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.Space))
            {
                _camera.Position += _camera.Up * cameraSpeed * (float)e.Time; // Up
            }
            if (input.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.LeftShift))
            {
                _camera.Position -= _camera.Up * cameraSpeed * (float)e.Time; // Down
            }

            var mouse = MouseState;

            if (_firstMove)
            {
                _lastPos = new Vector2(mouse.X, mouse.Y);
                _firstMove = false;
            }
            else
            {
                var deltaX = mouse.X - _lastPos.X;
                var deltaY = mouse.Y - _lastPos.Y;
                _lastPos = new Vector2(mouse.X, mouse.Y);

                _camera.Yaw += deltaX * sensitivity;
                _camera.Pitch -= deltaY * sensitivity;
            }
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);

            _camera.Fov -= e.OffsetY;
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, e.Width, e.Height);

            _camera.AspectRatio = Size.X / (float)Size.Y;
        }

        protected override void OnUnload()
        {
            base.OnUnload();

            CursorState = CursorState.Normal;
        }
    }
}
