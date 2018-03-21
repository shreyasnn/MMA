// This JS file is srevice for all the JS files 
(function (app) {
   //Here the variables are decleared for routing path to Templete API Controller 
    var route = 'api/templete';
    var route_obslist = '/GetObserverList';
    var route_arealist = '/GetAreaList';
    var route_timelist = '/GetTimeList';
    var route_employeelist = '/GetEmployeeList';
    var route_insertemployeelist = '/InsertEmployeeList';
    var route_selectemployeelist = '/GetSelectEmployeeList'; 
    var route_absentemployeelist = '/GetAbsentEmployeeList';
    var route_employeeactivitieslist = '/GetEmployeeActivitiesList';
    var route_employeesubactivitieslist = '/GetEmployeeSubActivitiesList';
    var route_machineline = '/GetMachineLineList'; 
    var route_machine = '/GetMachineList';
    var route_machinestate = '/GetMachineStateList';
    var route_timeslot = '/InsertTimeSlotList';
    var route_InsertMitarbeiterdata = '/InsertMitarbeiterData';
    var route_InsertMaschinedata = '/InsertMaschineData';
    var route_GetlangData = '/GetlangData';
    var route_insertobserverdata = '/InsertObserverData'; var route_editobserverdata = '/EditObserverData';
    var route_insertareadata = '/InsertAreaData'; var route_editareadata = '/EditAreaData';
    var route_insertemployeedata = '/InsertEmployeeData'; var route_editemployeedata = '/EditEmployeeData';
    var route_insertactivitydata = '/InsertActivityData'; var route_editactivitydata = '/EditActivityData'; var route_editsubactivitydata = '/EditSubActivityData';
    var route_insertmachinedata = '/InsertMachineData'; var route_editmachinedata = '/EditMachineData'; var route_editmachinelinedata = '/EditMachineLineData';
    var route_insertmachinestatedata = '/InsertMachineStateData'; var route_editmachinestatedata = '/EditMachineStateData';
    var route_employeefunction = '/GetEmployeeFunction';
    var route_getreport = '/GetReport';
    var route_getreportmachine = '/GetReportMachine';
    var Observername = ''; var Observerid = '';
    var Areaname = null; var SelectedEmployee = '';
    var parameter = ''; var Employeeid = null; var Machineid = '';
    var shift = '';
   
    app.HeroesService = ng.core
    .Class({
        constructor: [ng.http.Http, ng.md2.Md2Toast, function HeroesService(http, Md2Toast) {
            this.http = http;
            this.Md2Toast = Md2Toast;
            //HTTP Get request to function to get Observer list from API controller
            this.getObserver = function () {
                return this.http.get(route + route_obslist).map(response => response.json()).catch(error => error.json());
            }
            //HTTP Get request to function to get Area list from API controller
            this.getArea = function () {
                return this.http.get(route + route_arealist).map(response => response.json()).catch(error => error.json());
            }
            //HTTP Get request to function to get Time list from API controller
            this.getTime = function (area) {
                return this.http.get(route + route_timelist+'?area='+area).map(response => response.json()).catch(error => error.json());
            }
            //HTTP Get request to function to get Employee list from API controller
            this.getEmployee = function (area) {
                return this.http.get(route + route_employeelist + '?area=' + area).map(response => response.json()).catch(error => error.json());
            }
            //HTTP Get request to function to get Employee-Functions list from API controller
            this.getEmployeefunctions = function () {
                return this.http.get(route + route_employeefunction).map(response => response.json()).catch(error => error.json());
            }
            //HTTP Get request to function to insert employee list from API controller
            this.insertemployeelist = function (observerid,employeeidlist,action) {
                return this.http.get(route + route_insertemployeelist + '?observerid=' + observerid + '&employeeidlist=' + employeeidlist+'&action=' + action).map(response => response.json()).catch(error => error.json());;
            }
            //HTTP Get request to function to get Selected Employee list from API controller
            this.getSelectEmployee = function (area, Obsid) {
                return this.http.get(route + route_selectemployeelist + '?area=' + area + '&Observerid=' + Obsid).map(response => response.json()).catch(error => error.json());
            }//
            //HTTP Get request to function to get  Absent Employee list from API controller
            this.getAbsentEmployee = function (date,area,shift) {
                return this.http.get(route + route_absentemployeelist + '?date=' + date + '&area=' + area + '&shift=' + shift).map(response => response.json()).catch(error => error.json());
            }
            //HTTP Get request to function to get  Employee Activities list from API controller
            this.getEmployeeActivities = function () {
                return this.http.get(route + route_employeeactivitieslist).map(response => response.json()).catch(error => error.json());
            }
            //HTTP Get request to function to get Employee subactivities list from API controller
            this.getEmployeeSubActivities = function (activity) {
                return this.http.get(route + route_employeesubactivitieslist + '?activity=' + activity).map(response => response.json()).catch(error => error.json());
            }
            //This function return the list of all checked boxes in the form of array
            this.getCheckedBoxes= function (chkboxclassName) {
                var checkboxesChecked = [];
                var inputElements = document.getElementsByClassName(chkboxclassName);
                for (var i = 0; inputElements[i]; ++i) {
                    if (inputElements[i].checked) {
                        checkboxesChecked.push(inputElements[i].value);
                    }
                }
                return checkboxesChecked.length > 0 ? checkboxesChecked : null;
            }
            //HTTP Get request to function to get Machine line list from API controller
            this.getMachinelines = function (area,type) {
                return this.http.get(route + route_machineline + '?place=' + area + '&type=' + type).map(response => response.json()).catch(error => error.json());
            }
            //HTTP Get request to function to get Machines list from API controller
            this.getMachines = function (machineline,param) {
                return this.http.get(route + route_machine + '?machineline=' + machineline + '&param=' + param).map(response => response.json()).catch(error => error.json());
            }
            //HTTP Get request to function to get Machine-states list from API controller
            this.getMachinestates = function () {
                return this.http.get(route + route_machinestate).map(response => response.json()).catch(error => error.json());
            }
            //HTTP Get request to function to insert timeslot from API controller
            this.inserttimeslot = function (time, shift, ort, starttime, endtime) {
                return this.http.get(route + route_timeslot + '?time=' + time + '&place=' + ort + '&tour=' + shift + '&starttime=' + starttime + '&endtime=' + endtime).map(response => response.json()).catch(error => error.json());
            }
            //HTTP Get request to function to insert observed employee data from API controller
            this.insertemplyeedata = function (empdata) {
                return this.http.get(route + route_InsertMitarbeiterdata +'?data='+empdata).map(response => response.json()).catch(error => error.json());
            }
            //HTTP Get request to function to insert observed machine data from API controller
            this.insertmachinedata = function (machinedata) {
                return this.http.get(route + route_InsertMaschinedata + '?data=' + machinedata).map(response => response.json()).catch(error => error.json());
            }
            //HTTP Get request to function to get language data list from API controller
            this.getlanguagedata = function (lang) {
                return this.http.get(route + route_GetlangData + '?lang=' + lang).map(response => response.json()).catch(error => error.json());
            }
            //HTTP Get request to function to insert observer data from API controller
            this.insertobserverdata = function (name, vorname,action) {
                return this.http.get(route + route_insertobserverdata + '?name=' + name+'&vorname='+vorname+'&action='+action).map(response => response.json()).catch(error => error.json());
            }
            //HTTP Get request to function to update or delete observer data from API controller
            this.editobserverdata = function (id, name, vorname,action) {
                return this.http.get(route + route_editobserverdata + '?id=' + id + '&name=' + name + '&vorname=' + vorname + '&action=' + action).map(response => response.json()).catch(error => error.json());
            }
            //HTTP Get request to function to insert area data from API controller
            this.insertareadata = function (area,action) {
                return this.http.get(route + route_insertareadata + '?area=' + area + '&action=' + action).map(response => response.json()).catch(error => error.json());
            }//
            this.editareadata = function (id,area, action) {
                return this.http.get(route + route_editareadata + '?id=' + id + '&area=' + area + '&action=' + action).map(response => response.json()).catch(error => error.json());
            }
            //HTTP Get request to function to insert employee data from API controller
            this.insertemployeedata = function (name, vorname, empfunction, area, obsname, action) {
                return this.http.get(route + route_insertemployeedata + '?name=' + name + '&vorname=' + vorname + '&empfunction=' + empfunction + '&area=' + area + '&obsname=' + obsname + '&action=' + action).map(response => response.json()).catch(error => error.json());
            }
            //HTTP Get request to function to edit employee data from API controller
            this.editemployeedata = function (id, name, vorname, empfunction, area, action) {
                return this.http.get(route + route_editemployeedata + '?id='+id+'&name=' + name + '&vorname=' + vorname + '&empfunction=' + empfunction + '&area=' + area + '&action=' + action).map(response => response.json()).catch(error => error.json());
            }
            //HTTP Get request to function to insert activity data from API controller
            this.insertactivitydata = function (activity,downactivity,action) {
                return this.http.get(route + route_insertactivitydata + '?activity=' + activity + '&downactivity=' + downactivity+'&action=' + action).map(response => response.json()).catch(error => error.json());
            }
            //HTTP Get request to function to edit activity data from API controller
            this.editactivitydata = function (id,activity,action) {
                return this.http.get(route + route_editactivitydata + '?id='+id+'&activity=' + activity + '&action=' + action).map(response => response.json()).catch(error => error.json());
            }
            //HTTP Get request to function to edit sub-activity data from API controller
            this.editsubactivitydata = function (id, activity, action) {
                return this.http.get(route + route_editsubactivitydata + '?id=' + id + '&activity=' + activity + '&action=' + action).map(response => response.json()).catch(error => error.json());
            }
            //HTTP Get request to function to insert machine data from API controller
            this.insertmachine = function (place,machine,downmachine,type,action) {
                return this.http.get(route + route_insertmachinedata + '?place=' + place + '&machine=' + machine + '&downmachine=' + downmachine + '&type=' + type + '&action=' + action).map(response => response.json()).catch(error => error.json());
            }
            //HTTP Get request to function to edit machine line data from API controller
            this.editmachinelinedata = function (id,place, machineline,action) {
                return this.http.get(route + route_editmachinelinedata + '?id='+id+'&place=' + place + '&machineline=' + machineline + '&action=' + action).map(response => response.json()).catch(error => error.json());
            }//
            this.editmachinedata = function (id, place, machineline, action) {
                return this.http.get(route + route_editmachinedata + '?id=' + id + '&place=' + place + '&machineline=' + machineline + '&action=' + action).map(response => response.json()).catch(error => error.json());
            }
            //HTTP Get request to function to insert machine state data from API controller
            this.insertmachinestatedata = function (state,action) {
                return this.http.get(route + route_insertmachinestatedata + '?state=' + state + '&action=' + action).map(response => response.json()).catch(error => error.json());
            }
            //HTTP Get request to function to edit machine state data from API controller
            this.editmachinestatedata = function (id,statename, action) {
                return this.http.get(route + route_editmachinestatedata + '?id=' + id + '&statename=' + statename + '&action=' + action).map(response => response.json()).catch(error => error.json());
            }
            //HTTP Get request to function to get report data for employee from API controller
            this.getreport = function (startdate, enddate, obsradio, arearadio, actradio, funcradio, ortradio, obscheckedboxes, areacheckedboxes, actcheckedboxes, funccheckedboxes, ortcheckedboxes, shiftcheckedboxes,ExceptDate) {
                return this.http.get(route + route_getreport + '?startdate=' + startdate + '&enddate=' + enddate + '&obsradio=' + obsradio + '&arearadio=' + arearadio + '&actradio=' + actradio + '&funcradio=' + funcradio + '&ortradio=' + ortradio +
               '&obscheckedboxes=' + obscheckedboxes + '&areacheckedboxes=' + areacheckedboxes + '&actcheckedboxes=' + actcheckedboxes + '&funccheckedboxes=' + funccheckedboxes + '&ortcheckedboxes=' + ortcheckedboxes + '&shiftcheckedboxes=' + shiftcheckedboxes + '&ExceptDate=' + ExceptDate).map(response => response.json()).catch(error => error.json());
            }
            //HTTP Get request to function to get report data for machine from API controller
            this.getreportmachine = function (startdate, enddate, obsradio, arearadio, machradio, stateradio, obscheckedboxes, areacheckedboxes, machcheckedboxes, statecheckedboxes, shiftcheckedboxes, ExceptDate) {
                return this.http.get(route + route_getreportmachine + '?startdate=' + startdate + '&enddate=' + enddate + '&obsradio=' + obsradio + '&arearadio=' + arearadio + '&machradio=' + machradio + '&stateradio=' + stateradio  +
               '&obscheckedboxes=' + obscheckedboxes + '&areacheckedboxes=' + areacheckedboxes + '&machcheckedboxes=' + machcheckedboxes + '&statecheckedboxes=' + statecheckedboxes + '&shiftcheckedboxes=' + shiftcheckedboxes + '&ExceptDate=' + ExceptDate).map(response => response.json()).catch(error => error.json());
            }
            //These are the variables decleration for using it globally
            this.lang = 'DE';//Decleare language globally 
            this.Languagelist = [];// This list is used to change data onclick of language change
            this.EmployeeData = [];// This list is used to store the current selected employee data
            this.EmployeeDatalast = [];// This list is used to store the last selected employee data
            this.lastdata = [];// This list is used to store temporary data
            this.CompletedEmployee = [];// This list stored the all completed employee
            this.machineline = null;// Used to get currently selected machine line
            this.comletedmachinelines = [];//This list store the completed machinelines  
            this.MachineData = [];// his list is used to store the current selected machine data
            this.MachineDatalast = [];// This list is used to store the last selected machine data
            this.CompletedMachine = [];//This list store the completed machines in the line 
            this.Allmachine = '';
            this.Allemployee = '';
            this.reportlist = [];//This list used to store the data selected for the report
            this.selectedreport = [];// This list used to store the analysis report data
            this.Completedtimeslot = [];//This list store the completed time slots 
          
        }]
    });
})(window.app || (window.app = {}));