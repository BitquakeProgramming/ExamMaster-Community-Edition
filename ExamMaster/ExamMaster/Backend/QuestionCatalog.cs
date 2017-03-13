using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamMaster.Backend
{
    public class QuestionCatalog : List<Question>
    {
        private String catalogName;
        private int minutesToSolve;

        public QuestionCatalog(String name, int minutes)
        {
            this.catalogName = name;
            this.minutesToSolve = minutes;
        }

        public String QuestionCatalogName
        {
            get
            {
                return catalogName;
            }
        }

        public int MinutesToSolve
        {
            get
            {
                return minutesToSolve;
            }
        }
    }
}
