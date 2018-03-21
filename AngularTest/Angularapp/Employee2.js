//This JS file contains the component and functions for selected Employees 
(function (app) {
    var dd;
    var timer = 0;
    var delay = 200;
    var prevent = false;
    app.SelectedEmployeelistcomponent = ng.core
	.Component({
	    selector: 'my-employee2',
	    templateUrl: 'Home/SelectedEmployeeList'
	})
	.Class({
	    constructor: [app.HeroesService, ng.router.ActivatedRoute, ng.router.Router, function Employeelistcomponent(heroesService, route, router) {
	        this.route = route;
	        this.router = router;
	        this.heroesService = heroesService;
	        this.area = heroesService.Areaname;
	        this.Observername = heroesService.Observername;
	        this.head = this.heroesService.Languagelist[6];
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
	        this.firstname = this.heroesService.Languagelist[22];
	        this.surname = this.heroesService.Languagelist[23];
	        this.functionheader = this.heroesService.Languagelist[32];
	        this.lastdetails = this.heroesService.Languagelist[61];
	        //call function to check employee is not present
	        if (heroesService.Observername != undefined && heroesService.Areaname != undefined) {
	        var dt = new Date();
	        dd = dt.getFullYear() + "-" + (dt.getMonth() + 1) + "-" + dt.getDate();
	        //this.heroesService.getAbsentEmployee(dd, this.area, this.heroesService.shift).subscribe(response => this.Assignabsentlist(response), error => console.log(error));
	        }
	        }]
	});
    app.SelectedEmployeelistcomponent.prototype.ngOnInit = function () {
        this.heroesService.getAbsentEmployee(dd, this.area, this.heroesService.shift).subscribe(response => this.Assignabsentlist(response), error => console.log(error));
        if (this.heroesService.lastdata.length > 0) {
            this.lastdata = this.heroesService.lastdata;
        }
    };
    
    app.SelectedEmployeelistcomponent.prototype.Assignemployee = function (employeelist) {
        var empid = this.heroesService.Employeeid;
        var employeelist1 = [];
        var employeelist2 = [];
        var j = 0; var k = 0;
        for (var i = 0; i < employeelist[1].length; i++) {
            employeelist2[i] = [{ id: String(employeelist[1][i]), name: String(employeelist[0][i]) }];
        }
        this.employeelist2 = employeelist2;
        for (var i = 0; i < employeelist[1].length; i++) {
            if (this.heroesService.EmployeeDatalast.length > 0)
            {
                if (this.heroesService.EmployeeDatalast.length > 0 && this.heroesService.EmployeeDatalast[4] == String(employeelist[0][i]))
                {
                    if (this.heroesService.CompletedEmployee.length > 0 && this.heroesService.EmployeeDatalast[4] == (this.heroesService.CompletedEmployee[this.heroesService.CompletedEmployee.length - 1][0]).name) {
                    }
                    else {
                        this.heroesService.CompletedEmployee.push([{ id: String(employeelist[1][i]), name: String(employeelist[0][i]) }]);
                    }
                }
                else if (this.heroesService.CompletedEmployee.length > 0 && this.checkarray(String(employeelist[0][i]))) {
                               
                }
                else {
                    employeelist1[j] = [{ id: String(employeelist[1][i]), name: String(employeelist[0][i]) }];
                    j++;
                }
            }
            
            else {
                employeelist1[i] = [{ id: String(employeelist[1][i]), name: String(employeelist[0][i]) }];
            }
        }
        this.employeelist = employeelist1;       
        this.completedemployeelist = this.heroesService.CompletedEmployee;
        if (this.employeelist.length == 0)
        {
            this.heroesService.insertemplyeedata(String(this.heroesService.lastdata)).subscribe(response => this.Assignresult(response), error => console.log(error));
            this.heroesService.Allemployee = 'yes';
            //var link = ['/time'];
            //this.router.navigate(link);
        }
    };
    app.SelectedEmployeelistcomponent.prototype.Assignabsentlist = function (absentlist) {
        this.absentlist = (absentlist.length == 0) ? null : absentlist;
         this.heroesService.getSelectEmployee(this.area, this.heroesService.Observerid).subscribe(response => this.Assignemployee(response), error => console.log(error));
    };
    app.SelectedEmployeelistcomponent.prototype.checkarray = function (empname) {
        for (var k = 0; k < this.heroesService.CompletedEmployee.length; k++) {
            if ((this.heroesService.CompletedEmployee[k][0]).name == empname) {
                return true;
                break;
            }
        }
    };
    app.SelectedEmployeelistcomponent.prototype.goToEmployeeActivity = function (id, name) {
         timer = setTimeout(() => {
        if (!prevent) {

        if (this.heroesService.CompletedEmployee.length > 0 && name == (this.heroesService.CompletedEmployee[this.heroesService.CompletedEmployee.length - 1][0]).name) {
            this.heroesService.CompletedEmployee.pop();
        }
        this.heroesService.EmployeeData = [];
        //this.heroesService.parameter = 'emp';
        this.heroesService.Employeeid = id;
        this.heroesService.EmployeeData.push(this.heroesService.Observerid);
        var dt = new Date();
        //var dd = dt.getDate() + "/" + (dt.getMonth() + 1) + "/" + dt.getFullYear();
        dd = dt.getFullYear() + "-" + (dt.getMonth() + 1) + "-" + dt.getDate();
        this.heroesService.EmployeeData.push(dd); 
        this.heroesService.EmployeeData.push(this.heroesService.shift);
        this.heroesService.EmployeeData.push(this.heroesService.Areaname);
        this.heroesService.EmployeeData.push(name);
        this.heroesService.EmployeeData.push(id);
        var splited = name.split(" ");  
        this.heroesService.EmployeeData.push(splited[2]);
        var link = ['/employee3'];
        this.router.navigate(link);
        }
        prevent = false;
         }, delay);
    };
    app.SelectedEmployeelistcomponent.prototype.Assignresult = function (result) {
    };
    app.SelectedEmployeelistcomponent.prototype.getStyle = function (id) { 
        if (this.heroesService.CompletedEmployee.length > 0 && this.checkabsent(id) != "yes")
        {
            for (var k = 0; k < this.heroesService.CompletedEmployee.length; k++) {
                if ((this.heroesService.CompletedEmployee[k][0]).id == id) {
                    return "#07330e";
                    break;
                }
            }
            return "#266399";
        }
        else if (this.checkabsent(id) == "yes")
        {
            return "red";
        }
        else {
            return "#266399";
        }    
    };
   
    app.SelectedEmployeelistcomponent.prototype.checkabsent = function (id) {
        if (this.absentlist != null)
        {
          for (var k = 0; k < this.absentlist.length; k++) {
            if ((this.absentlist[k]) == id) {
                return "yes";
                break;
            }
          }
       }
      return "no";
    }

    app.SelectedEmployeelistcomponent.prototype.goToEdit = function (id, name, Edit) {
        clearTimeout(timer);
        prevent = true;
        this.id = id;
        var name = name.split(' ');
        this.name = name[0];
        this.vorname = name[1];
        this.function = name[2];
        Edit.show();
    };
    app.SelectedEmployeelistcomponent.prototype.goToUpdate = function (id, name, vorname, function1, area, Edit) {
        if (name != "" && vorname != "" && function1 != "" && area != "") {
            this.heroesService.editemployeedata(id, name, vorname, function1, area, 'Update').subscribe(response => this.Assignstatus(response), error => console.log(error));
            Edit.close();
        }
    };
    app.SelectedEmployeelistcomponent.prototype.goToDelete = function (id, name, vorname, function1, area, confirm1, Edit) {
        this.heroesService.editemployeedata(id, name,vorname,function1,area, 'Delete').subscribe(response => this.Assignstatus(response), error => console.log(error));
        Edit.close();
        confirm1.close();
    };
    app.SelectedEmployeelistcomponent.prototype.Assignstatus = function (status) {
        this.heroesService.Md2Toast.show(this.heroesService.Languagelist[31]); // The toast is used to show that action completed succesfully
        this.ngOnInit();
    };
})(window.app || (window.app = {}));