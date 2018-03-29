import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'people',
    templateUrl: './people.component.html'
})
export class PeopleComponent {
    public people: People[];
    public petType: string;
    public petResult: PetResult[];
    public baseUrl: string
    public http: Http

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        this.petType = 'cat';
        this.baseUrl = baseUrl
        this.http = http;
        this.loadPets();
    }

    loadPets() {
        
        this.http.get(this.baseUrl + 'api/people/GetPetsByOwnerGender?petType=' + this.petType).subscribe(result => {
            this.petResult = result.json() as PetResult[];
        }, error => console.error(error));
    }
}

interface PetResult {
    gender: string;    
    pets: Pet[];
}

interface People {
    name: string;
    gender: number;
    age: number;
    pets: Pet[];
}

interface Pet {
    name: string;
    type: string;
}
