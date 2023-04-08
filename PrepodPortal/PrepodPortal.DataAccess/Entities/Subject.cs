namespace PrepodPortal.DataAccess.Entities
{
    public class Subject
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public double HoursCount { get; set; }

        public string UserId { get; set; }

        public ApplicationUser? User { get; set; }
    }
}
