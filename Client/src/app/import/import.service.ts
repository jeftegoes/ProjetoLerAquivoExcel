import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { ILinhaArquivoExcel } from './../shared/ILinhaArquivoExcel';
import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ImportService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  GetImportById(id: number): Observable<ILinhaArquivoExcel[]> {
    return this.http.get<ILinhaArquivoExcel[]>(
      this.baseUrl + 'LinhaArquivoExcel/' + id
    );
  }
}
