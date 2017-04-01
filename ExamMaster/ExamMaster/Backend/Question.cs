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
        private QuestionType type;

        // Multiple choice fields
        private bool[] checkboxAnswers;
        private String[] multipleChoiceAnswers;
        private int rightAnswerMulti;
        
        // Single choice field
        private String singleChoiceAnswer;
        private String rightAnswerSingle;
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
            if (isNumeric)
            {
                type = QuestionType.MULTIPLE_CHOICE_NUMERIC;
            }
            else
            {
                type = QuestionType.MULTIPLE_CHOICE_STRING;
            }
        }

        public void InitSingleChoice(String rightAnswer, bool isNumeric = false)
        {
            singleChoiceAnswer = "";
            rightAnswerSingle = rightAnswer;
            if (isNumeric)
            {
                type = QuestionType.SINGLE_CHOICE_NUMERIC;
            }
            else
            {
                type = QuestionType.SINGLE_CHOICE_STRING;
            }
        }
        
        public QuestionType QuestionType
        {
            get
            {
                return type;
            }
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

        public String SingleChoiceAnswer
        {
            get
            {
                return singleChoiceAnswer;
            }
            set
            {
                singleChoiceAnswer = value;
            }
        }

        public int RightAnswerMultipleChoice
        {
            get
            {
                return rightAnswerMulti;
            }
        }

        public String RightAnswerSingleChoice
        {
            get
            {
                return rightAnswerSingle;
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

        public bool IsAnswerRight(String answer)
        {
            return rightAnswerSingle.ToUpper().Equals(answer.ToUpper());
        }

        public bool IsAnswerRight(int answer)
        {
            return rightAnswerMulti == answer;
        }
    }

    public enum QuestionType
    {
        MULTIPLE_CHOICE_STRING,
        MULTIPLE_CHOICE_NUMERIC,
        SINGLE_CHOICE_STRING,
        SINGLE_CHOICE_NUMERIC
    }
}
