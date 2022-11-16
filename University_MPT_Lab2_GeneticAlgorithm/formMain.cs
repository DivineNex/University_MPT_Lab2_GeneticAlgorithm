using OpenTK.Windowing.Desktop;
using System.Windows.Forms;
using University_MPT_Lab2_GeneticAlgorithm.Extensions;

namespace University_MPT_Lab2_GeneticAlgorithm
{
    public partial class formMain : Form
    {
        private delegate void MessageHandler(string message);

        private Color _lastLoggerColor = Color.White;
        private List<Point3D> _points;
        private int _surfaceLength;
        private int _surfaceWidth;

        public formMain()
        {
            InitializeComponent();
        }

        private void formMain_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_points != null)
            {
                NativeWindowSettings nativeWindowSettings = new NativeWindowSettings()
                {
                    Size = new OpenTK.Mathematics.Vector2i(1280, 720),
                    Title = "3D graph",
                    APIVersion = new Version(3, 3)
                };

                using (GraphWindow graph = new GraphWindow(GameWindowSettings.Default, 
                    nativeWindowSettings, _points, _surfaceLength, _surfaceWidth))
                {
                    graph.Run();
                }
                Close();
            }
            else
            {
                Log("Points not created!");
            }
        }

        private List<Point3D> Calculate(double xMin, double xMax, 
            double yMin, double yMax, double step, MessageHandler? messageHandler)
        {
            List<Point3D> result = new List<Point3D>();

            for (double x = xMin; Math.Round(x, 2) <= xMax; x += step)
            {
                for (double y = yMin; Math.Round(y, 2) <= yMax; y += step)
                {
                    double z = Math.Sin(x) + Math.Cos(y);
                    //double z = Math.Sin(10*(Math.Pow(x,2) + Math.Pow(y,2)))/10;
                    result.Add(new Point3D(x, y, z));
                }
            }

            _surfaceLength = (int)((xMax - xMin) / step + 1);
            _surfaceWidth = (int)((yMax - yMin) / step + 1);

            messageHandler?.Invoke($"{result.Count} points have been calculated");
            return result;
        }

        private void Log(string message)
        {
            if (_lastLoggerColor == Color.White)
            {
                rtbLog.AppendText(message, Color.LightGray, true);
                _lastLoggerColor = Color.LightGray;
            }
            else
            {
                rtbLog.AppendText(message, Color.White, true);
                _lastLoggerColor = Color.White;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClearPoints(Log);

            _points = Calculate((double)nudXMin.Value,
                                (double)nudXMax.Value,
                                (double)nudYMin.Value,
                                (double)nudYMax.Value,
                                (double)nudStep.Value, Log);
        }

        private void rtbLog_TextChanged(object sender, EventArgs e)
        {
            rtbLog.SelectionStart = rtbLog.Text.Length;
            rtbLog.ScrollToCaret();
        }

        private void ClearPoints(MessageHandler? messageHandler)
        {
            if (_points != null)
            {
                _points.Clear();
                messageHandler?.Invoke("Points cleared");
            }
        }

        private void bClearLog_Click(object sender, EventArgs e)
        {
            rtbLog.Clear();
        }
    }
}