using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace memory_game
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Button[] qutular;
        int tstep = 0, fstep = 0, acilan = 0;
        Button birinci = null, ikinci = null;
        bool gosterilir = false; 

        private void Form1_Load(object sender, EventArgs e)
        {
            qutular = new Button[]
            {
                button9, button10, button3, button4, button5, button6,
                button7, button8
            };

            foreach (Button b in qutular)
            {
                b.Click += QutuBas;
            }

         
            timer2.Tick -= timer2_Tick;      
            timer2.Tick += timer2_Tick;      

            OyunBasla(); 
        }

        void OyunBasla()
        {
            timer1.Stop();
            timer2.Stop();

            Color[] d = { Color.Red, Color.Green, Color.Blue, Color.Yellow };
            List<Color> list = new List<Color>(d);
            list.AddRange(d);

            Random r = new Random();
            foreach (Button b in qutular)
            {
                int k = r.Next(list.Count);
                b.Tag = list[k];
                b.BackColor = (Color)b.Tag; 
                list.RemoveAt(k);
            }

            tstep = 0;
            fstep = 0;
            acilan = 0;
            birinci = null;
            ikinci = null;
            label3.Text = "0";
            label4.Text = "0";

            gosterilir = true; 
            timer2.Start();   
        }

        
        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Stop();
            foreach (Button b in qutular)
            {
                b.BackColor = Color.Silver;
            }
            gosterilir = false; 
        }

        void QutuBas(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (gosterilir || timer1.Enabled || b.BackColor != Color.Silver)
            {
                return;
            }

            b.BackColor = (Color)b.Tag;

            if (birinci == null)
            {
                birinci = b;
                return;
            }

            ikinci = b;

            if ((Color)birinci.Tag == (Color)ikinci.Tag)
            {
                tstep++;
                acilan += 2;
                label3.Text = tstep.ToString();
                pictureBox1.ImageLocation = "D:\\ASP.NET\\img\\true1.png";
                birinci = null;
                ikinci = null;

                if (acilan == 8)
                {
                    if(tstep < fstep)
                    {
                        MessageBox.Show("Təəssüf! Səhv addımlarınızın sayı çoxdur.", "Məğlubiyyət", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    else
                    {
                        MessageBox.Show("Təbriklər! Oyunu uğurla bitirdiniz.", "Qalib", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                fstep++;
                label4.Text = fstep.ToString();
                pictureBox1.ImageLocation = "D:\\C# lesson\\img and details\\close.png";
                timer1.Start();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Oyundan çıxış edilsinmi?", "Sual", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (d == DialogResult.Yes)
            {
                Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OyunBasla();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            if (birinci != null) birinci.BackColor = Color.Silver;
            if (ikinci != null) ikinci.BackColor = Color.Silver;
            birinci = null;
            ikinci = null;
        }
    }
}