namespace Pure_Life.ViewModel.Termini
{
	public class GetTerminiViewModel
	{
		public int Id { get; set; }

		public string StartTime { get; set; }
        public double Price { get; set; }
        public string EndTime { get; set; }
        public string Status { get; set; }
		public string StatusPaid { get; set; }

		public int? PacientiId { get; set; }

		public string? PacientiName { get; set; }

		public string? PacientiLastName { get; set; }

		public string? PacientiNrTel { get; set; }

		public string? Doktori { get; set; }

		public string? Reparti { get; set; }

	}
}
