using System;
using Newtonsoft.Json.Linq;
using GraphQL;
using GraphQL.Types;
//using GraphQL.Authorization;

namespace GraphQLTest
{
    public class NorthwindSchema : GraphQL.Types.Schema
    {
            public NorthwindSchema(IServiceProvider serviceProvider): base(serviceProvider)
            {
                  Query = (NorthwindQuery)serviceProvider.GetService(typeof(NorthwindQuery));
            }
    }

    public class NorthwindQuery : ObjectGraphType
    {
        public NorthwindQuery()
        {
            Field<PageType<PaginatedGraphType<OrderType>,OrderType>>(
                "Orders",
                arguments:  new QueryArguments(
                        new QueryArgument<IntGraphType>{ Name = "index" },
                        new QueryArgument<IntGraphType>{ Name = "size" }
                ),
                resolve: context => {
                        var index = context.GetArgument<int>("index"); 
                        var size = context.GetArgument<int>("size"); 

                        var data = Repo.GetData();
                        var page = Repo.ToPaginateGraphTypeAsync(data, index, size, 0);
    
                        return page.Items;
                }); 
        }
    }

   
   public class PageType<TSource, TGraphType>: ObjectGraphType<PaginatedGraphType<TSource>> where TGraphType: IObjectGraphType
    {
        public PageType()
        {
            Name = string.Concat("Page",typeof(IdGraphType).Name);
            Field(x => x.Size);
            Field(x => x.Index);
            Field(x => x.Count);
            Field(x => x.HasPrevious);
            Field(x => x.HasNext);

            Field<ListGraphType<TGraphType>>("Items", resolve: context => 
                {
                    return context.Source.Items;
                }
            );
        }
    }

    public class OrderType : ObjectGraphType<Order>
    {
        public OrderType()
        {
            Field(x => x.OrderId,false);
            Field(x => x.OrderDate,true);
            Field(x => x.OrderDetails,true, type: typeof(ListGraphType<OrderDetailType>)).Description("Order Details");            
        }
    }

    public class OrderDetailType : ObjectGraphType<OrderDetail>
    {
        public OrderDetailType()
        {           
            Field(x => x.OrderId,false);
            Field(x => x.ProductId,false);            
            Field(x => x.UnitPrice,true);
            Field(x => x.Quantity,true);
            Field(x => x.Discount,true);
        }
    }


    public class GraphQLQuery
    {
        public string OperationName { get; set; }
        public string NamedQuery { get; set; }
        public string Query { get; set; }
        public JObject Variables { get; set; } //https://github.com/graphql-dotnet/graphql-dotnet/issues/389
    }
}