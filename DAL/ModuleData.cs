using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ModuleData
    {
        /*Properties for a module and user*/
        public string Code { get; set; }

        public string Name { get; set; }

        public double NumberOfCredits { get; set; }

        public double ClassHoursPerWeek { get; set; }

        public double NumberOfWeeks { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime ModuleStudyDate { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
