(function (app) {
    app.Timeslotcomponent = ng.core

	.Component({
	    selector: 'my-timeslot',
	    templateUrl: 'Home/SubmitTime'
	})
	.Class({
	    constructor: [app.HeroesService, ng.router.ActivatedRoute, ng.router.Router, function Employeelistcomponent(heroesService, route, router) {
	        this.route = route;
	        this.router = router;
	        this.heroesService = heroesService;
	        this.head = this.heroesService.Languagelist[40];
	        this.timeduration = this.heroesService.Languagelist[41];
	        this.shiftname = this.heroesService.Languagelist[42];
	        this.placename = this.heroesService.Languagelist[2];
	        this.warning = this.heroesService.Languagelist[24];
	        this.cancel = this.heroesService.Languagelist[20];
	        this.fillall = this.heroesService.Languagelist[43];
	        this.earlyshift = this.heroesService.Languagelist[44];
	        this.lateshift = this.heroesService.Languagelist[45];
	        this.nightshift = this.heroesService.Languagelist[46];
	        this.send = this.heroesService.Languagelist[28];
	    }]
	});
    app.Timeslotcomponent.prototype.ngOnInit = function () {
        this.heroesService.getArea().subscribe(response => this.Assignarea(response), error => console.log(error));
        this.itemtypec = "";
    };
    app.Timeslotcomponent.prototype.Assignarea = function (area) {
        var arealist1 = [{ id: '', name: '' }];
        for (var i = 0; i < area[0].length; i++) {
            arealist1[i] = [{ id: String(area[0][i]), name: String(area[1][i]) }];
        }
        this.area = arealist1;
       // this.area = area;
    };
    app.Timeslotcomponent.prototype.Assignstatus = function (status) {
        //alert("Sie änderten die Zeit erfolgreich");
        this.heroesService.Md2Toast.show(this.heroesService.Languagelist[31]);
        var link = ['/intro'];
        this.router.navigate(link);
    };
    app.Timeslotcomponent.prototype.goToSubmit = function (time, shift, ort,starttime,endtime) {
        this.heroesService.inserttimeslot(time, shift, ort,starttime, endtime).subscribe(response => this.Assignstatus(response), error => console.log(error));     
    };

})(window.app || (window.app = {}));