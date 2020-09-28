import { IErroArquivo } from './../shared/IErroArquivo';
import { IArquivoExcel } from './../shared/IArquivoExcel';
import { HomeService } from './home.service';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {
  arquivosExcel: IArquivoExcel[] = [];
  arquivo: File = null;

  titulo = '';
  mensagem = '';
  tipoMensagem = '';
  exibirMensagem = true;

  formAnexo: FormGroup;

  constructor(private homeService: HomeService, private router: Router) {}

  ngOnInit(): void {
    this.CarregaArquivosExcel();
    this.formAnexo = new FormGroup({
      anexo: new FormControl('', Validators.required),
    });
    this.onSetAlert(false);
  }

  onInsereArquivo(arquivos: FileList): void {
    this.arquivo = arquivos.item(0);

    this.homeService.Insert(this.arquivo).subscribe(
      (retornoApi) => {
        this.onSetAlert(
          true,
          'Sucesso!',
          'O arquivo excel foi carregado e processado com sucesso.',
          'success'
        );
        // Se der tudo certo, redireciona o usuário para a página import/
        this.router.navigateByUrl('/import/' + retornoApi[0].idArquivoExcel);
      },
      (error) => {
        // Monta a lista com os erros e mostra para o usuário.
        let erroFormatado = '';

        error.error.forEach((element) => {
          erroFormatado +=
            '\n Linha: ' +
            element.linha +
            ' Coluna: ' +
            element.coluna +
            ' Erro: ' +
            element.erro;
        });

        this.onSetAlert(
          true,
          'Erro!',
          'O arquivo excel NÃO foi carregado, forma encontrados os seguintes erros:' +
            erroFormatado,
          'danger'
        );
        console.log(error.error);
      }
    );
  }

  // Busca no endpoint a informação dos arquivos excel já importados.
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

  onSetAlert(
    exibirMensagem: boolean,
    titulo: string = '',
    mensagem: string = '',
    tipoMensagem: string = ''
  ): void {
    this.exibirMensagem = exibirMensagem;
    this.titulo = titulo;
    this.mensagem = mensagem;
    this.tipoMensagem = tipoMensagem;
  }
}
