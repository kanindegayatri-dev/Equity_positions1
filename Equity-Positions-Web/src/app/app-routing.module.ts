import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PositionsComponent } from './components/positions/positions.component';
import { AddTransactionComponent } from './components/add-transaction/add-transaction.component';

const routes: Routes = [
  {
    path: '',redirectTo: 'positions', pathMatch: 'full'
  },
  {
    path: 'positions', component: PositionsComponent
  },
  {
    path: 'add-transaction', component: AddTransactionComponent
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
