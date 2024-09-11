﻿namespace ChoosenCareHome.Data.Model
{
    public class Message
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public Profile User { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool All { get; set; }
        public bool Read { get; set; }

        public DateTime Date { get; set; }
    }
}
