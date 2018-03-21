(function (app) {
    app.dashboardcomponent = ng.core
	.Component({
	    selector: 'my-dashboard',//
	    templateUrl:'Home/Dashlist'
	})
	.Class({
	    constructor: [app.HeroesService, ng.router.Router, function dashboardcomponent(heroesService, router) {
	        this.router = router;
	        this.getheroes = function () {
	            this.getheroes = heroesService.getheroes().then(heroes => this.heroes = heroes.slice(1,5));
	            // console.log(heroesService.getheroes());
	        };
	    }]
	});

    app.dashboardcomponent.prototype.ngOnInit = function () {
        this.getheroes();
        this.myDate = '12.05.2016';
    };
    app.dashboardcomponent.prototype.goToDetails = function (hero) {
        var link = ['/detail', hero.id];
        this.router.navigate(link);
    };

    //app.AppComponent.prototype.onClickButton = function (hero) {
    //    this.slectedheroid = hero;
    //   // alert(slectedheroid);
    //};


})(window.app || (window.app = {}));