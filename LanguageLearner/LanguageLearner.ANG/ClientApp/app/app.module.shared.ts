import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { QuestionsComponent } from './components/questions/questions.component';
import { TestComponent } from './components/test/test.component';
import { LoginComponent } from './components/login/login.component';

import { CourseService } from './services/course.service';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        QuestionsComponent,
        TestComponent,
        LoginComponent,
        HomeComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'questions', component: QuestionsComponent },
            { path: 'test', component: TestComponent },
            { path: 'login', component: LoginComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [
        CourseService
    ]
})
export class AppModuleShared {
}
