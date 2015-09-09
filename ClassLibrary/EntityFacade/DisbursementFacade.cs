using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.EntityFacade
{
    public class DisbursementFacade
    {
        SSISDBEntities ctx;
        public DisbursementFacade()
        {
            ctx = new SSISDBEntities();
        }
        public void createDisbursement(disbursement d)
        {
            ctx.disbursements.Add(d);
            ctx.SaveChanges();
        }
        public void createDisbursementDetail(disbursementDetail dd, disbursement d)
        {
            var dis = ctx.disbursements.First(o => o.disbursementId == d.disbursementId);
            dis.disbursementDetails.Add(dd);
            ctx.SaveChanges();
        }
    }
}
