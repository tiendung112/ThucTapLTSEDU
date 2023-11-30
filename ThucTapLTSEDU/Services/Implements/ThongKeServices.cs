using ThucTapLTSEDU.Payloads.Responses;
using ThucTapLTSEDU.PayLoads.DTOs;
using ThucTapLTSEDU.Services.IServices;

namespace ThucTapLTSEDU.Services.Implements
{
    public class ThongKeServices : BaseServices, IThongKe
    {
        public async Task<ThongKeDoanhThuDTOs> doanhThuTheoNam(int nam)
        {
            var lstHoaDonDaThanhToan = context.Orders.Where(x => x.order_statusID == 2);
            var lstHoaDonTheoThang = lstHoaDonDaThanhToan
                .Where(x => x.created_at.Value.Year == nam);
            double? TongDoanhThu = 0;
            TongDoanhThu = lstHoaDonTheoThang.Sum(x => x.original_price);
            var SoLuongDon = lstHoaDonTheoThang.Count();
            return new ThongKeDoanhThuDTOs() { soLuongDon = SoLuongDon, tongDoanhThu = TongDoanhThu };
        }

        public async Task<ThongKeDoanhThuDTOs> doanhThuTheoQuy(int quy, int nam)
        {
            int thangBd, thangKT;
            if (quy == 1)
            {
                thangBd = 1; thangKT = 3;
            }
            else if (quy == 2)
            {
                thangBd = 4; thangKT = 6;
            }
            else if (quy == 3)
            {
                thangBd = 7; thangKT = 9;
            }
            else
            {
                thangBd = 10; thangKT = 12;
            }
            var lstHoaDonDaThanhToan = context.Orders.Where(x => x.order_statusID ==4);
            var lstHoaDonTheoThang = lstHoaDonDaThanhToan
                .Where(x => x.created_at.Value.Month >= thangBd && x.created_at.Value.Month <= thangKT && x.created_at.Value.Year == nam);
            double? TongDoanhThu = 0;
            TongDoanhThu = lstHoaDonTheoThang.Sum(x => x.original_price);
            var SoLuongDon = lstHoaDonTheoThang.Count();
            return new ThongKeDoanhThuDTOs() { soLuongDon = SoLuongDon, tongDoanhThu = TongDoanhThu };
        }

        public async Task<ThongKeDoanhThuDTOs> doanhThuTheoThang(int thang, int nam)
        {
            var lstHoaDonDaThanhToan = context.Orders.Where(x => x.order_statusID == 4);
            var lstHoaDonTheoThang = lstHoaDonDaThanhToan
                .Where(x => x.created_at.Value.Month == thang && x.created_at.Value.Year == nam);
            double? TongDoanhThu = 0;
            TongDoanhThu = lstHoaDonTheoThang.Sum(x => x.original_price);
            var SoLuongDon = lstHoaDonTheoThang.Count();
            return new ThongKeDoanhThuDTOs() { soLuongDon = SoLuongDon, tongDoanhThu = TongDoanhThu };
        }
    }
}
