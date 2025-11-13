using Microsoft.EntityFrameworkCore;
using TigerMarleyAdmin.Models;

namespace TigerMarleyAdmin.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        // ✅ DbSets
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<InventoryItem> InventoryItems => Set<InventoryItem>();
        public DbSet<SupportTicket> SupportTickets => Set<SupportTicket>();

        // ---------------- PostgreSQL function for Orders ----------------
        public async Task<List<Order>> GetOrdersFromFunctionAsync(int orderId = -1)
        {
            var sql = @"
                SELECT 
                    id,
                    customer_name AS CustomerName,
                    product_name AS ProductName,
                    price,
                    quantity,
                    status,
                    total,
                    order_date::timestamp AS OrderDate
                FROM public.fn_orders_list_getlist({0}::integer);
            ";
            return await Orders.FromSqlRaw(sql, orderId).ToListAsync();
        }

        // ---------------- PostgreSQL function for Products ----------------
        public async Task<List<Product>> GetProductsFromFunctionAsync(int productId = -1)
        {
            var sql = @"
                SELECT 
                    id,
                    product_name AS Name,
                    category_name AS Category,
                    price,
                    product_image AS ProductImage
                FROM public.fn_products_list_getlist({0}::integer);
            ";
            return await Products.FromSqlRaw(sql, productId).ToListAsync();
        }

        // ---------------- PostgreSQL function for Inventory ----------------
        public async Task<List<InventoryItem>> GetInventoryFromFunctionAsync(int categoryId = -1)
        {
            var sql = @"
                SELECT 
                    id,
                    product_name AS ""ProductName"",
                    category_id AS ""CategoryId"",
                    category_name AS ""CategoryName"",
                    price,
                    stock AS ""Stock"",
                    created_date::timestamp AS ""CreatedDate"",
                    is_active AS ""IsActive""
                FROM public.fn_inventory_list_getlist({0}::integer);
            ";
            return await InventoryItems.FromSqlRaw(sql, categoryId).ToListAsync();
        }

        // ---------------- PostgreSQL function for Customers ----------------
        public async Task<List<Customer>> GetCustomersFromFunctionAsync(int customerId = -1)
        {
            var sql = @"
                SELECT 
                    id,
                    customer_name AS ""CustomerName"",
                    email,
                    mobile,
                    order_count AS ""OrderCount"",
                    to_timestamp(join_date, 'DD-MM-YYYY HH12:MI:SS AM') AS ""JoinDate"",
                    is_active AS ""IsActive""
                FROM public.fn_customer_list_getlist({0}::integer);
            ";
            return await Customers.FromSqlRaw(sql, customerId).ToListAsync();
        }

        // ---------------- PostgreSQL function for Support Tickets ----------------
        public async Task<List<SupportTicket>> GetSupportTicketsFromFunctionAsync(int ticketId = -1)
        {
            var sql = @"
                SELECT 
                    id,
                    customer_name AS CustomerName,
                    product_name AS ProductName,
                    total,
                    subject_name AS SubjectName,
                    description,
                    created_date::timestamp AS CreatedDate
                FROM public.fn_sp_tckts_list_getlist({0});
            ";
            return await SupportTickets.FromSqlRaw(sql, ticketId).ToListAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("public");

            // Function-based entities (no primary key)
            modelBuilder.Entity<Order>().HasNoKey();
            modelBuilder.Entity<Product>().HasNoKey();
            modelBuilder.Entity<InventoryItem>().HasNoKey();
            modelBuilder.Entity<Customer>().HasNoKey();
            modelBuilder.Entity<SupportTicket>().HasNoKey();
        }
    }
}
