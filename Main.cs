using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;

namespace BinaryTree
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private BinaryTree _tree;

        void PaintTree()
        {
            if (_tree == null) return;
            pictureBox1.Image = _tree.Draw();
        }

        private void armarArbol(int personas)
        {

            List<int> lista = new List<int>();
            // 
            //Random rnd = new Random(); 
            for (int i = 1; i <= personas; i++)
            {
                //lista.Add(rnd.Next(100));
                lista.Add(i);
            }
            _tree = cargarArbolBinario(lista);
            PaintTree();

        }




        private void Form1_Load(object sender, EventArgs e)
        {
            // Se crea un nuevo arbol
            _tree = new BinaryTree();
            // Armo Arbol
            armarArbol(63);
        }

        private Node cargarArbolBinario(Node nodo, List<int> lista, int cantidad)
        {
            if (cantidad > 0)
            {
                int left, right, middle = cantidad / 2;
                int valor = lista[middle];
                nodo = new Node(valor);
                left = middle;
                right = cantidad - middle - 1;
                nodo.Left = cargarArbolBinario(nodo.Left, lista, left);
                nodo.Right = cargarArbolBinario(nodo.Right, lista.GetRange(middle + 1, lista.Count - middle - 1), right);
                return nodo;
            }
            return null;
        }

        private BinaryTree cargarArbolBinario(List<int> list)
        {
            Node root = cargarArbolBinario(null, list, list.Count);
            BinaryTree arbol = new BinaryTree();
            arbol.Add(root);
            return arbol;
        }


        public static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats
            var codecs = ImageCodecInfo.GetImageEncoders();
            // Find the correct image codec
            return codecs.FirstOrDefault(t => t.MimeType == mimeType);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            const double quality = 1;
            var d = new SaveFileDialog { Filter = @"jpeg files|*.jpg" };
            try
            {
                if (d.ShowDialog() != DialogResult.OK)
                    return;
                var bmp = pictureBox1.Image;
                var qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality,
                                                                     (long)(quality * 75));
                // Jpeg image codec
                var jpegCodec = GetEncoderInfo("image/jpeg");
                if (jpegCodec == null)
                    return;
                var encoderParams = new EncoderParameters(1);
                encoderParams.Param[0] = qualityParam;
                bmp.Save(d.FileName, jpegCodec, encoderParams);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Int32.TryParse(textBox1.Text, out int result))
            {
                Node aux = _tree.BuscarKesimo(_tree.RootNode, (Int32.Parse(textBox1.Text)));
                label1.Text = aux.Value.ToString();
            }

        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (Int32.TryParse(textBox2.Text, out int result))
            {
                armarArbol(Int32.Parse(textBox2.Text));
            }



        }

    }
}
