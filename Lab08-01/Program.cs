using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab08_01
{
    class Program
    {
        public static DataClasses1DataContext context = new DataClasses1DataContext();
        static void Main(string[] args)
        {
            Grouping();
            Console.Read();
        }
        static void IntroToLINQ()
        {
            // Thre three Parts of a LINQ Query:
            // 1. Data Sourceint
            int[] numbers = new int[7] { 0, 1, 2, 3, 4, 5, 6 };

            // 2. Query creation.
            // numQuery is an IEnumerable<int>
            var numQuery =
                from num in numbers
                where (num % 2) == 0
                select num;

            // 3. Query execution
            foreach (int num in numQuery)
            {
                Console.Write("{0,1}", num);
            }
        }
        static void DataSource()
        {
            var queryAllCustomers = from cust in context.clientes
                                    select cust;

            foreach (var item in queryAllCustomers)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }
        static void DataSourceLambda()
        {
            var queryAllCustomers = context.clientes.Select(cust => cust).ToList();

            foreach (var item in queryAllCustomers)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }

        //

        static void Filtering()
        {
            var queryLondonCustomers = from cust in context.clientes
                                       where cust.Ciudad == "Londres"
                                       select cust;

            foreach (var item in queryLondonCustomers)
            {
                Console.WriteLine(item.Ciudad);
            }
        }
        static void filteringLambda()
        {
            var customersLondon = context.clientes.Where(x => x.Ciudad == "Londres").ToList();

            foreach (var item in customersLondon)
            {
                Console.WriteLine(item.Ciudad);
            }
        }

        //

        static void Ordering()
        {
            var queryLondonCustomers3 =
                from cust in context.clientes
                where cust.Ciudad == "Londres"
                orderby cust.NombreCompañia ascending
                select cust;

            foreach (var item in queryLondonCustomers3)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }
        static void OrderinLambda()
        {
            var queryLondonCustomers3 = context.clientes.Where(cust => cust.Ciudad 
            == "Londres").OrderBy(x => x.NombreCompañia).ToList();

            foreach (var item in queryLondonCustomers3)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }

        //

        static void Grouping()
        {
            var queryCustomersByCity =
                from cust in context.clientes
                group cust by cust.Ciudad;

            // customerGroup is an IGrouping<string ,Customer>
            foreach (var customerGroup in queryCustomersByCity)
            {
                Console.WriteLine(customerGroup.Key);
                foreach (clientes customer in customerGroup)
                {
                    Console.WriteLine(" {0}", customer.NombreCompañia);
                }
            }
        }
        static void GroupingLambda()
        {
            var queryCustomerByCity = context.clientes.GroupBy(c => c.Ciudad).ToList();

            foreach (var customerGroup in queryCustomerByCity)
            {
                Console.WriteLine(customerGroup.Key);
                foreach (clientes customer in customerGroup)
                {
                    Console.WriteLine("\t{0}", customer.NombreCompañia);
                }
            }
        }

        //

        static void Grouping2()
        {
            var custQuery =
                from cust in context.clientes
                group cust by cust.Ciudad into custGroup
                where custGroup.Count() > 2
                orderby custGroup.Key
                select custGroup;

            foreach (var item in custQuery)
            {
                Console.WriteLine(item.Key);
            }
        }
        static void Grouping2Lambda()
        {
            var custQuery = context.clientes.GroupBy(c => c.Ciudad).Where(x => 
            x.Count() > 2).OrderBy(y => y.Key).ToList();

            foreach (var item in custQuery)
            {
                Console.WriteLine(item.Key);
            }
        }

        //

        static void Joining()
        {
            var innerJoinQuery =
                from cust in context.clientes
                join dist in context.Pedidos on cust.idCliente equals dist.IdCliente
                select new { CustomerName = cust.NombreCompañia, DistributorName = dist.PaisDestinatario };

            foreach (var item in innerJoinQuery)
            {
                Console.WriteLine(item.CustomerName);
            }
        }
        static void JoiningLambda()
        {
            var innerJoinQuery = context.clientes
                 .Join(context.Pedidos,
                     cust => cust.idCliente,
                     dist => dist.IdCliente,
                     (cust, dist) => new { CustomerName = cust.NombreCompañia, 
                         DistributorName = dist.PaisDestinatario })
                 .ToList();

            foreach (var item in innerJoinQuery)
            {
                Console.WriteLine(item.CustomerName);
            }
        }

    }
}
