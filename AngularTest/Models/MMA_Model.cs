using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
//using MySql.Data.MySqlClient;
using System.Configuration;


namespace AngularTest.Models
{

    public class MMA_Model
    {
        private static string conString = ConfigurationManager.ConnectionStrings["MMSConnection"].ConnectionString;
        private static SqlConnection con;
        //public string selectedid;
        public List<string>[] getList(string select)
        {
            //List<string> observerlist = new List<string>();
            //List<string> arealist = new List<string>();
            List<string>[] observerlist = new List<string>[2];
            observerlist[0] = new List<string>();
            observerlist[1] = new List<string>();
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
                SqlCommand cmd1 = new SqlCommand();
                SqlCommand cmd2= new SqlCommand();
                SqlDataReader reader1, reader2;
                if (select == "1")
                {
                cmd1.Connection = con;
                cmd1.CommandText = string.Format("select ID,Vorname from MMS_Beobachter");
                reader1 = cmd1.ExecuteReader();
                do
                {
                    while (reader1.Read())
                    {
                            string id = Convert.ToString(reader1.GetInt32(0));
                            observerlist[0].Add(id);
                            observerlist[1].Add(reader1.GetString(1));      
                    }
                } while (reader1.NextResult());
                reader1.Close();
                    return observerlist;
                }
                else if (select == "2")
                {
                cmd2.Connection = con;
                cmd2.CommandText = string.Format("select ID,Bereich from MMS_Bereich");
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
                else
                {
                    return null;
                }
               
            }
            //catch (Exception)
            //{
            //    throw;
            //}
            catch (InvalidOperationException exception)
            {
                // Error logging, post processing etc.
                return null;
            }

        }

