﻿namespace PYP_Task_VCard_WebApp.Models
{
    public class VCard
    {
        public int Id { get; set; }
        public string Firtname { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string? Image { get; set; }
    }
}
