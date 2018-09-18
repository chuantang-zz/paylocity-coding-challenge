import { Component, OnInit, AfterViewChecked, ElementRef, ViewChild } from '@angular/core';
import { analytics } from '../dashboard.data';

@Component
(
	{
		selector: 'app-analytics',
		templateUrl: './analytics.component.html'
	}
)

export class AnalyticsComponent implements OnInit, AfterViewChecked
{
	public analytics: any[];
	public showXAxis = true;
	public showYAxis = true;
	public gradient = false;
	public showLegend = false;
	public showXAxisLabel = false;
	public xAxisLabel = 'Year';
	public showYAxisLabel = false;
	public yAxisLabel = 'Profit';
	public colorScheme = { domain: ['#283593', '#039BE5', '#FF5252'] };
	public autoScale = true;
	public roundDomains = true;
	public previousWidthOfResizedDiv: number = 0;
	@ViewChild('resizedDiv') resizedDiv: ElementRef;

	constructor() { }

	ngOnInit() { this.analytics = analytics; }

	onSelect(event) { console.log(event); }

	ngAfterViewChecked()
	{
		if (this.previousWidthOfResizedDiv !== this.resizedDiv.nativeElement.clientWidth) { this.analytics = [...analytics]; }

		this.previousWidthOfResizedDiv = this.resizedDiv.nativeElement.clientWidth;
	}
}
