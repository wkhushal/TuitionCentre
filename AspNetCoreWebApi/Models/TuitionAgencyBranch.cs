using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreWebApi.Models
{
    public class TuitionAgencyBranch
    {
        public long TuitionAgencyBranchId { get; set; }
        public long TuitionAgencyId { get; set; }
        public string BranchName { get; set; }
        public string BranchAddress { get; set; }
    }
}
