using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace ExamMaster.Database
{
    [Serializable]
    [JsonObject("GlobalConfig")]
    public class GlobalConfig
    {
        public static readonly GlobalConfig INSTANCE;

        static GlobalConfig()
        {
            INSTANCE = (GlobalConfig)JsonConvert.DeserializeObject(File.ReadAllText(Environment.CurrentDirectory + @"\config.cfg"), typeof(GlobalConfig));
        }

        [JsonProperty]
        public List<CatalogModel> Catalogs = new List<CatalogModel>();


        public CatalogModel GetCatalogByName(String name)
        {
            return Catalogs.SingleOrDefault<CatalogModel>((i) => i.DisplayName.Equals(name));
        }
    }

    [Serializable]
    [JsonObject("CatalogModel")]
    public class CatalogModel
    {
        private String _catalogName;
        private String displayName;
        private short variations;
        private String sqlVariationName;
        private String _sqlTaskPid;
        private String taskTableName;

        [JsonProperty]
        public String DisplayName
        {
            get
            {
                return displayName;
            }
            set
            {
                displayName = value;
            }
        }
        
        [JsonProperty]
        public String SQLCatalogName
        {
            get
            {
                return _catalogName;
            }
            set
            {
                _catalogName = value;
            }
        }

        [JsonProperty]
        public String SQLVariationName
        {
            get
            {
                return sqlVariationName;
            }
            set
            {
                sqlVariationName = value;
            }
        }

        [JsonProperty]
        public String SQLTaskName
        {
            get
            {
                return _sqlTaskPid;
            }
            set
            {
                _sqlTaskPid = value;
            }
        }


        [JsonProperty]
        public short VariationCount
        {
            get
            {
                return variations;
            }
            set
            {
                variations = value;
            }
        }

        [JsonProperty]
        public String SQLTaskDbName
        {
            get
            {
                return taskTableName;
            }
            set
            {
                taskTableName = value;
            }
        }

        [JsonProperty]
        public String SQL_TaskID = "P_Id";
        [JsonProperty]
        public String SQL_Question = "Frage";
        [JsonProperty]
        public String SQL_Answer1 = "Antwort1";
        [JsonProperty]
        public String SQL_Answer2 = "Antwort2";
        [JsonProperty]
        public String SQL_Answer3 = "Antwort3";
        [JsonProperty]
        public String SQL_Answer4 = "Antwort4";
        [JsonProperty]
        public String SQL_RightAnswer = "RichtigeAntwort";

        [JsonProperty] public String DataSource = "localhost";
        [JsonProperty] public String Database = "binnenschifffahrt";
        [JsonProperty] public String Username = "root";
        [JsonProperty] public String Password = "";
    }
}
