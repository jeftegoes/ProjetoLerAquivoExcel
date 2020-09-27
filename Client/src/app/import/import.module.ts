import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ImportRoutingModule } from './import-routing.module';
import { ImportComponent } from './import.component';


@NgModule({
  declarations: [ImportComponent],
  imports: [
    CommonModule,
    ImportRoutingModule
  ]
})
export class ImportModule { }
