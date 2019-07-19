import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BlogAppComponent } from './blog-app.component';

describe('BlogAppComponent', () => {
  let component: BlogAppComponent;
  let fixture: ComponentFixture<BlogAppComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BlogAppComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BlogAppComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
