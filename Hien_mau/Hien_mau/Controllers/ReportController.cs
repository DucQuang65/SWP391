using ClosedXML.Excel;
using Hien_mau.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hien_mau.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly Hien_mauContext _context;

        public ReportController(Hien_mauContext context)
        {
            _context = context;
        }

        // 1. Báo cáo thống kê tổng quan hệ thống
        [HttpGet("system-summary/excel")]
        public async Task<IActionResult> ExportSystemSummaryExcel()
        {
            var totalUsers = await _context.Users.CountAsync();
            var totalContents = await _context.Contents.CountAsync();
            var totalAppointments = await _context.Appointments.CountAsync();
            var totalBloodUnits = await _context.BloodInventories.SumAsync(i => i.Quantity);

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Tổng quan hệ thống");

            ws.Cell("A1").Value = "BÁO CÁO TỔNG QUAN HỆ THỐNG";
            ws.Cell("A3").Value = "Tổng số người dùng";
            ws.Cell("B3").Value = totalUsers;
            ws.Cell("A4").Value = "Tổng số bài đăng";
            ws.Cell("B4").Value = totalContents;
            ws.Cell("A5").Value = "Tổng số lịch hẹn";
            ws.Cell("B5").Value = totalAppointments;
            ws.Cell("A6").Value = "Tổng số đơn vị máu trong kho";
            ws.Cell("B6").Value = totalBloodUnits;

            ws.Columns().AdjustToContents();

            var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "BaoCao_TongQuan.xlsx");
        }

        // 2. Báo cáo kho máu (tỷ lệ nhóm máu)
        [HttpGet("blood-statistics/excel")]
        public async Task<IActionResult> ExportBloodStatisticsExcel()
        {
            var bloodStats = await _context.BloodInventories
                .GroupBy(i => i.BloodGroup + i.RhType)
                .Select(g => new { Group = g.Key, Quantity = g.Sum(x => x.Quantity) })
                .ToListAsync();

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Kho máu");

            ws.Cell("A1").Value = "BÁO CÁO THỐNG KÊ KHO MÁU";
            ws.Cell("A3").Value = "Nhóm máu";
            ws.Cell("B3").Value = "Số lượng";

            int row = 4;
            foreach (var item in bloodStats)
            {
                ws.Cell($"A{row}").Value = item.Group;
                ws.Cell($"B{row}").Value = item.Quantity;
                row++;
            }

            ws.Columns().AdjustToContents();

            var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "BaoCao_KhoMau.xlsx");
        }

        // 3. Báo cáo riêng cho admin
        [HttpGet("admin-summary/excel")]
        public async Task<IActionResult> ExportAdminSummaryExcel()
        {
            var userRoles = await _context.Users
                .Include(u => u.Role)
                .GroupBy(u => u.Role.RoleName)
                .Select(g => new { Role = g.Key, Count = g.Count() })
                .ToListAsync();

            var contentTypes = await _context.Contents
                .GroupBy(c => c.ContentType)
                .Select(g => new { Type = g.Key, Count = g.Count() })
                .ToListAsync();

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Admin tổng hợp");

            ws.Cell("A1").Value = "BÁO CÁO DÀNH CHO ADMIN";

            ws.Cell("A3").Value = "Người dùng theo vai trò";
            int row = 4;
            foreach (var r in userRoles)
            {
                ws.Cell($"A{row}").Value = r.Role;
                ws.Cell($"B{row}").Value = r.Count;
                row++;
            }

            row += 2;
            ws.Cell($"A{row}").Value = "Bài viết theo loại nội dung";
            row++;
            foreach (var c in contentTypes)
            {
                ws.Cell($"A{row}").Value = c.Type;
                ws.Cell($"B{row}").Value = c.Count;
                row++;
            }

            ws.Columns().AdjustToContents();

            var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "BaoCao_Admin.xlsx");
        }
    }
}
