//This JS file contains the component and functions for All Employee
(function (app) {
    app.Employeelistcomponent = ng.core  
	.Component({
	    selector: 'my-employee',
	    templateUrl: 'Home/EmployeeList',
	})
	.Class({
	    constructor: [app.HeroesService, ng.router.ActivatedRoute, ng.router.Router,  function Employeelistcomponent(heroesService, route, router) {
	        this.route = route;
	        this.router = router;
	        this.heroesService = heroesService;
	        this.Observername = heroesService.Observername;
	        this.heroesService.Areaname = this.route.snapshot.params.area;
	        this.area = heroesService.Areaname;
	        this.head = this.heroesService.Languagelist[5];
	        this.new = this.heroesService.Languagelist[17];
	        this.delete = this.heroesService.Languagelist[19];
	        this.warning = this.heroesService.Languagelist[24];
	        this.warndata = this.heroesService.Languagelist[25];
	        this.yes = this.heroesService.Languagelist[26];
	        this.no = this.heroesService.Languagelist[27];
	        this.send = this.heroesService.Languagelist[28];
	        this.obsname = this.heroesService.Languagelist[1];
	        this.placename = this.heroesService.Languagelist[2];
	        this.nonselect = this.heroesService.Languagelist[57];
	    }]
	});
    app.Employeelistcomponent.prototype.ngOnInit = function () {     
        this.heroesService.getEmployee(this.area).subscribe(response => this.Assignemployee(response), error => console.log(error));
    };
    app.Employeelistcomponent.prototype.Assignemployee = function (employeelist) {
        var employeelist1 = [{ id:'', name:'' }];
        for (var i = 0; i < employeelist[1].length; i++) {
            employeelist1[i] = [{ id: String(employeelist[1][i]), name: String(employeelist[0][i]) }];
        }
        this.employeelist = (employeelist[0].length == 0) ? '' : employeelist1;
    };
    app.Employeelistcomponent.prototype.goToSelectEmployee = function (action, confirm1) {
        this.action = action;
        this.checkedboxes = this.heroesService.getCheckedBoxes("messageCheckbox");
        this.heroesService.SelectedEmployee = this.checkedboxes;
        this.heroesService.insertemployeelist(this.heroesService.Observerid, this.heroesService.SelectedEmployee,action).subscribe(response => this.Assignresult(response), error => console.log(error));    
        confirm1.close();
    };
    app.Employeelistcomponent.prototype.Assignresult = function (result) {
        this.heroesService.Md2Toast.show(this.heroesService.Languagelist[31]); // The toast is used to show that action completed succesfully
        if (this.action == "Insert") {
            var link = ['/time'];
            this.router.navigate(link);
        }
        else if (this.action == "Delete") {
            this.ngOnInit();
        }
    }
    app.Employeelistcomponent.prototype.goToColorChange = function (myDiv) {
        var mylable = document.getElementById(myDiv);
        if (mylable.style.backgroundColor == 'rgb(38, 99, 153)') {
            mylable.style.backgroundColor = "#07330e";
        }
        else {
            mylable.style.backgroundColor = "#266399";
        } 
    };
   
})(window.app || (window.app = {}));