using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_MPT_Lab2_GeneticAlgorithm.Extensions
{
    internal static class RichTextBoxExtensions
    {
        public static void AppendText(this RichTextBox box, string text, Color color, bool onNewLine)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionBackColor = color;

            if (onNewLine)
                box.AppendText($"{text}\r\n");
            else
                box.AppendText(text);
        }
    }
}
