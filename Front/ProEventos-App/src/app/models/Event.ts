import { Lote } from "./Lote";
import { Speaker } from "./Speaker";
import { SocialMedia } from "./SocialMedia";

export interface Event {
  id: number;
  local: string;
  eventDate?: Date;
  theme: string;
  qtdPeople: number;
  imageURL: string;
  telephone: string;
  email: string;
  lotes: Lote[];
  socialMedias: SocialMedia[];
  eventsSpeakers: Speaker[];
}
