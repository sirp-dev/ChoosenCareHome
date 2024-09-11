using ChoosenCareHome.Data.Model;
using Microsoft.AspNetCore.Mvc;
using static ChoosenCareHome.Data.Model.Enum;

namespace ChoosenCareHome.Data
{
    public class Invoice
    {
        public int Id { get; set; }
        public string? InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public InvoiceStatus InvoiceStatus { get; set; }
       public string UserId {  get; set; }
        public Profile User {  get; set; }
        public DateTime PeriodStart { get; set; }

       
        public DateTime PeriodEnd { get; set; }

       
        public decimal Rate { get; set; }

       
        public decimal TotalHours { get; set; }

       
        public decimal TotalPay { get; set; }

       
        public decimal NetPay { get; set; }

       
        public decimal IncomeTax { get; set; }

       
        public decimal NationalInsurance { get; set; }


       
        public string? NINumber { get; set; }


       
        public string? TaxCode { get; set; }
        public string? P45 { get; set; }

     


        public string? PaymentMethod { get; set; }
    }
}
