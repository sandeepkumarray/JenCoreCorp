using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RDCore;
using System.Data;
using System.Windows.Media;

namespace JENCORECORP
{
    public class ProjectsDAL
    {
        private DataBaseManager DBManager;

        public ProjectsDAL(DataBaseManager DBManager)
        {
            this.DBManager = DBManager;
        }

        public Projects GetProjectById(int ProjectId)
        {
            Projects Result = new Projects();
            DataSet ds = new DataSet();
            string CommandText = "SELECT * FROM Projects WHERE ProjectID = " + ProjectId;
            ds = DBManager.ExecuteDataSet(CommandText);
            if (ds.Tables != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Result.ProjectId = Convert.ToInt64(Convert.IsDBNull(dr["ProjectId"]) ? 0 : dr["ProjectId"]);
                        Result.ProjectName = Convert.ToString(Convert.IsDBNull(dr["ProjectName"]) ? "" : dr["ProjectName"]);
                        Result.Description = Convert.ToString(Convert.IsDBNull(dr["Description"]) ? "" : dr["Description"]);
                        Result.ControlHeader = Convert.ToString(Convert.IsDBNull(dr["ControlHeader"]) ? "" : dr["ControlHeader"]);
                        Result.Height = Convert.ToDouble(Convert.IsDBNull(dr["Height"]) ? 0 : dr["Height"]);
                        Result.HoverIcon = Convert.ToString(Convert.IsDBNull(dr["HoverIcon"]) ? "" : dr["HoverIcon"]);
                        Result.LabelColour = (Color)new ColorConverter().ConvertFromInvariantString(Convert.ToString(Convert.IsDBNull(dr["LabelColour"]) ? "" : dr["LabelColour"]));
                        Result.LabelColour2 = (Color)new ColorConverter().ConvertFromInvariantString(Convert.ToString(Convert.IsDBNull(dr["LabelColour2"]) ? "" : dr["LabelColour2"]));
                        Result.Overal = Convert.ToString(Convert.IsDBNull(dr["Overal"]) ? "" : dr["Overal"]);
                        Result.ProfitPercentage = Convert.ToString(Convert.IsDBNull(dr["ProfitPercentage"]) ? "" : dr["ProfitPercentage"]);
                        Result.StrokeThickness = Convert.ToDouble(Convert.IsDBNull(dr["StrokeThickness"]) ? 0 : dr["StrokeThickness"]);
                        Result.Width = Convert.ToDouble(Convert.IsDBNull(dr["Width"]) ? 0 : dr["Width"]);
                        Result.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    }
                }
            }
            return Result;
        }

        public List<Projects> GetAllJenProjects()
        {
            List<Projects> ProjectsList = new List<Projects>();
            Projects Result;
            DataSet ds = new DataSet();
            string CommandText = "SELECT * FROM Projects";
            ds = DBManager.ExecuteDataSet(CommandText);
            if (ds.Tables != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Result = new Projects();
                        Result.ProjectId = Convert.ToInt64(Convert.IsDBNull(dr["ProjectId"]) ? 0 : dr["ProjectId"]);
                        Result.ProjectName = Convert.ToString(Convert.IsDBNull(dr["ProjectName"]) ? "" : dr["ProjectName"]);
                        Result.Description = Convert.ToString(Convert.IsDBNull(dr["Description"]) ? "" : dr["Description"]);
                        Result.ControlHeader = Convert.ToString(Convert.IsDBNull(dr["ControlHeader"]) ? "" : dr["ControlHeader"]);
                        Result.Height = Convert.ToDouble(Convert.IsDBNull(dr["Height"]) ? 0 : dr["Height"]);
                        Result.HoverIcon = Convert.ToString(Convert.IsDBNull(dr["HoverIcon"]) ? "" : dr["HoverIcon"]);
                        Result.LabelColour = (Color)new ColorConverter().ConvertFromInvariantString(Convert.ToString(Convert.IsDBNull(dr["LabelColour"]) ? "" : dr["LabelColour"]));
                        Result.LabelColour2 = (Color)new ColorConverter().ConvertFromInvariantString(Convert.ToString(Convert.IsDBNull(dr["LabelColour2"]) ? "" : dr["LabelColour2"]));
                        Result.Overal = Convert.ToString(Convert.IsDBNull(dr["Overal"]) ? "" : dr["Overal"]);
                        Result.ProfitPercentage = Convert.ToString(Convert.IsDBNull(dr["ProfitPercentage"]) ? "" : dr["ProfitPercentage"]);
                        Result.StrokeThickness = Convert.ToDouble(Convert.IsDBNull(dr["StrokeThickness"]) ? 0 : dr["StrokeThickness"]);
                        Result.Width = Convert.ToDouble(Convert.IsDBNull(dr["Width"]) ? 0 : dr["Width"]);
                        Result.IsActive = Convert.ToBoolean(dr["IsActive"]);
                        ProjectsList.Add(Result);
                    }
                }
            }
            return ProjectsList;
        }

        public bool SaveProject(Projects Project)
        {
            bool IsSuccess = false;
            int Result = 0;
            string CommandText = "Insert into Projects(ProjectName,Description,ControlHeader,Height,HoverIcon,LabelColour,LabelColour2,Overal,ProfitPercentage,StrokeThickness,Width) " + string.Empty
                + " values('" + Project.ProjectName + "','" + Project.Description + "','" + Project.ControlHeader + "'," + Project.Height + ",'" + Project.HoverIcon + "','" +
                Library.GetColorName(Project.LabelColour) + "','" + Library.GetColorName(Project.LabelColour2) + "','" + Project.Overal + "','" + Project.ProfitPercentage + "'," + Project.StrokeThickness + "," +
                Project.Width +  "," + Project.IsActive + ")";
            Result = DBManager.ExecuteNonQuery(CommandText);
            if (Result > 0)
                IsSuccess = true;
            return IsSuccess;
        }

        public bool UpdateProject(Projects Project)
        {
            bool IsSuccess = false;
            int Result = 0;
            string CommandText = "update Projects set ProjectName = '" + Project.ProjectName + "',Description = '" + Project.Description + "',ControlHeader = '" + Project.ControlHeader + "',Height ='" + Project.Height
                + "',HoverIcon ='" + Project.HoverIcon + "',LabelColour='" + Library.GetColorName(Project.LabelColour) + "',LabelColour2='" + Library.GetColorName(Project.LabelColour2) + "',overal='" + Project.Overal +
                "',profitpercentage='" + Project.ProfitPercentage + "',strokethickness='" + Project.StrokeThickness + "',width='" + Project.Width + "',IsActive = " + Project.IsActive + " where Projectid = " + Project.ProjectId;
            Result = DBManager.ExecuteNonQuery(CommandText);
            if (Result > 0)
                IsSuccess = true;
            return IsSuccess;
        }
    }
}
