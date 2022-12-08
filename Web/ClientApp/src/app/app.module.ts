import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { MapComponent } from './map/map.component'
import { NavigationComponent } from './user/navigation/navigation.component'
import { LectureRoomListComponent } from './user/lecture-room-list/lecture-room-list.component'
import { MapViewComponent } from './user/map-view/map-view.component'
import { LectureRoomAddComponent } from './admin/crud/lecture-rooms/lecture-room-add.component';
import { NavigationNodeAddComponent } from './admin/crud/navigation-nodes/navigation-node-add.component';
import { NavigationEdgeComponent } from './admin/crud/navigation-edges/navigation-edge-add.component';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component'
import { LectureRoomEditComponent } from './admin/crud/lecture-rooms/lecture-room-edit.component'
import { NavigationNodeEditComponent } from './admin/crud/navigation-nodes/navigation-node-edit.component'
import { AuthenticationGuard } from './guards/auth.guard';
import { AuthService } from './services/auth.service';
import { LoginComponent } from './user/login/login.component'
import { RegisterComponent } from './user/register/register.component';
import { UsersCRUDComponent } from './admin/crud/users/users.crud.component';
import { RolesCRUDComponent } from './admin/crud/roles/roles.crud.component';
import { FacultiesCRUDComponent } from './admin/crud/faculties/faculties.crud.component';


import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { DropdownModule } from 'primeng/dropdown';
import { TableModule } from 'primeng/table';
import { ToastModule } from 'primeng/toast';
import { CalendarModule } from 'primeng/calendar';
import { SliderModule } from 'primeng/slider';
import { MultiSelectModule } from 'primeng/multiselect';
import { ContextMenuModule } from 'primeng/contextmenu';
import { DialogModule } from 'primeng/dialog';
import { ButtonModule } from 'primeng/button';
import { ProgressBarModule } from 'primeng/progressbar';
import { InputTextModule } from 'primeng/inputtext';
import { ToolbarModule } from 'primeng/toolbar';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationService } from 'primeng/api';




import { JwtHelperService } from '@auth0/angular-jwt';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    MapComponent,
    // User components
    NavigationComponent,
    LectureRoomListComponent,
    MapViewComponent,
    LoginComponent,
    RegisterComponent,
    // Admin components
    AdminPanelComponent,
    //
    LectureRoomAddComponent,
    LectureRoomEditComponent,
    //
    NavigationNodeAddComponent,
    NavigationNodeEditComponent,
    //
    NavigationEdgeComponent,
    //
    UsersCRUDComponent,
    RolesCRUDComponent,
    FacultiesCRUDComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    DropdownModule,
    TableModule,
    ToastModule,
    CalendarModule,
    SliderModule,
    MultiSelectModule,
    ContextMenuModule,
    DialogModule,
    ButtonModule,
    ProgressBarModule,
    InputTextModule,
    ToolbarModule,
    ConfirmDialogModule,

    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      // User pathes
/*      { path: 'university-map', component: MapComponent },*/
      { path: 'room-navigation', component: NavigationComponent },
      { path: 'lecture-room-list', component: LectureRoomListComponent },
      { path: 'map-view', component: MapViewComponent },
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent },
      {
        path: 'admin-panel', component: AdminPanelComponent, canActivate: [AuthenticationGuard],
        children: [
          { path: 'lecture-room-add', component: LectureRoomAddComponent, },
          { path: 'lecture-room-edit', component: LectureRoomEditComponent, },

          { path: 'navigation-node-add', component: NavigationNodeAddComponent, },
          { path: 'navigation-node-edit', component: NavigationNodeEditComponent, },

          { path: 'navigation-edge-add', component: NavigationEdgeComponent, },

          { path: 'faculties-crud', component: FacultiesCRUDComponent, },
          { path: 'users-crud', component: UsersCRUDComponent, },
          { path: 'roles-crud', component: RolesCRUDComponent, },


        ]
      },
    ]),
  ],
  providers: [AuthenticationGuard, AuthService, JwtHelperService],
  bootstrap: [AppComponent]
})
export class AppModule { }
