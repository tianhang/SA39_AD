using ClassLibrary.Entities;
using ClassLibrary.GaoFan;
using ClassLibrary.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SSISWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISSISWCFService" in both code and config file together.
    [ServiceContract]
    public interface ISSISWCFService
    {

        [OperationContract]
        [WebGet(UriTemplate = "/generateRetrieval/loadStationaryRetrievalController", ResponseFormat = WebMessageFormat.Json)]
        List<RetrievalHelper> gsrfloadStationaryRetrievalController();


        [OperationContract]
        [WebGet(UriTemplate = "/generateDisbursement", ResponseFormat = WebMessageFormat.Json)]
        int gdloadController();

        [OperationContract]
        [WebGet(UriTemplate = "/viewDisbursement/loadController", ResponseFormat = WebMessageFormat.Json)]
        List<Department> vdloadController();

        [OperationContract]
        [WebGet(UriTemplate = "/viewDisbursement/selectDepartment/{departmentId}", ResponseFormat = WebMessageFormat.Json)]
        List<Disbursement> vdselectDepartment(string departmentId);

        [OperationContract]
        [WebGet(UriTemplate = "/viewDisbursement/selectPendingDisbursement/{departmentId}", ResponseFormat = WebMessageFormat.Json)]
        Disbursement vdselectPendingDisbursement(string departmentId);

        [OperationContract]
        [WebGet(UriTemplate = "/viewDisbursement/selectDisbursementDetails/{disbursementId}", ResponseFormat = WebMessageFormat.Json)]
        List<DisbursementDetails> vdselectDisbursementDetails(string disbursementId);

        [OperationContract]
        [WebInvoke(UriTemplate = "/completeDelivery", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void cdselectCompleteDelivery(Disbursement disbursement);



        [OperationContract]
        [WebInvoke(UriTemplate = "/stationaryretrieval/updateStock", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void gsrfupdateStock(Item item);



        #region Gao Fan's Work

        //reorderItem---------------------------------------------------------------------
        [OperationContract]
        [WebGet(UriTemplate = "/getAllPendingReorderItem", ResponseFormat = WebMessageFormat.Json)]
        List<ReorderItem> getAllPendingReorderItem();

        [OperationContract]
        [WebInvoke(UriTemplate = "/updateReorderItemStatus", Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        void updateReorderItemStatus(ReorderItem rei);
        //user------------------------------------------------------------------------

        [OperationContract]
        [WebGet(UriTemplate = "/getManager", ResponseFormat = WebMessageFormat.Json)]
        User getManager();


        [OperationContract]
        [WebGet(UriTemplate = "/getUserByEmail/{email}", ResponseFormat = WebMessageFormat.Json)]
        User getUserByEmail(string email);

        [OperationContract]
        [WebGet(UriTemplate = "/getCurrentRepresentative/{partId}", ResponseFormat = WebMessageFormat.Json)]
        User getCurrentRepresentative(string partId);

        [OperationContract]
        [WebGet(UriTemplate = "/getCurrentDelegate/{departmentId}", ResponseFormat = WebMessageFormat.Json)]
        User getCurrentDelegate(string departmentId);

        [OperationContract]
        [WebGet(UriTemplate = "/getEmployeeByDepartmentId/{partId}", ResponseFormat = WebMessageFormat.Json)]
        List<User> getEmployeeByDepartmentId(string partId);

        [OperationContract]
        [WebGet(UriTemplate = "/getAllUserByDepartmentId/{departmentId}", ResponseFormat = WebMessageFormat.Json)]
        List<User> getAllUserByDepartmentId(string departmentId);

        [OperationContract]
        [WebInvoke(UriTemplate = "/updateUserRoleIdByName", Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        void updateUserRoleIdByName(User e);

        [OperationContract]
        [WebInvoke(UriTemplate = "/updateRoleBack", Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        void updateRoleBack(User ee);

        [OperationContract]
        [WebInvoke(UriTemplate = "/updateUser", Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        void updateUser(User e);

        //collection--------------------------------------------------------------------------------------
        [OperationContract]
        [WebGet(UriTemplate = "/getAllCollection", ResponseFormat = WebMessageFormat.Json)]
        List<CollectionPoint> getAllCollection();

        [OperationContract]
        [WebGet(UriTemplate = "/getCollection/{name}", ResponseFormat = WebMessageFormat.Json)]
        CollectionPoint getCollection(string name);

        //department------------------------------------------------------------------
        [OperationContract]
        [WebInvoke(UriTemplate = "/updateDepartmentCollectionPoint", Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        void updateDepartmentCollectionPoint(Department department);

        [OperationContract]
        [WebGet(UriTemplate = "/getDepartmentById/{departmentId}", ResponseFormat = WebMessageFormat.Json)]
        Department getDepartment(string departmentId);


        //category , item --------------------------------------------------------------------------

        [OperationContract]
        [WebGet(UriTemplate = "/getItemByItemName/{name}", ResponseFormat = WebMessageFormat.Json)]
        Item getItemByItemName(string name);

        [OperationContract]
        [WebGet(UriTemplate = "/getAllcategory", ResponseFormat = WebMessageFormat.Json)]
        List<Category> getAllCategory();


        [OperationContract]
        [WebGet(UriTemplate = "/getItemDescriptionByCategoryId/{categoryId}", ResponseFormat = WebMessageFormat.Json)]
        List<string> getAllItemDescriptionByCategoryId(string categoryId);

        [OperationContract]
        [WebGet(UriTemplate = "/getItemByCategoryId/{categoryId}", ResponseFormat = WebMessageFormat.Json)]
        List<Item> getItemByCategoryId(string categoryId);

        [OperationContract]
        [WebGet(UriTemplate = "/getCategoryId/{name}", ResponseFormat = WebMessageFormat.Json)]
        string getCategoryIdByName(string name);

        [OperationContract]
        [WebGet(UriTemplate = "/getCategoryByDescription/{description}", ResponseFormat = WebMessageFormat.Json)]
        Category getCategoryByDescription(string description);
        //codegenerate------------------------------------------------------------------------------
        [OperationContract]
        [WebGet(UriTemplate = "/getCode/{prefix}", ResponseFormat = WebMessageFormat.Json)]
        CodeGenerator getCode(string prefix);

        [OperationContract]
        [WebInvoke(UriTemplate = "/updateCodeGenerator", Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        void updateCodeGenerator(CodeGenerator cg);

        //requisition--------------------------------------------------------------------------------------------
        [OperationContract]
        [WebInvoke(UriTemplate = "/insertRequisition", Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        void insertRequisition(Requisition re);


        [OperationContract]
        [WebInvoke(UriTemplate = "/insertRequisitionDetails", Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        void insertRequisitionDetails(RequisitionDetailsGF red);

        [OperationContract]
        [WebGet(UriTemplate = "/getAllPendingRequisitionByDepartmentId/{departmentId}", ResponseFormat = WebMessageFormat.Json)]
        List<Requisition> getAllPendingRequisitionByDepartmentId(string departmentId);

        [OperationContract]
        [WebGet(UriTemplate = "/getRequisitionDetailsByRequisitionId/{requisitionId}", ResponseFormat = WebMessageFormat.Json)]
        List<RequisitionDetailsGF> getRequisitionDetailsByRequisitionId(string requisitionId);

        [OperationContract]
        [WebInvoke(UriTemplate = "/updateRequisitionStatus", Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        void updateRequisitionStatus(Requisition re);

        [OperationContract]
        [WebGet(UriTemplate = "/getAllRequisitionByUserId/{userId}", ResponseFormat = WebMessageFormat.Json)]
        List<Requisition> getAllRequisitionByUserId(string userId);

        //discrepancy---------------------------------------------
        [OperationContract]
        [WebGet(UriTemplate = "/getAllPendingDiscrepancy", ResponseFormat = WebMessageFormat.Json)]
        List<Discrepancy> getAllPendingDiscrepancy();

        [OperationContract]
        [WebGet(UriTemplate = "/getAllPendingDiscrepancyUnder250", ResponseFormat = WebMessageFormat.Json)]
        List<Discrepancy> getAllPendingDiscrepancyUnder250();

        [OperationContract]
        [WebGet(UriTemplate = "/getAllPendingDiscrepancyOver250", ResponseFormat = WebMessageFormat.Json)]
        List<Discrepancy> getAllPendingDiscrepancyOver250();

        [OperationContract]
        [WebInvoke(UriTemplate = "/insertDiscrepancy", Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        void insertDiscrepancy(Discrepancy dis);

        [OperationContract]
        [WebInvoke(UriTemplate = "/updateDiscrepancyStatus", Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        void updateDiscrepancyStatus(Discrepancy dis);

        // supplier price--------------------------------------------------------------------
        [OperationContract]
        [WebGet(UriTemplate = "/getSupplierPrice/{itemId}/{supplierId}", ResponseFormat = WebMessageFormat.Json)]
        SupplierPrice getSupplierPrice(string itemId, string supplierId);


        [OperationContract]
        [WebGet(UriTemplate = "/getAllSupplier", ResponseFormat = WebMessageFormat.Json)]
        List<Supplier> getAllSupplier();

        [OperationContract]
        [WebGet(UriTemplate = "/getSupplier/{name}", ResponseFormat = WebMessageFormat.Json)]
        Supplier getSupplierByName(string name);

        #endregion


    }
}
