using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_System.Business_Layer;

namespace DVLD_System
{
    class clsGlobal
    {
        public static clsUser User { get; set; } = clsUser.Find(2);
        public static string Female { get; set; } = @"Ressources/person_woman.png";
        public static string Male { get; set; } = @"Ressources/person_man_72px.png";
    }
}
