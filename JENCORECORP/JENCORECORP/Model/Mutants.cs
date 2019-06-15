using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JENCORECORP
{
    public class Mutants
    {
        public long MUTANTID { get; set; }
        public String MUTANTCODENAME { get; set; }
        public String FIRSTNAME { get; set; }
        public String MIDDLENAME { get; set; }
        public String LASTNAME { get; set; }
        public DateTime DATEOFBIRTH { get; set; }
        public int AGE { get; set; }
        public String PLACEOFBIRTH { get; set; }
        public long LEVEL { get; set; }
        public String ORIGIN { get; set; }
        public String ALIGNMENT { get; set; }
        public String ALIASES { get; set; }
        public String GENDER { get; set; }
        public String EYECOLOR { get; set; }
        public String HAIRCOLOR { get; set; }
        public int HEIGHT { get; set; }
        public int WEIGHT { get; set; }
        public String CITIZENSHIP { get; set; }
        public String OCCUPATION { get; set; }
        public String AFFILIATION { get; set; }
        public String MARITALSTATUS { get; set; }
        public bool ISACTIVE { get; set; }
        public DateTime ACTIVEDATE { get; set; }
        public DateTime CREATEDDATE { get; set; }
    }
}
