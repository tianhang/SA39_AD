using ClassLibrary.Controllers;
using ClassLibrary.Entities;
using ClassLibrary.GaoFan;
using ClassLibrary.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SSISWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SSISWCFService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SSISWCFService.svc or SSISWCFService.svc.cs at the Solution Explorer and start debugging.
    public class SSISWCFService : ISSISWCFService
    {
        public List<RetrievalHelper> gsrfloadStationaryRetrievalController()
        {
            GenerateStationaryRetrievalFormController generateStationaryRetrievalFormController = new GenerateStationaryRetrievalFormController();
            return generateStationaryRetrievalFormController.loadController();
        }

        public int gdloadController()
        {
            GenerateDisbursementController generateDisbursementController = new GenerateDisbursementController();
            return generateDisbursementController.loadController();
        }

        public List<Department> vdloadController()
        {
            ViewDisbursementController viewDisbursementController = new ViewDisbursementController();
            return viewDisbursementController.loadController();
        }

        public List<Disbursement> vdselectDepartment(string departmentId)
        {
            ViewDisbursementController viewDisbursementController = new ViewDisbursementController();
            return viewDisbursementController.selectDepartment(departmentId);
        }

        public Disbursement vdselectPendingDisbursement(string departmentId)
        {
            ViewDisbursementController viewDisbursementController = new ViewDisbursementController();
            return viewDisbursementController.selectPendingDisbursement(departmentId);
        }

        public List<DisbursementDetails> vdselectDisbursementDetails(string disbursementId)
        {
            ViewDisbursementController viewDisbursementController = new ViewDisbursementController();
            return viewDisbursementController.selectDisbursementDetails(disbursementId);
        }

        public void cdselectCompleteDelivery(Disbursement disbursement)
        {
            ViewDisbursementController viewDisbursementController = new ViewDisbursementController();
            viewDisbursementController.selectCompleteDelivery(disbursement);
        }


        public void gsrfupdateStock(Item item)
        {
            GenerateStationaryRetrievalFormController generateStationaryRetrievalFormController = new GenerateStationaryRetrievalFormController();
            generateStationaryRetrievalFormController.updateStock(item);
        }




        #region Gao Fan's Work

        requisitionGF requisitiongf = new requisitionGF();
        categoryGF categorygf = new categoryGF();
        collectionPointGF collectionPointgf = new collectionPointGF();
        codeGeneratorGF codeGeneratorgf = new codeGeneratorGF();
        itemGF itemgf = new itemGF();
        userGF usergf = new userGF();
        reorderGF reordergf = new reorderGF();
        departmentGF departmentgf = new departmentGF();
        discrepancyGF discrepancygf = new discrepancyGF();
        supplierPriceGF supplierPircegf = new supplierPriceGF();
        //reorder-----------------------
        public List<ReorderItem> getAllPendingReorderItem()
        {
            return reordergf.getReorderItemsByStatusGF();
        }

        public void updateReorderItemStatus(ReorderItem rei)
        {
            reordergf.updateReorderItemGF(rei);
        }
        //user-----------------------------
        public User getUserByEmail(string email)
        {
            return usergf.getUserByEmailGF(email);
        }

        public User getCurrentRepresentative(string partId)
        {
            return usergf.getCurrentRepresentativeGF(partId);
        }

        public List<User> getEmployeeByDepartmentId(string partId)
        {
            return usergf.getEmployeeByDepartmentIdGF(partId);
        }

        public List<User> getAllUserByDepartmentId(string departmentId)
        {
            return usergf.getAllUserByDepartmentIdGF(departmentId);
        }

        public void updateUserRoleIdByName(User u)
        {
            usergf.updateUserRoleIdByNameGF(u);
        }

        public void updateRoleBack(User e)
        {
            usergf.updateRoleBack(e);
        }

        public User getCurrentDelegate(string departmentId)
        {
            return usergf.getCurrentDelegateGF(departmentId);
        }

        public void updateUser(User e)
        {
            usergf.updateUserGF(e);
        }

        public User getManager()
        {
            return usergf.getManagerGF();
        }
        //collectionpoint-------------------------------
        public List<CollectionPoint> getAllCollection()
        {
            return collectionPointgf.getAllCollectionPointGF();
        }

        public CollectionPoint getCollection(string name)
        {
            return collectionPointgf.getCollection(name);
        }
        //department-------------------------------------------
        public void updateDepartmentCollectionPoint(Department department)
        {
            departmentgf.updateDepartmentCollectionGF(department);
        }

        public Department getDepartment(string departmentId)
        {
            return departmentgf.getDepartment(departmentId);
        }
        //Category----------------------
        public List<Category> getAllCategory()
        {
            return categorygf.getAllCategoryGF();
        }

        public string getCategoryIdByName(string name)
        {
            return categorygf.getCategoryIdByNameGF(name);
        }
        //codeGenerator---------------------
        public CodeGenerator getCode(string prefix)
        {
            return codeGeneratorgf.getCodeGF(prefix);
        }

        public void updateCodeGenerator(CodeGenerator cg)
        {
            codeGeneratorgf.updateCodeGeneratorGF(cg);
        }
        //requsition--------------
        public void insertRequisition(Requisition re)
        {
            requisitiongf.insertRequisitionGF(re);
        }

        public void insertRequisitionDetails(RequisitionDetailsGF red)
        {
            requisitiongf.insertRequisitionDetailsGF(red);
        }

        public List<Requisition> getAllPendingRequisitionByDepartmentId(string departmentId)
        {
            return requisitiongf.getAllPendingRequisitionByDepartmentIdGF(departmentId);
        }

        public List<RequisitionDetailsGF> getRequisitionDetailsByRequisitionId(string requisitionId)
        {
            return requisitiongf.getRequisitionDetailsByRequisitionIdGF(requisitionId);
        }

        public void updateRequisitionStatus(Requisition re)
        {
            requisitiongf.updateRequisitionStatusGF(re);
        }

        public List<Requisition> getAllRequisitionByUserId(string userId)
        {
            return requisitiongf.getAllRequisitionByUserIdGF(userId);
        }
        //item---------------------
        public Category getCategoryByDescription(string description)
        {
            return itemgf.getCategoryByDescriptionGF(description);
        }
        public List<string> getAllItemDescriptionByCategoryId(string categoryId)
        {
            return itemgf.getAllItemByCategoryIdGF(categoryId);
        }
        public Item getItemByItemName(string name)
        {
            return itemgf.getItemByDescriptionGF(name);
        }

        public List<Item> getItemByCategoryId(string categoryId)
        {
            return itemgf.getItemByCategoryIdGF(categoryId);
        }
        //discrepancy--------------------------------------------------------
        public List<Discrepancy> getAllPendingDiscrepancy()
        {
            return discrepancygf.getAllPendingDiscrepancyGF();
        }

        public void insertDiscrepancy(Discrepancy dis)
        {
            discrepancygf.insertDiscrepancyGF(dis);
        }
        public void updateDiscrepancyStatus(Discrepancy dis)
        {
            discrepancygf.updateDiscrepancyStatusGF(dis);
        }

        public List<Discrepancy> getAllPendingDiscrepancyUnder250()
        {
            return discrepancygf.getAllPendingDiscrepancyUnder250GF();
        }

        public List<Discrepancy> getAllPendingDiscrepancyOver250()
        {
            return discrepancygf.getAllPendingDiscrepancyOver250GF();
        }
        //supplier price--------------------------------------------------------------
        public SupplierPrice getSupplierPrice(string itemId, string supplierId)
        {
            return supplierPircegf.getSupplierPriceGF(itemId, supplierId);
        }

        public List<Supplier> getAllSupplier()
        {
            return supplierPircegf.getAllSupplier();
        }

        public Supplier getSupplierByName(string name)
        {
            return supplierPircegf.getSupplierByName(name);
        }

        #endregion



    }
}
