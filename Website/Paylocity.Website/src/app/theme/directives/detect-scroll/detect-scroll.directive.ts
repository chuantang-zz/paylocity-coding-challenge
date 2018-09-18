import { Directive, HostListener, Output, EventEmitter} from '@angular/core';

// tslint:disable-next-line:interface-over-type-literal
export type ScrollEvent =
{
	originalEvent: Event,
	isWindowEvent: boolean,
	scrollTop: number
};

@Directive
(
	{
		// tslint:disable-next-line:directive-selector
		selector: '[detectScroll]'
	}
)

export class DetectScrollDirective
{
	// tslint:disable-next-line:no-output-on-prefix
	@Output() onScroll = new EventEmitter<ScrollEvent>();

	@HostListener('scroll', ['$event']) elementScrolled(event)
	{
		const scrollTop = event.target.scrollTop;
		const emitValue: ScrollEvent = { originalEvent: event, isWindowEvent: false, scrollTop};
		this.onScroll.emit(emitValue);
	}

	@HostListener('window:scroll', ['$event']) windowScrolled(event)
	{
		const scrollTop = window.pageYOffset || document.documentElement.scrollTop || document.body.scrollTop || 0;
		const emitValue: ScrollEvent = { originalEvent: event, isWindowEvent: true, scrollTop };
		this.onScroll.emit(emitValue);
	}
}
