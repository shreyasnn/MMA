using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AngularTest.Models;
using System.Configuration;

namespace AngularTest.Controllers
{
    [RoutePrefix("api/templete")]
    public class TempleteController : ApiController
    {
        MMA_Model Model = new MMA_Model();
        private static string conString = ConfigurationManager.ConnectionStrings["MMSConnection"].ConnectionString;
        private static SqlConnection con;
     
        [HttpGet]
        [Route("GetObserverList")]//Return Observers list from database
        //prefix/api/templete/GetObserverList
        public List<String>[] GetObserverList()
        {
            //string select = "1";
            List<string>[] observerlist = new List<string>[2];
            observerlist[0] = new List<string>();
            observerlist[1] = new List<string>();
            try
            {
                con = new SqlConnection(conString);
                if (con.State != System.Data.ConnectionState.Open)
                {

                    con.Close();
                    con.Open();
                }
                else { con.Open(); }
                SqlCommand cmd1 = new SqlCommand();
                SqlDataReader reader1;

                cmd1.Connection = con;
                cmd1.CommandText = string.Format("select ID,Vorname,Name from MMS_Beobachter ORDER BY Name ASC");
                reader1 = cmd1.ExecuteReader();
                do
                {
                    while (reader1.Read())
                    {
                        string id = Convert.ToString(reader1.GetInt32(0));
                        observerlist[0].Add(id);
                        //observerlist[1].Add(reader1.GetString(1)+","+ reader1.GetString(2));
                        observerlist[1].Add(reader1.GetString(2) + "," + reader1.GetString(1));
                    }
                } while (reader1.NextResult());
                reader1.Close();
                return observerlist;
            }
            catch (InvalidOperationException exception)
            {
                // Error logging, post processing etc.
                return null;
            }


            //List<string>[] ObserverList = Model.getList(select);
            //return ObserverList;
        }

        [HttpGet]
        [Route("GetAreaList")]//Return Areas list from database
        //prefix/api/templete/GetAreaList  
        public List<String>[] GetAreaList()
        {
            List<string>[] arealist = new List<string>[2];
            arealist[0] = new List<string>();
            arealist[1] = new List<string>();
            try
            {
                con = new SqlConnection(conString);
                if (con.State != System.Data.ConnectionState.Open)
                {

                    con.Close();
                    con.Open();
                }
                else { con.Open(); }
                SqlCommand cmd2 = new SqlCommand();
                SqlDataReader reader2;
                cmd2.Connection = con;
                cmd2.CommandText = string.Format("select ID,Bereich from MMS_Bereich ORDER BY Bereich ASC");
                reader2 = cmd2.ExecuteReader();
                do
                {
                    while (reader2.Read())
                    {
                        string id = Convert.ToString(reader2.GetInt32(0));
                        arealist[0].Add(id);
                        arealist[1].Add(reader2.GetString(1));
                    }
                } while (reader2.NextResult());
                reader2.Close();
                con.Close();
                return arealist;
            }
            catch (InvalidOperationException exception)
            {
                // Error logging, post processing etc.
                return null;
            }
            //string select = "2";
            //List<string>[] AreaList = Model.getList(select);
            //return AreaList;
        }

        [HttpGet]
        [Route("GetEmployeeFunction")]//Return Employee functions list from database
        public List<String> GetFunctionList()
        {
          List<string> empfunctionlist = new List<string>();
            try
            {
                con = new SqlConnection(conString);
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Close();
                    con.Open();
                }
                else { con.Open(); }
                SqlCommand cmd2 = new SqlCommand();
                SqlDataReader reader2;
                cmd2.Connection = con;
                cmd2.CommandText = string.Format("Select Funktion from MMS_Mitarbeiterfunktion");
                reader2 = cmd2.ExecuteReader();
                do
                {
                    while (reader2.Read())
                    {     
                        empfunctionlist.Add(reader2.GetString(0));
                    }
                } while (reader2.NextResult());
                reader2.Close();
                con.Close();
                return empfunctionlist;
            }
            catch (InvalidOperationException)
            {
                return null;
            }   
        }

        [HttpGet]
        [Route("GetTimeList")]//Return All Time slots list from database
        public List<String> GetTimeList(string area)
        {
            //List<string> TimeList = Model.getTimelist(area);
            //return TimeList;
            List<string> timeslot = new List<string>();
            string tour = "";
            DateTime Currenttime = DateTime.Now;
            //int compareValue1 = Currenttime.CompareTo(DateTime.Parse("06:30:00"));
            //int compareValue2 = Currenttime.CompareTo(DateTime.Parse("14:30:00"));
            //int compareValue3 = Currenttime.CompareTo(DateTime.Parse("23:00:00"));
            //if (compareValue1 >= 0 && compareValue2 < 0)
            //{
            //    tour = "1";
            //}
            //else if (compareValue2 >= 0 && compareValue3 < 0)
            //{
            //    tour = "2";
            //}
            //else
            //{
            //    tour = "3";
            //}
            try
            {
                con = new SqlConnection(conString);
                if (con.State != System.Data.ConnectionState.Open)
                {

                    con.Close();
                    con.Open();
                }
                else { con.Open(); }
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con; 
                //cmd.CommandText = string.Format("Select COUNT(*) from MMS_Time where Bereich='" + area + "'");
                //int Present = Convert.ToInt32();
                cmd.CommandText = string.Format("select  TOP 1 Time from MMS_Time where Bereich='"+area+"' and Tour='1'");
                int compareValue1 = (Convert.ToString(cmd.ExecuteScalar())== "") ? 0 :Currenttime.CompareTo(DateTime.Parse(Convert.ToString(cmd.ExecuteScalar())+ ":00"));
                cmd.CommandText = string.Format("select  TOP 1 Time from MMS_Time where Bereich='" + area + "' and Tour='2'");
                int compareValue2 = (Convert.ToString(cmd.ExecuteScalar()) == "") ? 0 : Currenttime.CompareTo(DateTime.Parse(Convert.ToString(cmd.ExecuteScalar()) + ":00"));
                cmd.CommandText = string.Format("select  TOP 1 Time from MMS_Time where Bereich='" + area + "' and Tour='3'");
                int compareValue3 = (Convert.ToString(cmd.ExecuteScalar()) == "") ? 0 : Currenttime.CompareTo(DateTime.Parse(Convert.ToString(cmd.ExecuteScalar()) + ":00"));
                if (compareValue1 >= 0 && compareValue2 < 0)
                {
                    tour = "1";
                }
                else if (compareValue2 >= 0 && compareValue3 < 0)
                {
                    tour = "2";
                }
                else
                {
                    tour = "3";
                }

                cmd.CommandText = string.Format("select Time from MMS_Time where Bereich= '" + area + "' and Tour='" + tour + "'");
                SqlDataReader reader = cmd.ExecuteReader();
                do
                {
                    while (reader.Read())
                    {

                        timeslot.Add(reader.GetString(0));
                    }
                } while (reader.NextResult());
                timeslot.Add(tour);
                reader.Close();
                con.Close();
            }
            catch (InvalidOperationException exception)
            {
                // Error logging, post processing etc.
                return null;
            }
            return timeslot;
        }

        [HttpGet]
        [Route("GetEmployeeList")]//Return All Employee list for respective area from database
        public List<String>[] GetEmployeeList(string area)
        {
            //List<string> [] TimeList = Model.getEmployeelist(area);
            //return TimeList;
            List<string>[] employee = new List<string>[2];
            employee[0] = new List<string>();
            employee[1] = new List<string>();
            try
            {
                con = new SqlConnection(conString);
                if (con.State != System.Data.ConnectionState.Open)
                {

                    con.Close();
                    con.Open();
                }
                else { con.Open(); }
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = string.Format("select Name , Vorname , Funktion , MMS_Mitarbeiter.ID from MMS_Mitarbeiterfunktion,MMS_Mitarbeiter where MMS_Mitarbeiter.Funktion_ID = MMS_Mitarbeiterfunktion.ID and MMS_Mitarbeiter.Bereich_ID = (select ID from MMS_Bereich where Bereich ='" + area + "') ORDER BY Name ASC");
                SqlDataReader reader = cmd.ExecuteReader();
                do
                {
                    while (reader.Read())
                    {
                        employee[0].Add(reader.GetString(0) + " " + reader.GetString(1) + " " + reader.GetString(2));
                        string id = Convert.ToString(reader.GetInt32(3));
                        employee[1].Add(id);
                    }
                } while (reader.NextResult());

                reader.Close();
                con.Close();
            }
            catch (InvalidOperationException exception)
            {
                // Error logging, post processing etc.
                return null;
            }
            return employee;
        }

