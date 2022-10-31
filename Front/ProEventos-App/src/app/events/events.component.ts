import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { EventService } from '../services/event.service';
import { Event } from "./../models/Event";
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.scss']
})
export class EventsComponent implements OnInit {

  modalRef: BsModalRef;
  public events: Event[] = [];
  public filteredEvents: Event[] = [];

  public widthImage: number = 150;
  public marginImage: number = 2;
  public showImage: boolean = true;
  private _filterListed: string = '';

  public get filterList(): string {
    return this._filterListed;
  }

  public set filterList(value: string) {
    this._filterListed = value;
    this.filteredEvents = this.filterList ? this.filterEvents(this.filterList) : this.events;
  }

  public filterEvents(filterBy: string): Event[] {
    filterBy = filterBy.toLocaleLowerCase();
    return this.events.filter(
      (event: any) => event.theme.toLocaleLowerCase().indexOf(filterBy) !== -1 ||
      event.local.toLocaleLowerCase().indexOf(filterBy) !== -1
    );
  }

  constructor(
    private eventService: EventService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService) { }

  public ngOnInit(): void {
    this.getEvents();
    this.spinner.show();

    setTimeout(() => {
      this.spinner.hide();
    }, 1000)
  }

  public changeImage(): void {
    this.showImage = !this.showImage;
  }

  public getEvents(): void {
    this.eventService.getEvents().subscribe(
      (_events: Event[]) => {
        this.events = _events;
        this.filteredEvents = this.events;
      },
      error => console.log(error),
    );
  }

  openModal(template: TemplateRef<any>): void {
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(): void {
    this.modalRef.hide();
    this.toastr.success('Event deleted with success', 'Deleted!')
  }

  decline(): void {
    this.modalRef.hide();
  }
}
