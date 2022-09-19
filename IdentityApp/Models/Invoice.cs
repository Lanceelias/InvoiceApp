namespace IdentityApp.Models
{
    public class Invoice
    {
        /// <summary>
        /// Unique Id
        /// </summary>
        public int InvoiceId { get; set; }

        public double Pricing { get; set; }
        
        public double InvoiceAmount { get; set; }

        public string? InvoiceMonth { get; set; }
        /// <summary>
        /// Person who receives the invoice
        /// </summary>
        public string? InvoiceOwner { get; set; }
        /// <summary>
        /// The accountant/the user who creates the invoice/the users
        /// </summary>
        public string? CreatorId { get; set; }
        /// <summary>
        /// state of invoice
        /// </summary>
        public InvoiceStatus Status { get; set; }
    }


}

namespace IdentityApp
{
    public enum InvoiceStatus
    {
        Submitted,
        Approved,
        Rejected
    }
}

