import { NgModule } from '@angular/core';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import {MatTableModule} from '@angular/material/table';
import {MatSortModule} from '@angular/material/sort';
import {MatPaginatorModule} from '@angular/material/paginator';
import {MatInputModule} from '@angular/material/input';
import{MatProgressBarModule} from '@angular/material/progress-bar';
import {MatDialogModule} from '@angular/material/dialog';

import{FormsModule,ReactiveFormsModule}  from '@angular/forms';



@NgModule({
    imports:[
      FormsModule,
      ReactiveFormsModule,
      MatToolbarModule,
      MatIconModule,
      MatButtonModule,
      MatTableModule,
      MatSortModule,
      MatPaginatorModule,
      MatInputModule,
    MatProgressBarModule,
  MatDialogModule],

    exports:[
      FormsModule,
      ReactiveFormsModule,
      MatToolbarModule,
      MatIconModule,
      MatButtonModule ,
      MatTableModule,
      MatSortModule,
      MatPaginatorModule,
      MatInputModule,
      MatProgressBarModule,
    MatDialogModule],
})
export class AppModule
{

}
