﻿namespace InterSMeet.Core.DTO
{
    public class CompanyDTO : UserDTO
    {
        public int CompanyId { get; set; }
        public string Address { get; set; } = null!;
        public string CompanyName { get; set; } = null!;
    }
}
