import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})
export abstract class BaseService {

  apiUrlBase=environment.apiUrlBase;

  constructor() { }
}
