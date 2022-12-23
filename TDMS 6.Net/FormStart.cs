using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
using TDMS_6.Net;

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
            //ToDataGridViewComboBoxColumn.DataSource=db.Users.Local.ToBindingList();
            dataGridView2.DataSource = db.Project.Local.ToBindingList();
            dataGridView3.DataSource = db.Companies.Local.ToBindingList();
            dataGridView4.DataSource = db.Users.Local.ToBindingList();
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

        //            Letter
        // добавление
        private void button1_Click(object sender, EventArgs e)
        {
            LetterForm ltForm = new LetterForm();

            List<Project> projects = db.Project.ToList();
            ltForm.comboBox1.DataSource = projects;
            ltForm.comboBox1.ValueMember = "Id";
            ltForm.comboBox1.DisplayMember = "Name";

            List<User> usersFrom = db.Users.ToList();
            ltForm.comboBox2.DataSource = usersFrom;
            ltForm.comboBox2.ValueMember = "Id";
            ltForm.comboBox2.DisplayMember = "Name";

            List<User> usersTo = db.Users.ToList();
            ltForm.listBox1.DataSource = usersTo;
            ltForm.listBox1.ValueMember = "Id";
            ltForm.listBox1.DisplayMember = "Name";

            DialogResult result = ltForm.ShowDialog(this);
            if(result == DialogResult.Cancel)
                return;

            ParentLetter parentLetter = new ParentLetter();
            parentLetter.Type = ltForm.textBox1.Text;
            parentLetter.Number = (int)ltForm.numericUpDown1.Value;
            parentLetter.DateTime = ltForm.dateTimePicker1.Value;
            parentLetter.Project = (Project)ltForm.comboBox1.SelectedItem;
            parentLetter.From = (User)ltForm.comboBox2.SelectedItem;

            usersTo.Clear();
            foreach (var item in ltForm.listBox1.SelectedItems)
            {
                usersTo.Add((User)item);
            }
            //файл
            parentLetter.To=usersTo;
            db.Letters.Add(parentLetter);
            db.SaveChanges();

            MessageBox.Show("Новый объект добавлен");
        }
        //изменение
        private void button2_Click_1(object sender, EventArgs e)
        {
            //if (dataGridView1.SelectedRows.Count>0)
            //{
            //    int index = dataGridView1.SelectedRows[0].Index;
            //    int id = 0;
            //    bool converted = Int32.TryParse(dataGridView1[0,index].Value.ToString(), out id);
            //    if (converted == false)
            //        return;

            //    ParentLetter parentLetter =db.Letters.Find(id);

            //    LetterForm ltForm = new LetterForm();

            //    ltForm.textBox1.Text = parentLetter.Type;
            //    ltForm.numericUpDown1.Value = parentLetter.Number;
            //    ltForm.dateTimePicker1.Value = parentLetter.DateTime;

            //    List<Project> projects = db.Project.ToList();
            //    ltForm.comboBox1.DataSource = projects;
            //    ltForm.comboBox1.ValueMember = "Id";
            //    ltForm.comboBox1.DisplayMember = "Name";
            //    if (parentLetter.Project!=null)
            //    {
            //        ltForm.comboBox1.SelectedValue = parentLetter.Project.Id;
            //    }

            //    List<User> usersFrom = db.Users.ToList();
            //    ltForm.comboBox2.DataSource = usersFrom;
            //    ltForm.comboBox2.ValueMember = "Id";
            //    ltForm.comboBox2.DisplayMember = "Name";
            //    if (parentLetter.From != null)
            //    {
            //        ltForm.comboBox2.SelectedValue = parentLetter.From.Id;
            //    }

            //    List<User> usersTo = db.Users.ToList();
            //    ltForm.comboBox3.DataSource = usersTo;
            //    ltForm.comboBox3.ValueMember = "Id";
            //    ltForm.comboBox3.DisplayMember = "Name";
            //    if (parentLetter.To != null)
            //    {
            //        ltForm.comboBox3.SelectedValue = parentLetter.To.Id;
            //    }

            //    DialogResult result= ltForm.ShowDialog(this);
            //    if (result==DialogResult.Cancel)
            //        return;

            //    parentLetter.Type = ltForm.textBox1.Text;
            //    parentLetter.Number = (int)ltForm.numericUpDown1.Value;
            //    parentLetter.DateTime = ltForm.dateTimePicker1.Value;
            //    parentLetter.Project = (Project)ltForm.comboBox1.SelectedItem;
            //    parentLetter.From = (User)ltForm.comboBox2.SelectedItem;
            //    parentLetter.To = (User)ltForm.comboBox3.SelectedItem;
            //    //файл

            //    db.Entry(parentLetter).State = EntityState.Modified;
            //    db.SaveChanges();
            //    dataGridView1.Refresh();
            //    MessageBox.Show("Объект обновлен");
            //}
        }
        // удаление
        private void button3_Click_1(object sender, EventArgs e)
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

        //           Project
        //добавление
        private void button4_Click(object sender, EventArgs e)
        {
            ProjectForm pjForm = new ProjectForm();

            List<Company> companies = db.Companies.ToList();
            pjForm.comboBox1.DataSource = companies;
            pjForm.comboBox1.ValueMember = "Id";
            pjForm.comboBox1.DisplayMember = "Name";

            DialogResult result = pjForm.ShowDialog(this);

            if (result == DialogResult.Cancel)
                return;

            Project project = new Project();
            project.Name = pjForm.textBox1.Text;
            project.Company = (Company)pjForm.comboBox1.SelectedItem;

            db.Project.Add(project);
            db.SaveChanges();
            MessageBox.Show("Новый объект добавлен");
        }
        //изменение
        private void button5_Click_1(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count>0)
            {
                int index = dataGridView2.SelectedRows[0].Index;
                int id =0;
                bool converted = Int32.TryParse(dataGridView2[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;
                Project project=db.Project.Find(id);

                ProjectForm pjForm = new ProjectForm();
                pjForm.textBox1.Text = project.Name;

                List<Company> companies = db.Companies.ToList();
                pjForm.comboBox1.DataSource = companies;
                pjForm.comboBox1.ValueMember = "Id";
                pjForm.comboBox1.DisplayMember = "Name";
                if (project.Company != null)
                {
                    pjForm.comboBox1.SelectedValue = project.Company.Id;
                }

                DialogResult result = pjForm.ShowDialog(this);
                if (result == DialogResult.Cancel)
                    return;

                project.Name = pjForm.textBox1.Text;
                project.Company = (Company)pjForm.comboBox1.SelectedItem;

                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                dataGridView2.Refresh();
                MessageBox.Show("Объект обновлен");
            }
        }
        //удаление
        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("При удалении объекта удалятся все связанные элементы");
            if (dataGridView2.SelectedRows.Count > 0)
            {
                int index = dataGridView2.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView2[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;

                Project project = db.Project.Find(id);
                db.Project.Remove(project);
                db.SaveChanges();

                MessageBox.Show("Объект удален");
            }
        }

        //           Company
        //
        private void dataGridView3_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int id = 0;
                bool converted = Int32.TryParse(dataGridView3[0, e.RowIndex].Value.ToString(), out id);
                if (converted == false)
                    return;

                Company company = db.Companies.Find(id);
                listBox1.DataSource = company?.Users.ToList();
                listBox1.DisplayMember = "Name";
            }
        }
        
        //добавление
        private void button7_Click(object sender, EventArgs e)
        {
            CompanyForm coForm = new CompanyForm();
            DialogResult result = coForm.ShowDialog(this);

            if (result == DialogResult.Cancel)
                return;

            Company company = new Company();
            company.Name = coForm.textBox1.Text;

            db.Companies.Add(company);
            db.SaveChanges();
            MessageBox.Show("Новый объект добавлен");
        }
        //изменение
        private void button8_Click(object sender, EventArgs e)
        {
            if(dataGridView3.SelectedRows.Count>0)
            {
                int index = dataGridView3.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView3[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;

                Company company = db.Companies.Find(id);

                CompanyForm coForm = new CompanyForm();
                coForm.textBox1.Text = company.Name;

                DialogResult result = coForm.ShowDialog(this);
                if (result == DialogResult.Cancel)
                    return;

                company.Name = coForm.textBox1.Text;

                db.Entry(company).State = EntityState.Modified;
                db.SaveChanges();
                dataGridView3.Refresh();
                MessageBox.Show("Объект обновлен");
            }
        }
        //удаление
        private void button9_Click(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedRows.Count > 0)
            {
                int index = dataGridView3.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView3[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;

                Company company = db.Companies.Find(id);
                company.Users.Clear();
                db.Companies.Remove(company);
                db.SaveChanges();

                MessageBox.Show("Объект удален");
            }
        }

        //           User
        //добавление
        private void button10_Click(object sender, EventArgs e)
        {
            UserForm usForm = new UserForm();

            List<Company> companies = db.Companies.ToList();
            usForm.comboBox1.DataSource = companies;
            usForm.comboBox1.ValueMember = "Id";
            usForm.comboBox1.DisplayMember = "Name";

            DialogResult result = usForm.ShowDialog(this);

            if (result == DialogResult.Cancel)
                return;

            User user = new User();
            user.Name = usForm.textBox1.Text;
            user.Company = (Company)usForm.comboBox1.SelectedItem;

            db.Users.Add(user);
            db.SaveChanges();
            MessageBox.Show("Новый объект добавлен");
        }
        //изменение
        private void button11_Click(object sender, EventArgs e)
        {
            if (dataGridView4.SelectedRows.Count > 0)
            {
                int index = dataGridView4.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView4[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;

                User user = db.Users.Find(id);

                UserForm usForm = new UserForm();
                usForm.textBox1.Text = user.Name;

                List<Company> companies = db.Companies.ToList();
                usForm.comboBox1.DataSource = companies;
                usForm.comboBox1.ValueMember = "Id";
                usForm.comboBox1.DisplayMember = "Name";
                if (user.Company != null)
                {
                    usForm.comboBox1.SelectedValue = user.Company.Id;
                }

                DialogResult result = usForm.ShowDialog(this);
                if (result == DialogResult.Cancel)
                    return;

                user.Name = usForm.textBox1.Text;
                user.Company = (Company)usForm.comboBox1.SelectedItem;

                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                dataGridView4.Refresh();
                MessageBox.Show("Объект обновлен");
            }
        }
        //удаление
        private void button12_Click(object sender, EventArgs e)
        {
            if (dataGridView4.SelectedRows.Count > 0)
            {
                int index = dataGridView4.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView4[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;

                User user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();

                MessageBox.Show("Объект удален");
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {}

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {}

        private void tabPage2_Click(object sender, EventArgs e)
        {}

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {}

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {}

        private void parentLetterBindingSource_CurrentChanged(object sender, EventArgs e)
        {}

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage1_Click_1(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click_1(object sender, EventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            
        }

        
    }
}
