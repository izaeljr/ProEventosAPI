import { Event } from "./Event";

export interface Lote {
  id: number;
  name: string;
  price: number;
  initialDate?: Date;
  endDate?: Date;
  eventId: number;
  event: Event;
}
