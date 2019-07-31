import { Component, OnInit, Input } from '@angular/core';
import { Post } from '../../models/Post';

@Component({
  selector: 'app-post-card',
  templateUrl: './post-card.component.html',
  styleUrls: ['./post-card.component.scss']
})
export class PostCardComponent implements OnInit {

  @Input() post: Post;//从外层组件像它传递对象
  
  constructor() { }

  ngOnInit() {
  }

}
