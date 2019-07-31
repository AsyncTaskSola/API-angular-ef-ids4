import { QueryParameters } from 'src/app/shared/query-parameters';

export class postparmeters extends QueryParameters
{
    title?:string;

    constructor(init?:Partial<postparmeters>) {
        super(init);
        Object.assign(this, init);
        
    }
}