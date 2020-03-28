using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreWebApi.DTOs.Query
{
    public class TuitionAgencyBranchQueryDto
    {
        public TuitionAgencyBranchQueryDto(long branchId, long agencyId, string name, string address)
        {
            TuitionAgencyBranchId = branchId;
            TuitionAgencyId = agencyId;
            Name = name;
            Address = address;
        }

        public long TuitionAgencyBranchId { get; private set; }
        public long TuitionAgencyId { get; private set; }
        public string Name { get; private set; }
        public string Address { get; private set; }
    }
}
