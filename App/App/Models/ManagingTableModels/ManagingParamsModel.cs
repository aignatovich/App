﻿namespace App.Models
{
    public class ManagingDateModel
    {
        public int Id { get; set; }

        public int Day { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public  Reason Reason { get; set; }

        public int UserId { get; set; }

        public int ProjectId { get; set; }
    }
}