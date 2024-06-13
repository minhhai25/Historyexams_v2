namespace Historyexams.ModelViews
{
	public class CauHoiViewModel
	{
        public int Id { get; set; }
        public int? Idchuong { get; set; }
        public int? Idmucdo { get; set; }
		public string?  Macauhoi { get; set; }
        public string?  Noidung { get; set; }

        public bool IsChecked { get; set; }
    }
}
