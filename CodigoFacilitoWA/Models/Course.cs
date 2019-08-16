using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodigoFacilitoWA.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        public string Name { get; set; }

        public Int16 Duration { get; set; }

        public string InstructorName { get; set; }

        public bool IsActive { get; set; }
    }
}
