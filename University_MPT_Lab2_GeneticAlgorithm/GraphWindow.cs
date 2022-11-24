using ImGuiNET;
using Microsoft.VisualBasic.Logging;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Keys = OpenTK.Windowing.GraphicsLibraryFramework.Keys;

namespace University_MPT_Lab2_GeneticAlgorithm
{
    internal class GraphWindow : GameWindow
    {
        private GeneticAlgorithm _geneticAlgorithm;

        private int _minX;
        private int _maxX;
        private int _minY;
        private int _maxY;
        private float _step;
        private List<Point3D> _surfacePoints;
        private int _surfaceLength;
        private int _surfaceWidth;

        private int _vertexBufferObject;
        private int _vertexArrayObject;
        private int _elementBufferObject;

        private Shader _shader;
        private Camera _camera;
        private ImGuiController _guiController;

        private bool _firstMove = true;
        private Vector2 _lastPos;
        private double _time;
        private float minNormalizedZ;
        private float maxNormalizedZ;

        private float[] _vertices;
        private uint[] _indices;

        private static bool no_titlebar = false;
        private static bool no_scrollbar = false;
        private static bool no_move = false;
        private static bool no_resize = false;
        private static bool no_collapse = false;
        private static bool no_nav = false;
        private static bool no_background_in_view_mode = true;
        private static bool no_bring_to_front = false;

        private static System.Numerics.Vector4 _minimumGradientColor = new System.Numerics.Vector4(0.0f, 0.0f, 0.5f, 1.0f);
        private static System.Numerics.Vector4 _maximumGradientColor = new System.Numerics.Vector4(0.7f, 0.0f, 0.5f, 1.0f);

        private eControlMode _mode = eControlMode.Setup;
        private static int _renderPrimitive = 1; // 0 - points, 1 - triangles
        private static bool _metricsShow = true;
        private float[] _lastFramesMs = new float[20];
        private byte _counterForAverageMs = 0;
        private float _lastAverageMs = 0f;
        private Stopwatch _frameStopwatch = new Stopwatch();
        private static bool _rotationByY = true;
        private static bool _showGui = true;

        private double[] _gaValues = new double[2];
        private double _gaFitness = 0;
        private double _gaXoverRate = 0.8;
        private double _gaMutRate = 0.5;
        private int _gaPopulationSize = 100;
        private int _gaGenerationSize = 2000;
        private int _gaChromosomeLength = 2;

        public GraphWindow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
            _shader = new Shader(@"..\..\..\shader.vert", @"..\..\..\shader.frag");
            _camera = new Camera(new Vector3(0, 0.7f, 2), Size.X / (float)Size.Y);
            _camera.Pitch = -24; // start camera pitch (rotation by x axis)
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

            maxNormalizedZ = (float)(maxZ / coeff);
            minNormalizedZ = (float)(minZ / coeff);
            return result;
        }

        private void PointsToVertices(List<Point3D> points, out float[] vertices)
        {
            var verticesList = new List<float>();
            verticesList.Capacity = points.Count * 6;

            for (int i = 0; i < points.Count; i++)
            {
                //Y и Z поменяны местами специально, для поворота всей поверхности
                //position
                verticesList.Add((float)points[i].X);
                verticesList.Add((float)points[i].Z);
                verticesList.Add((float)points[i].Y);

                verticesList.Add(InterpolateFloatNumber(minNormalizedZ, 
                    _minimumGradientColor.X, maxNormalizedZ, 
                    _maximumGradientColor.X, (float)points[i].Z)); //r
                verticesList.Add(InterpolateFloatNumber(minNormalizedZ, 
                    _minimumGradientColor.Y, maxNormalizedZ, 
                    _maximumGradientColor.Y, (float)points[i].Z)); //g
                verticesList.Add(InterpolateFloatNumber(minNormalizedZ, 
                    _minimumGradientColor.Z, maxNormalizedZ, 
                    _maximumGradientColor.Z, (float)points[i].Z)); //b
            }
            vertices = verticesList.ToArray();
        }

