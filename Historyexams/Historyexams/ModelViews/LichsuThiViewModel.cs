using Historyexams.Models;

namespace Historyexams.ModelViews
{
	public class LichsuThiViewModel
	{
		public int idde {  get; set; }
		public int idTaiKhoan { get; set; }
		public string? Noidung { get; set; }
		public string? PaA { get; set; }
		public string? PaB { get; set; }
		public string? PaC { get; set; }

		public string? PaD { get; set; }

		public string? PaDung { get; set; }
		List<Dtch> dtche { get; set; }
		public string PaChon { get; set; }
	}
}
