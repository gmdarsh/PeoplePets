import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { PeopleComponent } from './components/people/people.component'

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,       
        PeopleComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'people', component: PeopleComponent },           
            { path: '**', redirectTo: 'home' }
        ])
    ]
})
export class AppModuleShared {
}
