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
        public int? TaskAssainUserID { get; set; }
        public DateTime? TaskAddeddate { get; set; }
        public DateTime? TaskPerformeddate { get; set; }
        public string ImageMapPath { get; set; }
        public double? AdjacentPoleHeight { get; set; }
        public string TransFormerLoading { get; set; }
        public string Notes { get; set; }
        public string ImagesTakenpath { get; set; }
        public int? UserID { get; set; }
    }
    public class PoleInfoViewModel
    {
        public int ID { get; set; }
        public int? UserID { get; set; }
        public int? TaskAssainUserID { get; set; }
        public string PoleID { get; set; }
        public int? TypeID { get; set; }
        public string TypeName { get; set; }
        public DateTime? TaskAddeddate { get; set; }
        public DateTime? TaskPerformeddate { get; set; }
        public string ImageMapPath { get; set; }
        public double? AdjacentPoleHeight { get; set; }
        public string TransFormerLoading { get; set; }
        public string Notes { get; set; }
        public string ImagesTakenpath { get; set; }
        public string UserFullName { get; set; }
        public string UserName { get; set; }

    }

    public class UserViewModel
    {
        public int Id { get; set; }
        public string AspNetUserId { get; set; }
        public string AspNetRoleID { get; set; }
        public string UserName { get; set; }
        public string UserFullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool? IsActive { get; set; }

        public string UPassword { get; set; }

    }


}
