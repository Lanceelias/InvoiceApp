using IdentityApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityApp.Pages
{
    public class IndexModel : PageModel
    {
        public Dictionary<string, int> revenueSubmitted;
        public Dictionary<string, int> revenueApproved;
        public Dictionary<string, int> revenueRejected;

        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;


        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context)
        {

            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {
            revenueSubmitted = InitDict(revenueSubmitted);
            revenueApproved = InitDict(revenueApproved);
            revenueRejected = InitDict(revenueRejected);

            var invoices = _context?.Invoice?.ToList();

            foreach (var invoice in invoices)
            {
                switch (invoice.Status)
                {
                    case InvoiceStatus.Submitted:
                        revenueSubmitted[invoice.InvoiceMonth] += (int)invoice.InvoiceAmount;
                        break;
                    case InvoiceStatus.Approved:
                        revenueApproved[invoice.InvoiceMonth] += (int)invoice.InvoiceAmount;
                        break;
                    case InvoiceStatus.Rejected:
                        revenueRejected[invoice.InvoiceMonth] += (int)invoice.InvoiceAmount;
                        break;
                    default:
                        break;
                }
            }

        }

        private Dictionary<string, int> InitDict(Dictionary<string, int> dict)
        {
            dict = new Dictionary<string, int>()
            {
                {"January", 0},
                {"February", 6},
                {"March", 2},
                {"April", 1},
                {"May", 8},
                {"June", 1},
                {"July", 2},
                {"August", 9},
                {"September", 9},
                {"October", 2},
                {"November", 5},
                {"December", 4},
            };

            return dict;
        }
    }
}