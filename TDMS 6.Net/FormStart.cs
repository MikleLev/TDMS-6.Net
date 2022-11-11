using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TDMS
{
    public partial class FormStart : Form
    {
        LetterContext db;
        public FormStart()
        {
            InitializeComponent();

            db = new LetterContext();
            db.Database.EnsureCreated();
            db.Letters.Load();
            db.Project.Load();
            db.Companies.Load();
            db.Users.Load();

            dataGridView1.DataSource = db.Letters.Local.ToBindingList();
            dataGridView2.DataSource = db.Users.Local.ToBindingList();
        }


        private void FormStart_Load(object sender, EventArgs e)
        {
            //TreeView Tree = new TreeView();
            //Tree.Location = new Point(10, 10);
            //Tree.Height = 100;
            //Tree.Width = 200;
            //TreeNode tovarNode = new TreeNode("Внешняя переписка");
            //// Добавляем новый дочерний узел к tovarNode
            //tovarNode.Nodes.Add(new TreeNode("Входящие"));
            //// Добавляем tovarNode вместе с дочерними узлами в TreeView
            //Tree.Nodes.Add(tovarNode);
            //// Добавляем второй очерний узел к первому узлу в TreeView
            //Tree.Nodes[0].Nodes.Add(new TreeNode("Исходящие"));
            
            //this.Controls.Add(Tree);

        }

        // добавление
        private void button1_Click_1(object sender, EventArgs e)
        {
            LetterForm ltForm = new LetterForm();
            DialogResult result = ltForm.ShowDialog(this);

            if(result == DialogResult.Cancel)
                return;

            ParentLetter parentLetter = new ParentLetter();
            parentLetter.Type = ltForm.textBox1.Text;  //player.Position = plForm.comboBox1.SelectedItem.ToString();
            parentLetter.Number = (int)ltForm.numericUpDown1.Value;
            parentLetter.DateTime = ltForm.dateTimePicker1.Value;
            parentLetter.Project.Name = ltForm.textBox2.Text;
            parentLetter.To.Name = ltForm.textBox3.Text;
            parentLetter.From.Name = ltForm.textBox4.Text;
            //файл

            db.Letters.Add(parentLetter);
            db.SaveChanges();

            MessageBox.Show("Новый объект добавлен");
        }
        //изменение
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count>0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView1[0,index].Value.ToString(), out id);
                if (converted == false)
                    return;

                ParentLetter parentLetter =db.Letters.Find(id);

                LetterForm ltForm = new LetterForm();

                ltForm.textBox1.Text = parentLetter.Type;//
                ltForm.numericUpDown1.Value = parentLetter.Number;
                ltForm.dateTimePicker1.Value = parentLetter.DateTime;
                ltForm.textBox2.Text = parentLetter.Project.Name;
                ltForm.textBox3.Text = parentLetter.To.Name;
                ltForm.textBox4.Text = parentLetter.From.Name;
                //файл
                
                DialogResult result= ltForm.ShowDialog(this);

                if (result==DialogResult.Cancel)
                    return;

                parentLetter.Type = ltForm.textBox1.Text;  //player.Position = plForm.comboBox1.SelectedItem.ToString();
                parentLetter.Number = (int)ltForm.numericUpDown1.Value;
                parentLetter.DateTime = ltForm.dateTimePicker1.Value;
                parentLetter.Project.Name = ltForm.textBox2.Text;
                parentLetter.To.Name = ltForm.textBox3.Text;
                parentLetter.From.Name = ltForm.textBox4.Text;
                //файл

                db.SaveChanges();
                dataGridView1.Refresh();
                MessageBox.Show("Объект обновлен");
            }
        }
        // удаление
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count>0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView1[0,index].Value.ToString(), out id);

                if (converted == false)
                    return;

                ParentLetter parentLetter = db.Letters.Find(id);
                db.Letters.Remove(parentLetter);
                db.SaveChanges();

                MessageBox.Show("Объект удален");
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void parentLetterBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }
    }
}
