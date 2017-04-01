using ExamMaster.Backend;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExamMaster.Database;

namespace ExamMaster.Frontend
{
    public partial class Form1 : Form
    {
        private Backend.Backend backEnd = new Backend.Backend();
        private QuestionItem currentQuestion;


        public Form1()
        {
            InitializeComponent();
        }


        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {
        }

        private void headerBackgroundPanel_Paint(object sender, PaintEventArgs e)
        {
        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void tableLayoutPanel8_Paint(object sender, PaintEventArgs e)
        {
        }

        private void labelQuestionHeader_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex >= 0)
            {
                CatalogModel selected = GlobalConfig.INSTANCE.Catalogs[comboBox1.SelectedIndex];
                bool tryFetchData = backEnd.LoadCatalog(selected);
                if (!tryFetchData)
                {
                    MessageBox.Show(this,
                                    "Es ist ein Fehler mit der Datenbank aufgetreten. Vorgang konnte nicht forgeführt werden!",
                                    "Fehler", MessageBoxButtons.OK);
                }
                else
                {
                    PrepareQuestionsPage();
                    tabControl.SelectTab("tabPageTest");
                }
            }
        }

        private void PrepareQuestionsPage()
        {
            foreach (Question question in backEnd.Catalog)
            {
                QuestionItem item = new QuestionItem(question, QuestionState.NONE);
                if (currentQuestion == null)
                {
                    LoadQuestion(item);
                }
                questionListView1.Items.Add(item);
                buttonBack.Enabled = false;
            }
        }


        private void panel5_Paint(object sender, PaintEventArgs e)
        {
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            ((Button)sender).ForeColor = Color.White;
            QuestionItem item = (QuestionItem) questionListView1.Items[currentQuestion.Index - 1];
            LoadQuestion(item);
            if (currentQuestion.Index == 0)
            {
                buttonBack.Enabled = false;
            }
            buttonNext.Text = "Weiter";
            questionListView1.Refresh();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (currentQuestion.Index == questionListView1.Items.Count - 1)
            {
                DialogResult result = MessageBox.Show(this,
                                                      "Möchtest du den Versuch abschließen? Es wird dir anschließend nicht mehr möglich sein deine Lösungen zu ändern.",
                                                      "Achtung", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    tabControl.SelectTab("tabPageResult");
                }
            }
            else if (currentQuestion.Index == questionListView1.Items.Count - 2)
            {
                QuestionItem item = (QuestionItem) questionListView1.Items[currentQuestion.Index + 1];
                LoadQuestion(item);
                buttonNext.Text = "Abschließen";
                buttonBack.Enabled = true;
            }
            else
            {
                QuestionItem item = (QuestionItem) questionListView1.Items[currentQuestion.Index + 1];
                LoadQuestion(item);
                buttonBack.Enabled = true;
            }
            questionListView1.Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tabControl.ItemSize = new Size(0, 1);
            backEnd.Init();
            foreach (CatalogModel model in GlobalConfig.INSTANCE.Catalogs)
            {
                comboBox1.Items.Add(model.DisplayName);
            }
            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }
        }

        private void LoadQuestion(QuestionItem question)
        {
            if (question != null && currentQuestion != question)
            {
                if (currentQuestion != null)
                {
                    currentQuestion.Selected = false;
                }
                currentQuestion = question;
                currentQuestion.Selected = true;
                if (questionImage.Image != null)
                {
                    questionImage.Image.Dispose();
                }
                Bitmap bmp = backEnd.GetImageFromID(currentQuestion.Question.Id);
                questionImage.Image = bmp;
                labelQuestionHeader.Text = currentQuestion.Question.QuestionText;
                answerCheck1.Checked = currentQuestion.Question.MultipleChoiceUserAnswers[0];
                answerCheck2.Checked = currentQuestion.Question.MultipleChoiceUserAnswers[1];
                answerCheck3.Checked = currentQuestion.Question.MultipleChoiceUserAnswers[2];
                answerCheck4.Checked = currentQuestion.Question.MultipleChoiceUserAnswers[3];
                answer1.Text = currentQuestion.Question.MultipleChoiceAnswers[0];
                answer2.Text = currentQuestion.Question.MultipleChoiceAnswers[1];
                answer3.Text = currentQuestion.Question.MultipleChoiceAnswers[2];
                answer4.Text = currentQuestion.Question.MultipleChoiceAnswers[3];
                if (bmp == null)
                {
                    tableLayoutPanel10.ColumnStyles[0].Width = 0f;
                }
                else
                {
                    tableLayoutPanel10.ColumnStyles[0].Width = 200f;
                }
            }
        }

        private void questionListView1_MouseClick(object sender, MouseEventArgs e)
        {
            QuestionItem item = (QuestionItem) questionListView1.GetItemAt(e.X, e.Y);
            if (item != null)
            {
                LoadQuestion(item);
            }
        }

        private void pictureBox1_Resize(object sender, EventArgs e)
        {
            questionImage.Height = questionImage.Width;
        }

        private void answerCheck1_CheckedChanged(object sender, EventArgs e)
        {
            if (currentQuestion != null)
            {
                currentQuestion.Question.MultipleChoiceUserAnswers[0] = answerCheck1.Checked;
                currentQuestion.Question.MultipleChoiceUserAnswers[1] = answerCheck2.Checked;
                currentQuestion.Question.MultipleChoiceUserAnswers[2] = answerCheck3.Checked;
                currentQuestion.Question.MultipleChoiceUserAnswers[3] = answerCheck4.Checked;
                bool hasChecked = answerCheck1.Checked |
                                  answerCheck2.Checked |
                                  answerCheck3.Checked |
                                  answerCheck4.Checked;
                if (hasChecked)
                {
                    currentQuestion.State = QuestionState.CHECKED_SOME;
                }
                else
                {
                    currentQuestion.State = QuestionState.NONE;
                }
            }
            questionListView1.Refresh();
        }

        private void MouseButtonEnter(object sender, EventArgs e)
        {
            ((Button) sender).ForeColor = Color.White;
        }

        private void MouseButtonLeave(object sender, EventArgs e)
        {
            ((Button)sender).ForeColor = Color.Black;
        }
    }

    public class QuestionItem : ListViewItem
    {
        private Question question;
        private QuestionState state;

        public QuestionItem(Question question, QuestionState state)
        {
            this.question = question;
            this.state = state;
        }

        public Question Question
        {
            get { return question; }
            set { question = value; }
        }

        public QuestionState State
        {
            get { return state; }
            set { state = value; }
        }
    }
}