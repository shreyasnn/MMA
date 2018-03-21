//This JS file contains all the functions to insert new data in database by User

(function (app) {
    app.Addentrycomponent = ng.core

	.Component({
	    selector: 'my-entry',
	    templateUrl: 'Home/Addentry'
	})
	.Class({
	    constructor: [app.HeroesService, ng.router.ActivatedRoute, ng.router.Router, function Addentrycomponent(heroesService, route, router) {
	        this.route = route;
	        this.router = router;
	        this.heroesService = heroesService;
	        this.select = this.route.snapshot.params.selected;
	        this.area = heroesService.Areaname;
	        this.Observername = heroesService.Observername;
	        this.Observerid=this.heroesService.Observerid;
          //All translator parameters  start
	        this.back = this.heroesService.Languagelist[0];
	        this.company = this.heroesService.Languagelist[21];
	        this.firstname = this.heroesService.Languagelist[22];
	        this.surname = this.heroesService.Languagelist[23];
	        this.send = this.heroesService.Languagelist[28];
	        this.placename = this.heroesService.Languagelist[29];
	        this.obsname = this.heroesService.Languagelist[1];
	        this.enterinfo = this.heroesService.Languagelist[30];
	        this.function = this.heroesService.Languagelist[32];
	        this.empactivity = this.heroesService.Languagelist[35];
	        this.empdownactivity = this.heroesService.Languagelist[36];
	        this.lineheader = this.heroesService.Languagelist[37];
	        this.station = this.heroesService.Languagelist[38];
	        this.machinestate = this.heroesService.Languagelist[39];
	      //All translator parameters end here
	        if (this.heroesService.parameter == 'emp') {
	            this.type = "Mitarbeiter";
	        }
	        else {
	            this.type = "Hybrid";
	        }
	        this.link = [''];
	    }]
	});

    app.Addentrycomponent.prototype.ngOnInit = function () {  
    };
    //Function to add Obsrever details into database
    app.Addentrycomponent.prototype.addobserver = function (name, vorname,action) {
        if (name != "" && vorname != "") {
            this.link = ['/obs'];
            this.heroesService.insertobserverdata(name, vorname,action).subscribe(response => this.Assignstatus(response), error => console.log(error));
        }
    };
    //Function to add Area details into database
    app.Addentrycomponent.prototype.addarea = function (area,action) {
        if (area != "") {
            this.link = ['/area'];
            this.heroesService.insertareadata(area, action).subscribe(response => this.Assignstatus(response), error => console.log(error));
        }
    };
    //Function to add Employee details into database
    app.Addentrycomponent.prototype.addemployee = function (name, vorname, empfunction, area, obsname,action) {
        if (name != "" && vorname != "" && empfunction != "" && area != "" && obsname != "") {
        if (obsname == "NA")
        {
            this.link = ['/employee1', this.area];
        }
        else {
            this.link = ['/employee2'];
        }
        this.heroesService.insertemployeedata(name,vorname,empfunction,area,obsname,action).subscribe(response => this.Assignstatus(response), error => console.log(error));  
    }
    };

    app.Addentrycomponent.prototype.checkunter = function (check,value) {
        if(check== false){
            this.downact = 'show';
        }
        else {
            this.downact = 'hide';
        }
    };

    //Function to add Activity details into database
    app.Addentrycomponent.prototype.addactivity = function (activity, downactivity,action) {
        if (activity != "") {
            this.link = ['/employee3'];
            if (this.downact == 'show' && downactivity != "")
        {
            this.heroesService.insertactivitydata(activity,downactivity,action).subscribe(response => this.Assignstatus(response), error => console.log(error));
        }
        else {
            this.heroesService.insertactivitydata(activity, "",action).subscribe(response => this.Assignstatus(response), error => console.log(error));
        }
    }
    };
    //Function to add Machine details into database
    app.Addentrycomponent.prototype.addmachine = function (place, machine, downmachine, type,action) {
       
        if (place != "" && machine != "" && type != "" && downmachine != "") {
            this.link = ['/machine1'];    
            this.heroesService.insertmachine(place,machine,downmachine,type,action).subscribe(response => this.Assignstatus(response), error => console.log(error));
        }
            
    };
    //Function to add Machine state details into database
    app.Addentrycomponent.prototype.addmachinestate = function (state,action) {
        if (state != "") {
            this.link = ['/machine3'];
            this.heroesService.insertmachinestatedata(state, action).subscribe(response => this.Assignstatus(response), error => console.log(error));
        }
    };
    //Function to route the data to next link if the action done successfully
    app.Addentrycomponent.prototype.Assignstatus = function (status) {
        this.heroesService.Md2Toast.show(this.heroesService.Languagelist[31]); // The toast is used to show that action completed succesfully
        this.router.navigate(this.link);
    };
    app.Addentrycomponent.prototype.goback = function () {
        window.history.go(-1);
        return false;
    };

   
})(window.app || (window.app = {}));

