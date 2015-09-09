using ClassLibrary.Entities;
using ClassLibrary.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.EntityFacade
{
    public class RequisitionFacade
    {
        ErrorLog errorobj;
        SSISDBEntities ctx;

        requisition requisitionEntity;
        requisitionDetail reqDetailEntity;


        public RequisitionFacade()
        {
            ctx = new SSISDBEntities();
            errorobj = new ErrorLog();
        }

        public void getRequisitionsWithStatus(string status, string departmentId)
        {
        }

        public List<RequisitionDetails> getRequisitionsForRetrieval()
        {

            List<RequisitionDetails> requisitionDetailsCollection = new List<RequisitionDetails>();
            try
            {
                /*IEnumerable<requisitionDetail> reqDCollection = from req in ctx.requisitions
                                                                join reqD in ctx.requisitionDetails on req.requisitionId equals reqD.requisitionId
                                                                where req.status == "Approved" || req.status == "Outstanding" && reqD.status == "Pending" && reqD.requestedQty != reqD.deliveredQty
                                                                select reqD;

                

                var reqVar = (from rqD in reqDCollection
                              select rqD.requisitionId.ToString()).Distinct();

                foreach (string r in reqVar)
                {
                    Requisition requisition = new Requisition();
                    requisition.RequisitionId = r;
                    List<RequisitionDetails> requisitionDetailsCollection = new List<RequisitionDetails>();
                    foreach (requisitionDetail rD in reqDCollection)
                    {
                        if (rD.requisitionId == r)
                        {
                            RequisitionDetails requisitionDetails = new RequisitionDetails();
                            requisitionDetails.ItemId = rD.itemId;
                            requisitionDetails.RequestedQty = Convert.ToInt32(rD.requestedQty);
                            requisitionDetails.DeliveredQty = Convert.ToInt32(rD.deliveredQty);

                            requisitionDetailsCollection.Add(requisitionDetails);
                        }

                    }

                    requisition.RequisitionDetailsCollection = requisitionDetailsCollection;
                    requisitionCollection.Add(requisition);
                }*/




                var reqDCollection = from rd in ctx.requisitionDetails
                                                                where
                                                                  (new string[] { "Approved", "Outstanding","In Progress" }).Contains(rd.requisition.status) &&
                                                                  rd.requestedQty != rd.deliveredQty
                                                                group rd by new
                                                                {
                                                                    rd.itemId
                                                                } into g
                                                                select new
                                                                {
                                                                    itemId = g.Key.itemId,
                                                                    requestedQty = (int?)g.Sum(p => p.requestedQty),
                                                                    deliveredQty = (int?)g.Sum(p => p.deliveredQty)
                                                                };

                    
                    foreach (var rD in reqDCollection)
                    {
                            RequisitionDetails requisitionDetails = new RequisitionDetails();
                            requisitionDetails.ItemId = rD.itemId;
                            requisitionDetails.RequestedQty = Convert.ToInt32(rD.requestedQty);
                            requisitionDetails.DeliveredQty = Convert.ToInt32(rD.deliveredQty);

                            requisitionDetailsCollection.Add(requisitionDetails);
                    }

            }
            catch (Exception exception)
            {
                errorobj.WriteErrorLog("RequisitionFacade-getRequisitionsForRetrieval():::" + exception.ToString());
                
            }
            

            return requisitionDetailsCollection;
            
        }

        public void getRequisitionDetails()
        {
        }

        public List<Disbursement> getDisbursementWithStatus(string status)
        {
            List<Disbursement> disburesementCollection = new List<Disbursement>();

            try
            {
                IEnumerable<disbursement> disC = from dis in ctx.disbursements
                                                 where dis.status == status
                                                 select dis;

                foreach(disbursement d in disC)
                {
                    Disbursement disbursement = new Disbursement();
                    disbursement.DisbursementId = d.disbursementId;
                    disbursement.DepartmentId = d.departmentId;
                    disbursement.Date = (DateTime)d.date.Value;
                    disbursement.DeliveryDate = (DateTime)d.deliveryDate.Value;
                    disbursement.Status = d.status;

                    disburesementCollection.Add(disbursement);
                }
            }
            catch (Exception exception)
            {
                errorobj.WriteErrorLog("RequisitionFacade-getDisbursementWithStatus():::" + exception.ToString());
                disburesementCollection = new List<Disbursement>();
            }

            return disburesementCollection;
            
        }

        public List<Disbursement> getDisbursementWithStatus(string status, string departmentId)
        {
            List<Disbursement> disburesementCollection = new List<Disbursement>();

            try
            {

                var di = (from disb in ctx.disbursements
                           from u in ctx.users
                           where
                             disb.departmentId == departmentId &&
                             disb.status == status &&
                             u.departmentId==departmentId &&
                             u.status == "Active" &&
                             u.role.name == "departmentRepresentative"
                           select new
                           {
                               disId = disb.disbursementId,
                               depId = disb.departmentId,
                               dt = disb.date,
                               deldate = disb.deliveryDate,
                               status = disb.status,
                               depName = u.department.name,
                               col = disb.department.collectionPoint.address,
                               uname = u.name
                           });

                foreach(var dis in di)
                {
                    Disbursement ds = new Disbursement();
                    ds.DisbursementId = dis.disId;
                    ds.DepartmentId = dis.depId;
                    ds.DepartmentName = dis.depName;
                    ds.Date = (DateTime)dis.dt.Value;
                    ds.CollectionPointName = dis.col;
                    ds.RepresentativeName = dis.uname;
                    if (dis.deldate != null)
                    {
                        ds.DeliveryDate = (DateTime)dis.deldate.Value;
                    }
                    ds.Status = dis.status;

                    disburesementCollection.Add(ds);
                }
            }
            catch (Exception exception)
            {
                errorobj.WriteErrorLog("RequisitionFacade-getDisbursementWithStatus():::" + exception.ToString());
                disburesementCollection = new List<Disbursement>();
            }

            return disburesementCollection;

        }

        public Disbursement getDisbursementWithDetails(string status, string departmentId)
        {
            Disbursement ds = new Disbursement();

            try
            {
                var dis = (from disb in ctx.disbursements
                                    from u in ctx.users
                                    where
                                      disb.departmentId == departmentId &&
                                      u.departmentId == departmentId &&
                                      disb.status == status &&
                                      u.status == "Active" &&
                                      u.role.name == "departmentRepresentative"
                                    select new
                                    {
                                        disId = disb.disbursementId,
                                        depId = disb.departmentId,
                                        dt = disb.date,
                                        deldate = disb.deliveryDate,
                                        status = disb.status,
                                        depName = u.department.name,
                                        col = disb.department.collectionPoint.address,
                                        uname = u.name
                                    }).FirstOrDefault();

                if (dis != null)
                {
                    ds = new Disbursement();
                    ds.DisbursementId = dis.disId;
                    ds.DepartmentId = dis.depId;
                    ds.DepartmentName = dis.depName;
                    ds.Date = (DateTime)dis.dt.Value;
                    ds.CollectionPointName = dis.col;
                    ds.RepresentativeName = dis.uname;
                    if (dis.deldate != null)
                    {
                        ds.DeliveryDate = (DateTime)dis.deldate.Value;
                    }
                    ds.Status = dis.status;

                    ds.DisbursementDetailsCollection = getDisbursementDetails(ds.DisbursementId);
                }
                else
                {
                    ds = null;
                }
            }
            catch (Exception exception)
            {
                errorobj.WriteErrorLog("RequisitionFacade-getDisbursementWithStatus():::" + exception.ToString());
                ds = null;
            }

            return ds;

        }

        public List<DisbursementDetails> getDisbursementDetails(string disbursementId)
        {
            List<DisbursementDetails> disbursementDetailsCollection = new List<DisbursementDetails>();

            try
            {
                var dsd = from d in ctx.disbursementDetails
                          join i in ctx.items
                          on d.itemId equals i.itemId
                          where d.disbursementId == disbursementId
                          select new
                          {
                              id = d.itemId,
                              des = i.description,
                              res = d.requestQty,
                              del = d.deliveredQty
                          };

                foreach (var d in dsd)
                {
                    DisbursementDetails disbursementDetails = new DisbursementDetails();
                    disbursementDetails.ItemId = d.id;
                    disbursementDetails.ItemName = d.des;
                    disbursementDetails.RequestedQty = Convert.ToInt32(d.res);
                    disbursementDetails.DeliveredQty = Convert.ToInt32(d.del);

                    disbursementDetailsCollection.Add(disbursementDetails);
                }
            }
            catch (Exception exception)
            {
                errorobj.WriteErrorLog("RequisitionFacade-getDisbursementDetails():::" + exception.ToString());
                disbursementDetailsCollection = null;
            }

            return disbursementDetailsCollection;
        }

        public List<DisbursementHelper> getRequisitionsForDisbursement(string itemId)
        {
            List<DisbursementHelper> disbursementHelperCollection = new List<DisbursementHelper>();
            try
            {
                var reqC = from rD in ctx.requisitionDetails
                           where
                             (new string[] { "Approved", "Outstanding","In Progress" }).Contains(rD.requisition.status) &&
                             rD.itemId == itemId &&
                             rD.requestedQty != rD.deliveredQty
                           group new { rD.requisition, rD } by new
                           {
                               rD.requisition.requisitionId,
                               Date = (DateTime?)rD.requisition.date,
                               rD.requisition.departmentId,
                               rD.requisition.status,
                               Column1 = (rD.requestedQty - rD.deliveredQty)
                           } into g
                           orderby
                             g.Key.Date ascending
                           select new
                           {
                               reqId = g.Key.requisitionId,
                               depId = g.Key.departmentId,
                               status = g.Key.status,
                               reqQ = g.Key.Column1
                           };
                
                foreach(var r in reqC)
                {
                    DisbursementHelper disbursementHelper = new DisbursementHelper();
                    disbursementHelper.RequisitionId = r.reqId;
                    disbursementHelper.DepartmentId = r.depId;
                    disbursementHelper.Status = r.status;
                    disbursementHelper.RequestedQty = Convert.ToInt32(r.reqQ);

                    disbursementHelperCollection.Add(disbursementHelper);
                }

            }
            catch (Exception exception)
            {
                errorobj.WriteErrorLog("RequisitionFacade-getRequisitionsForDisbursement():::" + exception.ToString());
                disbursementHelperCollection = new List<DisbursementHelper>();
            }

            return disbursementHelperCollection;
        }

        public List<DisbursementHelper> getRequisitionsForCompleteDisbursement(string itemId,string departmentId)
        {
            List<DisbursementHelper> disbursementHelperCollection = new List<DisbursementHelper>();
            try
            {
                var reqC = from rD in ctx.requisitionDetails
                           where
                           rD.requisition.departmentId == departmentId && 
                             (new string[] { "In Progress" }).Contains(rD.requisition.status) &&
                             rD.itemId == itemId &&
                             rD.requestedQty != rD.deliveredQty
                           group new { rD.requisition, rD } by new
                           {
                               rD.requisition.requisitionId,
                               Date = (DateTime?)rD.requisition.date,
                               rD.requisition.departmentId,
                               rD.requisition.status,
                               rD.requestedQty,
                               rD.deliveredQty
                           } into g
                           orderby
                             g.Key.Date ascending
                           select new
                           {
                               reqId = g.Key.requisitionId,
                               depId = g.Key.departmentId,
                               status = g.Key.status,
                               reqQ = g.Key.requestedQty,
                               delQ = g.Key.deliveredQty
                           };

                foreach (var r in reqC)
                {
                    DisbursementHelper disbursementHelper = new DisbursementHelper();
                    disbursementHelper.RequisitionId = r.reqId;
                    disbursementHelper.DepartmentId = r.depId;
                    disbursementHelper.Status = r.status;
                    disbursementHelper.RequestedQty = Convert.ToInt32(r.reqQ);
                    disbursementHelper.DeliveredQty = Convert.ToInt32(r.delQ);
                    disbursementHelperCollection.Add(disbursementHelper);
                }

            }
            catch (Exception exception)
            {
                errorobj.WriteErrorLog("RequisitionFacade-getRequisitionsForCompleteDisbursement():::" + exception.ToString());
                disbursementHelperCollection = new List<DisbursementHelper>();
            }

            return disbursementHelperCollection;
        }


        public string getUserRequisitionsWithStatus(string status, string userId)
        {
            return status;
        }

        public string getRequisitionDetails(string requisitionId)
        {
            return requisitionId;
        }

        public void updateRequisitionStatus(string requisitionId,string status)
        {
            try
            {
                ctx.updateReqStatusPro(requisitionId, status);

            }
            catch (Exception exception)
            {
                errorobj.WriteErrorLog("RequisitionFacade-updateRequisitionStatus():::" + exception.ToString());
            }
                
        }

        public void updateDisbursment(List<DisbursementDetails> disbursementDetailsCollection, string disbursementId )
        {
            try
            {
                foreach(DisbursementDetails disbursementDetail in disbursementDetailsCollection)
                {
                    ctx.updateDisbursmentPro(disbursementId, disbursementDetail.ItemId, disbursementDetail.DeliveredQty);

                    //var dis = (from d in ctx.disbursementDetails
                    //           where d.disbursementId == disbursementId && d.itemId == disbursementDetail.ItemId
                    //           select d).FirstOrDefault();

                    //dis.deliveredQty = disbursementDetail.DeliveredQty;

                    //ctx.SaveChanges();

                   
                }

                updateDisbursementStatus(disbursementId, "Completed");

            }
            catch (Exception exception)
            {
                errorobj.WriteErrorLog("RequisitionFacade-updateDisbursment():::" + exception.ToString());
            }
        }

        public void updateRequisitionDelivery(DisbursementHelper disbursementHelper, string itemId)
        {
            try
            {
                ctx.updateRequisitionDeliveryPro(disbursementHelper.RequisitionId, itemId, disbursementHelper.DeliveredQty);

                //requisitionDetail reqD = (from r in ctx.requisitionDetails
                //                          where r.itemId == itemId && r.requisitionId == disbursementHelper.RequisitionId
                //                          select r).FirstOrDefault();

                //reqD.deliveredQty = disbursementHelper.DeliveredQty;

                
                //ctx.SaveChanges();
            }
            catch (Exception exception)
            {
                errorobj.WriteErrorLog("RequisitionFacade-updateRequisitionDelivery():::" + exception.ToString());
            }
        }

        public void updateDisbursementStatus(string disbursementId,string status)
        {
            try
            {
                ctx.updateDisbursementStatus(disbursementId, status);

                //disbursement dis = (from d in ctx.disbursements
                //                    where d.disbursementId == disbursementId
                //                    select d).FirstOrDefault();

                //dis.status = status;

                //ctx.SaveChanges();
            }
            catch (Exception exception)
            {
                errorobj.WriteErrorLog("RequisitionFacade-updateDisbursementStatus():::" + exception.ToString());
            }
        }

        public void createDibursement(Disbursement disbursement)
        {
            if (disbursement.DisbursementDetailsCollection.Count != 0)
            {
                disbursement dis = new disbursement();
                dis.disbursementId = disbursement.DisbursementId;
                dis.departmentId = disbursement.DepartmentId;
                dis.date = disbursement.Date;
                dis.status = disbursement.Status;
                dis.deliveryDate = disbursement.Date;
                ctx.disbursements.Add(dis);
                ctx.SaveChanges();

                List<DisbursementDetails> disD = disbursement.DisbursementDetailsCollection;

                foreach (DisbursementDetails ds in disD)
                {
                    disbursementDetail dl = new disbursementDetail();
                    dl.disbursementId = disbursement.DisbursementId;
                    dl.itemId = ds.ItemId;
                    dl.requestQty = ds.RequestedQty;
                    dl.deliveredQty = 0;

                    ctx.disbursementDetails.Add(dl);
                    ctx.SaveChanges();

                }
            }

        }

        public void createRequisition()
        {
        }

        public void updateDisbursementRequisitions(string departmentId)
        {
            try
            {
                //SSISDBEntities ct = new SSISDBEntities();

                ctx.updateReqStatusFromInProgressPro(departmentId);

                IEnumerable<requisition> reqC = from r in ctx.requisitions
                                                where r.departmentId == departmentId && r.status == "Outstanding"
                                                select r;
                
                foreach(requisition req in reqC)
                {
                    IEnumerable<requisitionDetail> reqDC = from r in ctx.requisitionDetails
                                                           where r.requisitionId == req.requisitionId
                                                           select r;

                    int c = 0;
                    foreach(requisitionDetail reqD in reqDC)
                    {
                        if(reqD.requestedQty==reqD.deliveredQty)
                        {
                            c++;
                        }
                    }

                    if(c==reqDC.Count())
                    {
                        updateRequisitionStatus(req.requisitionId, "Completed");
                    }
                }

            }
            catch (Exception exception)
            {
                errorobj.WriteErrorLog("RequisitionFacade-updateDisbursementRequisitions():::" + exception.ToString());
            }
        }


        #region Lignan's Part

        public List<requisition> getRequisitionsWithStatus_Lingna(string status, string depId)
        {
            var reqs = from o in ctx.requisitions
                       where o.status == status && o.departmentId == depId
                       select o;
            return reqs.ToList();
        }

        public List<requisition> getRequisitions_Lingna()
        {
            var reqs = from o in ctx.requisitions
                       select o;
            return reqs.ToList();
        }

        public string getDisbursementWithStatus_Lingna(string status)
        {
            return status;
        }

        public string getRequisitionsForDisbursement_Lingna(string status, string itemId)
        {
            return status;
        }

        public List<requisition> getUserRequisitionsWithStatus_Lingna(string status, string userId)
        {
            var reqs = from o in ctx.requisitions
                       where o.status == status && o.userId == userId
                       select o;
            return reqs.ToList();
        }

        public List<requisitionDetail> getRequisitionDetails_Lingna(string requisitionId)
        {
            var list = from o in ctx.requisitionDetails
                       where o.requisitionId == requisitionId
                       select o;
            return list.ToList();
        }

        public void updateRequisitionStatus_Lingna(string status, string reqId)
        {
            var req = from o in ctx.requisitions
                      where o.requisitionId == reqId
                      select o;
            requisition r = req.First();
            r.status = status;
            ctx.SaveChanges();
        }

        #endregion



        #region Pyae Pyae's Part

        public string getDepartmentId_PyaePyae(string userId)
        {
            var departmentId = from x in ctx.users
                               where x.userId == userId
                               select x.departmentId;

            return departmentId.First();
        }

        public int getCodeGeneratorValue_PyaePyae(string prefix)
        {
            var g = ctx.codeGenerators.FirstOrDefault(o => o.prefix == prefix);
            return g.lastValue;
        }

        public void updateCodeGeneratorValue_PyaePyae(string prefix)
        {
            var g = ctx.codeGenerators.FirstOrDefault(o => o.prefix == prefix);
            g.lastValue++;
            ctx.SaveChanges();
        }
        public requisition getRequisitionsWithStatus_PyaePyae(string status, string departmentId)
        {
            var requisitionData_byDept = from x in ctx.requisitions
                                         where x.status == status
                                         & x.departmentId == departmentId
                                         select x;

            return requisitionData_byDept.SingleOrDefault();
        }

        public List<requisition> getRequisition_forApproveReject_PyaePyae(string departmentId)
        {
            var requisitionData = from x in ctx.requisitions
                                  where x.departmentId == departmentId
                                  & x.status == "Pending"
                                  select x;

            return requisitionData.ToList();
        }

        public List<requisition> getUserRequisitionsWithStatus_PyaePyae(string status, string userId)
        {
            var requisitionData_byUser = from x in ctx.requisitions
                                         where x.status == status
                                         & x.userId == userId
                                         select x;

            return requisitionData_byUser.ToList();
        }

        public List<requisitionDetail> getRequisitionDetails_PyaePyae(string requisitionId)
        {
            var requisitionDetailsData_byRId = from x in ctx.requisitionDetails
                                               where x.requisitionId == requisitionId
                                               select x;

            return requisitionDetailsData_byRId.ToList();
        }

        public List<string> getStatus_PyaePyae(string userId)
        {
            var statusData = from x in ctx.requisitions
                             where x.userId == userId
                             select x.status;

            return statusData.ToList();
        }

        public void updateRequisitionStatus_PyaePyae(string requisitionId, string status, string reason)
        {
            requisitionEntity = new requisition();
            requisitionEntity = (from x in ctx.requisitions
                                 where x.requisitionId == requisitionId
                                 select x).SingleOrDefault();
            requisitionEntity.status = status;
            requisitionEntity.rejectreason = reason;
            ctx.SaveChanges();
        }


        public void createRequisition_PyaePyae(string requisitionId, DateTime date, string userId, string departmentId, string rejectReason, string status, DataTable requisitionDetails)
        {
            requisitionEntity = new requisition();
            requisitionEntity.requisitionId = requisitionId;
            requisitionEntity.date = date;
            requisitionEntity.userId = userId;
            requisitionEntity.departmentId = departmentId;
            requisitionEntity.rejectreason = rejectReason;
            requisitionEntity.status = status;
            ctx.requisitions.Add(requisitionEntity);
            ctx.SaveChanges();
            foreach (DataRow dr in requisitionDetails.Rows)
            {
                reqDetailEntity = new requisitionDetail();
                reqDetailEntity.requisitionId = (dr["requisitionId"]).ToString();
                reqDetailEntity.itemId = (dr["itemId"]).ToString();
                reqDetailEntity.requestedQty = Convert.ToInt32(dr["quantity"]);
                reqDetailEntity.deliveredQty = 0;
                ctx.requisitionDetails.Add(reqDetailEntity);
                ctx.SaveChanges();
            }

        }


        #endregion

    }
}
