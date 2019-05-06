using System;
using System.Drawing;
using System.Windows.Forms;
using Sapper.Properties;

namespace Sapper
{
    public partial class Window : Form
    {
        private Field field;
        private GroupBox groupBox;
        
        public Window()
        {
            InitializeComponent();
            Resize += ResizeF;
            NewGame(10);
            listBox1.DataSource = field.Levels;
            listBox1.DisplayMember = "getName";
            this.Text = "Sapper";
        }

        private void NewGame(int n)   
        {
            if (n < 5) n = 5;
            if (n > 20) n = 20;
            field = listBox1.SelectedItem != null 
                ? new Field(n, ((Levels) listBox1.SelectedItem).Percent) 
                : new Field(n, 15);
            AddButtons();
            field.Change += ChangeButton;
            field.Lose += Lose;
            field.Win += Win;
            ChangeForm();
        }

        private void AddButtons()
        {
            groupBox = new GroupBox
            {
                Location = new Point(100, 100), 
                Size = new Size(40 * field.Count, 40 * field.Count), 
                Parent = this
            };

            for (var i = 0; i < field.Count; ++i)
            for (var j = 0; j < field.Count; ++j)
            {
                var b = new ControlButton(field, i, j)
                {
                    Width = 37,
                    Height = 37,
                    Location = new Point(j * 40, i * 40),
                    FlatStyle = FlatStyle.Flat,
                    BackgroundImage = Resources.cell,
                    ForeColor = SystemColors.Control,
                    Parent = groupBox

                };
            }
            
            ResizeF(this, new EventArgs());
        }

        private void ChangeButton(object s, ChangeArgs e)
        {
            foreach (var b in groupBox.Controls)
            {
                if ((b as ControlButton) == null || (b as ControlButton).X != e.X) continue;
                if ((b as ControlButton).Y != e.Y) continue;
                if (e.MinArr == "0")
                {
                    (b as ControlButton).Text = "";
                    (b as ControlButton).BackgroundImage = Resources.cellclear;
                }
                else
                {
                    (b as ControlButton).Text = e.MinArr;
                    (b as ControlButton).BackgroundImage = Resources.cellopen;
                }
            }
        }

        private void Lose(object s, EventArgs e)
        {
            FindCell(Resources.cellmine);
            MessageBox.Show("You lose");
            field.Change -= ChangeButton;
            field.Lose -= Lose;
        }

        private void Win(object s, EventArgs e)
        {
            FindCell(Resources.cellflag);
            MessageBox.Show("You win");
            field.Win -= Win;
        }

        private void FindCell(Image z)
        {
            for (var i = 0; i < field.Count; i++)
            for (var j = 0; j < field.Count; j++)
            {
                if (!field.Cells[i, j].HasMine) continue;
                foreach (ControlButton t in groupBox.Controls)
                    if (t.X == i && t.Y == j)
                        t.BackgroundImage = z;
            }
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            groupBox.Parent = null;
            NewGame(textBox1.Text != "" ? Convert.ToInt32(textBox1.Text) : 10);
        }

        private void ResizeF(object sender, EventArgs e)
        {
            groupBox1.Location = new Point((Width-groupBox1.Size.Width)/2,10);
            groupBox.Location = new Point((Width - groupBox.Size.Width)/2, 100);
        }

        private void ChangeForm()
        {
            Width = groupBox.Width + 200;
            Height = groupBox.Height + 200;
            if (Width <= groupBox1.Width) Width = groupBox1.Width + 40;
        }
    }
}
