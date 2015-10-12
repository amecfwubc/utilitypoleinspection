using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLite.Net.Attributes;

namespace AmecFWUPI.DataBaseModels
{
    [Table("LoginInfo")]
    public class LoginInfo
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        public string userName { get; set; }

        public string password { get; set; }

    }

    [Table("PoleInfo")]
    public class PoleInfo
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        public int userid { get; set; }

        public string poleID { get; set; }

        public string poleType { get; set; }

        public string dateTaskAdded { get; set; }

        public string dateTaskPerformed { get; set; }

        public string mapImagePath { get; set; }

        public string adjacentPoleHeight { get; set; }

        public string transformerLoading { get; set; }

        public string notes { get; set; }

        public string pathOfImagesTaken { get; set; }

    }

}
