using System;
using System.Drawing;
using System.Windows.Forms;


namespace Editor_de_textos
{
    public partial class Ventana1 : Form
    {
        //string var_Archivo;
        bool cambios = false;
        //int Guardar = 0;
        //string Nombre_arch;
        OpenFileDialog abrir_Doc = new OpenFileDialog();

        public Ventana1()
        {
            InitializeComponent();
        }

        private void Ventana1_Load(object sender, EventArgs e)
        {
            foreach (FontFamily var_Fuente in FontFamily.Families) //Cargar las fuentes del sistema.
            {
                CBxTipFuente.Items.Add(var_Fuente.Name.ToString());
                Fuente_toolStripComboBox1.Items.Add(var_Fuente.Name.ToString());
            }
        }
        //====================================MENU_PRINCIPAL====================================================================================//

        //---------------------ARCHIVO----------------------------------------------//
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cambios == true)
            {
                DialogResult res = MessageBox.Show("¿Cerrar sin guardar cambios?", "Advertencia",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (res == DialogResult.Yes)
                {
                    CajaTexto.Clear();
                    //var_Archivo = null;
                    Text = "Nuevo documento - TextCreator";
                }
                
            }

            //-------------------BARRA_DE_ESTADO--------------------//
            estado_toolStripStatusLabel1.Text = "Cargando ";
            barra_toolStripProgressBar1.Visible = true;
            timer1.Start();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            abrir_Doc.DefaultExt = "*.rtf";
            //.Filter = "Formato de texto enriquecido(.rtf)|*.rtf";

            if (abrir_Doc.ShowDialog() == DialogResult.OK && abrir_Doc.FileName.Length > 0)
            {
                CajaTexto.LoadFile(abrir_Doc.FileName);
                Text = abrir_Doc.FileName + " - TextCreator";
                //----------------BARRA_DE_ESTADO--------------------//
                estado_toolStripStatusLabel1.Text = "Cargando ";
                barra_toolStripProgressBar1.Visible = true;
                timer1.Start();
            }
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.DefaultExt = "*.rtf";
            saveFileDialog1.Filter = "RTF Files|*.rtf";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK && saveFileDialog1.FileName.Length > 0)
            {
                CajaTexto.SaveFile(saveFileDialog1.FileName);
                Text = saveFileDialog1.FileName + " - TextCreator";

                //-------------------BARRA_DE_ESTADO--------------------//
                estado_toolStripStatusLabel1.Text = "Guardando ";
                barra_toolStripProgressBar1.Visible = true;
                timer1.Start();
            }
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printDialog1.ShowDialog(CajaTexto);