        [HttpGet]
        [Route("InsertEmployeeList")]//Insert Selected Employee list in database for perticular Observers
        public bool insertEmployee(string observerid, string employeeidlist,string action)
        {
            //Model.InsertEmployeelist(observerid, employeeidlist);
            //int i = 0;
            //return true;
            try
            {
              if(employeeidlist != "null")
               { 
                var ids = employeeidlist.Split(',').ToList();
                con = new SqlConnection(conString);
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Close();
                    con.Open();
                }
                else { con.Open(); }
                SqlCommand cmd1 = new SqlCommand();
                cmd1.Connection = con;
                    if (action == "Insert")
                    {
                        cmd1.CommandText = string.Format(" Select COUNT(*) from MMS_BeoMitId where Beo_ID=" + Convert.ToInt32(observerid) + "");
                        int count2 = Convert.ToInt32(cmd1.ExecuteScalar());
                        if (count2 > 0)
                        {
                            SqlCommand cmd2 = new SqlCommand();
                            cmd2.Connection = con;
                            cmd2.CommandText = string.Format("Delete from MMS_BeoMitId where Beo_ID=" + Convert.ToInt32(observerid) + "");
                            cmd2.ExecuteNonQuery();
                        }
                        for (int i = 0; i < ids.Count(); i++)
                        {
                            SqlCommand cmd = new SqlCommand();
                            cmd.Connection = con;
                            cmd.CommandText = string.Format("Insert into MMS_BeoMitId values (" + Convert.ToInt32(observerid) + "," + Convert.ToInt32(ids[i]) + ")");
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else if (action == "Delete")
                    {
                        for (int i = 0; i < ids.Count(); i++)
                        {
                            cmd1.CommandText = string.Format("Delete FROM MMS_MitarbeiterData where Mit_ID   =" + Convert.ToInt32(ids[i]) + "");
                            cmd1.ExecuteNonQuery();
                            cmd1.CommandText = string.Format("Delete FROM MMS_BeoMitId where Mit_ID   =" + Convert.ToInt32(ids[i]) + "");
                            cmd1.ExecuteNonQuery();
                            cmd1.CommandText = string.Format("Delete FROM MMS_Mitarbeiter where ID   =" + Convert.ToInt32(ids[i]) + "");
                            cmd1.ExecuteNonQuery();
                        }
                    }

                    con.Close();
                }
            }
            catch (InvalidOperationException exception)
            {
                // Error logging, post processing etc.
                
            }
            return true;
        }

        [HttpGet]
        [Route("GetSelectEmployeeList")]//Return selected employees list for respective observer from database
        public List<String>[] getSelectEmployee(string area,string Observerid)
        {
            //List<string>[] EmployeeList = Model.getSelectEmployeelist(area, Observerid);
            //return EmployeeList;
            List<string>[] selectemployee = new List<string>[2];
            selectemployee[0] = new List<string>();
            selectemployee[1] = new List<string>();
            try
            {
                con = new SqlConnection(conString);
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Close();
                    con.Open();
                }
                else { con.Open(); }
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = string.Format("select mm.Name , mm.Vorname , mmf.Funktion , mm.ID from  MMS_Mitarbeiterfunktion mmf, MMS_Mitarbeiter mm,MMS_BeoMitId where mm.Funktion_ID = mmf.ID and MMS_BeoMitId.Beo_ID=" + Convert.ToInt32(Observerid) + " and MMS_BeoMitId.Mit_ID = mm.ID GROUP BY mm.Name , mm.Vorname , mmf.Funktion , mm.ID ORDER BY mm.Name ASC");
                SqlDataReader reader = cmd.ExecuteReader();
                do
                {
                    while (reader.Read())
                    {

                        selectemployee[0].Add(reader.GetString(0) + " " + reader.GetString(1) + " " + reader.GetString(2));
                        string id = Convert.ToString(reader.GetInt32(3));
                        selectemployee[1].Add(id);
                    }
                } while (reader.NextResult());
                reader.Close();
                con.Close();
                return selectemployee;
            }
            catch (InvalidOperationException exception)
            {
                // Error logging, post processing etc.
                return null;
            }
           
        }

