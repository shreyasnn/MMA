(function (app) {
	app.Herolistcomponent = ng.core
	.Component({
	    selector: 'app-list',//
		templateUrl: "Home/appcomponent",
		//styleUrls: ['Content/material-icon.css']
	})
	.Class({
	    constructor: [app.HeroesService, ng.router.ActivatedRoute, function Herolistcomponent(heroesService,route) {
	        this.heroesService = heroesService;
	        this.route = route;
	    }]
	});
	app.Herolistcomponent.prototype.ngOnInit = function () {
	    setTimeout(() => {
	        this.heroesService.getlanguagedata(this.heroesService.lang).subscribe(response => this.Assignresponse(response), error => console.log(error));
	    }, 1000);
	};
	app.Herolistcomponent.prototype.Assignresponse = function (list) {
	    this.list = list;
	    this.heading = list[10];
	    this.start = list[11];
	    this.userguide = list[12];
	    this.report = list[13];
	    this.timeslot = list[14];
	    this.defination = list[15];
	    this.userintro = list[16];
	    this.reportlink = this.heroesService.Languagelist[56];
	    this.timewindowlink = this.heroesService.Languagelist[40];
	};

})(window.app || (window.app = {}));