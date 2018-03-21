(function (app) {
    var act;
    var timer = 0;
    var delay = 200;
    var prevent = false;
    app.Employeeactivitieslistcomponent = ng.core
   
	.Component({
	    selector: 'my-employee3',
	    templateUrl: 'Home/EmployeeActivitiesList'
	})
	.Class({
	    constructor: [app.HeroesService, ng.router.ActivatedRoute, ng.router.Router, function Employeelistcomponent(heroesService, route, router) {
	        this.route = route;
	        this.router = router;
	        this.heroesService = heroesService;
	        this.area = heroesService.Areaname;
	        this.Observername = heroesService.Observername;
	        this.head = this.heroesService.Languagelist[7];
	        this.obsname = this.heroesService.Languagelist[1];
	        this.placename = this.heroesService.Languagelist[2];
	        this.new = this.heroesService.Languagelist[17];
	        this.update = this.heroesService.Languagelist[18];
	        this.delete = this.heroesService.Languagelist[19];
	        this.cancel = this.heroesService.Languagelist[20];
	        this.warning = this.heroesService.Languagelist[24];
	        this.warndata = this.heroesService.Languagelist[25];
	        this.yes = this.heroesService.Languagelist[26];
	        this.no = this.heroesService.Languagelist[27];
	        this.company = this.heroesService.Languagelist[21];
	        this.empactivity = this.heroesService.Languagelist[35];
	        this.lastdetails = this.heroesService.Languagelist[61];
	    }]
	});
    app.Employeeactivitieslistcomponent.prototype.ngOnInit = function () {
        this.heroesService.getEmployeeActivities().subscribe(response => this.Assignactivities(response), error => console.log(error));
        if (this.heroesService.lastdata.length > 0) {
            this.lastdata = this.heroesService.lastdata;
        }
    };
    app.Employeeactivitieslistcomponent.prototype.Assignactivities = function (activities) {
        var activities1 = [{ id: '', name: '' }];
        for (var i = 0; i < activities[0].length; i++) {
            activities1[i] = [{ id: String(activities[0][i]), name: String(activities[1][i]) }];
        }
        this.activities = activities1;
    };
    app.Employeeactivitieslistcomponent.prototype.getcolor = function (activity) {
        if (activity == "Nicht anwesend") {
            return "red";
        }
        else if (activity == "Nicht auffindbar") {
            return "#808000";
        }
        else {
            return "#266399";
        }
    };
    app.Employeeactivitieslistcomponent.prototype.goToNext = function (activity) {
        timer = setTimeout(() => {
        if (!prevent) {

           act = activity;
           if (activity == "Nicht anwesend" || activity == "Nicht auffindbar") {
             if (this.heroesService.lastdata.length > 0) {
                this.heroesService.insertemplyeedata(String(this.heroesService.lastdata)).subscribe(response => this.Assignresult(response), error => console.log(error));
                }
               this.heroesService.EmployeeDatalast = [];
              this.heroesService.lastdata = [];
              this.heroesService.EmployeeData.push(activity);
              this.heroesService.EmployeeDatalast = this.heroesService.EmployeeData
              this.heroesService.lastdata = this.heroesService.EmployeeData;
              this.heroesService.EmployeeData = [];
              var link = ['/employee2'];
              this.router.navigate(link);
           }
        else {
            this.heroesService.getEmployeeSubActivities(activity).subscribe(response => this.Assignsubactivities(response), error => console.log(error));
        } 
            }
            prevent = false;
        }, delay);
    };
    app.Employeeactivitieslistcomponent.prototype.Assignresult = function (result) {
    };
    app.Employeeactivitieslistcomponent.prototype.Assignsubactivities = function (subactivities) {     
        if (subactivities[0].length == 0) {
            this.heroesService.EmployeeData.push(act);
            //this.heroesService.EmployeeData.push(subactivities);
            var link = ['/machine1'];
            this.router.navigate(link);
        }
        else {
            var link = ['/employee4', act];
            this.router.navigate(link);
        }
    };

    app.Employeeactivitieslistcomponent.prototype.goToEdit = function (id, activity, Edit) {
        clearTimeout(timer);
        prevent = true;
        this.id = id;
        this.activityname = activity;
        Edit.show();
    };
    app.Employeeactivitieslistcomponent.prototype.goToUpdate = function (id, activity,Edit) {
        if (activity != "") {
            this.heroesService.editactivitydata(id, activity,'Update').subscribe(response => this.Assignstatus(response), error => console.log(error));
            Edit.close();
        }
    };
    app.Employeeactivitieslistcomponent.prototype.goToDelete = function (id, activity,confirm1, Edit) {
        this.heroesService.editactivitydata(id, activity, 'Delete').subscribe(response => this.Assignstatus(response), error => console.log(error));
        Edit.close();
        confirm1.close();
    };
    app.Employeeactivitieslistcomponent.prototype.Assignstatus = function (status) {
        this.heroesService.Md2Toast.show(this.heroesService.Languagelist[31]); // The toast is used to show that action completed succesfully
        this.ngOnInit();
    };

})(window.app || (window.app = {}));



  