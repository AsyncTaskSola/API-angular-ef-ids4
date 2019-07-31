import { Entity } from '../services/models/entity';

export class Post extends Entity{
    title:string;
    body:string;
    author:string;
    upDateTime:Date;
    remake?:string;
}