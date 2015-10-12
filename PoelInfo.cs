using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmecFWUPI
{
    public class PoelInfo
    {
        public int ID { get; set; }
        public string PoleID { get; set; }
        public int? TypeID { get; set; }
        public DateTime? TaskAddeddate { get; set; }
        public DateTime? TaskPerformeddate { get; set; }
        public string ImageMapPath { get; set; }
        public double? AdjacentPoleHeight { get; set; }
        public string TransFormerLoading { get; set; }
        public string Notes { get; set; }
        public string ImagesTakenpath { get; set; }
        public string UserID { get; set; }
    }
}
