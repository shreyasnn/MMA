(function (app) {
    app.Timelistcomponent = ng.core
	.Component({
	    selector: 'my-time',
	    templateUrl: 'Home/TimeList',
	})
	.Class({
	    constructor: [app.HeroesService, ng.router.ActivatedRoute, ng.router.Router, function Timelistcomponent(heroesService, route, router) {
	        this.route = route;
	        this.router = router;
	        this.heroesService = heroesService;
	        this.Observername = heroesService.Observername;
	        this.obsname = this.heroesService.Languagelist[1];
	        this.placename = this.heroesService.Languagelist[2];
	        this.new = this.heroesService.Languagelist[17];
	        if (this.route.snapshot.url == 'machine') {
	            this.heroesService.parameter = 'mach';
	            this.head = this.function = this.heroesService.Languagelist[33];
	        }
	        else {
	            this.head = this.function = this.heroesService.Languagelist[34];
	            this.heroesService.parameter = 'emp';
	        } 
	       if (heroesService.Areaname != null) {
	            if (this.route.snapshot.params.area == null)
	                this.area = heroesService.Areaname;
	            else {
	                this.area = this.route.snapshot.params.area;
	                heroesService.Areaname = this.route.snapshot.params.area;
	            }
	        }
	       else {
	            this.area = this.route.snapshot.params.area;
	            heroesService.Areaname = this.route.snapshot.params.area;
	        }    
	    }]
	});

    app.Timelistcomponent.prototype.ngOnInit = function () {
        if (this.heroesService.Observername != undefined && this.heroesService.Areaname != undefined) {
            this.heroesService.getTime(this.area).subscribe(response => this.Assigntime(response), error => console.log(error));
        }
        else {
            this.nonselect =this.heroesService.Languagelist[59];
        }
        };
    app.Timelistcomponent.prototype.Assigntime = function (timelist) {
        var timelist1 = [];
        for (var i = 0; i < timelist.length-1; i++) {
            timelist1.push(timelist[i]);
        }
        this.heroesService.shift=timelist.pop();
        this.timelist = timelist1;
       // this.timelist = timelist;  
    };
    app.Timelistcomponent.prototype.goToNext = function () {
        if (this.route.snapshot.url == 'machine') {
            //this.heroesService.parameter = 'mach';
            var link = ['/machine1'];
           this.router.navigate(link);
        }
        else {
            var link = ['/employee2'];
            this.router.navigate(link);
        };
    };
    app.Timelistcomponent.prototype.getStyle = function (time) {
        function addZero(i) {
            if (i < 10) {
                i = "0" + i;
            }
            return i;
        }
        function parseTime(s) {
            var c = s.split(':');
            return parseInt(c[0]) * 60 + parseInt(c[1]);
        }
        function timeFromMins(mins) {
            function z(n) { return (n < 10 ? '0' : '') + n; }
            var h = (mins / 60 | 0) % 24;
            var m = mins % 60;
            return z(h) + ':' + z(m);
        }

        var d = new Date();
        var n = addZero(d.getHours()) + ':' + addZero(d.getMinutes());
        var slot = parseTime(this.timelist[1]) - parseTime(this.timelist[0]);
        var nexttime = timeFromMins(parseTime(time) + parseTime("00:" + slot));//
        if (this.heroesService.Allemployee == 'yes' && this.heroesService.Allmachine == 'yes' && n >= time && n < nexttime) {
            this.heroesService.Completedtimeslot.push(time);
            this.heroesService.Allemployee = '';
            this.heroesService.Allmachine = '';
            this.heroesService.CompletedEmployee = [];
            this.heroesService.EmployeeDatalast = [];
            this.heroesService.comletedmachinelines = [];
            this.heroesService.machinelinecomplete = '';
            this.heroesService.MachineDatalast = [];
        }
        if (this.heroesService.Completedtimeslot.length > 0) {
            for (var k = 0; k < this.heroesService.Completedtimeslot.length; k++) {
                if ((this.heroesService.Completedtimeslot[k]) == time) {
                    return "#07330e";
                    break;
                }
            }
        }
       if (n >= time && n < nexttime) {
            return "#ff0000";
        }
        else {
            this.prevtime = time;
            return "#266399";
        }          
    };

})(window.app || (window.app = {}));