        private void GenerateIndicesForSurface(int surfaceLenght, int surfaceWidth)
        {
            List<uint> indices = new List<uint>();
            indices.Capacity = (surfaceWidth - 1) * (surfaceWidth - 1) * 6;

            int pointCount = _vertices.Length / 2 / 3;

            for (int i = 0; i < pointCount- surfaceWidth; i++)
            {
                if ((i + 1) % surfaceWidth != 0)
                {
                    indices.Add((uint)i);
                    indices.Add((uint)i + 1);
                    indices.Add((uint)(i + surfaceWidth));

                    indices.Add((uint)i + 1);
                    indices.Add((uint)(i + surfaceWidth + 1));
                    indices.Add((uint)(i + surfaceWidth));
                }
            }

            _indices = indices.ToArray();
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            InitValues();

            Title += ": OpenGL Version: " + GL.GetString(StringName.Version);
            _guiController = new ImGuiController(ClientSize.X, ClientSize.Y);

            //задаем цвет очистки
            GL.ClearColor(0.5f, 0.7f, 0.7f, 1.0f);

            //генерируем буфер VBO и биндим его
            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);

            //генерируем буфер VAO и биндим его
            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

            //генерируем EBO и биндим его
            _elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);

            //создаем указатель на позицию вершин и включаем атрибут
            var vertexLocation = _shader.GetAttribLocation("aPosition");
            GL.EnableVertexAttribArray(vertexLocation);
            GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, true, 6 * sizeof(float), 0);

            //создаем указатель на цвет вершин и включаем атрибут
            var colorLocation = _shader.GetAttribLocation("aColor");
            GL.EnableVertexAttribArray(colorLocation);
            GL.VertexAttribPointer(colorLocation, 3, VertexAttribPointerType.Float, true, 6 * sizeof(float), 3 * sizeof(float));

            _shader.Use();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            _frameStopwatch.Restart();

            base.OnRenderFrame(e);

            _guiController.Update(this, (float)e.Time);

            if (_rotationByY)
                _time += 4.0 * e.Time;

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);

            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Lequal);

            _shader.Use();

            var model = Matrix4.Identity * Matrix4.CreateRotationY((float)MathHelper.DegreesToRadians(_time));

            _shader.SetMatrix4("model", model);
            _shader.SetMatrix4("view", _camera.GetViewMatrix());
            _shader.SetMatrix4("projection", _camera.GetProjectionMatrix());

            GL.BindVertexArray(_vertexArrayObject);

            GL.PointSize(3f);

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            if (_renderPrimitive == 0) //points
            {
                GL.DrawArrays(PrimitiveType.Points, 0, _vertices.Length / 6);
            }
            else if (_renderPrimitive == 1) //triangles
            {
                GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
            }

            if (_showGui)
                ShowImGui();
            //ImGui.ShowDemoWindow();

            _guiController.Render();
            ImGuiController.CheckGLError("End of frame");

            SwapBuffers();

            _frameStopwatch.Stop();
            if (_counterForAverageMs == 20)
            {
                _counterForAverageMs = 0;
                _lastAverageMs = _lastFramesMs.Sum() / 20;
            }
            _lastFramesMs[_counterForAverageMs] = _frameStopwatch.ElapsedTicks / 10000;
            _counterForAverageMs++;
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (!IsFocused)
                return;

            KeyboardState input = KeyboardState;

            if (input.IsKeyPressed(Keys.F4))
                Close();
            if (input.IsKeyPressed(Keys.F1))
                SwitchControlMode();
            if (input.IsKeyPressed(Keys.F2))
            {
                if (WindowState == WindowState.Normal)
                    this.WindowState = WindowState.Fullscreen;
                else if (WindowState == WindowState.Fullscreen)
                    this.WindowState = WindowState.Normal;
            }
            if (input.IsKeyPressed(Keys.F3))
                _showGui = !_showGui;

            if (_mode == eControlMode.View)
            {
                const float cameraSpeed = 1.5f;
                const float sensitivity = 0.2f;

                if (input.IsKeyDown(Keys.W))
                    _camera.Position += _camera.Front * cameraSpeed * (float)e.Time; // Forward
                if (input.IsKeyDown(Keys.S))
                    _camera.Position -= _camera.Front * cameraSpeed * (float)e.Time; // Backwards
                if (input.IsKeyDown(Keys.A))
                    _camera.Position -= _camera.Right * cameraSpeed * (float)e.Time; // Left
                if (input.IsKeyDown(Keys.D))
                    _camera.Position += _camera.Right * cameraSpeed * (float)e.Time; // Right
                if (input.IsKeyDown(Keys.Space))
                    _camera.Position += _camera.Up * cameraSpeed * (float)e.Time; // Up
                if (input.IsKeyDown(Keys.LeftShift))
                    _camera.Position -= _camera.Up * cameraSpeed * (float)e.Time; // Down

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

                    if (Math.Abs(deltaX) < 100 && Math.Abs(deltaY) < 100)
                    {
                        _camera.Yaw += deltaX * sensitivity;
                        _camera.Pitch -= deltaY * sensitivity;
                    }
                }
            }
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);

            if (_mode == eControlMode.Setup)
                _guiController.MouseScroll(e.Offset);
            else if (_mode == eControlMode.View)
                _camera.Fov -= e.OffsetY;
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, ClientSize.X, ClientSize.Y);
            _guiController.WindowResized(ClientSize.X, ClientSize.Y);

            _camera.AspectRatio = Size.X / (float)Size.Y;
        }

        protected override void OnUnload()
        {
            base.OnUnload();

            CursorState = CursorState.Normal;
        }

        private float InterpolateFloatNumber(float x1, float y1, float x2, float y2, float x3)
        {
            return y2 + ((y1 - y2) / (x1 - x2)) * (x3 - x2);
        }

        protected override void OnTextInput(TextInputEventArgs e)
        {
            base.OnTextInput(e);

            _guiController.PressChar((char)e.Unicode);
        }

        private void SwitchControlMode()
        {
            if (_mode == eControlMode.Setup)
            {
                _mode = eControlMode.View;
                CursorState = CursorState.Grabbed;
            }
            else if (_mode == eControlMode.View)
            {
                _mode = eControlMode.Setup;
                CursorState = CursorState.Normal;
            }
        }

        private void InitValues()
        {
            _minX = -10;
            _maxX = 10;
            _minY = -10;
            _maxY = 10;
            _step = 0.1f;
            _vertices = new float[0];
            _indices = new uint[0];
            _surfacePoints = new List<Point3D>(100000);
        }

        private void Rebuild()
        {
            var capacity = ((_maxX - _minX) / Math.Round(_step, 2) + 1) * ((_maxX - _minX) / Math.Round(_step, 2) + 1);
            _surfacePoints = new List<Point3D>((int)capacity);
            _vertices = new float[0];
            _indices = new uint[0];

            CalculatePoints(_minX, _maxX, _minY, _maxY, Math.Round(_step, 2));
            var normalizedPoints = NormalizePoints(_surfacePoints);
            PointsToVertices(normalizedPoints, out _vertices);
            GenerateIndicesForSurface(_surfaceLength, _surfaceWidth);

            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StreamDraw);
            GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StreamDraw);
        }

        private void CalculatePoints(double xMin, double xMax,
            double yMin, double yMax, double step)
        {
            for (double x = xMin; x <= xMax; x += step)
            {
                for (double y = yMin; y <= yMax; y += step)
                {
                    double z = Math.Sin(x) + Math.Cos(y);
                    //double z = Math.Sin(10*(Math.Pow(x,2) + Math.Pow(y,2)))/10;
                    _surfacePoints.Add(new Point3D(x, y, z));
                }
            }

            _surfaceLength = (int)((xMax - xMin) / step + 1);
            _surfaceWidth = (int)((yMax - yMin) / step + 1);
        }

        private void ShowImGui()
        {
            ImGuiWindowFlags window_flags = 0;
            if (no_titlebar) window_flags |= ImGuiWindowFlags.NoTitleBar;
            if (no_scrollbar) window_flags |= ImGuiWindowFlags.NoScrollbar;
            if (no_move) window_flags |= ImGuiWindowFlags.NoMove;
            if (no_resize) window_flags |= ImGuiWindowFlags.NoResize;
            if (no_collapse) window_flags |= ImGuiWindowFlags.NoCollapse;
            if (no_nav) window_flags |= ImGuiWindowFlags.NoNav;
            if (no_background_in_view_mode && _mode == eControlMode.View) window_flags |= ImGuiWindowFlags.NoBackground;
            if (no_bring_to_front) window_flags |= ImGuiWindowFlags.NoBringToFrontOnFocus;

            ImGui.SetNextWindowPos(new System.Numerics.Vector2(3, 3), ImGuiCond.Once);
            ImGui.SetNextWindowSize(new System.Numerics.Vector2(450, ClientSize.Y - 6), ImGuiCond.Once);

            ImGui.PushStyleVar(ImGuiStyleVar.WindowRounding, 10.0f);
            ImGui.Begin("Genetic Algorithm", window_flags);

            ImGui.PushItemWidth(ImGui.GetFontSize() * -12);

            if (ImGui.CollapsingHeader("Information"))
            {
                ImGui.Text("About this program:");
                ImGui.Text("With this program, you can build a 3D surface by function,\n" +
                    "adjust the ranges, step, color gradient of the graph.");
                ImGui.Text("Using a genetic algorithm, you can find the minimum\n" +
                    "or maximum of a function on given ranges.");
                ImGui.Separator();

                ImGui.Text("GUI guide:");
                ImGui.BulletText("The \"Information\" tab contains basic information,\n" +
                    "as well as control keys");
                ImGui.BulletText("The \"Function parameters\" tab contains settings\n" +
                    "for the ranges and step of the function");
                ImGui.BulletText("The \"Genetic Algorithm\" tab contains settings\n" +
                    "and control of the genetic algorithm");
                ImGui.BulletText("The \"Settings\" tab contains general settings and\n" +
                    "3D graph style settings");
                ImGui.Separator();

                ImGui.Text("Keys guide:");
                ImGui.BulletText("F1  - switch between setup mode and view mode");
                ImGui.BulletText("F2  - toggle fullscreen mode");
                ImGui.BulletText("F3  - show/hide GUI");
                ImGui.BulletText("F4  - close program");
                ImGui.BulletText("In View mode W, A, S, D, LShift, Space - camera movement");
                ImGui.BulletText("In View mode Mouse Wheel - change camera FOV");
                ImGui.Separator();

                if (ImGui.TreeNode("User guide"))
                    ImGui.ShowUserGuide();
            }

            if (ImGui.CollapsingHeader("Function parameters"))
            {
                ImGui.TextColored(new System.Numerics.Vector4(0.3f, 0.7f, 0.5f, 1.0f), "z(x,y) = sin(x)+cos(y)");
                ImGui.Separator();

                ImGui.InputInt("Min X", ref _minX, 1);
                ImGui.InputInt("Max X", ref _maxX, 1);
                ImGui.InputInt("Min Y", ref _minY, 1);
                ImGui.InputInt("Max Y", ref _maxY, 1);
                ImGui.InputFloat("Step", ref _step, 0.01f);

                if (ImGui.Button("Rebuild"))
                    Rebuild();
            }

            if (ImGui.CollapsingHeader("Genetic algorithm"))
            {
                ImGui.InputDouble("Crossover rate", ref _gaXoverRate, 0.1);
                ImGui.InputDouble("Mutation rate", ref _gaMutRate, 0.1);
                ImGui.InputInt("Population size", ref _gaPopulationSize, 1);
                ImGui.InputInt("Generation size", ref _gaGenerationSize, 1);
                ImGui.InputInt("Chromosome length", ref _gaChromosomeLength, 1);

                if (ImGui.Button("Launch GA"))
                    LaunchGA();

                ImGui.Separator();
                ImGui.TextColored(new System.Numerics.Vector4(255, 255, 255, 255), "Result:");
                ImGui.Text($"X-coord = {_gaValues[0]}");
                ImGui.Text($"Y-coord = {_gaValues[1]}");
                ImGui.Text($"Value (Z) = {_gaFitness}");
            }

            if (ImGui.CollapsingHeader("Settings"))
            {
                if (ImGui.TreeNode("General"))
                {
                    ImGui.Text("GUI window settings");
                    if (ImGui.BeginTable("split", 3))
                    {
                        ImGui.TableNextColumn(); ImGui.Checkbox("No titlebar", ref no_titlebar);
                        ImGui.TableNextColumn(); ImGui.Checkbox("No scrollbar", ref no_scrollbar);
                        ImGui.TableNextColumn(); ImGui.Checkbox("No move", ref no_move);
                        ImGui.TableNextColumn(); ImGui.Checkbox("No resize", ref no_resize);
                        ImGui.TableNextColumn(); ImGui.Checkbox("No collapse", ref no_collapse);
                        ImGui.TableNextColumn(); ImGui.Checkbox("No nav", ref no_nav);
                        ImGui.EndTable();
                    }
                    ImGui.Separator();
                    ImGui.Checkbox("Hide GUI background in View mode", ref no_background_in_view_mode);
                    ImGui.Checkbox("Show metrics window", ref _metricsShow);
                    ImGui.Checkbox("Rotation by Y axis", ref _rotationByY);

                    ImGui.TreePop();
                }
                if (ImGui.TreeNode("Style"))
                {
                    float w = (ImGui.GetContentRegionAvail().X) * 0.50f;
                    if (ImGui.BeginTable("split1", 2))
                    {
                        ImGui.TableNextColumn();
                        ImGui.SetNextItemWidth(w);
                        ImGui.Text("Minimum gradient color");
                        ImGui.SetNextItemWidth(w);
                        ImGui.ColorPicker4("", ref _minimumGradientColor, ImGuiColorEditFlags.PickerHueWheel | ImGuiColorEditFlags.NoSidePreview | ImGuiColorEditFlags.NoInputs | ImGuiColorEditFlags.NoAlpha);

                        ImGui.TableNextColumn();
                        ImGui.SetNextItemWidth(w);
                        ImGui.Text("Maximum gradient color");
                        ImGui.SetNextItemWidth(w);
                        ImGui.ColorPicker4("", ref _maximumGradientColor, ImGuiColorEditFlags.PickerHueWheel | ImGuiColorEditFlags.NoSidePreview | ImGuiColorEditFlags.NoInputs | ImGuiColorEditFlags.NoAlpha);

                        ImGui.EndTable();
                    }

                    if (ImGui.Button("Rebuild"))
                        Rebuild();

                    ImGui.Spacing();

                    ImGui.Text("Render primitives");
                    ImGui.RadioButton("Points", ref _renderPrimitive, 0); 
                    ImGui.SameLine();
                    ImGui.RadioButton("Triangles", ref _renderPrimitive, 1);

                    ImGui.TreePop();
                }
            }

            ImGui.End();

            if (_metricsShow)
            {
                ImGui.SetNextWindowPos(new System.Numerics.Vector2(ClientSize.X-263, 3), ImGuiCond.Always);
                ImGui.SetNextWindowSize(new System.Numerics.Vector2(260, 170), ImGuiCond.Once);

                var metrics_window_flags = ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoResize;
                if (no_background_in_view_mode && _mode == eControlMode.View)
                    metrics_window_flags |= ImGuiWindowFlags.NoBackground;

                ImGui.Begin("Metrics", metrics_window_flags);

                ImGui.Text($"Average - for the last 20 frames");
                ImGui.Separator();

                ImGui.Text($"Average ms/frame = {_lastAverageMs}");
                ImGui.Text($"Average FPS = {1000 / _lastAverageMs}");
                ImGui.Separator();
                ImGui.Text($"Vertices = {_vertices.Length / 2}");
                ImGui.Text($"Indices = {_indices.Length}");
                ImGui.Text($"Points = {_vertices.Length / 2 / 3}");
                ImGui.Text($"Triangles = {_indices.Length / 3}");

                ImGui.End();
            }
        }

        private double GAFunction(double[] values)
        {
            if (values.GetLength(0) != 2)
                throw new Exception("should only have 2 args");

            double x = values[0]; 
            double y = values[1];

            return Math.Sin(x) + Math.Cos(y);
        }

        private void LaunchGA()
        {
            _geneticAlgorithm = new GeneticAlgorithm(0.8, 0.5, 100, 2000, 2);
            _geneticAlgorithm.FitnessFunction = GAFunction;
            _geneticAlgorithm.Elitism = true;
            _geneticAlgorithm.Launch();

            _geneticAlgorithm.GetBestValues(out _gaValues, out _gaFitness);
        }
    }
}
