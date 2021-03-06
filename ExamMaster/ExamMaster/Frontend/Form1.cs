﻿using ExamMaster.Backend;
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
        private bool selfFlag = false;


        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex >= 0)
            {
                CatalogModel selected = GlobalConfig.INSTANCE.Catalogs[comboBox1.SelectedIndex];
                bool tryFetchData = backEnd.LoadCatalog(selected);
                if (tryFetchData)
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

        private void buttonBack_Click(object sender, EventArgs e)
        {
            ((Button) sender).ForeColor = Color.White;
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
                                                      "Achtung", MessageBoxButtons.YesNo, MessageBoxIcon.None);
                if (result == DialogResult.Yes)
                {
                    PrepareResultPage();
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

        private void PrepareResultPage()
        {
            float maxPoints = 0;
            float reachedPoints = 0;
            for (int i = 0; i < backEnd.Catalog.Count; i++)
            {
                maxPoints += backEnd.Catalog[i].MaxPoints;
                int userAnswer = -1;
                bool broke = false;
                for (int j = 0; j < backEnd.Catalog[i].MultipleChoiceUserAnswers.Length; j++)
                {
                    if (backEnd.Catalog[i].MultipleChoiceUserAnswers[j] == true)
                    {
                        if (userAnswer == -1)
                        {
                            // Add Points
                            userAnswer = j + 1;
                        }
                        else
                        {
                            // Remove Points and continue with next question
                            userAnswer = -1;
                            broke = true;
                            break;
                        }
                    }
                }
                if (userAnswer == backEnd.Catalog[i].RightAnswerMultipleChoice && !broke)
                {
                    reachedPoints += backEnd.Catalog[i].MaxPoints;
                }
            }
            roundProgress1.ReachedPercent = (float)reachedPoints / (float) maxPoints * 100f;
            labelResultPoints.Text = reachedPoints + " von " + maxPoints;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tabControl.ItemSize = new Size(1, 1);
            ExceptionHandler.Init(this);
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
            PrepareAvoidSelfCheck();
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
                answerCheck1.Refresh();
                answerCheck2.Refresh();
                answerCheck3.Refresh();
                answerCheck4.Refresh();


                if (bmp == null)
                {
                    tableLayoutPanel10.ColumnStyles[0].Width = 0f;
                }
                else
                {
                    tableLayoutPanel10.ColumnStyles[0].Width = 200f;
                }
            }
            EndAvoidSelfCheck();
        }

        private void questionListView1_MouseClick(object sender, MouseEventArgs e)
        {
            QuestionItem item = (QuestionItem) questionListView1.GetItemAt(e.X, e.Y);
            if (item != null)
            {
                LoadQuestion(item);

                if (currentQuestion.Index == questionListView1.Items.Count - 1)
                {
                    buttonNext.Text = "Abschließen";
                    buttonBack.Enabled = true;
                }
                else if (currentQuestion.Index == 0)
                {
                    buttonBack.Enabled = false;
                    buttonNext.Text = "Weiter";
                }
                else
                {
                    buttonBack.Enabled = true;
                    buttonNext.Text = "Weiter";
                }
            }
        }

        private void pictureBox1_Resize(object sender, EventArgs e)
        {
            questionImage.Height = questionImage.Width;
        }

        private void answerCheck1_CheckedChanged(object sender, EventArgs e)
        {
            if (selfFlag) return;
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

        private void anwerCheck1ByLabel_Click(object sender, EventArgs e)
        {
            answerCheck1.Checked = !answerCheck1.Checked;
            answerCheck1_CheckedChanged(sender, e);
        }

        private void anwerCheck2ByLabel_Click(object sender, EventArgs e)
        {
            answerCheck2.Checked = !answerCheck2.Checked;
            answerCheck1_CheckedChanged(sender, e);
        }

        private void anwerCheck3ByLabel_Click(object sender, EventArgs e)
        {
            answerCheck3.Checked = !answerCheck3.Checked;
            answerCheck1_CheckedChanged(sender, e);
        }

        private void anwerCheck4ByLabel_Click(object sender, EventArgs e)
        {
            answerCheck4.Checked = !answerCheck4.Checked;
            answerCheck1_CheckedChanged(sender, e);
        }

        private void MouseButtonEnter(object sender, EventArgs e)
        {
            ((Button) sender).ForeColor = Color.White;
        }

        private void MouseButtonLeave(object sender, EventArgs e)
        {
            ((Button) sender).ForeColor = Color.Black;
        }

        public void PrepareAvoidSelfCheck()
        {
            selfFlag = true;
        }

        public void EndAvoidSelfCheck()
        {
            selfFlag = false;
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