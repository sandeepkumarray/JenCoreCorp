using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RDCore;
using System.Data;
using System.Windows.Media;
using System.Data.Common;

namespace JENCORECORP
{
    public class MutantsDAL
    {
        private DataBaseManager DBManager;

        public MutantsDAL(DataBaseManager DBManager)
        {
            this.DBManager = DBManager;
        }

        public bool SaveMutants(Mutants Mutant)
        {
            bool IsSuccess = false;
            int Result = 0;
            //string CommandText = "INSERT INTO [MTDB].[dbo].[MUTANT] ([MUTANTCODENAME],[FIRSTNAME],[MIDDLENAME],[LASTNAME],[DATEOFBIRTH],[AGE],[PLACEOFBIRTH],[LEVEL],[ORIGIN],[ALIGNMENT],[ALIASES],[GENDER],[EYECOLOR],[HAIRCOLOR],[HEIGHT],[WEIGHT],[CITIZENSHIP],[OCCUPATION],[AFFILIATION],[MARITALSTATUS],[ISACTIVE],[ACTIVEDATE],[CREATEDDATE])" +
            // " values('" + ")";         

            DbCommand command = DBManager.GetStoredProcCommand("sp_insert_MUTANT");
            DBManager.AddInParameter(command, "@MUTANTCODENAME", DbType.String, Mutant.MUTANTCODENAME);
            DBManager.AddInParameter(command, "@FIRSTNAME", DbType.String, Mutant.FIRSTNAME);
            DBManager.AddInParameter(command, "@MIDDLENAME", DbType.String, Mutant.MIDDLENAME);
            DBManager.AddInParameter(command, "@LASTNAME", DbType.String, Mutant.LASTNAME);
            DBManager.AddInParameter(command, "@DATEOFBIRTH", DbType.DateTime, Mutant.DATEOFBIRTH);
            DBManager.AddInParameter(command, "@AGE", DbType.String, Mutant.AGE);
            DBManager.AddInParameter(command, "@PLACEOFBIRTH", DbType.DateTime, Mutant.PLACEOFBIRTH);
            DBManager.AddInParameter(command, "@LEVEL", DbType.String, Mutant.LEVEL);
            DBManager.AddInParameter(command, "@ORIGIN", DbType.String, Mutant.ORIGIN);
            DBManager.AddInParameter(command, "@ALIGNMENT", DbType.String, Mutant.ALIGNMENT);
            DBManager.AddInParameter(command, "@ALIASES", DbType.String, Mutant.ALIASES);
            DBManager.AddInParameter(command, "@GENDER", DbType.String, Mutant.GENDER);
            DBManager.AddInParameter(command, "@EYECOLOR", DbType.String, Mutant.EYECOLOR);
            DBManager.AddInParameter(command, "@HAIRCOLOR", DbType.String, Mutant.HAIRCOLOR);
            DBManager.AddInParameter(command, "@HEIGHT", DbType.String, Mutant.HEIGHT);
            DBManager.AddInParameter(command, "@WEIGHT", DbType.String, Mutant.WEIGHT);
            DBManager.AddInParameter(command, "@CITIZENSHIP", DbType.String, Mutant.CITIZENSHIP);
            DBManager.AddInParameter(command, "@OCCUPATION", DbType.String, Mutant.OCCUPATION);
            DBManager.AddInParameter(command, "@AFFILIATION", DbType.String, Mutant.AFFILIATION);
            DBManager.AddInParameter(command, "@MARITALSTATUS", DbType.String, Mutant.MARITALSTATUS);
            DBManager.AddInParameter(command, "@ISACTIVE", DbType.String, Mutant.ISACTIVE);
            DBManager.AddInParameter(command, "@ACTIVEDATE", DbType.DateTime, Mutant.ACTIVEDATE);
            DBManager.AddInParameter(command, "@CREATEDDATE", DbType.DateTime, Mutant.CREATEDDATE);

            Result = DBManager.ExecuteNonQuery(command);
            if (Result > 0)
                IsSuccess = true;
            return IsSuccess;
        }

        public bool SaveBulkMutants(List<Mutants> MutantList)
        {
            bool IsSuccess = false;
            int Result = 0; 
            
            foreach(Mutants mutant in MutantList)
            {
                SaveMutants(mutant);
            }

            if (Result > 0)
                IsSuccess = true;
            return IsSuccess;
        }
    }
}
