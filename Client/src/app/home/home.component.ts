import { IArquivoExcel } from './../shared/IArquivoExcel';
import { HomeService } from './home.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {
  arquivosExcel: IArquivoExcel[] = [];

  constructor(private homeService: HomeService) {}

  ngOnInit(): void {
    this.CarregaArquivosExcel();
  }

  onInsereArquivo(): void {}

  CarregaArquivosExcel(): void {
    this.homeService.GetAllImports().subscribe(
      (data) => {
        this.arquivosExcel = data;
      },
      (erros) => {
        console.log('Erro');
      }
    );
  }
}
