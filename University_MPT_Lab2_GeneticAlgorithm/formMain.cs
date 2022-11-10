namespace University_MPT_Lab2_GeneticAlgorithm
{
    public partial class formMain : Form
    {
        public formMain()
        {
            InitializeComponent();
        }

        private void formMain_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (GraphWindow graph = new GraphWindow(800, 600, "3D graph"))
            {
                graph.Run();
            }
        }

        private List<Point3D> Calculate(int xMin, int xMax, int yMin, int yMax, int step)
        {
            List<Point3D> result = new List<Point3D>();

            for (int x = xMin; x <= xMax; x += step)
            {
                for (int y = yMin; y <= yMax; y += step)
                {
                    double z = Math.Sin(x) + Math.Cos(y);
                    result.Add(new Point3D(x, y, z));
                }
            }

            return result;
        }
    }
}