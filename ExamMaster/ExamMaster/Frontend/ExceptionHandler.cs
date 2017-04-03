using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamMaster.Frontend
{
    public class ExceptionHandler
    {
        private static Form parentForm;

        /// <summary>
        /// Initializes the Handler using a Form or null.
        /// </summary>
        /// <param name="form">The dialogs parent</param>
        public static void Init(Form form)
        {
            parentForm = form;
        }

        /// <summary>
        /// Handles the way the user interacts with exceptions.
        /// </summary>
        /// <param name="e">The exception object</param>
        /// <param name="msg">The exception message</param>
        /// <param name="header">The error message header</param>
        public static void Throw(Exception e, String msg = "Es ist ein Fehler aufgetreten!", String header = "Fehler")
        {
            String errorMessage = msg + (e == null ? "" :  " " + e.Message);
            if (parentForm != null)
            {
                MessageBox.Show(parentForm, errorMessage, header, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Console.WriteLine("Exception occured: " + errorMessage);
            }
        }
    }
}
