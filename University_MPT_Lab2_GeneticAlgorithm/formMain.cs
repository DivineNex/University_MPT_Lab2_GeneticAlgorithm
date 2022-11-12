using System.Windows.Forms;
using University_MPT_Lab2_GeneticAlgorithm.Extensions;

namespace University_MPT_Lab2_GeneticAlgorithm
{
    public partial class formMain : Form
    {
        private delegate void MessageHandler(string message);

        private Color _lastLoggerColor = Color.White;
        private List<Point3D> _points;

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
                using (GraphWindow graph = new GraphWindow(800, 600, "3D graph", _points))
                {
                    graph.Run();
                }
            }
        }

        private List<Point3D> Calculate(double xMin, double xMax, 
            double yMin, double yMax, double step, MessageHandler? messageHandler)
        {
            List<Point3D> result = new List<Point3D>();

            for (double x = xMin; x <= xMax; x += step)
            {
                for (double y = yMin; y <= yMax; y += step)
                {
                    double z = Math.Sin(x) + Math.Cos(y);
                    result.Add(new Point3D(x, y, z));
                }
            }

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