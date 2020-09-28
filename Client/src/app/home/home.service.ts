import { Retorno } from './../shared/IRetorno';
import { environment } from './../../environments/environment';
import { IArquivoExcel } from './../shared/IArquivoExcel';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class HomeService {
  baseUrl = environment.apiUrl;
  url = 'ArquivoExcel';

  constructor(private http: HttpClient) {}

  GetAllImports(): Observable<IArquivoExcel[]> {
    return this.http.get<IArquivoExcel[]>(this.baseUrl + this.url);
  }

  Insert(arquivo: File): Observable<Retorno> {
    let formData = new FormData();

    formData.append('arquivo', arquivo);

    return this.http.post(this.baseUrl + this.url, formData).pipe(
      map((retorno: Retorno) => {
        if (retorno) {
          return retorno;
        }
      })
    );
  }
}
