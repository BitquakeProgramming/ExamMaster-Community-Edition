﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExamMaster.Database;

namespace ExamMaster
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Frontend.Form1());
        }

        static void RunTest()
        {
            DB db = new DB();

            // Test 1
            UnitTest.CheckDBConnection(db, GlobalConfig.INSTANCE.Catalogs[0]);

            // Test 2
            UnitTest.TestLoadCatalog(db, GlobalConfig.INSTANCE.Catalogs[0]);

            // Test 3
            UnitTest.TestLoadImages();
        }
    }
}
