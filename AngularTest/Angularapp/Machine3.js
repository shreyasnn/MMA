(function (app) {
    var timer = 0;
    var delay = 200;
    var prevent = false;
    app.Machinestatecomponent = ng.core

	.Component({
	    selector: 'my-machine3',
	    templateUrl: 'Home/MachinesStateList'
	})
	.Class({
	    constructor: [app.HeroesService, ng.router.ActivatedRoute, ng.router.Router, function Employeelistcomponent(heroesService, route, router) {
	        this.route = route;
	        this.router = router;
	        this.heroesService = heroesService;
	        this.area = heroesService.Areaname;
	        this.Observername = heroesService.Observername;
	        this.head = this.heroesService.Languagelist[9];
	        this.new = this.heroesService.Languagelist[17];
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
	        this.machinestate = this.heroesService.Languagelist[39];
	        this.lastdetails = this.heroesService.Languagelist[61];
	    }]
	});

    app.Machinestatecomponent.prototype.ngOnInit = function () {
        this.heroesService.getMachinestates().subscribe(response => this.Assignmachinestate(response), error => console.log(error));
         if (this.heroesService.MachineDatalast.length > 0) {
            this.lastdata = this.heroesService.MachineDatalast;
        }
    };
    app.Machinestatecomponent.prototype.Assignmachinestate = function (states) {
        var states1 = [{ id: '', name: '' }];
        for (var i = 0; i < states[0].length; i++) {
            states1[i] = [{ id: String(states[0][i]), name: String(states[1][i]) }];
        }
        this.states = states1;
    };
    app.Machinestatecomponent.prototype.goToNext = function (state) {
      timer = setTimeout(() => {
          if (!prevent) {
        this.heroesService.MachineData.push(state);
        if (this.heroesService.MachineDatalast.length > 0) {
            this.heroesService.insertmachinedata(String(this.heroesService.MachineDatalast)).subscribe(response => this.Assignresult(response), error => console.log(error));
        }
        else {
            this.heroesService.MachineDatalast = [];
            this.heroesService.MachineDatalast = this.heroesService.MachineData;
            var link = ['/machine2', this.heroesService.machineline];
            this.router.navigate(link);
        }
          }
          prevent = false;
      }, delay);
    };

    app.Machinestatecomponent.prototype.Assignresult = function (result) {
        this.heroesService.MachineDatalast = [];
        this.heroesService.MachineDatalast = this.heroesService.MachineData;
        var link = ['/machine2', this.heroesService.machineline];
        this.router.navigate(link);
    };

    app.Machinestatecomponent.prototype.goToEdit = function (id, name, Edit) {
        clearTimeout(timer);
        prevent = true;
        this.stateid = id;
        this.name = name;
        Edit.show();
    };
    app.Machinestatecomponent.prototype.goToUpdate = function (id, name, Edit) {
        if (name != "") {
            this.heroesService.editmachinestatedata(id, name, 'Update').subscribe(response => this.Assignstatus(response), error => console.log(error));
            Edit.close();
        }
    };
    app.Machinestatecomponent.prototype.goToDelete = function (id, name, confirm1, Edit) {
        this.heroesService.editmachinestatedata(id, name, 'Delete').subscribe(response => this.Assignstatus(response), error => console.log(error));
        Edit.close();
        confirm1.close();
    };
    app.Machinestatecomponent.prototype.Assignstatus = function (status) {
        this.heroesService.Md2Toast.show(this.heroesService.Languagelist[31]); // The toast is used to show that action completed succesfully
        this.ngOnInit();
    };
})(window.app || (window.app = {}));