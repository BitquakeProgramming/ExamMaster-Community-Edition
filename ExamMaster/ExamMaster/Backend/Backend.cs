using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamMaster.Backend
{
    public class Backend : IDisposable
    {
        private String questionCatalog;
        private bool hasInited = false;
        private QuestionCatalog currentCatalog;

        public void LoadCatalog(String questionCatalog)
        {
            this.questionCatalog = questionCatalog;
            this.hasInited = true;
        }

        public QuestionCatalog Catalog
        {
            get
            {
                return currentCatalog;
            }
        }

        #region Dispose
        void IDisposable.Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool managed)
        {
            if (managed)
            {
                hasInited = false;
            }
        }

        ~Backend()
        {
            Dispose(false);
        }
        #endregion
    }
}
