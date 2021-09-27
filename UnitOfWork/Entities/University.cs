namespace UnitOfWork.Entities
{
    public class University
    {
        public string country { get; set; }
        public string name { get; set; }
        public string alpha_two_code { get; set; }
        public bool state_province { get; set; }
        /* Diziler Veritabanına yazılırken düz string türüne çevrilecektir. */
        public string[] domains { get; set; }
        public string[] web_pages { get; set; }

    }
}
