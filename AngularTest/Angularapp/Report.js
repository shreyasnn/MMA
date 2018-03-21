//Calling the method from service.js

(function (app) {
    app.ReportComponent = ng.core
	.Component({
	    selector: 'my-report',
	    templateUrl: "Home/Report"
	})
	.Class({
	    constructor: [app.HeroesService, ng.router.ActivatedRoute, ng.router.Router, function ReportComponent(heroesService, route, router) {
	        this.router = router;
	        this.edited = true;
	        this.heroesService = heroesService;
	        this.route = route;
	        this.reportlist = [];
	        this.selection = this.heroesService.Languagelist[47];
	        this.dateselect = this.heroesService.Languagelist[48];
	        this.allselect = this.heroesService.Languagelist[49];
	        this.onlyselect = this.heroesService.Languagelist[50];
	        this.exceptselect = this.heroesService.Languagelist[51];
	        this.employeeselect = this.heroesService.Languagelist[3];
	        this.machinestation = this.heroesService.Languagelist[38];
	        this.machinestate = this.heroesService.Languagelist[39];
	        this.observername = this.heroesService.Languagelist[1];
	        this.placename = this.heroesService.Languagelist[2];
	        this.shiftname = this.heroesService.Languagelist[42];
	        this.earlyshift = this.heroesService.Languagelist[44];
	        this.lateshift = this.heroesService.Languagelist[45];
	        this.nightshift = this.heroesService.Languagelist[46];
	        this.send = this.heroesService.Languagelist[28];
	        this.empfunction = this.heroesService.Languagelist[32];
	        this.empactivity = this.heroesService.Languagelist[35];
	        this.warning = this.heroesService.Languagelist[24];
	        this.cancel = this.heroesService.Languagelist[20];
	        this.fillall = this.heroesService.Languagelist[43];
	        this.process = this.heroesService.Languagelist[52];
	        this.observations = this.heroesService.Languagelist[53];
	        this.proportion = this.heroesService.Languagelist[54];
	        this.total = this.heroesService.Languagelist[55];
	        this.report = this.heroesService.Languagelist[56];
	        this.printreport = this.heroesService.Languagelist[60];
	    }]
	});

    
    app.ReportComponent.prototype.Assignobser = function (obserlist) {
        var obserlist1 = [{ id: '', name: '' }];
        for (var i = 0; i < obserlist[0].length; i++) {
            obserlist1[i] = [{ id: String(obserlist[0][i]), name: String(obserlist[1][i]) }];
        }
        this.obserlist = obserlist1;
        this.heroesService.getArea().subscribe(response => this.Assignarea(response), error => console.log(error));
    };
    app.ReportComponent.prototype.Assignarea = function (arealist) {
        var arealist1 = [{ id: '', name: '' }];
        for (var i = 0; i < arealist[0].length; i++) {
            arealist1[i] = [{ id: String(arealist[0][i]), name: String(arealist[1][i]) }];
        }
        this.arealist = arealist1;
        this.heroesService.getEmployeeSubActivities("ReportActivities").subscribe(response => this.Assignemployeeactivity(response), error => console.log(error));
    };
    app.ReportComponent.prototype.Assignemployeeactivity = function (activities) {
         var activities1 = [{ id: '', name: '' }];
        for (var i = 0; i < activities[0].length; i++) {
            activities1[i] = [{ id: String(activities[0][i]), name: String(activities[1][i]) }];
        }
        this.activities = activities1;
        this.heroesService.getMachinestates().subscribe(response=> this.AssignState(response), error => console.log(error));
    };
    app.ReportComponent.prototype.Assignfunction = function (Functionlist) {
        this.Functionlist = Functionlist;
        this.heroesService.getMachines('','emp').subscribe(response=> this.Assignort(response), error => console.log(error));
    };
    app.ReportComponent.prototype.Assignort = function (ortlist) {
        var ortlist1 = [{ id: '', name: '' }];
        for (var i = 0; i < ortlist[0].length; i++) {
            ortlist1[i] = [{ id: String(ortlist[0][i]), name: String(ortlist[1][i]) }];
        }
        this.ortlist = ortlist1;
        this.heroesService.getMachines('', 'machine').subscribe(response=> this.Assignmachine(response), error => console.log(error));
    };
    app.ReportComponent.prototype.Assignmachine = function (Machinelist) {
        var machineslist1 = [{ id: '', name: '' }];
        for (var i = 0; i < Machinelist[0].length; i++) {
            machineslist1[i] = [{ id: String(Machinelist[0][i]), name: String(Machinelist[1][i]) }];
        }
        this.Machinelist = machineslist1;
    };
    app.ReportComponent.prototype.AssignState = function (MachineStatelist) {
        var states1 = [{ id: '', name: '' }];
        for (var i = 0; i < MachineStatelist[0].length; i++) {
            states1[i] = [{ id: String(MachineStatelist[0][i]), name: String(MachineStatelist[1][i]) }];
        }
        this.MachineStatelist = states1;
        this.heroesService.getEmployeefunctions().subscribe(response => this.Assignfunction(response), error => console.log(error));
    };
    app.ReportComponent.prototype.ngOnInit = function () {     
        this.heroesService.getObserver().subscribe(response => this.Assignobser(response), error => console.log(error));       
        //this.heroesService.getEmployeelist().subscribe(response => this.Assignfunction(response), error => console.log(error));
        //this.heroesService.getMachinelist().subscribe(response=> this.Assignmachine(response), error => console.log(error));   
    }; 
    
    app.ReportComponent.prototype.gotoselect1 = function () {
        this.edited = true;
    };
    app.ReportComponent.prototype.gotoselect2 = function () {
        this.edited = false;     
    };
   
    app.ReportComponent.prototype.goToViewReport = function (startdate, enddate, obsradio, arearadio, actradio, funcradio, ortradio, ExceptDate) {
        this.reportparam = "employee";
        var obscheckedboxes = (obsradio == "Alle") ? "" : this.heroesService.getCheckedBoxes("obscheck");
        var areacheckedboxes = (arearadio == "Alle") ? "" : this.heroesService.getCheckedBoxes("areacheck");
        var actcheckedboxes = (actradio == "Alle") ? "" : this.heroesService.getCheckedBoxes("actcheck");
        var funccheckedboxes = (funcradio == "Alle") ? "" : this.heroesService.getCheckedBoxes("funccheck");
        var ortcheckedboxes = (ortradio == "Alle") ? "" : this.heroesService.getCheckedBoxes("ortcheck");
        var shiftcheckedboxes = this.heroesService.getCheckedBoxes("shiftcheck");
        //Assign all selected parameters 
        this.activity = actradio + "-" + actcheckedboxes;
        this.ort = ortradio + "-" + ortcheckedboxes;
        this.function = funcradio + "-" + funccheckedboxes; 
        this.shift = shiftcheckedboxes;
        this.area = arearadio + "-" + areacheckedboxes;
        this.observer = obsradio + "-" + obscheckedboxes;
        this.date = startdate + " bis " + enddate;
        this.heroesService.getreport(startdate, enddate, obsradio, arearadio, actradio, funcradio, ortradio, obscheckedboxes, areacheckedboxes, actcheckedboxes, funccheckedboxes, ortcheckedboxes, shiftcheckedboxes, ExceptDate)
        .subscribe(response => this.Assignreport(response), error => console.log(error));     
        
    }; 
    app.ReportComponent.prototype.goToViewReportMachine = function (startdate, enddate, obsradio, arearadio, machradio, stateradio, ExceptDate) {
        this.reportparam = "machine";
        var obscheckedboxes = (obsradio == "Alle") ? "" : this.heroesService.getCheckedBoxes("obscheck");
        var areacheckedboxes = (arearadio == "Alle") ? "" : this.heroesService.getCheckedBoxes("areacheck");
        var machcheckedboxes = (machradio == "Alle") ? "" : this.heroesService.getCheckedBoxes("machinecheck");
        var statecheckedboxes = (stateradio == "Alle") ? "" : this.heroesService.getCheckedBoxes("statecheck");
        var shiftcheckedboxes = this.heroesService.getCheckedBoxes("shiftcheck");
        //Assign all selected parameters 
        this.state = stateradio + "-" + statecheckedboxes;
        this.machine = machradio + "-" + machcheckedboxes;
        this.shift = shiftcheckedboxes;
        this.area = arearadio + "-" + areacheckedboxes;
        this.observer = obsradio + "-" + obscheckedboxes;
        this.date = startdate + " bis " + enddate;
        this.heroesService.getreportmachine(startdate, enddate, obsradio, arearadio, machradio, stateradio, obscheckedboxes, areacheckedboxes, machcheckedboxes, statecheckedboxes, shiftcheckedboxes, ExceptDate)
        .subscribe(response => this.Assignreport(response), error => console.log(error));    
    };
    app.ReportComponent.prototype.Assignreport = function (reportlist) {
        this.reportlist = reportlist;
        document.getElementById("openReportView").click();
    };
    app.ReportComponent.prototype.toggle = function (select) {
        if (select == 'allobs')
        { this.alleobs = true; }
        else {
            this.alleobs = false;
        }
    };
    app.ReportComponent.prototype.togglearea = function (select) {
        if (select == 'allarea') {
            this.allearea = true;
        }
        else {
            this.allearea = false;
        }
    };
    app.ReportComponent.prototype.toggleactivity = function (select) {
        if (select == 'allactivity') {
            this.alleactivity = true;
        }
        else {
            this.alleactivity = false;
        }
    };
    app.ReportComponent.prototype.togglefunc = function (select) {
        if (select == 'allfunc') {
            this.allefunc = true;
        }
        else {
            this.allefunc = false;
        }
    };
    app.ReportComponent.prototype.toggleplace = function (select) {
        if (select == 'allplace') {
            this.alleplace = true;
        }
        else {
            this.alleplace = false;
        }
    };
    
    app.ReportComponent.prototype.togglemachine = function (select) {
        if (select == 'allmachine') {
            this.allemachine = true;
        }
        else {
            this.allemachine = false;
        }
    };
    app.ReportComponent.prototype.togglestate = function (select) {
        if (select == 'allstate') {
            this.allestate = true;
        }
        else {
            this.allestate = false;
        }
    };
  
    app.ReportComponent.prototype.printPDF = function () {
        window.print();
    };
    app.ReportComponent.prototype.fnExcelReport = function () {
        //var blob = new Blob([document.getElementById('Table').innerHTML], {
        //    type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=utf-8"
        //});
        //saveAs(blob, "Report.xlsx")
        //};
        var tab_text = "<table border='2px'><tr bgcolor='#87AFC6'>";
        var textRange; var j = 0;
        tab = document.getElementById('Table'); // id of table

        for (j = 0 ; j < tab.rows.length ; j++) {
            tab_text = tab_text + tab.rows[j].innerHTML + "</tr>";
            //tab_text=tab_text+"</tr>";
        }
        tab_text = tab_text + "</table>";
        tab_text = tab_text.replace(/<A[^>]*>|<\/A>/g, "");//remove if u want links in your table
        tab_text = tab_text.replace(/<img[^>]*>/gi, ""); // remove if u want images in your table
        tab_text = tab_text.replace(/<input[^>]*>|<\/input>/gi, ""); // reomves input params

        var ua = window.navigator.userAgent;
        var msie = ua.indexOf("MSIE ");

        if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./))      // If Internet Explorer
        {
            txtArea1.document.open("txt/html", "replace");
            txtArea1.document.write(tab_text);
            txtArea1.document.close();
            txtArea1.focus();
            sa = txtArea1.document.execCommand("SaveAs", true, "Report");
        }
        else                 //other browser not tested on IE 11
            sa = window.open('data:application/vnd.ms-excel,' + encodeURIComponent(tab_text));
        return (sa);
    };


})(window.app || (window.app = {}));


