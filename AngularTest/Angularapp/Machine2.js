(function (app) {
    var timer = 0;
    var delay = 200;
    var prevent = false;
    app.Machinenamelistcomponent = ng.core

	.Component({
	    selector: 'my-machine2',
	    templateUrl: 'Home/MachinesList'
	})
	.Class({
	    constructor: [app.HeroesService, ng.router.ActivatedRoute, ng.router.Router, function Employeelistcomponent(heroesService, route, router) {
	        this.route = route;
	        this.router = router;
	        this.heroesService = heroesService;
	        this.area = heroesService.Areaname;
	        this.Observername = heroesService.Observername;
	        this.head = this.heroesService.Languagelist[4];
	        this.obsname = this.heroesService.Languagelist[1];
	        this.placename = this.heroesService.Languagelist[2];
	        this.update = this.heroesService.Languagelist[18];
	        this.delete = this.heroesService.Languagelist[19];
	        this.cancel = this.heroesService.Languagelist[20];
	        this.warning = this.heroesService.Languagelist[24];
	        this.warndata = this.heroesService.Languagelist[25];
	        this.yes = this.heroesService.Languagelist[26];
	        this.no = this.heroesService.Languagelist[27];
	        this.company = this.heroesService.Languagelist[21];
	        this.station = this.heroesService.Languagelist[38];
	        this.lastdetails = this.heroesService.Languagelist[61];
	    }]
	});

    app.Machinenamelistcomponent.prototype.ngOnInit = function () {
        this.heroesService.getMachines(this.route.snapshot.params.line, this.heroesService.parameter).subscribe(response => this.Assignmachinelines(response), error => console.log(error));
        this.heroesService.machineline = this.route.snapshot.params.line;
        if (this.heroesService.lastdata.length > 0 && this.heroesService.parameter == 'emp') {
            this.lastdata = this.heroesService.lastdata;
        }
        if (this.heroesService.MachineDatalast.length > 0 && this.heroesService.parameter != 'emp') {
            this.lastdata = this.heroesService.MachineDatalast;
        }
    };
    app.Machinenamelistcomponent.prototype.Assignmachinelines = function (machines) {
       
        if (this.heroesService.parameter == 'emp')
        {
            //this.machines = machines;
            var machines1 = [{ id: '', name: '' }];
            for (var i = 0; i < machines[0].length; i++) {
                machines1[i] = [{ id: String(machines[0][i]), name: String(machines[1][i]) }];
            }
            this.machines = machines1;
        }
        else {
            //this.machines = machines;
            var machines1 = [{ id: '', name: '' }];
            for (var i = 0; i < machines[0].length; i++) {
                machines1[i] = [{ id: String(machines[0][i]), name: String(machines[1][i]) }];
            }
            this.machines = machines1;
        var machinelist1 = [];
        var j = 0; var k = 0;
        for (var i = 0; i < machines[0].length; i++) {
            if (this.heroesService.MachineDatalast.length > 0 && this.heroesService.parameter != 'emp') {
                if (this.heroesService.MachineDatalast.length > 0 && this.heroesService.MachineDatalast[4] == String(machines[1][i])) {
                    if (this.heroesService.CompletedMachine.length > 0 && this.heroesService.MachineDatalast[4] == (this.heroesService.CompletedMachine[this.heroesService.CompletedMachine.length - 1])) {
                    }
                    else {
                        this.heroesService.CompletedMachine.push(String(machines[1][i]));
                    }
                }
                else if (this.heroesService.CompletedMachine.length > 0 && this.checkarray(String(machines[1][i]))) {
                }
                else {
                    machinelist1[j] = String(machines[1][i]);
                    j++;
                }
            }
            else {
                machinelist1[i] = String(machines[1][i]);
            }
        }
        //this.machines = machinelist1;
        //this.completedmachinelist = this.heroesService.CompletedMachine;
        if (machinelist1.length == 0)
        {
            if (this.heroesService.MachineDatalast.length > 0) {
                this.heroesService.insertmachinedata(String(this.heroesService.MachineDatalast)).subscribe(response => this.Assignmachineresult(response), error => console.log(error));
            }    
        }
      }
    };
    app.Machinenamelistcomponent.prototype.Assignmachineresult = function (result) {
        this.heroesService.CompletedMachine = [];
        this.heroesService.machinelinecomplete = 'yes';
        var link = ['/machine1'];
        this.router.navigate(link);
    };
    app.Machinenamelistcomponent.prototype.goToNext = function (machine) {
         timer = setTimeout(() => {
        if (!prevent) {
        if (this.heroesService.parameter == 'emp')
        {
            if (this.heroesService.lastdata.length > 0) {
                this.heroesService.insertemplyeedata(String(this.heroesService.lastdata)).subscribe(response => this.Assignresult(response), error => console.log(error));
            }
            else{
                this.heroesService.EmployeeDatalast = [];
                this.heroesService.lastdata = [];
                this.heroesService.EmployeeData.push(machine);
                this.heroesService.EmployeeDatalast = this.heroesService.EmployeeData
                this.heroesService.lastdata = this.heroesService.EmployeeData;
                var link = ['/employee2'];
                this.router.navigate(link);
            }
            
        }
        else {
            if (this.heroesService.CompletedMachine.length > 0 && machine == (this.heroesService.CompletedMachine[this.heroesService.CompletedMachine.length - 1])) {
                this.heroesService.CompletedMachine.pop();
            }
            this.heroesService.MachineData = [];
            this.heroesService.MachineData.push(this.heroesService.Observerid);
            var dt = new Date();
            //var dd = dt.getDate() + "/" + (dt.getMonth() + 1) + "/" + dt.getFullYear();
            var dd = dt.getFullYear() + "-" + (dt.getMonth() + 1) + "-" + dt.getDate();
            this.heroesService.MachineData.push(dd);
            this.heroesService.MachineData.push(this.heroesService.shift);
            this.heroesService.MachineData.push(this.heroesService.Areaname);
            this.heroesService.MachineData.push(machine);
            var link = ['/machine3']; 
            this.router.navigate(link);
        }
        }
        prevent = false;
         }, delay);
    };

    app.Machinenamelistcomponent.prototype.checkarray = function (machinename) {
        for (var k = 0; k < this.heroesService.CompletedMachine.length; k++) {
            if ((this.heroesService.CompletedMachine[k]) == machinename) {
                return true;
                break;
            }
        }
    };
    app.Machinenamelistcomponent.prototype.Assignresult = function (result) {
        this.heroesService.EmployeeDatalast = [];
        this.heroesService.lastdata = [];
        this.heroesService.EmployeeData.push(machine);
        this.heroesService.EmployeeDatalast = this.heroesService.EmployeeData
        this.heroesService.lastdata = this.heroesService.EmployeeData;
        var link = ['/employee2'];
        this.router.navigate(link);
    };
    app.Machinenamelistcomponent.prototype.getButtonLabel = function (machine) {
        //var mach = machine.split(" ").join(" ⇔ ");
        //return mach;
        return machine;
    };

    app.Machinenamelistcomponent.prototype.getStyle = function (machine) {
        for (var k = 0; k < this.heroesService.CompletedMachine.length; k++) {
            if ((this.heroesService.CompletedMachine[k]) == machine && this.heroesService.parameter != 'emp') {
                return "#07330e";
                break;
            }
        }
        return "#266399";
    };
    app.Machinenamelistcomponent.prototype.goToEdit = function (machine, id, Edit) {
        clearTimeout(timer);
        prevent = true;
        this.machine = machine;
        this.machineid = id;
        Edit.show();
    };
    app.Machinenamelistcomponent.prototype.goToUpdate = function (id, area, machine, Edit) {
        if (machine != "") {
            this.heroesService.editmachinedata(id, area, machine, 'Update').subscribe(response => this.Assignstatus(response), error => console.log(error));
            Edit.close();
        }
    };
    app.Machinenamelistcomponent.prototype.goToDelete = function (id, area, machine, confirm1, Edit) {
        this.heroesService.editmachinedata(id, area, machine, 'Delete').subscribe(response => this.Assignstatus(response), error => console.log(error));
        Edit.close();
        confirm1.close();
    };
    app.Machinenamelistcomponent.prototype.Assignstatus = function (status) {
        this.heroesService.Md2Toast.show(this.heroesService.Languagelist[31]); // The toast is used to show that action completed succesfully
        this.ngOnInit();
    };
})(window.app || (window.app = {}));