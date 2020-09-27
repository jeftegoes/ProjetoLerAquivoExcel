import { environment } from './../../environments/environment';
import { IArquivoExcel } from './../shared/IArquivoExcel';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class HomeService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  GetAllImports(): Observable<IArquivoExcel[]> {
    return this.http.get<IArquivoExcel[]>(this.baseUrl + 'Api/ArquivoExcel');
  }
}
