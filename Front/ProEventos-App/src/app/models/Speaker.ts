import { SocialMedia } from "./SocialMedia";

export interface Speaker {
  id: number;
  name: string;
  miniCV: string;
  imageURL: string;
  phone: string;
  email: string;
  eventSpeakers: Event[];
  socialMedias: SocialMedia[];
}

