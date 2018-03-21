(function (app) {
    var timer = 0;
    var delay = 200;
    var prevent = false;
    app.Employeesubactivitieslistcomponent = ng.core

	.Component({
	    selector: 'my-employee4',
	    templateUrl: 'Home/EmployeeSubActivitiesList'
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

    app.Employeesubactivitieslistcomponent.prototype.ngOnInit = function () {
        this.heroesService.getEmployeeSubActivities(this.route.snapshot.params.activity).subscribe(response => this.Assignsubactivities(response), error => console.log(error));
        if (this.heroesService.lastdata.length > 0) {
            this.lastdata = this.heroesService.lastdata;
        }
    };
    app.Employeesubactivitieslistcomponent.prototype.Assignsubactivities = function (activities) {
        var activities1 = [{ id: '', name: '' }];
        for (var i = 0; i < activities[0].length; i++) {
            activities1[i] = [{ id: String(activities[0][i]), name: String(activities[1][i]) }];
        }
        this.activities = activities1;
    };
    app.Employeesubactivitieslistcomponent.prototype.goToNext = function (activity) {
        timer = setTimeout(() => {
        if (!prevent) {
        this.heroesService.EmployeeData.push(activity);
        var link = ['/machine1'];
        this.router.navigate(link);
        }
        prevent = false;
        }, delay);
    };
    app.Employeesubactivitieslistcomponent.prototype.goToEdit = function (id, activity, Edit) {
        clearTimeout(timer);
        prevent = true;
        this.id = id;
        this.activityname = activity;
        Edit.show();
    };
    app.Employeesubactivitieslistcomponent.prototype.goToUpdate = function (id, activity, Edit) {
        if (activity != "") {
            this.heroesService.editsubactivitydata(id, activity, 'Update').subscribe(response => this.Assignstatus(response), error => console.log(error));
            Edit.close();
        }
    };
    app.Employeesubactivitieslistcomponent.prototype.goToDelete = function (id, activity, confirm1, Edit) {
        this.heroesService.editsubactivitydata(id, activity, 'Delete').subscribe(response => this.Assignstatus(response), error => console.log(error));
        Edit.close();
        confirm1.close();
    };
    app.Employeesubactivitieslistcomponent.prototype.Assignstatus = function (status) {
        this.heroesService.Md2Toast.show(this.heroesService.Languagelist[31]); // The toast is used to show that action completed succesfully
        this.ngOnInit();
    };

})(window.app || (window.app = {}));