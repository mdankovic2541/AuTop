using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuTOP.Common
{
    public class Paging
    {
        private int rpp = 18;
        private int page;
        private bool dontPage = false;

        public Paging() { }
        public Paging(int page)
        {
            Page = page;
        }
        public Paging(bool dontPage)
        {
            DontPage = dontPage;
        }

        public int Rpp { get => rpp; set => rpp = value; }
        public int Page { get => page; set => page = value; }
        public bool DontPage { get => dontPage; set => dontPage = value; }

        public int GetStartElement()
        {
            return rpp * (page - 1);
        }
    }
}
