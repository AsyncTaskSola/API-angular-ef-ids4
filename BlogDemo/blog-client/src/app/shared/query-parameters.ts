export abstract class QueryParameters {

    pageIndex?:number;/// 显示第几页
    pageSize?:number;/// 每页有多少条数据
    fields?:string;
    orderBy?:string; /// 排序
    
    constructor(init?:Partial<QueryParameters>) {
        Object.assign(this,init)

    // let s=new QueryParameters(fields:'dsf')
    }
}