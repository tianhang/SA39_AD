using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.Entities;

namespace ClassLibrary.GaoFan
{
    public class codeGeneratorGF
    {
        SSISDBEntities ctx = new SSISDBEntities();
        public int getLastValueByPrefixGF(string prefix)
        {
            codeGenerator cg = ctx.codeGenerators.FirstOrDefault(o => o.prefix == prefix);
            return cg.lastValue;
            //gaofan
        }

        public void updateCodeGeneratorGF(CodeGenerator cg) {
            codeGenerator c = ctx.codeGenerators.FirstOrDefault(o => o.prefix == cg.Prefix);
            c.lastValue = cg.LastValue;
            ctx.SaveChanges();
        }

        public CodeGenerator getCodeGF(string prefix)
        {
            codeGenerator cg = ctx.codeGenerators.FirstOrDefault(o => o.prefix == prefix);
            CodeGenerator cc = new CodeGenerator();
            cc.Prefix = cg.prefix;
            cc.LastValue = cg.lastValue;
            return cc;
        }
    }
}
