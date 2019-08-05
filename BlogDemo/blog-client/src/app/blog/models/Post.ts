import { Entity } from '../services/models/entity';

export class Post extends Entity{
    title:string;
    Boby:string;
    author:string;
    upDateTime:Date;
    remake?:string;
}