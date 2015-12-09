﻿namespace App.Models
{
    public class ManagingRequest
    {
        public int? Month { get; set; }
        public int? Year { get; set; }
        public int? Sort { get; set; }
        public int? Page { get; set; }    
        public int? ProjectId { get; set; }
        public Roles? Role { get; set; }
    }
}