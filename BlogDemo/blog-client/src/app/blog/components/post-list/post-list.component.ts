import { Component, OnInit } from '@angular/core';
import { PostService } from '../../services/post.service';
import { postparmeters } from '../../models/post-parameters';
import { ResultWithLinks } from '../../services/models/result-with-links';
import { Post } from '../../models/Post';

@Component({
  selector: 'app-post-list',
  templateUrl: './post-list.component.html',
  styleUrls: ['./post-list.component.scss']
})
export class PostListComponent implements OnInit {
  
  posts:Post[];

  postParameter=new postparmeters({orderBy:"id desc",pageSize:10,pageIndex:0});
  constructor(private postService:PostService) { }

  ngOnInit() {
    this.getPosts()
  }

  getPosts()
  {
    this.postService.getPagedPosts(this.postParameter).subscribe(resp=>{
      console.log(resp.body);        
      let pageMeta = resp.headers.get('X-Pagination');
      console.log(pageMeta ,"我也不知道为什么事空的，反正应该返回的是X-Pagination相关数值");
      const result={...resp.body}as ResultWithLinks<Post>
       console.dir(result);
      this.posts=result.value;
    });
    
  }

}
