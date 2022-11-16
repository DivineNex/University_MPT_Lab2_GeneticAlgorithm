using OpenTK.Windowing.Desktop;
using System.Windows.Forms;
using University_MPT_Lab2_GeneticAlgorithm.Extensions;

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
            NativeWindowSettings nativeWindowSettings = new NativeWindowSettings()
            {
                Size = new OpenTK.Mathematics.Vector2i(1280, 720),
                Title = "Genetic Algorithm",
                APIVersion = new Version(3, 3)
            };

            using (GraphWindow graph = new GraphWindow(GameWindowSettings.Default, nativeWindowSettings))
            {
                graph.Run();
            }
            Close();
        }
    }
}