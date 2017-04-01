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

        public QuestionCatalog(String name)
        {
            this.catalogName = name;
        }

        public String QuestionCatalogName
        {
            get
            {
                return catalogName;
            }
        }
    }
}
