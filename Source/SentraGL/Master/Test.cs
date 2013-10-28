using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;

namespace SentraGL.Master
{
    public class Test : ParentEntity
    {
        public AutoUpdateBindingList<Akun> ListAkun;

        public string Akun;

        protected override void InitUI()
        {
            ListAkun = FastLoadEntities<Akun>("NoAkun,NamaAkun", string.Empty, "NoAkun", true);
        }

        protected override void EndUI()
        {
            ListAkun.Close();
        }
    }
}
