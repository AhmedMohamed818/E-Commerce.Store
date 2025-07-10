using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IServiceManager
    {
        IProductService ProductService { get; }
        IBasketService BasketService { get; }
        ICacheService CacheService { get; }
        //ICategoryService CategoryService { get; }
        //IOrderService OrderService { get; }
        //ICustomerService CustomerService { get; }
        IAuthService AuthService { get; }
        IOrderService OrderService { get; }
        IPaymentService PaymentService { get; }
        //IEmailService EmailService { get; }
        //IFileStorageService FileStorageService { get; }
        //INotificationService NotificationService { get; }
        //IReportService ReportService { get; }
    }
}