        [HttpGet]
        [Route("GetAbsentEmployeeList")]//Return Absent Employee list from database(Nicht anwesend)
        public List<String> GetAbsentEmployeeList(string date, string area,string shift)
        {
            List<string> absentemployee = new List<string>();
            
            try
            {
                con = new SqlConnection(conString);
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Close();
                    con.Open();
                }
                else { con.Open(); }
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = string.Format("select Mit_ID from  MMS_MitarbeiterData where Datum='"+date+ "' And Bereich='" + area + "'And Schicht='" + shift + "'And Taegtigkeit='Nicht anwesend'");
                SqlDataReader reader = cmd.ExecuteReader();
                do
                {
                    while (reader.Read())
                    {
                        
                        absentemployee.Add(Convert.ToString(reader.GetInt32(0)));
                    }
                } while (reader.NextResult());
                reader.Close();
                con.Close();
                return absentemployee;
            }
            catch (InvalidOperationException exception)
            {
                // Error logging, post processing etc.
                return null;
            }
        }


        [HttpGet]
        [Route("GetEmployeeActivitiesList")]//Return Employee Activities list from database
        public List<String>[] getEmployeeActivities()
        {
            List<string>[] employeeactivities = new List<string>[2];
            employeeactivities[0] = new List<string>();
            employeeactivities[1] = new List<string>();
            try
            {
                con = new SqlConnection(conString);
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Close();
                    con.Open();
                }
                else { con.Open(); }
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = string.Format("Select ID,Taetigkeit from MMS_Taetigkeit T where T.ID NOT IN  (select Untertaetigkeit from MMS_Obertaetigkeit_Untertaetigkeit)  ORDER BY Taetigkeit ASC");
                SqlDataReader reader = cmd.ExecuteReader();
                do
                {
                    while (reader.Read())
                    {
                        string id = Convert.ToString(reader.GetInt32(0));
                        employeeactivities[0].Add(id);
                        employeeactivities[1].Add(reader.GetString(1));
                    }
                } while (reader.NextResult());

                reader.Close();
                con.Close();
                return employeeactivities;
            }
            catch (InvalidOperationException exception)
            {
                // Error logging, post processing etc.
                return null;
            }
           
        }

        [HttpGet]
        [Route("GetEmployeeSubActivitiesList")]//Return Employee Sub-activities list from database
        public List<String>[] getEmployeeSubActivities(string activity)
        {
            List<string>[] employeesubactivities = new List<string>[2];
            employeesubactivities[0] = new List<string>();
            employeesubactivities[1] = new List<string>();
            try
            {
                con = new SqlConnection(conString);
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Close();
                    con.Open();
                }
                else { con.Open(); }
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
              
                if(activity == "ReportActivities")//Get activities for report only
                {
                    //cmd.CommandText = string.Format("Select ID,Taetigkeit from MMS_Taetigkeit where NOT (Taetigkeit='Nicht anwesend')");
                    cmd.CommandText = string.Format("Select ID,Taetigkeit from MMS_Taetigkeit ORDER BY Taetigkeit ASC");
                }
               else if (activity != null)
                {
                    cmd.CommandText = string.Format("Select T.ID,T.Taetigkeit from MMS_Taetigkeit T , MMS_Obertaetigkeit_Untertaetigkeit OU where T.ID=OU.Untertaetigkeit and OU.Obertaetigkeit= (Select ID from MMS_Taetigkeit  where MMS_Taetigkeit.Taetigkeit='" + activity + "') ORDER BY T.Taetigkeit ASC");
                }
                else
                {
                    cmd.CommandText = string.Format("Select T.ID,T.Taetigkeit from MMS_Taetigkeit T , MMS_Obertaetigkeit_Untertaetigkeit OU where T.ID=OU.Untertaetigkeit ORDER BY T.Taetigkeit ASC");
                }
                
                SqlDataReader reader = cmd.ExecuteReader();
                do
                {
                    while (reader.Read())
                    {
                        string id = Convert.ToString(reader.GetInt32(0));
                        employeesubactivities[0].Add(id);
                        employeesubactivities[1].Add(reader.GetString(1));
                    }
                } while (reader.NextResult());
            }
            catch (InvalidOperationException exception)
            {
                // Error logging, post processing etc.
                return null;
            }
            return employeesubactivities;
        }

        [HttpGet]
        [Route("GetMachineLineList")]//Return Machine lines list from database
        public List<String>[] getMachineline(string place,string type)
        {
            List<string>[] machinelines = new List<string>[2];
            machinelines[0] = new List<string>();
            machinelines[1] = new List<string>();
            try
            {
                con = new SqlConnection(conString);
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Close();
                    con.Open();
                }
                else { con.Open(); }
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                if (type == "emp")
                {
                    cmd.CommandText = string.Format("select O.ID,O.Bezeichnung from MMS_Ort O, MMS_Oberort_Unterort OU where O.ID NOT IN(select Unterort from MMS_Oberort_Unterort) and O.Bereich_ID = (select ID from MMS_Bereich where Bereich = '" + place + "')  group by O.ID,O.Bezeichnung ORDER BY O.Bezeichnung ASC");      
                }
                else
                {
                    //cmd.CommandText = string.Format("select O.ID,O.Bezeichnung from MMS_Ort O, MMS_Oberort_Unterort OU where O.ID NOT IN(select Unterort from MMS_Oberort_Unterort) and O.Bereich_ID = (select ID from MMS_Bereich where Bereich = '" + place + "')and  O.Typ='Hybrid' group by O.ID,O.Bezeichnung");
                    cmd.CommandText = string.Format("select O.ID,O.Bezeichnung from MMS_Ort O, MMS_Oberort_Unterort OU where O.ID NOT IN(select Unterort from MMS_Oberort_Unterort) and O.Bereich_ID = (select ID from MMS_Bereich where Bereich = '" + place + "')and  O.Typ='Hybrid' group by O.ID,O.Bezeichnung  ORDER BY O.Bezeichnung ASC");
                }
                SqlDataReader reader = cmd.ExecuteReader();
                do
                {
                    while (reader.Read())
                    {
                        string id = Convert.ToString(reader.GetInt32(0));
                        machinelines[0].Add(id);
                        machinelines[1].Add(reader.GetString(1));
                    }
                } while (reader.NextResult());

                reader.Close();
                con.Close();
                return machinelines;
            }
            catch (InvalidOperationException exception)
            {
                // Error logging, post processing etc.
                return null;
            }
           
        }

        [HttpGet]
        [Route("GetMachineList")]//Return Machines list from database
        public List<String>[] getMachines(string machineline , string param)
        {
            //List<string> Machinelist = Model.getMachinelist(machineline,param);
            //return Machinelist;
            //List<string> machineslist = new List<string>();
            List<string>[] machineslist = new List<string>[2];
            machineslist[0] = new List<string>();
            machineslist[1] = new List<string>();
            try
            {
                con = new SqlConnection(conString);
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Close();
                    con.Open();
                }
                else { con.Open(); }
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                if(machineline != null)
                {
                if (param == "emp")
                {
                    cmd.CommandText = string.Format("Select O.ID,O.Bezeichnung from MMS_Ort O , MMS_Oberort_Unterort OU where O.ID=OU.Unterort and OU.Oberort= (Select ID from MMS_Ort  where MMS_Ort.Bezeichnung='" + machineline + "')ORDER BY O.Bezeichnung ASC");
                }
                else
                {
                    cmd.CommandText = string.Format("Select O.ID,O.Bezeichnung from MMS_Ort O , MMS_Oberort_Unterort OU where O.ID=OU.Unterort and  O.Typ='Hybrid' and OU.Oberort= (Select ID from MMS_Ort  where MMS_Ort.Bezeichnung='" + machineline + "')ORDER BY O.Bezeichnung  ASC");
                }
                }
                else
                {
                    if (param == "emp")
                    {
                        cmd.CommandText = string.Format("Select O.ID,O.Bezeichnung from MMS_Ort O , MMS_Oberort_Unterort OU where O.ID=OU.Unterort");
                   }
                    else
                    {
                        cmd.CommandText = string.Format("Select O.ID,O.Bezeichnung from MMS_Ort O , MMS_Oberort_Unterort OU where O.ID=OU.Unterort and  O.Typ='Hybrid'");
                    }
                }
                SqlDataReader reader = cmd.ExecuteReader();
                do
                {
                    while (reader.Read())
                    {
                        //machineslist.Add(reader.GetString(0));
                        string id = Convert.ToString(reader.GetInt32(0));
                        machineslist[0].Add(id);
                        machineslist[1].Add(reader.GetString(1));
                    }
                } while (reader.NextResult());

                reader.Close();
                con.Close();
                return machineslist;
            }
            catch (InvalidOperationException exception)
            {
                // Error logging, post processing etc.
                return null;
            }
           
        }
       
        [HttpGet]
        [Route("GetMachineStateList")]//Return Machine state list from database
        public List<String>[] getMachinestates()
        {
            //List<string> machinestate = new List<string>();
            List<string>[] machinestate = new List<string>[2];
            machinestate[0] = new List<string>();
            machinestate[1] = new List<string>();
            try
            {
                con = new SqlConnection(conString);
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Close();
                    con.Open();
                }
                else { con.Open(); }
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = string.Format("select ID,Zustand from MMS_Maschinenzustand ORDER BY Zustand ASC");
                SqlDataReader reader = cmd.ExecuteReader();
                do
                {
                    while (reader.Read())
                    {
                        //machinestate.Add(reader.GetString(0));
                        string id = Convert.ToString(reader.GetInt32(0));
                        machinestate[0].Add(id);
                        machinestate[1].Add(reader.GetString(1));
                    }
                } while (reader.NextResult());

                reader.Close();
                con.Close();
                return machinestate;
            }
            catch (InvalidOperationException exception)
            {
                // Error logging, post processing etc.
                return null;
            }     
        }

        [HttpGet]
        [Route("InsertTimeSlotList")]//Insert different timeslots in database
        public bool InsertTimeslots(string time, string place, string tour,string starttime,string endtime)
        {
            DateTime startTime = DateTime.Parse(starttime + ":00");
            DateTime endTime = DateTime.Parse(endtime + ":00");
            try
            {
                con = new SqlConnection(conString);
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Close();
                    con.Open();
                }
                else { con.Open(); }
                SqlCommand cmd1 = new SqlCommand();
                cmd1.Connection = con;
                cmd1.CommandText = string.Format(" Select COUNT(*) from MMS_Time where Bereich= '" + place + "' and Tour='" + tour + "'");
                int count2 = Convert.ToInt32(cmd1.ExecuteScalar()); 
                if (count2 > 0)
                {
                    SqlCommand cmd2 = new SqlCommand();
                    cmd2.Connection = con;
                    cmd2.CommandText = string.Format("Delete from MMS_Time where Bereich= '" + place + "' and Tour='" + tour + "'");
                    cmd2.ExecuteNonQuery();
                }
               while (startTime <= endTime)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = string.Format("Insert into MMS_Time values ('" + startTime.ToString("HH:mm") + "','" + place + "' ,'" + tour + "')");
                    cmd.ExecuteNonQuery();
                    startTime = startTime.AddMinutes(Convert.ToInt32(time));
                }
                con.Close();
            }
            catch (InvalidOperationException exception)
            {
                // Error logging, post processing etc.

            }
            return true;
        }
        
        [HttpGet]
        [Route("InsertMitarbeiterData")]//Insert Employee Data for report
        public bool InsertEmployeeData(string data)
        {
            //Model.Insertemployeedata(data);
            //return true;
          try
            {
                //var empdata = data.Split(',').ToList();
                var empdata = (data == null) ? null : data.Split(',').ToList();
                con = new SqlConnection(conString);
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Close();
                    con.Open();
                }
                else { con.Open(); }
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                if (empdata != null) { 
                if(empdata.Count()==10)
                {
                    cmd.CommandText = string.Format("Insert into MMS_MitarbeiterData values (" + Convert.ToInt32(empdata[0]) + ",'" + empdata[1] + "','" + empdata[2] + "','" + empdata[3] + "','" + empdata[9] + "','" + empdata[6] + "','" + empdata[7] + "'," + empdata[5] + ")");
                }
                else if(empdata.Count() == 8)
                {
                    cmd.CommandText = string.Format("Insert into MMS_MitarbeiterData values (" + Convert.ToInt32(empdata[0]) + ",'" + empdata[1] + "','" + empdata[2] + "','" + empdata[3] + "','','" + empdata[6] + "','" + empdata[7] + "'," + empdata[5] + ")");
                }
                }
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (InvalidOperationException exception)
            {
                // Error logging, post processing etc.

            }
            return true;
        }

        [HttpGet]
        [Route("InsertMaschineData")]//Insert Machine Data for report
        public bool InsertMachineData(string data)
        {
            //Model.Insertmachinedata(data);
            //return true;
            try
            {
                var machinedata = data.Split(',').ToList();
                con = new SqlConnection(conString);
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Close();
                    con.Open();
                }
                else { con.Open(); }
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                if (machinedata.Count() == 6)
                {
                    cmd.CommandText = string.Format("Insert into MMS_MaschineData values (" + Convert.ToInt32(machinedata[0]) + ",'" + machinedata[1] + "','" + machinedata[2] + "','" + machinedata[3] + "','" + machinedata[4] + "','" + machinedata[5] + "')");
                    cmd.ExecuteNonQuery();
                }  
                con.Close();
            }
            catch (InvalidOperationException exception)
            {
                // Error logging, post processing etc.
            }
            return true;
        }

        [HttpGet]
        [Route("GetlangData")]//Return different languages data from database
        public List<String> getlangdata(string lang)
        {
            // lang = "DE";
            //List<string> langlist = Model.getLangdata(lang);
            //return langlist;
            List<string> langdata = new List<string>();
            try
            {
                con = new SqlConnection(conString);
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Close();
                    con.Open();
                }
                else { con.Open(); }
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = string.Format("SELECT Textdata  FROM MMS_Multilanguage where lang='" + lang + "'");
                SqlDataReader reader = cmd.ExecuteReader();
                do
                {
                    while (reader.Read())
                    {
                        langdata.Add(reader.GetString(0));
                    }
                } while (reader.NextResult());

                reader.Close();
                con.Close();
                return langdata;
            }
            catch (InvalidOperationException exception)
            {
                // Error logging, post processing etc.
                return null;
            }
           
        
       }

        [HttpGet]
        [Route("InsertObserverData")]//Insert Observers in the Database
        public bool insertobserver(string name, string vorname,string action)
        {
            try
            {
                con = new SqlConnection(conString);
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Close();
                    con.Open();
                }
                else { con.Open(); }
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                if(action == "Insert")
                {
                    cmd.CommandText = string.Format("INSERT INTO MMS_Beobachter (Name,Vorname) VALUES ('" + name + "','" + vorname + "')");
                }
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (InvalidOperationException exception)
            {
                // Error logging, post processing etc.

            }
            return true;
        }

        [HttpGet]
        [Route("EditObserverData")]//Edit Observers in the Database
        public bool editobserver(int id,string name, string vorname, string action)
        {
            try
            {
                con = new SqlConnection(conString);
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Close();
                    con.Open();
                }
                else { con.Open(); }
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                if (action == "Update")
                {
                    cmd.CommandText = string.Format("UPDATE MMS_Beobachter SET Name = '" + name + "' ,Vorname='" + vorname + "'  WHERE ID = " + id + "");
                    cmd.ExecuteNonQuery();
                }
                else if (action == "Delete")
                {
                    cmd.CommandText = string.Format("Delete FROM MMS_MitarbeiterData where Beo_ID =" + id + "");
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = string.Format("Delete FROM MMS_MaschineData where Beo_ID =" + id + "");
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = string.Format("Delete FROM MMS_BeoMitId where Beo_ID =" + id + "");
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = string.Format("Delete FROM MMS_Beobachter where ID=" + id + "");
                    cmd.ExecuteNonQuery();
                }
            
                con.Close();
            }
            catch (InvalidOperationException exception)
            {
                // Error logging, post processing etc.

            }
            return true;
        }

        [HttpGet]
        [Route("InsertAreaData")]//Insert Areas in the Database
        public bool insertarea(string area, string action)
        {
            try
            {
                con = new SqlConnection(conString);
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Close();
                    con.Open();
                }
                else { con.Open(); }
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                if (action == "Insert")
                {
                    cmd.CommandText = string.Format("INSERT INTO MMS_Bereich (Bereich) VALUES ('" + area + "')");
                }
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (InvalidOperationException exception)
            {
                // Error logging, post processing etc.

            }
            return true;
        }
        [HttpGet]
        [Route("EditAreaData")]//Edit Areas in the Database
        public bool editarea(int id,string area, string action)
        {
            List<int> Idlist = new List<int>();
            try
            {
                con = new SqlConnection(conString);
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Close();
                    con.Open();
                }
                else { con.Open(); }
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;
                cmd.Connection = con;
                if (action == "Update")
                {
                    cmd.CommandText = string.Format("UPDATE MMS_Bereich SET Bereich = '" + area + "' WHERE ID = " + id + "");
                    cmd.ExecuteNonQuery();
                }
                else if (action == "Delete")
                {   
                    cmd.CommandText = string.Format("select ID from MMS_Mitarbeiter where Bereich_ID=" + id + "");
                    reader = cmd.ExecuteReader();
                    do
                    {
                        while (reader.Read())
                        {
                            Idlist.Add(reader.GetInt32(0));   
                        }
                    } while (reader.NextResult());
                    reader.Close();
                    for (int i=0;i<Idlist.Count();i++)
                    {
                        cmd.CommandText = string.Format("Delete FROM MMS_MitarbeiterData where Mit_ID   =" + Idlist[i] + "");
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = string.Format("Delete FROM MMS_BeoMitId where Mit_ID   =" + Idlist[i] + "");
                        cmd.ExecuteNonQuery();
                    }
                    cmd.CommandText = string.Format("Delete FROM MMS_Mitarbeiter where Bereich_ID  =" + id + "");
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = string.Format("Delete FROM MMS_Bereich where ID=" + id + "");
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
            catch (InvalidOperationException exception)
            {
                // Error logging, post processing etc.

            }
            return true;
        }

        [HttpGet]
        [Route("InsertEmployeeData")]//Insert Employees in the Database
        public bool insertemployee(string name,string vorname,string empfunction, string area,string obsname,string action)
        {
           int id = 0;
            try
            {
                con = new SqlConnection(conString);
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Close();
                    con.Open();
                }
                else { con.Open(); }
                
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = string.Format("Select MF.ID from MMS_Mitarbeiterfunktion MF where  MF.Funktion='"+ empfunction + "'");
                SqlDataReader reader1 = cmd.ExecuteReader();
                if (reader1.FieldCount > 0)
                {
                    while (reader1.Read())
                    {
                        id = reader1.GetInt32(0);
                        //string id1 = Convert.ToString(reader1.GetInt32(0));
                    }
                    reader1.Close();
                }
               if(id == 0)
                {
                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.Connection = con;
                    cmd1.CommandText = string.Format("INSERT INTO MMS_Mitarbeiterfunktion (Funktion) VALUES ('" + empfunction + "')");
                    cmd1.ExecuteNonQuery();
                    SqlCommand cmd2 = new SqlCommand();
                    cmd2.Connection = con;
                    cmd2.CommandText = string.Format("Select MF.ID from MMS_Mitarbeiterfunktion MF where  MF.Funktion='" + empfunction + "'");
                    SqlDataReader reader2 = cmd2.ExecuteReader();
                    if (reader2.FieldCount > 0)
                    {
                        while (reader2.Read())
                        {
                            id = reader2.GetInt32(0);
                        }
                        reader2.Close();
                    }
                }    
                SqlCommand cmd3 = new SqlCommand();
                cmd3.Connection = con;
                if (action == "Insert")
                {
                    cmd3.CommandText = string.Format("INSERT INTO  MMS_Mitarbeiter  (Name,Vorname,Funktion_ID,Bereich_ID) VALUES ('" + name + "','" + vorname + "'," + id + ", (select ID from MMS_Bereich where Bereich ='" + area + "'))");
                    cmd3.ExecuteNonQuery();
                }
                
              if(obsname != "NA" && action == "Insert")
                {
                    SqlCommand cmd4 = new SqlCommand();
                    cmd4.Connection = con;
                    cmd4.CommandText = string.Format("INSERT INTO  MMS_BeoMitId  (Beo_ID,Mit_ID) VALUES (" + Convert.ToInt32(obsname) + ",(select ID from MMS_Mitarbeiter where Name ='" + name + "' and Vorname='" + vorname + "' and Funktion_ID='" + id + "'))");
                    cmd4.ExecuteNonQuery();
                }
                con.Close();
            }
            catch (InvalidOperationException exception)
            {
                // Error logging, post processing etc.
            }
            return true;
        }
        
        [HttpGet]
        [Route("EditEmployeeData")]//Insert Employees in the Database
        public bool editemployee(int id,string name, string vorname, string empfunction, string area,string action)
        {
            try
            {
                con = new SqlConnection(conString);
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Close();
                    con.Open();
                }
                else { con.Open(); }
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                if (action == "Update")
                {
                    cmd.CommandText = string.Format("UPDATE MMS_Mitarbeiter SET Name = '" + name + "',Vorname = '" + vorname + "',Bereich_ID =(select ID from MMS_Bereich where Bereich ='" + area + "') WHERE ID = " + id + "");
                    cmd.ExecuteNonQuery();
                }
                else if (action == "Delete")
                {      
                        cmd.CommandText = string.Format("Delete FROM MMS_MitarbeiterData where Mit_ID   =" + id + "");
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = string.Format("Delete FROM MMS_BeoMitId where Mit_ID   =" + id + "");
                        cmd.ExecuteNonQuery();
                        //cmd.CommandText = string.Format("Delete FROM MMS_Mitarbeiter where ID   =" + id + "");
                        //cmd.ExecuteNonQuery();
                }
                con.Close();
            }
            catch (InvalidOperationException exception)
            {
                // Error logging, post processing etc.
            }
            return true;
        }

        [HttpGet]
        [Route("InsertActivityData")]//Insert Employee activities in the Database
        public bool insertactivity(string activity, string downactivity,string action)
        {
            int id = 0;
            try
            {
                con = new SqlConnection(conString);
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Close();
                    con.Open();
                }
                else { con.Open(); }
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = string.Format("Select ID from MMS_Taetigkeit where  Taetigkeit='" + activity + "'");
                SqlDataReader reader1 = cmd.ExecuteReader();
                if (reader1.FieldCount > 0)
                {
                    while (reader1.Read())
                    {
                        id = reader1.GetInt32(0);
                    }
                    reader1.Close();
                }
                if (id == 0 && action == "Insert")
                {
                    //reader1.Close();
                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.Connection = con;
                    cmd1.CommandText = string.Format("INSERT INTO MMS_Taetigkeit (Taetigkeit) VALUES ('" + activity + "')");
                    cmd1.ExecuteNonQuery();
                }
                if(downactivity != null && action == "Insert")
                {
                    SqlCommand cmd2 = new SqlCommand();
                    cmd2.Connection = con;
                    cmd2.CommandText = string.Format("INSERT INTO MMS_Taetigkeit (Taetigkeit) VALUES ('" + downactivity + "')");
                    cmd2.ExecuteNonQuery();
                    SqlCommand cmd3 = new SqlCommand();
                    cmd3.Connection = con;
                    cmd3.CommandText = string.Format("INSERT INTO MMS_Obertaetigkeit_Untertaetigkeit (Obertaetigkeit,Untertaetigkeit) VALUES ((Select ID from MMS_Taetigkeit where  Taetigkeit='" + activity + "'),(Select ID from MMS_Taetigkeit where  Taetigkeit='" + downactivity + "'))");
                    cmd3.ExecuteNonQuery();
                }
                con.Close();
            }
            catch (InvalidOperationException exception)
            {
                // Error logging, post processing etc.
            }
            return true;
        }
        
        [HttpGet]
        [Route("EditActivityData")]//Insert Employee activities in the Database
        public bool editactivity(int id,string activity,string action)
        {
            try
            {
                List<int> Idlist = new List<int>();
                con = new SqlConnection(conString);
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Close();
                    con.Open();
                }
                else { con.Open(); }
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                if (action == "Update")
                {
                    cmd.CommandText = string.Format("UPDATE MMS_Taetigkeit SET Taetigkeit = '" + activity + "' WHERE ID = " + id + "");
                    cmd.ExecuteNonQuery();
                }
                else if (action == "Delete")
                {
                    cmd.CommandText = string.Format("select Untertaetigkeit from MMS_Obertaetigkeit_Untertaetigkeit where Obertaetigkeit=" + id + "");
                    SqlDataReader reader = cmd.ExecuteReader();
                    do
                    {
                        while (reader.Read())
                        {
                            Idlist.Add(reader.GetInt32(0));
                        }
                    } while (reader.NextResult());
                    reader.Close();
                    cmd.CommandText = string.Format("Delete FROM MMS_Obertaetigkeit_Untertaetigkeit where Obertaetigkeit   =" + id + "");
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = string.Format("Delete FROM MMS_Taetigkeit where ID   =" + id + "");
                    cmd.ExecuteNonQuery();
                    for (int i = 0; i < Idlist.Count(); i++)
                    {
                        cmd.CommandText = string.Format("Delete FROM MMS_Taetigkeit where ID   =" + Idlist[i] + "");
                        cmd.ExecuteNonQuery();
                    }
                }
                con.Close();
            }
            catch (InvalidOperationException exception)
            {
                // Error logging, post processing etc.

            }
            return true;
        }
        [HttpGet]
        [Route("EditSubActivityData")]//Insert Employee activities in the Database
        public bool editsubactivity(int id, string activity, string action)
        {
            try
            {
                con = new SqlConnection(conString);
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Close();
                    con.Open();
                }
                else { con.Open(); }
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                if (action == "Update")
                {
                    cmd.CommandText = string.Format("UPDATE MMS_Taetigkeit SET Taetigkeit = '" + activity + "' WHERE ID = " + id + "");
                    cmd.ExecuteNonQuery();
                }
                else if (action == "Delete")
                {
                    cmd.CommandText = string.Format("Delete FROM MMS_Obertaetigkeit_Untertaetigkeit where Untertaetigkeit   =" + id + "");
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = string.Format("Delete FROM MMS_Taetigkeit where ID   =" + id + "");
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
            catch (InvalidOperationException exception)
            {
                // Error logging, post processing etc.

            }
            return true;
        }
        [HttpGet]
        [Route("InsertMachineData")]//Insert Machines in the Database
        public bool insertmachine(string place, string machine,string downmachine,string type,string action)
        {
            int id = 0;
            try
            {
                con = new SqlConnection(conString);
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Close();
                    con.Open();
                }
                else { con.Open(); }
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = string.Format("Select ID from MMS_Ort where  Bezeichnung='" + machine + "'");
                SqlDataReader reader1 = cmd.ExecuteReader();
                if (reader1.FieldCount > 0)
                {
                    while (reader1.Read())
                    {
                        id = reader1.GetInt32(0);
                    }
                    reader1.Close();
                }
                if (id == 0 && action=="Insert")
                {
                    reader1.Close();
                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.Connection = con;
                    cmd1.CommandText = string.Format("INSERT INTO MMS_Ort (Bereich_ID,Bezeichnung,Typ) VALUES ((Select ID from MMS_Bereich where  Bereich='" + place + "'),'" + machine + "','" + type + "')");
                    cmd1.ExecuteNonQuery();
                }
                if (action == "Insert") {
                    SqlCommand cmd2 = new SqlCommand();
                    cmd2.Connection = con;
                    cmd2.CommandText = string.Format("INSERT INTO MMS_Ort (Bereich_ID,Bezeichnung,Typ) VALUES ((Select ID from MMS_Bereich where  Bereich='" + place + "'),'" + downmachine + "','" + type + "')");
                    cmd2.ExecuteNonQuery();
                    SqlCommand cmd3 = new SqlCommand();
                    cmd3.Connection = con;
                    cmd3.CommandText = string.Format("INSERT INTO MMS_Oberort_Unterort (Oberort,Unterort) VALUES ((Select ID from MMS_Ort where  Bezeichnung='" + machine + "'),(Select ID from MMS_Ort where  Bezeichnung='" + downmachine + "'))");
                    cmd3.ExecuteNonQuery();
                }
                //if (action == "Delete" && downmachine != "NA")
                //{
                //    SqlCommand cmd3 = new SqlCommand();
                //    cmd3.Connection = con;
                //    cmd3.CommandText = string.Format("Delete From MMS_Oberort_Unterort where Oberort=(Select ID from MMS_Ort where  Bezeichnung='" + machine + "') and Unterort=(Select ID from MMS_Ort where  Bezeichnung='" + downmachine + "')");
                //    cmd3.ExecuteNonQuery();
                //    SqlCommand cmd2 = new SqlCommand();
                //    cmd2.Connection = con;
                //    cmd2.CommandText = string.Format("Delete From MMS_Ort where Bereich_ID=(Select ID from MMS_Bereich where  Bereich='" + place + "') and Bezeichnung= '" + downmachine + "' and Typ='" + type + "'");
                //    cmd2.ExecuteNonQuery();  
                //}
                //if (action == "Delete" && downmachine == "NA")
                //{
                //    SqlCommand cmd3 = new SqlCommand();
                //    cmd3.Connection = con;
                //    cmd3.CommandText = string.Format("Delete From MMS_Oberort_Unterort where Oberort=(Select ID from MMS_Ort where  Bezeichnung='" + machine + "')");
                //    cmd3.ExecuteNonQuery();
                //    SqlCommand cmd2 = new SqlCommand();
                //    cmd2.Connection = con;
                //    cmd2.CommandText = string.Format("Delete From MMS_Ort where Bereich_ID=(Select ID from MMS_Bereich where  Bereich='" + place + "') and Bezeichnung= '" + machine + "' and Typ='" + type + "'");
                //    cmd2.ExecuteNonQuery();
                //}

                con.Close();
            }
            catch (InvalidOperationException exception)
            {
                // Error logging, post processing etc.

            }
            return true;
        }
        
        [HttpGet]
        [Route("EditMachineLineData")]//Edit Machine lines in the Database
        public bool editmachineline(int id,string place, string machineline,string action)
        {
            List<int> Idlist = new List<int>();
            try
            {
                con = new SqlConnection(conString);
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Close();
                    con.Open();
                }
                else { con.Open(); }
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                if (action == "Update")
                {
                    cmd.CommandText = string.Format("UPDATE MMS_Ort SET Bezeichnung = '" + machineline + "' WHERE ID = " + id + "");
                    cmd.ExecuteNonQuery();
                }
                else if (action == "Delete")
                {
                    cmd.CommandText = string.Format("select Unterort from MMS_Oberort_Unterort where Oberort=" + id + "");
                    SqlDataReader reader = cmd.ExecuteReader();
                    do
                    {
                        while (reader.Read())
                        {
                            Idlist.Add(reader.GetInt32(0));
                        }
                    } while (reader.NextResult());
                    reader.Close();
                    cmd.CommandText = string.Format("Delete From MMS_Oberort_Unterort where Oberort="+id+"");
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = string.Format("Delete From MMS_Ort where ID=" + id + "");
                    cmd.ExecuteNonQuery();
                    for (int i = 0; i < Idlist.Count(); i++)
                    {
                        cmd.CommandText = string.Format("Delete FROM MMS_Ort where ID   =" + Idlist[i] + "");
                        cmd.ExecuteNonQuery();
                    }
                }
                con.Close();
            }
            catch (InvalidOperationException exception)
            {
                // Error logging, post processing etc.

            }
            return true;
        }
        [HttpGet]
        [Route("EditMachineData")]//Edit Machines in the Database
        public bool editmachine(int id, string place, string machineline, string action)
        {
            try
            {
                con = new SqlConnection(conString);
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Close();
                    con.Open();
                }
                else { con.Open(); }
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                if (action == "Update")
                {
                    cmd.CommandText = string.Format("UPDATE MMS_Ort SET Bezeichnung = '" + machineline + "' WHERE ID = " + id + "");
                    cmd.ExecuteNonQuery();
                }
                else if (action == "Delete")
                {                
                    cmd.CommandText = string.Format("Delete From MMS_Oberort_Unterort where Unterort=" + id + "");
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = string.Format("Delete From MMS_Ort where ID=" + id + "");
                    cmd.ExecuteNonQuery();                
                }
                con.Close();
            }
            catch (InvalidOperationException exception)
            {
                // Error logging, post processing etc.
            }
            return true;
        }
        [HttpGet]
        [Route("InsertMachineStateData")] //Insert Machine state in the Database
        public bool insertmachinestate(string state,string action)
        {
            try
            {
                con = new SqlConnection(conString);
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Close();
                    con.Open();
                }
                else { con.Open(); }
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                if (action == "Insert")
                {
                    cmd.CommandText = string.Format("INSERT INTO MMS_Maschinenzustand (Zustand) VALUES ('" + state + "')");
                }
                else if (action == "Delete")
                {
                    cmd.CommandText = string.Format("Delete From  MMS_Maschinenzustand where Zustand='" + state + "'");
                }

                    cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (InvalidOperationException exception)
            {
                // Error logging, post processing etc.

            }
            return true;
        }

        [HttpGet]
        [Route("EditMachineStateData")] //Insert Machine state in the Database
        public bool editmachinestate(int id,string statename, string action)
        {
            try
            {
                con = new SqlConnection(conString);
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Close();
                    con.Open();
                }
                else { con.Open(); }
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                if (action == "Update")
                {
                    cmd.CommandText = string.Format("UPDATE MMS_Maschinenzustand SET Zustand = '" + statename + "' WHERE ID = " + id + "");
                    cmd.ExecuteNonQuery();
                }
                else if (action == "Delete")
                {
                    cmd.CommandText = string.Format("Delete From  MMS_Maschinenzustand where ID='" + id + "'");
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
            catch (InvalidOperationException exception)
            {
                // Error logging, post processing etc.

            }
            return true;
        }
        [HttpGet]
        [Route("GetReport")]//Return Report for Employee
        public List<String> GetReport(string startdate, string enddate, string obsradio, string arearadio, string actradio, string funcradio, string ortradio, string obscheckedboxes, string areacheckedboxes, string actcheckedboxes, string funccheckedboxes, string ortcheckedboxes, string shiftcheckedboxes,string ExceptDate)
       {
            List<string> reportlist = new List<string>();
            int count1 = 0, count2 = 0, count3 = 0, count4 = 0, count5 = 0, count6 = 0;
          try
            {       
                con = new SqlConnection(conString);
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Close();
                    con.Open();
                }
                else { con.Open(); }
                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = con;
                SqlCommand cmd3 = new SqlCommand();
                cmd3.Connection = con;
                var obsids = (obscheckedboxes == null) ? null : obscheckedboxes.Split(',').ToList();// WHERE (Datum >= '2017-1-5' AND Datum <=  '2017-1-9' And Not(Datum='2017-1-5'))
                var ExceptDates = (ExceptDate == null) ? null : ExceptDate.Split(',').ToList();
                if (ExceptDate == null)
                { 
                    cmd2.CommandText = string.Format("Select COUNT(*) from MMS_MitarbeiterData where Datum >= '" + startdate + "' AND Datum <= '" + enddate + "'");
                }
                else
                {
                    cmd2.CommandText = string.Format("Select COUNT(*) from MMS_MitarbeiterData where Datum >= '" + startdate + "' AND Datum <= '" + enddate + "'") + string.Format("AND (");
                    for (int i = 0; i < ExceptDates.Count(); i++)
                    {
                        cmd2.CommandText = (i == ExceptDates.Count - 1) ? cmd2.CommandText + string.Format("NOT(Datum='" + (ExceptDates[i]) + "'))") : cmd2.CommandText + string.Format("NOT(Datum='" +(ExceptDates[i]) + "') AND ");
                    }
                }
                if (obsradio == "Alle")
                   {
                        count1 = Convert.ToInt32(cmd2.ExecuteScalar());
                        cmd2.CommandText = (CallArea(arearadio,areacheckedboxes) == null) ? cmd2.CommandText : cmd2.CommandText + string.Format(CallArea(arearadio, areacheckedboxes));
                        count2 = Convert.ToInt32(cmd2.ExecuteScalar());
                        cmd2.CommandText = (CallShift(shiftcheckedboxes) == null) ? cmd2.CommandText : cmd2.CommandText + string.Format(CallShift(shiftcheckedboxes));
                        count3 = Convert.ToInt32(cmd2.ExecuteScalar());
                        cmd2.CommandText = (CallFunction(funcradio,funccheckedboxes) == null) ? cmd2.CommandText : cmd2.CommandText + string.Format(CallFunction(funcradio, funccheckedboxes));
                        count4 = Convert.ToInt32(cmd2.ExecuteScalar());
                        cmd2.CommandText = (CallOrt(ortradio, ortcheckedboxes) == null) ? cmd2.CommandText : cmd2.CommandText + string.Format(CallOrt(ortradio, ortcheckedboxes));
                        count5 = Convert.ToInt32(cmd2.ExecuteScalar());
                        cmd2.CommandText = (CallActivity(actradio, actcheckedboxes) == null) ? cmd2.CommandText : cmd2.CommandText + string.Format(CallActivity(actradio, actcheckedboxes));
                        count6 = Convert.ToInt32(cmd2.ExecuteScalar());
                  }
             else if (obsradio == "Nur von" && obsids[0] != "null")
                  {
                    cmd2.CommandText = cmd2.CommandText + string.Format("AND (");
                    for (int i=0;i< obsids.Count();i++)
                      {
                        cmd2.CommandText = (i == obsids.Count - 1) ? cmd2.CommandText + string.Format("Beo_ID=" + Convert.ToInt32(obsids[i]) + ")") : cmd2.CommandText + string.Format("Beo_ID=" + Convert.ToInt32(obsids[i]) + " OR ");
                      }
                    count1 = Convert.ToInt32(cmd2.ExecuteScalar());
                    cmd2.CommandText = (CallArea(arearadio, areacheckedboxes) == null) ? cmd2.CommandText : cmd2.CommandText + string.Format(CallArea(arearadio, areacheckedboxes));
                    count2 = Convert.ToInt32(cmd2.ExecuteScalar());
                    cmd2.CommandText = (CallShift(shiftcheckedboxes) == null) ? cmd2.CommandText : cmd2.CommandText + string.Format(CallShift(shiftcheckedboxes));
                    count3 = Convert.ToInt32(cmd2.ExecuteScalar());
                    cmd2.CommandText = (CallFunction(funcradio, funccheckedboxes) == null) ? cmd2.CommandText : cmd2.CommandText + string.Format(CallFunction(funcradio, funccheckedboxes));
                    count4 = Convert.ToInt32(cmd2.ExecuteScalar());
                    cmd2.CommandText = (CallOrt(ortradio, ortcheckedboxes) == null) ? cmd2.CommandText : cmd2.CommandText + string.Format(CallOrt(ortradio, ortcheckedboxes));
                    count5 = Convert.ToInt32(cmd2.ExecuteScalar());
                    cmd2.CommandText = (CallActivity(actradio, actcheckedboxes) == null) ? cmd2.CommandText : cmd2.CommandText + string.Format(CallActivity(actradio, actcheckedboxes));
                    count6 = Convert.ToInt32(cmd2.ExecuteScalar());
                 }
                else if (obsradio == "Nur von" && obsids[0] != "null")
                {
                    cmd2.CommandText = cmd2.CommandText + string.Format("AND (");
                    for (int i = 0; i < obsids.Count(); i++)
                       {
                        cmd2.CommandText = (i == obsids.Count - 1) ? cmd2.CommandText + string.Format("NOT(Beo_ID=" + Convert.ToInt32(obsids[i]) + "))") : cmd2.CommandText + string.Format("NOT(Beo_ID=" + Convert.ToInt32(obsids[i]) + ") AND ");
                       }
                    count1 = Convert.ToInt32(cmd2.ExecuteScalar());
                    cmd2.CommandText = (CallArea(arearadio, areacheckedboxes) == null) ? cmd2.CommandText : cmd2.CommandText + string.Format(CallArea(arearadio, areacheckedboxes));
                    count2 = Convert.ToInt32(cmd2.ExecuteScalar());
                    cmd2.CommandText = (CallShift(shiftcheckedboxes) == null) ? cmd2.CommandText : cmd2.CommandText + string.Format(CallShift(shiftcheckedboxes));
                    count3 = Convert.ToInt32(cmd2.ExecuteScalar());
                    cmd2.CommandText = (CallFunction(funcradio, funccheckedboxes) == null) ? cmd2.CommandText : cmd2.CommandText + string.Format(CallFunction(funcradio, funccheckedboxes));
                    count4 = Convert.ToInt32(cmd2.ExecuteScalar());
                    cmd2.CommandText = (CallOrt(ortradio, ortcheckedboxes) == null) ? cmd2.CommandText : cmd2.CommandText + string.Format(CallOrt(ortradio, ortcheckedboxes));
                    count5 = Convert.ToInt32(cmd2.ExecuteScalar());
                    cmd2.CommandText = (CallActivity(actradio, actcheckedboxes) == null) ? cmd2.CommandText : cmd2.CommandText + string.Format(CallActivity(actradio, actcheckedboxes));
                    count6 = Convert.ToInt32(cmd2.ExecuteScalar());
                }               
            reportlist.Add((count1 == 0) ? "NA" : ((count2 * 100) / count1).ToString());
            reportlist.Add((count2 == 0) ? "NA" : ((count3 * 100) / count2).ToString());
            reportlist.Add((count3 == 0) ? "NA" : ((count4 * 100) / count3).ToString());
            reportlist.Add((count4 == 0) ? "NA" : ((count5 * 100) / count4).ToString());
            reportlist.Add((count5 == 0) ? "NA" : ((count6 * 100) / count5).ToString());
           //reportlist.Add(((count3 * 100) / Convert.ToInt32(reportlist[reportlist.Count-1])).ToString());
            reportlist.Add(count1.ToString());
            reportlist.Add(count2.ToString());
            reportlist.Add(count3.ToString());
            reportlist.Add(count4.ToString());
            reportlist.Add(count5.ToString());
            reportlist.Add(count6.ToString());
            con.Close();
            return reportlist;
        }
        catch (InvalidOperationException exception)
        {
            return null;
        }
    }
        
        [HttpGet]
        [Route("GetReportMachine")]//Return Report for Machine
        public List<String> GetReportmachine(string startdate, string enddate, string obsradio, string arearadio, string machradio, string stateradio,string obscheckedboxes, string areacheckedboxes, string machcheckedboxes, string statecheckedboxes,string shiftcheckedboxes, string ExceptDate)
        {
            List<string> reportlist = new List<string>();
            int count1 = 0, count2 = 0, count3 = 0, count4 = 0, count5 = 0;
            try
            {
                con = new SqlConnection(conString);
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Close();
                    con.Open();
                }
                else { con.Open(); }
                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = con;
                SqlCommand cmd3 = new SqlCommand();
                cmd3.Connection = con;
                var obsids = (obscheckedboxes == null) ? null : obscheckedboxes.Split(',').ToList();
                var ExceptDates = (ExceptDate == null) ? null : ExceptDate.Split(',').ToList();
                if (ExceptDate == null)
                {
                    cmd2.CommandText = string.Format("Select COUNT(*) from MMS_MaschineData where Datum >= '" + startdate + "' AND Datum <= '" + enddate + "'");
                }
                else
                {
                    cmd2.CommandText = string.Format("Select COUNT(*) from MMS_MaschineData where Datum >= '" + startdate + "' AND Datum <= '" + enddate + "'") + string.Format("AND (");
                    for (int i = 0; i < ExceptDates.Count(); i++)
                    {
                        cmd2.CommandText = (i == ExceptDates.Count - 1) ? cmd2.CommandText + string.Format("NOT(Datum='" + (ExceptDates[i]) + "'))") : cmd2.CommandText + string.Format("NOT(Datum='" + (ExceptDates[i]) + "') AND ");
                    }
                }
                if (obsradio == "Alle")
                {
                    count1 = Convert.ToInt32(cmd2.ExecuteScalar());
                    cmd2.CommandText = (CallArea(arearadio, areacheckedboxes) == null) ? cmd2.CommandText : cmd2.CommandText + string.Format(CallArea(arearadio, areacheckedboxes));
                    count2 = Convert.ToInt32(cmd2.ExecuteScalar());
                    cmd2.CommandText = (CallShift(shiftcheckedboxes) == null) ? cmd2.CommandText : cmd2.CommandText + string.Format(CallShift(shiftcheckedboxes));
                    count3 = Convert.ToInt32(cmd2.ExecuteScalar());
                    cmd2.CommandText = (CallMachine(machradio, machcheckedboxes) == null) ? cmd2.CommandText : cmd2.CommandText + string.Format(CallMachine(machradio, machcheckedboxes));
                    count4 = Convert.ToInt32(cmd2.ExecuteScalar());
                    cmd2.CommandText = (CallState(stateradio, statecheckedboxes) == null) ? cmd2.CommandText : cmd2.CommandText + string.Format(CallState(stateradio, statecheckedboxes));
                    count5 = Convert.ToInt32(cmd2.ExecuteScalar());
      
                }
                else if (obsradio == "Nur von" && obsids[0] != "null")
                {
                    cmd2.CommandText = cmd2.CommandText + string.Format("AND (");
                    for (int i = 0; i < obsids.Count(); i++)
                    {
                        cmd2.CommandText = (i == obsids.Count - 1) ? cmd2.CommandText + string.Format("Beo_ID=" + Convert.ToInt32(obsids[i]) + ")") : cmd2.CommandText + string.Format("Beo_ID=" + Convert.ToInt32(obsids[i]) + " OR ");
                    }
                    count1 = Convert.ToInt32(cmd2.ExecuteScalar());
                    cmd2.CommandText = (CallArea(arearadio, areacheckedboxes) == null) ? cmd2.CommandText : cmd2.CommandText + string.Format(CallArea(arearadio, areacheckedboxes));
                    count2 = Convert.ToInt32(cmd2.ExecuteScalar());
                    cmd2.CommandText = (CallShift(shiftcheckedboxes) == null) ? cmd2.CommandText : cmd2.CommandText + string.Format(CallShift(shiftcheckedboxes));
                    count3 = Convert.ToInt32(cmd2.ExecuteScalar());
                    cmd2.CommandText = (CallMachine(machradio, machcheckedboxes) == null) ? cmd2.CommandText : cmd2.CommandText + string.Format(CallMachine(machradio, machcheckedboxes));
                    count4 = Convert.ToInt32(cmd2.ExecuteScalar());
                    cmd2.CommandText = (CallState(stateradio, statecheckedboxes) == null) ? cmd2.CommandText : cmd2.CommandText + string.Format(CallState(stateradio, statecheckedboxes));
                    count5 = Convert.ToInt32(cmd2.ExecuteScalar());
                }
                else if (obsradio == "Außer von" && obsids[0] != "null")
                {
                    cmd2.CommandText = cmd2.CommandText + string.Format("AND (");
                    for (int i = 0; i < obsids.Count(); i++)
                    {
                        cmd2.CommandText = (i == obsids.Count - 1) ? cmd2.CommandText + string.Format("NOT(Beo_ID=" + Convert.ToInt32(obsids[i]) + "))") : cmd2.CommandText + string.Format("NOT(Beo_ID=" + Convert.ToInt32(obsids[i]) + ") AND ");
                    }
                    count1 = Convert.ToInt32(cmd2.ExecuteScalar());
                    cmd2.CommandText = (CallArea(arearadio, areacheckedboxes) == null) ? cmd2.CommandText : cmd2.CommandText + string.Format(CallArea(arearadio, areacheckedboxes));
                    count2 = Convert.ToInt32(cmd2.ExecuteScalar());
                    cmd2.CommandText = (CallShift(shiftcheckedboxes) == null) ? cmd2.CommandText : cmd2.CommandText + string.Format(CallShift(shiftcheckedboxes));
                    count3 = Convert.ToInt32(cmd2.ExecuteScalar());
                    cmd2.CommandText = (CallMachine(machradio, machcheckedboxes) == null) ? cmd2.CommandText : cmd2.CommandText + string.Format(CallMachine(machradio, machcheckedboxes));
                    count4 = Convert.ToInt32(cmd2.ExecuteScalar());
                    cmd2.CommandText = (CallState(stateradio, statecheckedboxes) == null) ? cmd2.CommandText : cmd2.CommandText + string.Format(CallState(stateradio, statecheckedboxes));
                    count5 = Convert.ToInt32(cmd2.ExecuteScalar());
                }
                reportlist.Add((count1 == 0) ? "NA" : ((count2 * 100) / count1).ToString());
                reportlist.Add((count2 == 0) ? "NA" : ((count3 * 100) / count2).ToString());
                reportlist.Add((count3 == 0) ? "NA" : ((count4 * 100) / count3).ToString());
                reportlist.Add((count4 == 0) ? "NA" : ((count5 * 100) / count4).ToString());
                reportlist.Add(count1.ToString());
                reportlist.Add(count2.ToString());
                reportlist.Add(count3.ToString());
                reportlist.Add(count4.ToString());
                reportlist.Add(count5.ToString());
                con.Close();
                return reportlist;
            }
            catch (InvalidOperationException exception)
            {
                return null;
            }
        }
        //Return the query for selected areas 
        public string CallArea(string arearadio, string areacheckedboxes)
        {
            string query = "";
            var areas = (areacheckedboxes == null) ? null : areacheckedboxes.Split(',').ToList();
            if (arearadio == "Alle")
            {
                query = null;
            }
            else if (arearadio == "Nur von" && areas[0] != "null")
            {
                query = string.Format("AND (");
                for (int i = 0; i < areas.Count(); i++)
                {
                    query = (i == areas.Count - 1) ? query + string.Format("Bereich='" + (areas[i]) + "')") : query + string.Format("Bereich='" + (areas[i]) + "' OR ");
                }     
            }
            else if (arearadio == "Außer von" && areas[0] != "null")
            {
                query = query + string.Format("AND (");
                for (int i = 0; i < areas.Count(); i++)
                {
                    query = (i == areas.Count - 1) ? query + string.Format("NOT (Bereich='" + (areas[i]) + "'))") : query + string.Format("NOT (Bereich='" + (areas[i]) + "') AND ");
                }
            }
            return query;
        }
        //Return the query for selected shifts 
        public string CallShift(string shiftcheckedboxes)
        {
            string query = "";
            var shifts = shiftcheckedboxes.Split(',').ToList();
            query = query + string.Format("AND (");//Shift-start
            for (int i = 0; i < shifts.Count(); i++)
            {
                query = (i == shifts.Count - 1) ? query + string.Format("Schicht='" + (shifts[i]) + "')") : query + string.Format("Schicht='" + (shifts[i]) + "' OR ");
            }
            return query;
        }
        //Return the query for selected functions 
        public string CallFunction(string funcradio, string funccheckedboxes)
        {
            string query = "";
            var functions = (funccheckedboxes == null) ? null : funccheckedboxes.Split(',').ToList();
            if (funcradio == "Alle")
            {
                query = null;
            }
            else if (funcradio == "Nur von" && functions[0] != "null")
            {
                query = string.Format("AND (");
                for (int i = 0; i < functions.Count(); i++)
                {
                    query = (i == functions.Count - 1) ? query + string.Format("Funktion='" + (functions[i]) + "')") : query + string.Format("Funktion='" + (functions[i]) + "' OR ");
                }
            }
            else if (funcradio == "Außer von" && functions[0] != "null")
            {
                query = query + string.Format("AND (");
                for (int i = 0; i < functions.Count(); i++)
                {
                    query = (i == functions.Count - 1) ? query + string.Format("NOT (Funktion='" + (functions[i]) + "'))") : query + string.Format("NOT (Funktion='" + (functions[i]) + "'') AND ");
                }
            }
            return query;
        }
        //Return the query for selected orts 
        public string CallOrt(string ortradio,string ortcheckedboxes)
        {
            string query="";
            var orts = (ortcheckedboxes == null) ? null : ortcheckedboxes.Split(',').ToList();
            if (ortradio == "Alle")
            {
                query = null;
            }
            else if (ortradio == "Nur von" &&  orts[0] != "null")
            {
                query= string.Format("AND (");
                for (int i = 0; i < orts.Count(); i++)
                {
                    query = (i == orts.Count - 1) ? query + string.Format("Funktion='" + (orts[i]) + "')") : query + string.Format("Funktion='" + (orts[i]) + "' OR ");
                }
            }
            else if (ortradio == "Außer von" && orts[0] != "null")
            {
                query = query + string.Format("AND (");
                for (int i = 0; i < orts.Count(); i++)
                {
                    query = (i == orts.Count - 1) ? query + string.Format("NOT (Funktion='" + (orts[i]) + "'))") : query + string.Format("NOT (Funktion='" + (orts[i]) + "') AND ");
                }
            }
          return query;
        }
        //Return the query for selected Activities 
        public string CallActivity(string actradio, string actcheckedboxes)
        {
            string query = "";
            var activities = (actcheckedboxes == null) ? null : actcheckedboxes.Split(',').ToList();
            if (actradio == "Alle")
            {
                query = null;
            }
            else if (actradio == "Nur von" && activities[0] != "null")
            {
                query = string.Format("AND (");
                for (int i = 0; i < activities.Count(); i++)
                {
                    query = (i == activities.Count - 1) ? query + string.Format("Taegtigkeit='" + (activities[i]) + "')") : query + string.Format("Taegtigkeit='" + (activities[i]) + "' OR ");
                }
            }
            else if (actradio == "Außer von" && activities[0] != "null")
            {
                query = query + string.Format("AND (");
                for (int i = 0; i < activities.Count(); i++)
                {
                    query = (i == activities.Count - 1) ? query + string.Format("NOT (Taegtigkeit='" + (activities[i]) + "'))") : query + string.Format("NOT (Taegtigkeit='" + (activities[i]) + "') AND ");
                }
            }
            return query;
        }
        //Return the query for selected Machines 
        public string CallMachine(string machradio, string machcheckedboxes)
        {
            string query = "";
            var machines = (machcheckedboxes == null) ? null : machcheckedboxes.Split(',').ToList();
            if (machradio == "Alle")
            {
                query = null;
            }
            else if (machradio == "Nur von" && machines[0] != "null")
            {
                query = string.Format("AND (");
                for (int i = 0; i < machines.Count(); i++)
                {
                    query = (i == machines.Count - 1) ? query + string.Format("Maschine='" + (machines[i]) + "')") : query + string.Format("Maschine='" + (machines[i]) + "' OR ");
                }
            }
            else if (machradio == "Außer von" && machines[0] != "null")
            {
                query = query + string.Format("AND (");
                for (int i = 0; i < machines.Count(); i++)
                {
                    query = (i == machines.Count - 1) ? query + string.Format("NOT (Maschine='" + (machines[i]) + "'))") : query + string.Format("NOT (Maschine='" + (machines[i]) + "') AND ");
                }
            }
            return query;
        }
        //Return the query for selected States of machines 
        public string CallState(string stateradio, string statecheckedboxes)
        {
            string query = "";
            var states = (statecheckedboxes == null) ? null : statecheckedboxes.Split(',').ToList();
            if (stateradio == "Alle")
            {
                query = null;
            }
            else if (stateradio == "Nur von" && states[0] != "null")
            {
                query = string.Format("AND (");
                for (int i = 0; i < states.Count(); i++)
                {
                    query = (i == states.Count - 1) ? query + string.Format("Zustand='" + (states[i]) + "')") : query + string.Format("Zustand='" + (states[i]) + "' OR ");
                }
            }
            else if (stateradio == "Außer von" && states[0] != "null")
            {
                query = query + string.Format("AND (");
                for (int i = 0; i < states.Count(); i++)
                {
                    query = (i == states.Count - 1) ? query + string.Format("NOT (Zustand='" + (states[i]) + "'))") : query + string.Format("NOT (Zustand='" + (states[i]) + "') AND ");
                }
            }
            return query;
        }

    }
}