            //-------------------BARRA_DE_ESTADO--------------------//
            estado_toolStripStatusLabel1.Text = "Imprimiendo ";
            barra_toolStripProgressBar1.Visible = true;
            timer1.Start();

        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Environment.Exit(0);
            Ventana1.ActiveForm.Close();
        }
        
        //---------------------------------EDIDCION----------------------------------------//
        private void seleccionarTodoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CajaTexto.SelectAll();
            seleccionarTodoToolStripMenuItem.Text = ("&SelectAll");
        }

        private void copiarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CajaTexto.Copy();
        }

        private void cortarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CajaTexto.Cut();
        }

        private void pegarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CajaTexto.Paste();
        }

        private void deshacerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CajaTexto.Undo();
        }

        private void rehacerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CajaTexto.Redo();
        }

        //----------------------------FORMATO--------------------------------------------//
        private void Fuente_toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CajaTexto.SelectionFont = new Font(Fuente_toolStripComboBox1.Text, CajaTexto.SelectionFont.Size, CajaTexto.SelectionFont.Style);
            }
            catch { }
            CBxTipFuente.Text = CajaTexto.SelectionFont.Name.ToString();
        }

        private void TamañotoolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CajaTexto.SelectionFont = new Font(CajaTexto.SelectionFont.FontFamily, float.Parse(TamañotoolStripComboBox1.SelectedItem.ToString()), CajaTexto.SelectionFont.Style);
            }
            catch { }
            CBxTamFuente.Text = CajaTexto.SelectionFont.Size.ToString();
        }

        private void colorDeFuenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = CajaTexto.SelectionColor;

            if (colorDialog1.ShowDialog() == DialogResult.OK && colorDialog1.Color != CajaTexto.SelectionColor)
            {
                CajaTexto.SelectionColor = colorDialog1.Color;
            }
        }

        private void negritaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CajaTexto.SelectionFont != null)
            {
                if (CajaTexto.SelectionFont.Bold == false)
                {
                    CajaTexto.SelectionFont = new Font(CajaTexto.SelectionFont, CajaTexto.SelectionFont.Style | FontStyle.Bold);
                }
                else
                {
                    CajaTexto.SelectionFont = new Font(CajaTexto.SelectionFont, CajaTexto.SelectionFont.Style & ~FontStyle.Bold);
                }
            }
        }

        private void cursivaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CajaTexto.SelectionFont != null)
            {
                if (CajaTexto.SelectionFont.Italic == false)
                {
                    CajaTexto.SelectionFont = new Font(CajaTexto.SelectionFont, CajaTexto.SelectionFont.Style | FontStyle.Italic);
                }
                else
                {
                    CajaTexto.SelectionFont = new Font(CajaTexto.SelectionFont, CajaTexto.SelectionFont.Style & ~FontStyle.Italic);
                }
            }
        }

        private void subrayadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CajaTexto.SelectionFont != null)
            {
                if (CajaTexto.SelectionFont.Underline == false)
                {
                    CajaTexto.SelectionFont = new Font(CajaTexto.SelectionFont, CajaTexto.SelectionFont.Style | FontStyle.Underline);
                }
                else
                {
                    CajaTexto.SelectionFont = new Font(CajaTexto.SelectionFont, CajaTexto.SelectionFont.Style & ~FontStyle.Underline);
                }
            }
        }

        private void tachadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CajaTexto.SelectionFont != null)
            {
                if (CajaTexto.SelectionFont.Strikeout == false)
                {
                    CajaTexto.SelectionFont = new Font(CajaTexto.SelectionFont, CajaTexto.SelectionFont.Style | FontStyle.Strikeout);
                }
                else
                {
                    CajaTexto.SelectionFont = new Font(CajaTexto.SelectionFont, CajaTexto.SelectionFont.Style & ~FontStyle.Strikeout);
                }
            }
        }

        //---------------------------------PARRAFO----------------------------------------//
        private void izquierdaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CajaTexto.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void centrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CajaTexto.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void derechaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CajaTexto.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void viñetaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CajaTexto.SelectionBullet != true)
            {
                CajaTexto.SelectionBullet = true;
            }
            else
            {
                CajaTexto.SelectionBullet = false;
            };
        }

        //------------------------------------AYUDA-------------------------------------//
        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form abrirFormulario = new Form2();
            abrirFormulario.Show();
        }

        //==================================================================================================================================//

        //===========================BARRA_HERRAMIENTAS=====================================================================================//

        //-----------------------------PORTAPAPELES---------------------------------------//
        private void BtCopiar_Click(object sender, EventArgs e)
        {
            CajaTexto.Copy();
        }

        private void BtCortar_Click(object sender, EventArgs e)
        {
            CajaTexto.Cut();
        }

        private void BtPegar_Click(object sender, EventArgs e)
        {
            CajaTexto.Paste();
        }

        private void BtDeshacer_Click(object sender, EventArgs e)
        {
            CajaTexto.Undo();
        }

        private void BtRehacer_Click(object sender, EventArgs e)
        {
            CajaTexto.Redo();
        }

        private void BtGuardar_Click(object sender, EventArgs e)
        {
            saveFileDialog1.DefaultExt = "*.rtf";
            saveFileDialog1.Filter = "RTF Files|*.rtf";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK && saveFileDialog1.FileName.Length > 0)
            {
                CajaTexto.SaveFile(saveFileDialog1.FileName);

                //-------------------BARRA_DE_ESTADO--------------------//
                estado_toolStripStatusLabel1.Text = "Guardando ";
                barra_toolStripProgressBar1.Visible = true;
                timer1.Start();
            }
        }

        //--------------------------Formato---------------------------------------------//
        private void CBxTipFuente_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CajaTexto.SelectionFont = new Font(CBxTipFuente.Text, CajaTexto.SelectionFont.Size, CajaTexto.SelectionFont.Style);
            }
            catch { }
            Fuente_toolStripComboBox1.Text = CajaTexto.SelectionFont.Name.ToString();
        }

        private void CBxTamFuente_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CajaTexto.SelectionFont = new Font(CajaTexto.SelectionFont.FontFamily, float.Parse(CBxTamFuente.SelectedItem.ToString()), CajaTexto.SelectionFont.Style);
            }
            catch { }
            TamañotoolStripComboBox1.Text = CajaTexto.SelectionFont.Size.ToString();
        }

        private void BtClrFuente_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = CajaTexto.SelectionColor;

            if (colorDialog1.ShowDialog() == DialogResult.OK && colorDialog1.Color != CajaTexto.SelectionColor)
            {
                CajaTexto.SelectionColor = colorDialog1.Color;
            }
        }

        private void BtNegrita_Click(object sender, EventArgs e)
        {
            if (CajaTexto.SelectionFont != null)
            {
                if (CajaTexto.SelectionFont.Bold == false)
                {
                    CajaTexto.SelectionFont = new Font(CajaTexto.SelectionFont, CajaTexto.SelectionFont.Style | FontStyle.Bold);
                }
                else 
                {
                    CajaTexto.SelectionFont = new Font(CajaTexto.SelectionFont, CajaTexto.SelectionFont.Style & ~FontStyle.Bold);
                }
            }
        }

        private void BtCursiva_Click(object sender, EventArgs e)
        {
            if (CajaTexto.SelectionFont != null)
            {
                if (CajaTexto.SelectionFont.Italic == false)
                {
                    CajaTexto.SelectionFont = new Font(CajaTexto.SelectionFont, CajaTexto.SelectionFont.Style | FontStyle.Italic);
                }
                else
                {
                    CajaTexto.SelectionFont = new Font(CajaTexto.SelectionFont, CajaTexto.SelectionFont.Style & ~FontStyle.Italic);
                }
            }
        }

        private void BtSubrayado_Click(object sender, EventArgs e)
        {
            if (CajaTexto.SelectionFont != null)
            {
                if (CajaTexto.SelectionFont.Underline == false)
                {
                    CajaTexto.SelectionFont = new Font(CajaTexto.SelectionFont, CajaTexto.SelectionFont.Style | FontStyle.Underline);
                }
                else
                {
                    CajaTexto.SelectionFont = new Font(CajaTexto.SelectionFont, CajaTexto.SelectionFont.Style & ~FontStyle.Underline);
                }
            }
        }

        private void BtTachado_Click(object sender, EventArgs e)
        {
            if (CajaTexto.SelectionFont != null)
            {
                if (CajaTexto.SelectionFont.Strikeout == false)
                {
                    CajaTexto.SelectionFont = new Font(CajaTexto.SelectionFont, CajaTexto.SelectionFont.Style | FontStyle.Strikeout);
                }
                else
                {
                    CajaTexto.SelectionFont = new Font(CajaTexto.SelectionFont, CajaTexto.SelectionFont.Style & ~FontStyle.Strikeout);
                }
            }
        }

        //---------------------------PARRAFO--------------------------------------------//
        private void BtAizquierda_Click(object sender, EventArgs e)
        {
            CajaTexto.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void BtAcentrar_Click(object sender, EventArgs e)
        {
            CajaTexto.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void BtAderecha_Click(object sender, EventArgs e)
        {
            CajaTexto.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void BtViñeta_Click(object sender, EventArgs e)
        {
            if (CajaTexto.SelectionBullet != true)
            {
                CajaTexto.SelectionBullet = true;
            }
            else
            {
                CajaTexto.SelectionBullet = false;
            }
        }

        //============================================================================================//

        //=========================MENU_CONTEXTUAL=================================//
        private void seleccionarTodoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CajaTexto.SelectAll();
        }

        private void copiarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CajaTexto.Copy();
        }

        private void cortarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CajaTexto.Cut();
        }

        private void pegarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CajaTexto.Paste();
        }

        private void resetearFormatoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CajaTexto.SelectAll();
            CajaTexto.SelectionFont = new Font("Calibri", 12, FontStyle.Regular);
            CajaTexto.SelectionColor = Color.Black;
        }
        //===================================================================================//

        //=============================BARRA_DE_ESTADO========================================//
        private void zoom_150_Click(object sender, EventArgs e)
        {
            CajaTexto.ZoomFactor = 1.5f;
        }

        private void zoom_120_Click(object sender, EventArgs e)
        {
            CajaTexto.ZoomFactor = 1.2f;
        }

        private void zoom_100_Click(object sender, EventArgs e)
        {
            CajaTexto.ZoomFactor = 1f;
        }

        private void zoom_70_Click(object sender, EventArgs e)
        {
            CajaTexto.ZoomFactor = 0.7f;
        }

        private void zoom_50_Click(object sender, EventArgs e)
        {
            CajaTexto.ZoomFactor = 0.5f;
        }

        private void zoom_25_Click(object sender, EventArgs e)
        {
            CajaTexto.ZoomFactor = 0.25f;
        }

        private void CajaTexto_TextChanged(object sender, EventArgs e)
        {
            if (CajaTexto.Text.Trim() != "")
                cambios = true;
            else
                cambios = false;

            linea_toolStripStatusLabel3.Text = "Lineas: " + CajaTexto.Lines.Length.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            barra_toolStripProgressBar1.Value += 5;
            if (barra_toolStripProgressBar1.Value == 100)
            {
                estado_toolStripStatusLabel1.Text = "          ";
                barra_toolStripProgressBar1.Value = 0;
                barra_toolStripProgressBar1.Visible = false;
                timer1.Stop();
            }
        }

        private void Ventana1_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (cambios == true)
            {
                DialogResult res = MessageBox.Show("¿Cerrar sin guardar cambios?", "Advertencia",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (res == DialogResult.No)
                    e.Cancel = true;
            }
        }
    } 
}
