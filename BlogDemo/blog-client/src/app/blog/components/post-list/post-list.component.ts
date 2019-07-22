import { Component, OnInit } from '@angular/core';
import { PostService } from '../../services/post.service';
import { postparmeters } from '../../models/post-parameters';

@Component({
  selector: 'app-post-list',
  templateUrl: './post-list.component.html',
  styleUrls: ['./post-list.component.scss']
})
export class PostListComponent implements OnInit {

  postParameter=new postparmeters({orderBy:"id desc",pageSize:10,pageIndex:0});
  constructor(private postService:PostService) { }

  ngOnInit() {
    this.getPosts()
  }

  getPosts()
  {
    this.postService.getPagedPosts(this.postParameter).subscribe(resp=>{
      console.log(resp);
    });
  }

}
