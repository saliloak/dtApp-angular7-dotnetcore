import { Routes } from '@angular/router';
import { HomeComponent } from "./home/home.component";
import { MemberListComponent } from "./member-list/member-list.component";
import { MessagesComponent } from "./messages/messages.component";
import { ListsComponent } from "./lists/lists.component";
import { AuthGuard } from './_guards/auth.guard';

//Both route methods can be used

//using conventional routes
//export const appRoutes: Routes = [
//  { path: 'home', component: HomeComponent },
//  { path: 'members', component: MemberListComponent, canActivate: [AuthGuard] },
//  { path: 'messages', component: MessagesComponent },
//  { path: 'lists', component: ListsComponent },
//  { path: '**', redirectTo: 'home', pathMatch: 'full' }
//];

//using dummy routes
export const appRoutes: Routes = [
  { path: '', component: HomeComponent },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      { path: 'members', component: MemberListComponent },
      { path: 'messages', component: MessagesComponent },
      { path: 'lists', component: ListsComponent }
    ]
  },
  { path: '**', redirectTo: '', pathMatch: 'full' }
];
