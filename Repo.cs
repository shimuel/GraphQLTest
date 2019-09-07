using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphQLTest
{
    public static class Repo
    {
        public static PaginatedGraphType<T> ToPaginateGraphTypeAsync<T>(
            IList<T> source, 
            int index, 
            int size,
            int from = 0)
        {
            if (from > index) throw new ArgumentException($"From: {from} > Index: {index}, must From <= Index");

            var count = source.Count();
            var items = source.Skip((index - from) * size).Take(size).ToList();
            
            var list = new PaginatedGraphType<T>
            {
                Index = index,
                Size = size,
                From = from,
                Count = count,
                Items = items,
                Pages = (int)Math.Ceiling(count / (double)size)
            };

            return list;
        }

        public static IList<Order> GetData(){

            var orders = new  List<Order>();

            orders.Add(new Order{
                    OrderId = 1,
                    OrderDate = DateTime.Now.ToString(),
                    OrderDetails = new List<OrderDetail> {
                        new OrderDetail{OrderId=1,ProductId=1, Quantity=2, UnitPrice="$3", Discount=1},
                        new OrderDetail{OrderId=1,ProductId=2, Quantity=1, UnitPrice="$1.5", Discount=0.5},
                    }
            });

            orders.Add(new Order{
                    OrderId = 2,
                    OrderDate = DateTime.Now.ToString(),
                    OrderDetails = new List<OrderDetail> {
                        new OrderDetail{OrderId=2,ProductId=1, Quantity=2, UnitPrice="$4", Discount=1},
                        new OrderDetail{OrderId=2,ProductId=2, Quantity=1, UnitPrice="$1.5", Discount=0.5},
                    }
            });
            
            orders.Add(new Order{
                    OrderId = 1,
                    OrderDate = DateTime.Now.ToString(),
                    OrderDetails = new List<OrderDetail> {
                        new OrderDetail{OrderId=1,ProductId=1, Quantity=2, UnitPrice="$4", Discount=1},
                        new OrderDetail{OrderId=1,ProductId=2, Quantity=1, UnitPrice="$1.5", Discount=0.5},
                    }
            });

            orders.Add(new Order{
                    OrderId = 2,
                    OrderDate = DateTime.Now.ToString(),
                    OrderDetails = new List<OrderDetail> {
                        new OrderDetail{OrderId=2,ProductId=1, Quantity=2, UnitPrice="$5", Discount=1},
                        new OrderDetail{OrderId=2,ProductId=2, Quantity=1, UnitPrice="$1.5", Discount=0.5},
                    }
            });
            
            orders.Add(new Order{
                    OrderId = 2,
                    OrderDate = DateTime.Now.ToString(),
                    OrderDetails = new List<OrderDetail> {
                        new OrderDetail{OrderId=3,ProductId=1, Quantity=2, UnitPrice="$6", Discount=1},
                        new OrderDetail{OrderId=3,ProductId=2, Quantity=1, UnitPrice="$1.5", Discount=0.5},
                    }
            });

            orders.Add(new Order{
                    OrderId = 2,
                    OrderDate = DateTime.Now.ToString(),
                    OrderDetails = new List<OrderDetail> {
                        new OrderDetail{OrderId=4,ProductId=1, Quantity=2, UnitPrice="$7", Discount=1},
                        new OrderDetail{OrderId=4,ProductId=2, Quantity=1, UnitPrice="$1.5", Discount=0.5},
                    }
            });
            
            orders.Add(new Order{
                    OrderId = 1,
                    OrderDate = DateTime.Now.ToString(),
                    OrderDetails = new List<OrderDetail> {
                        new OrderDetail{OrderId=5,ProductId=1, Quantity=2, UnitPrice="$8", Discount=1},
                        new OrderDetail{OrderId=5,ProductId=2, Quantity=1, UnitPrice="$1.5", Discount=0.5},
                    }
            });

            orders.Add(new Order{
                    OrderId = 2,
                    OrderDate = DateTime.Now.ToString(),
                    OrderDetails = new List<OrderDetail> {
                        new OrderDetail{OrderId=6,ProductId=1, Quantity=2, UnitPrice="$3", Discount=1},
                        new OrderDetail{OrderId=6,ProductId=2, Quantity=1, UnitPrice="$1.5", Discount=0.5},
                    }
            });
            
            return orders;
        }

    }

    public class Order
    {
        public IList<OrderDetail> OrderDetails = null;
        public Order()
        {
            OrderDetails = new  List<OrderDetail>();
        }

        public long OrderId { get; set; }
        public string OrderDate { get; set; }
    }

    public class OrderDetail
    {
        public long OrderId { get; set; }
        public long ProductId { get; set; }
        public string UnitPrice { get; set; }
        public long Quantity { get; set; }
        public double Discount { get; set; }
    }

}