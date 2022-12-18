namespace testeraja.Models
{
    public class AddContactRequest
    {
        private string? fullName;
        public string? FullName
        {
            get { return this.fullName; }
            set
            {
                if (value?.Length <= 0)
                {
                    this.fullName = null;
                }
                else
                {
                    this.fullName = value;
                }
            }
        }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
    }
}
// aaa