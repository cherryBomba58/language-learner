import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { MdToolbarModule, MdRadioModule, MdButtonModule, MdIconModule, MdInputModule, MdCardModule, MdCheckboxModule } from '@angular/material';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { QuestionsComponent } from './components/questions/questions.component';
import { TestComponent } from './components/test/test.component';
import { LoginComponent } from './components/login/login.component';
import { RegistComponent } from './components/regist/regist.component';
import { TaskComponent } from './components/task/task.component';
import { ResultsComponent } from './components/results/results.component';

import { CourseService } from './services/course.service';
import { SharedDataService } from "./services/shared-data.service";
import { LearnableService } from "./services/learnable.service";
import { AccountService } from "./services/account.service";
import { ResultsService } from "./services/results.service";

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        QuestionsComponent,
        TestComponent,
        LoginComponent,
        RegistComponent,
        TaskComponent,
        ResultsComponent,
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
            { path: 'regist', component: RegistComponent },
            { path: 'task/:courseid', component: TaskComponent },
            { path: 'results/:courseid', component: ResultsComponent },
            { path: '**', redirectTo: 'home' }
        ]),
        MdToolbarModule,
        MdRadioModule,
        MdButtonModule,
        MdIconModule,
        MdInputModule,
        MdCardModule,
        MdCheckboxModule
    ],
    providers: [
        CourseService,
        SharedDataService,
        LearnableService,
        AccountService,
        ResultsService
    ]
})
export class AppModuleShared {
}