        public List<string> getTimelist(string area)
        {
            List<string> timeslot = new List<string>();
            string tour = "";
            DateTime Currenttime = DateTime.Now;
            int compareValue1 = Currenttime.CompareTo(DateTime.Parse("06:30:00"));
            int compareValue2 = Currenttime.CompareTo(DateTime.Parse("14:30:00"));
            int compareValue3 = Currenttime.CompareTo(DateTime.Parse("23:00:00"));
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
            try
            {
                con = new SqlConnection(conString);
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
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

        public List<string>[] getEmployeelist(string Place)
        {
            List<string>[] employee = new List<string>[2];
            employee[0] = new List<string>();
            employee[1] = new List<string>();
            try
            {
                con = new SqlConnection(conString);
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = string.Format("select Name , Vorname , Funktion , MMS_Mitarbeiter.ID from MMS_Mitarbeiterfunktion,MMS_Mitarbeiter where MMS_Mitarbeiter.Funktion_ID = MMS_Mitarbeiterfunktion.ID and MMS_Mitarbeiter.Bereich ='" + Place + "' ORDER BY Name ASC");
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

        public List<string>[] getSelectEmployeelist(string Place, string Observerid)
        {
            List<string>[] selectemployee = new List<string>[2];
            selectemployee[0] = new List<string>();
            selectemployee[1] = new List<string>();
            try
            {      
               // var ids = Id.Split(',').Select(Int32.Parse).ToList();
                con = new SqlConnection(conString);
                con.Open();
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
                } while (reader.NextResult()) ;
                reader.Close();
                con.Close();
            }
            catch (InvalidOperationException exception)
            {
                // Error logging, post processing etc.
                return null;
            }
            return selectemployee;
        }
        public List<string> getEmployeeActivitieslist()
        {
            List<string> employeeactivities = new List<string>();
            try
            {
                con = new SqlConnection(conString);
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = string.Format("Select Taetigkeit from MMS_Taetigkeit T where T.ID NOT IN  (select Untertaetigkeit from MMS_Obertaetigkeit_Untertaetigkeit)");
                SqlDataReader reader = cmd.ExecuteReader();
                do
                {
                    while (reader.Read())
                    {
                        employeeactivities.Add(reader.GetString(0));
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
            return employeeactivities;
        }

        public List<string> getEmployeeSubActivitieslist(string Activity)
        {
            List<string> employeesubactivities = new List<string>();
            try
            {
                con = new SqlConnection(conString);
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = string.Format("Select T.Taetigkeit from MMS_Taetigkeit T , MMS_Obertaetigkeit_Untertaetigkeit OU where T.ID=OU.Untertaetigkeit and OU.Obertaetigkeit= (Select ID from MMS_Taetigkeit  where MMS_Taetigkeit.Taetigkeit='" + Activity + "')");
                SqlDataReader reader = cmd.ExecuteReader();
                do
                {
                    while (reader.Read())
                    {
                        employeesubactivities.Add(reader.GetString(0));
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

        public List<string> getMachineLinelist(string place,string type)
        {
            List<string> machinelines = new List<string>();
            try
            {
                con = new SqlConnection(conString);
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                if (type=="emp") {
                    cmd.CommandText = string.Format("select O.Bezeichnung from MMS_Ort O, MMS_Oberort_Unterort OU where O.ID=OU.Oberort and O.Bereich_ID=(select ID from MMS_Bereich where Bereich='" + place + "')  group by O.Bezeichnung");
                }
                else
                {
                    cmd.CommandText = string.Format("select O.Bezeichnung from MMS_Ort O, MMS_Oberort_Unterort OU where O.ID=OU.Oberort and O.Bereich_ID=(select ID from MMS_Bereich where Bereich='" + place + "') and O.Typ='Hybrid' group by O.Bezeichnung");
                }
                SqlDataReader reader = cmd.ExecuteReader();
                do
                {
                    while (reader.Read())
                    {
                        machinelines.Add(reader.GetString(0));
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
            return machinelines;
        }

        public List<string> getMachinelist(string machineline,string param)
        {
            List<string> machineslist = new List<string>();
            try
            {
                con = new SqlConnection(conString);
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                if (param=="emp")
                {
                    cmd.CommandText = string.Format("Select O.Bezeichnung from MMS_Ort O , MMS_Oberort_Unterort OU where O.ID=OU.Unterort and OU.Oberort= (Select ID from MMS_Ort  where MMS_Ort.Bezeichnung='" + machineline + "')");
                }
                else
                {
                    cmd.CommandText = string.Format("Select O.Bezeichnung from MMS_Ort O , MMS_Oberort_Unterort OU where O.ID=OU.Unterort and  O.Typ='Hybrid' and OU.Oberort= (Select ID from MMS_Ort  where MMS_Ort.Bezeichnung='" + machineline + "')");
                } 
                SqlDataReader reader = cmd.ExecuteReader();
                do
                {
                    while (reader.Read())
                    {
                        machineslist.Add(reader.GetString(0));
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
            return machineslist;
        }

        public List<string> getMachineStatelist()
        {
            List<string> machinestate = new List<string>();
            try
            {
                con = new SqlConnection(conString);
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = string.Format("select Zustand from MMS_Maschinenzustand");
                SqlDataReader reader = cmd.ExecuteReader();
                do
                {
                    while (reader.Read())
                    {
                        machinestate.Add(reader.GetString(0));
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
            return machinestate;
        }

        public void Inserttime(string time, string place, string tour)
        {

            DateTime startTime = DateTime.Parse("06:30:00");
            DateTime endTime = DateTime.Parse("14:30:00");

            if (tour == "2")
            {
                startTime = DateTime.Parse("14:30:00");
                endTime = DateTime.Parse("23:00:00");
            }
            else if (tour == "3")
            {
                startTime = DateTime.Parse("23:00:00");
                endTime = DateTime.Parse("06:30:00");
            }
            try
            {
                con = new SqlConnection(conString);
                con.Open();
                SqlCommand cmd1 = new SqlCommand();
                cmd1.Connection = con;
                cmd1.CommandText = string.Format("select Time from MMS_Time where Bereich= '" + place + "' and Tour='" + tour + "'");
                SqlDataReader reader1 = cmd1.ExecuteReader();
                if (reader1.FieldCount > 0)
                {
                    reader1.Close();
                    SqlCommand cmd2 = new SqlCommand();
                    cmd2.Connection = con;
                    cmd2.CommandText = string.Format("Delete from MMS_Time where Bereich= '" + place + "' and Tour='" + tour + "'");
                    cmd2.ExecuteNonQuery();
                }
                else
                {
                    reader1.Close();
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
        }

        public void InsertEmployeelist(string observerid, string employeeids)
        {

            try
            {
                var ids = employeeids.Split(',').ToList();
                con = new SqlConnection(conString);
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Close();
                    con.Open();
                }
                else { con.Open(); }
                SqlCommand cmd1 = new SqlCommand();
                cmd1.Connection = con;
                cmd1.CommandText = string.Format("select Beo_ID from MMS_BeoMitId where Beo_ID=" + Convert.ToInt32(observerid) + "");
                SqlDataReader reader1 = cmd1.ExecuteReader();
                if (reader1.FieldCount > 0)
                {
                    reader1.Close();
                    SqlCommand cmd2 = new SqlCommand();
                    cmd2.Connection = con;
                    cmd2.CommandText = string.Format("Delete from MMS_BeoMitId where Beo_ID=" + Convert.ToInt32(observerid) + "");
                    cmd2.ExecuteNonQuery();
                }
                else
                {
                    reader1.Close();
                }
                for (int i = 0; i < ids.Count(); i++)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = string.Format("Insert into MMS_BeoMitId values (" + Convert.ToInt32(observerid) + "," + Convert.ToInt32(ids[i]) + ")");
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
            catch (InvalidOperationException exception)
            {
                // Error logging, post processing etc.
              
            }
        }


        public void Insertemployeedata(string data)
        {
            try
            {
                var empdata = data.Split(',').ToList();
                con = new SqlConnection(conString);
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Close();
                    con.Open();
                }
                else { con.Open(); }
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = string.Format("Insert into MMS_MitarbeiterData values (" + Convert.ToInt32(empdata[0]) + ",'" + empdata[1] + "','" + empdata[2] + "','" + empdata[3] + "','" + empdata[8] + "','" + empdata[5] + "','" + empdata[6] + "')");
                    cmd.ExecuteNonQuery();  
                    con.Close();
            }
            catch (InvalidOperationException exception)
            {
                // Error logging, post processing etc.
               
            }
        }

        public void Insertmachinedata(string data)
        {
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
                cmd.CommandText = string.Format("Insert into MMS_MaschineData values (" + Convert.ToInt32(machinedata[0]) + ",'" + machinedata[1] + "','" + machinedata[2] + "','" + machinedata[3] + "','" + machinedata[4] + "','" + machinedata[5] + "')");
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (InvalidOperationException exception)
            {
                // Error logging, post processing etc.
            }
        }


        public List<string> getLangdata(string lang)
        {
            List<string> langdata = new List<string>();
            try
            {
                con = new SqlConnection(conString);
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = string.Format("SELECT Textdata  FROM MMS_Multilanguage where lang='"+lang+"'");
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
            }
            catch (InvalidOperationException exception)
            {
                // Error logging, post processing etc.
               
            }
            return langdata;
        }
    }
}