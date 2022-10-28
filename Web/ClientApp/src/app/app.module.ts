import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { MapComponent } from './map/map.component'
import { NavigationComponent } from './user/navigation/navigation.component'
import { LectureRoomListComponent } from './user/lecture-room-list/lecture-room-list.component'
import { MapViewComponent } from './user/map-view/map-view.component'

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    MapComponent,
    NavigationComponent,
    LectureRoomListComponent,
    MapViewComponent

  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'university-map', component: MapComponent },
      { path: 'room-navigation', component: NavigationComponent },
      { path: 'lecture-room-list', component: LectureRoomListComponent },
      { path: 'map-view', component: MapViewComponent }
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
