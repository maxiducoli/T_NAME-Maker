using System.Drawing.Imaging;
using bmptim;
using WEUtils;

namespace T_NAME_Maker
{
    public partial class frmT_NAME : Form
    {
        public frmT_NAME()
        {
            InitializeComponent();
            InitializeComponents();
        }

        private void btnClearTeam_Click(object sender, EventArgs e)
        {
            ClearTextBoxs("Team");
        }

        private void ClearTextBoxs(string partialName)
        {
            foreach (Control control in this.Controls)
            {
                if (control is GroupBox && control.Name.Contains(partialName))
                {
                    foreach (Control ctrl in control.Controls)
                    {
                        if (ctrl is TextBox)
                            ctrl.Text = string.Empty;
                    }
                }
            }
            pbTname.Image = null;
        }

        private void btnClearStadium_Click(object sender, EventArgs e)
        {
            ClearTextBoxs("Stadium");
        }

        private void rbTeam_CheckedChanged(object sender, EventArgs e)
        {
            gbStadium.Enabled = !rbTeam.Checked;
            gbTeams.Enabled = rbTeam.Checked;
            InitializeComponents();
        }

        private void rbStadium_CheckedChanged(object sender, EventArgs e)
        {
            gbTeams.Enabled = !rbStadium.Checked;
            gbStadium.Enabled = rbStadium.Checked;
            InitializeComponents();
        }

