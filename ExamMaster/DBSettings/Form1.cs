using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExamMaster.Database;

namespace DBSettings
{
    public partial class Form1 : Form
    {
        private GlobalConfig config = new GlobalConfig();
        private int catalogNum = 0;
        public Form1()
        {
            InitializeComponent();
            MaximumSize = Size;
            MinimumSize = Size;
        }

        private void ToolStripButton1OnClick(object sender, EventArgs eventArgs)
        {
            catalogNum = 0;
            OpenFileDialog dialog = new OpenFileDialog();
            DialogResult result = dialog.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                if (File.Exists(dialog.FileName))
                {
                    try
                    {
                        config = GlobalConfig.Create(dialog.FileName);
                        CreatePages();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(this, "Datei konnte nicht geöffnet werden!", "Fehler");
                    }
                }
            }
        }

        private void CreatePages()
        {
            if (config != null)
            {
                for (int i = 0; i < config.Catalogs.Count; i++)
                {
                    CatalogModel model = config.Catalogs[i];
                    catalogNum++;
                    TabPage page = new TabPage("Katalog " + catalogNum);

                    DBSettingsPage settingsPage = new DBSettingsPage();
                    settingsPage.textDatenbankAdresse.Text = model.DataSource;
                    settingsPage.numPort.Value = model.Port;
                    settingsPage.textDatenbankName.Text = model.Database;
                    settingsPage.textBenutzername.Text = model.Username;
                    settingsPage.textPasswort.Text = model.Password;

                    settingsPage.textDisplayName.Text = model.DisplayName;
                    settingsPage.textSQLKatalogTabellenname.Text = model.SQLCatalogName;
                    settingsPage.textSQLVariationFeldname.Text = model.SQLVariationName;
                    settingsPage.textSQLAufgabenREFName.Text = model.SQLTaskName;
                    settingsPage.textSQLAufgabenTabellenname.Text = model.SQLTaskDbName;
                    settingsPage.textAufgabenId.Text = model.SQL_TaskID;
                    settingsPage.textFrageFeldname.Text = model.SQL_Question;
                    settingsPage.textAntwort1.Text = model.SQL_Answer1;
                    settingsPage.textAntwort2.Text = model.SQL_Answer2;
                    settingsPage.textAntwort3.Text = model.SQL_Answer3;
                    settingsPage.textAntwort4.Text = model.SQL_Answer4;
                    settingsPage.textRichtigeAntwort.Text = model.SQL_RightAnswer;
                    settingsPage.numVariations.Value = model.VariationCount;
                    settingsPage.checkMischen.Checked = model.Shuffle;
                    settingsPage.Dock = DockStyle.Fill;
                    page.Controls.Add(settingsPage);
                    customTabControl1.TabPages.Add(page);
                }
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            catalogNum++;
            TabPage page = new TabPage("Katalog " + catalogNum);

            DBSettingsPage settingsPage = new DBSettingsPage();
            settingsPage.Dock = DockStyle.Fill;
            page.Controls.Add(settingsPage);
            config.Catalogs.Add(new CatalogModel());
            customTabControl1.TabPages.Add(page);
            
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                String filename = dialog.FileName.EndsWith(".cfg") ? dialog.FileName : dialog.FileName + ".cfg";
                File.WriteAllText(filename, config.Serialize());
            }
        }
    }
}