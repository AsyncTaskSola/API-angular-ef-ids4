# API-angular-ef-ids4

BlogDemo 更新于2019 6.27 主要添加了设置环境的方法，ef用的是sqllite,这个不支持字段的改变，那样会导致迁移报错，用了仓储模式，做了个post添加字段的功能，Repository要结合Unit of Work一起用的。删除，增加，排序Repository 可能有多个，Unit of Work单独抽出来，这样不违反高级受低级模块控制，面对接口，对象
