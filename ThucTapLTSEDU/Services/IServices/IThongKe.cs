using ThucTapLTSEDU.PayLoads.DTOs;

namespace ThucTapLTSEDU.Services.IServices
{
    public interface IThongKe
    {
        Task<ThongKeDoanhThuDTOs> doanhThuTheoThang(int thang,int nam);
        Task<ThongKeDoanhThuDTOs> doanhThuTheoQuy(int quy, int nam);
        Task<ThongKeDoanhThuDTOs> doanhThuTheoNam(int nam);
    }
}
