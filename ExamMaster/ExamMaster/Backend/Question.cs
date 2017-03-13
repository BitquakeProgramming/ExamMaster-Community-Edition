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
        private String[] multipleChoiceQuestions;
        private short rightAnswerMulti;
        
        // Single choice field
        private String singleChoiceAnswer;
        private String rightAnswerSingle;

        public Question(String question, float maxPoints)
        {
            this.questionText = question;
            this.maxPoints = maxPoints;
        }

        public void InitMultipleChoice(String[] answers, short rightAnswer, bool isNumeric = false)
        {
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

        public String[] MultipleChoiceQuestions
        {
            get
            {
                return multipleChoiceQuestions;
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

        public short RightAnswerMultipleChoice
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
