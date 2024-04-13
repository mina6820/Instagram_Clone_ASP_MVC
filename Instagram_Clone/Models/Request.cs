namespace Instagram_Clone.Models
{
    public class Request
    {
        public int Id { get; set; }
        public ApplicationUser Sender { get; set; }
        public ApplicationUser Receiver { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public bool IsAccepted { get; set; } = true;



    }
}
