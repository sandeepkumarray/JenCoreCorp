using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RDCore;
using System.Data;
using System.Windows.Media;

namespace JENCORECORP
{
    public class ZonesDAL
    {
        private DataBaseManager DBManager;
        public ZonesDAL(DataBaseManager DBManager)
        {
            this.DBManager = DBManager;
        }

        public Zones GetZoneById(int ZoneId)
        {
            Zones Result = new Zones();
            DataSet ds = new DataSet();
            string CommandText = "SELECT * FROM Zones WHERE ZoneID = " + ZoneId;
            ds = DBManager.ExecuteDataSet(CommandText);
            if (ds.Tables != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Result.ZoneId = Convert.ToInt64(Convert.IsDBNull(dr["ZoneId"]) ? 0 : dr["ZoneId"]);
                        Result.ZoneName = Convert.ToString(Convert.IsDBNull(dr["ZoneName"]) ? "" : dr["ZoneName"]);
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
                    }
                }
            }
            return Result;
        }

        public List<Zones> GetAllJenZones()
        {
            List<Zones> ZonesList = new List<Zones>();
            Zones Result;
            DataSet ds = new DataSet();
            string CommandText = "SELECT * FROM zones";
            ds = DBManager.ExecuteDataSet(CommandText);
            if (ds.Tables != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Result = new Zones();
                        Result.ZoneId = Convert.ToInt64(Convert.IsDBNull(dr["ZoneId"]) ? 0 : dr["ZoneId"]);
                        Result.ZoneName = Convert.ToString(Convert.IsDBNull(dr["ZoneName"]) ? "" : dr["ZoneName"]);
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
                        ZonesList.Add(Result);
                    }
                }
            }
            return ZonesList;
        }

        public bool SaveZone(Zones Zone)
        {
            bool IsSuccess = false;
            int Result = 0;
            string CommandText = "Insert into Zones(ZoneName,Description,ControlHeader,Height,HoverIcon,LabelColour,LabelColour2,Overal,ProfitPercentage,StrokeThickness,Width) " + string.Empty
                + " values('" + Zone.ZoneName + "','" + Zone.Description + "','" + Zone.ControlHeader + "'," + Zone.Height + ",'" + Zone.HoverIcon + "','" +
                Library.GetColorName(Zone.LabelColour) + "','" + Library.GetColorName(Zone.LabelColour2) +"','" + Zone.Overal + "','" + Zone.ProfitPercentage + "'," + Zone.StrokeThickness + "," +
                Zone.Width + ")";
            Result = DBManager.ExecuteNonQuery(CommandText);
            if (Result > 0)
                IsSuccess = true;
            return IsSuccess;
        }

        public bool UpdateZone(Zones Zone)
        {
            bool IsSuccess = false;
            int Result = 0;
            string CommandText = "update Zones set ZoneName = '" + Zone.ZoneName + "',Description = '" + Zone.Description + "',ControlHeader = '" + Zone.ControlHeader + "',Height ='" + Zone.Height
                + "',HoverIcon ='" + Zone.HoverIcon + "',LabelColour='" + Library.GetColorName(Zone.LabelColour) + "',LabelColour2='" + Library.GetColorName(Zone.LabelColour2) + "',overal='" + Zone.Overal + "',profitpercentage='" + Zone.ProfitPercentage + "',strokethickness='" + Zone.StrokeThickness + "',width='" + Zone.Width + "' where zoneid = " + Zone.ZoneId;
            Result = DBManager.ExecuteNonQuery(CommandText);
            if (Result > 0)
                IsSuccess = true;
            return IsSuccess;
        }
    }
}
