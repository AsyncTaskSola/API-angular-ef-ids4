import { Injectable } from '@angular/core';
import { BaseService } from 'src/app/shared/base.service';
import { HttpHeaders } from '@angular/common/http';
import { HttpClient } from '@angular/common/http';
import { postparmeters } from '../models/post-parameters';
import { Post } from '../models/Post';

@Injectable({
  providedIn: 'root'
})

export class PostService extends BaseService {
  
  constructor(private http:HttpClient) {
    super();
  }
  
  getPagedPosts(postParameter?: any|postparmeters) {
    return this.http.get(`${this.apiUrlBase}/posts`, {
      headers: new HttpHeaders({
        'Accept': 'application/vnd.cgzl.hateoas+json',
      }),
      observe: 'response',
      params: postParameter
    });
  }
}
