import { Component, OnInit, ViewChild, HostListener, AfterViewInit } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { AppSettings } from '../app.settings';
import { Settings } from '../app.settings.model';
import { MenuService } from '../theme/components/menu/menu.service';

@Component
(
	{
		selector: 'app-pages',
		templateUrl: './pages.component.html',
		styleUrls: ['./pages.component.scss'],
		providers: [ MenuService ]
	}
)

export class PagesComponent implements OnInit, AfterViewInit
{
	@ViewChild('sidenav') sidenav: any;
	public settings: Settings;
	public menus = ['vertical', 'horizontal'];
	public menuOption: string;
	public menuTypes = ['default', 'compact', 'mini'];
	public menuTypeOption: string;
	public isStickyMenu: boolean = false;
	public lastScrollTop: number = 0;
	public showBackToTop: boolean = false;
	public toggleSearchBar: boolean = false;
	private defaultMenu: string; // declared for return default menu when window resized
	public scrolledContent: any;

	constructor(public appSettings: AppSettings, public router: Router, private menuService: MenuService) { this.settings = this.appSettings.settings; }

	ngOnInit()
	{
		if (window.innerWidth <= 768)
		{
			this.settings.menu = 'vertical';
			this.settings.sidenavIsOpened = false;
			this.settings.sidenavIsPinned = false;
		}

		this.menuOption = this.settings.menu;
		this.menuTypeOption = this.settings.menuType;
		this.defaultMenu = this.settings.menu;
	}

	ngAfterViewInit()
	{
		setTimeout(() => { this.settings.loadingSpinner = false; }, 300);
		this.router.events.subscribe
		(
			event =>
			{
				if (event instanceof NavigationEnd)
				{
					if (!this.settings.sidenavIsPinned) { this.sidenav.close(); }

					if (window.innerWidth <= 768) { this.sidenav.close(); }
				}
			}
		);

		const menu = this.menuService.getVerticalMenuItems();
		if (this.settings.menu === 'vertical') this.menuService.expandActiveSubMenu(menu);
	}

	public toggleSidenav() { this.sidenav.toggle(); }

	public onPsScrollY(event)
	{
		this.scrolledContent = event.target;
		(this.scrolledContent.scrollTop > 300) ? this.showBackToTop = true : this.showBackToTop = false;
		if (this.settings.menu === 'horizontal')
		{
			if (this.settings.fixedHeader)
			{
				const currentScrollTop = (this.scrolledContent.scrollTop > 56) ? this.scrolledContent.scrollTop : 0;
				(currentScrollTop > this.lastScrollTop) ? this.isStickyMenu = true : this.isStickyMenu = false;
				this.lastScrollTop = currentScrollTop;
			}
			else { (this.scrolledContent.scrollTop > 56) ? this.isStickyMenu = true : this.isStickyMenu = false; }
		}
	}

	public scrollToTop()
	{
		const scrollDuration = 200;
		const scrollStep = -this.scrolledContent.scrollTop / (scrollDuration / 20);
		const scrollInterval = setInterval
		(
			() =>
			{
				if (this.scrolledContent.scrollTop !== 0) { this.scrolledContent.scrollBy(0, scrollStep); }
				else { clearInterval(scrollInterval); }

			}
			, 10
		);

		if (window.innerWidth <= 768) { this.scrolledContent.scrollTop = 0; }
	}

	@HostListener('window:resize')
	public onWindowResize(): void
	{
		if (window.innerWidth <= 768)
		{
			this.settings.sidenavIsOpened = false;
			this.settings.sidenavIsPinned = false;
			this.settings.menu = 'vertical';
		}
		else
		{
			(this.defaultMenu === 'horizontal') ? this.settings.menu = 'horizontal' : this.settings.menu = 'vertical';
			this.settings.sidenavIsOpened = true;
			this.settings.sidenavIsPinned = true;
		}
	}

	public closeSubMenus()
	{
		const menu = document.querySelector('.sidenav-menu-outer');
		if (menu)
		{
			for (let i = 0; i < menu.children[0].children.length; i++)
			{
				const child = menu.children[0].children[i];
				if (child)
				{
					if (child.children[0].classList.contains('expanded'))
					{
						child.children[0].classList.remove('expanded');
						child.children[1].classList.remove('show');
					}
				}
			}
		}
	}
}
