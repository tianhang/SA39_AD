using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.Entities;

namespace ClassLibrary.GaoFan
{
    public class collectionPointGF
    {
        SSISDBEntities ctx = new SSISDBEntities();
        public List<CollectionPoint> getAllCollectionPointGF()
        {
            var item = from o in ctx.collectionPoints
                       select new
                       {
                           collectionPointId = o.collectionPointId,
                           adress = o.address,
                           time = o.time
                       };
            List<CollectionPoint> list = new List<CollectionPoint>();
            foreach (var x in item)
            {
                CollectionPoint cp = new CollectionPoint();
                cp.CollectionPointId = x.collectionPointId;
                cp.Address = x.adress;
                cp.Time = Convert.ToString(x.time);
                list.Add(cp);
            }
            return list;
            //gaofan
        }

         public CollectionPoint getCollection(string name)
        {
            CollectionPoint cp = new CollectionPoint();
            collectionPoint point = ctx.collectionPoints.FirstOrDefault(x => x.address == name);
            cp.CollectionPointId = point.collectionPointId;
            cp.Address = point.address;
            cp.Time = point.time;
            return cp;
        }

    }
}
