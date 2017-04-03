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

        public String SQL_TaskID = "P_Id";
        public String SQL_Question = "Frage";
        public String SQL_Answer1 = "Antwort1";
        public String SQL_Answer2 = "Antwort2";
        public String SQL_Answer3 = "Antwort3";
        public String SQL_Answer4 = "Antwort4";
        public String SQL_RightAnswer = "RichtigeAntwort";

        public String _dataSource = "localhost";
        public Int32 _port = 3306;
        public String _database = "binnenschifffahrt";
        public String _username = "root";
        public String _password = "";
        public bool _shuffle = true;

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
        public string DataSource
        {
            get { return _dataSource; }
            set { _dataSource = value; }
        }

        [JsonProperty]
        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }

        [JsonProperty]
        public string Database1
        {
            get { return _database; }
            set { _database = value; }
        }

        [JsonProperty]
        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        [JsonProperty]
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        [JsonProperty]
        public bool Shuffle
        {
            get { return _shuffle; }
            set { _shuffle = value; }
        }

        [JsonProperty]
        public string Database
        {
            get { return _database; }
            set { _database = value; }
        }

        [JsonProperty]
        public string SqlRightAnswer
        {
            get { return SQL_RightAnswer; }
            set { SQL_RightAnswer = value; }
        }

        [JsonProperty]
        public string SqlTaskId
        {
            get { return SQL_TaskID; }
            set { SQL_TaskID = value; }
        }

        [JsonProperty]
        public string SqlQuestion
        {
            get { return SQL_Question; }
            set { SQL_Question = value; }
        }

        [JsonProperty]
        public string SqlAnswer1
        {
            get { return SQL_Answer1; }
            set { SQL_Answer1 = value; }
        }

        [JsonProperty]
        public string SqlAnswer2
        {
            get { return SQL_Answer2; }
            set { SQL_Answer2 = value; }
        }

        [JsonProperty]
        public string SqlAnswer3
        {
            get { return SQL_Answer3; }
            set { SQL_Answer3 = value; }
        }

        [JsonProperty]
        public string SqlAnswer4
        {
            get { return SQL_Answer4; }
            set { SQL_Answer4 = value; }
        }
    }
}
