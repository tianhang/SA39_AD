using ClassLibrary.Helper;
using ClassLibrary.Entities;
using ClassLibrary.EntityFacade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ClassLibrary.Controllers
{
    public class GenerateDisbursementController
    {
        ErrorLog errorobj;
        RequisitionFacade requisitionFacade;
        CatalogueFacade catalogueFacade;
        DepartmentFacade departmentFacade;
        HelperFacade helperFacade;

        public GenerateDisbursementController()
        {
            errorobj = new ErrorLog();
            requisitionFacade = new RequisitionFacade();
            catalogueFacade = new CatalogueFacade();
            departmentFacade = new DepartmentFacade();
            helperFacade = new HelperFacade();
        }

        public int loadController()
        {
            try
            {
                List<Disbursement> disbursementCollection = requisitionFacade.getDisbursementWithStatus("Outstanding");
                if(disbursementCollection.Count>0)
                {
                    return 0;
                }
                else
                {
                    List<Item> itemCollection = catalogueFacade.getItems("Active");
                    List<Department> departmentCollection = departmentFacade.getDepartments();
                    disbursementCollection.Clear();
                    disbursementCollection = new List<Disbursement>();

                    codeGenerator codeG = helperFacade.getCode("disbursementId");
                    Hashtable depItems = new Hashtable();
                    foreach(Department d in departmentCollection)
                    {
                        Disbursement disbursement = new Disbursement();
                        string newId = CodeGeneratorHelper.returnCode(codeG.prefix,codeG.lastValue);
                        codeG.lastValue = Convert.ToInt32(newId.Substring(codeG.prefix.Length));
                        disbursement.DisbursementId = newId;
                        disbursement.DepartmentId = d.DepartmentId;
                        disbursement.Date = DateTime.Now;
                        disbursement.Status = "Outstanding";
                        List<DisbursementDetails> disbursementDetailsCollection = new List<DisbursementDetails>();
                        disbursement.DisbursementDetailsCollection = disbursementDetailsCollection;
                        disbursementCollection.Add(disbursement);
                        depItems.Add(d.DepartmentId, 0);
                    }
                    
                    foreach(Item item in itemCollection)
                    {
                        foreach (Department d in departmentCollection)
                        {
                            depItems[d.DepartmentId]= 0;
                        }
                        List<DisbursementHelper> disbursementHelperCollection = requisitionFacade.getRequisitionsForDisbursement(item.ItemId);
                        if (disbursementHelperCollection.Count > 0)
                        {
                            foreach (DisbursementHelper d in disbursementHelperCollection)
                            {
                                if (item.StockBalance > 0)
                                {
                                    if (item.StockBalance >= d.RequestedQty)
                                    {
                                        depItems[d.DepartmentId] = Convert.ToInt32(depItems[d.DepartmentId]) + d.RequestedQty;
                                        item.StockBalance -= d.RequestedQty;
                                    }
                                    else
                                    {
                                        depItems[d.DepartmentId] = Convert.ToInt32(depItems[d.DepartmentId]) + item.StockBalance;
                                        item.StockBalance -= item.StockBalance;
                                    }
                                    d.Status = "In Progress";
                                    requisitionFacade.updateRequisitionStatus(d.RequisitionId,"In Progress");
                                }
                                else
                                {
                                    break;
                                }
                            }

                            foreach (Department d in departmentCollection)
                            {
                                if (Convert.ToInt32(depItems[d.DepartmentId]) > 0)
                                {
                                    DisbursementDetails disbursementDetails = new DisbursementDetails();
                                    disbursementDetails.ItemId = item.ItemId;
                                    disbursementDetails.RequestedQty = Convert.ToInt32(depItems[d.DepartmentId]);
                                    Disbursement disbursement = disbursementCollection.Find(e => e.DepartmentId == d.DepartmentId);
                                    List<DisbursementDetails> disbursementDetailsCollection = disbursement.DisbursementDetailsCollection;
                                    disbursementDetailsCollection.Add(disbursementDetails);
                                    disbursement.DisbursementDetailsCollection = disbursementDetailsCollection;
                                }
                            }
                        }
                    }
                    
                    foreach(Disbursement disbursement in disbursementCollection)
                    {
                        requisitionFacade.createDibursement(disbursement);
                    }

                    helperFacade.updateCode(codeG);

                    return 1;
                }
                

            }
            catch (Exception exception)
            {
                errorobj.WriteErrorLog("GenerateDisbursementController-loadController():::" + exception.ToString());
                return -1;
            }
            
        }

    }
}
