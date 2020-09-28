import { IErroArquivo } from './IErroArquivo';
import { ILinhaArquivoExcel } from './ILinhaArquivoExcel';

export class Retorno {
  statusCode: string;
  linhaArquivoExcel: ILinhaArquivoExcel[] = [];
  errosArquivo: IErroArquivo[] = [];
}
