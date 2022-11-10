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
            splitCVerticalLevel1.SplitterDistance = splitCVerticalLevel1.Width / 8;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (GraphWindow graph = new GraphWindow(800, 600, "3D graph"))
            {
                graph.Run();
            }
        }
    }
}