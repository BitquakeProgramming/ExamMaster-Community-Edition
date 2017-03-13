using ExamMaster.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamMaster.Backend
{
    public class Backend : IDisposable
    {
        private CatalogModel questionCatalog;
        private bool hasInited = false;
        private QuestionCatalog currentCatalog;

        public void LoadCatalog(CatalogModel questionCatalog)
        {
            this.questionCatalog = questionCatalog;
            this.hasInited = true;

            String variationQuery = GetQueryFor(questionCatalog, new Random().Next(1, 15));
        }

        private String GetQueryFor(CatalogModel catalog, int variation)
        {
            return "SELECT * FROM " + catalog.SQLName + " WHERE " + catalog.SQLVariationName + " = " + variation;
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
