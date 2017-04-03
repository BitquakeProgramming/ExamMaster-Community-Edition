using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamMaster.Backend
{
    public class Question
    {
        private String questionText;

        private float maxPoints;
        private float reachedPoints;
        
        private bool[] checkboxAnswers;
        private String[] multipleChoiceAnswers;
        private int rightAnswerMulti;
        
        private int id;
        public Question(String question, float maxPoints, int taskID)
        {
            this.questionText = question;
            this.maxPoints = maxPoints;
            this.Id = taskID;
        }

        public void InitMultipleChoice(String[] answers, int rightAnswer, bool isNumeric = false)
        {
            this.multipleChoiceAnswers = answers;
            this.checkboxAnswers = new bool[answers.Length];
            this.rightAnswerMulti = rightAnswer;
        }

        public float MaxPoints
        {
            get
            {
                return maxPoints;
            }
        }

        public float ReachedPoints
        {
            get
            {
                return reachedPoints;
            }
            set
            {
                reachedPoints = value;
            }
        }

        public String[] MultipleChoiceAnswers
        {
            get
            {
                return multipleChoiceAnswers;
            }
        }

        public bool[] MultipleChoiceUserAnswers
        {
            get
            {
                return checkboxAnswers;
            }
        }
        

        public int RightAnswerMultipleChoice
        {
            get
            {
                return rightAnswerMulti;
            }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string QuestionText
        {
            get { return questionText; }
        }
        
        public bool IsAnswerRight(int answer)
        {
            return rightAnswerMulti == answer;
        }
    }
}
