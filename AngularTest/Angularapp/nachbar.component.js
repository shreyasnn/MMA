(function (app) {
	app.nachbarcomponent = ng.core

	.Component({
		selector: 'my-app',
		template:
		  "<section layout='row' layout-sm='column' layout-align='center center' layout-wrap class='bgimg noprint'>" +
				 "<button (click)='goToBack()'>&laquo; {{back}} </button>" +
				 "<button routerLink='/intro' routerLinkActive='activeclass'>MMA</button>" +
				 "<button routerLink='/obs' routerLinkActive='activeclass'>{{Observer}}</button>" +
				 "<button routerLink='/area' routerLinkActive='activeclass'>{{Place}}</button>" +
				 "<button routerLink='/employee' routerLinkActive='activeclass'>{{Employee}}</button>" +
				 "<button routerLink='/machine' routerLinkActive='activeclass'>{{Machine}}</button>" +
				 "<button value='click to toggle fullscreen' (click)='toggleFullScreen()'>VB</button>" +
				  "<select #t (change)='getlangpage(t.value)'>" +
					   "<option *ngFor='let lang of languages' value={{lang}}>{{lang}}</option>" +
				 "</select>" +
				 "<button onclick='#' style='position: absolute; right: 0;'>{{date}}</button>" +  //style='float: right;'
		 "</section>" +
		 "<router-outlet></router-outlet>",
		styles: [".bgimg {background: url('Picture/Bosch_Farbbalken.png')}" +
				 "button {background-color: Transparent;background-repeat:no-repeat;cursor:pointer;overflow: hidden; outline:none; border:none;" +
				 "color: #ffffff; font-size: 180%;margin-left: 0%; margin-right: 0.8%;}"+
				 ".activeclass{ border-style: solid; border-width: 3px;}"]
	})
	   
	.Class({
		constructor: [app.HeroesService, ng.router.Router,function nachbarcomponent(heroesService, router) {
			this.service = heroesService;
			this.router = router;
			this.lastdata = this.service.EmployeeDatalast;
			setInterval(() => {
				var dateall = new Date();
				var dd=dateall.getDate();
				var mm=dateall.getMonth() + 1;
				var yyyy = dateall.getFullYear();
				var dateonly = (dd < 10 ? dd = '0' + dd : dd = dd) + '.' +( mm < 10 ? mm = '0' + mm : mm = mm )+ '.' + yyyy;
				var time = dateall.toLocaleTimeString();
				this.date = dateonly + ' ' + time;
			}, 1000);
		}]
	});

	app.nachbarcomponent.prototype.ngOnInit = function () {
		this.service.getlanguagedata('DE').subscribe(response => this.Assignresponse(response), error => console.log(error));
		this.lang = this.service.lang;
		this.languages = ['DE', 'EN', 'HG'];
	};
	app.nachbarcomponent.prototype.Assignresponse = function (list) {
		this.service.Languagelist = list;
		this.back = this.service.Languagelist[0];
		this.Observer = this.service.Languagelist[1];
		this.Place = this.service.Languagelist[2];
		this.Employee = this.service.Languagelist[3];
		this.Machine = this.service.Languagelist[4];
	};
	app.nachbarcomponent.prototype.goToBack = function () {
		if (this.service.parameter == 'emp') {
			this.service.EmployeeData.pop();
			this.service.EmployeeDatalast = [''];
		}
		else{
		this.service.MachineData.pop();
		this.service.MachineDatalast = [];
		this.service.machinelinecomplete = '';
		}
		window.history.go(-1);
		return false;
	};
   //This function is for Fullscreen View
	app.nachbarcomponent.prototype.toggleFullScreen = function () {
		if ((document.fullScreenElement && document.fullScreenElement !== null) ||
		 (!document.mozFullScreen && !document.webkitIsFullScreen)) {
			if (document.documentElement.requestFullScreen) {
				document.documentElement.requestFullScreen();
			} else if (document.documentElement.mozRequestFullScreen) {
				document.documentElement.mozRequestFullScreen();
			} else if (document.documentElement.webkitRequestFullScreen) {
				document.documentElement.webkitRequestFullScreen(Element.ALLOW_KEYBOARD_INPUT);
			}
		}
		else {
			if (document.cancelFullScreen) {
				document.cancelFullScreen();
			} else if (document.mozCancelFullScreen) {
				document.mozCancelFullScreen();
			} else if (document.webkitCancelFullScreen) {
				document.webkitCancelFullScreen();
			}
		}
	};

	app.nachbarcomponent.prototype.getlangpage = function (lang) {
		this.service.lang = lang;
		this.service.getlanguagedata(lang).subscribe(response => this.Assignresponselist(response), error => console.log(error));  
	};
	app.nachbarcomponent.prototype.Assignresponselist = function (list) {
		this.service.Languagelist = list;
		this.back = this.service.Languagelist[0];
		this.Observer = this.service.Languagelist[1];
		this.Place = this.service.Languagelist[2];
		this.Employee = this.service.Languagelist[3];
		this.Machine = this.service.Languagelist[4];
		if (this.service.lang=='EN') {
			var link = ['/introEN'];
			this.router.navigate(link);
		}
		else if (this.service.lang == 'HG') {
			var link = ['/introHG'];
			this.router.navigate(link);
		}
	   else{
		var link = ['/intro'];
		this.router.navigate(link);
		}
	};
		
})(window.app || (window.app = {}));