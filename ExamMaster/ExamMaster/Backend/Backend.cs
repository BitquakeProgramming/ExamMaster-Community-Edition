using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExamMaster.Database;

namespace ExamMaster.Backend
{
    public class Backend : IDisposable
    {
        private DB database = new DB();
        private CatalogModel questionCatalog;
        private bool hasInited = false;
        private QuestionCatalog currentCatalog;
        private String imgDir = Environment.CurrentDirectory + @"\local\img\Binnen\";

        private Dictionary<int, String> imageMap = new Dictionary<int, string>();

        public void Init()
        {
            DirectoryInfo dir = new DirectoryInfo(imgDir);
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                try
                {
                    Int32 imgID = Int32.Parse(file.Name.Split('.')[0]);
                    imageMap.Add(imgID, file.FullName);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Die Datei {0} konnte nicht geladen werden.", file.FullName);
                }
            }
        }

        public Bitmap GetImageFromID(int id)
        {
            if (imageMap.ContainsKey(id))
            {
                return new Bitmap(imageMap[id]);
            }
            return null;
        }
        public bool LoadCatalog(CatalogModel questionCatalog)
        {
            this.questionCatalog = questionCatalog;
            this.hasInited = true;

            database.OpenConnection();

            currentCatalog = new QuestionCatalog(this.questionCatalog.DisplayName);
            bool canContinue = false;
            Random rand = new Random();
            for (int i = 0; canContinue == false || i < 10; i++)
            {
                canContinue = database.FillCatalogBinnen(ref currentCatalog, this.questionCatalog,
                                           rand.Next(0, this.questionCatalog.VariationCount) + 1);
                if (currentCatalog.Count < 30) canContinue = false;

                while (currentCatalog.Count > 30)
                {
                    for (int j = currentCatalog.Count - 1; j >= 0; j--)
                    {
                        if (rand.Next(0, 5) == 2)
                        {
                            currentCatalog.RemoveAt(j);
                            if (currentCatalog.Count == 30)
                            {
                                break;
                            }
                        }
                    }
                }
                if (canContinue) break;
            }
            database.CloseConnection();
            return canContinue;
        }

        private String GetQueryFor(CatalogModel catalog, int variation)
        {
            return "SELECT * FROM " + catalog.SQLCatalogName + " WHERE " + catalog.SQLVariationName + " = " + variation;
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
