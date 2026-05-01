namespace Manzili.Api.Common
{
    public static class Messages
    {
        public static class Service
        {
            public const string Created = "Service created successfully.";
            public const string Updated = "Service updated successfully.";
            public const string Deleted = "Service deleted successfully.";

            public static string CustomMessage(string action) => $"Service {action} successfully.";
                
        }

        public static class Review
        {
            public const string Deleted = "Review deleted successfully.";
        }

        public static class Order
        {
            public const string Created = "Order placed successfully.";
            public const string Approved = "Order has been approved successfully.";
            public const string Rejected = "Order has been Rejected.";
            public const string Repriced = "Order has been RePriced successfully.";

            public static string CustomMessage(string action) => $"Order {action} successfully.";
        }
    }
}
