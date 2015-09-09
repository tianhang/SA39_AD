using ClassLibrary.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.EntityFacade
{
    public class HelperFacade
    {
        SSISDBEntities ctx;
        ErrorLog errorobj;

        public HelperFacade()
        {
            ctx = new SSISDBEntities();
            errorobj = new ErrorLog();
        }

        public codeGenerator getCode(string codeName)
        {
            codeGenerator codeG;
            try
            {
                codeG = new codeGenerator();
                codeG = (from c in ctx.codeGenerators
                        where c.codeName == codeName
                        select c).FirstOrDefault();
            }
            catch (Exception exception)
            {
                errorobj.WriteErrorLog("HelperFacade-codeGenerator():::" + exception.ToString());
                codeG = new codeGenerator();
            }
            return codeG;
        }

        public void updateCode(codeGenerator codeGen)
        {
            try
            {
                codeGenerator codeG = (from c in ctx.codeGenerators
                         where c.codeId == codeGen.codeId
                         select c).FirstOrDefault();

                codeG.lastValue = codeGen.lastValue;
                ctx.SaveChanges();
            }
            catch (Exception exception)
            {
                errorobj.WriteErrorLog("HelperFacade-updateCode():::" + exception.ToString());
            }
        }

    }
}