        private void InitializeComponents()
        {

            foreach (Control control in this.Controls)
            {
                if (control is GroupBox)
                {
                    foreach (Control ctrl in control.Controls)
                    {
                        if (ctrl is TextBox)
                        {
                            ctrl.Text = string.Empty;
                        }
                    }
                }
            }
            txtExample.Font = txtTeam01.Font;
            txtExample.ForeColor = lblTextColor.BackColor;
            txtExample.Text = "This is an example text.Winning Eleven 2002";
            pbTname.Image = null;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void SelectFont()
        {
            using (FontDialog fd = new FontDialog())
            {
                if (fd.ShowDialog() == DialogResult.OK)
                {
                    foreach (Control control in this.Controls)
                    {
                        if (control is GroupBox)
                        {
                            foreach (Control ctrl in control.Controls)
                            {
                                if (ctrl is TextBox)
                                {
                                    ctrl.Font = fd.Font;
                                }
                            }
                        }
                    }
                    txtExample.Font = fd.Font;
                }
            }
        }

        private void btnFont_Click(object sender, EventArgs e)
        {
            SelectFont();
        }

        private void SelectColor(bool isShadow)
        {
            using (ColorDialog cd = new ColorDialog())
            {
                if (cd.ShowDialog() == DialogResult.OK)
                {
                    if (!isShadow)
                    {
                        lblTextColor.BackColor = cd.Color;
                    }
                    else
                    {
                        lblShadowColor.BackColor = cd.Color;
                    }
                    txtExample.ForeColor = cd.Color;
                }
            }
        }

        private void btnFontColor_Click(object sender, EventArgs e)
        {
            SelectColor(false);
        }

        private void btnShadowColor_Click(object sender, EventArgs e)
        {
            SelectColor(true);
        }

        private Bitmap[] GetBitmaps(bool isTeam)
        {
            int height = isTeam ? 16 : 12;
            int arraySize = height == 16 ? 4 : 5;
            Bitmap[] result = new Bitmap[arraySize];
            Brush shadowBrush = new SolidBrush(lblShadowColor.BackColor);
            Brush textBrush = new SolidBrush(lblTextColor.BackColor);
            int graphIndex = 0;

            try
            {
                foreach (Control ctrl in this.Controls)
                {
                    if (ctrl is GroupBox)
                    {
                        for (int i = ctrl.Controls.Count - 1; i >= 0; i--)
                        {
                            //foreach (Control control in ctrl.Controls)
                            //{
                            if ((ctrl.Controls[i] is TextBox) && ((ctrl.Controls[i].Name.Contains("Team") && isTeam) || (ctrl.Controls[i].Name.Contains("Stadium") && !isTeam)))
                            {
                                Bitmap bitmap = new Bitmap(256, height);
                                using (Graphics graphics = Graphics.FromImage(bitmap))
                                {
                                    graphics.Clear(Color.Black);
                                    string text = ctrl.Controls[i].Text;
                                    System.Drawing.Font font = txtExample.Font;
                                    SizeF size = graphics.MeasureString(text, font, 256);

                                    StringFormat format = new StringFormat();
                                    if (rbLeft.Checked)
                                        format.Alignment = StringAlignment.Near;
                                    if(rbCenter.Checked)
                                        format.Alignment = StringAlignment.Center;
                                    if (rbRigth.Checked)
                                        format.Alignment = StringAlignment.Far;
                                        //LineAlignment = StringAlignment.Center
                                    
                                    RectangleF rect = new RectangleF(0, 0, 256, height);
                                    RectangleF ShadowRect = new RectangleF(0, 1, 256, height);
                                    // Dibujar sombra del texto
                                    graphics.DrawString(text, font, shadowBrush, ShadowRect, format);
                                    // Dibujar texto principal
                                    graphics.DrawString(text, font, textBrush, rect, format);

                                    // Mensaje de depuración
                                    //Console.WriteLine($"Dibujado texto: '{text}' en rectángulo: {rect}");
                                }
                                result[graphIndex] = bitmap;
                                //result[graphIndex].Save(control.Name + ".bmp",ImageFormat.Bmp);
                                graphIndex++;
                                if (graphIndex >= arraySize)
                                {
                                    break;
                                }
                            }
                        }
                        if (graphIndex >= arraySize)
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


            return result;
        }

        private void WritePictureBox(Bitmap[] bitmaps)
        {
            // Índice del BMP temporal
            int bmpTempIndex = 0;
            int i = 0;
            // Alto de la imagen
            int heigth = bitmaps.Length == 4 ? 32 : 25;
            // Creamos el Bitmap con las dos líneas de texto
            Bitmap finalBitmap = new Bitmap(128, 128);
            // Bitmap Temporal
            Bitmap[] tempBMP = new Bitmap[bitmaps.Length];

            // Cargamos cada texto cortado a la mitad en el array de los BMP temporales.
            foreach (Bitmap bitmap in bitmaps)
            {
                // Instanciamos un BMP temporal del array
                tempBMP[bmpTempIndex] = new Bitmap(128, heigth);
                using (Graphics g = Graphics.FromImage(tempBMP[bmpTempIndex]))
                {
                    g.Clear(Color.Black);

                    // Dibujar la parte superior del bitmap original en la parte superior de tempBMP
                    g.DrawImage(bitmap, new Rectangle(0, 0, 128, heigth / 2), new Rectangle(0, 0, 128, heigth / 2), GraphicsUnit.Pixel);

                    // Dibujar la parte inferior del bitmap original en la parte inferior de tempBMP
                    g.DrawImage(bitmap, new Rectangle(0, heigth / 2, 128, heigth / 2), new Rectangle(128, 0, 128, heigth / 2), GraphicsUnit.Pixel);
                }
                bmpTempIndex++;

                //tempBMP[bmpTempIndex - 1].Save((bmpTempIndex - 1).ToString() + ".BMP", ImageFormat.Bmp);
            }
            // Escribimos el Bitmap final y lo cargamos en el PictureBox
            using (Graphics g = Graphics.FromImage(finalBitmap))
            {
                //g.Clear(Color.Black);
                foreach (Bitmap bitmap in tempBMP)
                {
                    g.DrawImage(bitmap, 0, heigth * i);
                    i++;
                    //finalBitmap.Save("FinalBitmap" + (heigth * i).ToString()  +".bmp", ImageFormat.Bmp);
                }
            }
            pbTname.Image = finalBitmap;
        }

        private void btnWtriteTeam_Click(object sender, EventArgs e)
        {
            pbTname.Image = null;
            Bitmap[] bitmaps = GetBitmaps(true);
            WritePictureBox(bitmaps);
        }

        private void btnWriteStadium_Click(object sender, EventArgs e)
        {
            Bitmap[] bitmaps = GetBitmaps(false);
            WritePictureBox(bitmaps);
        }

        private void btnSavePicture_Click(object sender, EventArgs e)
        {
            BMPtoTIMConverter bMPtoTIMConverter = new BMPtoTIMConverter();
            BitmapConverter bitmapConverter = new BitmapConverter();
            byte[] timData = null;
            Bitmap bmp = (Bitmap)pbTname.Image;
            Bitmap newBMP = null;
            string fileBINPath = string.Empty;
            string fileBIN = string.Empty;
            string fileRAW = Path.GetTempFileName();
            try
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Archivos bmp|*.bmp";
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        fileBINPath = Path.GetPathRoot(sfd.FileName);
                        fileBIN = fileBINPath +  Path.GetFileNameWithoutExtension(sfd.FileName) + ".bin";
                       // Bitmap b = new Bitmap(pbTname.Width, pbTname.Height, PixelFormat.Format4bppIndexed);
                       // b = (Bitmap)pbTname.Image;
                        //pbTname.Image.Save(b,PixelFormat.Format4bppIndexed);
                        pbTname.Image.Save(sfd.FileName, ImageFormat.Bmp);
                        newBMP = bitmapConverter.ConvertTo4Bit(bmp);
                        newBMP.Save(sfd.FileName, ImageFormat.Bmp);
                        Simsala_BIN.CallCompressBMPImage(sfd.FileName, fileRAW, fileBIN);
                    }
                    //if (File.Exists(sfd.FileName))
                    //{
                    //    string timPath = Path.GetDirectoryName(sfd.FileName) + "\\" + Path.GetFileNameWithoutExtension(sfd.FileName) + ".TIM";
                    //    bMPtoTIMConverter.ConvertToTim(sfd.FileName, timPath);
                    //    pbTname.Image = newBMP;
                    //}
                    if (File.Exists(fileBIN))
                    {
                        MessageBox.Show("Archivos creados!");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void pbTname_Paint(object sender, PaintEventArgs e)
        {
            // Create a Bitmap object from a file.
            Bitmap myBitmap = new Bitmap(128, 128);

            // Draw myBitmap to the screen.
            e.Graphics.DrawImage(myBitmap, 0, 0, myBitmap.Width,
                myBitmap.Height);

            // Set each pixel in myBitmap to black.
            for (int Xcount = 0; Xcount < myBitmap.Width; Xcount++)
            {
                for (int Ycount = 0; Ycount < myBitmap.Height; Ycount++)
                {
                    myBitmap.SetPixel(Xcount, Ycount, Color.Black);
                }
            }

            // Draw myBitmap to the screen again.
            e.Graphics.DrawImage(myBitmap, myBitmap.Width, 0,
                myBitmap.Width, myBitmap.Height);

            //pbTname.Image = myBitmap;
        }

        private void frmT_NAME_Load(object sender, EventArgs e)
        {

        }
    }
}