import { ILinhaArquivoExcel } from './../shared/ILinhaArquivoExcel';
import { ImportService } from './import.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-import',
  templateUrl: './import.component.html',
  styleUrls: ['./import.component.scss'],
})
export class ImportComponent implements OnInit {
  linhasArquivoExcel: ILinhaArquivoExcel[] = [];

  constructor(
    private route: ActivatedRoute,
    private importService: ImportService
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');

    this.importService.GetImportById(+id).subscribe(
      (data) => {
        this.linhasArquivoExcel = data;
      },
      (error) => {
        console.log(error);
      }
    );
  }
}
