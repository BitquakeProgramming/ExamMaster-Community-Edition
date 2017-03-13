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
        
        // Single choice field
        private String singleChoiceAnswer;

        public Question(String question, float maxPoints)
        {
            this.questionText = question;
            this.maxPoints = maxPoints;
        }

        public void InitMultipleChoice(String[] answers, bool isNumeric = false)
        {
            this.checkboxAnswers = new bool[answers.Length];
            if (isNumeric)
            {
                type = QuestionType.MULTIPLE_CHOICE_NUMERIC;
            }
            else
            {
                type = QuestionType.MULTIPLE_CHOICE_STRING;
            }
        }

        public void InitSingleChoice(bool isNumeric = false)
        {
            singleChoiceAnswer = ""; 
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
    }

    public enum QuestionType
    {
        MULTIPLE_CHOICE_STRING,
        MULTIPLE_CHOICE_NUMERIC,
        SINGLE_CHOICE_STRING,
        SINGLE_CHOICE_NUMERIC
    }
}
