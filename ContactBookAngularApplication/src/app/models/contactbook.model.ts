import { ContactCountry } from "./contact.country.model";
import { ContactState } from "./contact.state.model";

export interface Contactbook{
    contactId : number;
    name : string;
    email : string;
    phoneNumber : string;
    company : string;
    fileName : string;
    file:string;
    birthDate:string;
    gender : string;
    favourite : boolean;
    stateId : number | null;
    countryId : number | null;
    state:ContactState;
    country:ContactCountry;
}