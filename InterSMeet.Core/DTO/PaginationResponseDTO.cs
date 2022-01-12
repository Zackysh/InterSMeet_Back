using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterSMeet.Core.DTO
{
    public class PaginationResponseDTO<T>
    {
        public BasePaginationDTO Pagination { get; set; } = null!;
        public IEnumerable<T> Items { get; set; } = null!;
    }
}
