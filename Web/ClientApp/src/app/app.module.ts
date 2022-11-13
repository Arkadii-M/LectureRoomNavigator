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
import { LectureRoomAddComponent } from './admin/lecture-rooms/lecture-room-add.component';
import { NavigationNodeAddComponent } from './admin/navigation-nodes/navigation-node-add.component';
import { NavigationEdgeComponent } from './admin/navigation-edges/navigation-edge-add.component';
import { AuthenticationGuard } from './guards/auth.guard';
import { AuthService } from './services/auth.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    MapComponent,
    NavigationComponent,
    LectureRoomListComponent,
    MapViewComponent,
    LectureRoomAddComponent,
    NavigationNodeAddComponent,
    NavigationEdgeComponent

  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      // User pathes
/*      { path: 'university-map', component: MapComponent },*/
      { path: 'room-navigation', component: NavigationComponent },
      { path: 'lecture-room-list', component: LectureRoomListComponent },
      { path: 'map-view', component: MapViewComponent },
      // Admin pathes
      { path: 'lecture-room-add', component: LectureRoomAddComponent, canActivate: [AuthenticationGuard] },
      { path: 'navigation-node-add', component: NavigationNodeAddComponent, canActivate: [AuthenticationGuard] },
      { path: 'navigation-edge-add', component: NavigationEdgeComponent, canActivate: [AuthenticationGuard] }
    ])
  ],
  providers: [AuthenticationGuard, AuthService],
  bootstrap: [AppComponent]
})
export class AppModule { }
