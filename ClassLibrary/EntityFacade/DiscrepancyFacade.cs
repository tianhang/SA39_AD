using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.Helper;
using System.Data.SqlClient;

namespace ClassLibrary.EntityFacade
{
    public class DiscrepancyFacade
    {
        SSISDBEntities em = new SSISDBEntities();

        public void addDiscrepancyItem(discrepancy d)
        {
            em.discrepancies.Add(d);//Discrepancy(class) is in entity pool.
            em.SaveChanges();
        }


        public void updateDiscrepancyItem(discrepancy d)
        {
            em.SaveChanges();
        }

        public List<discrepancy> getAddedDatas()
        {

            var query = from d in em.discrepancies
                        where d.status == "Added"
                        select d;
            return query.ToList();
        }


        public List<discrepancy> getSubmittedDatas()
        {

            var query = from d in em.discrepancies
                        where d.status == "Pending"
                        select d;
            return query.ToList();
        }


        public List<discrepancy> getAllDiscrepancies()
        {

            var query = from d in em.discrepancies
                        select d;
            return query.ToList();
        }

        public List<discrepancy> getNotAddedDiscrepancies()
        {

            var query = from d in em.discrepancies
                        where d.status != "Added"
                        select d;
            return query.ToList();
        }

        public string getItemID(string itemName)
        {

            var query = from i in em.items
                        where i.description == itemName
                        select i.itemId;
            return query.First();

        }

        public string getItemDescription(string itemId)
        {
            var query = from i in em.items
                        where i.itemId == itemId
                        select i.description;
            return query.First();
        }

        public List<string> getAllDescriptions()
        {
            var query = from i in em.items                        
                        select i.description;
            return query.ToList();
        }

        public string getCategoryId(string itemName)
        {
            var query = from i in em.items
                        where i.description == itemName
                        select i.categoryId;
            return query.First();
        }

        public string getCategoryName(string itemName)
        {
            string id = getCategoryId(itemName);
            var query = from i in em.categories
                        where i.categoryId == id
                        select i.name;
            return query.First();
        }

        public List<string> getSupplierID()
        {
            var query = from s in em.suppliers
                        select s.supplierId;
            return query.ToList();
        }

        public float getPrice(String supplierID, String itemID)
        {
            var query = from p in em.supplierPrices
                        where p.supplierId == supplierID && p.itemId == itemID
                        select p.price;
            return (float)query.First();
        }

        public string generateID()
        {
            var lastValueQuery = from lastValue in em.codeGenerators
                                 where lastValue.codeName == "discrepancyId"
                                 select lastValue.lastValue;
            int index = lastValueQuery.First();
            var prefixQuery = from prefix in em.codeGenerators
                              where prefix.codeName == "discrepancyId"
                              select prefix.prefix;
            string prefixValue = prefixQuery.First();

            em.codeGenerators.Find("CD3").lastValue = (index + 1);
            em.SaveChanges();


            string indexStr = (index + 1) + "";
            int times = 4 - indexStr.Length;
            string id = prefixValue;
            for (int i = 0; i < times; i++)
            {
                id += "0";
            }
            return id + indexStr;
        }

        //for mananger
        public List<discrepancy> selectMajor()
        {
            var query = from d in em.discrepancies
                        where d.amount >= 250
                        select d;
            return query.ToList();

        }

        //for supervisor
        public List<discrepancy> selectMinor()
        {
            var query = from d in em.discrepancies
                        where d.amount < 250
                        select d;
            return query.ToList();

        }


        public void updateDiscrepancyStatue(string statueEx, string statueNow)
        {
            var dList = from d in em.discrepancies
                        where d.status == statueEx
                        select d;
            foreach (var d in dList)
            {
                d.status = statueNow;
            }
            em.SaveChanges();
        }

        public void updateStatue(List<string> idList, string status, DateTime date)
        {
            foreach (string id in idList)
            {
                discrepancy d = em.discrepancies.First(i => i.discrepancyId == id);
                d.status = status;
                d.submitDate = date;
            }
            em.SaveChanges();
        }

        public void updateRejAppStatue(List<string> idList, string status, DateTime date)
        {
            foreach (string id in idList)
            {
                discrepancy d = em.discrepancies.First(i => i.discrepancyId == id);
                d.status = status;
                d.approveDate = date;
            }
            em.SaveChanges();
        }

        public void updateRejectReason(string id, string reason)
        {
            discrepancy d = em.discrepancies.First(i => i.discrepancyId == id);
            d.rejectReason = reason;
            em.SaveChanges();
        }

        public void deleteDiscrepancy(string id)
        {
            try
            {
                discrepancy dis = em.discrepancies.First(i => i.discrepancyId == id);
                em.discrepancies.Remove(dis);
                em.SaveChanges();
            }
            catch (SqlException exc)
            {
                Console.WriteLine(exc.Message);
            }
        }

        public void updateDiscrepancy(int qty, string reason, string id)
        {
            try
            {
                discrepancy dis = em.discrepancies.First(i => i.discrepancyId == id);
                dis.quantity = qty;
                dis.reason = reason;
                dis.amount = getPrice(dis.supplierId, dis.itemId) * qty;
                em.SaveChanges();
            }
            catch (SqlException exc)
            {
                Console.WriteLine(exc.Message);
            }
        }
    }
}
