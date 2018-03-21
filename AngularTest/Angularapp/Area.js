//This JS file contains the component and functions for Area 
(function (app) {
    var timer = 0;
    var delay = 200;
    var prevent = false;
    app.Arealistcomponent = ng.core
    
	.Component({
	    selector: 'my-area',
	    templateUrl: 'Home/AreaList'
	})
	.Class({
	    constructor: [app.HeroesService, ng.router.ActivatedRoute, ng.router.Router, function Arealistcomponent(heroesService, route, router) {
	        this.route = route;
	        this.router = router;
	        this.heroesService = heroesService;
	        this.observer = heroesService.Observername;  
	        this.head = this.heroesService.Languagelist[2];
	        this.observername = this.heroesService.Languagelist[1];
	        this.new = this.heroesService.Languagelist[17];
	        this.update = this.heroesService.Languagelist[18];
	        this.delete = this.heroesService.Languagelist[19];
	        this.cancel = this.heroesService.Languagelist[20];
	        this.company = this.heroesService.Languagelist[21];
	        this.placename = this.heroesService.Languagelist[29];
	        this.warning = this.heroesService.Languagelist[24];
	        this.warndata = this.heroesService.Languagelist[25];
	        this.yes = this.heroesService.Languagelist[26];
	        this.no = this.heroesService.Languagelist[27];
	  
	    }]
	});
    app.Arealistcomponent.prototype.ngOnInit = function () {
        if (this.heroesService.Observername != undefined) {
            this.heroesService.getArea().subscribe(response => this.Assignarea(response), error => console.log(error));
        }
        else {
            this.nonselect =this.heroesService.Languagelist[58];
        }
       };
    app.Arealistcomponent.prototype.Assignarea = function (arealist) {
        var arealist1 = [{ id: '', name: '' }];
        for (var i = 0; i < arealist[0].length; i++) {
            arealist1[i] = [{ id: String(arealist[0][i]), name: String(arealist[1][i]) }];
        }
        this.area = arealist1;
    };
    app.Arealistcomponent.prototype.goToEmployee = function (area) {
        timer = setTimeout(() => {
        if (!prevent) {
        var link = ['/employee1', area];
        this.router.navigate(link);
            }
            prevent = false;
        }, delay);
    };
    app.Arealistcomponent.prototype.goToEdit = function (id, name, Edit) {
        clearTimeout(timer);
        prevent = true;
        this.id = id;
        this.name = name;
        Edit.show();
    };
    app.Arealistcomponent.prototype.goToUpdate = function (id, name,Edit) {
        if (name != "") {
            this.heroesService.editareadata(id, name, 'Update').subscribe(response => this.Assignstatus(response), error => console.log(error));
            Edit.close();
        }
    };
    app.Arealistcomponent.prototype.goToDelete = function (id, name, confirm1, Edit) {
        this.heroesService.editareadata(id, name, 'Delete').subscribe(response => this.Assignstatus(response), error => console.log(error));
        Edit.close();
        confirm1.close();
    };
    app.Arealistcomponent.prototype.Assignstatus = function (status) {
        this.heroesService.Md2Toast.show(this.heroesService.Languagelist[31]); // The toast is used to show that action completed succesfully
        this.ngOnInit();
    };
})(window.app || (window.app = {}));