namespace AppWebDesbloqueos.Models
{
    public class DashboardViewModel
    {
        public List<KeyValuePair<string, int>> ListaCounts { get; set; }
        public List<KeyValuePair<string, int>> MesesCounts { get; set; }
        public List<KeyValuePair<string, int>> DiasCounts { get; set; }
        public List<KeyValuePair<string, int>> CnCounts { get; set; }
        public List<KeyValuePair<string, int>> PendientesCounts { get; set; }
        public List<KeyValuePair<string, int>> AtendidosHoyCounts { get; set; }
    }
}
