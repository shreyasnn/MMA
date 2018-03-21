(function (app) {
    var timer = 0;
    var delay = 200;
    var prevent = false;
    app.Machinelistcomponent = ng.core

	.Component({
	    selector: 'my-machine',
	    templateUrl: 'Home/MachineLineList'
	})
	.Class({
	    constructor: [app.HeroesService, ng.router.ActivatedRoute, ng.router.Router, function Employeelistcomponent(heroesService, route, router) {
	        this.route = route;
	        this.router = router;
	        this.heroesService = heroesService;
	        this.area = heroesService.Areaname;
	        this.Observername = heroesService.Observername;
	        this.head = this.heroesService.Languagelist[8];
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
	        this.lineheader = this.heroesService.Languagelist[37];
	        this.lastdetails = this.heroesService.Languagelist[61];
	    }]
	});

    app.Machinelistcomponent.prototype.ngOnInit = function () {
        this.heroesService.getMachinelines(this.heroesService.Areaname, this.heroesService.parameter).subscribe(response => this.Assignmachinelines(response), error => console.log(error));
        if (this.heroesService.lastdata.length > 0 && this.heroesService.parameter == 'emp') {
            this.lastdata = this.heroesService.lastdata;
        }
        if (this.heroesService.MachineDatalast.length > 0 && this.heroesService.parameter != 'emp') {
            this.lastdata = this.heroesService.MachineDatalast;
        }
    };
    app.Machinelistcomponent.prototype.Assignmachinelines = function (machinelines) {
       var k = 0;
       var machinelist1 = [];
       if (this.heroesService.parameter != 'emp')
        {
           //this.machinelines = machinelines;
           var machinelines1 = [{ id: '', name: '' }];
           for (var i = 0; i < machinelines[0].length; i++) {
               machinelines1[i] = [{ id: String(machinelines[0][i]), name: String(machinelines[1][i]) }];
           }
           this.machinelines = machinelines1;
           for (var i = 0; i < machinelines[0].length; i++) {
               if (this.heroesService.machinelinecomplete == 'yes' && machinelines[1][i] == this.heroesService.machineline) {
                   this.heroesService.comletedmachinelines.push(machinelines[1][i]);
                   this.heroesService.machinelinecomplete = '';//free for next time
                }
                else {
                   machinelist1[k] = machinelines[1][i];
                    k++;
                }
            }
           this.para = 'machine';
          // this.machinelines = machinelines;
           this.comletedmachinelines = this.heroesService.comletedmachinelines;
           if (this.machinelines.length == this.comletedmachinelines.length) {
           this.heroesService.Allmachine = 'yes';
          }
       }

       else {
           this.parameter = 'emp';
           //this.machinelines = machinelines;
           var machinelines1 = [{ id: '', name: '' }];
           for (var i = 0; i < machinelines[0].length; i++) {
               machinelines1[i] = [{ id: String(machinelines[0][i]), name: String(machinelines[1][i]) }];
           }
           this.machinelines = machinelines1;
        }
        
        
    };
    app.Machinelistcomponent.prototype.goToNext = function (line) {
        timer = setTimeout(() => {
        if (!prevent) {
        if (this.heroesService.parameter == 'emp') {  
                this.heroesService.EmployeeData.push(line); 
                var link = ['/machine2', line];
                this.router.navigate(link);
             }
        else {
        var link = ['/machine2', line];
        this.router.navigate(link);
             }
        }
        prevent = false;
        }, delay);
    };
    
    app.Machinelistcomponent.prototype.getStyle = function (line) {
        for (var k = 0; k < this.heroesService.comletedmachinelines.length; k++) {
            if ((this.heroesService.comletedmachinelines[k]) == line && this.heroesService.parameter != 'emp') {
                return "#07330e";
                break;
            }
        }
        return "#266399";
    };

    app.Machinelistcomponent.prototype.goToEdit = function (machine,id, Edit) {
        clearTimeout(timer);
        prevent = true;
        this.machine = machine;
        this.machineid = id;
        Edit.show();
    };
    app.Machinelistcomponent.prototype.goToUpdate = function (id,area, machine, Edit) {
        if (machine != "") {
            this.heroesService.editmachinelinedata(id,area, machine, 'Update').subscribe(response => this.Assignstatus(response), error => console.log(error));
            Edit.close();
        }
    };
    app.Machinelistcomponent.prototype.goToDelete = function (id,area, machine, confirm1, Edit) {
        this.heroesService.editmachinelinedata(id,area, machine, 'Delete').subscribe(response => this.Assignstatus(response), error => console.log(error));
        Edit.close();
        confirm1.close();
    };
    app.Machinelistcomponent.prototype.Assignstatus = function (status) {
        this.heroesService.Md2Toast.show(this.heroesService.Languagelist[31]); // The toast is used to show that action completed succesfully
        this.ngOnInit();
    };

})(window.app || (window.app = {}));