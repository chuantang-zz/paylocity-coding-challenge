import { Component, OnInit, AfterViewChecked, ElementRef, ViewChild } from '@angular/core';
import { disk_space } from '../dashboard.data';

@Component
(
	{
		selector: 'app-disk-space',
		templateUrl: './disk-space.component.html'
	}
)

export class DiskSpaceComponent implements OnInit, AfterViewChecked
{
	public data: any[];
	public showLegend = false;
	public gradient = true;
	public colorScheme = { domain: ['#2F3E9E', '#D22E2E', '#378D3B'] };
	public showLabels = true;
	public explodeSlices = true;
	public doughnut = false;
	public previousWidthOfResizedDiv: number = 0;
	@ViewChild('resizedDiv') resizedDiv: ElementRef;

	constructor() { }

	ngOnInit() { this.data = disk_space; }

	public onSelect(event) { console.log(event); }

	ngAfterViewChecked()
	{
		if (this.previousWidthOfResizedDiv !== this.resizedDiv.nativeElement.clientWidth) { setTimeout(() => this.data = [...disk_space]); }

		this.previousWidthOfResizedDiv = this.resizedDiv.nativeElement.clientWidth;
	}
}
