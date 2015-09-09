using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.Entities;
using ClassLibrary.EntityFacade;
using ClassLibrary.Helper;

namespace ClassLibrary.Controllers
{
    public class GenerateStationaryRetrievalFormController
    {
        ErrorLog errorobj;
        private CatalogueFacade catalogueFacade;
        private RequisitionFacade requisitionFacade;

        public GenerateStationaryRetrievalFormController()
        {
            catalogueFacade = new CatalogueFacade();
            requisitionFacade = new RequisitionFacade();
            errorobj = new ErrorLog();
        }


        public List<RetrievalHelper> loadController()
        {
            List<RetrievalHelper> retrievalHelperCollection = new List<RetrievalHelper>();

            try
            {
                List<Item> itemCollection = catalogueFacade.getItems("Active");

                List<RequisitionDetails> requisitionDetails = requisitionFacade.getRequisitionsForRetrieval();

                foreach (RequisitionDetails reqDetails in requisitionDetails)
                {
                     RetrievalHelper retrievalHelper = new RetrievalHelper();
                    retrievalHelper.ItemId = reqDetails.ItemId;
                    retrievalHelper.Description = (from i in itemCollection
                                                   where i.ItemId == reqDetails.ItemId
                                                   select i.Description).FirstOrDefault().ToString();
                    retrievalHelper.BinNumber = (from i in itemCollection
                                                 where i.ItemId == reqDetails.ItemId
                                                 select i.BinNumber).FirstOrDefault().ToString();
                    retrievalHelper.StockBalance = Convert.ToInt32((from i in itemCollection
                                                                    where i.ItemId == reqDetails.ItemId
                                                                    select i.StockBalance).FirstOrDefault());
                    retrievalHelper.RequiredQty = reqDetails.RequestedQty;

                    int x = reqDetails.RequestedQty - reqDetails.DeliveredQty;

                    if (x <= retrievalHelper.StockBalance)
                    {
                        retrievalHelper.RetrievalQty = x;
                    }
                    else
                    {
                        retrievalHelper.RetrievalQty = retrievalHelper.StockBalance;
                    }

                    retrievalHelperCollection.Add(retrievalHelper);
                }
            }
            catch (Exception exception )
            {
                errorobj.WriteErrorLog("GenerateStationaryRetrievalFormController-loadController():::" + exception.ToString());
                retrievalHelperCollection = new List<RetrievalHelper>();

            }

            return retrievalHelperCollection;
            
        }

        public void updateStock(Item item)
        {
            catalogueFacade.updateStockret(item.ItemId, item.StockBalance);
        }
    }
}
