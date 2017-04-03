using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ExamMaster.Backend;
using ExamMaster.Database;
using ExamMaster.Frontend;

namespace ExamMaster
{
    public class UnitTest
    {
        public static void CheckDBConnection(DB db, CatalogModel model)
        {
            Console.WriteLine("--- Starting Unit Test ---");
            ExceptionHandler.Init(null);
            try
            {
                if (db.OpenConnection(model))
                {
                    LogTestSucceed();
                    db.CloseConnection();
                }
                else
                {
                    throw new Exception("Could not establish connection to database!");
                }
            }
            catch (Exception e)
            {
                LogTestFailed(e);
            }
        }

        public static void TestLoadCatalog(DB db, CatalogModel model)
        {
            Console.WriteLine("--- Starting Unit Test ---");
            ExceptionHandler.Init(null);
            Backend.Backend backend = new Backend.Backend();
            try
            {

                if (backend.LoadCatalog(model))
                {
                    LogTestSucceed();
                }
                else
                {
                    throw new Exception("Could not load catalog model!");
                }
            }
            catch (Exception e)
            {
                LogTestFailed(e);
            }
        }

        public static void TestLoadImages()
        {
            Console.WriteLine("--- Starting Unit Test ---");
            ExceptionHandler.Init(null);
            Backend.Backend backend = new Backend.Backend();
            try
            {

                backend.Init();
                LogTestSucceed();
            }
            catch (Exception e)
            {
                LogTestFailed(e);
            }
        }

        private static void LogTestFailed(Exception e, [CallerMemberName]string caller = "unknown")
        {
            Console.WriteLine("Unit test failed! Procedure name: {0} failed width exception {1}", caller, e.Message);
        }
        private static void LogTestSucceed([CallerMemberName]string caller = "unknown")
        {
            Console.WriteLine("Unit test succeed! Procedure name: {0}", caller);
        }
    }
}
